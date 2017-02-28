using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CodeModel.Model
{
    public class GlyphFactory 
    {
        public static Dictionary<string, Dictionary<string, Glyph>> Dictionaries = new Dictionary<string, Dictionary<string, Glyph>>();
        public static  T Create<T>(string Content) where T : Glyph, new()
        {
            var r = new T();
            r.Value = Content;
 
            return r;
        }
        public static T Create<T>(Dictionary<string,string> dict,string key) where T : Glyph, new()
        {
            var r = new T();
            r.Value = dict[key];
            r.Name = key;

            return r;
        }
        public static T Create<T>(string syntaxname, string Name, string Content) where T : Glyph, new()
        {
            var item = Create<T>(Content);
            if (!Dictionaries.ContainsKey(syntaxname)) 
            {
                Dictionaries.Add(syntaxname, new Dictionary<string, Glyph>());
            }
            Dictionaries[syntaxname].Add(Name, item);
            return item;
        }
        public static BoundarySeparator CreateBoundarySeparator(string Content, bool IsLeft)
        {
            var item = new BoundarySeparator();
            item.IsLeft = IsLeft;
            item.Value = Content;
            return item;
        }
        public static BoundarySeparator CreateBoundarySeparator(Boundary boundary, bool IsLeft)
        {
            var item = new BoundarySeparator();
            item.IsLeft = IsLeft;
            item.Value = IsLeft ? boundary.Left : boundary.Right;
            item.Name = boundary.Name;
            return item;
        }
        public static Boundary CreateBoundary(string syntaxname,string Name, string left,string right) 
        {
            var item = new Boundary();
            if (!Dictionaries.ContainsKey(syntaxname))
            {
                Dictionaries.Add(syntaxname, new Dictionary<string, Glyph>());
            }
            Dictionaries[syntaxname].Add(Name, item);
            return item;
        }
   
    }
    public enum GlyphType
    {
        String=1,
        Numeric=2,
        DateTime=3,
        Boolean=4
    }
    public class Glyph:IMaterializable
    {

        public string Name = "";
        public string Value = "";
        public Glyph() { }
        public Glyph(string Value) { this.Value = Value; }
        public Glyph(string Name, string Value) { this.Name = Name; this.Value = Value; }

        public static bool HasValue(Glyph g, string value) 
        {

            return g != null ? g.Value == value : false;
        }

        public virtual string Materialize(Language language) 
        {
            return language.Materializer.Materialize(this);
        }
        public virtual string Materialize(Materializer m, Language language)
        {
            return m.Materialize(this);
        }
        public override string ToString()
        {
            return String.Format("{0}: {1} {2}",this.GetType().Name,this.Value,this.Name);
        }

        public static bool HasName(Glyph g, string value)
        {
            return g != null ? g.Name == value : false;
        }
    }
    public class Literal : Glyph
    {

    }
    public class Operator : Glyph
    {

    }
    public class Separator : Glyph
    {

    }
    public class BoundarySeparator : Separator
    {
        public bool IsLeft = true;
    }
    public class Symbol : Glyph 
    {

    }
    public class FunctionName : Symbol
    {

    }
    public class KeyWord : Glyph 
    {

    }

    public class Boundary:Glyph
    {
        public string Name;
        public string Left;
        public string Right;
    }

    public class BoundaryInstance :Glyph
    {
        public Boundary Boundary = null;
        public BoundarySeparator Left;
        public BoundarySeparator Right;
        public BoundaryInstance() { }
        public BoundaryInstance(Boundary Boundary) { this.Boundary = Boundary; }
    }
   

}
