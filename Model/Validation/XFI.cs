using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public partial class Functions
    {
        public Period XFI_Period(ValidationParameter a)
        {
            var fact = a.CurrentFacts.FirstOrDefault();
            var ct = Instance.Contexts.Items[fact.ContextID];

            return Instance.Contexts.Periods[ct.PeriodID];
            //return null;
        }
        public DateTime? XFI_Period_Instant(Period p)
        {
            return p.Instant;
        }
        public Entity XFI_Entity(ValidationParameter a)
        {
            var fact = a.CurrentFacts.FirstOrDefault();
            var ct = Instance.Contexts.Items[fact.ContextID];

            return Instance.Contexts.Entitites[ct.EntityID];
            //return fact.Entity;
            //return null;

        }
        public string XFI_Entity_Identifier(Entity e)
        {
            return e.ID;
        }
        public nString XFI_Fact_Typed_Dimension_Value(ValidationParameter a, string qname)
        {
            var result = "";
            var fact = a.CurrentFacts.FirstOrDefault();
            var dim = fact.Dimensions.FirstOrDefault(i => i.DimensionItem == qname);
            if (dim != null)
            {
                result = dim.DomainMember;
            }
            return new nString(result);
        }
        public nString XFI_Fact_Explicit_Dimension_Value(ValidationParameter a, string qname)
        {
            var result = "";
            var fact = a.CurrentFacts.FirstOrDefault();
            var dim = fact.Dimensions.FirstOrDefault(i => i.DimensionItem == qname);
            if (dim != null)
            {
                result = dim.DomainAndMember;
            }
            return new nString(result);
        }
    }
}
