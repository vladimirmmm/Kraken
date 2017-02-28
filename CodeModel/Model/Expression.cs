using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CodeModel.Model
{
   
    public class Expression:Tree<Glyph>,IMaterializable
    {

        //public static void Exchange(Tree<Glyph> expressionitem, Expression li)
        public static void Exchange(Tree<Glyph> expressionitem, Expression li)
        {
            var parent = expressionitem.Parent;
            if (parent != null) 
            {
                parent.Children.Remove(expressionitem);
                parent.Add(li);
            }
            foreach (var child in expressionitem.Children) 
            {
                li.Add(child);
            }
            expressionitem.Parent=null;
            expressionitem.Children.Clear();
        }
        public virtual string Materialize(Materializer m,Language language) 
        {
            return language.Materializer.Materialize(this);
            //return m.Materialize(this);
        }
        //public new IEnumerator<Expression> GetEnumerator()
        //{
        //    yield return this;
        //    foreach (var h in Children)
        //    {
        //        yield return h;
        //    }
        //}

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    return this.GetEnumerator();

        //}

        public override string ToString()
        {
            return String.Format("{0}", this.GetType().Name);
        }
    }
    public class LiteralExpression : Expression
    {

    }
    public class ListExpression : Expression 
    {

    }
    public class FunctionExpression : Expression
    {
        public string Name = "";
        public Expression Parameters = null;
    }
    public class IfExpression : Expression 
    {
        public FunctionExpression Condition { get; set; }
        public FunctionExpression True { get; set; }
        public FunctionExpression False { get; set; }
    }
    public class LoopExpression : FunctionExpression
    {
        public Expression Action = null;
        public Func<Boolean> Condition = () => false;
    }
    public class ForExpression : LoopExpression
    {
        public string VariableName = "";
        public Action<Object> AfterAction = (o) => { };
    }
    public class QueryExpression : ForExpression
    {
 
    }
}
