using BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class InstanceContext:Identifiable
    {
        [JsonIgnore]
        public string EntityID { get; set; }
        [JsonIgnore]
        public string PeriodID { get; set; }
        [JsonIgnore]
        public Entity Entity { get; set; }
        [JsonIgnore]
        public Period Period { get; set; }
        [JsonIgnore]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        private List<Dimension> _Dimensions = new List<Dimension>();
        [JsonIgnore]
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }
        private List<int> _DimensionsIds = new List<int>();
        [JsonIgnore]
        public List<int> DimensionIds { get { return _DimensionsIds; } set { _DimensionsIds = value; } }
        private bool _IsValid = true;

        [JsonIgnore]
        public bool IsValid { get { return _IsValid; } set { _IsValid = true; } }
        private string _Content = "";

        [JsonProperty]
        public string Content { get { return _Content; } set { _Content = value; } }

        public void SetContent()
        {
            _Content = String.Format("{0}<@>{1}<@>{2}<@>{3}<@>{4}", this.ID, this.EntityID, this.PeriodID, this.IsValid, Utilities.Strings.ListToString(DimensionIds, ","));
        }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            var tab = Literals.Tab;
            sb.AppendLine(String.Format(tab + "<xbrli:context id=\"{0}\">", this.ID));
            sb.AppendLine(Entity.ToXmlString(tab + tab));
            sb.AppendLine(Period.ToXmlString(tab + tab));
            //sb.AppendLine(Scenario.ToXmlString(tab + tab));
            sb.AppendLine(tab + "</xbrli:context>");

            return sb.ToString();
        }
    }
}
