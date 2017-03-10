using BaseModel;
using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities;
using XBRLProcessor.Model.Base;

namespace XBRLProcessor.Model
{
    public class XbrlHierarchy<TClass> where TClass : IdentifiablewithLabel,new()
    {
        public List<TClass> Items = new List<TClass>();
        public List<Arc> Arcs = new List<Arc>();
        public Hierarchy<TClass> DefinitionRoot { get; set; }

        private AttributeSelector idattributeselector = new AttributeSelector("id");
        private AttributeSelector labelattributeselector = new AttributeSelector("*:label");
        private AttributeSelector hrefattributeselector = new AttributeSelector("*:href");
        private AttributeSelector fromattributeselector = new AttributeSelector("*:from");
        private AttributeSelector toattributeselector = new AttributeSelector("*:to");

        public void LoadFromXml(XmlNode node)
        {
            var allchilds = Utilities.Xml.SelectChildNodes(node, "*").ToList();
            var arcnodes = allchilds.Where(i =>
                !String.IsNullOrEmpty(Utilities.Xml.Attr(i, fromattributeselector)) //i.Attributes["from"] != null 
                && !String.IsNullOrEmpty(Utilities.Xml.Attr(i, toattributeselector)) //i.Attributes["to"] != null
                ).ToList();
            var identifiables = allchilds.Where(i => !String.IsNullOrEmpty(Utilities.Xml.Attr(i, labelattributeselector))).ToList();

            var arcmapping = Mapping.Mappings.CurrentMapping.MappingCollection.FirstOrDefault(i => i.ClassType == typeof(Arc));
            foreach (var arcnode in arcnodes) 
            {
                var arc = arcmapping.Map<Arc>(arcnode);
                Arcs.Add(arc);
            }
            foreach (var identifiable in identifiables)
            {
                var item = new IdentifiablewithLabel();
                item.ID = Utilities.Xml.Attr(identifiable, idattributeselector);
                item.LabelID = Utilities.Xml.Attr(identifiable, labelattributeselector);
                item.HRef = Utilities.Xml.Attr(identifiable, hrefattributeselector);
                Items.Add(item as TClass);
            }
        }

        public virtual Hierarchy<TClass> GetHierarchy() 
        {
            //DefinitionRoot = Hierarchy<TClass>.GetHierarchy(Arcs, Items,
            //                  (i, a) => i.Item.ID == a.From || i.Item.LabelID == a.From,
            //                  (i, a) => i.Item.ID == a.To || i.Item.LabelID == a.To,
            //                  (i, a) =>
            //                  {
            //                      i.Order = a.Order;
            //                  });

            DefinitionRoot = Hierarchy<TClass>.GetHierarchy(Arcs, Items,
                         (i, a) => i.Item.LabelID == a.From || i.Item.LabelID == a.From,
                         (i, a) => i.Item.LabelID == a.To || i.Item.LabelID == a.To,
                         (i, a) =>
                         {
                             i.Order = a.Order;
                         });

            return DefinitionRoot;
        }
    }
}
