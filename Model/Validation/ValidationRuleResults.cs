using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public enum ValidationRuleIssueType
    {
        None,
        NonSequencedMultipleFacts,

    }
    public class ValidationRuleIssue 
    {
        public ValidationRuleIssueType IssuType = ValidationRuleIssueType.None;
        public string RuleID = "";
        public string ParameterName = "";

        public ValidationRuleIssue() { }
        public ValidationRuleIssue(ValidationRuleIssueType IssuType) { this.IssuType = IssuType; }
        public ValidationRuleIssue(ValidationRuleIssueType IssuType,string ruleid,string parametername) 
        { 
            this.IssuType = IssuType;
            this.ParameterName = parametername;
            this.RuleID = ruleid;
        }

        public static string IssueToString(ValidationRuleIssueType issue)
        {
            if (issue == ValidationRuleIssueType.NonSequencedMultipleFacts)
            {
                return "Multiple Facts detected for Non Sequenced Parameter";
            }
            return "";
        }

        public override string ToString()
        {
            return String.Format("Parameter {0}: {1}", ParameterName, IssueToString(this.IssuType));
        }
    }

 
    public class ValidationRuleResult
    {
        public bool IsOk = true;
        public string ID { get; set; }
        public string HasAllFind { get; set; }
        public List<string> FactGroup = null;
        private List<SimpleValidationParameter> _Parameters = new List<SimpleValidationParameter>();
        public List<SimpleValidationParameter> Parameters { get { return _Parameters; } set { _Parameters = value; } }
        public string Message = "";
        public ValidationRule Rule = null;
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
                foreach (var item in p.FactIDs)
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
  
        public ValidationRuleResult Copy() 
        {
            var source = this;
            var result = new ValidationRuleResult();
            result.IsOk = source.IsOk;
            result.ID = source.ID;
            result.HasAllFind = source.HasAllFind;
            result.FactGroup = source.FactGroup;
            result.IsOk = source.IsOk;
            result.Rule = source.Rule;
            result.Parameters = source.Parameters.Select(i => i.Copy()).ToList();

            return result;
        }
    }

}
