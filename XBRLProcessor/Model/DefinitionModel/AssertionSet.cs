using BaseModel;
using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;

namespace XBRLProcessor.Model.DefinitionModel
{
    public class AssertionSet : XbrlHierarchy<IdentifiablewithLabel>
    {
        public override Hierarchy<IdentifiablewithLabel> GetHierarchy()
        {
            DefinitionRoot = Hierarchy<IdentifiablewithLabel>.GetHierarchy(Arcs, Items,
                    (i, a) => i.Item.LabelID == a.From || i.Item.LabelID == a.From,
                    (i, a) => i.Item.LabelID == a.To || i.Item.LabelID == a.To,
                    (i, a) =>
                    {
                        i.Order = a.Order;
                        var role = "";
                        if (a.ArcRole.EndsWith("applies-to-table")) 
                        {
                            role = "table";
                        }
                        if (a.ArcRole.EndsWith("assertion-set"))
                        {
                            role = "rule";
                        }
                        i.Item.ParentRole = role;
                        i.Item.ID = i.Item.LabelID.StartsWith("loc_") ? i.Item.LabelID.Substring("loc_".Length) : i.Item.LabelID;
                    });

            return DefinitionRoot;
        }

    
    }
    class c1 
    {
        protected string x = "";
    }
    class c2 : c1 
    {
        public string xx 
        {
            get { return x; }
            set { x = value; }
        }
        public void sasfasf() 
        {
            var x = 0;
            for (int i = 0; i < 10; i++)
            {
                x = i;
            }
        }
        public void sasfasf2()
        {
            var z = 0;
            for (int i = 0; i < 10; i++)
            {
               z = i+1;
            }
        }

        public void sasfasf3()
        {
            var z = 0;
            for (int i = 0; i < 10; i++)
            {
                z = i + 1;
            }
        }

        public void sasfasf4()
        {
            var z = 0;
            for (int i = 0; i < 10; i++)
            {
                z = i + 1;
            }
        }
    }
}
