using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;

namespace Model.DefinitionModel
{
    public class Hier
    {
        private List<DefinitionLink> _DefinitionLinks = new List<DefinitionLink>();
        public List<DefinitionLink> DefinitionLinks { get { return _DefinitionLinks; } set { _DefinitionLinks = value; } }
    }
}
