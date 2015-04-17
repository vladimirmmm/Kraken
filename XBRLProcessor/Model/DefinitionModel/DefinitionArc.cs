using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Enums;
using XBRLProcessor.Model.Base;

namespace Model.DefinitionModel
{
    public class DefinitionArc:Arc
    {
        public Boolean Closed { get; set; }

        public Boolean Usable { get; set; }

        private String _ContextElement = "";
        public String ContextElement { get { return _ContextElement; } set { _ContextElement=value;} }

        private String _TargetRole = "";
        public String TargetRole { get { return _TargetRole; } set { _TargetRole = value; } }

        public String RoleType
        {
            get
            {
                return ArcRoleType.GetKeyByValue(ArcRole);
            }
        }
    }
}
