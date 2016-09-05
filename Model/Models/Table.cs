﻿using BaseModel;
using LogicalModel.Base;
using LogicalModel.Dimensions;
using LogicalModel.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel
{

    public class Table : Identifiable,ILabeled
    {
        private int datacellminwidth = 100;
        private int cellpadding = 15;
        private int titlecellminwidth = 300; 
        private string _HtmlPath = "";
        public static string DefaultExtensionCode = "000";
        public static string LabelCodeFormat = "{0:D5}";
        public static string KeyLabelCodeFormat 
        {
            get { return "Key " + LabelCodeFormat; }
        }
        public static string ExtensionLableContentFormat = "Extension {0}";

        public string Name { get; set; }
        public string FilingIndicator { get; set; }
        public string HtmlPath 
        {
            get { return _HtmlPath; }
            set { _HtmlPath=value; }
        }
        public string JsonFileName
        {
            get { return _HtmlPath.Replace(".html", ".json"); }
        }
        public string JsonPath
        {
            get { return this.Taxonomy.TaxonomyLayoutFolder + JsonFileName; }
        }
        public string FactsPath
        {
            get { return Taxonomy.TaxonomyLayoutFolder + _HtmlPath.Replace(".html", "-facts.json"); }
        }
        public string FactMapPath
        {
            get { return Taxonomy.TaxonomyLayoutFolder + _HtmlPath.Replace(".html", "-factmap.js"); }
        }
        public string FactPath 
        {
            get { return Taxonomy.TaxonomyLayoutFolder + _HtmlPath.Replace(".html", "-facts.txt"); }
        }
        public string CubePath
        {
            get { return Taxonomy.TaxonomyLayoutFolder + _HtmlPath.Replace(".html", "-cubes.txt"); }
        }
        public string DefPath
        {
            get { return Taxonomy.TaxonomyLayoutFolder + _HtmlPath.Replace(".html", "-def.txt"); }
        }
        public string LayoutPath
        {
            get { return Taxonomy.TaxonomyLayoutFolder + _HtmlPath.Replace(".html", "-layout.txt"); }
        }
        private string _FolderName = "";
        public string FolderName
        {
            get { return _FolderName; }
            set { _FolderName = value; }
        }

        public Taxonomy Taxonomy = null;
        public static Func<String,bool, Label> LabelAccessor = null;
        private List<Dimension> _Dimensions = new List<Dimension>();
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }

       
        public Hierarchy<LayoutItem> LayoutRoot { get; set; }
        [JsonIgnore]
        public List<Hierarchy<LayoutItem>> LayoutItems = new List<Hierarchy<LayoutItem>>();

        private List<HyperCube> _HyperCubes = new List<HyperCube>();
        public List<HyperCube> HyperCubes { get { return _HyperCubes; } set { _HyperCubes = value; } }

        private List<String> _ValidationRules = new List<String>();
        public List<String> ValidationRules { get { return _ValidationRules; } set { _ValidationRules = value; } }

        private List<KeyValue<int, List<Hierarchy<LayoutItem>>>> X_Axis = new List<KeyValue<int, List<Hierarchy<LayoutItem>>>>();
        private List<KeyValue<int, List<Hierarchy<LayoutItem>>>> Y_Axis = new List<KeyValue<int, List<Hierarchy<LayoutItem>>>>();
        private List<KeyValue<int, List<Hierarchy<LayoutItem>>>> Z_Axis = new List<KeyValue<int, List<Hierarchy<LayoutItem>>>>();

        public List<Hierarchy<LayoutItem>> Rows = new List<Hierarchy<LayoutItem>>();
        public List<Hierarchy<LayoutItem>> Columns = new List<Hierarchy<LayoutItem>>();
        private Hierarchy<LayoutItem> _Extensions = new Hierarchy<LayoutItem>();
        public Hierarchy<LayoutItem> Extensions 
        {
            get { return _Extensions; }
            set { _Extensions = value; }
        }

        public LayoutItem CurrentExtension = null;

        private Hierarchy<LayoutItem> rowsnode = null;
        private Hierarchy<LayoutItem> columnsnode = null;
        private Hierarchy<LayoutItem> extensionnode = null;

        //private List<String> _FactList = new List<string>();
        //public List<String> FactList 
        //{
        //    get { return _FactList;  }
        //    set { _FactList = value; }
        //}

        //private List<Cell> _LayoutCells = new List<Cell>();
        //public List<Cell> LayoutCells
        //{
        //    get { return _LayoutCells; }
        //    set { _LayoutCells = value; }
        //}
        private Dictionary<string, Cell> _LayoutCellDictionary = new Dictionary<string, Cell>();
        public Dictionary<string, Cell> LayoutCellDictionary
        {
            get { return _LayoutCellDictionary; }
            set { _LayoutCellDictionary = value; }
        }

        //protected List<String> FactKeys = new List<string>();
        public int InstanceFactsCount
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1} - {2}", this.GetType().Name, this.ID, this.Name);
        }

        public void BuildLevels(List<KeyValue<int, List<Hierarchy<LayoutItem>>>> AxisLevels, Hierarchy<LayoutItem> item, bool childfirst = false, int level = 0)
        {
            var lvlitem = AxisLevels.FirstOrDefault(i => i.Key == level);
            if (lvlitem == null)
            {
                lvlitem = new KeyValue<int, List<Hierarchy<LayoutItem>>>(level, new List<Hierarchy<LayoutItem>>());
                AxisLevels.Add(lvlitem);
            }
            if (!childfirst)
            {
                AddLevelitem(lvlitem, item);
            }
            var children = item.Children.Where(i => i.Item.IsStructural).ToList();
            foreach (var child in children)
            {
                child.Parent = item;
                BuildLevels(AxisLevels, child, childfirst, level + 1);

            }

            if (childfirst)
            {
                AddLevelitem(lvlitem, item);

            }
 
        }

        public void SetSpans(Hierarchy<LayoutItem> item, Hierarchy<LayoutItem> root) //, int levelcount
        {
            if (root == null)
            {
                root = item;
            }
            var lc = item.GetLeafCount(i=>i.Item.IsStructural);
            var level = item.GetLevel(root);
            var totallevelcount = root.GetSubLevelCount(i=>i.Item.IsStructural);
            var sublevelcount = totallevelcount - level;

            var colspan = lc > 1 ? String.Format("colspan=\"{0}\"", lc) : "";
            item.Item.ColSpan = colspan;
            var children = item.Children.Where(i => i.Item.IsStructural).ToList();
            if (children.Count == 0)
            {
                var rowspan = sublevelcount > 0 ? String.Format("rowspan=\"{0}\"", sublevelcount+1) : "";
                item.Item.RowSpan = rowspan;
            }
            foreach (var child in children) 
            {
                SetSpans(child, root);
            }
  
        }

        public void FixLayoutItem(Hierarchy<LayoutItem> item,Hierarchy<LayoutItem> root, bool childfirst = false)
        {
            if (root == null) 
            {
                root = item;
            }
            int totallevelcount = root.GetSubLevelCount(i => i.Item.IsStructural);
            var placeholderitem = new LayoutItem(item.Item);
            placeholderitem.LabelContent = " ";
            placeholderitem.IsPlaceholder = true;
            placeholderitem.ID = "PH_" + placeholderitem.ID;
            /*
            var level = item.GetLevel(root);
            var sublevelcount = totallevelcount - level + 1;
            var rowspan = sublevelcount > 0 ? String.Format("rowspan=\"{0}\"", sublevelcount) : "";
            placeholderitem.RowSpan = rowspan;
            */
            var placeholder = new Hierarchy<LayoutItem>(placeholderitem);


            if (!childfirst && ShouldAddPlaceholder(item))
            {

                AddPlaceHolderIfNotExists(item, placeholder, 0);
                item.Item.IsAbstract = true;

            }
            foreach (var child in item.Children)
            {
                FixLayoutItem(child, root, childfirst);
            }
            if (childfirst && ShouldAddPlaceholder(item))
            {
                AddPlaceHolderIfNotExists(item, placeholder, -1);

            }
        }

        private bool ShouldAddPlaceholder(Hierarchy<LayoutItem> item) 
        {
            var result = false;
            if (!item.Item.IsAbstract && item.Item.IsVisible) 
            { 
                if (item.Children.Count>0)
                {
        
                    var isallabstarct = item.FirstOrDefault(i =>
                        !i.Item.IsAbstract 
                        && i.Item.IsVisible 
                        && i!=item) == null;
                    if (!isallabstarct) 
                    {
                        result = true;
                    }
                } 
            }
            return result;
        }

        private void AddPlaceHolderIfNotExists(Hierarchy<LayoutItem> target, Hierarchy<LayoutItem> placeholder, int ix)
        {
            if (!target.Item.IsAbstract && target.Children.Count > 0)
            {
                var existing = target.Children.FirstOrDefault(i => i.Item.ID == placeholder.Item.ID);
                if (existing == null)
                {
                    if (ix == -1)
                    {
                        ix = target.Children.Count;
                    }
                    target.Children.Insert(ix, placeholder);
                    placeholder.Parent = target;
                }
            }
        }

        private void AddLevelitem(KeyValue<int, List<Hierarchy<LayoutItem>>> levelcontainer, Hierarchy<LayoutItem> item) 
        {
            levelcontainer.Value.Add(item);

        }

        public void SetHtmlPath()
        {
            string folder = Taxonomy.TaxonomyLayoutFolder;
            var filepath = this.ID + ".html";
            HtmlPath = filepath;
        }

        public string FullHtmlPath
        {
            get 
            {
                return String.Format("{0}{1}", Taxonomy.TaxonomyLayoutFolder, HtmlPath);
            }
        }

        public void MapCells() 
        {

        }

        public void Reload() 
        {
            if (LayoutItems.Count == 0)
            {
                LayoutRoot.SetParents();
                this.LayoutItems = this.LayoutRoot.All();
                this.LayoutItems.ForEach(i => i.Item.Table = this);
            }

            foreach (var hypercube in HyperCubes) 
            {
                hypercube.SetReferences();
            }
            LoadLabels();
        }

        private void LoadLabels()
        {
            foreach (var item in LayoutItems) 
            {
                item.Item.LoadLabel(this.Taxonomy);
            }
        }

        public void LoadDefinitions() 
        {
            var folder = this.FolderName;
            var findlabel = Taxonomy.TaxonomyLabels.FirstOrDefault(
                i => i.Type == Literals.FilingIndicator && i.FileName == folder);

            if (findlabel != null)
            {
                this.FilingIndicator = findlabel.Content;
            }
            else 
            {
                Logger.WriteLine(String.Format("Filing Indicator not found for table {0}", this.ID));
            }
      // " http://www.eurofiling.info/xbrl/role/filing-indicator-code"

            var slices = new List<IEnumerable<QualifiedName>>();
            var sb_fact = new StringBuilder();
            //FactKeys.Clear();
    
            foreach(var hypercube in HyperCubes){
                var cubeslices = GetCubeSlices(hypercube);
                foreach (var slice in cubeslices) {
                    var childslices = slice.ToList();
                    var key = "";
                    var intkeys = new List<int>() ;
                    var conceptslicechild = childslices[0];
                    if (conceptslicechild is Concept == false) 
                    {
                        throw new Exception("First item of the hypercube slice is not a Concept!"); 
                    }
                    childslices.Remove(conceptslicechild);
                    //childslices=childslices.OrderBy(i=>(i as DimensionMember).Domain)
                    var conceptpart = string.Format("{0},",conceptslicechild.FullName);
                    //sb_fact.Append(conceptpart);
                    key += conceptpart;

                    intkeys.Add(Taxonomy.FactParts[conceptslicechild.FullName]);

                    var dimkeyparts =new List<string>();
                    for (int i = 0; i < childslices.Count; i++)
                    {
                        var slicechild = childslices[i];

                        var dimmember = slicechild as DimensionMember;
                        if (!dimmember.IsDefaultMember)
                        {
                            var dimitem = String.Format("[{0}]{1}", dimmember.Domain.DimensionItem.FullName, dimmember.FullName);
                            //old
                            //if (dimmember is TypedDimensionMember)
                            //{
                            //   // dimitem = String.Format("[{0}]{1},", dimmember.Domain.DimensionItem.FullName, dimmember.Domain.ID);
                            //    dimitem = String.Format("[{0}]{1},", dimmember.Domain.DimensionItem.FullName, dimmember.FullName);

                            //}
                            //else
                            //{
                            //}
                            dimkeyparts.Add(dimitem);

                        }


                    }
                    dimkeyparts.Sort(StringComparer.Ordinal);
                    //dimkeyparts = dimkeyparts.OrderBy(i => i, StringComparer.Ordinal);
                  
                    foreach (var dimkeypart in dimkeyparts)
                    {
                        //performance
                        //key += dimkeypart + ",";
                        intkeys.Add(Taxonomy.FactParts[dimkeypart]);

                    }
                    /*
                    var tempfact = new FactBase();
                    tempfact.SetFromString(key);
                    key = tempfact.GetFactKey();
                    */
                    //FactKeys.Add(key);
                    //performance
                    //sb_fact.AppendLine(key);
                    //var newkey = Taxonomy.GetFactIntKey(key);
                    var newkey = intkeys;
                    if (!this.Taxonomy.HasFact(newkey.ToArray())) 
                    {
                        //if (key == "de_sprv_met:si6,[de_sprv_dim:COL]de_sprv_CL:x010,[*:GRA]de_sprv_typ:GR,[*:ROW]de_sprv_RO:x010,[*:TEM]de_sprv_TE:GRP,") 
                        //{ 

                        //}
                        this.Taxonomy.AddFactKey(newkey);
               
                    }
                    //sb_fact.AppendLine();
                }
            }
            //Logger.WriteLine(String.Format("Facts: {0}", this.Taxonomy.Facts.Count));
            //var factpath = HtmlPath.Replace(".html", "-facts.txt");
            //var cubepath = HtmlPath.Replace(".html", "-cubes.txt");
            //Utilities.FS.WriteAllText(FactPath, sb_fact.ToString());

            //var jsonfacts = Utilities.Converters.ToJson(this.FactList);
            System.IO.File.WriteAllText(FactsPath, sb_fact.ToString());
            sb_fact.Clear();

        }
       
        private void SetExtensions()
        {

            if (this.ID.Contains("07.00"))
            {

            }
            if (extensionnode != null)
            {
                //var leafs = extensionnode.GetLeafs().Where(i => i.Parent != null).ToList();
                var leafs = extensionnode.All().Where(i => i.Item.IsVisible).ToList();
                
                //this.Extensions =  TableHelpers.CombineExtensionNodes(extensionnode, this);
                this.Extensions = TableHelpers.GetExtensions(extensionnode, this);
            }
        }

        public Hierarchy<LayoutItem> CreateAxisNode(string axis) 
        {
            var nodes = LayoutRoot.Where(i => String.Equals(i.Item.Axis, axis, StringComparison.InvariantCultureIgnoreCase)).OrderBy(i => i.Order).ToList();
            var axislayoutitem = new LayoutItem();
            axislayoutitem.Category=LayoutItemCategory.BreakDown;
            axislayoutitem.ID = String.Format("Axis {0}", axis);
            axislayoutitem.IsAbstract = true;
            var axisnode = new Hierarchy<LayoutItem>(axislayoutitem);
            
            //Expand AspectNodes
            foreach (var node in nodes)
            {
                var aspectnodes = node.Where(j => j.Item.IsAspect).ToList();
                foreach (var aspectnode in aspectnodes)
                {
                    if (!node.Item.Dimensions.Any(i => i.IsTyped))
                    {
                        var expandednodes = TableHelpers.GetAspectItems(aspectnode, this);
                        var parent = aspectnode.Parent;
                        parent.Remove(aspectnode);
                        parent.AddChildren(expandednodes);
                    }

                }
            }
       
            //Project Axisnodes
            if (nodes.Count > 0)
            {
                var basenode = nodes.FirstOrDefault();

                for (int i=1;i<nodes.Count;i++)
                {
                    var node= nodes[i];
                    var baseleafs = basenode.GetLeafs();
                    List<LayoutItem> projectionnodes = null;

                    projectionnodes = node.GetLeafs().Select(l => l.Item).ToList();
                    foreach (var leaf in baseleafs)
                    {
                        foreach (var projectionnode in projectionnodes)
                        {
                            var hi = new Hierarchy<LayoutItem>(projectionnode);
                            leaf.AddChild(hi);
                        }
                    }
                }
                axisnode.Children.Add(basenode);
                basenode.Item.Category = LayoutItemCategory.BreakDown;
            }

            //previous
            //foreach (var node in nodes) 
            //{
            //    axisnode.Children.Add(node);
            //    node.Item.Category = LayoutItemCategory.BreakDown;
            //}
            return axisnode;
     
        }


        private void FixLabelCodes(List<Hierarchy<LayoutItem>> items,string format) 
        {
            var ix = 1;
            foreach (var hli in items) 
            {
                if (!hli.Item.IsDynamic && String.IsNullOrEmpty(hli.Item.LabelCode)) 
                {
                    hli.Item.LabelCode = String.Format(format, ix);
                    ix++;
                }
            }
        }

        private void FixNamespaces(List<Hierarchy<LayoutItem>> items)
        {
            foreach (var hli in items)
            {
                if (hli.Item.Concept != null)
                {
                    var concept = hli.Item.Concept;
                   
                    var ce = this.Taxonomy.Concepts.Values.FirstOrDefault(i=>i.Name==concept.Name);
                    if (ce != null && concept.Namespace!=ce.Namespace)
                    {
                        concept.Namespace = ce.Namespace;
                    }
                }
            }
        }

        public LayoutItem GetDefaultExtension() 
        {
            var li = new LayoutItem();
            li.Category = LayoutItemCategory.BreakDown;
            li.ID = "";
            var label = new Label();
            label.Code = DefaultExtensionCode;
            label.Lang = "en";
            label.Content = "Default";
            li.Label = label;
            return li;
        }

        public LayoutItem GetRootExtension()
        {
            var li = new LayoutItem();
            li.ID = "";
            li.Category = LayoutItemCategory.BreakDown;
            var label = new Label();
            label.Code = DefaultExtensionCode;
            label.Lang = "en";
            label.Content = "Extensions";
            li.Axis = "z";
            li.Label = label;
            return li;
        }
        
        protected void SetMapID(List<LayoutItem> items) 
        {
            foreach (var item in items) 
            {
                if (item.Concept != null)
                {
                    if (!this.Taxonomy.FactParts.ContainsKey(item.Concept.Content))
                    {
                        var msg = String.Format("Cant find FactPart {0} at Table {1}!", item.Concept.Content, this.ID);
                        Utilities.Logger.WriteLine(msg);
                    }
                    item.Concept.MapID = this.Taxonomy.FactParts[item.Concept.Content];
                }
                foreach (var dimension in item.Dimensions) 
                {
                    if (!this.Taxonomy.FactParts.ContainsKey(dimension.DomainMemberFullName))
                    {
                        var msg = String.Format("Cant find FactPart {0} at Table {1}!", dimension.DomainMemberFullName, this.ID);
                        Utilities.Logger.WriteLine(msg);
                    }
                    dimension.MapID = this.Taxonomy.FactParts[dimension.DomainMemberFullName];
                }
            }
        }
        
        public void LoadLayout()
        {
            Logger.WriteLine(String.Format("Layout for {0}", this.ID));
            X_Axis.Clear();
            Y_Axis.Clear();
            Z_Axis.Clear();

            if (this.ID.Contains("S.19.01.01.01"))
            {

            }
            rowsnode = CreateAxisNode("y");
            columnsnode = CreateAxisNode("x");
            extensionnode = CreateAxisNode("z");

   
             var aspects=new List<Hierarchy<LayoutItem>>();
             aspects.AddRange(rowsnode.Where(i => i.Item.IsAspect));
             aspects.AddRange(columnsnode.Where(i => i.Item.IsAspect));

            TableHelpers.SetDynamicAxis(this, rowsnode, columnsnode);
            TableHelpers.SetDynamicAxis(this, columnsnode, rowsnode);       
            
            //fix here
            FixLayoutItem(columnsnode,null);
            SetSpans(columnsnode,null);

            BuildLevels(X_Axis, columnsnode);
            BuildLevels(Y_Axis, rowsnode);
            Func<Hierarchy<LayoutItem>, bool> IsChildren = i => 
                ( !i.Children.Any(j => j.Item.IsVisible) && i.Item.IsVisible) 
                && i!=columnsnode;

            //Columns = columnsnode.Where(i => IsChildren(i));
            Columns = columnsnode.ToHierarchyList().Where(i => i.Item.IsVisible).ToList();


            //Rows = rowsnode.ToHierarchyList().Where(i => i.Item.IsStructural && !i.Item.IsAbstract).ToList();
            Rows = rowsnode.ToHierarchyList().Where(i => i.Item.IsVisible).ToList();
            Rows = Rows.Where(i => i.Item.Label!=null || i.Item.IsDynamic).ToList();

            FixLabelCodes(Columns, Table.LabelCodeFormat);
            FixLabelCodes(Rows, Table.LabelCodeFormat);

            var xRows = Rows.Where(i => String.IsNullOrEmpty(i.Item.LabelCode)).ToList();
            if (xRows.Count > 1) 
            {

            }
            if (Extensions.Children.Count == 0)
            {
                SetExtensions();
            }
            else 
            {
                foreach (var Extension in Extensions.All())
                {
                    Extension.Item.Table = this;
                }
            }
            var factdeflist = new List<String>();
            var blocked = new Dictionary<string, bool>();
  
            if (LayoutCellDictionary.Count == 0)
            {

                var exts = Extensions.Children.Select(i => i.Item).ToList();
                if (exts.Count == 0)
                {    
                    exts.Add(Extensions.Item);
                }

                //Set MapID for Dimensions and concepts

                SetMapID(exts);
                SetMapID(rowsnode.All().Select(i=>i.Item).ToList());
                SetMapID(columnsnode.All().Select(i => i.Item).ToList());
                //var factmap = new Dictionary<string, Dictionary<string,string>>();
                foreach (var ext in exts)
                {
                    //var factextdict = new Dictionary<string, string>();
                    //factmap.Add(ext.FactString, factextdict);

                    foreach (var row in Rows)
                    {
                        foreach (var col in Columns)
                        {
                            var tempcell = new Cell();
                            tempcell.Row = row.Item.LabelCode;
                            tempcell.Column = col.Item.LabelCode;
                            Cell cell = null;
  
                            if (!this.LayoutCellDictionary.ContainsKey(tempcell.LayoutID))
                            {
                                cell = new Cell();
                                cell.Report = this.ID;
                                cell.LayoutRow = row;
                                cell.Row = row.Item.LabelCode;
                                cell.LayoutColumn = col;
                                cell.Column = col.Item.LabelCode;
                                cell.Concept = row.Item.Concept != null ? row.Item.Concept : col.Item.Concept;
                                var isrowkey = row.Item.IsDynamic && cell.Concept == null;
                                var iscolkey = col.Item.IsDynamic && cell.Concept == null;
                                if (!isrowkey)
                                {
                                    cell.Dimensions.AddRange(row.Item.Dimensions);//.Where(i=> !i.IsDefaultMember));
                                }
                                if (!iscolkey)
                                {
                                    cell.Dimensions.AddRange(col.Item.Dimensions);//.Where(i => !i.IsDefaultMember));
                                }
                                var rowRole = String.IsNullOrEmpty(row.Item.Role) ? "" : String.Format("Role:{0};Axis:{1};", row.Item.Role, row.Item.RoleAxis);
                                var colRole = String.IsNullOrEmpty(col.Item.Role) ? "" : String.Format("Role:{0};Axis:{1};", col.Item.Role, col.Item.RoleAxis);
                                cell.Role = iscolkey ? rowRole : isrowkey ? colRole : "";
                                cell.IsKey = isrowkey || iscolkey;
                                if (cell.IsKey) 
                                {

                                }
                                SetDimensions(cell);
                                cell.Dimensions = cell.Dimensions.Where(i => !i.IsDefaultMember).ToList();
                                //LayoutCells.Add(cell);
                                LayoutCellDictionary.Add(cell.LayoutID, cell);
                            }
                            else 
                            {
                                cell = this.LayoutCellDictionary[tempcell.LayoutID];
                            }
                            var xcell = new Cell();
                            xcell.Report = cell.Report;
                            xcell.Row = cell.Row;
                            xcell.Column = cell.Column;
                            xcell.Extension = ext.LabelCode;
                            xcell.Concept = cell.Concept == null ? ext.Concept : cell.Concept;
                            xcell.Dimensions.AddRange(cell.Dimensions);
                            xcell.Dimensions.AddRange(ext.Dimensions);
                            //xcell.Dimensions = xcell.Dimensions.OrderBy(i => i.DomainMemberFullName, StringComparer.Ordinal).ToList();
                            xcell.Dimensions = xcell.Dimensions.OrderBy(i => i.MapID).ToList();
                            //xcell.Dimensions.Sort((x1, x2) => String.CompareOrdinal( x1.DomainMemberFullName, x2.DomainMemberFullName));
          
                            var columndimensions = cell.LayoutColumn.Item.Dimensions;
                            var factkeys = new List<String>(20);
                            var factintkeys = new List<int[]>(20);


                            factintkeys.Add(xcell.FactIntKey);
                            //factkeys.Add(xcell.FactKey);
                            cell.IsBlocked = cell.IsKey ? false : true;
                            for (int i = 0; i < factintkeys.Count; i++)
                            //foreach (var factintkey in factintkeys)
                            {
                                var factintkey = factintkeys[i];
                                //var factkey = factkeys[i];
                                if (this.Taxonomy.Facts.ContainsKey(factintkey))
                                {
                                    //var item = this.Taxonomy.GetCellsOfFact(factkey);
                                    var item = this.Taxonomy.GetCellsOfFact(factintkey);
                                    item.Add(xcell.CellID);
                                    cell.IsBlocked = false;

                                }
                               
                            }
                            /*Old 
                            
                            var dimensionswithoutmember = cell.Dimensions.Where(i => String.IsNullOrEmpty(i.DomainMember) && !i.IsTyped).ToList();
                            var firstsuchdim = dimensionswithoutmember.FirstOrDefault();
                            if (firstsuchdim != null) 
                            {
                                var dimensiondomain = firstsuchdim.Domain;
                                dimensiondomain = dimensiondomain.IndexOf("_") > -1 ? dimensiondomain.Substring(dimensiondomain.LastIndexOf("_") + 1) : dimensiondomain;
                                var aspect = aspects.FirstOrDefault(i=>i.Item.Dimensions.Any(j=>j.DimensionItemFullName==firstsuchdim.DimensionItemFullName));
                                var role = aspect.Item.Role;
                                var domainmembers = GetMembersOf(firstsuchdim.Domain, role);

                                foreach (var domainandmember in domainmembers)
                                {
                                    var newfactkey = "";
                                    var tempfact = new FactBase();
                                    tempfact.SetFromString(xcell.FactKey);
                                    var dimension = tempfact.Dimensions.FirstOrDefault(i => i.DimensionItemFullName == firstsuchdim.DimensionItemFullName);
                                    if (dimension.Domain.EndsWith(":"))
                                    {
                                        dimension.Domain = dimension.Domain.Substring(0, dimension.Domain.Length - 1);
                                    }

                                    var qn = new QualifiedName(domainandmember);
                                    if (qn.Name == "x0")
                                    {
                                        tempfact.Dimensions.Remove(dimension);
                                    }
                                    else
                                    {
                                        dimension.DomainMember = qn.Name;
                                    }
                                    newfactkey = tempfact.GetFactKey();

                                    factkeys.Add(newfactkey);
                                }
                            }
                            else
                            {
                                //factkeys.Add(xcell.FactKey);
                                factintkeys.Add(xcell.FactIntKey);
                            }

                            cell.IsBlocked = cell.IsKey ? false : true;
                            foreach (var factkey in factkeys)
                            {
                                if (this.Taxonomy.HasFact(factkey))
                                {
                                    var item = this.Taxonomy.GetCellsOfFact(factkey);
                                    item.Add(xcell.CellID);
                                    cell.IsBlocked = false;

                                }
                                else
                                {
                                    if (this.ID.Contains("S.02.02.01.02")) //s2md_tS.02.02.01.02-facts.json
                                    {
                                    }
                                    var aspectcolumnitem = cell.LayoutColumn.Item;
                                    if (cell.LayoutColumn.Parent != null && !aspectcolumnitem.IsAspect) 
                                    {
                                        aspectcolumnitem = cell.LayoutColumn.Parent.Item;
                                    }

                               
                                    if (cell.Concept==null
                                        && cell.LayoutColumn.Item.Category != LayoutItemCategory.BreakDown
                                        && cell.LayoutRow.Item.Category != LayoutItemCategory.BreakDown
                                        )
                                    {
                                        //cell.Dimensions = aspectcolumnitem.Dimensions;
                                    }
                                    else
                                    {
                                        //cell.IsBlocked = true;
                                       
                                    }
                                }
                            }
                            */
                            if (cell.IsBlocked && !blocked.ContainsKey(cell.ToString()))
                            {
                                blocked.Add(cell.ToString(), true);
                            }
                        }
                    }
                }
                //var jscontent = "var FactMap = " + jsoncontent.Replace("\r\n", "\\ \r\n") + ";";
                var jscontent = "{\r\n";//{\"FactMap\": " + jsoncontent + ", \r\n";
                jscontent += "\"ExtensionsRoot\": " + Utilities.Converters.ToJson(Extensions) + ", \r\n";
                jscontent += "\"HtmlTemplatePath\": \"" + this.HtmlPath + "\"}";
                System.IO.File.WriteAllText(FactMapPath, jscontent);


                

            }

            CreateHtmlLayout();
        }

        private Dictionary<string, List<string>> _Hierarchies = new Dictionary<string, List<string>>();
        private List<String> GetMembersOf(string domain, string role) 
        {
            var members = new List<string>();
            if (!_Hierarchies.ContainsKey(domain))
            {
                // i => i.Item.Name == domain &&
                var hierarchy = this.Taxonomy.Hierarchies.SingleOrDefault(
                                            i=>i.Item.Role == role 
                                         );
                var domainmembers = hierarchy.Where(i => i.Item.Namespace == domain).Select(i => i.Item.Content).Distinct().ToList();
                _Hierarchies.Add(domain, domainmembers);
            }
            members = _Hierarchies[domain];
            return members;
        }

        public void EnsureHtmlLayout()
        {
            Taxonomy.ManageUIFiles();

            if (!System.IO.File.Exists(FullHtmlPath))
            {
                CreateHtmlLayout();
            }
        }
        
        public void CreateHtmlLayout(bool regenerate = false)
        {
            if (regenerate || !System.IO.File.Exists(FullHtmlPath))
            {
                var html = GetTableHtml();
                Utilities.FS.WriteAllText(FullHtmlPath, html);
        
            }
        }

        public string GetStyle() 
        {
            var sb = new StringBuilder();
            //sb.AppendLine(String.Format(".report td.data{{ min-width:{0}px; }}", datacellminwidth));
            //sb.AppendLine(String.Format(".report th.title{{ min-width:{0}px; }}", titlecellminwidth));
            //sb.AppendLine(String.Format(".report th.pad{{ min-width:{0}px; }}", cellpadding));
            var tableminwidth=this.Columns.Count * datacellminwidth + Y_Axis.Count * cellpadding + titlecellminwidth;
            sb.AppendLine(String.Format("table.report {{ min-width:{0}px; }}", tableminwidth));
            return sb.ToString();
        }

        public string GetTableHtml()
        {
            if (this.Name.Contains("09.01.a"))
            {

            }
            
            var sb = new StringBuilder();
            sb.AppendLine("<table class=\"report\">");
            sb.AppendLine("<thead>");
            var collevelnr= X_Axis.Count;
            var rowlevelnr= rowsnode.GetSubLevelCount(i=>i.Item.IsStructural) + 1;
            //var xleafcount = columnsnode.GetLeafCount(null);


            sb.AppendLine(String.Format("<tr><th class=\"left\" colspan=\"{1}\"><h1>{0}</h1></th></tr>", LayoutRoot.Item.LabelContent, rowlevelnr + Columns.Count + 1));
            var columncoderow = "";
            foreach (var lvl in X_Axis)
            {
                sb.AppendLine("<tr>");
                if (lvl.Key == 0)
                {
                    sb.AppendLine(String.Format("<th id=\"Extension\" colspan=\"{0}\" rowspan=\"{1}\">{2}</th>", rowlevelnr + 1, collevelnr + 1, rowsnode.Item.LabelContent));

                }
      
    
                foreach (var item in lvl.Value)
                {
                    var cssclass = "class=\"\"";

                    if (item.Children.Where(i=>i.Item.IsStructural).Count() > 0) {
                        cssclass = cssclass.Replace("=\"", "=\"haschild ");

                    }
                    if (item.Item.IsPlaceholder) 
                    {
                        cssclass = cssclass.Replace("=\"", "=\"placeholder ");
                    }
                    sb.AppendLine(String.Format("<th {0} {1} {5} factid=\"{3}\" title=\"{4}\">{2}</th>",
                        item.Item.ColSpan, item.Item.RowSpan, item.Item.LabelContent, item.Item.FactString, item.Item.FactString, cssclass));
                }
                sb.AppendLine("</tr>");

            }
            foreach (var column in Columns) 
            {
                var colclass = "";
                //if (column.Item.IsAspect || column.Item.Dimensions.FirstOrDefault(i => i.IsTyped) != null || column.Item.IsDynamic)
                if (column.Item.IsDynamic)
                {
                    colclass = "dynamic";
                }
                colclass = String.IsNullOrEmpty(colclass) ? "" : "class=\"" + colclass + "\"";
                columncoderow = columncoderow + String.Format("<th {0} {1}>{2}</th>\r\n", colclass, "", column.Item.LabelCode);

            }
            sb.AppendLine("<tr>");
            sb.AppendLine(columncoderow);
            sb.AppendLine("</tr>");

            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            //new version
            var maxlevel = Y_Axis.Count;
            foreach (var row in Rows) 
            {
                var level = row.GetLevel(rowsnode); //-3 for the LayoutRoot and -2 for the Rows roots node
                var sublevelcount = level > -1 ? maxlevel - level : maxlevel;
                var rowclass = "";
                //if (row.Item.IsAspect || row.Item.Dimensions.FirstOrDefault(i => i.IsTyped) != null || row.Item.IsDynamic)
                if ( row.Item.IsDynamic)
                {
                    rowclass = "dynamic";
                }
                sb.AppendLine(String.Format("<tr class=\"{0}\">", rowclass));
                var colspan = sublevelcount > 0 ? String.Format(" colspan=\"{0}\"", sublevelcount) : "";

                if (level > 0)
                {
                    var tagcolspan = String.Format(" colspan=\"{0}\"", level);
                    var width = 10 * level;
                    //sb.AppendLine(String.Format("<th {0} style=\"min-width:{1}px\"></th>", tagcolspan, width));

                }
                var rowspan = "";

                //adding cells for parent padding
                for (int i = 0; i < level; i++)
                {
                    sb.AppendLine("<th class=\"pad\">&nbsp;</th>");
                }
                
                //adding the label cell

                sb.AppendLine(String.Format("<th class=\"title left\" {0}{1} factid=\"{3}\" title=\"{4}\">{2}</th>", 
                colspan, rowspan,
                row.Item.LabelContent, row.Item.FactString, row.Item.LabelID+"  "+row.Item.FactString));
                
                //adding the code cell
                if (String.IsNullOrEmpty(row.Item.LabelCode))
                {

                }
                sb.AppendLine(String.Format("<th class=\"code left\">{0}</th>", row.Item.LabelCode));

                //adding the data cells
                foreach (var column in Columns) 
                {
                    var tempcell = new Cell();
                    tempcell.Row = row.Item.LabelCode;
                    tempcell.Column = column.Item.LabelCode;
                    Cell cell = null;

                    cell = LayoutCellDictionary[tempcell.LayoutID];
                    //if (!String.IsNullOrEmpty(cell.FactKey) && String.IsNullOrEmpty(row.Item.LabelCode)) 
                    //{
                    //}

                    var alt = String.Format("{0}|{1}", row.Item.LabelCode, column.Item.LabelCode);
                    var cssclass = "data";
                    if (cell.IsBlocked) 
                    {
                        cssclass+=" blocked";
                    }
                    if (cell.IsKey)
                    {
                        cssclass += " key";
                    }
                    var rl = String.IsNullOrEmpty(cell.Role) ? "" : String.Format("role=\"{0}\"", cell.Role);
                    sb.AppendLine(String.Format(
                        "<td id=\"{0}\" {3} factstring=\"{2}\" class=\"{1}\" title=\"{0}\"></td>",
                        alt, cssclass, cell.FactKey, rl
                        ));
                }

                sb.AppendLine("</tr>");

            }
            

            sb.AppendLine("</tbody>");

            sb.AppendLine("</table>");
            return sb.ToString();

        }
        
        private void SetDimensions(Cell cell)
        {
            var currentrow = cell.LayoutRow.Parent;
            while (currentrow != null)
            {
                MergeDimensions(cell.Dimensions, currentrow.Item.Dimensions);
                if (cell.Concept == null) 
                {
                    cell.Concept = currentrow.Item.Concept;
                }
                currentrow = currentrow.Parent;
            }
            var currentcol = cell.LayoutColumn.Parent;
            while (currentcol != null)
            {
                MergeDimensions(cell.Dimensions, currentcol.Item.Dimensions);
                if (cell.Concept == null)
                {
                    cell.Concept = currentcol.Item.Concept;
                }
                currentcol = currentcol.Parent;
            }
            cell.Dimensions = cell.Dimensions.Where(i => !i.IsDefaultMember).OrderBy(i=>i.MapID).ToList();
            cell.Dimensions = cell.Dimensions.Distinct().ToList();
        }


        private void MergeDimensions(List<Dimension> target, List<Dimension> items)
        {
            foreach (var item in items)
            {
                var existing = target.FirstOrDefault(i => i.Domain == item.Domain && i.DimensionItem == item.DimensionItem);
                if (existing == null)
                {
                    target.Add(item);
                }
            }
        }

        #region Cubes

        public IEnumerable<IEnumerable<QualifiedName>> GetCubeSlices(HyperCube cube)
        {
            var cubelist = new List<List<QualifiedName>>();

            cubelist.Add(cube.Concepts.Cast<QualifiedName>().ToList());
            var domains = cube.DimensionItems.OrderBy(i => i.FullName).Select(i => i.Domains.FirstOrDefault()).OrderBy(i => i.FullName).ToList();
            foreach (var domain in domains)
            {
                cubelist.Add(domain.DomainMembers.Cast<QualifiedName>().ToList());
            }
            var combinations = Utilities.MathX.CartesianProduct(cubelist);

            return combinations;

        }
        /*
        public List<HyperCube> GetHyperCubes(LayoutItem row, LayoutItem column, StringBuilder sb)
        {
            var result = new List<HyperCube>();
            if (row.IsAbstract || column.IsAbstract)
            {
                return result;
            }
            var concept = row.Concept == null ? column.Concept : row.Concept;
            var dimensions = new List<Dimension>();
            dimensions.AddRange(row.Dimensions);
            dimensions.AddRange(column.Dimensions);
            dimensions = dimensions.Distinct().ToList();

            var cubeswithconcept = this.HyperCubes.Where(i => i.Concepts.Any(j => j.FullName == concept.Content)).ToList();
            var cubes = cubeswithconcept.ToList();
            foreach (var dimension in dimensions)
            {
                if (!dimension.IsDefaultMemeber)
                {
                    cubes = cubes.Where(i => i.HasDimension(dimension)).ToList();
                    if (cubes.Count == 0)
                    {
                        sb.AppendLine(String.Format("dimension {0} not found for R{1}|C{2}", dimension, row.LabelCode, column.LabelCode));
                    }
                }
            }
            foreach (var cube in cubes)
            {
                var zdimensionitems = Extensions.SelectMany(i => i.Dimensions).Distinct().ToList(); ;
                var notin = cube.NotIn(dimensions, zdimensionitems);
                if (notin.Count == 0)
                {
                    result.Add(cube);
                }
                else
                {

                }
            }

            return result;

        }
        */
        private string CubesToString(List<HyperCube> cubes)
        {
            var sb = new StringBuilder();
            foreach (var cube in cubes)
            {
                sb.AppendLine(cube.ToFullString());
            }
            return sb.ToString();
        }
     
        #endregion


        private Label _Label;
        public Label Label { get { return _Label; } set { _Label = value; } }

        public string LabelContent
        {
            get { return _Label != null ? _Label.Content : ""; }
        }

        public string LabelCode
        {
            get { return _Label != null ? _Label.Code : ""; }
        }

        public string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }

    }


}
