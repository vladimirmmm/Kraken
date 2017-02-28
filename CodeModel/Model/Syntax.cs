using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeModel.Model
{
    public class Syntax2 
    {
        private Dictionary<string, string> _Operators = new Dictionary<string, string>();
        public Dictionary<string, string> Operators { get { return _Operators; } set { _Operators = value; } }

        private Dictionary<string, string> _Keywords = new Dictionary<string, string>();
        public Dictionary<string, string> Keywords { get { return _Keywords; } set { _Keywords = value; } }

        private Dictionary<string, Boundary> _Boundaries = new Dictionary<string, Boundary>();
        public Dictionary<string, Boundary> Boundaries { get { return _Boundaries; } set { _Boundaries = value; } }

        private Dictionary<string, string> _Separators = new Dictionary<string, string>();
        public Dictionary<string, string> Separators { get { return _Separators; } set { _Separators = value; } }

        private Dictionary<string, string> _Implementations = new Dictionary<string, string>();
        public Dictionary<string, string> Implementations { get { return _Implementations; } set { _Implementations = value; } }



        public virtual void Load()
        {

        }
        public Syntax2()
        {
            Load();
        }

        public void Load(string syntaxjson)
        {
            Utilities.Converters.JsonToObject<Syntax2>(syntaxjson, this);
        }


    }

    //public class Syntax
    //{
    //    private Dictionary<string, Operator> _Operators = new Dictionary<string, Operator>();
    //    public Dictionary<string, Operator> Operators { get { return _Operators; } set { _Operators = value; } }

    //    private Dictionary<string, KeyWord> _Keywords = new Dictionary<string, KeyWord>();
    //    public Dictionary<string, KeyWord> Keywords { get { return _Keywords; } set { _Keywords = value; } }

    //    private Dictionary<string, Boundary> _Boundaries = new Dictionary<string, Boundary>();
    //    public Dictionary<string, Boundary> Boundaries { get { return _Boundaries; } set { _Boundaries = value; } }

    //    private Dictionary<string, Separator> _Separators = new Dictionary<string, Separator>();
    //    public Dictionary<string, Separator> Separators { get { return _Separators; } set { _Separators = value; } }
        
    //    private Dictionary<string, string> _Implementations = new Dictionary<string, string>();
    //    public Dictionary<string, string> Implementations { get { return _Implementations; } set { _Implementations = value; } }

    //    //public List<Operator> OperatorList = new List<Operator>();
    //    //public List<KeyWord> KeyWordList = new List<KeyWord>();
    //    //public List<Boundary> BoundaryList = new List<Boundary>();
    //    //public List<Separator> SeparatorList = new List<Separator>();

    //    public Operator Cast;
    //    public Operator Addition;
    //    public Operator Subtraction;
    //    public Operator IntegerDivision;
    //    public Operator Division;
    //    public Operator Modulo;
    //    public Operator Multiplication;
    //    public Operator And;
    //    public Operator AndAlso;
    //    public Operator Not;
    //    public Operator Or;
    //    public Operator OrAlso;
    //    public Operator GreaterOrEqual;
    //    public Operator LessOrEqual;
    //    public Operator NotEquals;
    //    public Operator Equals;
    //    public Operator Greater;
    //    public Operator Less;
    //    public Operator VGreaterOrEqual;
    //    public Operator VLessOrEqual;
    //    public Operator VNotEquals;
    //    public Operator VEquals;
    //    public Operator VGreater;
    //    public Operator VLess;

    //    public KeyWord If;
    //    public KeyWord Then;
    //    public KeyWord Else;
    //    public KeyWord Case;
    //    public KeyWord Switch;
    //    public KeyWord Return;
    //    public KeyWord For;
    //    public KeyWord ForEach;
    //    public KeyWord While;
    //    public KeyWord In;

    //    public Boundary BlockBoundary;
    //    public Boundary ArrayBoundary;
    //    public Boundary ParameterBoundary;

    //    public Separator LiteralSeparator;
    //    public Separator Literal2Separator;
    //    public Separator EnumerationSeparator;
    //    public Separator GlyphSeparator;
    //    public Separator BlockSeparator;

    //    public virtual void Load() 
    //    {

    //    }
    //    public Syntax() 
    //    {
    //        Load();
    //    }

    //    public void Load(string syntaxjson) 
    //    {

    //    }

    //    public void Add() 
    //    {

    //    }

    //    public virtual Expression GetExpression(string expressionstring) 
    //    {
    //        return null;
    //    }
    //    public virtual string Translate(Expression expr) 
    //    {
    //        return null;

    //    }
    //}


}
