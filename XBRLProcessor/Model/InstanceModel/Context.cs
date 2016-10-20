using BaseModel;
using LogicalModel;
using LogicalModel.Base;
using Model.DefinitionModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InstanceModel
{
    public class Context:Identifiable
    {
        public Entity Entity { get; set; }
        public Period Period { get; set; }
        [JsonIgnore]
        public Scenario Scenario { get; set; }

        public string ToXmlString() 
        {
            var sb = new StringBuilder();
            var tab = Literals.Tab;
            sb.AppendLine(String.Format(tab + "<xbrli:context id=\"{0}\">", this.ID));
            sb.AppendLine(Entity.ToXmlString(tab + tab));
            sb.AppendLine(Period.ToXmlString(tab + tab));
            sb.AppendLine(Scenario.ToXmlString(tab + tab));
            sb.AppendLine(tab + "</xbrli:context>");

            return sb.ToString();
        }
    }

    public class ContextContainer 
    {
        public Dictionary<String, Dimension> Parts = new Dictionary<String, Dimension>();
        public Dictionary<string, Entity> Entitites = new Dictionary<string, Entity>();
        public Dictionary<string, Period> Periods = new Dictionary<string, Period>();
        public Dictionary<string, InstanceContext> Items = new Dictionary<string, InstanceContext>();

        public void Add(Context xbrlcontext) 
        {
            var context = new InstanceContext();
            context.ID = xbrlcontext.ID;
            if (!Entitites.ContainsKey(xbrlcontext.Entity.ID)) 
            {
                Entitites.Add(xbrlcontext.Entity.ID, xbrlcontext.Entity);
            }
            context.Entity = Entitites[xbrlcontext.Entity.ID];
            if (!Periods.ContainsKey(xbrlcontext.Period.ID))
            {
                Periods.Add(xbrlcontext.Period.ID, xbrlcontext.Period);
            }
            context.Period = Periods[xbrlcontext.Period.ID];
            if (xbrlcontext.Scenario != null)
            {
                foreach (var dim in xbrlcontext.Scenario.Dimensions)
                {
                    if (!Parts.ContainsKey(dim.DomainMemberFullName))
                    {
                        Parts.Add(dim.DomainMemberFullName, dim);
                    }
                    context.FactParts.Add(Parts[dim.DomainMemberFullName]);
                }
            }
            Items.Add(context.ID, context);
        }

        internal void Clear()
        {
            this.Items.Clear();
        }
    }
}
