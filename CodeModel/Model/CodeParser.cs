using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeModel.Model
{
    public class CodeParser
    {
        public Syntax2 Syntax = null;
        public CodeParser(Syntax2 Syntax) 
        {
            this.Syntax = Syntax;
        }

        public virtual List<Glyph> ConvertToGlyphs(string item)
        {

            return null;
        }
        public virtual Expression ConvertToExpressionTree(List<Glyph> glyphs)
        {

            return null;
        }

        public virtual Expression Specialize(Expression expression)
        {
            return expression;
        }
        public virtual Expression GetExpression(string expressionstring)
        {
            return null;
        }
        public virtual string Translate(Expression expr)
        {
            return null;

        }
    }

}
