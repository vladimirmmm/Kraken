using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InstanceModel
{
    public class FilingIndicator
    {
        public string Value { get; set; }
        public string ContextID { get; set; }

        public const string DefaultContextID = "CT_F";

        public string ToXmlString() 
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("<find:filingIndicator contextRef=\"{1}\">{0}</find:filingIndicator>", this.Value, this.ContextID));
            return sb.ToString(); 
        }
    }
}
