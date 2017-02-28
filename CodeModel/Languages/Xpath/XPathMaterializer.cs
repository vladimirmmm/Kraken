using CodeModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeModel.Languages.Xpath
{
    public class XPathMaterializer:Materializer
    {
        public new XPathLanguage Language { get { return base.Language as XPathLanguage; } }
        public XPathMaterializer(XPathLanguage Language) 
        {
            base.Language = Language;
        }
        public override string Materialize(Expression expression)
        {

            return base.Materialize(expression);
           
        }
        public override string Materialize(Glyph g)
        {
            var op = g as Operator;
            if (op != null) 
            {
                return Language.Syntax.Operators[op.Name];
            }
            var keyword = g as KeyWord;
            if (keyword != null)
            {
                return Language.Syntax.Keywords[keyword.Name];
            }
            var separator = g as Separator;
            if (separator != null)
            {
                return Language.Syntax.Separators[separator.Name];
            }
            var literal = g as Literal;
            if (literal != null)
            {
                var literaldelimiter = Language.Syntax.Separators["Literal"];
                return literaldelimiter + literal.Value + literaldelimiter;
            }
            return g.Value;
        }
    }
}
