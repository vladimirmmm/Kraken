using CodeModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CodeModel
{
    public class Language
    {
        public string ResourceFolder = "Languages";
        public Syntax2 Syntax = null;
        public CodeParser Parser = null;
        public IMaterializer Materializer = null;
        public Language() 
        {

        }
        public Language(Syntax2 syntax, CodeParser parser) 
        {
            this.Syntax = syntax;
            this.Parser = parser;
        }

        public virtual Expression GetExpression(string expressionstring) 
        {
            return null;
        }

    }
}
