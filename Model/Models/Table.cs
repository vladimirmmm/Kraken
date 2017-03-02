using BaseModel;
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
        public List<string> XmlPathColection = new List<string>();
        public static string DefaultExtensionCode = "000";
        public static string LabelCodeFormat = "{0:D5}";
        public static string KeyLabelCodeFormat 
        {
            get { return "Key " + LabelCodeFormat; }
        }
        public static string ExtensionLableContentFormat = "Extension {0}";

        public string Name { get; set; }
        public string FilingIndicator { get; set; }
        public string RelatedFiles 
        {
            get { 
                var sb = new StringBuilder();
                foreach (var xml in XmlPathColection)
                {
                    sb.Append(xml + "\n");
                }
                sb.Append(this.LayoutPath+"\n");
                sb.Append(this.DefPath + "\n");
                return sb.ToString();
            }

        }
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
        public string MappingTextPath
        {
            get { return Taxonomy.TaxonomyLayoutFolder+@"Mapping\" + _HtmlPath.Replace(".html", ".txt"); }
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

        private List<Hierarchy<LayoutItem>> _Rows = new List<Hierarchy<LayoutItem>>();
        public List<Hierarchy<LayoutItem>> Rows { get { return _Rows; } set { _Rows = value; } }
        
        private List<Hierarchy<LayoutItem>> _Columns = new List<Hierarchy<LayoutItem>>();
        public List<Hierarchy<LayoutItem>> Columns { get { return _Columns; } set { _Columns = value; } }

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

        protected FactsPartsDictionary FactsOfParts = new FactsPartsDictionary();
        protected Dictionary<int, List<int>> FactsOfPartsTemp_Old = new Dictionary<int, List<int>>();
        protected Dictionary<int, IntervalList> FactsOfPartsTemp = new Dictionary<int, IntervalList>();
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
        private IntervalList _FactindexList = new IntervalList();
        public IntervalList FactindexList 
        {
            get { return _FactindexList; }
            set { _FactindexList = value; }
        }

        private List<int[]> FactList = new List<int[]>();
        public List<Tintint> FactIndexToCells = new List<Tintint>();

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
                    placeholder.Item.LabelCode = target.Item.LabelCode;
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
        public void ReloadLayout()
        {
            LayoutRoot.SetParents();
            this.LayoutItems = this.LayoutRoot.All();
            this.LayoutItems.ForEach(i => 
            { 
                i.Item.Table = this;
                foreach (var dim in i.Item.Dimensions) 
                {
                    Taxonomy.SetMapID(dim);
                }
            });
            var allaxisvalues = this.Extensions.Children.Union(this.Rows).Union(this.Columns).ToList();
            allaxisvalues.ForEach(i =>
            {
                foreach (var dim in i.Item.Dimensions)
                {
                    Taxonomy.SetMapID(dim);
                }
            });
        }
        public void Reload() 
        {
            //if (LayoutItems.Count == 0)
            //{
            //    LayoutRoot.SetParents();
            //    this.LayoutItems = this.LayoutRoot.All();
            //    this.LayoutItems.ForEach(i => i.Item.Table = this);
            //}
            ReloadLayout();
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
   

  
        public void LoadDefinitions2()
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

            var slices = new List<IEnumerable<int>>();
            var sb_fact = new StringBuilder();
            //FactKeys.Clear();
            if (this.ID.Contains("eba_tC_07.00.a")) 
            {

            }
            var ic = new IntArrayEqualityComparer();
           //
            var factcounter = 0;
            foreach (var hypercube in HyperCubes)
            {
                var domainiddict = new Dictionary<int, IntervalList>();
                var cubeslices = GetCubeSlices2(hypercube, domainiddict);
                var domainids = domainiddict.Keys.ToList();
               
                EnsureDomainKeyParts(domainids);

                var domaininterval = new IntervalList();

                foreach (var slice in cubeslices)
                {
                    int[] key = null;
                    var items = new List<int>(domainids.Count + 1);
                    var slicedomainids = new List<int>(domainids.Count);
                    var ix=0;
                    foreach (var k in slice)
                    {
                        if (k != -2) 
                        {
                            items.Add(k);
                            if (ix != 0)
                            {
                                slicedomainids.Add(domainids[ix - 1]);
                            }

                        }
                       
                        ix++;
                    }

                    key = items.ToArray();
                    var concept = key[0];
                    key[0] = -3;
                    Array.Sort(key);
                    key[0] = concept;
                    var factix = 0;

                    var hashkeys = this.Taxonomy.FactsManager.GetHashKeys(key);
                    factix = this.Taxonomy.FactsManager.GetFactIndexByHashKey(hashkeys);

                    if (factix == -1)
                    {
                
                        factix = this.Taxonomy.AddFactKey(key, hashkeys);

                    }

                    domaininterval.Add(factix);

                    this.AddFactKeyParts(key, slicedomainids, factix);

               
                    FactindexList.Add(factix);
                    factcounter++;
                    //sb_fact.AppendLine();
                }
                //
            }
     
            this.HyperCubes.Clear();
            foreach (var item in this.FactsOfPartsTemp) 
            {
         
                this.FactsOfParts.Add(item.Key, item.Value);

            }
            this.FactsOfPartsTemp.Clear();
      
            System.IO.File.WriteAllText(FactsPath, sb_fact.ToString());
            sb_fact.Clear();
            FactIndexToCells = new List<Tintint>(FactindexList.Count);

        }
        public void EnsureDomainKeyParts(IEnumerable<int> domainids) 
        {
            foreach (var domainid in domainids)
            {
                if (!this.FactsOfPartsTemp.ContainsKey(domainid))
                {
                    this.FactsOfPartsTemp.Add(domainid, new IntervalList());
                }

            }
        }
        public void AddFactKeyParts(int[] key,List<int> domainids, int factix)
        {
            foreach (var domainid in domainids) 
            {
                //if (!this.FactsOfPartsTemp.ContainsKey(domainid))
                //{
                //    this.FactsOfPartsTemp.Add(domainid, new IntervalList());
                //}
                this.FactsOfPartsTemp[domainid].Add(factix);

            }
            foreach (var part in key)
            {
                //var dimensiondomainpart = this.Taxonomy.GetDimensionDomainPart(part);
                //if (dimensiondomainpart != -1)
                //{
                //    if (!this.FactsOfPartsTemp.ContainsKey(dimensiondomainpart))
                //    {
                //        this.FactsOfPartsTemp.Add(dimensiondomainpart, new List<int>());
                //    }
                //    this.FactsOfPartsTemp[dimensiondomainpart].Add(factix);
                //    //this.FactsOfParts.AddIfNotExists(dimensiondomainpart, factix);
                //}
                if (!this.FactsOfPartsTemp.ContainsKey(part))
                {
                    this.FactsOfPartsTemp.Add(part, new IntervalList());
                }
                this.FactsOfPartsTemp[part].Add(factix);
                //this.FactsOfParts.AddIfNotExists(part, factix);


            }
        }
        public void AddFactKeyParts(int[] key, int factix) 
        {
            foreach (var part in key)
            {
                var dimensiondomainpart = this.Taxonomy.GetDimensionDomainPart(part);
                if (dimensiondomainpart != -1)
                {
                    if (!this.FactsOfPartsTemp.ContainsKey(dimensiondomainpart)) 
                    {
                        this.FactsOfPartsTemp_Old.Add(dimensiondomainpart, new List<int>());
                    }
                    this.FactsOfPartsTemp[dimensiondomainpart].Add(factix);
                    //this.FactsOfParts.AddIfNotExists(dimensiondomainpart, factix);
                }
                if (!this.FactsOfPartsTemp.ContainsKey(part))
                {
                    this.FactsOfPartsTemp_Old.Add(part, new List<int>());
                }
                this.FactsOfPartsTemp[part].Add(factix);
                //this.FactsOfParts.AddIfNotExists(part, factix);
      

            }
        }
        private void SetExtensions()
        {
            if (this.ID.Contains("09.04")) 
            { 
            }
            if (extensionnode != null)
            {
                //this.Extensions =  TableHelpers.CombineExtensionNodes(extensionnode, this);
                this.Extensions = TableHelpers.GetExtensions3(extensionnode, this);
              
            }
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
                    //item.Concept.MapID = this.Taxonomy.FactParts[item.Concept.Content];
                    this.Taxonomy.SetMapID(item.Concept);
                }
                foreach (var dimension in item.Dimensions) 
                {
               
                    this.Taxonomy.SetMapID(dimension);
                    //dimension.MapID = this.Taxonomy.FactParts[dimension.DomainMemberFullName];
                    //dimension.DomMapID = this.Taxonomy.FactParts[dimension.DimensionDomain];
                }
            }
        }
        
        public void LoadLayout()
        {
            Logger.WriteLine(String.Format("Layout for {0}", this.ID));
            X_Axis.Clear();
            Y_Axis.Clear();
            Z_Axis.Clear();

            if (this.ID.Contains("eba_tC_07.00.a")) 
            {

            }
  
            var lrstr = Utilities.Converters.ToJson(LayoutRoot);
            var lr = Utilities.Converters.JsonTo<Hierarchy<LayoutItem>>(lrstr);
            LayoutRoot = Utilities.Converters.JsonTo<Hierarchy<LayoutItem>>(lrstr);
            ReloadLayout();
            var sbe = new StringBuilder();
            rowsnode = TableHelpers.CreateAxisNode3(this, "y");
            columnsnode = TableHelpers.CreateAxisNode3(this, "x");
            extensionnode = TableHelpers.CreateAxisNode3(this, "z");
            
            LayoutRoot.Clear(); LayoutRoot.AddChildren(new List<Hierarchy<LayoutItem>>() { rowsnode, columnsnode, extensionnode });
            
            sbe.AppendLine(TableHelpers.GetStateOfNodes(LayoutRoot,"Step 1"));

            TableHelpers.SetDynamicAxis2(this, rowsnode, columnsnode);
            TableHelpers.SetDynamicAxis2(this, columnsnode, rowsnode);

            sbe.AppendLine(TableHelpers.GetStateOfNodes(LayoutRoot, "Step 2"));

            TableHelpers.ProjectNodes(rowsnode);
            TableHelpers.ProjectNodes(columnsnode);
            TableHelpers.ProjectNodes(extensionnode);

            columnsnode.SetParents();
            sbe.AppendLine(TableHelpers.GetStateOfNodes(LayoutRoot, "Step 3"));
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
            TableHelpers.SetDimensions(Rows);
            TableHelpers.SetDimensions(Columns);
            Rows = Rows.Where(i => !String.IsNullOrEmpty( i.Item.LabelContent) || i.Item.IsDynamic).ToList();

            FixLabelCodes(Columns, Table.LabelCodeFormat);
            FixLabelCodes(Rows, Table.LabelCodeFormat);

            //var xRows = Rows.Where(i => String.IsNullOrEmpty(i.Item.LabelCode)).ToList();
            //if (xRows.Count > 1) 
            //{

            //}
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
                var bigcell =new Cell();
                //var dimensions =new List<Dimension>();
                //dimensions.AddRange(exts.SelectMany(i=>i.Dimensions));
                //dimensions.AddRange(Rows.SelectMany(i=>i.Item.Dimensions));
                //dimensions.AddRange(Columns.SelectMany(i=>i.Item.Dimensions));



                var rowintervaldict = new Dictionary<int, IntervalList>();
                var colintervaldict = new Dictionary<int, IntervalList>();
                var extintervaldict = new Dictionary<int, IntervalList>();

                var extdomains = new List<int>();
                var rowdomains = new List<int>();
                var coldomains = new List<int>();

                var rows = Rows.Select(i => i.Item).ToList();//.Where(i => i.Category != LayoutItemCategory.Key).ToList();
                var extensions = exts;
                var columns = Columns.Select(i => i.Item).ToList();//.Where(i => i.Category != LayoutItemCategory.Key).ToList();

                foreach (var item in extensions) 
                {
                    if (!item.IsKey)
                    {
                        extdomains.AddRange(item.GetDimensionDomains());
                    }
                }
                extdomains = extdomains.Distinct().ToList();
                foreach (var item in rows)
                {
                    if (!item.IsKey)
                    {
                        rowdomains.AddRange(item.GetDimensionDomains());
                    }
                }
                rowdomains = rowdomains.Distinct().ToList();
                foreach (var item in columns)
                {
                    if (!item.IsKey)
                    {
                        coldomains.AddRange(item.GetDimensionDomains());
                    }
                }
                coldomains = coldomains.Distinct().ToList();
                var ffacts = this.FactindexList;
                //ffacts = null;
                //if (1 == 2)
                //{
                    for (int i = 0; i < extensions.Count; i++)
                    {
                        var item = extensions[i];
                        var keys = item.GetAspectKeys();

                        if (!item.IsKey)
                        {
                            var keyswithoutdomains = item.GetAspectKeysWithoutDomains();
                            var domains = item.GetDimensionDomains();
                            var unuseddomains = extdomains.Except(domains);
                            var data = (IntervalList)this.Taxonomy.SearchFactsGetIndex4(keyswithoutdomains, this.FactsOfParts, ffacts, false);

                            data.Normalize();
                            foreach (var unuseddomain in unuseddomains)
                            {
                                if (this.FactsOfParts.ContainsKey(unuseddomain))
                                {
                                    data = Utilities.Objects.SortedExcept(data, this.FactsOfParts[unuseddomain], null);
                                }
                            }

                            extintervaldict.Add(i, data);
                        }
                    }
                    for (int i = 0; i < rows.Count; i++)
                    {
                        var item = rows[i];
                        var keys = item.GetAspectKeys();
                        if (!item.IsKey)
                        {
                            var domains = item.GetDimensionDomains();
                            var unuseddomains = rowdomains.Except(domains);
                            var data = (IntervalList)this.Taxonomy.SearchFactsGetIndex4(keys, this.FactsOfParts, ffacts, false);
                            data.Normalize();
                            foreach (var unuseddomain in unuseddomains)
                            {
                                if (this.FactsOfParts.ContainsKey(unuseddomain))
                                {
                                    data = Utilities.Objects.SortedExcept(data, this.FactsOfParts[unuseddomain], null);
                                }
                            }
                            rowintervaldict.Add(i, data);
                        }
                    }
                    for (int i = 0; i < columns.Count; i++)
                    {
                        var item = columns[i];
                        var keys = item.GetAspectKeys();
                        if (!item.IsKey)
                        {
                            var domains = item.GetDimensionDomains();
                            var unuseddomains = coldomains.Except(domains);
                            var data = (IntervalList)this.Taxonomy.SearchFactsGetIndex4(keys, this.FactsOfParts, ffacts, false);
                            data.Normalize();
                            foreach (var unuseddomain in unuseddomains)
                            {
                                if (this.FactsOfParts.ContainsKey(unuseddomain))
                                {
                                    data = Utilities.Objects.SortedExcept(data, this.FactsOfParts[unuseddomain], null);
                                }
                            }
                            colintervaldict.Add(i, data);
                        }
                    }
                //}
                var eq = new ArrayEqualityComparer();
                var extix = 0;
                IntervalList extdata = null;
                IntervalList rowdata = null;
                IntervalList coldata = null;
                foreach (var ext in exts)
                {
                    var rowix=0;
                    IList<int> extvsrow = null;
                    extdata = extintervaldict.ContainsKey(extix) ? extintervaldict[extix] : null;
                

                    var members = ext.Dimensions.Where(i => i.MapID != i.DomMapID).ToList();
        
                    foreach (var row in Rows)
                    {
                        rowdata = rowintervaldict.ContainsKey(rowix) ? rowintervaldict[rowix] : null;
                        if (rowdata != null)
                        {
                            extvsrow = Utilities.Objects.IntersectSorted(extdata, rowdata, null);
                        }
                        else 
                        {
                            extvsrow = null;
                        }

                        var colix = 0;
                        foreach (var col in Columns)
                        {
                            var tempcell = new Cell();
                            tempcell.Row = row.Item.LabelCode;
                            tempcell.Column = col.Item.LabelCode;
                            Cell cell = null;
                            var layoutid = String.Format("{0}|{1}", rowix, colix);
                            if (!this.LayoutCellDictionary.ContainsKey(layoutid))
                            {
                                cell = new Cell();
                                cell.Report = this.ID;
                                cell.LayoutRow = row;
                                cell.Row = row.Item.LabelCode;
                                cell.LayoutColumn = col;
                                cell.Column = col.Item.LabelCode;
                                cell.Concept = row.Item.Concept != null ? row.Item.Concept : col.Item.Concept;
                                var isrowkey = row.Item.IsKey;
                                var iscolkey = col.Item.IsKey;
                                
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
                                    if (isrowkey) 
                                    {
                                        cell.FactString = row.Item.FactString;

                                    }
                                    if (iscolkey) 
                                    {
                                        cell.FactString = col.Item.FactString;

                                    }
                                }
                                TableHelpers.SetDimensions(cell);
                                cell.Dimensions = cell.Dimensions.Where(i => !i.IsDefaultMember).ToList();
                                LayoutCellDictionary.Add(layoutid, cell);
                            }
                            else 
                            {
                                cell = this.LayoutCellDictionary[layoutid];
                            }
                            var xcell = new Cell();
                            xcell.Report = cell.Report;
                            xcell.Row = cell.Row;
                            xcell.Column = cell.Column;
                            xcell.Extension = ext.LabelCode;
                            xcell.Concept = cell.Concept == null ? ext.Concept : cell.Concept;
                            xcell.Dimensions.AddRange(cell.Dimensions);
                            xcell.Dimensions.AddRange(ext.Dimensions);
                            xcell.Dimensions = xcell.Dimensions.OrderBy(i => i.MapID).ToList();
                            var cellix = Taxonomy.CellIndexDictionary.Count;
                            Taxonomy.CellIndexDictionary.Add(Taxonomy.CellIndexDictionary.Count, xcell.CellID);
                            var columndimensions = cell.LayoutColumn.Item.Dimensions;
        


                            cell.IsBlocked = cell.IsKey ? false : true;
                            if (!cell.IsKey)
                            {

                                var factintkey = xcell.FactIntKey;

                                var factix = this.Taxonomy.FactsManager.GetFactIndex(factintkey);
                                if (factix > -1)
                                {
                                    var factixinterval = new IntervalList(factix);
                                    var factixinthis = Utilities.Objects.IntersectSorted(factixinterval, FactindexList, null);
                                    if (factixinthis.Count == 1)
                                    {
                                        this.FactIndexToCells.Add(new Tintint(factix, cellix));
                                        //this.ProcessedFactindexList.Add(factix);
                                        cell.IsBlocked = false;
                                    }
                                    else
                                    {
                                        factix = -1;
                                    }
                                }
                                if (factix==-1)
                                {
                                    if (!factintkey.Any(fk => fk < 0))
                                    {

                                        coldata = colintervaldict[colix];
                                        IList<int> results = new List<int>();

                                        results = Utilities.Objects.IntersectSorted(extvsrow, coldata, null);
                                        //results = results.Where(i => this.Taxonomy.FactsManager.FactsOfPages.FactKeyCountOfIndexes[i] == factintkey.Length).ToList();

                                        //var old_results = this.Taxonomy.SearchFactsGetIndex4(factintkey, this.FactsOfParts, null, true, false);


                                        //if (old_results.Count != results.Count)
                                        //{

                                        //}
                                        //if (old_results.Count > results.Count)
                                        //{
                                        //    var difference = Utilities.Objects.SortedExcept(old_results, results);
                                        //    var str = this.Taxonomy.GetFactStringKeys(difference);
                                        //}
                                       
                                        cell.IsBlocked = results.Count == 0;

                                        foreach (var factindex in results)
                                        {
                                            this.FactIndexToCells.Add(new Tintint(factindex, cellix));
                                            //this.ProcessedFactindexList.Add(factindex);
                                        }
                                 

                                    }
                                    else
                                    {
                                        //sbe.AppendLine(xcell.CellID + " not mapped for " + Taxonomy.GetFactStringKey(factintkey));
                                    }
                                }

                            
                            }
                            if (cell.IsBlocked && !blocked.ContainsKey(cell.ToString()))
                            {
                                blocked.Add(cell.ToString(), true);
                            }
                            colix++;
                        }
                        rowix++;
                    }
                    extix++;
                }
                //var jscontent = "var FactMap = " + jsoncontent.Replace("\r\n", "\\ \r\n") + ";";
                var jscontent = "{\r\n";//{\"FactMap\": " + jsoncontent + ", \r\n";
                jscontent += "\"ExtensionsRoot\": " + Utilities.Converters.ToJson(Extensions) + ", \r\n";
                jscontent += "\"HtmlTemplatePath\": \"" + this.HtmlPath + "\"}";
                System.IO.File.WriteAllText(FactMapPath, jscontent);


                

            }
            //TableHelpers.ExpandExtensions(this.Extensions, this);
            FactIndexToCells.TrimExcess();
            var fics = FactIndexToCells.OrderBy(i => i.v1);
            //FactIndexToCells = FactIndexToCells.OrderBy(i => i.v1).ToList();
            var factsmapped=0;
            //Utilities.Logger.WriteLine("Saving Cells of Facts");
            foreach (var tuple in fics)
            {
                this.Taxonomy.AddCellToFact(tuple.v1, tuple.v2, null);
                factsmapped++;
            }
            //Utilities.Logger.WriteLine("Saving Cells of Facts finished");

            if (factsmapped != FactindexList.Count)
            {
                var unmapped = FactindexList.Except(FactIndexToCells.Select(i => i.v1)).ToList();
                var unmappedkeys = Taxonomy.GetFactStringKeys(unmapped);
                var x = Taxonomy.FactsManager.GetFactKey(unmapped.FirstOrDefault()); 
            }
            FactIndexToCells.Clear();
            var fsb = new StringBuilder();
            var firstunmappedfact = "";
            var unmappednr = 0;
            if (factsmapped != FactindexList.Count)
            {
                foreach (var factindex in FactindexList.AsEnumerable())
                {
                    //;
                    if (this.Taxonomy.GetCellsOfFact(factindex).Count == 0)
                    {
                        //var wasprocessed = ProcessedFactindexList.Contains(factindex);
                        var page = this.Taxonomy.FactsManager.FactsOfPages.GetPageByFactIndex(factindex);
                        var fact = this.Taxonomy.FactsManager.GetFactKey(factindex);
                        // var item = Taxonomy.SearchFacts3(this.FactsOfParts, fact,);
                        this.Taxonomy.AddCellToFact(factindex, -1, null);
                        unmappednr++;
                        var identifier = String.Format("Fact {0} not mapped for {1}", this.Taxonomy.GetFactStringKey(fact), this.ID);
                        if (String.IsNullOrEmpty(firstunmappedfact))
                        {
                            firstunmappedfact = identifier.Replace(",", ",\n");
                        }
                        fsb.AppendLine(identifier);
                    }
                }
            }
            sbe.Clear();
            sbe.AppendLine("ID: " + this.ID);
            sbe.AppendLine("Name: " + this.Name);
            sbe.AppendLine("Extenstions");
            foreach (var item in Extensions.Children) 
            {
                sbe.AppendLine("    " + item.Item.ToCompareString());
            }
            sbe.AppendLine("Rows");
            foreach (var item in Rows)
            {
                sbe.AppendLine("    " + item.Item.ToCompareString());
            }
            sbe.AppendLine("Columns");
            foreach (var item in Columns)
            {
                sbe.AppendLine("    " + item.Item.ToCompareString());
            }
            sbe.AppendLine("Blocked");
            foreach (var item in blocked)
            {
                sbe.AppendLine("    " + item.Key.ToString());
            }
            sbe.AppendLine("Filing Indicator: "+this.FilingIndicator);

            Utilities.FS.WriteAllText(MappingTextPath, sbe.ToString());
            if (!String.IsNullOrEmpty(firstunmappedfact)) 
            {
                sbe.AppendLine(firstunmappedfact);
                Utilities.Logger.WriteLine(fsb.ToString());


            }
            FactList.Clear();
            //FactindexList.Clear();
            //ProcessedFactindexList.Clear();

            System.IO.File.WriteAllText(LayoutPath.Replace(".txt", ".sbe.dat"), sbe.ToString());
            sbe.Clear();

            FactsOfParts.MoveTo(this.Taxonomy.FactsOfParts);
            FactsOfParts.Clear();

      
            CreateHtmlLayout();
            LayoutCellDictionary.Clear();
            this.LayoutRoot = lr;
            if (factsmapped > 100000)
            {
                GC.Collect();
            }
            //if (this.Taxonomy.Tables.Where(i => i.LayoutCellDictionary.Count > 0).Count() > 50)
            //{
            //    Utilities.Logger.WriteLine("Waiting");

            //    System.Threading.Thread.Sleep(10 * 1000);
            //}
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
                columncoderow = columncoderow + String.Format("<th {0} {1} factid=\"{3}\">{2}</th>\r\n", colclass, "", column.Item.LabelCode, column.Item.FactString);

            }
            sb.AppendLine("<tr>");
            sb.AppendLine(columncoderow);
            sb.AppendLine("</tr>");

            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            //new version
            var maxlevel = Y_Axis.Count;
            var rowix = 0;
            foreach (var row in Rows) 
            {
                var colix = 0;
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
                row.Item.LabelContent, row.Item.FactString, row.Item.LabelID + "  " + row.Item.FactString));
                
                //adding the code cell
                sb.AppendLine(String.Format("<th class=\"code left\" factid=\"{1}\">{0}</th>", row.Item.LabelCode, row.Item.FactString));

                //adding the data cells
                foreach (var column in Columns) 
                {
                    var tempcell = new Cell();
                    tempcell.Row = row.Item.LabelCode;
                    tempcell.Column = column.Item.LabelCode;
                    Cell cell = null;
                    var layoutid = String.Format("{0}|{1}", rowix, colix);
                    cell = LayoutCellDictionary[layoutid];
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
                    //sb.AppendLine(String.Format(
                    //    "<td id=\"{0}\" {3} factstring=\"{2}\" class=\"{1}\" title=\"{0}\"></td>",
                    //    alt, cssclass, cell.FactKey, rl
                    //    ));
                    sb.AppendLine(String.Format(
                   "<td id=\"{0}\" {2} class=\"{1}\" title=\"{0}\"></td>",
                   alt, cssclass, rl
                   ));
                    colix++;
                }

                sb.AppendLine("</tr>");
                rowix++;
            }
            

            sb.AppendLine("</tbody>");

            sb.AppendLine("</table>");
            return sb.ToString();

        }





        #region Cubes

        public IEnumerable<IEnumerable<QualifiedName>> GetCubeSlices(HyperCube cube)
        {
            var cubelist = new List<List<QualifiedName>>();

            cubelist.Add(cube.Concepts.Cast<QualifiedName>().ToList());
            var domains = cube.DimensionItems.OrderBy(i => i.FullName, StringComparer.Ordinal).Select(i => i.Domains.FirstOrDefault()).OrderBy(i => i.FullName, StringComparer.Ordinal).ToList();
            foreach (var domain in domains)
            {
                cubelist.Add(domain.DomainMembers.Cast<QualifiedName>().ToList());
            }
            var combinations = Utilities.MathX.CartesianProduct(cubelist);

            return combinations;

        }
        public IEnumerable<IEnumerable<int>> GetCubeSlices2(HyperCube cube, Dictionary<int,IntervalList> domainids)
        {
            var cubelist = new List<List<int>>();

            cubelist.Add(cube.Concepts.Select(i=>Taxonomy.FactParts[i.FullName]).ToList());
            //var domains = cube.DimensionItems.OrderBy(i => i.FullName, StringComparer.Ordinal).Select(i => i.Domains.FirstOrDefault()).OrderBy(i => i.FullName, StringComparer.Ordinal).ToList();
            //var domains = cube.DimensionItems.Select(i => i.Domains.FirstOrDefault()).OrderBy(i => i.DimensionItem.Content + i.Content).ToList();
            var domains = cube.DimensionItems.Select(i => i.Domains.FirstOrDefault()).OrderBy(i => i.DomainMembers.Count).ToList();
    
            foreach (var domain in domains)
            {
                //.Where(i=>!i.IsDefaultMember)
                cubelist.Add(domain.DomainMembers.OrderBy(i=>i.Content).Select(i =>
                {
                    if (i.IsDefaultMember) 
                    {
                        return -2;
                    }
                    var dimitem = String.Format("[{0}]{1}", i.Domain.DimensionItem.FullName, i.FullName);
                    return Taxonomy.FactParts[dimitem];
                }).ToList());
            }
            for (int i = 0; i < cubelist.Count; i++)
            {
                var firstitem = cubelist[i].FirstOrDefault(k => k > -1);
                var domainid = Taxonomy.GetDimensionDomainPart(firstitem);
                if (domainid > -1) 
                {
                    domainids.Add(domainid, new IntervalList());
                }
            }
      
            var combinations = Utilities.MathX.CartesianProduct(cubelist,-2);

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
