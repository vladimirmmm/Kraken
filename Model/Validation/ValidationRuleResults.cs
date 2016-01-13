using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public class ValidationRuleResult
    {
        public bool IsOk = true;
        public string ID { get; set; }
        public string HasAllFind { get; set; }
        public List<string> FactGroup = null;
        private List<SimpleValidationParameter> _Parameters = new List<SimpleValidationParameter>();
        public List<SimpleValidationParameter> Parameters { get { return _Parameters; } set { _Parameters = value; } }
        public string Message = "";

        public string GetDetails()
        {
            var sb = new StringBuilder();
            //sb.AppendLine(Message);
            sb.Append("    ");
            foreach (var p in Parameters)
            {
                sb.AppendFormat("{0}({1}), ", p.Name, p.Value);

            }
            sb.AppendLine();
            sb.Append("    ");

            foreach (var p in Parameters)
            {
                sb.AppendFormat("{0}[", p.Name);
                foreach (var item in p.Facts)
                {
                    sb.Append(item + ", ");
                }
                sb.Append("], ");

            }
            sb.AppendLine();

            return sb.ToString();
        }
       
        public override string ToString()
        {
            return String.Format("{0} - {1}", this.GetType().Name, this.ID);
        }
    }

}
