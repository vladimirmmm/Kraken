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
       
        public static Hierarchy<LayoutItem> GetExtensions(Hierarchy<LayoutItem> axisnode, Table table) 
        {
            var extensions = new Hierarchy<LayoutItem>(table.GetRootExtension());
            //Getting the layout nodes
            var nodes = axisnode.All().Where(i => i.Item.IsLayout).ToList();

            //ensuring typed
            foreach (var node in nodes) 
            {
                foreach (var dim in node.Item.Dimensions) 
                {
                    dim.SetTyped();
                }
            }

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

                //set typed
                foreach (var dim in dimensions)
                {
                    dim.SetTyped();
                }

                li.Dimensions = dimensions;

                extensions.Children.Add(hli);
                ix++;
            }
            if (typednodes.Count > 0) 
            {

            }
            //Fixing the labels
            FixLabels(extensions);

            return extensions;
        }

        public static void FixLabels(Hierarchy<LayoutItem> hli)
        {
            var ix =0;
            var hastypedextension = false;
            var firstchild = hli.Children.FirstOrDefault();
            if (firstchild!=null)
            {
                hastypedextension = firstchild.Where(i => i.Item.Dimensions.Any(j => j.IsTyped)).Count > 1;
            }

            foreach (var child in hli.Children)
            {
                var firstnode= child.Children.FirstOrDefault();

                if (child.Children.Count == 1
                    && !String.IsNullOrEmpty(firstnode.Item.LabelCode)
                    && !hastypedextension)
                {
                    child.Item.LabelCode = firstnode.Item.LabelCode;
                    //var dim = firstchild.Item.Dimensions.FirstOrDefault();
                    //var label = TaxonomyEngine.CurrentEngine.CurrentTaxonomy.GetLabelForDimension()

                    child.Item.LabelContent = firstnode.Item.LabelContent;
                    child.Item.ID = firstnode.Item.ID;
                }
                else
                {
                    var code = String.Format(Table.LabelCodeFormat, ix);
                    var content = String.Format(Table.ExtensionLableContentFormat, code);
                    if (hastypedextension)
                    {
                        child.Item.LabelCode = "_";

                    }
                    else
                    {
                        child.Item.LabelCode = code;
                    }
                    child.Item.LabelContent = content;
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
  
        

    }
}
