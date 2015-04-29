using BaseModel;
using LogicalModel.Base;
using LogicalModel.Classes;
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

        private List<Fact> _Fact = new List<Fact>();
        public List<Fact> Fact { get { return _Fact; } set { _Fact = value; } }

       
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

        private List<String> _CellList = new List<string>();
        public List<String> CellList
        {
            get { return _CellList; }
            set { _CellList = value; }
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
                slices.AddRange(cubeslices);
                foreach (var slice in cubeslices) {
                    foreach (var slicechild in slice)
                    {
                        if (slicechild is Concept)
                        {
                            sb_fact.AppendFormat("{0},", slicechild.Name);

                        }
                        else 
                        {
                            var dimmember = slicechild as DimensionMember;
                            if (!dimmember.IsDefaultMember)
                            {
                                sb_fact.AppendFormat("[{0}]{1},", dimmember.Domain.DimensionItem.Content, dimmember.Content);
                            }

                        }
                        
                    }
                    sb_fact.AppendLine();
                }
            }

            var factpath = HtmlPath.Replace(".html", "-facts.txt");
            var cubepath = HtmlPath.Replace(".html", "-cubes.txt");
            //System.IO.File.WriteAllText(factpath, sb_fact.ToString());

        }

        public IEnumerable<IEnumerable<QualifiedName>> GetCubeSlices(HyperCube cube) 
        {


            var cubelist = new List<List<QualifiedName>>();

            cubelist.Add(cube.Concepts.Cast<QualifiedName>().ToList());
            var domains = cube.DimensionItems.Select(i => i.Domains.FirstOrDefault()).ToList();
            foreach (var domain in domains) 
            {
                cubelist.Add(domain.DomainMembers.Cast<QualifiedName>().ToList());
            }
            var combinations = Utilities.MathX.CartesianProduct(cubelist);

            return combinations;

        }

        public void LoadLayout()
        {
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
            Rows = rowsnode.ToHierarchy().ToList(); //rowsnode.GetLeafs(); .Where(i => !i.Item.IsAbstract)
            SetDimensions(Rows);

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
                        var hypercubes = HyperCubes.Where(i => i.DimensionItems.Any(j => j.FullName == dimension.Content)).ToList();
                        var domains = hypercubes.SelectMany(i => i.DimensionItems.Where(j => j.FullName == dimension.Content)).SelectMany(k => k.Domains).ToList();
                        var distinctdomains = domains.Distinct().ToList();
                        if (distinctdomains.Count == 1)
                        {
                            foreach (var dm in distinctdomains.FirstOrDefault().DomainMembers)
                            {
                                var li = new LayoutItem();
                                li.Table = this;
                                var dim = new Dimension();
                                dim.Value = dm.Name;
                                dim.Domain = dm.Domain.Name;
                                dim.DomainMember = dm.Name;
                                li.Dimensions.Add(dim);
                                li.LabelID = dm.ID;
                                var labelkey = Label.GetKey(dim.Domain, dm.ID);
                                li.Label = this.Taxonomy.FindLabel(labelkey);
                        
                                //li.LoadLabel(this.Taxonomy);
                                extensions.Add(li);
                            }
                        }
                    }
                }
                else 
                {
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

            var factdeflist = new List<String>();

            foreach (var row in Rows) 
            {
                foreach (var col in Columns)
                {
                    CellList.Add(String.Format("{0}|R{1}|C{2}: {3},{4}", 
                        this.ID, row.Item.LabelCode, col.Item.LabelCode,
                        row.Item.FactString, col.Item.FactString));
                }
            }
            CreateHtmlLayout();
        }

        private void SetDimensions(List<Hierarchy<LayoutItem>> items) 
        {
            foreach (var item in items) 
            {
                var current=item;
                while (current.Parent != null) 
                {
                    MergeDimensions(item.Item.Dimensions, current.Parent.Item.Dimensions);
                    current = current.Parent;
                }
            }
        }

        private void MergeDimensions(List<Dimension> target, List<Dimension> items) 
        {
            foreach (var item in items) 
            {
                var existing = target.FirstOrDefault(i => i.Domain == item.Domain && i.Value == item.Value);
                if (existing == null) 
                {
                    target.Add(item);
                }
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
                htmlbuilder.AppendLine(html);

      

                System.IO.File.WriteAllText(HtmlPath, htmlbuilder.ToString());
            }
        }

        public string GetStyle() 
        {
            var sb = new StringBuilder();
            sb.AppendLine(".left { text-align:left;}");
            sb.AppendLine(".right { text-align:right;}");
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
                        colspan, rowspan, item.Item.LabelContent, item.Item.Factidentifier, item.Item.FactString));
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
                var sublevelcount = maxlevel - level;
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
                    var alt = String.Format("R{0}|C{1}", row.Item.LabelCode, column.Item.LabelCode);
                    sb.AppendLine(String.Format("<td data=\"{0}\" class=\"data\" title=\"{0}\"></td>", alt));
                }

                sb.AppendLine("</tr>");

            }
            

            sb.AppendLine("</tbody>");

            sb.AppendLine("</table>");
            return sb.ToString();

        }
    }


}
