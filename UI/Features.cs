﻿using LogicalModel;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using XBRLProcessor;
using Utilities;

namespace UI
{
    public class Features
    {
        public MenuCommand CommandContainer = null;
        public ControlNode FeatureContainer = null;

        public XbrlEngine Engine = new XbrlEngine();

        private const string RegSettingsPath = @"Software\WKFS\X-TreeM\";
        private const string RegKey_Recent_Taxonomies = "Recent_Taxonomies";
        private const string RegKey_Recent_Instances = "Recent_Instances";

        public MainWindow UI = null;
        public Settings Settings = Settings.Current;
        public List<String> RecentInstances = new List<string>();
        public List<String> RecentTaxonomies = new List<string>();

        public Features(MainWindow ui) 
        {
            this.UI = ui;
            Load();
            this.Engine.TaxonomyLoaded += Engine_TaxonomyLoaded;
            this.Engine.InstanceLoaded += Engine_InstanceLoaded;
            this.Settings.Load(null);
            this.Settings = Settings.Current;
        }

        private void Engine_TaxonomyLoaded(object sender, EventArgs e)
        {
            LoadTaxonomyToUI();
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
        
        public void Load()
        {
            UI.TB_TaxonomyPath.Text = GetRegValue(RegSettingsPath + "LastTaxonomy"); //@"C:\My\Tasks\!Tools\Taxonomies\XBRl taxonomy 2.2\XBRL Taxonomy and Supporting Documents.2.2\Taxonomy\2.2.0.0\www.eba.europa.eu\eu\fr\xbrl\crr\fws\corep\its-2013-02\2014-07-31\mod\corep_ind.xsd";
            CommandContainer = new MenuCommand("root", "",
                new MenuCommand("File", "",
                    new MenuCommand("O_tax", "Open Taxonomy", (o) => { OpenTaxonomy(); }),
                    new MenuCommand("O_inst", "Open Xbrl Instance", (o) => { OpenInstance(); }),
                    new MenuCommand("Save Xbrl Instance", ""),
                    new MenuCommand("R_Inst", "Recent Instance"),
                    new MenuCommand("R_Tax", "Recent Taxonomies"),
                    new MenuCommand("Exit", "", (o) => { UI.Close(); })
                    ),
                new MenuCommand("Instance", "",
                    new MenuCommand("Validate", "", (o) => { ValidateInstance(); }),
                    new MenuCommand("Validate Folder", "", (o) => { ValidateFolder(); }),
                    new MenuCommand("Save", "", (o) => { SaveInstance(""); }),
                    new MenuCommand("Save As", "", (o) => { SaveInstanceAs(); }),
                    new MenuCommand("Close", "", (o) => { CloseInstance(); }),
                    new MenuCommand("Test", "")
                    ),
                new MenuCommand("Taxonomy", "",
                    new MenuCommand("Refresh all (but Structure)", "", (o) => { Clear(ClearEnum.AllButStructure); }),
                    new MenuCommand("Refresh", "",
                        new MenuCommand("Refresh all", "", (o) => { Clear(ClearEnum.All); }),
                        new MenuCommand("Structure", "", (o) => { Clear(ClearEnum.Structure); ; }),
                        new MenuCommand("Tables", "", (o) => { Clear(ClearEnum.Tables); }),
                        new MenuCommand("Layout (HTML)", "", (o) => { Clear(ClearEnum.Layout); }),
                        new MenuCommand("SchemaElements", "", (o) => { Clear(ClearEnum.Elements); }),
                        new MenuCommand("Hierarchies", "", (o) => { Clear(ClearEnum.Hierarchies); }),
                        new MenuCommand("Concepts", "", (o) => { Clear(ClearEnum.Concepts); }),
                        new MenuCommand("Labels", "", (o) => { Clear(ClearEnum.Labels); }),
                        new MenuCommand("Facts", "", (o) => { Clear(ClearEnum.Facts); }),
                        new MenuCommand("Validations", "", (o) => { Clear(ClearEnum.Validations); })
                        )
                    ),
               new MenuCommand("Tools", "",
                    new MenuCommand("Settings", "", (o) => { ShowSettings(); })
                    ),
               new MenuCommand("Help", "",
                    new MenuCommand("About", "", (o) => {  })
                    )
                );

            FeatureContainer = new ControlNode("root", "Features", null,
                new ControlNode("", "Taxonomy", (o) => { ShowTab(o); },
                    new ControlNode("", "XML", (o) => { ShowTab(o); }),
                    new ControlNode("", "Tables", (o) => { ShowTab(o); },
                        new ControlNode("", "Layout", (o) => { ShowTab(o); }),
                        new ControlNode("", "HyperCubes", (o) => { ShowTab(o); }),
                        new ControlNode("", "Facts", (o) => { ShowTab(o); }),
                        new ControlNode("", "labels", (o) => { ShowTab(o); }),
                        new ControlNode("", "Cells", (o) => { ShowTab(o); })
                        ),
                    new ControlNode("", "Labels", (o) => { ShowTab(o); }),
                    new ControlNode("", "Facts", (o) => { ShowTab(o); }),
                    new ControlNode("", "Cells", (o) => { ShowTab(o); }),
                    new ControlNode("", "Hierarchy", (o) => { ShowTab(o); })
                    ),
                new ControlNode("", "Instance", (o) => { ShowTab(o); ShowInstance(); },
                    new ControlNode("", "Xml", (o) => { ShowTab(o); }),
                    new ControlNode("", "Contexts", (o) => { ShowTab(o); }),
                    new ControlNode("", "Facts", (o) => { ShowTab(o); }),
                    new ControlNode("", "Info", (o) => { ShowTab(o); })
                    )
                );

            LoadRecent();
            LoadRecentCommands();

        }

        public void ShowInstance() 
        {
            if (Engine.CurrentTaxonomy != null) 
            {
                ShowInBrowser(Engine.HtmlPath); 
            }
        }

        public void Clear(ClearEnum cleartype) 
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

        public void ShowTab(object treeitemobj)
        {
            if (UI.Dispatcher.CheckAccess())
            {
                var ti = treeitemobj as ControlNode;
                var p = ti.Parent;
                var tabid = "";
                if (p == null)
                {
                    tabid = String.Format("{0}", ti.Title.ToLower());
                }
                else
                {
                    tabid = String.Format("{0}_{1}", p.Title.ToLower(), ti.Title.ToLower());

                }
                foreach (TabItem item in UI.TabControl_Left.Items)
                {
                    if (item.Header.ToString().ToLower() == tabid)
                    {
                        UI.TabControl_Left.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                UI.Dispatcher.Invoke(() => { ShowTab(treeitemobj); });
            }
     
        }

        public void LoadRecent()
        {
            RegValueToList(RegKey_Recent_Taxonomies, RecentTaxonomies);
            RegValueToList(RegKey_Recent_Instances, RecentInstances);
           
        }

        public void NavigateToCell(string report, string extension, string row, string column) 
        {
            var table = Engine.CurrentTaxonomy.Tables.FirstOrDefault(i => i.ID.ToLower() == report);
            if (table != null) 
            {
                table.CurrentExtension = table.Extensions.FirstOrDefault(i => i.LabelCode == extension);
                table.EnsureHtmlLayout();
                var url = String.Format("{0}#ext={1};cell=R{2}_C{3};", table.HtmlPath, extension, row, column);
                
                UI.Browser.Navigate(url);
                UI.TB_Title.Text = String.Format("{0} - {1}", table.ID, table.HtmlPath);
            }
        }

        private string GetTableHtmlPath(string report, string extension, string row, string column) 
        {
            var table = Engine.CurrentTaxonomy.Tables.FirstOrDefault(i => i.ID.ToLower() == report);
            var url = "";
            if (table != null)
            {
                table.CurrentExtension = table.Extensions.FirstOrDefault(i => i.LabelCode == extension);
                table.EnsureHtmlLayout();
                url = String.Format("{0}#ext={1};cell=R{2}_C{3};", table.HtmlPath, extension, row, column);

                //UI.Browser.Navigate(url);
                //UI.TB_Title.Text = String.Format("{0} - {1}", table.ID, table.HtmlPath);
            }
            return url;
        }
        
        private void RegValueToList(string regkey, List<string> items) 
        {
            items.Clear();
            var r_str = GetRegValue(RegSettingsPath + regkey);
            var r_items = r_str.Split(new string[] { ";","," }, StringSplitOptions.RemoveEmptyEntries);
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
                var m = new MenuCommand(item, item, (o) => { OpenInstance(item); });
                recent_instance.Children.Add(m);
            }

            recent_taxonomy.Children.Clear();
            foreach (var item in RecentTaxonomies)
            {
                var m = new MenuCommand(item, Utilities.Strings.GetFileName(item), (o) => { OpenTaxonomy(item); });
                recent_taxonomy.Children.Add(m);
            }
            LoadMenu(CommandContainer, null);
        }

        public void OpenTaxonomy()
        {
            var path = BrowsForFile("","");
            if (!String.IsNullOrEmpty(path))
            {
                OpenTaxonomy(path);
            }
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

        public void OpenTaxonomy(string path) 
        {   
            Engine.LoadTaxonomy(path);
    
        }
        public void OpenInstance()
        {
            var path = BrowsForFile("","");
            if (!String.IsNullOrEmpty(path))
            {
                OpenInstance(path);
            }
        }
        public void OpenInstance(string path)
        {
            SetRegValue(RegSettingsPath + "LastInstance", path);
            AddToRecent(RecentInstances,RegKey_Recent_Instances, path);
            
            Engine.InstanceLoaded -= Engine_InstanceLoaded;
            Engine.InstanceLoaded += Engine_InstanceLoaded;
            Engine.TaxonomyLoad -= Engine_TaxonomyLoad;
            Engine.TaxonomyLoad += Engine_TaxonomyLoad;
            Engine.LoadInstance(path);

      
           
            LoadInstanceToUI();
            if (Settings.ValidateOnInstanceLoaded) 
            {
                ValidateInstance();
            }
            //ValidateInstance();
        }
        private void Engine_TaxonomyLoad(object sender, TaxonomyEventArgs e)
        {
            SetRegValue(RegSettingsPath + "LastTaxonomy", e.FilePath);
            AddToRecent(RecentTaxonomies, RegKey_Recent_Taxonomies, e.FilePath);
        }
        private void Engine_InstanceLoaded(object sender, EventArgs e)
        {
            UIReload();

        }

        public void UIReload() 
        {

            if (UI.Dispatcher.CheckAccess())
            {
                UI.Browser.Refresh();
            }
            else 
            {
                UI.Dispatcher.Invoke(() => { LoadInstanceToUI(); });
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
                var filepath = BrowsForFile(folder, filename);
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

            }
        }

        public void ValidateFolder() 
        {
            var path = BrowsForFolder("");
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
        public void LoadInstanceToUI()
        {
            if (UI.Dispatcher.CheckAccess())
            {
                UI.TB_Title.Text = Engine.CurrentInstance.FullPath;
                //UI.XML_Tree.ItemsSource = new List<TaxonomyDocument>() { Engine.CurrentTaxonomy.EntryDocument };
                //UI.Table_Tree.ItemsSource = Engine.CurrentTaxonomy.Tables;
            }
            else
            {
                UI.Dispatcher.Invoke(() => { LoadInstanceToUI(); });
            }
        }

        public void LoadTaxonomyToUI()
        {
            if (UI.Dispatcher.CheckAccess())
            {
                UI.XML_Tree.ItemsSource = new List<TaxonomyDocument>() { Engine.CurrentTaxonomy.EntryDocument };
                UI.Table_Tree.ItemsSource = Engine.CurrentTaxonomy.Tables;
                UI.TB_TaxonomyPath.Text = Engine.CurrentTaxonomy.EntryDocument.LocalPath;
                ShowInBrowser(Engine.HtmlPath); 

            }
            else
            {
                UI.Dispatcher.Invoke(() => { LoadTaxonomyToUI(); });
            }
        }

        public void SearchLabels(string searchpattern) 
        {
            if (searchpattern.Length > 1)
            {
                var labels = Engine.CurrentTaxonomy.TaxonomyLabels.Where(i => i.DisplayName.Contains(searchpattern));
                LoadToLV(labels, UI.LV_labels);
            }
        }
        public void SearchFacts(string searchpattern)
        {
            if (searchpattern.Length > 1)
            {
                var labels = Engine.CurrentTaxonomy.Facts.Where(i => i.Key.Contains(searchpattern));
                var items = labels.SelectMany(i=>i.Value);
                LoadToLV(items, UI.LV_Facts);
            }
        }
        public void SearchCells(string searchpattern)
        {
            if (searchpattern.Length > 1)
            {
                var labels = Engine.CurrentTaxonomy.Cells.Where(i => i.Key.Contains(searchpattern)).SelectMany(i => i.Value);
                LoadToLV(labels, UI.LV_Cells);
            }
        }

        public void SearchItems<T>(string searchpattern,Func<List<T>> items)
        {
            if (searchpattern.Length > 1)
            {
                //var labels = list.Where(i => i.Key.Contains(searchpattern)).labels.SelectMany(i => i.Value);
                LoadToLV(items(), UI.LV_Facts);
            }
        }

        public void LoadToLV(IEnumerable items, ListView lv) 
        {
            if (UI.Dispatcher.CheckAccess())
            { 
               lv.ItemsSource = items;
            }
            else
            {
                UI.Dispatcher.Invoke(() => { LoadToLV(items, lv); });
            }
        }
        
        public void LoadMenu(MenuCommand command, MenuItem menuitem)
        {
            if (UI.Dispatcher.CheckAccess())
            {
                ItemCollection itemcollection = null;
                if (menuitem == null) { itemcollection = UI.Menu.Items;
                UI.Menu.Items.Clear();
                }
                else
                {
                    itemcollection = menuitem.Items;
                }

                foreach (var c in command.Children)
                {
                    var m = new MenuItem();
                    m.Header = c.DisplayName.Replace("_","__");
                    m.IsEnabled = c.Enabled;
                    if (c.Children.Count == 0)
                    {
                        m.Click += (s, e) => { c.ExecuteAsync(); };
                    }
                    itemcollection.Add(m);
                    LoadMenu(c, m);
                }
            }
            else 
            {
                UI.Dispatcher.Invoke(() => { LoadMenu(command,menuitem); });

            }

        }
        public void ShowSettings() 
        {
            if (UI.Dispatcher.CheckAccess())
            {
                var window = new SettingsWindow();
                window.SetFeatures(this);
                window.Show();
            }
            else
            {
                UI.Dispatcher.Invoke(() => { ShowSettings(); });

            }
        }
        public void LoadFeatures(ControlNode cnode, TreeViewItem uiitem)
        {
            if (UI.Dispatcher.CheckAccess())
            {
                ItemCollection itemcollection = null;
                if (uiitem == null)
                {
                    itemcollection = UI.Tree_Control.Items;
                    UI.Tree_Control.Items.Clear();
                }
                else
                {
                    itemcollection = uiitem.Items;
                }

                foreach (var c in cnode.Children)
                {
                    var m = new TreeViewItem();
                    m.Header = c.Title.Replace("_", "__");
                    //m.IsEnabled = c.Enabled;
                    //if (c.Children.Count == 0)
                    //{
                        m.MouseLeftButtonUp += (s, e) => { c.ExecuteAsync(); };
                    //}
                    itemcollection.Add(m);
                    LoadFeatures(c, m);
                }
            }
            else
            {
                UI.Dispatcher.Invoke(() => { LoadFeatures(cnode, uiitem); });

            }

        }

        public void LoadTaxonomy(string path) 
        {
            Engine.LoadTaxonomy(path);
            //LoadTaxonomyToUI();
        }
        public string BrowsForFolder(string folder)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = GetRegValue(RegSettingsPath + "LastFolder");
            dlg.CheckFileExists = false;
            dlg.CheckPathExists = false;
            dlg.ValidateNames = false;
            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.FileName = "Folder.";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                folder = Utilities.Strings.GetFolder(filename);
                SetRegValue(RegSettingsPath + "LastFolder", folder);
                return folder;
            }
            return "";
        }
        public string BrowsForFile(string folder,string proposedfilename)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = GetRegValue(RegSettingsPath + "LastFolder");
            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.ValidateNames = false;
            dlg.CheckPathExists = false;
            dlg.CheckFileExists = false;
            dlg.FileName = proposedfilename;
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                folder = Utilities.Strings.GetFolder(filename);
                SetRegValue(RegSettingsPath + "LastFolder", folder);
                return filename;
            }
            return "";
        }

        private string GetRegValue(string key, string defaultvalue = "")
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

        private void SetRegValue(string key, string value)
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey sk1 = rk.CreateSubKey(key);
            sk1.SetValue(key.ToUpper(), value);
        }


        public void Search(string p)
        {
            var context = UI.Tree_Control.SelectedItem as ControlNode;
            if (context.Title == "Facts")
            {
                
            }
        }

        public void ShowInBrowser(string path) 
        {
            if (UI.Dispatcher.CheckAccess())
            {
     
                UI.Browser.Navigate(path);
                UI.TB_Title.Text = path;
              
            }
            else
            {
                UI.Dispatcher.Invoke(() => { ShowInBrowser(path); });
            }
        }

        public object ProcessRequest(Request request)
        {
            object result = null;
            var urlparts = request.url.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
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
                            result = json;
                        }
                        if (part1 == "validation")
                        {
                            var json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.CurrentInstanceValidationResultPath);
                            result = json;
                            
                        }
                    }
                }
                if (part0 == "taxonomy")
                {
                    var json = "";
                    if (part1 == "concepts")
                    {
                        json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyConceptPath);

                    }
                    if (part1 == "validationrules")
                    {
                        json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyValidationPath);

                    }
                    if (part1 == "hierarchies")
                    {
                        json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyHierarchyPath);                        

                    }
                    if (part1 == "labels")
                    {
                        json = Utilities.FS.ReadAllText(Engine.CurrentTaxonomy.TaxonomyLabelPath);

                    }
                    result = json;
                }
                if (part0 == "table")
                {
                    if (part1 == "get")
                    {
                        var cell = request.parameters["cell"];
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

                        result = GetTableHtmlPath(report, extension, row, column);
                    }
                    if (part1 == "list")
                    {
                        var h = new BaseModel.Hierarchy<LogicalModel.TableInfo>();
                        var tgs = Engine.CurrentTaxonomy.TableGroups.Select(i => { var ti = new TableInfo(); ti.ID = i.FilingIndicator; ti.Name =  i.FilingIndicator; ti.Description=i.LabelContent; return ti; });
                        foreach (var tg in tgs) 
                        {
                            var htg = new BaseModel.Hierarchy<LogicalModel.TableInfo>(tg);
                            h.Children.Add(htg);
                            var tables = Engine.CurrentTaxonomy.Tables.Where(i=>i.FilingIndicator==tg.ID);
                            //if (tables.Count() == 1) 
                            //{
                            //    htg.Item.ID == String.Format("{0}<>", tables.FirstOrDefault().ID);
                            //}
                            foreach (var tbl in tables)
                            {
                                var tbinfo = new TableInfo();
                                var ht = new BaseModel.Hierarchy<LogicalModel.TableInfo>(tbinfo);
                                ht.Item.SetFrom(tbl);
                                ht.Item.ID = String.Format("{0}<>", tbl.ID);

                                htg.Children.Add(ht);
                                foreach (var ext in tbl.Extensions) 
                                {
                                    var extinfo = new TableInfo();
                                    extinfo.ID = String.Format("{0}<{1}>", tbl.ID, ext.LabelCode);
                                    extinfo.Name = ext.LabelContent;
                                    var hext = new BaseModel.Hierarchy<LogicalModel.TableInfo>(extinfo);
                                    ht.Children.Add(hext);
                                }
                            }
                         
                          
                        }
                        result = Utilities.Converters.ToJson(h);
                    }
                }
            }

            return result;
        }
    }
}
