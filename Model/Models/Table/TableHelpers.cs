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

        public Hierarchy<LayoutItem> SetExts(Table table, Hierarchy<LayoutItem> extensionnode)
        {
           
            foreach (var node in extensionnode.Children) 
            {

            }
            return null;
        }
        public static Hierarchy<LayoutItem> GetExtensions(Hierarchy<LayoutItem> axisnode, Table table) 
        {
            var extensions = new Hierarchy<LayoutItem>(table.GetRootExtension());
            //Getting the layout nodes
            var nodes = axisnode.All().Where(i => i.Item.IsLayout).ToList();
            //Getting typed nodes
            var typednodes = nodes.Where(i => i.Item.Dimensions.Count == 1 && i.Item.Dimensions.FirstOrDefault().IsTyped).ToList();
            //Getting non typed ndoes
            var nontypednodes = nodes.Except(typednodes);
            //Set the aspect nodes
            var aspectnodes = axisnode.Where(i => i.Item.Category == LayoutItemCategory.Aspect).ToList();
            foreach (var aspectnode in aspectnodes)
            {
                var aspectnodeitems = GetAspectItems(aspectnode, table);
                if (aspectnodeitems.Count != 0)
                {
                    aspectnode.Children.Clear();

                    foreach (var aspectnodeitem in aspectnodeitems)
                    {
                        var hli = new Hierarchy<LayoutItem>(aspectnodeitem);
                        hli.Parent = aspectnode;
                        aspectnode.Children.Add(hli);
                    }
                }
     

            }

            var zaxisnodelistcollection = new List<List<Hierarchy<LayoutItem>>>();
            //get the axis nodes
            var zaxisnodes = axisnode.Where(i => i.Item.Axis == "z").ToList();
         
            foreach (var zaxisnode in zaxisnodes) 
            {
                var zaxisnodelist = GetItemsFor(zaxisnode, table);
                if (zaxisnodelist.Count > 0)
                {
                    zaxisnodelistcollection.Add(zaxisnodelist);
                }
            }

            var singlenodes = zaxisnodelistcollection.Where(i => i.Count == 1 && !i.Any(j => j.Item.IsDynamic)).ToList();

            //combine the nodelist
            var combinations = Utilities.MathX.CartesianProduct(zaxisnodelistcollection);
            var firstcombination = combinations.FirstOrDefault();
            var ix = 0;
            foreach (var combination in combinations) 
            {
                var li = new LayoutItem();
                var hli = new Hierarchy<LayoutItem>(li);

                //setting the dimensions for the combination
                var dimensions = new List<Dimension>();
                foreach (var item in combination)
                {
                    Dimension.SetDimensions(item);
                    Dimension.MergeDimensions(dimensions, item.Item.Dimensions);

                    //adding the parts of the combination to the extesnion node
                    if (!item.Item.IsAbstract)
                    {
                        hli.Children.Add(item);
                    }

                }
                li.Dimensions = dimensions;

                //setting the lable for the combination
                //if (combinationcount == 1) 
                //{
                //    var item = combination.FirstOrDefault();
                //    li.LabelCode = item.Item.LabelCode;
                //    li.LabelContent = item.Item.LabelContent;
                //    li.ID = item.Item.ID;
                //}
                //if (combinationcount > 1) 
                //{
                //    li.LabelCode = String.Format(Table.LabelCodeFormat, ix);
                //    li.LabelContent = String.Format(Table.ExtensionLableContentFormat, li.LabelCode);
                  
                //}

                extensions.Children.Add(hli);
                ix++;
            }

            //Fixing the labels
            FixLabels(extensions);

            return extensions;
        }

        public static void FixLabels(Hierarchy<LayoutItem> hli)
        {
            var ix =0;
            foreach (var child in hli.Children) 
            {
                var firstchild = child.Children.FirstOrDefault();
                if (firstchild != null)
                {
                    foreach (var children in child.Children)
                    {
                        foreach (var dim in children.Item.Dimensions)
                        {
                            dim.SetTyped();
                        }
                    }
                    if (child.Children.Count == 1
                        && !String.IsNullOrEmpty(firstchild.Item.LabelCode)
                        && !firstchild.Item.Dimensions.Any(i => i.IsTyped))
                    {
                        child.Item.LabelCode = firstchild.Item.LabelCode;
                        //var dim = firstchild.Item.Dimensions.FirstOrDefault();
                        //var label = TaxonomyEngine.CurrentEngine.CurrentTaxonomy.GetLabelForDimension()

                        child.Item.LabelContent = firstchild.Item.LabelContent;
                        child.Item.ID = firstchild.Item.ID;
                    }
                    else
                    {
                        var code = String.Format(Table.LabelCodeFormat, ix);
                        var content = String.Format(Table.ExtensionLableContentFormat, code);

                        child.Item.LabelCode = code;
                        child.Item.LabelContent = content;
                    }
                }
                else 
                {

                }
                ix++;  
            }
        }

        public static List<Hierarchy<LayoutItem>> GetItemsFor(Hierarchy<LayoutItem> axisnode, Table table) 
        {
            var result = new List<Hierarchy<LayoutItem>>();
            result = axisnode.Where(i => LayoutItem.IsLayoutLeaf(i)).ToList();
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

        public static Hierarchy<LayoutItem> CombineExtensionNodes(Hierarchy<LayoutItem> axisnode,Table table) 
        {

            var rootextensionli = table.GetRootExtension();
            rootextensionli.Category = LayoutItemCategory.BreakDown;
            var extensionnode = new Hierarchy<LayoutItem>(rootextensionli);
            var nodecontainer = extensionnode;
            var sets = new List<List<LayoutItem>>();
            var nodes = axisnode.All().Where(i => i.Item.IsVisible).ToList();
            if (table.ID.Contains("08"))
            {
            }
            var typednodes = nodes.Where(i => i.Item.Dimensions.Count == 1 && i.Item.Dimensions.FirstOrDefault().IsTyped).ToList();           
            var normalnodes = nodes.Except(typednodes).ToList();

            //add dynamic nodes based on typed nodes
            if (typednodes.Count > 0)
            {
                var dynamicli = new LayoutItem();
                dynamicli.LabelCode = "";
                dynamicli.LabelContent = "Dynamic";
                dynamicli.Category = LayoutItemCategory.Dynamic;
                var dynamicnode = new Hierarchy<LayoutItem>(dynamicli);

                foreach (var typednode in typednodes) 
                {
                    Dimension.MergeDimensions(dynamicli.Dimensions, typednode.Item.Dimensions);
                }
                /*
                extensionnode.Children.Add(dynamicnode);
                dynamicnode.Parent = extensionnode;
                nodecontainer = dynamicnode;
                */
            }

            //set the aspectnodes if any
            var firstnode = nodes.FirstOrDefault();
            if (firstnode != null)
            {
                //var axisroot = firstnode.Parents().LastOrDefault(i => !String.IsNullOrEmpty(i.Item.Axis));

                var aspectnodes = axisnode.Where(i => i.Item.Category == LayoutItemCategory.Aspect).ToList();
                foreach (var aspectnode in aspectnodes)
                {
                    var aspectnodeitems = GetAspectItems(aspectnode, table);
                    if (aspectnodeitems.Count != 0)
                    {
                        aspectnode.Children.Clear();

                        foreach (var aspectnodeitem in aspectnodeitems)
                        {
                            var hli = new Hierarchy<LayoutItem>(aspectnodeitem);
                            hli.Parent = aspectnode;
                            aspectnode.Children.Add(hli);
                        }
                    }
            

                }
                foreach (var axischildren in axisnode.Children)
                {
                    var axischildrennodes = axischildren.Where(i => i.Item.IsVisible && i.Item.Category!=LayoutItemCategory.Aspect).ToList();
                    axischildrennodes = axischildrennodes.Where(i => !i.Parents().Any(j => j.Item.IsAbstract)).ToList();
                    if (axischildrennodes.Count > 200)
                    {

                    }
                    if (axischildrennodes.Count > 0)
                    {
                        sets.Add(axischildrennodes.Select(i => i.Item).ToList());
                    }

                }
            }

            //add root filters from abstractnodes
            var abstractnodes = normalnodes.Where(i => i.Item.IsAbstract || i.Parents().Any(j=>j.Item.IsAbstract)).ToList();
            foreach (var abstractnode in abstractnodes) 
            {
                Dimension.MergeDimensions(rootextensionli.Dimensions, abstractnode.Item.Dimensions);
            }
            normalnodes = normalnodes.Except(abstractnodes).ToList();

            //set the combination set
            foreach (var normalnode in normalnodes) 
            {
                Dimension.SetDimensions(normalnode);
                //sets.Add(normalnode.Children.Select(i => i.Item).ToList());
                //sets.Add(new List<LayoutItem>(){ normalnode.Item});
            }

            //Create combinations from normalnodes
            normalnodes = normalnodes.OrderBy(i => i.Children.Count).ToList();
            var combinations = Utilities.MathX.CartesianProduct(sets);
            var extensionlist = new List<LayoutItem>();
            foreach (var combination in combinations) 
            {
                if (combination.Count() > 0)
                {
                    AddCombinationTo(combination, extensionlist);
                }
            }
            //set typed dimensions on normal nodes
            foreach (var extension in extensionlist) 
            {
                Dimension.MergeDimensions(extension.Dimensions, rootextensionli.Dimensions);
                if (nodecontainer.Item.Category ==LayoutItemCategory.Dynamic)
                {
                    Dimension.MergeDimensions(extension.Dimensions, nodecontainer.Item.Dimensions);
                }

            }


            //extensionnode.Children.AddRange(extensionlist);

            nodecontainer.Children.AddRange(extensionlist.Select(i => new Hierarchy<LayoutItem>(i)));
            if (typednodes.Count > 1) 
            {
                //TODO is this ok?
                //throw new NotImplementedException("Multiple TypedDimension nodes on z axis");
            }

            return nodecontainer;//extensionnode;
        }

        private static void AddCombinationTo(IEnumerable<LayoutItem> combination, List<LayoutItem> target) 
        {
            var li = new LayoutItem();
            li.Category = LayoutItemCategory.Rule;
            foreach (var item in combination) 
            {
                Dimension.MergeDimensions(li.Dimensions, item.Dimensions);
            }       

            target.Add(li);
            foreach (var dim in li.Dimensions) 
            {
                dim.SetTyped();
            }
            if (li.Dimensions.Any(j => j.IsTyped))
            {
                li.Category = LayoutItemCategory.Dynamic;
                li.LabelCode = "*";
                li.LabelContent = "Dynamic";
            }
            else 
            {
                if (combination.Count() == 1)
                {
                    var item = combination.FirstOrDefault();
                    if (item.Category != LayoutItemCategory.Aspect)
                    {
                        li.Label = item.Label;
                        li.LabelCode = item.LabelCode;
                        li.LabelContent = item.LabelContent;
                    }
                }
                else 
                {
                    li.LabelCode = String.Format(Table.LabelCodeFormat, target.Count);
                    li.LabelContent = String.Format("Extension {0}", target.Count);
                }
            }

        }

        //private static void AddCombinationTo(IEnumerable<LayoutItem> combination, Hierarchy<LayoutItem> target)
        //{
        //    var count = combination.Count();
        //    var currentparent = target;
        //    for (int i = 0; i<count; i++) 
        //    { 
        //        var li = combination.ElementAt(i);

        //        var hli = currentparent.Children.FirstOrDefault(o => o.Item.ID == li.ID);
        //        if (hli == null)
        //        {
        //            hli = new Hierarchy<LayoutItem>(li);
        //            currentparent.Children.Add(hli);
        //            hli.Parent = currentparent;
        //        }
        //        currentparent = hli;
        //    }
        //}
   
    }
}
