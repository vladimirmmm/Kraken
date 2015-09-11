using LogicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace UI.Services
{
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
                        json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyFactsPath);

                    }
                    result.Data = json;
                }
                if (part0 == "table")
                {
                    if (part1 == "get")
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

                        result.Data = GetTableHtmlPath(report, extension, row, column);
                    }
                    if (part1 == "list")
                    {
                        var h = new BaseModel.Hierarchy<LogicalModel.TableInfo>();
                        var tgsitems = Engine.CurrentTaxonomy.Module.TableGroups.Where(i => i.Item.TableIDs.Count > 0);
                        var tgs = tgsitems.Select(i => { 
                            var ti = new TableInfo();
                            ti.ID = i.Item.FilingIndicator; 
                            ti.Name = i.Item.FilingIndicator;
                            ti.Description = i.Item.LabelContent;
                            return ti; });
                        foreach (var tg in tgs)
                        {
                            var htg = new BaseModel.Hierarchy<LogicalModel.TableInfo>(tg);
                            htg.Item.Type = "tablegroup";

                            //h.Children.Add(htg);
                            var tables = Engine.CurrentTaxonomy.Tables.Where(i => i.FilingIndicator == tg.ID);

                            foreach (var tbl in tables)
                            {
                                var tbinfo = new TableInfo();
                                var ht = new BaseModel.Hierarchy<LogicalModel.TableInfo>(tbinfo);

                                ht.Item.ID = String.Format("{0}<>", tbl.ID);
                               // var name = tbl.Extensions.Count > 1 ? String.Format("{0}({1})", tbl.Name, tbl.Extensions.Count) : tbl.Name;
                                var name = tbl.Name;
                                ht.Item.Name = name;
                                ht.Item.Description = tbl.LabelContent;
                                ht.Item.Type = "table";
                                htg.Children.Add(ht);
                                foreach (var ext in tbl.Extensions)
                                {
                                    var extinfo = new TableInfo();
                                    extinfo.ID = String.Format("{0}<{1}>", tbl.ID, ext.LabelCode);
                                    extinfo.Name = ext.LabelContent;
                                    extinfo.Description = ext.LabelCode;
                                    var hext = new BaseModel.Hierarchy<LogicalModel.TableInfo>(extinfo);
                                    hext.Item.Type = "extension";
                                    ht.Children.Add(hext);
                                }
                            }
                            if (tables.Count() == 1)
                            {
                                h.Children.Add(htg.Children.FirstOrDefault());
                            }
                            else
                            {
                                h.Children.Add(htg);
                            }


                        }
                        result.Data = Utilities.Converters.ToJson(h);
                    }
                }
            }
            Logger.WriteLine("Finished " + request.Url);

            return result;
        }

        private string GetTableHtmlPath(string report, string extension, string row, string column)
        {
            var table = Engine.CurrentTaxonomy.Tables.FirstOrDefault(i => i.ID.ToLower() == report.ToLower());
            var url = "";
            if (table != null)
            {
                table.CurrentExtension = table.Extensions.FirstOrDefault(i => i.LabelCode == extension);
                table.EnsureHtmlLayout();
                url = String.Format("{0}#ext={1};cell=R{2}_C{3};", table.FullHtmlPath, extension, row, column);

                //UI.Browser.Navigate(url);
                //UI.TB_Title.Text = String.Format("{0} - {1}", table.ID, table.HtmlPath);
            }
            return url;
        }
  
    }
}
