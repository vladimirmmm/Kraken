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
    public class Table : Identifiable
    {
        private int datacellminwidth = 100;
        private int cellpadding = 15;
        private int titlecellminwidth = 300; 
        private string _HtmlPath = "";
        public string HtmlPath 
        {
            get { return _HtmlPath; }
            set { _HtmlPath=value; }
        }
        public string FactMapPath
        {
            get { return _HtmlPath.Replace(".html", "-factmap.js"); }
        }
        public string FactPath 
        {
            get { return _HtmlPath.Replace(".html", "-facts.txt"); }
        }
        public string CubePath
        {
            get { return _HtmlPath.Replace(".html", "-cubes.txt"); }
        }
        public string DefPath
        {
            get { return _HtmlPath.Replace(".html", "-def.txt"); }
        }
        public string LayoutPath
        {
            get { return _HtmlPath.Replace(".html", "-layout.txt"); }
        }
        private string _FolderName = "";
        public string FolderName
        {
            get { return _FolderName; }
            set { _FolderName = value; }
        }

        public Taxonomy Taxonomy = null;
        public static Func<String, Label> LabelAccessor = null;
        private List<Dimension> _Dimensions = new List<Dimension>();
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }

        //private List<Fact> _Fact = new List<Fact>();
        //public List<Fact> Fact { get { return _Fact; } set { _Fact = value; } }

       
        public Hierarchy<LayoutItem> LayoutRoot { get; set; }
        [JsonIgnore]
        public List<Hierarchy<LayoutItem>> LayoutItems = new List<Hierarchy<LayoutItem>>();

        private List<HyperCube> _HyperCubes = new List<HyperCube>();
        public List<HyperCube> HyperCubes { get { return _HyperCubes; } set { _HyperCubes = value; } }


        public string FilingIndicator;

        private List<KeyValue<int, List<Hierarchy<LayoutItem>>>> X_Axis = new List<KeyValue<int, List<Hierarchy<LayoutItem>>>>();
        private List<KeyValue<int, List<Hierarchy<LayoutItem>>>> Y_Axis = new List<KeyValue<int, List<Hierarchy<LayoutItem>>>>();
        private List<KeyValue<int, List<Hierarchy<LayoutItem>>>> Z_Axis = new List<KeyValue<int, List<Hierarchy<LayoutItem>>>>();

        private List<Hierarchy<LayoutItem>> Rows = new List<Hierarchy<LayoutItem>>();
        private List<Hierarchy<LayoutItem>> Columns = new List<Hierarchy<LayoutItem>>();
        private List<LayoutItem> _Extensions = new List<LayoutItem>();
        public List<LayoutItem> Extensions 
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

        public override string ToString()
        {
            return base.ToString();
        }

        public void BuildLevels(List<KeyValue<int, List<Hierarchy<LayoutItem>>>> AxisLevels, Hierarchy<LayoutItem> item, int level = 0)
        {
            var lvlitem = AxisLevels.FirstOrDefault(i => i.Key == level);
            if (lvlitem == null)
            {
                lvlitem = new KeyValue<int, List<Hierarchy<LayoutItem>>>(level, new List<Hierarchy<LayoutItem>>());
                AxisLevels.Add(lvlitem);
            }
            lvlitem.Value.Add(item);

            foreach (var child in item.Children)
            {
                child.Parent = item;
                BuildLevels(AxisLevels, child, level + 1);

            }
        }   

        public void SetHtmlPath()
        {
            string folder = Taxonomy.TaxonomyLayoutFolder;
            var filepath = folder + this.ID + ".html";
            HtmlPath = filepath;
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
                       
                            dimkeyparts.Add(dimitem);

                        }


                    }

                    dimkeyparts = dimkeyparts.OrderBy(i => i).ToList();

                    foreach (var dimkeypart in dimkeyparts)
                    {
                        sb_fact.Append(dimkeypart);
                        key += dimkeypart;

                    }

                    if (!this.Taxonomy.Facts.ContainsKey(key)) 
                    {
                        this.Taxonomy.Facts.Add(key, new List<String>());
                    }
                    sb_fact.AppendLine();
                }
            }
            Console.WriteLine(String.Format("Facts: {0}", this.Taxonomy.Facts.Count));
            //var factpath = HtmlPath.Replace(".html", "-facts.txt");
            //var cubepath = HtmlPath.Replace(".html", "-cubes.txt");
            //Utilities.FS.WriteAllText(FactPath, sb_fact.ToString());
            sb_fact.Clear();

        }

        public IEnumerable<IEnumerable<QualifiedName>> GetCubeSlices(HyperCube cube) 
        {
            var cubelist = new List<List<QualifiedName>>();

            cubelist.Add(cube.Concepts.Cast<QualifiedName>().ToList());
            var domains = cube.DimensionItems.OrderBy(i=>i.FullName).Select(i => i.Domains.FirstOrDefault()).OrderBy(i=>i.FullName).ToList();
            foreach (var domain in domains) 
            {
                cubelist.Add(domain.DomainMembers.Cast<QualifiedName>().ToList());
            }
            var combinations = Utilities.MathX.CartesianProduct(cubelist);

            return combinations;

        }

        public void LoadLayout()
        {
            Console.WriteLine(String.Format("Layout for {0}", this.ID));
            X_Axis.Clear();
            Y_Axis.Clear();
            Z_Axis.Clear();

            rowsnode = LayoutRoot.Find(i => String.Equals(i.Item.Axis, "y", StringComparison.InvariantCultureIgnoreCase));
            if (rowsnode.Children.Count == 1)
            {
                rowsnode = rowsnode.Children.FirstOrDefault();
            }
            columnsnode = LayoutRoot.Find(i => String.Equals(i.Item.Axis, "x", StringComparison.InvariantCultureIgnoreCase)); //LayoutRoot.Find(i => i.Item.LabelContent.ToLower() == "columns"); //TODO Axis should be used
            if (columnsnode.Children.Count == 1)
            {
                columnsnode = columnsnode.Children.FirstOrDefault();
            }
            extensionnode = LayoutRoot.Find(i => String.Equals(i.Item.Axis, "z", StringComparison.InvariantCultureIgnoreCase)); //TODO Axis should be used

            BuildLevels(X_Axis, columnsnode);
            BuildLevels(Y_Axis, rowsnode);

            Columns = columnsnode.GetLeafs();
            SetDimensions(Columns);
            //Rows = rowsnode.ToHierarchy().ToList().Where(i => !i.Item.IsAbstract).ToList(); //rowsnode.GetLeafs(); .Where(i => !i.Item.IsAbstract)
            Rows = rowsnode.ToHierarchy().ToList();
            SetDimensions(Rows);


            SetExtensions();

            var factdeflist = new List<String>();
            var blocked = new Dictionary<string, bool>();
            //var sb = new StringBuilder();
            //LayoutCells.Clear();
            if (LayoutCells.Count == 0)
            {
                var exts = Extensions.ToList();
                if (exts.Count == 0)
                {
                    var li = new LayoutItem();
                    li.ID = "";
                    var label = new Label();
                    label.Code = "000";
                    label.Lang = "en";
                    li.Label = label;
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
                                cell.Row = row.Item.LabelCode;
                                cell.Column = col.Item.LabelCode;
                                cell.Concept = row.Item.Concept != null ? row.Item.Concept : col.Item.Concept;
                                cell.Dimensions.AddRange(row.Item.Dimensions);
                                cell.Dimensions.AddRange(col.Item.Dimensions);
                                cell.Dimensions = cell.Dimensions.Distinct().OrderBy(i => i.DomainMemberFullName).ToList();
                                LayoutCells.Add(cell);

                            }
                            //var cubes = GetHyperCubes(row.Item, col.Item, sb);
                            var xcell = new Cell();
                            xcell.Report = cell.Report;
                            xcell.Row = cell.Row;
                            xcell.Column = cell.Column;
                            xcell.Extension = ext.LabelCode;
                            xcell.Concept = cell.Concept;
                            xcell.Dimensions.AddRange(cell.Dimensions);
                            xcell.Dimensions.AddRange(ext.Dimensions);
                            xcell.Dimensions = xcell.Dimensions.Where(i => !i.IsDefaultMemeber).OrderBy(i => i.DomainMemberFullName).ToList();

                            if (this.Taxonomy.Facts.ContainsKey(xcell.FactKey))
                            {


                                var item = this.Taxonomy.Facts[xcell.FactKey];
                                item.Add(xcell.CellID);
                                factextdict.Add(xcell.LayoutID, xcell.FactKey);


                            }
                            else
                            {
                                cell.IsBlocked = true;
                                if (!blocked.ContainsKey(cell.ToString()))
                                {
                                    blocked.Add(cell.ToString(), true);
                                    //sb.AppendLine(String.Format("Cell {0} - {1} is blocked! ", cell, cell.FactString));
                                }
                            }
                        }
                    }
                }
                var jsoncontent = Utilities.Converters.ToJson(factmap);
                //var jscontent = "var FactMap = " + jsoncontent.Replace("\r\n", "\\ \r\n") + ";";
                var jscontent = "var FactMap = " + jsoncontent + ";";
                System.IO.File.WriteAllText(FactMapPath, jscontent);

            }

            CreateHtmlLayout();
        }

        public List<HyperCube> GetHyperCubes(LayoutItem row, LayoutItem column, StringBuilder sb) 
        {
            var result =new List<HyperCube>();
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
                var zdimensionitems = Extensions.SelectMany(i => i.Dimensions).Distinct().ToList();;
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

        private string CubesToString(List<HyperCube> cubes) 
        {
            var sb = new StringBuilder();
            foreach (var cube in cubes) 
            {
                sb.AppendLine(cube.ToFullString());
            }
            return sb.ToString();
        }

        private void SetExtensions() 
        {
            if (extensionnode != null)
            {
                BuildLevels(Z_Axis, extensionnode);
                var leafs = extensionnode.GetLeafs();
                var extensions = new List<LayoutItem>();
                if (leafs.Count == 1)
                {
                    var extension = leafs.FirstOrDefault();
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
                                dim.Domain = dm.Domain.ID;
                                dim.DomainMember = dm.Name;
                                li.Dimensions.Add(dim);
                                li.LabelID = dm.ID;
                                var labelkey = Label.GetKey(dim.Domain, dm.ID);
                                li.Label = this.Taxonomy.FindLabel(labelkey);
                                if (li.Label == null) 
                                {
                                    labelkey = Label.GetKey(dim.Domain.Replace("eba_",""), dm.ID);
                                    li.Label = this.Taxonomy.FindLabel(labelkey);
                                }
                                //li.LoadLabel(this.Taxonomy);
                                extensions.Add(li);
                            }
                        }
                    }
                }
                else
                {
                    SetDimensions(leafs);
                    foreach (var leaf in leafs)
                    {
                        var li = leaf.Item;
                        var dm = new DimensionMember();
                        //dm.Content = li.Dimensions.FirstOrDefault().Value;
                        extensions.Add(li);
                    }
                }
                this.Extensions = extensions;
            }
        }

        private void SetDimensions(List<Hierarchy<LayoutItem>> items) 
        {
            foreach (var item in items) 
            {
                var current=item.Parent;
                while (current != null) 
                {
                    MergeDimensions(item.Item.Dimensions, current.Item.Dimensions);

                    if (current.Item.Concept==null || current.Item.Concept.Content == item.Item.Concept.Content)
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
        
        public void EnsureHtmlLayout()
        {
            Taxonomy.ManageUIFiles();

            if (!System.IO.File.Exists(HtmlPath))
            {
                CreateHtmlLayout();
            }
        }
        
        public void CreateHtmlLayout(bool regenerate = false)
        {
            if (regenerate || !System.IO.File.Exists(HtmlPath))
            {
                var folder = Utilities.Strings.GetFolder(HtmlPath);
                var htmlbuilder = new StringBuilder();
                var template = System.IO.File.ReadAllText("TableTemplate.html");
                var html = template;
                html = html.Replace("#table#", GetTableHtml());
                html = html.Replace("#style#", GetStyle());
                html = html.Replace("#jsonpath#", Utilities.Strings.GetFileName( this.FactMapPath));
                htmlbuilder.AppendLine(html);



                Utilities.FS.WriteAllText(HtmlPath, htmlbuilder.ToString());
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
            
            var sb = new StringBuilder();
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            var xlevelcount = X_Axis.Count;
            var ylevelcount = rowsnode.GetSubLevelCount(null) + 1;
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
                    var tc = item.GetLeafCount(null);
                    var sublevelcount = item.GetSubLevelCount(null);
                    var colspan = tc > 0 ? String.Format(" colspan=\"{0}\"", tc) : "";
                    var levelx = xlevelcount - lvl.Key;
                    var rowspan = levelx > 0 ? String.Format(" rowspan=\"{0}\"", levelx) : "";
                    if (sublevelcount != 0)
                    {
                        rowspan = "";
                    }
                    if (item.Children.Count == 0)
                    {
                        columncoderow = columncoderow + String.Format("<th {0} {1}>{2}</th>\r\n", colspan, rowspan, item.Item.LabelCode);
                    }
                    sb.AppendLine(String.Format("<th {0} {1} factid=\"{3}\" title=\"{4}\">{2}</th>",
                        colspan, rowspan, item.Item.LabelContent, item.Item.FactString, item.Item.FactString));
                }
                sb.AppendLine("</tr>");

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
                var level = row.GetLevel()-3; //-1 for the LayoutRoot and -2 for the Rows roots node
                var sublevelcount = level > -1 ? maxlevel - level : maxlevel;
                sb.AppendLine("<tr>");
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
                sb.AppendLine(String.Format("<th class=\"left\">{0}</th>", row.Item.LabelCode));

                //adding the data cells
                foreach (var column in Columns) 
                {
                    var cell = LayoutCells.FirstOrDefault(i => i.Row == row.Item.LabelCode && i.Column == column.Item.LabelCode);
                    var alt = String.Format("R{0}|C{1}", row.Item.LabelCode, column.Item.LabelCode);
                    var cssclass = "data";
                    if (cell.IsBlocked) 
                    {
                        cssclass+=" blocked";
                    }
                    sb.AppendLine(String.Format("<td layoutid=\"{0}\" factstring=\"{2}\" class=\"{1}\" title=\"{0}\"></td>", alt, cssclass, cell.FactKey));
                }

                sb.AppendLine("</tr>");

            }
            

            sb.AppendLine("</tbody>");

            sb.AppendLine("</table>");
            return sb.ToString();

        }
    }


}
