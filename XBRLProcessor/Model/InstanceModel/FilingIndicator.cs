using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBRLProcessor.Model.InstanceModel
{
    public class FilingIndicator
    {
        public string Value { get; set; }
        public string ContextID { get; set; }
        public bool _Filed = true;
        public bool Filed { get { return _Filed;} set { _Filed=value;} }

        public const string DefaultContextID = "CT_F";

        public string ToXmlString() 
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("<find:filingIndicator contextRef=\"{1}\" find:filed=\"{2}\">{0}</find:filingIndicator>", this.Value, this.ContextID,this.Filed));
            return sb.ToString(); 
        }
    }
}
