using LogicalModel;
using Model.DefinitionModel.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.DefinitionModel.Formula;

namespace XBRLProcessor.Model.DefinitionModel
{
    public class RuleNode:XbrlIdentifiable
    {
        private Boolean _Abstract = false;
        public Boolean Abstract { get { return _Abstract; } set { _Abstract = value; } }

        private Concept _Concept = null;
        public Concept Concept { get { return _Concept; } set { _Concept = value; } }

        private List<RuleDimension> _Dimensions = new List<RuleDimension>();
        public List<RuleDimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }
    }
}
