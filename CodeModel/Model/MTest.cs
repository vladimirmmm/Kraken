using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CodeModel.Model
{
    public abstract class Materializer<T> where T:Tree<Glyph>
    {
        private Language _Language = null;
        public Language Language
        {
            get
            {
                return _Language;
            }
            set
            {
                _Language = value;
            }
        }

        public virtual string Materialize(T expression) 
        {
            return "";
        }
        public virtual string Materialize(Glyph glyph)
        {
            return "";
        }
    }

    public class ExpressionMaterializer : Materializer<Tree<Glyph>> 
    {
        public Dictionary<Type, ExpressionMaterializer> Materializers = new Dictionary<Type, ExpressionMaterializer>();

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

        public override string Materialize(Tree<Glyph> expression)
        {
            var expressiontype = expression.GetType();
            if (Materializers.ContainsKey(expressiontype)) 
            {
                return Materializers[expressiontype].Materialize(expression);
            }
            var sb = new StringBuilder();
            if (expression.Children.Count > 0)
            {
                var blockboundary = Language.Syntax.Boundaries["Block"];
                sb.Append(blockboundary.Left);
                foreach (var child in expression.Children)
                {
                    var childstring = "";
                    if (child is Expression)
                    {
                        var type = child.GetType();


                        childstring = Materialize(child as Expression);

                    }
                    else
                    {
                        childstring = Materialize(child.Item);

                    }
                    sb.AppendLine(childstring);

                }
                sb.Append(blockboundary.Right);
            }

            return sb.ToString();
        }
    }
}
