using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class TableHelpers
    {
        public static List<LayoutItem> GetAspectItems(Hierarchy<LayoutItem> hli, Table table)
        {
            var results = new List<LayoutItem>();
            if (hli.Item.IsAspect)
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

        public static Hierarchy<LayoutItem> CombineExtensionNodes(List<Hierarchy<LayoutItem>> nodes,Table table) 
        {
            var typednodes = nodes.Where(i => i.Item.Dimensions.Count == 1 && i.Item.Dimensions.FirstOrDefault().IsTyped).ToList();
            var typednode = typednodes.FirstOrDefault();
            var normalnodes = nodes.Except(typednodes);
            //set the aspectnodes if any
            foreach (var normalnode in normalnodes) 
            {
                var parentnode = normalnode.Parent;
                var aspectnodes = GetAspectItems(parentnode, table);
                if (aspectnodes.Count != 0) 
                {
                    parentnode.Children.Clear();
                    foreach (var aspectnode in aspectnodes) 
                    {
                        var hli = new Hierarchy<LayoutItem>(aspectnode);
                        hli.Parent = parentnode;
                        parentnode.Children.Add(hli);
                    }
                }
            }

            normalnodes = normalnodes.OrderBy(i => i.Children.Count).ToList();
            var sets = new List<List<LayoutItem>>();
            if (typednodes.Count > 0)
            {
                foreach (var normalnode in normalnodes)
                {
                    sets.Add(normalnode.Children.Select(i => i.Item).ToList());
                    Dimension.MergeDimensions(normalnode.Item.Dimensions, typednode.Item.Dimensions);
                    if (normalnode.Item.Concept == null)
                    {
                        normalnode.Item.Concept = typednode.Item.Concept;
                    }
                }
            }
            var combinations = Utilities.MathX.CartesianProduct(sets);
            var extensionnode = new Hierarchy<LayoutItem>(new LayoutItem());
            foreach (var combination in combinations) 
            {
                AddCombinationTo(combination, extensionnode);
            }
            if (typednodes.Count > 1) 
            {
                throw new NotImplementedException("Multiple TypedDimension nodes on z axis");
            }
            return extensionnode;
        }
        private static void AddCombinationTo(IEnumerable<LayoutItem> combination, Hierarchy<LayoutItem> target)
        {
            var count = combination.Count();
            var currentparent = target;
            for (int i = 0; i<count; i++) 
            { 
                var li = combination.ElementAt(i);

                var hli = currentparent.Children.FirstOrDefault(o => o.Item.ID == li.ID);
                if (hli == null)
                {
                    hli = new Hierarchy<LayoutItem>(li);
                    currentparent.Children.Add(hli);
                    hli.Parent = currentparent;
                }
                currentparent = hli;
            }
        }
    }
}
