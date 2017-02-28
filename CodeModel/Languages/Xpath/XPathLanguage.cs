using CodeModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeModel.Languages.Xpath
{
    public class XPathLanguage:Language
    {
        public XPathLanguage() 
        {
            var file = System.IO.File.ReadAllText(ResourceFolder + @"\Xpath\Xpath.json");
            var syntax = new Syntax2();
            syntax.Load(file);
            this.Syntax = syntax;
            this.Parser = new XPathParser(syntax);
            this.Materializer = new XPathMaterializer(this);
        }
    }
}
