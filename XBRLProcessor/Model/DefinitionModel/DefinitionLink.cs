using BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public void LoadHierarchy()
        {
            Arcs.AddRange(DefinitionArcs);
            foreach (var locator in Locators) 
            {
                var hi = new Hierarchy<Locator>(locator);
                DefinitionItems.Add(hi);

            }
            DefinitionRoot = Hierarchy<Locator>.GetHierarchy(Arcs, DefinitionItems,
                               (i, a) => i.Item.LabelID == a.From, (i, a) => i.Item.LabelID == a.To,
                               (i, a) => { i.Order = a.Order; i.Item.RoleType = ((DefinitionArc)a).RoleType; });
        }
    }
}
