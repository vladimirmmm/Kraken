using BaseModel;
using LogicalModel.Base;
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
        private string _HtmlPath = "";
        public string HtmlPath 
        {
            get { return _HtmlPath; }
            set { _HtmlPath=value; }
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

        public Hierarchy<Locator> DefinitionRoot { get; set; }
        [JsonIgnore]
        public List<Hierarchy<Locator>> DefinitionItems = new List<Hierarchy<Locator>>();

        public string FilingIndicator;

        
        public override string ToString()
        {
            return base.ToString();
        }


        public void CreateHtmlLayout(bool regenerate=false)
        {
            var folder = Utilities.Strings.GetFolder(HtmlPath);
            var htmlbuilder = new StringBuilder();
            var template = System.IO.File.ReadAllText("TableTemplate.html");
            htmlbuilder.AppendLine(template.Replace("#table#", GetTableHtml()));
     
            if (!System.IO.Directory.Exists(folder)) 
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            System.IO.File.WriteAllText(HtmlPath, htmlbuilder.ToString());
        }

        public string GetTableHtml()
        {

            var rowsnode = LayoutRoot.Find(i => i.Item.LabelContent.ToLower() == "rows");
            var columnsnode = LayoutRoot.Find(i => i.Item.LabelContent.ToLower() == "columns");
            BuildLevelX(columnsnode);
            BuildLevelY(rowsnode.Children.Count > 0 ? rowsnode.Children.FirstOrDefault() : rowsnode);
            var sb = new StringBuilder();
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            var xlevelcount = X_Axis.Count;
            var ylevelcount = rowsnode.GetSubLevelCount(null)+1;
            var xleafcount = columnsnode.GetLeafCount(null);
            var max_y = Y_Axis.Max(i => i.Value.Count);
            var max_x = X_Axis.Max(i => i.Value.Count) + xleafcount;

            sb.AppendLine(String.Format("<tr><th colspan=\"{1}\">{0}</th></tr>", LayoutRoot.Item.LabelContent, xleafcount + ylevelcount+1));
            var columncoderow = "";
            foreach (var lvl in X_Axis)
            {
                sb.AppendLine("<tr>");
                if (lvl.Key == 0)
                {
                    sb.AppendLine(String.Format("<th colspan=\"{0}\" rowspan=\"{1}\">{2}</th>", ylevelcount+1 , xlevelcount+1, rowsnode.Item.LabelContent));

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
                    sb.AppendLine(String.Format("<th {0} {1} factid=\"{3}\">{2}</th>", colspan, rowspan, item.Item.LabelContent, item.Item.Factidentifier));
                }
                sb.AppendLine("</tr>");
             
            }
            sb.AppendLine("<tr>");
            sb.AppendLine(columncoderow);
            sb.AppendLine("</tr>");

            sb.AppendLine("</thead>");
            var style = " style=\"text-align:left;\"";
            sb.AppendLine("<tbody>");
            foreach (var lvl in Y_Axis)
            {

                foreach (var item in lvl.Value)
                {
                    sb.AppendLine("<tr>");
                    var tc = item.GetLeafCount(null);
                    var sublevelcount = item.GetSubLevelCount(null);
                    var itemlevel = item.GetLevel()-1;
                    var levelx = ylevelcount - itemlevel;

                    var colspan = levelx > 0 ? String.Format(" colspan=\"{0}\"", levelx) : "";
                    var rowspan = "";

                    for (int i = 0; i < itemlevel; i++)
                    {
                        sb.AppendLine("<th style=\"width:10px\"></th>");

                    }

                    sb.AppendLine(String.Format("<th{0}{1}{3} factid=\"{4}\">{2}</th>", colspan, rowspan, 
                        item.Item.LabelContent, style, item.Item.Factidentifier));
                    sb.AppendLine(String.Format("<th>{0}</th>", item.Item.LabelCode, style));

                    for (int i = 0; i < xleafcount; i++) 
                    {
                        var alt = "";
                        sb.AppendLine(String.Format("<td alt=\"{0}\"></td>",alt));

                    }
                    sb.AppendLine("</tr>");

                }

            }

            sb.AppendLine("</tbody>");

            sb.AppendLine("</table>");
            return sb.ToString();

        }


        private List<KeyValue<int, List<Hierarchy<LayoutItem>>>> X_Axis = new List<KeyValue<int, List<Hierarchy<LayoutItem>>>>();
        private List<KeyValue<int, List<Hierarchy<LayoutItem>>>> Y_Axis = new List<KeyValue<int, List<Hierarchy<LayoutItem>>>>();

        public void BuildLevelX(Hierarchy<LayoutItem> item, int level = 0)
        {
            var lvlitem = X_Axis.FirstOrDefault(i => i.Key == level);
            if (lvlitem == null)
            {
                lvlitem = new KeyValue<int, List<Hierarchy<LayoutItem>>>(level, new List<Hierarchy<LayoutItem>>());
                X_Axis.Add(lvlitem);
            }
            lvlitem.Value.Add(item);

            foreach (var child in item.Children)
            {
                child.Parent = item;
                BuildLevelX(child, level + 1);

            }

        }

        public void BuildLevelY(Hierarchy<LayoutItem> item, int level = 0)
        {
            var lvlitem = Y_Axis.FirstOrDefault(i => i.Key == level);
            if (lvlitem == null)
            {
                lvlitem = new KeyValue<int, List<Hierarchy<LayoutItem>>>(level, new List<Hierarchy<LayoutItem>>());
                Y_Axis.Add(lvlitem);
            }
            lvlitem.Value.Add(item);

            foreach (var child in item.Children)
            {
                child.Parent = item;
                BuildLevelY(child, level);

            }
        }


        public void SetHtmlPath()
        {
            string folder = Taxonomy.TaxonomyLayoutFolder;
            var filepath = folder + this.ID + ".html";
            HtmlPath = filepath;
        }
    }


}
