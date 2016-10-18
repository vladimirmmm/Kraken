using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.StringEnums;

namespace XBRLProcessor.Model.DefinitionModel
{
    public class ValueAssertion : XbrlIdentifiable
    {
        public AspectModel _AspectModel = AspectModel.Dimensional;
        public AspectModel AspectModel { get { return _AspectModel; } set { _AspectModel = value; } }

        private string _Test = "";
        public string Test { get { return _Test; } set { _Test = value; } }

        public Boolean ImplicitFiltering { get; set; }


    }
    public class Variable : XbrlIdentifiable
    {
        public String Name { get; set; }
        public Boolean BindAsSequence { get; set; }

        private string _FallbackValue = "";
        public string FallbackValue { get { return _FallbackValue; } set { _FallbackValue = value; } }

        public override string ToString()
        {
            return base.ToString() + (BindAsSequence ? " [SEQ]" : "");
        }

    }
    public class FactVariable : Variable 
    {
    
    }
    public class ComplementArc : Arc 
    {
        public Boolean Complement { get; set; }

    }
    public class VariableFilterArc : ComplementArc 
    {
        public Boolean Cover { get; set; }


    }
    public class VariableSetFilterArc : ComplementArc
    {

    }
    public class VariableArc : Arc
    {
        private string _Name = "";
        public string Name { get { return _Name; } set { _Name = value; } }

    }
}
