using System;
namespace CodeModel.Model
{
    interface ISyntax
    {
        void Add();
        Operator Addition { get; }
        Operator And { get; }
        Operator AndAlso { get; }
        Boundary ArrayBoundary { get; }
        Boundary BlockBoundary { get; }
        Separator BlockSeparator { get; }
        System.Collections.Generic.Dictionary<string, string> Boundaries { get; set; }
        KeyWord Case { get; }
        Operator Cast { get; }
        Operator Division { get; }
        KeyWord Else { get; }
        Separator EnumerationSeparator { get; }
        Operator Equals { get; }
        KeyWord For { get; }
        KeyWord ForEach { get; }
        Expression GetExpression(string expressionstring);
        Separator GlyphSeparator { get; }
        Operator Greater { get; }
        Operator GreaterOrEqual { get; }
        KeyWord If { get; }
        System.Collections.Generic.Dictionary<string, string> Implementations { get; set; }
        KeyWord In { get; }
        Operator IntegerDivision { get; }
        System.Collections.Generic.Dictionary<string, string> Keywords { get; set; }
        Operator Less { get; }
        Operator LessOrEqual { get; }
        Separator Literal2Separator { get; }
        Separator LiteralSeparator { get; }
        void Load();
        void Load(string syntaxjson);
        Operator Modulo { get; }
        Operator Multiplication { get; }
        Operator Not { get; }
        Operator NotEquals { get; }
        System.Collections.Generic.Dictionary<string, string> Operators { get; set; }
        Operator Or { get; }
        Operator OrAlso { get; }
        Boundary ParameterBoundary { get; }
        KeyWord Return { get; }
        System.Collections.Generic.Dictionary<string, string> Separators { get; set; }
        Operator Subtraction { get; }
        KeyWord Switch { get; }
        KeyWord Then { get; }
        string Translate(Expression expr);
        Operator VEquals { get; }
        Operator VGreater { get; }
        Operator VGreaterOrEqual { get; }
        Operator VLess { get; }
        Operator VLessOrEqual { get; }
        Operator VNotEquals { get; }
        KeyWord While { get; }
    }
}
