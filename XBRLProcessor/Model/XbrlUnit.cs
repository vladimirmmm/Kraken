using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBRLProcessor.Model
{
    public class XbrlUnit:Identifiable
    {
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string NsUnit { get; set; }
        public string ItemType { get; set; }
        public DateTime ItemTypedate;
        public string Definition { get; set; }
        public string BaseStandard { get; set; }
        public string Status {get; set;}
        public DateTime VersionDate;
    }
}
