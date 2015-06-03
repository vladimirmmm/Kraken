using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LogicalModel.Validation;


namespace LogicalModel.Expressions
{

    public class CSharpParser:Parser
    {
        public CSharpParser() 
        {
            this.Syntax = new LanguageSyntax();

            this.Syntax.ExpressionContainer_Left = "(";
            this.Syntax.ExpressionContainer_Right = ")";
            this.Syntax.BlockContainer_Left = "{";
            this.Syntax.BlockContainer_Right = "}";
            this.Syntax.ParameterSeparator = ",";
            this.Syntax.CodeItemSeparator = " ";
            this.Syntax.StringDelimiter = "\"";
            this.Syntax.If = "if";
            this.Syntax.Then = "";
            this.Syntax.Else = "else";
            this.Syntax.ParameterSpecifier = "p_"; //"$";
            this.Syntax.ExpressionPlaceholder = "#";
            this.Syntax.StatementEnd = ";";
            this.Syntax.CaseSensitive = true;


            this.Syntax.Operators.AddItem(OperatorEnum.Unknown, " !!unknown!! ");
            this.Syntax.Operators.AddItem(OperatorEnum.Addition, " + ");
            this.Syntax.Operators.AddItem(OperatorEnum.And, " & ");
            this.Syntax.Operators.AddItem(OperatorEnum.AndAlso, " && ");
            this.Syntax.Operators.AddItem(OperatorEnum.Division, " / ");
            this.Syntax.Operators.AddItem(OperatorEnum.Equals, " == ");
            this.Syntax.Operators.AddItem(OperatorEnum.Greater, " > ");
            this.Syntax.Operators.AddItem(OperatorEnum.GreaterOrEqual, " >= ");
            this.Syntax.Operators.AddItem(OperatorEnum.IntegerDivision, " / ");
            this.Syntax.Operators.AddItem(OperatorEnum.Less, " < ");
            this.Syntax.Operators.AddItem(OperatorEnum.LessOrEqual, " <= ");
            this.Syntax.Operators.AddItem(OperatorEnum.Modulo, " \\ ");
            this.Syntax.Operators.AddItem(OperatorEnum.Multiplication, " * ");
            this.Syntax.Operators.AddItem(OperatorEnum.Not, "!");
            this.Syntax.Operators.AddItem(OperatorEnum.NotEquals, "!=");
            this.Syntax.Operators.AddItem(OperatorEnum.Or, " | ");
            this.Syntax.Operators.AddItem(OperatorEnum.OrAlso, " || ");
            this.Syntax.Operators.AddItem(OperatorEnum.Subtraction, " - ");

            this.Syntax.AddFunction("iaf:numeric-equal", (Functions i) => i.N_Equals(1, 1));
            this.Syntax.AddFunction("iaf:numeric-less-than", (Functions i) => i.N_Less(1, 1));
            this.Syntax.AddFunction("iaf:numeric-less-equal-than", (Functions i) => i.N_LessEqual(1,1));
            this.Syntax.AddFunction("iaf:numeric-greater-than", (Functions i) => i.N_Greater(1,1));
            this.Syntax.AddFunction("iaf:numeric-greater-equal-than", (Functions i) => i.N_GreaterEqual(1,1));
            this.Syntax.AddFunction("iaf:numeric-equal-treshold", (Functions i) => i.N_Equals_Treshold(1,1));
            this.Syntax.AddFunction("iaf:numeric-less-than-treshold", (Functions i) => i.N_Less_Treshold(1,1));
            this.Syntax.AddFunction("iaf:numeric-less-equal-than-treshold", (Functions i) => i.N_LessEqual_Treshold(1,1));
            this.Syntax.AddFunction("iaf:numeric-greater-than-treshold",(Functions i) => i.N_Greater_Treshold(1,1));
            this.Syntax.AddFunction("iaf:numeric-greater-equal-than-treshold", (Functions i) => i.N_GreaterEqual_Treshold(1,1));
            this.Syntax.AddFunction("iaf:numeric-subtract", (Functions i) => i.N_Subtract(1,1));
            this.Syntax.AddFunction("iaf:numeric-divide", (Functions i) => i.N_Divide(1,1));
            this.Syntax.AddFunction("iaf:numeric-multiply", (Functions i) => i.N_Multiply(1,1));
            this.Syntax.AddFunction("iaf:numeric-unary-minus", (Functions i) => i.N_Unary_Minus(1));
            this.Syntax.AddFunction("iaf:numeric-unary-plus", (Functions i) => i.N_Unary_Plus(1));
            this.Syntax.AddFunction("xs:qname", (Functions i) => i.XS_QName(""));
            this.Syntax.AddFunction("not", (Functions i) => i.not(true));
            this.Syntax.AddFunction("empty", (Functions i) => i.empty(null));
            this.Syntax.AddFunction("iaf:abs", (Functions i) => i.abs(0));
            this.Syntax.AddFunction("iaf:sum", (Functions i) => i.sum(0));
            this.Syntax.AddFunction("iaf:max", (Functions i) => i.max(0));
        }

        public override String Translate(Expression expr) 
        {
            if (typeof(Expression) == expr.GetType())
            {
                if (expr.Operators.Count>0 && expr.Operators[0] == OperatorEnum.Equals && expr.SubExpressions[1] is FunctionExpression)
                {
                    var functionexpression = expr.SubExpressions[1] as FunctionExpression;
                    if (String.IsNullOrEmpty(functionexpression.Name))
                    {
                        return Translate(expr.SubExpressions[0]) + ".In"+Syntax.ExpressionContainer_Left + Translate(expr.SubExpressions[1])+Syntax.ExpressionContainer_Right;
                    }
                }
            }
            return base.Translate(expr);
        }

        public override String Translate(FunctionExpression expression)
        {
            if (expression.Name.In("true", "false"))
            {
                return expression.Name;
            }
            else
            {
                var sb = new StringBuilder();
                var fname = expression.Name;
                if (String.IsNullOrEmpty(expression.Name))
                {
                    sb.Append(Translate(expression as ListExpression));

                }
                else
                {
                    if (Syntax.FunctionMap.ContainsKey(fname.ToLower()))
                    {
                        fname = Syntax.FunctionMap[fname.ToLower()];
                    }
                    sb.Append(String.Format("{0}({1})", fname, Translate(expression as ListExpression)));
                }

                return sb.ToString();
            }
        }
        
        public override String Translate(IfExpression expr)
        {
            var format = "{0} ? {1} : {2}";
            return string.Format(format, Translate(expr.condition), Translate(expr.truepath), Translate(expr.falsepath));
        }

        public override string GetFunction(ValidationRule rule)
        {
            var sb = new StringBuilder();
            var tab = "   ";
            sb.AppendFormat(tab + tab + "public bool {0}(List<ValidationParameter> parameters)\r\n", rule.FunctionName);
            sb.AppendLine(tab + tab + Syntax.BlockContainer_Left);
            var body = Translate(rule.RootExpression);
            //var parameters = rule.RootExpression.ChildExpressions();
            var parameters = rule.RootExpression.ChildExpressions();
            foreach (var pm in rule.Parameters) 
            {

                sb.AppendFormat("{0}{0}{0}var {3}{1} = parameters.FirstOrDefault(i => i.Name == \"{1}\").{2};\r\n", tab, pm.Name, pm.TypeString,Syntax.ParameterSpecifier);
            }

            sb.AppendLine(tab + tab + tab + "return " + body + Syntax.StatementEnd);
            sb.AppendLine(tab + tab + Syntax.BlockContainer_Right);

            return sb.ToString();
        }

        public void Test()
        {
   
        }
    }
}
