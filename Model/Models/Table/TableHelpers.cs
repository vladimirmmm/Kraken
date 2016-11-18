using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel
{
    public class LayoutTable 
    {
        public List<LayoutItem> Header = new List<LayoutItem>();

    }
    public class TableHelpers
    {
       
     
        public static Hierarchy<LayoutItem> GetExtensions3(Hierarchy<LayoutItem> axisnode, Table table)
        {
            var extensions = new Hierarchy<LayoutItem>(table.GetRootExtension());
            //Getting the layout nodes
            var nodes = axisnode.All().Where(i => i.Item.IsLayout).ToList();

            //ensuring typed
            foreach (var node in nodes)
            {
                node.Item.SetTyped();
            }

            //Getting typed nodes
            var typednodes = nodes.Where(i => i.Item.Dimensions.Count == 1 && i.Item.Dimensions.FirstOrDefault().IsTyped).ToList();
            //Getting non typed ndoes
            var nontypednodes = nodes.Except(typednodes);


            //Set the aspect nodes
            var aspectnodes = axisnode.Where(i => i.Item.Category == LayoutItemCategory.Aspect).ToList();
            foreach (var aspectnode in aspectnodes)
            {
                aspectnode.Item.Category = LayoutItemCategory.Dynamic;

            }
            var leafs = axisnode.GetLeafs();
            foreach (var leaf in leafs) 
            {
                SetDimensions(leaf);
            }
            extensions.AddChildren(leafs);
            //Fixing the labels
            if (table.ID.Contains("eba_tC_67.00.w")) 
            {

            }
            FixLabels(extensions);

            return extensions;
        }


        public static void FixLabels(Hierarchy<LayoutItem> hli)
        {
            var ix =0;

            var taxonomy = TaxonomyEngine.CurrentEngine.CurrentTaxonomy;
            foreach (var child in hli.Children)
            {
                var firstnode = child.Children.FirstOrDefault();
                var li = child.Item;
                if (string.IsNullOrEmpty(li.LabelCode))
                {
                    if (li.Dimensions.Count == 1)
                    {
                        var dim = li.Dimensions.FirstOrDefault();

                        var l = taxonomy.GetLabelForMember(dim.DomainAndMember);           
                        li.LabelCode = l.Code;
                        li.LabelContent = l.Content;
                    }
                    if (li.Dimensions.Count > 1)
                    {
                        var code = String.Format(Table.LabelCodeFormat, ix);
                        var content = "";
                        var dimcodes = "";
                        var dimlabelcontents = "";

                        foreach (var dim in li.Dimensions)
                        {
                            dimcodes += dim.DomainMemberFullName + ",";
                            var l = taxonomy.GetLabelForMember(dim.DomainAndMember);
                            dimlabelcontents += l.Content + ",";
                        }
                        dimcodes = dimcodes.TrimEnd(',');
                        dimlabelcontents = dimlabelcontents.TrimEnd(',');
                        content = "<" + dimcodes + "><" + dimlabelcontents + ">";
                        li.LabelCode = code;
                        li.LabelContent = content;

                    }
                    if (li.Dimensions.Count == 0)
                    {
                        var code = String.Format(Table.LabelCodeFormat, ix);
                        var content = "Default"; String.Format(Table.ExtensionLableContentFormat, ix);
                        li.LabelCode = code;
                        li.LabelContent = content;

                    }
                    if (li.IsDynamic)
                    {
                        li.LabelCode = Literals.DynamicCode;
                    }
                }

                ix++;
            }
        }


        public static List<int> GetOptionalItems(int[] factintkey,Table table) 
        {
            var result = new List<int>();
            foreach (var keypart in factintkey) 
            {
                if (table.Taxonomy.DimensionDomainsOfMembers.ContainsKey(keypart))
                {
                    var items = GetAspectItems(keypart, table);
                    if (items.Any(i => i.Contains(Literals.DefaultMember))) 
                    {
                        result.Add(keypart);
                    }
                }
            }
            return result;
        }

        public static List<string> GetAspectItems(int dimkey, Table table)
        {
            var result = new List<string>();
            var dimensionstr = table.Taxonomy.CounterFactParts[dimkey];
            var f = new LogicalModel.Base.FactBase();
            f.SetFromStringStable(dimensionstr);
            var dimension = f.Dimensions.FirstOrDefault();

            if (!dimension.IsTyped)
            {
                var hypercubes = table.HyperCubes.Where(i => i.DimensionItems.Any(j => j.FullName == dimension.DimensionItemFullName)).ToList();
                var domains = hypercubes.SelectMany(i => i.DimensionItems.Where(j => j.FullName == dimension.DimensionItemFullName)).SelectMany(k => k.Domains).ToList();
                var distinctdomains = domains.Distinct().ToList();
                if (distinctdomains.Count == 1)
                {
                    foreach (var dm in distinctdomains.FirstOrDefault().DomainMembers)
                    {

                        result.Add(dm.Content);
                    }
                }
            }
            return result;
        }
        public static List<LayoutItem> GetAspectItems(Hierarchy<LayoutItem> hli, Table table)
        {
            var results = new List<LayoutItem>();
            if (hli.Item.Category==LayoutItemCategory.Aspect)
            {
                var dimension = hli.Item.Dimensions.FirstOrDefault();
                var hypercubes = table.HyperCubes.Where(i => i.DimensionItems.Any(j => j.FullName == dimension.DimensionItemFullName)).ToList();
                var domains = hypercubes.SelectMany(i => i.DimensionItems.Where(j => j.FullName == dimension.DimensionItemFullName)).SelectMany(k => k.Domains).ToList();
                var distinctdomains = domains.Distinct().ToList();
                if (distinctdomains.Count == 1)
                {
                    foreach (var dm in distinctdomains.FirstOrDefault().DomainMembers)
                    {
                        var li = new LayoutItem();
                        li.Table = table;
                        li.Category = LayoutItemCategory.Rule;
                        var dim = new Dimension();
                        dim.DimensionItem = dimension.DimensionItemFullName;
                        if (Taxonomy.IsTyped(dm.Domain.Namespace))
                        {
                            dim.Domain = dm.Domain.ToString();
                            dim.DomainMember = "";
                            li.Dimensions.Add(dim);
                            li.LabelID = dm.ID;
                            li.LabelCode = dim.Domain;
                            li.Category = LayoutItemCategory.Dynamic;
                        }
                        else
                        {
                            dim.Domain = dm.Domain.ID;
                            dim.DomainMember = dm.Name;
                            li.Dimensions.Add(dim);
                            li.LabelID = dm.ID;
                        }
                        var domainfolder = dim.Domain.IndexOf("_") > -1 ? dim.Domain.Substring(dim.Domain.LastIndexOf("_") + 1) : dim.Domain;
                        var labelkey = Label.GetKey(domainfolder, dm.ID);
                        li.Label = table.Taxonomy.FindLabel(labelkey);
                        if (String.IsNullOrEmpty(li.LabelCode))
                        {
                            li.LabelCode = li.LabelID;
                        }

                        results.Add(li);
                    }
                }
            }
            return results;

        }

        public static void SetDynamicAxis(Table table, Hierarchy<LayoutItem> source, Hierarchy<LayoutItem> target)
        {

            //var rowsnode = table.GetAxisNode("y");
            //var columnsnode = table.GetAxisNode("x");
            //var extensionnode = table.GetAxisNode("z");

            var aspects = source.Where(i =>i.Item.Category!= LayoutItemCategory.Key && i.Item.Dimensions.Any(d=>d.IsTyped)).ToList();

            var targetAxisnode = target.FirstOrDefault(i => !String.IsNullOrEmpty(i.Item.Axis));
            if (targetAxisnode != null)
            {
                var aspect_source_dimensions = new List<Dimension>();

                var ix = 0;
                foreach (var aspect in aspects)
                {
                    aspect.Item.LabelID = aspect.Parent.Item.LabelID;
                    aspect.Item.Label = aspect.Parent.Item.Label;
                    aspect.Item.LabelContent = aspect.Parent.Item.LabelContent;
                    aspect.Item.LabelCode = String.Format(Table.KeyLabelCodeFormat, ix);
                    aspect.Item.LabelID = "";
                    var rolenode = aspect.FirstOrDefault(i => !String.IsNullOrEmpty(i.Item.Role));
                    if (rolenode != null)
                    {
                        aspect.Item.Role = rolenode.Item.Role;
                        aspect.Item.RoleAxis = rolenode.Item.RoleAxis;

                    }
                    aspect.Item.Category = LayoutItemCategory.Key;

                    targetAxisnode.Children.Insert(ix, aspect);

                    source.Remove(aspect.Parent);

                    aspect.Parent.Remove(aspect);
                    aspect.Parent = targetAxisnode;
                    ix++;
                    aspect_source_dimensions.AddRange(aspect.Item.Dimensions);
                    //aspect.Item.Dimensions.Clear();
                }
                if (source.Children.Count==0)
                {
                    var dyn_li = new LayoutItem();

                    dyn_li.ID = "dynamic_" + source.Item.Axis;
                    dyn_li.Dimensions = aspect_source_dimensions;
                    dyn_li.Category = LayoutItemCategory.Dynamic;
                    //var dyn_h = new Hierarchy<LayoutItem>(dyn_li);
                    source.Item = dyn_li;

                }
            }
        }
        public static void MergeDimensions(List<Dimension> target, List<Dimension> items)
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
        public static void SetDimensions(List<Hierarchy<LayoutItem>> hlis)
        {
            foreach (var hli in hlis) 
            {
                SetDimensions(hli);
            }
         
        }

        public static void SetDimensions(Hierarchy<LayoutItem> hli)
        {
            var currentrow = hli.Parent;
            while (currentrow != null)
            {
                MergeDimensions(hli.Item.Dimensions, currentrow.Item.Dimensions);
                if (hli.Item.Concept == null)
                {
                    hli.Item.Concept = currentrow.Item.Concept;
                }
                currentrow = currentrow.Parent;
            }
            hli.Item.ClearFactStringCache();
        }

        public static void SetDimensions(Cell cell)
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
            cell.Dimensions = cell.Dimensions.Where(i => !i.IsDefaultMember).OrderBy(i => i.MapID).ToList();
            cell.Dimensions = cell.Dimensions.Distinct().ToList();
        }


        public static void SetDynamicAxis2(Table table, Hierarchy<LayoutItem> source, Hierarchy<LayoutItem> target)
        {


            var aspects = source.Where(i => i.Item.Category == LayoutItemCategory.Aspect).ToList();

            var targetAxisnode = target.FirstOrDefault(i => !String.IsNullOrEmpty(i.Item.Axis));
            var sourceAxisnode = source.FirstOrDefault(i => !String.IsNullOrEmpty(i.Item.Axis));

            if (targetAxisnode != null)
            {
                var aspect_source_dimensions = new List<Dimension>();

                var ix = 0;
                foreach (var aspect in aspects)
                {
                    aspect.Item.LabelID = aspect.Parent.Item.LabelID;
                    aspect.Item.Label = aspect.Parent.Item.Label;
                    aspect.Item.LabelContent = aspect.Parent.Item.LabelContent;
                    aspect.Item.LabelCode = String.Format(Table.KeyLabelCodeFormat, ix);
                    aspect.Item.LabelID = "";
                    var rolenode = aspect.FirstOrDefault(i => !String.IsNullOrEmpty(i.Item.Role));
                    if (rolenode != null)
                    {
                        aspect.Item.Role = rolenode.Item.Role;
                        aspect.Item.RoleAxis = rolenode.Item.RoleAxis;

                    }
                    aspect.Item.Category = LayoutItemCategory.Key;
                    SetDimensions(aspect);
                    targetAxisnode.Children.Insert(ix, aspect);
                    
                    source.Remove(aspect.Parent);

                    aspect.Parent.Remove(aspect);
                    aspect.Parent = targetAxisnode;
                    ix++;
                    aspect_source_dimensions.AddRange(aspect.Item.Dimensions);
                    //aspect.Item.Dimensions.Clear();
                }
                var visiblenodes = source.Where(i => i.Item.IsVisible);
          
                if (aspects.Count > 0)
                {
                    var dyn_li = new LayoutItem();

                    dyn_li.ID = "dynamic_" + sourceAxisnode.Item.Axis;
                    dyn_li.Dimensions = aspect_source_dimensions;
                    dyn_li.Category = LayoutItemCategory.Dynamic;
                    dyn_li.LabelCode = Literals.DynamicCode;
                    //var dyn_h = new Hierarchy<LayoutItem>(dyn_li);
                    //source.Item = dyn_li;
                    var dyn_hli = new Hierarchy<LayoutItem>(dyn_li);
                    source.AddChild(dyn_hli);
                }
                //source.Item

            }
        }

        public static Hierarchy<LayoutItem> CreateAxisNode(Table table, string axis)
        {
            var nodes = table.LayoutRoot.Where(i => String.Equals(i.Item.Axis, axis, StringComparison.InvariantCultureIgnoreCase)).OrderBy(i => i.Order).ToList();
            var axislayoutitem = new LayoutItem();
            axislayoutitem.Category = LayoutItemCategory.BreakDown;
            axislayoutitem.ID = String.Format("Axis {0}", axis);
            axislayoutitem.IsAbstract = true;
            var axisnode = new Hierarchy<LayoutItem>(axislayoutitem);

            //Expand AspectNodes
            foreach (var node in nodes)
            {
                var aspectnodes = node.Where(j => j.Item.IsAspect).ToList();
                foreach (var aspectnode in aspectnodes)
                {
                    if (!aspectnode.Item.Dimensions.Any(i => i.IsTyped))
                    {
                        var expandednodes = GetAspectItems(aspectnode, table);
                        if (expandednodes.Count > 0)
                        {
                            var parent = aspectnode.Parent;
                            parent.Remove(aspectnode);
                            parent.AddChildren(expandednodes);
                        }
                    }

                }
            }

            //Project Axisnodes
            if (nodes.Count > 0)
            {
                var basenode = nodes.FirstOrDefault();
                //var basenodelist = new List<Hierarchy<LayoutItem>>();
                //basenodelist.Add(basenode);

                for (int i = 1; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    var baseleafs = basenode.GetLeafs();
                    //List<LayoutItem> projectionnodes = null;

                    var projectionnodes = node.GetLeafs();
                    foreach (var leaf in baseleafs)
                    {
                        foreach (var projectionnode in projectionnodes)
                        {
                            var hi = new Hierarchy<LayoutItem>(projectionnode.Item);
                            hi.Parent = projectionnode.Parent;
                            var targetparent = leaf;
                            var targetchild = hi;
                            if (leaf.Parent.Children.Count == 1)
                            {
                                targetparent = leaf.Parent;
                                targetchild = hi.Parent;
                            }
                            targetchild.Parent.Remove(targetchild);
                            targetparent.AddChild(targetchild);
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
            axisnode.All().ForEach(i => i.Item.SetTyped());

            return axisnode;

        }

        public static void ProjectNodes(Hierarchy<LayoutItem> axisnode) 
        {
            var nodes = axisnode.Children.Where(i=> Utilities.ObjectExtensions.In( i.Item.Category, LayoutItemCategory.BreakDown, LayoutItemCategory.Dynamic)).ToList();
            //Project Axisnodes
            if (nodes.Count > 0)
            {
                var basenode = nodes.FirstOrDefault();
                //var basenodelist = new List<Hierarchy<LayoutItem>>();
                //basenodelist.Add(basenode);

                for (int i = 1; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    var baseleafs = basenode.GetLeafs();
                    //List<LayoutItem> projectionnodes = null;

                    var projectionnodes = node.GetLeafs();
                    foreach (var leaf in baseleafs)
                    {
                        foreach (var projectionnode in projectionnodes)
                        {
                            var copynode = projectionnode.Copy();
                            copynode.All().ForEach(n => n.Item = new LayoutItem(n.Item));
                            leaf.AddChild(copynode);
                            //var hi = new Hierarchy<LayoutItem>(projectionnode.Item);
                            //hi.Parent = projectionnode.Parent;
                            //var targetparent = leaf;
                            //var targetchild = hi;
                            //if (leaf.Parent.Children.Count == 1)
                            //{
                            //    targetparent = leaf.Parent;
                            //    targetchild = hi.Parent;
                            //}
                            //targetchild.Parent.Remove(targetchild);
                            //targetparent.AddChild(targetchild);
                        }
                    }
                }
                if (nodes.Count > 1) 
                {
                    var li = new LayoutItem();
                    li.Category = LayoutItemCategory.BreakDown;
                    li.Axis = axisnode.Item.Axis;
                    li.ID = "Effective_Breakdown_" + li.Axis;
                    li.LabelID = li.ID;
                    var hli = new Hierarchy<LayoutItem>(li);
                    hli.AddChild(basenode);
                    axisnode.Clear();
                    axisnode.AddChild(hli);
                }
                //axisnode.Clear();
                //axisnode.AddChild(basenode);
                //basenode.Item.Category = LayoutItemCategory.BreakDown;
            }
        }

        public static string GetStateOfNodes(Hierarchy<LayoutItem> node, string tag) 
        {
            var sbe = new StringBuilder();
            sbe.AppendLine(tag);
            sbe.AppendLine(node.ToHierarchyString2());
            sbe.AppendLine();
            return sbe.ToString();
        }
        public static Hierarchy<LayoutItem> CreateAxisNode3(Table table, string axis)
        {
            var nodes = table.LayoutRoot.Where(i => String.Equals(i.Item.Axis, axis, StringComparison.InvariantCultureIgnoreCase)).OrderBy(i => i.Order).ToList();
            var axislayoutitem = new LayoutItem();
            axislayoutitem.Category = LayoutItemCategory.BreakDown;
            axislayoutitem.ID = String.Format("Axis {0}", axis);
            axislayoutitem.Axis = axis;
            axislayoutitem.IsAbstract = true;
            var axisnode = new Hierarchy<LayoutItem>(axislayoutitem);
            axisnode.AddChildren(nodes);
            var nodestoremove = new List<Hierarchy<LayoutItem>>();
            axisnode.All().ForEach(i => {
                i.Item.SetTyped();
                if (i.Item.Category == LayoutItemCategory.Filter) 
                {
                    var parent = i.Parent;
                    if (parent!= null) 
                    {
                        parent.Item.Role = i.Item.Role;
                        parent.Item.RoleAxis = i.Item.RoleAxis;
                        nodestoremove.Add(i);
                    }
                }
            });
            foreach (var n in nodestoremove) 
            {
                n.Parent.Remove(n);
            }
            return axisnode;
        }
        public static Hierarchy<LayoutItem> CreateAxisNode2(Table table, string axis)
        {
            var nodes = table.LayoutRoot.Where(i => String.Equals(i.Item.Axis, axis, StringComparison.InvariantCultureIgnoreCase)).OrderBy(i => i.Order).ToList();
            var axislayoutitem = new LayoutItem();
            axislayoutitem.Category = LayoutItemCategory.BreakDown;
            axislayoutitem.ID = String.Format("Axis {0}", axis);
            axislayoutitem.IsAbstract = true;
            var axisnode = new Hierarchy<LayoutItem>(axislayoutitem);

            //Project Axisnodes
            if (nodes.Count > 0)
            {
                var basenode = nodes.FirstOrDefault();
                //var basenodelist = new List<Hierarchy<LayoutItem>>();
                //basenodelist.Add(basenode);

                for (int i = 1; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    var baseleafs = basenode.GetLeafs();
                    //List<LayoutItem> projectionnodes = null;

                    var projectionnodes = node.GetLeafs();
                    foreach (var leaf in baseleafs)
                    {
                        foreach (var projectionnode in projectionnodes)
                        {
                            var hi = new Hierarchy<LayoutItem>(projectionnode.Item);
                            hi.Parent = projectionnode.Parent;
                            var targetparent = leaf;
                            var targetchild = hi;
                            if (leaf.Parent.Children.Count == 1)
                            {
                                targetparent = leaf.Parent;
                                targetchild = hi.Parent;
                            }
                            targetchild.Parent.Remove(targetchild);
                            targetparent.AddChild(targetchild);
                        }
                    }
                }
                axisnode.Children.Add(basenode);
                basenode.Item.Category = LayoutItemCategory.BreakDown;
            }

            axisnode.All().ForEach(i => i.Item.SetTyped());

            return axisnode;

        }


       
    }
}
