using BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Enums;
using XBRLProcessor.Mapping;
using XBRLProcessor.Model.Base;

namespace Model.DefinitionModel
{
    public class DefinitionLink:Link
    {
        private List<Locator> _Locators = new List<Locator>();
        [JsonIgnore]
        public List<Locator> Locators { get { return _Locators; } set { _Locators = value; } }

        private List<DefinitionArc> _DefinitionArcs = new List<DefinitionArc>();
        [JsonIgnore]
        public List<DefinitionArc> DefinitionArcs { get { return _DefinitionArcs; } set { _DefinitionArcs = value; } }

        public List<Link> Xlinks = new List<Link>();
        public List<Arc> Arcs = new List<Arc>();

        public Hierarchy<Locator> DefinitionRoot { get; set; }
        [JsonIgnore]
        public List<Hierarchy<Locator>> DefinitionItems = new List<Hierarchy<Locator>>();

        private string _RoleType = "";
        public string RoleType
        {
            get { return _RoleType; }
            set { _RoleType = value; }
        }

        public void LoadHierarchy()
        {
            //Arcs.AddRange(DefinitionArcs);
            DefinitionItems.Clear();
            foreach (var locator in Locators) 
            {
                var hi = new Hierarchy<Locator>(locator);
                DefinitionItems.Add(hi);

            }
            DefinitionRoot = Hierarchy<Locator>.GetHierarchy(DefinitionArcs, DefinitionItems,
                               (i, a) => i.Item.LabelID == a.From, (i, a) => i.Item.LabelID == a.To,
                               (i, a) => { 
                                   i.Order = a.Order;
                                   var defarc = a as DefinitionArc;
                                   i.Item.RoleType = defarc.RoleType;
                                   i.Item.TargetRole = defarc.TargetRole;
                                   //if (i.Item.RoleType == ArcRoleType.domain_member)
                                   //{
                                      
                                   //}
                                   //var defarc = a as DefinitionArc;
                                   //if (defarc != null)
                                   //{
                                   //    if (!String.IsNullOrEmpty(defarc.TargetRole)) 
                                   //    { 
                                   //        links.FirstOrDefault(i=>i.)
                                   //    }
                                   //}
                               });

            if (DefinitionRoot.Children.Count == 1) 
            {
                RoleType = DefinitionRoot.Children.FirstOrDefault().Item.RoleType;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}",this.GetType().Name, this.DefinitionRoot);
        }
    }
}
