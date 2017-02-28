using CodeModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CodeModel
{
    public class XPathParser : CodeParser
    {
        public XPathParser(Syntax2 syntax) :base(syntax)
        {

        }
        public override Expression Specialize(Expression expression)
        {
            //extract enumerations
            var expressions = expression.Where(i=>i.Parent!=null);
            expressions = expression.Where(i=> Objects.IsOfType<Expression>( i) && i.Parent!=null).ToList();
            foreach (var expressionitem in expressions) 
            {
                if (expressionitem.Children.Count > 1 && expressionitem.Children[1].Item is Separator) 
                {
                    var li = new ListExpression();

                    Expression.Exchange(expressionitem,li);
                }
            }
            //extract if then else
            expressions = expression.Where(i => i.Item is KeyWord && i.Parent != null).ToList();
            foreach (var expressionitem in expressions)
            {

                if (expressionitem.Item.Value == "if")
                {
                    var ifcontainer = expressionitem.Parent;

                    var if_condition = expressionitem;
                    var if_condition_g = if_condition.Item;
                    var if_condition_ix = ifcontainer.Children.IndexOf(if_condition)+1;

                    var if_then = ifcontainer.Children.FirstOrDefault(g => g.Item is KeyWord && Glyph.HasValue(g.Item, "then"));
                    var if_then_g = if_then.Item;
                    var if_then_ix = ifcontainer.Children.IndexOf(if_then)+1;

                    var if_else = ifcontainer.Children.FirstOrDefault(g => Glyph.HasValue(g.Item, "else"));
                    Glyph if_else_g = if_else != null ? if_else.Item : null;
                    var if_else_ix = if_else != null ? ifcontainer.Children.IndexOf(if_else) + 1 : ifcontainer.Children.Count;


                    Tree<Glyph> Condition = null;
                    Tree<Glyph> TrueBranch = null;
                    Tree<Glyph> FalseBranch = null;

                    var ifexpr = new IfExpression();
                    if (ifcontainer.Parent == null)
                    {
                        expression = ifexpr;
                    }
                    Expression.Exchange(ifcontainer, ifexpr);


                    //handling the condition part
                    var conditions = ifexpr.Children.Skip(if_condition_ix).Take(if_then_ix - if_condition_ix-1).ToList();
                    if (conditions.Count > 0) 
                    {
                        Condition = conditions.FirstOrDefault();
                    }
                    var if_condition_expr = new FunctionExpression();
                    Expression.Exchange(Condition, if_condition_expr);


                    //handling the then part
                    var trues = ifexpr.Children.Skip(if_then_ix).Take(if_else_ix - if_then_ix-1).ToList();
                    if (trues.Count > 0)
                    {
                        TrueBranch = trues.FirstOrDefault();
                    }
                    var if_then_expr = new FunctionExpression();
                    Expression.Exchange(TrueBranch, if_then_expr);

                    //handling the else part
                    if (if_else != null)
                    {
                        var falses = ifexpr.Children.Skip(if_else_ix).ToList();
                        if (falses.Count > 0)
                        {
                            FalseBranch = falses.FirstOrDefault();
                        }
                        var if_else_expr = new FunctionExpression();
                        Expression.Exchange(FalseBranch, if_else_expr);
                    }
                }

            }
            //extract functions
            expressions = expression.Where(i => Objects.IsOfType<Tree<Glyph>>(i) && i.Parent != null).ToList();
            foreach (var expressionitem in expressions)
            {
                if (expressionitem.Item is FunctionName)
                {
                    var parent = expressionitem.Parent;
                    var fi = new FunctionExpression();
                    var listexpression = parent.Children[parent.Children.IndexOf(expressionitem) + 1];
                    //fi.Parameters = listexpression is ListExpression ? listexpression as ListExpression : null;
                    fi.Parameters = listexpression as Expression;// is ListExpression ? listexpression as ListExpression : null;
                    
                    Expression.Exchange(expressionitem, fi);
                }
            }

            return expression;
        }
        
        public override Expression ConvertToExpressionTree(List<Glyph> glyphs)
        {
            var result = new Expression();
        
            Expression expressioncontainer = result;
            var i=0;

            while (i < glyphs.Count) 
            {
                var glyph = glyphs[i];
                if (glyph.GetType() == typeof(BoundaryInstance))
                {
                    var boundaryinstance = (BoundaryInstance)glyph;

                    var l_ix = glyphs.IndexOf(boundaryinstance) + 1;
                    var r_ix = glyphs.IndexOf(boundaryinstance, l_ix);
                    var c = r_ix-l_ix;
                    var sublist = glyphs.Skip(l_ix).Take(c).ToList();
                    var expr = ConvertToExpressionTree(sublist);
                    expressioncontainer.Add(expr);
                    i = r_ix;
                }
                else 
                {
                    expressioncontainer.Add(glyph);
                }
                i++;
            }
            var logicalseparators = result.Where(g => Glyph.HasName(g.Item,"Logical")).ToList();
            foreach (var t in logicalseparators) 
            {
                if (t.Item.Name == "Logical") 
                {
                    if (t.Parent != null) 
                    {
                        t.Parent.Remove(t);
                    }
                }
            }
            return result;
        }

        public override List<Glyph> ConvertToGlyphs(string item)
        {

            var glyph = new Glyph(item);
            var glyphs = new List<Glyph>() { glyph };
            glyphs = ProcessLiterals(glyphs);
            glyphs = ProcessBoundaries(glyphs);
            glyphs = ProcessSeparators(glyphs);
            glyphs = ProcessFunctions(glyphs);
            glyphs = ProcessKeywords(glyphs);
            glyphs = ProcessOperators(glyphs);
            glyphs = ProcessOperators(glyphs);
            glyphs = ProcessBoundaryInstances(glyphs);
            glyphs = ProcessSymbols(glyphs);
            return glyphs;
        }

        public override Expression GetExpression(string expressionstring)
        {
            var glyphs = this.ConvertToGlyphs(expressionstring);
            var basicexpression = this.ConvertToExpressionTree(glyphs);
            var generalizedexpression = this.Specialize(basicexpression);
            return generalizedexpression;
        }

        public List<Glyph> ProcessLiterals(List<Glyph> glyphs)
        {
            List<Glyph> result = glyphs;
            var sep = GlyphFactory.Create<Separator>(Syntax.Separators["Literal"]);
            result = ProcessLiteral(sep, result);
            sep = GlyphFactory.Create<Separator>(Syntax.Separators["Literal2"]);
            result = ProcessLiteral(sep, result);
            return result;
        }
        public List<Glyph> ProcessSeparators(List<Glyph> glyphs)
        {
            List<Glyph> result = glyphs;
            var s_enum = GlyphFactory.Create<Separator>(Syntax.Separators, "Enumeration");
            var s_logic = GlyphFactory.Create<Separator>(Syntax.Separators, "Logical");
            result = ProcessGlyph(s_enum, result);
            result = ProcessGlyph(s_logic, result);
            return result;
        }
        public List<Glyph> ProcessBoundaries( List<Glyph> glyphs)
        {
            List<Glyph> result = glyphs;
            result = ProcessBoundary(Syntax.Boundaries["Block"], result);
            result = ProcessBoundary(Syntax.Boundaries["Enumeration"], result);
            //result = ProcessBoundary(this.Boundaries["Array"], result);
            return result;

        }
        public List<Glyph> ProcessFunctions(List<Glyph> glyphs)
        {
            List<Glyph> result = glyphs;
            var enumeration_left = Syntax.Boundaries["Enumeration"].Left;
            for(int i=1;i<glyphs.Count;i++)
            {
                var glyph = glyphs[i];
                if (glyph.GetType()==typeof(BoundarySeparator))
                {
                    if (glyph.Value == enumeration_left)
                    {
                        var prevglyph = glyphs[i - 1];
                        if (prevglyph.GetType() == typeof(Glyph) && !String.IsNullOrEmpty(prevglyph.Value))
                        {
                            glyphs[i - 1] = GlyphFactory.Create<FunctionName>(prevglyph.Value);
                        }
                    }

                }
            }
      
            return result;
        }
        public List<Glyph> ProcessKeywords(List<Glyph> glyphs)
        {
            List<Glyph> result = glyphs;
            foreach (var kv in Syntax.Keywords) 
            {
                var glyph = GlyphFactory.Create<KeyWord>(Syntax.Keywords, kv.Key);
                result = ProcessGlyph(glyph, result);
            }
            return result;
        }
        public List<Glyph> ProcessOperators(List<Glyph> glyphs)
        {
            List<Glyph> result = glyphs;
            foreach (var kv in Syntax.Operators)
            {
                var glyph = GlyphFactory.Create<Operator>(Syntax.Operators, kv.Key);
                result = ProcessGlyph(glyph, result);
            }
            return result;
        }
        //Regex.IsMatch(text, "\\bthe\\b", RegexOptions.IgnoreCase);

        public List<Glyph> ProcessBoundary(Boundary boundary, List<Glyph> glyphs)
        {
            List<Glyph> result = glyphs;

            var s_l = GlyphFactory.CreateBoundarySeparator(boundary,true);
            var s_r = GlyphFactory.CreateBoundarySeparator(boundary,false);
            result = ProcessGlyph(s_l, result);
            result = ProcessGlyph(s_r, result);
            //create boundary instances
            return result;
        }
        public List<Glyph> ProcessSymbols(List<Glyph> glyphs)
        {
            var result = new List<Glyph>();
            foreach (var glyph in glyphs) 
            {
                if (glyph.Value.StartsWith("$") && glyph.GetType()!=typeof(Literal))
                {
                    var symbol = GlyphFactory.Create<Symbol>(glyph.Value);
                    result.Add(symbol);
                }
                else 
                {
                    result.Add(glyph);
                }
            }
            return result;
        }
        //Regex.IsMatch(text, "\\bthe\\b", RegexOptions.IgnoreCase);

 
        public List<Glyph> ProcessBoundaryInstances(List<Glyph> glyphs)
        {
            List<Glyph> result = glyphs;
            var boundarydict = new Dictionary<string,List<BoundaryInstance>>();
            var ix = 0;
            for (int i = 0; i < glyphs.Count; i++) 
            {
                var glyph = glyphs[i];
                var glyphtype = glyph.GetType();
                if (typeof(BoundarySeparator).IsAssignableFrom(glyphtype))
                {
                    var separator = (BoundarySeparator)glyph;
                    if (!boundarydict.ContainsKey(glyph.Name))
                    {
                        boundarydict.Add(glyph.Name, new List<BoundaryInstance>());
                    }
                    var separatorlist = boundarydict[glyph.Name];
                    if (separator.IsLeft)
                    {
                        var boundaryinstance = new BoundaryInstance();
                        glyphs[i] = boundaryinstance;
                        boundaryinstance.Left = separator;
                        separatorlist.Add(boundaryinstance);
                    }
                    else
                    {
                        var boundaryinstance = separatorlist.LastOrDefault(s => s.Right == null);
                        glyphs[i] = boundaryinstance;
                        boundaryinstance.Right = separator;
                    }
                }
            }
      
            return result;
        }

        public List<Glyph> ProcessGlyph(Glyph g, List<Glyph> glyphs) 
        {
            var result = new List<Glyph>();
            var gstr=g.Value;
            foreach (var glyph in glyphs)
            {
                if (glyph.GetType() == typeof(Glyph))
                {
                    var content = glyph.Value;

                    var items = content.Split(new string[] { gstr }, StringSplitOptions.None);
                    for (int i = 0; i < items.Length;i++ )
                    {
                        var itemglyph = GlyphFactory.Create<Glyph>(items[i]);
                        AddGlyph(itemglyph,result);
                        if (i < items.Length - 1) 
                        {
                            result.Add(g);
                        }
                    }
                }
                else 
                {
                    result.Add(glyph);
                }
            }
            return result;
        }
        public List<Glyph> ProcessLiteral(Separator sep, List<Glyph> glyphs) 
        {
            var result = new List<Glyph>();
            var separator = sep.Value;
            foreach (var glyph in glyphs) 
            {
                if (glyph.GetType() == typeof(Glyph))
                {
                    var content = glyph.Value;

                    //TODO handle escaped separators
                    var x1 = 0;
                    var literals = Utilities.Strings.TextsBetween(content, separator, separator);
                    var six = 0;
                    foreach (var literal in literals)
                    {
                        var fp = separator + literal + separator;
                        var ix = content.IndexOf(fp, six);
                        var l = ix - six;
                        var gstr = content.Substring(six, l);
                        if (!String.IsNullOrEmpty(gstr))
                        {
                            result.Add(new Glyph(gstr));
                        }
                        result.Add(GlyphFactory.Create<Literal>(literal));
                        six = ix + fp.Length;
                    }
                    if (content.Length - six > 0)
                    {
                        var gstr = content.Substring(six);
                        result.Add(new Glyph(gstr));

                    }
                }
                else 
                {
                    result.Add(glyph);
                }

            }
            return result;
        }

        public void AddGlyph(Glyph g, List<Glyph> glyphs) 
        {
            if (g.GetType() == typeof(Glyph))
            {

            }
            if (!String.IsNullOrEmpty(g.Value)) 
            {
                glyphs.Add(g);
            }
        }
    }
}
