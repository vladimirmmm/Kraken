using BaseModel;
using LogicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Engine.Services
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
        private AppEngine AppEngine = null;
        private TaxonomyEngine Engine = null;
        public DataService(AppEngine engine)
        {
            AppEngine = engine;
            Engine = engine.Features.Engine;
        }
        public Message ProcessRequest(Message request)
        {
            var ajaxtag = "ajax";

          
            //Logger.WriteLine("ProcessRequest " + request.Url);
            Message result = new Message();
            result.Id = request.Id;
            result.Url = request.Url;
            result.Error = "";
            result.ContentType = request.ContentType;
            result.Category = request.Category;
            if (request.Category == "notification") 
            {
                Utilities.Logger.WriteLine(request.Data);
                if (request.Data.ToLower().Contains("ui ready")) {
                    AppEngine.Features.OnUIReady();
                    //AppEngine.Features.LoadTaxonomyToUI();

                }
                
            }
            if (request.Category == ajaxtag)
            {
                if (Utilities.FS.FileExists(request.Url))
                {
                    result.Data = Utilities.FS.ReadAllText(request.Url);
                }
                else
                {
                    if (request.Url.EndsWith(".html"))
                    {

                        var table = Engine.CurrentTaxonomy.Tables.FirstOrDefault(i => i.HtmlPath == request.Url);
                        if (table != null)
                        {
                            result.Data = Utilities.FS.ReadAllText(table.FullHtmlPath);

                        }


                    }
                    var urlparts = request.Url.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    if (urlparts.Length == 2)
                    {
                        var part0 = urlparts[0].ToLower();
                        var part1 = urlparts[1].ToLower();
                        if (part0 == "settings")
                        {
                            var settings = Settings.Current;

                            if (part1 == "get")
                            {
                                result.Data = settings.GetJsonObj();
                            }
                            if (part1 == "save")
                            {

                                settings.CheckValidationCells = request.GetParameter<bool>("CheckValidationCells");
                                settings.ReDownloadFiles = request.GetParameter<bool>("ReDownloadFiles");
                                settings.ReloadFullTaxonomy = request.GetParameter<bool>("ReloadFullTaxonomy");
                                settings.ReloadFullTaxonomyButStructure = request.GetParameter<bool>("ReloadFullTaxonomyButStructure");
                                if (settings.ReloadFullTaxonomy)
                                {
                                    settings.ReloadFullTaxonomyButStructure = true;
                                }
                                settings.ReloadTaxonomyOnInstanceLoaded = request.GetParameter<bool>("ReloadTaxonomyOnInstanceLoaded");
                                settings.ValidateOnInstanceLoaded = request.GetParameter<bool>("ValidateOnInstanceLoaded");

                                this.AppEngine.Features.SaveSettings();
                            }
                        }
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
                            var url = request.Url.Replace("/", "\\");
                            var content = System.IO.File.ReadAllText(url);
                            result.Data = content;
                        }
                        if (part0 == "ui")
                        {
                            if (part1 == "menu")
                            {
                                var commandid = request.GetParameter("command").ToLower();
                                var p1 = request.GetParameter("p1").ToLower();
                                if (!string.IsNullOrEmpty(commandid))
                                {
                                    var command = this.AppEngine.Features.CommandContainer.Where(i => i.ID.ToLower() == commandid).FirstOrDefault();
                                    if (command != null)
                                    {
                                        var originalca = command.ContextAccessor;
                                        command.ContextAccessor = () => { return new object[] { p1 }; };
                                        command.Execute();
                                        command.ContextAccessor = originalca;
                                    }
                                }
                                else
                                {
                                    var json = AppEngine.Features.GetJsonObj(AppEngine.Features.CommandContainer);
                                    result.Data = json;
                                }
                            }
                        }
                        if (part0 == "browse")
                        {
                            var json = "";
                            if (part1 == "file")
                            {
                                json = AppEngine.Features.UI.BrowseFile("", "");
                            }
                            if (part1 == "folder")
                            {
                                json = AppEngine.Features.UI.BrowseFolder("");

                            }
                            result.Data = json;
                        }
                        if (part0 == "app")
                        {
                            var json = "";
                            if (part1 == "info")
                            {
                                json = AppEngine.Features.GetAppInfo();
                            }
                            result.Data = json;
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
                                    var page = int.Parse(request.GetParameter("page"));
                                    var pagesize = int.Parse(request.GetParameter("pagesize"));
                                    var factstring = request.GetParameter("factstring").ToLower();
                                    var cellid = request.GetParameter("cellid").ToLower();
                                    var rs = new DataResult<KeyValue>();
                                    var query = Engine.CurrentTaxonomy.Facts.AsQueryable();

                                    var factstrings = factstring.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                    //TODO
                                    var taxonomy = AppEngine.Features.Engine.CurrentTaxonomy;
                                    foreach (var fs in factstrings)
                                    {
                                        query = query.Where(i => taxonomy.KeyContains(i.Key, fs));
                                    }
                                    if (!String.IsNullOrEmpty(cellid))
                                    {
                                        query = query.Where(i => i.Value.Any(j => j.Contains(cellid)));
                                    }

                                    rs.Items = query.Skip(pagesize * page).Take(pagesize).Select(i=>new KeyValue(taxonomy.GetFactStringKey(i.Key),i.Value)).ToList();
                                    rs.Total = query.Count();
                                    json = Utilities.Converters.ToJson(rs);
                                    
                                }
                                if (part1 == "table")
                                {
                                    if (request.Parameters.ContainsKey("cell"))
                                    {
                                        var cell = request.Parameters["cell"];
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
                                    if (request.Parameters.ContainsKey("item"))
                                    {
                                        var item = request.Parameters["item"];
                                        if (item == "factmap")
                                        {
                                            var reportid = request.Parameters["reportid"];
                                            var table = this.Engine.CurrentTaxonomy.Tables.FirstOrDefault(i => i.ID == reportid);
                                            if (table != null)
                                            {

                                                json = Utilities.FS.ReadAllText(table.FactMapPath);
                                            }
                                        }

                                    }
                                }
                                if (part1 == "tables")
                                {
                                    var h = new BaseModel.Hierarchy<TableInfo>();
                                    var tablegroups = Engine.CurrentTaxonomy.Module.TableGroups.Cast<TableInfo>(i =>
                                    {
                                        var ti = new TableInfo();
                                        ti.ID = i.ID;// String.IsNullOrEmpty(i.FilingIndicator) ? i.ID : i.FilingIndicator;
                                        ti.Tables = i.TableIDs;
                                        //var name = String.IsNullOrEmpty(i.LabelCode) ? i.LabelContent : i.LabelCode;
                                        var name = String.IsNullOrEmpty(i.FilingIndicator) ? i.LabelContent : i.FilingIndicator;
                                        ti.Name = Utilities.Strings.TrimTo(name, 40);
                                        ti.Description = i.LabelContent;
                                        ti.Type = "tablegroup";
                                        return ti;
                                    });

                                    var tgs = tablegroups.All();

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
                                                ht = new BaseModel.Hierarchy<TableInfo>(tbinfo);
                                                htg.Children.Add(ht);
                                            }

                                            ht.Item.ID = String.Format("{0}<>", tbl.ID);
                                            ht.Item.HasData = tbl.InstanceFactsCount;
                                            // var name = tbl.Extensions.Count > 1 ? String.Format("{0}({1})", tbl.Name, tbl.Extensions.Count) : tbl.Name;
                                            var name = tbl.Name;
                                            ht.Item.Name = Utilities.Strings.TrimTo(name, 40);
                                            ht.Item.Description = string.IsNullOrEmpty(tbl.LabelContent) ? name : tbl.LabelContent;
                                            ht.Item.Type = "table";
                                            //TODO EXT
                                            //var tbextensions = tbl.Extensions.Children;
                                            var tbextensions = Engine.CurrentInstance.GetTableExtensions(tbl);


                                            //var extensions = tbextensions.Select(i =>
                                            //{
                                            //    var ti = new TableInfo();
                                            //    ti.ID = String.Format("{0}<{1}>", tbl.ID, i.Item.LabelCode);
                                            //    ti.Name = i.Item.LabelContent;
                                            //    ti.Description = i.Item.LabelContent;
                                            //    ti.Type = "extension";
                                            //    return ti;
                                            //});
                                            //extensions.Item.Type = "extension";
                                            //foreach(var ext in extensions.ch)
                                            ht.Children.AddRange(tbextensions.Select(i => new BaseModel.Hierarchy<TableInfo>(i)));


                                        }



                                    }
                                    h = tablegroups;

                                    json = Utilities.Converters.ToJson(h);
                                }
                            }
                            result.Data = json;
                        }

                    }
                }
                //Logger.WriteLine("Finished " + request.Url);
            }
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
                url = String.Format("{0}#report={1};ext={2};cell={3}_{4};", table.FullHtmlPath, report, extension, row, column);

                //UI.Browser.Navigate(url);
                //UI.TB_Title.Text = String.Format("{0} - {1}", table.ID, table.HtmlPath);
            }
            return url;
        }
  
    }
    public class Test{
        public void TestX() 
        {
            var files = System.IO.Directory.GetFiles(@"C:\My\XBRL\Taxonomies\Israel-XBRL_26.10.2015\layout");
            var sb = new StringBuilder();
            foreach (var file in files) 
            {
                sb.AppendLine(System.IO.File.ReadAllText(file));
                sb.AppendLine("</br>");
            }
            System.IO.File.WriteAllText("all.html", sb.ToString());
        }
    
    }
}
