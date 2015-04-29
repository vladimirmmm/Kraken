using Model.DefinitionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBRLProcessor.Model.DefinitionModel.Formula
{
    public class DimensionAspect:QName
    {
        public Boolean IncludeUnreportedValue { get; set; }

        public DimensionAspect()
        {
        }
        public DimensionAspect(string content) :base(content)
        {
        }

    }
}
