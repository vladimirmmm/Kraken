using BaseModel;
using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XBRLProcessor.Model.Base;

namespace XBRLProcessor.Model
{
    public class XbrlHierarchy<TClass> where TClass : IdentifiablewithLabel
    {
        public List<TClass> Items = new List<TClass>();
        public List<Arc> Arcs = new List<Arc>();
        public Hierarchy<TClass> DefinitionRoot { get; set; }

        public void LoadFromXml(XmlNode node)
        {
            var allchilds = Utilities.Xml.SelectChildNodes(node, "*").ToList();
            var arcnodes = allchilds.Where(i => 
                !String.IsNullOrEmpty(Utilities.Xml.Attr(i,"*:from")) //i.Attributes["from"] != null 
                &&  !String.IsNullOrEmpty(Utilities.Xml.Attr(i,"*:to")) //i.Attributes["to"] != null
                ).ToList();
            var identifiables = allchilds.Where(i => !String.IsNullOrEmpty(Utilities.Xml.Attr( i,"*:label"))).ToList();

            var arcmapping = Mapping.Mappings.CurrentMapping.MappingCollection.FirstOrDefault(i => i.ClassType == typeof(Arc));
            foreach (var arcnode in arcnodes) 
            {
                var arc = arcmapping.Map<Arc>(arcnode);
                Arcs.Add(arc);
            }
            foreach (var identifiable in identifiables)
            {
                var item = new IdentifiablewithLabel();
                item.ID = Utilities.Xml.Attr(identifiable, "id");
                item.LabelID = Utilities.Xml.Attr(identifiable, "*:label");
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
