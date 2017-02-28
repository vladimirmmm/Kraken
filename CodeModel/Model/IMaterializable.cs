using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CodeModel.Model
{
    public interface IMaterializable
    {
        //string Materialize(IMaterializer m);
        string Materialize(Materializer m, Language language);
        //string Materialize(Materializable m);

    }
    public class Materializable
    {
        public virtual string Materialize(IMaterializer m)
        {
            return "";
        }

    }
    public interface IMaterializer
    {
        Language Language { get; set; }
        string Materialize(IMaterializable expression);

    }


    public class Materializer :IMaterializer
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

        public Dictionary<Type, Func<Expression, string>> ExpressionMaterializers = new Dictionary<Type, Func<Expression, string>>();
       

        public virtual string Materialize(IMaterializable expression) 
        {
            var glyph = expression as Glyph;
            if (glyph != null) { return Materialize(glyph); }
            var expr = expression as Expression;
            if (expr != null) { return Materialize(expr); }
            return "";
        }
        public virtual string Materialize(Glyph g)
        {
            return "";
        }

        public virtual string Materialize(Expression expression) 
        {
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

                        var materializer = ExpressionMaterializers.ContainsKey(type) ? ExpressionMaterializers[type] : null;

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

    public class testm : Materializer 
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
                throw new NotImplementedException();
            }
        }
        public testm()
        {
            var l = new Language();
            l.Materializer = this;
            _Language = l;
        }




        public override string Materialize(IMaterializable expression)
        {
            return "";
        }

     
    }
    public class test 
    {
        public void testme()
        {
            var ifexpression = new IfExpression();
            var x = new Expression();
            var g = new Tree<Glyph>();
            IMaterializer m = new testm();
          
            m.Materialize(ifexpression);
            m.Materialize(x);
           
        }
        
      
    }
}
