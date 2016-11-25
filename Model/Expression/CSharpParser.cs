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
            this.Syntax.StringDelimiter2 = "\"";
            this.Syntax.Spacing = " ";
            this.Syntax.If = "if";
            this.Syntax.Then = "";
            this.Syntax.Else = "else";
            this.Syntax.ParameterSpecifier = "p_"; //"$";
            this.Syntax.ExpressionPlaceholder = "#";
            this.Syntax.StatementEnd = ";";
            this.Syntax.CaseSensitive = true;


            this.Syntax.Operators.AddItem(OperatorEnum.Unknown, " !!unknown!! ");

            this.Syntax.Operators.AddItem(OperatorEnum.Cast, " as ");
            this.Syntax.Operators.AddItem(OperatorEnum.Addition, " + ");
            this.Syntax.Operators.AddItem(OperatorEnum.And, " & ");
            this.Syntax.Operators.AddItem(OperatorEnum.AndAlso, " && ");
            this.Syntax.Operators.AddItem(OperatorEnum.Division, " / ");

            this.Syntax.Operators.AddItem(OperatorEnum.Equals, " == ");
            this.Syntax.Operators.AddItem(OperatorEnum.GreaterOrEqual, " >= ");
            this.Syntax.Operators.AddItem(OperatorEnum.LessOrEqual, " <= ");
            this.Syntax.Operators.AddItem(OperatorEnum.Greater, " > ");
            this.Syntax.Operators.AddItem(OperatorEnum.Less, " < ");
            this.Syntax.Operators.AddItem(OperatorEnum.NotEquals, "!=");


            this.Syntax.Operators.AddItem(OperatorEnum.VEquals, " == ");
            this.Syntax.Operators.AddItem(OperatorEnum.VGreaterOrEqual, " >= ");
            this.Syntax.Operators.AddItem(OperatorEnum.VLessOrEqual, " <= ");
            this.Syntax.Operators.AddItem(OperatorEnum.VGreater, " > ");
            this.Syntax.Operators.AddItem(OperatorEnum.VLess, " < ");
            this.Syntax.Operators.AddItem(OperatorEnum.VNotEquals, "!=");


            this.Syntax.Operators.AddItem(OperatorEnum.IntegerDivision, " / ");
            this.Syntax.Operators.AddItem(OperatorEnum.Modulo, " \\ ");
            this.Syntax.Operators.AddItem(OperatorEnum.Multiplication, " * ");
            this.Syntax.Operators.AddItem(OperatorEnum.Not, "!");
            this.Syntax.Operators.AddItem(OperatorEnum.Or, " | ");
            this.Syntax.Operators.AddItem(OperatorEnum.OrAlso, " || ");
            this.Syntax.Operators.AddItem(OperatorEnum.Subtraction, " - ");

            this.Syntax.AddFunction("iaf:numeric-add", (Functions i) => i.IAF_N_Add(null, null));
            this.Syntax.AddFunction("iaf:numeric-equal", (Functions i) => i.IAF_N_Equals(1, 1));
            this.Syntax.AddFunction("iaf:numeric-less-than", (Functions i) => i.IAF_N_Less(1, 1));
            this.Syntax.AddFunction("iaf:numeric-less-equal-than", (Functions i) => i.IAF_N_LessEqual(1, 1));
            this.Syntax.AddFunction("iaf:numeric-greater-than", (Functions i) => i.IAF_N_Greater(1, 1));
            this.Syntax.AddFunction("iaf:numeric-greater-equal-than", (Functions i) => i.IAF_N_GreaterEqual(1, 1));
            this.Syntax.AddFunction("iaf:numeric-equal-treshold", (Functions i) => i.IAF_N_Equals_Treshold(1, 1));
            this.Syntax.AddFunction("iaf:numeric-less-than-treshold", (Functions i) => i.IAF_N_Less_Treshold(1, 1));
            this.Syntax.AddFunction("iaf:numeric-less-equal-than-treshold", (Functions i) => i.IAF_N_LessEqual_Treshold(1, 1));
            this.Syntax.AddFunction("iaf:numeric-greater-than-treshold", (Functions i) => i.IAF_N_Greater_Treshold(1, 1));
            this.Syntax.AddFunction("iaf:numeric-greater-equal-than-treshold", (Functions i) => i.IAF_N_GreaterEqual_Treshold(1, 1));
            this.Syntax.AddFunction("iaf:numeric-subtract", (Functions i) => i.IAF_N_Subtract(1, 1));
            this.Syntax.AddFunction("iaf:numeric-divide", (Functions i) => i.IAF_N_Divide(1, 1));
            this.Syntax.AddFunction("iaf:numeric-multiply", (Functions i) => i.IAF_N_Multiply(1, 1));
            this.Syntax.AddFunction("iaf:numeric-unary-minus", (Functions i) => i.IAF_N_Unary_Minus(1));
            this.Syntax.AddFunction("iaf:numeric-unary-plus", (Functions i) => i.IAF_N_Unary_Plus(1));
            this.Syntax.AddFunction("iaf:abs", (Functions i) => i.IAF_abs(0));
            this.Syntax.AddFunction("iaf:sum", (Functions i) => i.IAF_sum(0));
            this.Syntax.AddFunction("iaf:max", (Functions i) => i.IAF_max(0));
            this.Syntax.AddFunction("iaf:min", (Functions i) => i.IAF_min(0));

            this.Syntax.AddFunction("xfi:fact-typed-dimension-value", (Functions i) => i.XFI_Fact_Typed_Dimension_Value(null, ""));
            this.Syntax.AddFunction("xfi:fact-explicit-dimension-value", (Functions i) => i.XFI_Fact_Explicit_Dimension_Value(null, ""));
            this.Syntax.AddFunction("xfi:period", (Functions i) => i.XFI_Period(null));
            this.Syntax.AddFunction("xfi:period-instant", (Functions i) => i.XFI_Period_Instant(null));
            this.Syntax.AddFunction("xfi:entity", (Functions i) => i.XFI_Entity(null));
            this.Syntax.AddFunction("xfi:entity-identifier", (Functions i) => i.XFI_Entity_Identifier(null));
            this.Syntax.AddFunction("count", (Functions i) => i.Count(null));


            this.Syntax.AddFunction("xs:qname", (Functions i) => i.XS_QName(""));
            this.Syntax.AddFunction("xs:string", (Functions i) => i.XS_String(""));
            this.Syntax.AddFunction("xs:integer", (Functions i) => i.XS_Integer(""));
            this.Syntax.AddFunction("xs:date", (Functions i) => i.XS_Date(""));
            this.Syntax.AddFunction("qname", (Functions i) => i.QName("",""));
            this.Syntax.AddFunction("string", (Functions i) => i.String(""));
            this.Syntax.AddFunction("xs:boolean", (Functions i) => i.XS_Boolean(""));
            this.Syntax.AddFunction("matches", (Functions i) => i.RegexpMatches("",""));
            this.Syntax.AddFunction("not", (Functions i) => i.not(true));
            this.Syntax.AddFunction("nilled", (Functions i) => i.nilled(""));
            this.Syntax.AddFunction("empty", (Functions i) => i.empty(null));
            this.Syntax.AddFunction("exists", (Functions i) => i.Exists(null));

            this.Syntax.AddFunction("abs", (Functions i) => i.abs(0));
            this.Syntax.AddFunction("floor", (Functions i) => i.floor(0));
            this.Syntax.AddFunction("number", (Functions i) => i.Number(""));
            this.Syntax.AddFunction("sum", (Functions i) => i.sum(0));
            this.Syntax.AddFunction("max", (Functions i) => i.max(0));

            this.Syntax.AddFunction("concat", (Functions i) => string.Concat(new string[] { }));
   
            this.Syntax.AddFunction("day-from-dateTime", (Functions i) => i.Day(DateTime.Now));
            this.Syntax.AddFunction("month-from-dateTime", (Functions i) => i.Month(DateTime.Now));
            this.Syntax.AddFunction("year-from-dateTime", (Functions i) => i.Year(DateTime.Now));
            this.Syntax.AddFunction("day-from-date", (Functions i) => i.Day(DateTime.Now));
            this.Syntax.AddFunction("month-from-date", (Functions i) => i.Month(DateTime.Now));
            this.Syntax.AddFunction("year-from-date", (Functions i) => i.Year(DateTime.Now));
            this.Syntax.AddFunction("string-length", (Functions i) => i.StringLength(""));
            this.Syntax.AddFunction("substring", (Functions i) => i.Substring("", 0, 0));
            this.Syntax.AddFunction("translate", (Functions i) => i.Translate("", "", ""));
        }

        public override String Translate(Expression expr) 
        {
            if (typeof(Expression) == expr.GetType())
            {
                if (expr.SubExpressions.Count==2 && expr.Operators.Count > 0 && expr.Operators[0] == OperatorEnum.Equals && expr.SubExpressions[1] is FunctionExpression)
                {
                    var functionexpression = expr.SubExpressions[1] as FunctionExpression;
                    if (String.IsNullOrEmpty(functionexpression.Name))
                    {
                        return Translate(expr.SubExpressions[0]) + ".In"+Syntax.ExpressionContainer_Left + Translate(expr.SubExpressions[1])+Syntax.ExpressionContainer_Right;
                    }
                }

                if (expr.SubExpressions.Count == 0) 
                {
                    if (!expr.IsParameter && !expr.IsString && Utilities.Strings.IsDigitsOnly(expr.StringValue, '.', '-')) 
                    {
                        //this is for decimal;
                        //if (String.IsNullOrEmpty(expr.StringValue)) 
                        //{ 

                        //}
                        return expr.StringValue + "m";
                    }
                }
                if (expr.IsString)
                {
                    return this.Syntax.StringDelimiter + expr.StringValue.Replace("\\","\\\\") + Syntax.StringDelimiter;
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
                    else 
                    {
                        fname = String.Format("<<{0}: {1}>>", FunctionNotFound, expression.Name);

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

                //sb.AppendFormat("{0}{0}{0}var {3}{1} = parameters.FirstOrDefault(i => i.Name == \"{1}\").{2};\r\n", tab, pm.Name, pm.TypeString, Syntax.ParameterSpecifier);
                sb.AppendFormat("{0}{0}{0}var {3}{1} = parameters.FirstOrDefault(i => i.Name == \"{1}\");\r\n", tab, pm.Name, pm.TypeString, Syntax.ParameterSpecifier);
            }
            if (body.Contains(Parser.FunctionNotFound)) 
            {
                Logger.WriteLine(String.Format("Rule {0} contains unimplemented function(s) {1}!", rule.ID, body.TextBetween("<<" + Parser.FunctionNotFound, ">>")));
                body = "true";
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
