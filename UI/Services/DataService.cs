using LogicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace UI.Services
{
    public class DataResult<T> 
    {
        private List<T> _Items = new List<T>();
        public List<T> Items { get { return _Items; } set { _Items = value; } }
        public T Item { get; set; }
        public int Total { get; set; }
    }
    public class DataService
    {
        private TaxonomyEngine Engine=null;
        public DataService(TaxonomyEngine engine)
        {
            Engine = engine;
        }
        public Message ProcessRequest(Message request)
        {
            Logger.WriteLine("ProcessRequest " + request.Url);
            Message result = new Message();
            result.Id = request.Id;
            result.Url = request.Url;
            result.Error = "";
            result.ContentType = request.ContentType;
            result.Category = request.Category;
            var urlparts = request.Url.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (urlparts.Length == 2)
            {
                var part0 = urlparts[0].ToLower();
                var part1 = urlparts[1].ToLower();
                if (part0 == "instance")
                {
                    if (Engine.CurrentInstance != null)
                    {
                        if (part1 == "get")
                        {
                            var json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.CurrentInstancePath);
                            result.Data = json;
                        }
                        if (part1 == "validation")
                        {
                            var json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.CurrentInstanceValidationResultPath);
                            result.Data = json;

                        }
                    }
                }
                if (part0 == "layout")
                {
                    var url = request.Url.Replace("/","\\");
                    var content = System.IO.File.ReadAllText(url);
                    result.Data = content;
                }
                if (part0 == "taxonomy")
                {
                    var json = "";
                    if (Engine.CurrentTaxonomy != null)
                    {
                        if (part1 == "get")
                        {
                            json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyModulePath);
                        }
                        if (part1 == "concepts")
                        {
                            json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyConceptPath);

                        }
                        if (part1 == "validationrules")
                        {
                            json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomySimpleValidationPath);

                        }
                        if (part1 == "validationrule")
                        {
                            var id = request.Parameters["id"];
                            var rule = Engine.CurrentTaxonomy.ValidationRules.FirstOrDefault(i => i.ID == id);
                            if (rule != null)
                            {
                                var results = Engine.CurrentInstance.Facts.Count > 0 ? rule.GetAllInstanceResults(Engine.CurrentInstance) : rule.GetAllResults();
                                json = Utilities.Converters.ToJson(results);
                            }
                        }
                        if (part1 == "hierarchies")
                        {
                            json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyHierarchyPath);

                        }
                        if (part1 == "labels")
                        {
                            json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyLabelPath);

                        }
                        if (part1 == "facts")
                        {
                            var page = int.Parse(request.Parameters["page"]);
                            var pagesize = int.Parse(request.Parameters["pagesize"]);
                            var rs = new DataResult<KeyValuePair<string,List<String>>>();
                            var query =Engine.CurrentTaxonomy.Facts.Where(i => i.Key == i.Key);
                            rs.Items = query.Skip(pagesize * page).Take(pagesize * (page + 1)).ToList();
                            rs.Total = query.Count();
                            //json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyFactsPath);
                            json = Utilities.Converters.ToJson(rs);

                        }
                        if (part1 == "table")
                        {
                            var cell = request.Parameters["cell"];
                            //var cell = command.Substring(command.IndexOf(":") + 1);
                            var report = "";
                            var extension = "";
                            var row = "";
                            var column = "";
                            if (cell.IndexOf(">") > -1)
                            {
                                report = cell.Remove(cell.IndexOf("<"));
                                var cellspecifiers = cell.TextBetween("<", ">").Split('|');
                                var nr_params = cellspecifiers.Length;
                                extension = nr_params > 0 ? cellspecifiers[0] : "";
                                row = nr_params > 1 ? cellspecifiers[1] : "";
                                column = nr_params > 2 ? cellspecifiers[2] : "";
                            }
                            else
                            {
                                report = cell;
                            }

                            json = GetTableHtmlPath(report, extension, row, column);
                        }
                        if (part1 == "tables")
                        {
                            var h = new BaseModel.Hierarchy<LogicalModel.TableInfo>();
                            var tablegroups = Engine.CurrentTaxonomy.Module.TableGroups.Cast<LogicalModel.TableInfo>(i =>
                            {
                                var ti = new TableInfo();
                                ti.ID = i.ID;// String.IsNullOrEmpty(i.FilingIndicator) ? i.ID : i.FilingIndicator;
                                ti.Tables = i.TableIDs;
                                //var name = String.IsNullOrEmpty(i.LabelCode) ? i.LabelContent : i.LabelCode;
                                var name = i.LabelContent;
                                ti.Name = name;
                                ti.Description = i.LabelContent;
                                ti.Type = "tablegroup";
                                return ti;
                            });
                            var tgs = tablegroups.All();
                            //var tgsitems = Engine.CurrentTaxonomy.Module.TableGroups.Where(i => i.Item.TableIDs.Count > 0);
                            //var tgs = tgsitems.Select(i => { 
                            //    var ti = new TableInfo();
                            //    ti.ID = i.Item.FilingIndicator; 
                            //    ti.Name = i.Item.FilingIndicator;
                            //    ti.Description = i.Item.LabelContent;
                            //    return ti; });
                            foreach (var tgcontainer in tgs)
                            {
                                var tg = tgcontainer.Item;
                                var htg = tgcontainer;// new BaseModel.Hierarchy<LogicalModel.TableInfo>(tg);

                                //h.Children.Add(htg);
                                //var tables = Engine.CurrentTaxonomy.Tables.Where(i => i.FilingIndicator == tg.ID);
                                var tables = Engine.CurrentTaxonomy.Tables.Where(i => tg.Tables.Contains(i.ID)).ToList();
                                if (tables.Count > 0)
                                {
                                    //htg.Children.Clear();
                                }
                                foreach (var tbl in tables)
                                {
                                    var tbinfo = new TableInfo();
                                    var ht = htg.Children.FirstOrDefault(i => i.Item.ID == tbl.ID);
                                    if (ht == null)
                                    {
                                        ht = new BaseModel.Hierarchy<LogicalModel.TableInfo>(tbinfo);
                                        htg.Children.Add(ht);
                                    }

                                    ht.Item.ID = String.Format("{0}<>", tbl.ID);
                                    // var name = tbl.Extensions.Count > 1 ? String.Format("{0}({1})", tbl.Name, tbl.Extensions.Count) : tbl.Name;
                                    var name = tbl.Name;
                                    ht.Item.Name = Utilities.Strings.TrimTo(name, 40);
                                    ht.Item.Description = string.IsNullOrEmpty(tbl.LabelContent) ? name : tbl.LabelContent;
                                    ht.Item.Type = "table";
                                    //TODO EXT
                                    /*
                                    var extensions = tbl.Extensions.Where(i=>i.LabelCode!=Table.DefaultExtensionCode).ToList();
                                    foreach (var ext in extensions)
                                    {
                                        var extinfo = new TableInfo();
                                        extinfo.ID = String.Format("{0}<{1}>", tbl.ID, ext.LabelCode);
                                        extinfo.Name = ext.LabelContent;
                                        extinfo.Description = ext.LabelCode;
                                        var hext = new BaseModel.Hierarchy<LogicalModel.TableInfo>(extinfo);
                                        hext.Item.Type = "extension";
                                        ht.Children.Add(hext);
                                    }
                                    */
                                }



                            }
                            h = tablegroups;

                            json = Utilities.Converters.ToJson(h);
                        }
                    }
                    result.Data = json;
                }

            }
            //Logger.WriteLine("Finished " + request.Url);

            return result;
        }

        private string GetTableHtmlPath(string report, string extension, string row, string column)
        {
            var table = Engine.CurrentTaxonomy.Tables.FirstOrDefault(i => i.ID.ToLower() == report.ToLower());
            var url = "";
            if (table != null)
            {
                var hext = table.Extensions.FirstOrDefault(i => i.Item.LabelCode == extension);
                if (hext!=null){
                   // table.CurrentExtension = hext.Item;
                }
                table.EnsureHtmlLayout();
                url = String.Format("{0}#ext={1};cell=R{2}_C{3};", table.FullHtmlPath, extension, row, column);

                //UI.Browser.Navigate(url);
                //UI.TB_Title.Text = String.Format("{0} - {1}", table.ID, table.HtmlPath);
            }
            return url;
        }
  
    }
}
