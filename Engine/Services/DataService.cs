using BaseModel;
using LogicalModel;
using LogicalModel.Validation;
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
                    var filextension="";
                    var extix = request.Url.LastIndexOf(".");
                    if (extix > -1)
                    {
                        filextension = request.Url.Substring(extix + 1);
                    }
                    if (filextension.In("html"))
                    {
                        var table = Engine.CurrentTaxonomy.Tables.FirstOrDefault(i => i.HtmlPath == request.Url);
                        if (table != null)
                        {
                            result.Data = Utilities.FS.ReadAllText(table.FullHtmlPath);
                        }
                    }

                    if (filextension.In("xml", "xsd"))
                    {
                        var doc = Engine.CurrentTaxonomy.TaxonomyDocuments.FirstOrDefault(i => i.LocalRelPath == request.Url);
                        if (doc != null)
                        {
                            result.Data = Utilities.FS.ReadAllText(doc.LocalPath);
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
                                var keys = settings.Keys.ToList();

                                foreach (var key in keys) 
                                {
                                    var value = request.GetParameter(key);
                                    settings.SetValue(key, value);
                                }
                                //settings.CheckValidationCells = request.GetParameter<bool>("CheckValidationCells");
                                //settings.ReDownloadFiles = request.GetParameter<bool>("ReDownloadFiles");
                                //settings.ReloadFullTaxonomy = request.GetParameter<bool>("ReloadFullTaxonomy");
                                //settings.ReloadFullTaxonomyButStructure = request.GetParameter<bool>("ReloadFullTaxonomyButStructure");
                                if (settings.ReloadFullTaxonomy)
                                {
                                    settings.ReloadFullTaxonomyButStructure = true;
                                }
                                //settings.ReloadTaxonomyOnInstanceLoaded = request.GetParameter<bool>("ReloadTaxonomyOnInstanceLoaded");
                                //settings.ValidateOnInstanceLoaded = request.GetParameter<bool>("ValidateOnInstanceLoaded");

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
                                    json = json.Replace("\r\n", "");

                                    result.Data = json;
                                }
                                if (part1 == "save")
                                {
                                    var factsjson = request.GetParameter("facts");
                                    var facts = Utilities.Converters.JsonTo<List<InstanceFact>>(factsjson);
                                    var json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.CurrentInstancePath);
                                    Engine.CurrentInstance.SaveFacts(facts);
                                    this.AppEngine.Features.SaveInstance("");
                                    //result.Data = "Ok";
                                }
                                if (part1 == "validation")
                                {
                                    var json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.CurrentInstanceValidationResultPath);
                                    result.Data = json;

                                }
                                if (part1 == "factindexes")
                                {
                                    var indexes_str = request.GetParameter("indexes").ToLower();
                                    var indexes = indexes_str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    var items = Engine.CurrentInstance.GetFactStringsByFactIdStrings(indexes);
                                    var json = Utilities.Converters.ToJson(items);
                                    result.Data = json;
                                }
                                if (part1 == "validationresults") 
                                {
                                    var page = int.Parse(request.GetParameter("page"));
                                    var pagesize = int.Parse(request.GetParameter("pagesize"));
                                    var ruleid = request.GetParameter("ruleid").ToLower();
                                    var full = request.GetParameter("full").ToLower();

                                    var taxonomy = AppEngine.Features.Engine.CurrentTaxonomy;
                                    var instance = AppEngine.Features.Engine.CurrentInstance;

                                    var rs = new DataResult<LogicalModel.Validation.ValidationRuleResult>();
                                    var query = Engine.CurrentInstance.ValidationRuleResults.AsQueryable();
                                    if (full == "1")
                                    { 
                                        SimpleValidationRule rule= taxonomy.ValidationRules.FirstOrDefault(i=>i.ID.Equals(ruleid,StringComparison.OrdinalIgnoreCase));
                                        if (rule == null)
                                        {
                                            rule = taxonomy.SimpleValidationRules.FirstOrDefault(i => i.ID.Equals(ruleid, StringComparison.OrdinalIgnoreCase));
                                        }
                                        if (rule != null)
                                        {
                                            query = rule.GetAllInstanceResults(instance).AsQueryable();
                                        }
                                        else 
                                        {
                                            Utilities.Logger.WriteLine(String.Format("Rule {0} was not Found (DataService)",ruleid));
                                        }
                                    }
                                    query = query.Where(i => i.ID.Equals(ruleid, StringComparison.OrdinalIgnoreCase));
                                    //TODO
                              
                                    var idlist = new List<int>();


                                    rs.Items = query.Skip(pagesize * page).Take(pagesize).ToList();
                                    foreach (var item in rs.Items) 
                                    {
                                        foreach (var p in item.Parameters) 
                                        {
                                            foreach (var fid in p.FactIDs)
                                            {
                                                var fact = instance.GetFactBaseByIndexString(fid);
                                                if (fact != null)
                                                {
                                                    //p.Facts.Add(fact.FactString);
                                                }
                                            }
                                        }
                                    }
                                    rs.Total = query.Count();
                                    var json = Utilities.Converters.ToJson(rs);
                                    foreach (var item in rs.Items)
                                    {
                                        foreach (var p in item.Parameters)
                                        {
                                            p.InstanceFacts.Clear();
                                        }
                                    }
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
                                if (part1 == "cellindexes")
                                {
                                    json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyCellIndexPath);
                              
                                }
                                if (part1 == "documents")
                                {
                                    json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyStructurePath);
                                    if (Utilities.FS.FileExists( Engine.CurrentInstance.FullPath))
                                    {
                                        var td = new TaxonomyDocument();
                                        td.LocalRelPath = Engine.CurrentInstance.FullPath;
                                        td.FileName = Utilities.Strings.GetFileName(td.LocalRelPath);
                                        var instjson = Utilities.Converters.ToJson(td);
                                        json = json.Insert(json.IndexOf("[") + 1, instjson+", ");
                                    }

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
                                    var page = int.Parse(request.GetParameter("page"));
                                    var pagesize = int.Parse(request.GetParameter("pagesize"));
                                    var key = request.GetParameter("key");
                                    var code = request.GetParameter("code");
                                    var content = request.GetParameter("content");
                                    var query = AppEngine.Features.Engine.CurrentTaxonomy.TaxonomyLabels.AsQueryable();
                                    if (!String.IsNullOrEmpty(key)) 
                                    {
                                        query = query.Where(i => i.Key.IndexOf(key, StringComparison.InvariantCultureIgnoreCase) > -1);
                                    }
                                    if (!String.IsNullOrEmpty(code))
                                    {
                                        query = query.Where(i => i.Code.IndexOf(code, StringComparison.InvariantCultureIgnoreCase) > -1);
                                    }
                                    if (!String.IsNullOrEmpty(content))
                                    {
                                        query = query.Where(i => i.Content.IndexOf(content, StringComparison.InvariantCultureIgnoreCase) > -1);
                                    }
                                    var rs = new DataResult<Label>();
                                    rs.Items = query.Skip(pagesize * page).Take(pagesize).ToList();
                                    rs.Total = query.Count();
                                    json = Utilities.Converters.ToJson(rs);

                                }
                                if (part1 == "facts")
                                {
                                    var page = int.Parse(request.GetParameter("page"));
                                    var pagesize = int.Parse(request.GetParameter("pagesize"));
                                    var factstring = request.GetParameter("factstring");
                                    var cellid = request.GetParameter("cellid").ToLower();
                                    var rs = new DataResult<KeyValue>();
                                    var query = Engine.CurrentTaxonomy.FactIndexEnumerable();

                                    var factstrings = factstring.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                    //TODO
                                    var taxonomy = AppEngine.Features.Engine.CurrentTaxonomy;
                                    IList<int> idlist = new List<int>();
                                    var keys = new List<int>();
                                    var qry = new LogicalModel.Base.FactBaseQuery();
                                    foreach (var fs in factstrings)
                                    {
                                        var ors = fs.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                                        if (ors.Length == 1)
                                        {
                                            var oritem = ors.FirstOrDefault();
                                            var isnegative = false;
                                            if (oritem.StartsWith("!"))
                                            {
                                                isnegative = true;
                                                oritem = oritem.Substring(1);
                                            }
                                            if (taxonomy.FactParts.ContainsKey(oritem))
                                            {
                                                qry.AddIndex(taxonomy.FactParts[oritem], isnegative);
                                            }
                                            else
                                            {
                                                var partkeys = taxonomy.FactParts.Keys.Where(i => i.IndexOf(oritem, StringComparison.InvariantCultureIgnoreCase)>-1).ToList();
                                                if (partkeys.Count > 0)
                                                {
                                                    if (isnegative)
                                                    {
                                                        foreach (var partkey in partkeys)
                                                        {
                                                            var partqry = new LogicalModel.Base.FactBaseQuery();
                                                            qry.AddIndex(taxonomy.FactParts[partkey], isnegative);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var qrypool = new LogicalModel.Base.FactPoolQuery();
                                                        foreach (var partkey in partkeys)
                                                        {
                                                            var partqry = new LogicalModel.Base.FactBaseQuery();
                                                            partqry.AddIndex(taxonomy.FactParts[partkey], isnegative);
                                                            qrypool.AddChildQuery(partqry);
                                                        }
                                                        qry.AddChildQuery(qrypool);
                                                    }


                                                }
                                                else 
                                                {
                                                    qry.DictFilterIndexes.Add(-1);
                                                }

                                            }
                                        }
                                        else 
                                        {
                                            var qrypool = new LogicalModel.Base.FactPoolQuery();
                                            var isnegative = false;

                                            for (int i = 0; i < ors.Length;i++)
                                            {
                                                var oritem = ors[i];
                                                if (oritem.StartsWith("!"))
                                                {
                                                    isnegative = true;
                                                    oritem = oritem.Substring(1);
                                                }

                                                LogicalModel.Base.FactBaseQuery subquery = null;
                                                if (taxonomy.FactParts.ContainsKey(oritem))
                                                {
                                                    subquery = new LogicalModel.Base.FactBaseQuery();
                                                    subquery.AddIndex(taxonomy.FactParts[oritem],isnegative);
                                                }
                                                else
                                                {
                                                    var partkeys = taxonomy.FactParts.Keys.Where(k => k.IndexOf(oritem,StringComparison.InvariantCultureIgnoreCase)>-1).ToList();
                                                    if (partkeys.Count > 0)
                                                    {
                                                        if (isnegative)
                                                        {
                                                            subquery = new LogicalModel.Base.FactBaseQuery();

                                                            foreach (var partkey in partkeys)
                                                            {
                                                                var partqry = new LogicalModel.Base.FactBaseQuery();
                                                                subquery.AddIndex(taxonomy.FactParts[partkey], isnegative);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            subquery = new LogicalModel.Base.FactPoolQuery();
                                                            foreach (var partkey in partkeys)
                                                            {
                                                                var partqry = new LogicalModel.Base.FactBaseQuery();
                                                                partqry.AddIndex(taxonomy.FactParts[partkey], isnegative);
                                                                subquery.AddChildQuery(partqry);
                                                            }
                                                        }


                                                       
                                                    }
                                                    else
                                                    {
                                                        qry.DictFilterIndexes.Add(-1);
                                                    }
                                                }
                                                qrypool.AddChildQuery(subquery);
                                            }
                                            qry.AddChildQuery(qrypool);
                                        }

                                    
                                       

                                    }
                                    if (factstrings.Length > 0)
                                    {
                                        var all = new Interval(0, taxonomy.FactsManager.FactsOfPages.HashKeys.Count);
                                        var allist = new IntervalList();
                                        allist.AddInterval(all);
                                        var idlist2 = qry.EnumerateIntervals(taxonomy.FactsOfParts, 0, allist, false).SelectMany(i => i);

                                        var indexes = idlist2.Select(i => i).Distinct().ToDictionary(k => k, v => true);
                                        var comparer = new Utilities.IntArrayEqualityComparer();
                                        query = query.Where(i => indexes.ContainsKey(i));
                                    }
                                    if (!String.IsNullOrEmpty(cellid))
                                    {
                                        //var cells = taxonomy.CellIndexDictionary.Where(i => i.Value.IndexOf(cellid, StringComparison.Ordinal)>-1).Select(i=>i.Key);
                                        //longcellid=cellid
                                        //query = query.Where(i => i.Value.Any(j => cells.Contains(j)));
                                        query = query.Where(i => taxonomy.FactsManager.GetFact(i).Value.Any(j=>taxonomy.CellIndexDictionary[j].IndexOf(cellid, StringComparison.OrdinalIgnoreCase)>-1));
                                    }

                                    rs.Items = query.Skip(pagesize * page).Take(pagesize).Select(i =>
                                            {
                                                var keycell = taxonomy.FactsManager.GetFactKeywithCells(i);
                                                var keystr = taxonomy.GetFactStringKey(keycell.FactKey);
                                                return new KeyValue(keystr, keycell.CellIndexes);//.Select(c => taxonomy.CellIndexDictionary[c]).ToList()
                                            }
                                        ).ToList();
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
                                    var tablesingroups = tgs.SelectMany(i => i.Item.Tables).Distinct().ToList();
                                    var alltableids = Engine.CurrentTaxonomy.Tables.Select(i => i.ID).ToList();
                                    var ungrouped = alltableids.Except(tablesingroups).ToList();
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
                                            var name = tbl.Name;
                                            ht.Item.Name = Utilities.Strings.TrimTo(name, 40);
                                            ht.Item.Description = string.IsNullOrEmpty(tbl.LabelContent) ? name : tbl.LabelContent;
                                            ht.Item.Type = "table";
                                            //TODO EXT
                                            //var tbextensions = tbl.Extensions.Children;
                                            var tbextensions = Engine.CurrentInstance.GetTableExtensions(tbl);
                                            ht.Children.AddRange(tbextensions.Select(i => new BaseModel.Hierarchy<TableInfo>(i)));


                                        }



                                    }
                                    h = tablegroups;
                                    if (ungrouped.Count > 0) 
                                    {
                                        var ungroupedti = new TableInfo();
                                        ungroupedti.Type = "tablegroup";
                                        ungroupedti.Name = "Ungrouped";
                                        var hungr = new Hierarchy<TableInfo>(ungroupedti);
                                        h.AddChild(hungr);
                                        foreach (var t in ungrouped) 
                                        {
                                            var tbinfo = new TableInfo();
                                            var tbl = Engine.CurrentTaxonomy.Tables.FirstOrDefault(i => i.ID == t);
                                            var ht = new BaseModel.Hierarchy<TableInfo>(tbinfo);
        

                                            ht.Item.ID = String.Format("{0}<>", tbl.ID);
                                            ht.Item.HasData = tbl.InstanceFactsCount;
                                            var name = tbl.Name;
                                            ht.Item.Name = Utilities.Strings.TrimTo(name, 40);
                                            ht.Item.Description = string.IsNullOrEmpty(tbl.LabelContent) ? name : tbl.LabelContent;
                                            ht.Item.Type = "table";
                                            hungr.AddChild(ht);
                                        }
                                    }
                                    var itemswithdata = h.Where(i => i.Item.HasData > 0).ToList();
                                    foreach (var itemwithdata in itemswithdata) 
                                    {
                                        var current = itemwithdata;
                                        while (current.Parent != null) 
                                        {
                                            current = current.Parent;
                                            current.Item.HasData = itemwithdata.Item.HasData;
                                        }
                                    }
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
