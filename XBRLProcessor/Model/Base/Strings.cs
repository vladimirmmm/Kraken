using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBRLProcessor.Model.StringEnums
{
    public class StringEnum
    {
        private readonly String name;
        private readonly int value;

        protected static Dictionary<Type, Dictionary<String, StringEnum>> Values = new Dictionary<Type, Dictionary<String, StringEnum>>();
        public static readonly StringEnum Unspecified = new StringEnum(0, "Unspecified");

        public StringEnum(int value, String name)
        {
            this.name = name.ToLower();
            this.value = value;
            var mytype =this.GetType();
            if (!StringEnum.Values.ContainsKey(mytype))
            {
                StringEnum.Values.Add(this.GetType(), new Dictionary<string, StringEnum>());
            }
            var items = StringEnum.Values[mytype];
            items.Add(name, this);
        }
        public static StringEnum Get(Type StringEnumType, string key) 
        {
            var items = StringEnum.Values[StringEnumType];
            return items[key.ToLower()];
        }
        public override String ToString()
        {
            return name;
        }
    }
    public class ParentChildOrder: StringEnum
    {
        public ParentChildOrder(int value, String name) : base(value, name) { }
        public static readonly ParentChildOrder ParentFirst = new ParentChildOrder(0, "parent-first");
        public static readonly ParentChildOrder ChildFirst = new ParentChildOrder(1, "child-first");

    }
    public class AspectModel : StringEnum
    {
        public AspectModel(int value, String name) : base(value, name) { }
        public static readonly AspectModel Dimensional = new AspectModel(0, "dimensional");

    }

    public class Axis : StringEnum
    {
        public Axis(int value, String name) : base(value, name) { }
  
        public static readonly Axis X = new Axis(0, "x");
        public static readonly Axis Y = new Axis(0, "y");
        public static readonly Axis Z = new Axis(0, "z");

    }
}
