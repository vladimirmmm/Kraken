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
    public class TableInfo : Identifiable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public List<string> Tables = new List<string>();

        public TableInfo() 
        {

        }

        public void SetFrom(Table table) 
        {
            this.ID = table.ID;
            this.Name = table.Name;     
        }


    }

    public class Table : Identifiable,ILabeled
    {
        private int datacellminwidth = 100;
        private int cellpadding = 15;
        private int titlecellminwidth = 300; 
        private string _HtmlPath = "";
        public static string DefaultExtensionCode = "000";
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

        private List<Hierarchy<LayoutItem>> Rows = new List<Hierarchy<LayoutItem>>();
        private List<Hierarchy<LayoutItem>> Columns = new List<Hierarchy<LayoutItem>>();
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

        private List<String> _FactList = new List<string>();
        public List<String> FactList 
        {
            get { return _FactList;  }
            set { _FactList = value; }
        }

        private List<Cell> _LayoutCells = new List<Cell>();
        public List<Cell> LayoutCells
        {
            get { return _LayoutCells; }
            set { _LayoutCells = value; }
        }

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

            foreach (var child in item.Children)
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
            var lc = item.GetLeafCount(null);
            var level = item.GetLevel(root);
            var totallevelcount = root.GetSubLevelCount();
            var sublevelcount = totallevelcount - level + 1;

            var colspan = lc > 1 ? String.Format("colspan=\"{0}\"", lc) : "";
            item.Item.ColSpan = colspan;

            if (item.Children.Count == 0)
            {
                var rowspan = sublevelcount > 0 ? String.Format("rowspan=\"{0}\"", sublevelcount) : "";
                item.Item.RowSpan = rowspan;
            }
            foreach (var child in item.Children) 
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
            int totallevelcount = root.GetSubLevelCount();
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


            if (!childfirst && !item.Item.IsAbstract && item.Children.Count>  0)
            {
                if (String.IsNullOrEmpty(item.Item.Axis))
                {
                    AddPlaceHolderIfNotExists(item, placeholder, 0);
                }
            }
            foreach (var child in item.Children)
            {
                FixLayoutItem(child, root, childfirst);
            }
            if (childfirst && !item.Item.IsAbstract && item.Children.Count > 0)
            {
                AddPlaceHolderIfNotExists(item, placeholder, -1);

            }
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
            var slices = new List<IEnumerable<QualifiedName>>();
            var sb_fact = new StringBuilder();
            if (this.ID.Contains("29")) 
            {

            }
            foreach(var hypercube in HyperCubes){
                var cubeslices = GetCubeSlices(hypercube);
                foreach (var slice in cubeslices) {
                    var childslices = slice.ToList();
                    var key = "";
                    var conceptslicechild = childslices[0];
                    if (conceptslicechild is Concept == false) 
                    {
                        throw new Exception("First item of the hypercube slice is not a Concept!"); 
                    }
                    childslices.Remove(conceptslicechild);
                    //childslices=childslices.OrderBy(i=>(i as DimensionMember).Domain)
                    var conceptpart = string.Format("{0},",conceptslicechild.FullName);
                    sb_fact.Append(conceptpart);
                    key += conceptpart;
                    var dimkeyparts =new List<string>();
                    for (int i = 0; i < childslices.Count; i++)
                    {
                        var slicechild = childslices[i];

                        var dimmember = slicechild as DimensionMember;
                        if (!dimmember.IsDefaultMember)
                        {
                            var dimitem = String.Format("[{0}]{1},", dimmember.Domain.DimensionItem.FullName, dimmember.FullName);

                            if (dimmember is TypedDimensionMember)
                            {
                               // dimitem = String.Format("[{0}]{1},", dimmember.Domain.DimensionItem.FullName, dimmember.Domain.ID);
                                dimitem = String.Format("[{0}]{1},", dimmember.Domain.DimensionItem.FullName, dimmember.FullName);

                            }
                            else
                            {
                            }
                            dimkeyparts.Add(dimitem);

                        }


                    }

                    dimkeyparts = dimkeyparts.OrderBy(i => i).ToList();

                    foreach (var dimkeypart in dimkeyparts)
                    {
                        sb_fact.Append(dimkeypart);
                        key += dimkeypart;

                    }
                    var tempfact = new FactBase();
                    tempfact.SetFromString(key);
                    key = tempfact.GetFactKey();
                    if (!this.Taxonomy.Facts.ContainsKey(key)) 
                    {
                        this.Taxonomy.Facts.Add(key, new List<String>());
                        if (key.Contains("eba_typ"))
                        {
                        }
                    }
                    sb_fact.AppendLine();
                }
            }
            //Logger.WriteLine(String.Format("Facts: {0}", this.Taxonomy.Facts.Count));
            //var factpath = HtmlPath.Replace(".html", "-facts.txt");
            //var cubepath = HtmlPath.Replace(".html", "-cubes.txt");
            //Utilities.FS.WriteAllText(FactPath, sb_fact.ToString());
            sb_fact.Clear();

        }
       
        private void SetExtensions()
        {
            if (this.Name.Contains("RDP-R13")) 
            {

            }
            if (extensionnode != null)
            {
                var leafs = extensionnode.GetLeafs().Where(i => i.Parent != null).ToList();

                this.Extensions = TableHelpers.CombineExtensionNodes(leafs, this);
            }
            /*
            if (extensionnode != null)
            {
                BuildLevels(Z_Axis, extensionnode);
                var leafs = extensionnode.GetLeafs().Where(i => i.Parent != null).ToList();
                var abstractleafs = leafs.Where(i => i.Parent.Item.IsAbstract).ToList();

                var nonabstractleafs = leafs.Where(i => !i.Parent.Item.IsAbstract).ToList();
             


                var tempfact = new FactBase();
                foreach (var abstractleaf in abstractleafs) 
                {
                    MergeDimensions(tempfact.Dimensions, abstractleaf.Item.Dimensions);
                    tempfact.Concept = tempfact.Concept == null ? abstractleaf.Item.Concept : tempfact.Concept;
                }
                //foreach (var nonabstractleaf in nonabstractleafs) 
                //{
                //    MergeDimensions(nonabstractleaf.Item.Dimensions, tempfact.Dimensions);
                //    nonabstractleaf.Item.Concept = tempfact.Concept == null ? tempfact.Concept : nonabstractleaf.Item.Concept;
             
                //}

                var extensions = new List<LayoutItem>();
                //if (extensionnode.Children.Count == 1)
                //if (leafs.Count == 1)
                if (nonabstractleafs.Count==1)
                {
                    var nonabstractleaf = nonabstractleafs.FirstOrDefault();
                    //var extension = leafs.FirstOrDefault();
                    //var extension = extensionnode.Children.FirstOrDefault();
                    var extension = nonabstractleaf.Parent;
                    if (extension.Item.IsAspect)
                    {
                        var dimension = extension.Item.Dimensions.FirstOrDefault();
                        var hypercubes = HyperCubes.Where(i => i.DimensionItems.Any(j => j.FullName == dimension.DimensionItemFullName)).ToList();
                        var domains = hypercubes.SelectMany(i => i.DimensionItems.Where(j => j.FullName == dimension.DimensionItemFullName)).SelectMany(k => k.Domains).ToList();
                        var distinctdomains = domains.Distinct().ToList();
                        if (distinctdomains.Count == 1)
                        {
                            foreach (var dm in distinctdomains.FirstOrDefault().DomainMembers)
                            {
                                var li = new LayoutItem();
                                li.Table = this;
                                var dim = new Dimension();
                                dim.DimensionItem = dimension.DimensionItemFullName;
                                if (Taxonomy.IsTyped(dm.Domain.Namespace))
                                {
                                    dim.Domain = dm.Domain.ToString();
                                    dim.DomainMember = "";
                                    li.Dimensions.Add(dim);
                                    li.LabelID = dm.ID;
                                    li.LabelCode = dim.Domain;
                                }
                                else
                                {
                                    dim.Domain = dm.Domain.ID;
                                    dim.DomainMember = dm.Name;
                                    li.Dimensions.Add(dim);
                                    li.LabelID = dm.ID;
                                }
                                //var x = Taxonomy.Prefix
                                //var domainfolder = dim.Domain.StartsWith(Taxonomy.Prefix) ? dim.Domain.Substring(Taxonomy.Prefix.Length) : dim.Domain;
                                var domainfolder = dim.Domain.IndexOf("_") > -1 ? dim.Domain.Substring(dim.Domain.LastIndexOf("_") + 1) : dim.Domain;
                                var labelkey = Label.GetKey(domainfolder, dm.ID);
                                li.Label = this.Taxonomy.FindLabel(labelkey);
                                if (String.IsNullOrEmpty(li.LabelCode))
                                {
                                    li.LabelCode = li.LabelID;
                                }
                                MergeDimensions(li.Dimensions, tempfact.Dimensions);
                                li.Concept = li.Concept == null ? tempfact.Concept : li.Concept;
             
                                extensions.Add(li);
                            }
                        }
                    }
                    else 
                    {
                        var li = GetDefaultExtension();
                        var firstleaf = leafs.FirstOrDefault();
                        if (firstleaf != null)
                        {
                            var extli = firstleaf.Item;
                            li.Dimensions.AddRange(extli.Dimensions);
                            li.Concept = extli.Concept;
                            extensions.Add(li);
                        }
                    }
                }
                else
                {
                    SetDimensions(leafs);
                    foreach (var leaf in leafs)
                    {
                        var li = leaf.Item;
                        if (String.IsNullOrEmpty(li.LabelCode)) 
                        {

                            li.LabelCode = li.LabelID;
                        }

                        extensions.Add(li);
                    }
                }
                this.Extensions = extensions;
            }
            */
        }

        private Hierarchy<LayoutItem> GetAxisNode(string axis) 
        {
            var nodes = LayoutRoot.Where(i => String.Equals(i.Item.Axis, axis, StringComparison.InvariantCultureIgnoreCase)).OrderBy(i => i.Order).ToList();
            var axislayoutitem = new LayoutItem();
            axislayoutitem.IsVisible = false;
            axislayoutitem.ID = String.Format("Axis {0}", axis);
            axislayoutitem.IsAbstract = true;
            var axisnode = new Hierarchy<LayoutItem>(axislayoutitem);
            foreach (var node in nodes) 
            {
                axisnode.Children.Add(node);
                node.Item.IsVisible = false;
            }
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

        private LayoutItem GetDefaultExtension() 
        {
            var li = new LayoutItem();
            li.ID = "";
            var label = new Label();
            label.Code = DefaultExtensionCode;
            label.Lang = "en";
            label.Content = "Default";
            li.Label = label;
            return li;
        }

        public void LoadLayout()
        {
            Logger.WriteLine(String.Format("Layout for {0}", this.ID));
            X_Axis.Clear();
            Y_Axis.Clear();
            Z_Axis.Clear();
  
            rowsnode = GetAxisNode("y");
            columnsnode = GetAxisNode("x");
            extensionnode = GetAxisNode("z");
            //extensionnode = LayoutRoot.Find(i => String.Equals(i.Item.Axis, "z", StringComparison.InvariantCultureIgnoreCase)); //TODO Axis should be used

            var aspects = rowsnode.Where(i => i.Item.IsAspect);

    
            var ix=0;
            var columnAxisnode = columnsnode.FirstOrDefault(i => !String.IsNullOrEmpty(i.Item.Axis));
            var aspect_row_dimension = new List<Dimension>();
            foreach (var aspect in aspects)
            {
                aspect.Item.LabelID = aspect.Parent.Item.LabelID;
                aspect.Item.Label = aspect.Parent.Item.Label;
                var rolenode = aspect.FirstOrDefault(i => !String.IsNullOrEmpty(i.Item.Role));
                if (rolenode != null)
                {
                    aspect.Item.Role = rolenode.Item.Role;

                }
                columnAxisnode.Children.Insert(ix, aspect);
                rowsnode.Remove(aspect.Parent);
                ix++;
                aspect_row_dimension.AddRange(aspect.Item.Dimensions);
                //aspect.Item.Dimensions.Clear();
            }
            if (rowsnode.Children.Count == 0) 
            {
                var dynrow_li = new LayoutItem();
                dynrow_li.IsDynamic = true;
                dynrow_li.ID = "dynrow";
                dynrow_li.Dimensions = aspect_row_dimension;
                var dyn_h = new Hierarchy<LayoutItem>(dynrow_li);
                rowsnode = dyn_h;

            }

            if (this.ID.Contains("08")) 
            {

            }
            FixLayoutItem(columnsnode,null);
            SetSpans(columnsnode,null);

            BuildLevels(X_Axis, columnsnode);
            BuildLevels(Y_Axis, rowsnode);

            Columns = columnsnode.GetLeafs();
    
            Rows = rowsnode.ToHierarchy().Where(i => i.Item.IsVisible).ToList();
            //Rows = Rows.Where(i => !String.IsNullOrEmpty(i.Item.LabelCode) || i.Item.IsDynamic).ToList();
            Rows = Rows.Where(i => i.Item.Label!=null || i.Item.IsDynamic).ToList();

            FixLabelCodes(Columns, "{0:D4}");
            FixLabelCodes(Rows, "{0:D4}");

            //FixNamespaces(Columns);
            //FixNamespaces(Rows);

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
  
            if (LayoutCells.Count == 0)
            {
                var exts = Extensions.Where(i=>!i.Item.IsPlaceholder).Select(i=>i.Item).ToList();
                if (exts.Count == 0)
                {
                    var li = GetDefaultExtension();
                    exts.Add(li);
                }
                var factmap = new Dictionary<string, Dictionary<string,string>>();
                foreach (var ext in exts)
                {
                    var factextdict = new Dictionary<string, string>();
                    factmap.Add(ext.LabelCode, factextdict);

                    foreach (var row in Rows)
                    {
                        foreach (var col in Columns)
                        {
                            var cell = this.LayoutCells.FirstOrDefault(i => i.Row == row.Item.LabelCode && i.Column == col.Item.LabelCode);
                            if (cell == null)
                            {
                                cell = new Cell();
                                cell.Report = this.ID;
                                cell.LayoutRow = row;
                                cell.Row = row.Item.LabelCode;
                                cell.LayoutColumn = col;
                                cell.Column = col.Item.LabelCode;
                                cell.Concept = row.Item.Concept != null ? row.Item.Concept : col.Item.Concept;
                                cell.Dimensions.AddRange(row.Item.Dimensions);
                                cell.Dimensions.AddRange(col.Item.Dimensions);
                                SetDimensions(cell);

                                LayoutCells.Add(cell);

                            }
                            var xcell = new Cell();
                            xcell.Report = cell.Report;
                            xcell.Row = cell.Row;
                            xcell.Column = cell.Column;
                            xcell.Extension = ext.LabelCode;
                            xcell.Concept = cell.Concept;
                            xcell.Dimensions.AddRange(cell.Dimensions);
                            xcell.Dimensions.AddRange(ext.Dimensions);
                            xcell.Dimensions = xcell.Dimensions.Where(i => !i.IsDefaultMemeber).OrderBy(i => i.DomainMemberFullName).ToList();
          
                            var columndimensions = cell.LayoutColumn.Item.Dimensions;
                            var factkeys = new List<String>();
                            
                            var dimensionswithoutmember = xcell.Dimensions.Where(i => String.IsNullOrEmpty(i.DomainMember) && !i.IsTyped).ToList();
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
                                factkeys.Add(xcell.FactKey);
                            }
                           
                           
                            foreach (var factkey in factkeys)
                            {
                                if (this.Taxonomy.Facts.ContainsKey(factkey))
                                {


                                    var item = this.Taxonomy.Facts[factkey];
                                    item.Add(xcell.CellID);
                                    if (!factextdict.ContainsKey(xcell.LayoutID))
                                    {
                                        factextdict.Add(xcell.LayoutID, factkey);// xcell.FactKey);
                                    }
                                    else
                                    {

                                    }


                                }
                                else
                                {
                                    if (cell.LayoutColumn.Item.IsAspect)
                                    {
                                        cell.Dimensions = cell.LayoutColumn.Item.Dimensions;
                                    }
                                    else
                                    {
                                        cell.IsBlocked = true;
                                        if (!blocked.ContainsKey(cell.ToString()))
                                        {
                                            blocked.Add(cell.ToString(), true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                var jsoncontent = Utilities.Converters.ToJson(factmap);
                //var jscontent = "var FactMap = " + jsoncontent.Replace("\r\n", "\\ \r\n") + ";";
                var jscontent = "var FactMap = " + jsoncontent + ";\r\n";
                jscontent += "var Extensions = " + Utilities.Converters.ToJson(Extensions) + ";";
                System.IO.File.WriteAllText(FactMapPath, jscontent);
                System.IO.File.WriteAllText(this.FullHtmlPath.Replace(".html", "_layout.txt"), LayoutRoot.ToHierarchyString(i => i.ID + " code: " + i.LabelCode + " >>> " + i.FactString));

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
                var taxscriptincludes = "";
                //foreach (var taxfile in Taxonomy.TaxFiles)
                //{
                //    taxscriptincludes += String.Format("<script src=\"{0}\" type=\"text/javascript\" ></script>\r\n", taxfile);
                //}

                var folder = Utilities.Strings.GetFolder(FullHtmlPath);
                var htmlbuilder = new StringBuilder();
                var template = System.IO.File.ReadAllText("TableTemplate.html");
                var html = template;
                html = html.Replace("#table#", GetTableHtml());
                html = html.Replace("#style#", GetStyle());
                html = html.Replace("#jsonpath#", Utilities.Strings.GetFileName( this.FactMapPath));
                html = html.Replace("#TaxScripts#", taxscriptincludes);
                html = html.Replace("#extensions#", Utilities.Converters.ToJson(this.Extensions));
                
                htmlbuilder.AppendLine(html);



                Utilities.FS.WriteAllText(FullHtmlPath, htmlbuilder.ToString());
            }
        }

        public string GetStyle() 
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("td.data{{ min-width:{0}px; }}", datacellminwidth));
            sb.AppendLine(String.Format("th.title{{ min-width:{0}px; }}", titlecellminwidth));
            sb.AppendLine(String.Format("th.pad{{ min-width:{0}px; }}", cellpadding));
            var tableminwidth=this.Columns.Count * datacellminwidth + Y_Axis.Count * cellpadding + titlecellminwidth;
            sb.AppendLine(String.Format("table {{ min-width:{0}px; }}", tableminwidth));
            return sb.ToString();
        }

        public string GetTableHtml()
        {
            if (this.Name.Contains("RDP-R13"))
            {

            }
            
            var sb = new StringBuilder();
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            var xlevelcount = X_Axis.Count;
            var ylevelcount = rowsnode.GetSubLevelCount() + 1;
            var xleafcount = columnsnode.GetLeafCount(null);
            var max_y = Y_Axis.Max(i => i.Value.Count);
            var max_x = X_Axis.Max(i => i.Value.Count) + xleafcount;

            sb.AppendLine(String.Format("<tr><th class=\"left\" colspan=\"{1}\"><h1>{0}<h1></th></tr>", LayoutRoot.Item.LabelContent, xleafcount + ylevelcount + 1));
            var columncoderow = "";
            foreach (var lvl in X_Axis)
            {
                sb.AppendLine("<tr>");
                if (lvl.Key == 0)
                {
                    sb.AppendLine(String.Format("<th id=\"Extension\" colspan=\"{0}\" rowspan=\"{1}\">{2}</th>", ylevelcount + 1, xlevelcount + 1, rowsnode.Item.LabelContent));

                }
      
    
                foreach (var item in lvl.Value)
                {
                    var cssclass = "class=\"\"";

                    if (item.Children.Count > 0) {
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
                columncoderow = columncoderow + String.Format("<th {0} {1}>{2}</th>\r\n", "", "", column.Item.LabelCode);

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
                if (row.Item.IsAspect || row.Item.Dimensions.FirstOrDefault(i=>i.IsTyped)!=null || row.Item.IsDynamic)
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
                    var cell = LayoutCells.FirstOrDefault(i => i.Row == row.Item.LabelCode && i.Column == column.Item.LabelCode);
                    if (!String.IsNullOrEmpty(cell.FactKey) && String.IsNullOrEmpty(row.Item.LabelCode)) 
                    {
                    }

                    var alt = String.Format("R{0}|C{1}", row.Item.LabelCode, column.Item.LabelCode);
                    var cssclass = "data";
                    if (cell.IsBlocked) 
                    {
                        cssclass+=" blocked";
                    }
                    sb.AppendLine(String.Format("<td id=\"{0}\" factstring=\"{2}\" class=\"{1}\" title=\"{0}\"></td>", alt, cssclass, cell.FactKey));
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
            cell.Dimensions = cell.Dimensions.Where(i => !i.IsDefaultMemeber).OrderBy(i=>i.DomainMemberFullName).ToList();
            cell.Dimensions = cell.Dimensions.Distinct().ToList();
        }
        
        private void SetDimensions(List<Hierarchy<LayoutItem>> items)
        {
            foreach (var item in items)
            {
                var current = item.Parent;
                while (current != null)
                {
                    MergeDimensions(item.Item.Dimensions, current.Item.Dimensions);

                    if (current.Item.Concept == null || current.Item.Concept.Content == item.Item.Concept.Content)
                    {
                    }

                    current = current.Parent;
                }
                item.Item.Dimensions = item.Item.Dimensions.Where(i => !i.IsDefaultMemeber).ToList();
            }
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
