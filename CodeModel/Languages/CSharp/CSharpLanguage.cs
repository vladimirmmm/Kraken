using CodeModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeModel.Languages.CSharp
{
    public class CSharpLanguage:Language
    {
        public CSharpLanguage() 
        {
            var file = System.IO.File.ReadAllText(ResourceFolder + @"\CSharp\CSharp.json");

            var syntax = new Syntax2();
            syntax.Load(file);
            this.Syntax = syntax;
            this.Materializer = new CSharpMaterializer(this);
            //this.Parser = new XPathParser(syntax);
        }
    }
}
