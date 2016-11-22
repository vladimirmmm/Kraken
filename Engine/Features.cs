
using BaseModel;
using Engine.Services;
using LogicalModel;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities;


namespace Engine
{
    public abstract class UIService 
    {
        public Features Features = null;
        public Func<bool> DispatcherCheckAccess = () => true;
        public Action<Action> DispatcherInvoke = (action) => { };
        public Action Close = () => { };
        public Action ShowSettings = () => { };
        public Action<string> BrowserNavigate = (url) => { };
        public Action BrowserRefresh = () => { };
        public Action<Message> ProcessRequest = (m) => { };
        public Action<Message> ToUI = (m) => { };
        public Action<string> Log = (s) => { };
        public Action<string, string> BrowserInvokeScript = (script, parameters) => { };
        public Func<String, String> BrowseFolder = (folder) => { return ""; };
        public Func<String,String,String> BrowseFile = (folder,file) => { return ""; };
 


    }

    public class Features
    {
        public MenuCommand CommandContainer = null;
        private Object BrowserLocker = new Object();
        public TaxonomyEngine Engine = new XBRLProcessor.XbrlEngine();
        public DataService DataService = null;


        public UIService UI = null;
        public Settings Settings = Settings.Current;
        public List<String> RecentInstances = new List<string>();
        public List<String> RecentTaxonomies = new List<string>();


        private const string RegSettingsPath = @"Software\WKFS\X-TreeM\";
        private const string RegKey_Recent_Taxonomies = "Recent_Taxonomies";
        private const string RegKey_Recent_Instances = "Recent_Instances";

        public void Start(UIService ui) 
        {
            this.UI = ui;
            Load();
            this.Engine.TaxonomyLoaded += Engine_TaxonomyLoaded;
            this.Engine.InstanceLoaded += Engine_InstanceLoaded;
            this.Settings.Load(null);
            this.Settings = Settings.Current;
            ShowInBrowser(Engine.HtmlPath); 

            //AppDomain.CurrentDomain.UnhandledException+=CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.WriteLine((Exception)e.ExceptionObject);
        }
        public bool CanLoadToUI() 
        {
            return Engine.CurrentInstance != null;
        }
        private void Engine_TaxonomyLoaded(object sender, EventArgs e)
        {
            LoadTaxonomyToUI();
            if (!Engine.IsInstanceLoading) 
            {
                InstanceToUI();
            }
        }
        
        public Instance CurrentInstance
        {
            get
            {
                Instance inst = null;
                if (this.Engine.CurrentInstance != null)
                {
                    inst = this.Engine.CurrentInstance;
                }
                return inst;
            }
        }
        public void SetParent(MenuCommand command) 
        {
            foreach (var childcommand in command.Children) 
            {
                childcommand.Parent = command;
                SetParent(childcommand);
            }
        }

        public void Load()
        {
            //UI.TB_TaxonomyPath.Text = GetRegValue(RegSettingsPath + "LastTaxonomy"); //@"C:\My\Tasks\!Tools\Taxonomies\XBRl taxonomy 2.2\XBRL Taxonomy and Supporting Documents.2.2\Taxonomy\2.2.0.0\www.eba.europa.eu\eu\fr\xbrl\crr\fws\corep\its-2013-02\2014-07-31\mod\corep_ind.xsd";
            CommandContainer = new MenuCommand("root", "",
                new MenuCommand("File", "File",
                    new MenuCommand("O_tax_l", "Open Taxonomy (local)", (o) => { OpenTaxonomy(o); }, () => new object[] { UI.BrowseFile("", "") }),
                    new MenuCommand("O_tax_w", "Open Taxonomy (web)", (o) => { OpenTaxonomy(o); }, () => new object[] { UI.BrowseFile("", "") }),
                    new MenuCommand("O_inst", "Open Xbrl Instance", (o) => { OpenInstance(o); }, () => new object[] { UI.BrowseFile("", "") }),
                    new MenuCommand("Save_Xbrl_Instance", "Save Xbrl Instance", (o) => { }, null),
                    new MenuCommand("R_Inst", "Recent Instance", (o) => { }, null),
                    new MenuCommand("R_Tax", "Recent Taxonomies", (o) => { }, null),
                    new MenuCommand("Exit", "Exit", (o) => { UI.Close(); }, null)
                    ),
                new MenuCommand("Instance", "Instance",
                    new MenuCommand("Validate_Instance", "Validate", (o) => { ValidateInstance(); }, null),
                    new MenuCommand("Validate_Folder", "Validate Folder", (o) => { ValidateFolder(o); }, () => new object[] { UI.BrowseFolder("") }),
                    new MenuCommand("ToCompareFile", "To Compare File", (o) => { InsatnceToCompareFile(); }, null),
                    new MenuCommand("CehckMissing", "Check Missing Facts", (o) => { CheckMissing(); }, null),
                    new MenuCommand("SaveInstance", "Save", (o) => { SaveInstance(""); }, null),
                    new MenuCommand("SaveInstanceAs", "Save As", (o) => { SaveInstanceAs(); }, () => new object[] { UI.BrowseFile("", "") }),
                    new MenuCommand("CloseInstance", "Close", (o) => { CloseInstance(); }, null),
                    new MenuCommand("TestInstance", "Test")
                    ),
                new MenuCommand("Taxonomy", "Taxonomy",
                    new MenuCommand("Process_Folder", "Process Folder", (o) => { ProcessFolder(o); }, () => new object[] { UI.BrowseFolder("") }),
                    new MenuCommand("TestTaxonomy", "Test", (o) => { Taxonomy_Test(); }, null),
                    new MenuCommand("Z_Axis_Test", "Z Axis Test", (o) => { Extensions_Test(); }, null),
                    new MenuCommand("Export_Layout", "Export Layout", (o) => { Export_Layout(); }, null),
                    new MenuCommand("Export_Validations_RG", "Export Validations 4 RG", (o) => { Export_Validations_RG(); }, null),
                    new MenuCommand("Export_Validations", "Export Validations", (o) => { Export_Validations(); }, null),

                    new MenuCommand("Refresh_all_but_Structure", "Refresh all (but Structure)", (o) => { ClearTaxonomy(ClearEnum.AllButStructure); }, null),
                    new MenuCommand("Refresh", "Refresh",
                        new MenuCommand("Refresh_all", "Refresh all", (o) => { ClearTaxonomy(ClearEnum.All); }, null),
                        new MenuCommand("Refresh_Structure", "Structure", (o) => { ClearTaxonomy(ClearEnum.Structure); ; }, null),
                        new MenuCommand("Refresh_Tables", "Tables", (o) => { ClearTaxonomy(ClearEnum.Tables); }, null),
                        new MenuCommand("Refresh_Layout_HTML", "Layout (HTML)", (o) => { ClearTaxonomy(ClearEnum.Layout); }, null),
                        new MenuCommand("Refresh_SchemaElements", "Schema Elements", (o) => { ClearTaxonomy(ClearEnum.Elements); }, null),
                        new MenuCommand("Refresh_Hierarchies", "Hierarchies", (o) => { ClearTaxonomy(ClearEnum.Hierarchies); }, null),
                        new MenuCommand("Refresh_Concepts", "Concepts", (o) => { ClearTaxonomy(ClearEnum.Concepts); }, null),
                        new MenuCommand("Refresh_Labels", "Labels", (o) => { ClearTaxonomy(ClearEnum.Labels); }, null),
                        new MenuCommand("Refresh_Facts", "Facts", (o) => { ClearTaxonomy(ClearEnum.Facts); }, null),
                        new MenuCommand("Refresh_Validations", "Validations", (o) => { ClearTaxonomy(ClearEnum.Validations); }, null)
                        )
                    ),
               new MenuCommand("Tools", "Tools",
                    new MenuCommand("ClearAllProcessedTaxonomies", "Clear All Processed Files", (o) => { ClearProcessedTaxonmies(); }, null),
                    new MenuCommand("Settings", "Settings", (o) => { }, null),
                    new MenuCommand("Debug_UI", "Debug UI", (o) => { DebugUI(); }, null)
                    ),
               new MenuCommand("Help", "Help",
                    new MenuCommand("About", "About", (o) => { }, null)
                    )
                );
            SetParent(CommandContainer);

            LoadRecent();
            LoadRecentCommands();

        }

        private void InsatnceToCompareFile()
        {
            var instance = Engine.CurrentInstance;
            var taxonomy = Engine.CurrentTaxonomy;
            var comparer = new Utilities.IntArrayComparer();
            var factkeys = instance.FactDictionary.FactsByTaxonomyKey.Keys.OrderBy(i => i, comparer).ToList();
            var sb = new StringBuilder();
            foreach (var factkey in factkeys)
            {
                var keystr = taxonomy.GetFactStringKey(factkey);
                var instancefacts = instance.FactDictionary.FactsByTaxonomyKey[factkey].Select(i=>instance.FactDictionary.FactsByIndex[i]).OrderBy(i=>i.InstanceKey,comparer).ToList();
                foreach(var instancefact in instancefacts)
                {
                    var key = instance.GetFactStringKey(instancefact.InstanceKey);
                    var value = instancefact.Value;
                    sb.AppendLine(String.Format("{0}: {1}", key, value));

                }
            }
            var instancefilename = Utilities.Strings.GetFileName(TaxonomyEngine.CurrentEngine.CurrentInstance.FullPath);

            var path = TaxonomyEngine.LocalFolder + "FeatureOutput\\" + String.Format("ToCompare-{0}.txt", instancefilename);
            Utilities.FS.WriteAllText(path, sb.ToString());
            Utilities.Logger.WriteLine("Comarision file for this instance was created at:  " + path);


        }

        public string GetJsonObj(MenuCommand command) 
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine(String.Format("\"id\":\"{0}\", \"displayname\":\"{1}\", ", command.ID.Replace("\\", "\\\\"), command.DisplayName.Replace("\\", "\\\\")));
            sb.Append("\"Children\": [");

            for (var i = 0; i < command.Children.Count;i++ )
            {
                var childcommand = command.Children[i];
                sb.Append(GetJsonObj(childcommand));
                if (i < command.Children.Count - 1)
                {
                    sb.AppendLine(", ");
                }

            }
            sb.AppendLine("]");

            sb.AppendLine("}");

            return sb.ToString();
        }

        private void DebugUI()
        {
            var msg = new Message();
            msg.Category = "debug";
            UI.ToUI(msg);
        }

        public void ShowInstance() 
        {
            if (Engine.CurrentTaxonomy != null) 
            {
                //ShowInBrowser(Engine.HtmlPath); 
            }
        }
        
        public void ClearProcessedTaxonmies() 
        {
            var taxonomycontainerfolder = TaxonomyEngine.LocalFolder;
            var files = System.IO.Directory.GetFiles(taxonomycontainerfolder, Taxonomy.StructureFileName, System.IO.SearchOption.AllDirectories);
            foreach (var file in files) 
            {
                var folder = Utilities.Strings.GetFolder(file);
                var subfiles = System.IO.Directory.GetFiles(folder);
                
                Utilities.Logger.WriteLine(String.Format("Deleting folder {0}",folder));
                foreach (var subfile in subfiles) 
                {
                    Utilities.FS.DeleteFile(subfile);
                }
                Utilities.FS.DeleteFolder(folder);
            }
        }
       
        public void ClearTaxonomy(ClearEnum cleartype) 
        {
            if (Engine.CurrentTaxonomy != null) 
            {
                switch (cleartype) 
                {
                    case ClearEnum.All:
                        Engine.CurrentTaxonomy.Clear_All();
                        break;
                    case ClearEnum.AllButStructure:
                        Engine.CurrentTaxonomy.Clear_All_But_Structure();
                        break;
                    case ClearEnum.Structure:
                        Engine.CurrentTaxonomy.Clear_Structure();
                        break;
                    case ClearEnum.Elements:
                        Engine.CurrentTaxonomy.Clear_SchemaElements();
                        break;
                    case ClearEnum.Facts:
                        Engine.CurrentTaxonomy.Clear_Facts();
                        break;
                    case ClearEnum.Labels:
                        Engine.CurrentTaxonomy.Clear_Labels();
                        break;
                    case ClearEnum.Layout:
                        Engine.CurrentTaxonomy.Clear_Layout();
                        break;
                    case ClearEnum.Tables:
                        Engine.CurrentTaxonomy.Clear_Tables();
                        break;
                    case ClearEnum.Hierarchies:
                        Engine.CurrentTaxonomy.Clear_Hierarchies();
                        break;
                    case ClearEnum.Concepts:
                        Engine.CurrentTaxonomy.Clear_Concepts();
                        break;
                    case ClearEnum.Validations:
                        Engine.CurrentTaxonomy.Clear_Validations();
                        break; 
                    default:
                        break;
                }
            }
        }

        public void LoadRecent()
        {
            RegValueToList(RegKey_Recent_Taxonomies, RecentTaxonomies);
            RegValueToList(RegKey_Recent_Instances, RecentInstances);
           
        }
        
        public static string GetSettingValue(string key, string defaultvalue = "")
        {
            return GetRegValue(RegSettingsPath + key);
        }
        
        public static void SetSettingValue(string key, string value )
        {
            SetRegValue(RegSettingsPath + key, value);

        }
        
        public static string GetRegValue(string key, string defaultvalue = "")
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey sk1 = rk.OpenSubKey(key);
            if (sk1 == null)
            {
                SetRegValue(key, defaultvalue);
                sk1 = rk.OpenSubKey(key);
            }
            return String.Format("{0}", sk1.GetValue(key));
        }

        private static void SetRegValue(string key, string value)
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey sk1 = rk.CreateSubKey(key);
            sk1.SetValue(key.ToUpper(), value);
        }

        private void RegValueToList(string regkey, List<string> items)
        {
            items.Clear();
            var r_str = GetRegValue(RegSettingsPath + regkey);
            var r_items = r_str.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            items.AddRange(r_items);
        }

        private void ListToRegValue(string regkey, List<string> items)
        {
            var sb = new StringBuilder();
            foreach (var x in items)
            {
                sb.Append(x + ",");
            }
            SetRegValue(RegSettingsPath + regkey, sb.ToString());
        }
        
        public void NavigateToCell(string report, string extension, string row, string column) 
        {
            var table = Engine.CurrentTaxonomy.Tables.FirstOrDefault(i => i.ID.ToLower() == report);
            if (table != null) 
            {
                // table.CurrentExtension = table.Extensions.FirstOrDefault(i => i.LabelCode == extension);
                table.EnsureHtmlLayout();
                var url = String.Format("{0}#ext={1};cell=R{2}_C{3};", table.FullHtmlPath, extension, row, column);
                
                UI.BrowserNavigate(url);
                //UI.TB_Title.Text = String.Format("{0} - {1}", table.ID, table.FullHtmlPath);
            }
        }
   
        
  

        public void SaveSettings() 
        {
            Settings.Save(null);
        }

        public void LoadRecentCommands()
        {
            var recent_taxonomy = CommandContainer.Where(i => i.ID == "R_Tax").FirstOrDefault();
            var recent_instance = CommandContainer.Where(i => i.ID == "R_Inst").FirstOrDefault();
            
            recent_instance.Children.Clear();
            foreach (var item in RecentInstances) 
            {
                var m = new MenuCommand(item, item, (o) => { OpenInstance(item); }, null);
                recent_instance.Children.Add(m);
            }

            recent_taxonomy.Children.Clear();
            foreach (var item in RecentTaxonomies)
            {
                var m = new MenuCommand(item, Utilities.Strings.GetFileName(item), (o) => { OpenTaxonomy(item); },null);
                recent_taxonomy.Children.Add(m);
            }
            //TODO MENU
            //UI.LoadMenu(CommandContainer, null);
        }


        public void AddToRecent(List<String> items,string RegID, string item) 
        {
             int max=20;
             if (items.Count >= max)
             {
                 items.Remove(items.Last());
             }
             if (!items.Contains(item))
             {
                 items.Insert(0, item);
             }
             else 
             {
                 items.Remove(item);
                 items.Insert(0, item);
             }
             ListToRegValue(RegID, items);
             LoadRecentCommands();
        }
        
        protected void OpenTaxonomy(object[] parameters)
        {
            if (parameters.Length > 0) 
            {
                var paths = String.Format("{0}", parameters[0]).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                var fixedpaths = paths.Where(i => !String.IsNullOrEmpty(i.Trim())).Select(i => i.Trim()).ToList();

                OpenTaxonomy(fixedpaths.FirstOrDefault());
            }
        }
        
        public void OpenTaxonomy(string path) 
        {
            Engine.TaxonomyLoad -= Engine_TaxonomyLoad;
            Engine.TaxonomyLoad += Engine_TaxonomyLoad;
            Engine.LoadTaxonomy(path);
    
        }
        
        public void OpenInstance(object[] parameters)
        {
            if (parameters.Length > 0)
            {
                OpenInstance(String.Format("{0}", parameters[0]));
            }
        }
        
        public void OpenInstance(string path)
        {
            SetRegValue(RegSettingsPath + "LastInstance", path);
            AddToRecent(RecentInstances,RegKey_Recent_Instances, path);

            Engine.TaxonomyLoad -= Engine_TaxonomyLoad;
            Engine.TaxonomyLoad += Engine_TaxonomyLoad;

            Engine.TaxonomyLoaded -= Engine_TaxonomyLoaded;
            Engine.TaxonomyLoaded += Engine_TaxonomyLoaded;

            Engine.InstanceLoaded -= Engine_InstanceLoaded;
            Engine.InstanceLoaded += Engine_InstanceLoaded;
     
            Engine.LoadInstance(path);


     
            //ValidateInstance();
        }
        
        private void Engine_TaxonomyLoad(object sender, TaxonomyEventArgs e)
        {
            SetRegValue(RegSettingsPath + "LastTaxonomy", e.FilePath);
            AddToRecent(RecentTaxonomies, RegKey_Recent_Taxonomies, e.FilePath);
        }
        public void InstanceToUI() 
        {
            var msg = new Message();
            msg.Category = "action";
            msg.Data = "instanceloaded";
            UI.ToUI(msg);
        }
        private void Engine_InstanceLoaded(object sender, EventArgs e)
        {
            InstanceToUI();
      
            if (Settings.ValidateOnInstanceLoaded)
            {
                ValidateInstance();
            }

        }
        
        public void CloseInstance()
        {
            Engine.CurrentInstance = null;
            Utilities.FS.DeleteFile(Engine.CurrentTaxonomy.CurrentInstancePath);
            Utilities.FS.DeleteFile(Engine.CurrentTaxonomy.CurrentInstanceValidationResultPath);
        }
        
        public void SaveInstanceAs()
        {
            if (Engine.CurrentInstance != null)
            {
                string path = Engine.CurrentInstance.FullPath.Replace(".xbrl", "_modified.xbrl");
                string folder = Utilities.Strings.GetFolder(path);
                string filename = Utilities.Strings.GetFileName(path);
                var filepath = UI.BrowseFile(folder, filename);
                SaveInstance(filepath);
            }
        }
       
        public void SaveInstance(string path)
        {
            if (Engine.CurrentInstance != null)
            {
                path = string.IsNullOrEmpty(path) ? CurrentInstance.FullPath : path;
                if (!path.EndsWith("_modifed.xbrl"))
                {
                    path = path.Replace(".xbrl", "_modified.xbrl");
                }
                Engine.CurrentInstance.Save(path);

            }
        }

        public void ValidateInstance()
        {
            if (Engine.CurrentInstance != null) 
            {
                var results = Engine.CurrentInstance.Validate(null);
                var msg= new Message();
                msg.Category="action";
                msg.Url="Instance";
                msg.Data="instancevalidated";
                UI.ToUI(msg);
            }
        }

        public void CheckMissing() 
        {
            if (Engine.CurrentInstance != null)
            {
                var results = Engine.CurrentInstance.Validate(null);
                var instance = Engine.CurrentInstance;
                var taxonomy = Engine.CurrentTaxonomy;
                var allfiling = taxonomy.Tables.Select(i => i.FilingIndicator).Distinct().ToList();
                var instfiling = instance.FilingIndicators.Where(i => i.Filed).Select(i=>i.ID).ToList();
                var missingfilling = allfiling.Except(instfiling).ToList();
                var instancefactkeys = new List<int[]>();
                foreach (var instancefact in instance.Facts) 
                {
                    //var keys = taxonomy.GetFactStringKeyFromStringKey(instance.Facts[0].FactKey);
                    var keys = taxonomy.GetFactIntKey(instancefact.FactKey).ToArray();
                    //var key = taxonomy.GetFactIntStringKey(instancefact.FactKey);
                    instancefactkeys.Add(keys);

                }
                var taxkeylist = taxonomy.FactKeysAsEnumerable().ToList(); //.Select(i=>Utilities.Strings.ArrayToString(i,",")+",").ToList();
                var missingfacts = taxkeylist.Except(instancefactkeys,new Utilities.IntArrayEqualityComparer()).ToList();
                var instancefilename = Utilities.Strings.GetFileName(TaxonomyEngine.CurrentEngine.CurrentInstance.FullPath);
                var csvpath = TaxonomyEngine.LocalFolder +"FeatureOutput\\"+ String.Format("MissingInstanceFactsFor-{0}.csv",instancefilename);
                var sb = new StringBuilder();
                var c = 0;
                Utilities.FS.WriteAllText(csvpath,"Report,Extension Code,Extension Label,Row,Col\n");

                foreach (var missingfact in missingfacts) 
                {
                    c++;
                    var cells = taxonomy.GetCellsOfFact(missingfact);
                    foreach (var cell in cells) 
                    {
                        var cellobj = new Cell();
                        string cellid = ""; //from cell
                        cellobj.SetFromCellID(cellid);
                        var table = taxonomy.Tables.FirstOrDefault(i => i.ID == cellobj.Report);
                        var extension = table.Extensions.FirstOrDefault(i=>i.Item.LabelCode==cellobj.Extension);
                        var extlabel = extension != null ? extension.Item.LabelContent : "";
                        

                        sb.AppendLine(String.Format("\"\t{0}\",\"\t{1}\",\"\t{2}\",\"\t{3}\",\"\t{4}\"", cellobj.Report, cellobj.Extension,extlabel, cellobj.Row, cellobj.Column));
                    }
                    if (c % 10000 == 0) 
                    {
                        Utilities.FS.AppendAllText(csvpath, sb.ToString());
                        sb.Clear();
                    }
                }
                Utilities.FS.AppendAllText(csvpath, sb.ToString());
                sb.Clear();
                Utilities.Logger.WriteLine("Missing cells exported to " + csvpath);
                var msg = new Message();
                msg.Category = "action";
                msg.Url = "Instance";
                msg.Data = "instancevalidated";
                UI.ToUI(msg);
            }
        }



        public void ValidateFolder(object[] parameters)
        {
            if (parameters.Length > 0)
            {
                var path = String.Format("{0}", parameters[0]);

                if (!String.IsNullOrEmpty(path))
                {
                    var files = System.IO.Directory.GetFiles(path, "*.xbrl", System.IO.SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        OpenInstance(file);
                        ValidateInstance();
                    }
                }
            }
        }

        public void ProcessFolder(object[] parameters)
        {
            if (parameters.Length > 0)
            {
                var path = String.Format("{0}", parameters[0]);
                if (!String.IsNullOrEmpty(path))
                {
                    var modulefolders = System.IO.Directory.GetDirectories(path, "mod", System.IO.SearchOption.AllDirectories);
                    foreach (var modulefolder in modulefolders)
                    {
                        var files = System.IO.Directory.GetFiles(modulefolder, "*.xsd", System.IO.SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            OpenTaxonomy(file);
                            //OpenInstance(file);
                            //ValidateInstance();
                        }
                    }
                }
            }
        }

        public void LoadTaxonomyToUI()
        {
            if (this.Engine.CurrentTaxonomy != null)
            {
                if (UI.DispatcherCheckAccess())
                {

                    var msg = new Message();
                    msg.Category = "action";
                    msg.Data = "taxonomyloaded";
                    UI.ToUI(msg);
             
                }
                else
                {
                    UI.DispatcherInvoke(() => { LoadTaxonomyToUI(); });
                }
            }
        }

        public void SearchLabels(string searchpattern) 
        {
            if (searchpattern.Length > 1)
            {
                var labels = Engine.CurrentTaxonomy.TaxonomyLabels.Where(i => i.DisplayName.Contains(searchpattern));
                //LoadToLV(labels, UI.LV_labels);
            }
        }
        public void SearchFacts(string searchpattern)
        {
            if (searchpattern.Length > 1)
            {
                //TODO
                /*
                var labels = Engine.CurrentTaxonomy.Facts.Where(i => i.Key.Contains(searchpattern));
                var items = labels.SelectMany(i=>i.Value);
                 */
                //LoadToLV(items, UI.LV_Facts);
            }
        }
        public void SearchCells(string searchpattern)
        {
            if (searchpattern.Length > 1)
            {
               // var labels = Engine.CurrentTaxonomy.Cells.Where(i => i.Key.Contains(searchpattern)).SelectMany(i => i.Value);
                //LoadToLV(labels, UI.LV_Cells);
            }
        }

        public void SearchItems<T>(string searchpattern,Func<List<T>> items)
        {
            if (searchpattern.Length > 1)
            {
                //var labels = list.Where(i => i.Key.Contains(searchpattern)).labels.SelectMany(i => i.Value);
                //LoadToLV(items(), UI.LV_Facts);
            }
        }

       
        public void LoadTaxonomy(string path) 
        {
            Engine.LoadTaxonomy(path);
            //LoadTaxonomyToUI();
        }




        public void Search(string p)
        {
            //var context = UI.Tree_Control.SelectedItem as ControlNode;
            //if (context.Title == "Facts")
            //{
                
            //}
        }

        public void ShowInBrowser(string path) 
        {
            if (UI.DispatcherCheckAccess())
            {
     
                UI.BrowserNavigate(path);
                //UI.TB_Title.Text = path;
              
            }
            else
            {
                UI.DispatcherInvoke(() => { ShowInBrowser(path); });
            }
        }


        public Message ProcessRequest(Message request)
        {
            return DataService.ProcessRequest(request);
        }

        public void OnUIReady() 
        {
            var engineassembly = Assembly.GetAssembly(this.GetType());
            var engineassemblyfileinfo = new System.IO.FileInfo(engineassembly.Location);
            Logger.WriteLine(String.Format("Engine Version: {0:" + Utilities.Converters.DateTimeFormat + "}", engineassemblyfileinfo.LastWriteTime));

            Logger.WriteLine(GetUIVersion());
            LoadTaxonomyToUI();
        }
        private string GetUIVersion() 
        {
            var uifiles = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\Scripts", "*.ts");
            DateTime lastdate = DateTime.Now.AddYears(-100);
            foreach (var uifile in uifiles)
            {
                var fileinfo = new System.IO.FileInfo(uifile);
                if (fileinfo.LastWriteTime > lastdate)
                {
                    lastdate = fileinfo.LastWriteTime;
             
                }
            }
            return String.Format("UI Version: {0:" + Utilities.Converters.DateTimeFormat + "}", lastdate);
        }
        
        public string GetAppInfo()
        {
            var sb = new StringBuilder();
            var engineassembly = Assembly.GetAssembly(this.GetType());
            var modules = engineassembly.GetReferencedAssemblies().ToList();
            sb.AppendLine(GetAssimblyInfoString(engineassembly));

            foreach (var module in modules)
            {
                var asm = Assembly.Load(module);
                var infostring = GetAssimblyInfoString(asm);
                if (infostring.Contains("Vladimir"))
                {
                    sb.AppendLine(infostring);

                }
            }
            sb.AppendLine(GetUIVersion());

            return sb.ToString();
        }

        private string GetAssimblyInfoString(Assembly asm) 
        {
            var copyright = GetCopyRightInfo(asm);
            var fileinfo = new System.IO.FileInfo(asm.Location);
            var builddate = fileinfo.LastWriteTime;
            var name = asm.FullName;
            return String.Format("{0} {1} {2}", name, builddate, copyright);
        }


        public string GetCopyRightInfo(Assembly asm) 
        {
            object[] attribs = asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
            var result="";
            if(attribs.Length > 0)
            {
                result = ((AssemblyCopyrightAttribute)attribs[0]).Copyright;
            }
            return result;
        }
        public void Taxonomy_Test() 
        {
            var dimensions = this.Engine.CurrentTaxonomy.GetAllDimensions();
            var text = Utilities.Strings.ArrayToString(dimensions.ToArray(),"\r\n");
            Utilities.Logger.WriteLine(text);
        }
        public void Export_Validations_RG()
        {
            var tax = this.Engine.CurrentTaxonomy;
            var tablerules = new Dictionary<string,List<LogicalModel.Validation.ValidationRule>>();
            foreach (var rule in tax.ValidationRules) 
            {
                var key = Utilities.Strings.ArrayToString(rule.Tables.Select(i => tax.Tables.FirstOrDefault(j => j.ID == i).Name).ToArray(), "__");
                if (!tablerules.ContainsKey(key)) 
                {
                    tablerules.Add(key, new List<LogicalModel.Validation.ValidationRule>());
                }
                tablerules[key].Add(rule);

            }
            var wts_sb = new StringBuilder();
            wts_sb.AppendLine("insert into message (msg_id,msg_ref,msg_type,msg_language,msg_text)");
            wts_sb.AppendLine("values (:1,:2,:3,:4,:5)");
            wts_sb.AppendLine("\\");

            foreach (var item in tablerules) 
            {
                var sb = new StringBuilder();
                /*
                 insert into message (msg_id,msg_ref,msg_type,msg_language,msg_text)
values (:1,:2,:3,:4,:5)
\
                 */
          
                foreach (var rule in item.Value)
                {
                    var ruleresults = rule.GetAllResults();
                    var simplerule = tax.SimpleValidationRules.FirstOrDefault(i => i.ID == rule.ID);
                    var stringrgrules = new List<string>();
                    var taxdate = tax.Module.FromDate.HasValue ? String.Format("{0:yyyymmdd}", tax.Module.FromDate) : "1001-01-01";

                    foreach (var ruleresult in ruleresults) 
                    {
                        var rg_rule_expression = simplerule.OriginalExpression;
                        var errorkey = String.Format("{0}_{1}_{2}", rule.ID, tax.Module.Name, taxdate);
                        foreach (var param in ruleresult.Parameters) 
                        {
                            var cells = param.Cells.FirstOrDefault();
                            if (cells != null && cells.Count > 0)
                            {
                                var cellid = cells.FirstOrDefault();
                                var cell = new Cell();
                                cell.SetFromCellID(cellid);
                                var table = tax.Tables.FirstOrDefault(i => i.ID == cell.Report);
                                var ext = table.Extensions.FirstOrDefault(i => i.Item.LabelCode == cell.Extension);
                                var extname = ext == null ? Literals.DynamicCode : ext.Item.LabelCode == "000" ? Literals.DynamicCode : ext.Item.LabelContent;
                                //<'C05.01_CA5 rev4','Total'>['Row210'|'Col060']
                                var cellreference = String.Format(" <'{0}','{1}'>['{2}'|'{3}'] ", table.Name, extname, cell.Row, cell.Column);
                                rg_rule_expression = rg_rule_expression.Replace("$" + param.Name, cellreference);

                            }
                            else 
                            {
                                rg_rule_expression = rg_rule_expression.Replace("$" + param.Name, "0");


                            }
                        }
                        var rg_rule = String.Format("Y {0}; ERROR={1}; $1", rg_rule_expression, errorkey);

                        if (!stringrgrules.Contains(rg_rule))
                        {
                            sb.AppendLine("/* " + simplerule.DisplayText + " */");
                            sb.AppendLine("/* " + simplerule.OriginalExpression + " */");
                            sb.AppendLine(rg_rule);
                            sb.AppendLine();
                            /*
                             insert into message (msg_id,msg_ref,msg_type,msg_language,msg_text)
values (:1,:2,:3,:4,:5)
\
9001,COREP_FINREP_C1000,D,ENG,Return_id
                             */

                            wts_sb.AppendLine(String.Format("0,{0},W,ENG,\"{1}\"", errorkey, simplerule.DisplayText));
                            stringrgrules.Add(rg_rule);
                        }
                    }
                }
                var outputpath = String.Format("{0}{1}.ina", tax.TaxonomyValidationFolder + "RG\\", item.Key);
                Utilities.FS.WriteAllText(outputpath, sb.ToString());
                Utilities.Logger.WriteLine(String.Format("Validation Rules exported to {0}", outputpath));
           
               
            }
            var wtsoutputpath = String.Format("{0}{1}.wts", tax.TaxonomyValidationFolder + "RG\\", "Validation_Messages");
            Utilities.FS.WriteAllText(wtsoutputpath, wts_sb.ToString());

        }
       
        public void Export_Layout()
        {
            if (Engine.CurrentTaxonomy != null)
            {
                var files = System.IO.Directory.GetFiles(Engine.CurrentTaxonomy.ModuleFolder, "*.html", System.IO.SearchOption.AllDirectories);

                var sb = new StringBuilder();
                sb.AppendLine("<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />");
                sb.AppendLine("<style>" + System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Table.css") + "\r\n</style>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");
                foreach (var file in files)
                {
                    sb.AppendLine(String.Format("{0}<br/>", System.IO.File.ReadAllText(file)));
                }
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                var outputpath = String.Format("{0}AllTemplates_{1}.html", TaxonomyEngine.LocalFolder, Engine.CurrentTaxonomy.Module.Name);
                Utilities.FS.WriteAllText(outputpath, sb.ToString());
                Utilities.Logger.WriteLine(String.Format("Validation Rules exported to {0}", outputpath));
            }
        }
        public void Export_Validations()
        {
            if (Engine.CurrentTaxonomy != null) 
            {
                var sb = new StringBuilder();
                foreach (var rule in Engine.CurrentTaxonomy.SimpleValidationRules)
                {
                    sb.AppendLine(String.Format("Rule {0}", rule.ID));
                    sb.AppendLine(String.Format("Message: {0}", rule.DisplayText));
                    sb.AppendLine(String.Format("Formula: {0}", rule.OriginalExpression));
                    sb.AppendLine("_____________________________________________________");
                }
                var outputpath = String.Format("{0}ValidationsRules_{1}.txt", TaxonomyEngine.LocalFolder, Engine.CurrentTaxonomy.Module.Name);
                Utilities.FS.WriteAllText(outputpath, sb.ToString());
                Utilities.Logger.WriteLine(String.Format("Validation Rules exported to {0}", outputpath));
            }
        }
        public void Extensions_Test() 
        {
            var taxonomycontainerfolder = TaxonomyEngine.LocalFolder;
            var files = System.IO.Directory.GetFiles(taxonomycontainerfolder, "*layout.txt*", System.IO.SearchOption.AllDirectories);
            var sb = new StringBuilder();
            foreach (var file in files) 
            {
                string content = Utilities.FS.ReadAllText(file);
                var folder = Utilities.Strings.GetFolder(file);
                var filename = Utilities.Strings.GetFileName(file);
                var zix = content.IndexOf("    z ");
                if (zix>-1){
                    var zcontent = content.Substring(zix);
                    sb.AppendLine("_______________________________________________________");
                    sb.AppendLine(filename);
                    sb.AppendLine(folder);
                    sb.AppendLine(zcontent);
                    sb.AppendLine("_______________________________________________________");
                    sb.AppendLine();
                }
            }
            var outputpath = taxonomycontainerfolder + "ZAxisTest.txt";
            Utilities.FS.WriteAllText(outputpath, sb.ToString());
            Utilities.Logger.WriteLine(String.Format("Z Axis analyzis exported to {0}", outputpath));
        }
    }
}
