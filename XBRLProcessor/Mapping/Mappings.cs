using XBRLProcessor.Model.StringEnums;
using Model.DefinitionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;

namespace XBRLProcessor.Mapping
{
    public partial class Mappings
    {
        private static Mappings _CurrentMapping = null;
        public static Mappings CurrentMapping 
        {
            get 
            {
                if (_CurrentMapping == null) 
                {
                    _CurrentMapping = new Mappings();
                }
                return _CurrentMapping;
            }
        }

        public static List<String> GetTagsCovered() 
        {
            var tags = new List<String>();
            foreach (var mapping in CurrentMapping.MappingCollection) 
            {
                tags.Add(mapping.XmlSelector);
                foreach (var pm in mapping.PropertyMappings) 
                {
                    if (pm.XmlSelector.StartsWith("<")) 
                    {
                        tags.Add(pm.XmlSelector);

                    }
                }
            }
            return tags.Distinct().ToList();
        }

        public List<ClassMapping> MappingCollection = new List<ClassMapping>();

        public T Map<T>(XmlNode node, T target) where T : class
        {
            //var mapping = MappingCollection.FirstOrDefault(i => i.XmlSelector == "<" + node.Name + ">");
            var mapping = MappingCollection.FirstOrDefault(i => i.ClassType==typeof(T));
            if (mapping != null)
            {
                return mapping.Map<T>(node, target);
            }
            else
            {
                Console.WriteLine("Mapping not found for " + node.Name);
            }
            return null;
        }

        public T Map<T>(XmlNode node) where T : class
        {
            var mapping = MappingCollection.FirstOrDefault(i => i.XmlSelector == "<" + node.Name + ">");
            if (mapping != null)
            {
                return mapping.Map<T>(node);
            }
            else
            {
                Console.WriteLine("Mapping not found for " + node.Name);
            }
            return null;
        }

        public object Map(XmlNode node)
        {
            var mapping = MappingCollection.FirstOrDefault(i => i.XmlSelector == "<" + node.Name + ">");
            if (mapping != null)
            {
                return mapping.Map(node);
            }
            else 
            {
                Console.WriteLine("Mapping not found for " + node.Name);
            }
            return null;
        }

        public static PropertyMapping<C, P> PropertyMap<C, P>(string XmlTag, Expression<Func<C, P>> PropertyAccessor) where C : class
        {
            var pm = new PropertyMapping<C, P>(XmlTag, PropertyAccessor);
            //pm.XmlSelector = XmlTag;
            //pm.PropertyAccessor = PropertyAccessor;
            return pm;
        }

        public static PropertyMapping<C> PropertyMapX<C, P>(string XmlTag, Expression<Func<C, P>> PropertyAccessor) where C : class
        {
            var pm = new PropertyMapping<C, P>(XmlTag, PropertyAccessor);
            //pm.XmlSelector = XmlTag;
            //pm.PropertyAccessor = PropertyAccessor;
            return pm;
        }

        public static PropertyMapping<C, P> CreatePM<C, P>(string XmlTag, Expression<Func<C, P>> PropertyAccessor) where C : class
        {
            var pm = new PropertyMapping<C, P>(XmlTag, PropertyAccessor);
            //pm.XmlSelector = XmlTag;
            //pm.PropertyAccessor = PropertyAccessor;
            return pm;
        }

        public static ClassMapping<C> Create<C>(string XmlTag) where C : class
        {
            var cm = new ClassMapping<C>(XmlTag);
            return cm;
        }

        public static ClassMapping<C> Map<C>(string XmlTag, List<PropertyMapping<C>> PropertyMappings) where C : class
        {
            var cm = new ClassMapping<C>(XmlTag, PropertyMappings);
            return cm;
        }

        public static ClassMapping<C> Map<C>(string XmlTag, params PropertyMapping<C>[] PropertyMappings) where C : class
        {
            var cm = new ClassMapping<C>(XmlTag, PropertyMappings.ToList());
            return cm;
        }

        public Mapping GetMapping(Type type) 
        {
            return MappingCollection.FirstOrDefault(i => i.ClassType == type);
              
        }
   
        public Mapping GetMapping<T>(XmlNode node,T ClassType) where T:class
        {
            var identifier = "<" + node.Name + ">";
            if (ClassType == null)
            {
                return MappingCollection.FirstOrDefault(i => i.XmlSelector == identifier);
            }
            else 
            {
                var classmapping = (ClassMapping<T>)MappingCollection.FirstOrDefault(i => i.ClassType == typeof(T));
                var propertyMapping = classmapping.PropertyMappings.FirstOrDefault(i => i.XmlSelector == identifier);
                return propertyMapping;
            }
        }

        public Mapping GetMapping(XmlNode node, Type ClassType) 
        {
            var identifier = "<" + node.Name + ">";
            var localidentifier = "<" + node.LocalName + ">";

            if (ClassType == null)
            {
                var m = MappingCollection.FirstOrDefault(i => i.XmlSelector == identifier);
                if (m == null) 
                {
                    m = MappingCollection.FirstOrDefault(i => i.XmlSelector == localidentifier);
                }
                if (m != null) 
                {
                    //var mappings = MappingCollection.Where(i => i.ClassType == m.ClassType && i!=m);
                    //foreach (var mapping in mappings) 
                    //{
                    //    m.prop
                    //}
                }
                return m;
            }
            else
            {
                var classmapping = MappingCollection.FirstOrDefault(i => i.ClassType == ClassType);
                var propertyMapping = classmapping.PropertyMappings.FirstOrDefault(i => i.XmlSelector == identifier);
                return propertyMapping;
            }
        }
    }
    
    public class Mapping
    {
        public Type ClassType = null;
        public String XmlSelector = "";

        protected XmlNamespaceManager manager = null;
        XmlDocument currentdoc = null;
        protected void LoadNameSpaces(XmlDocument doc)
        {
            if (doc != currentdoc)
            {
                manager = Utilities.Xml.GetTaxonomyNamespaceManager(doc);
                currentdoc = doc;
            }

        }
        public virtual T Map<T>(XmlNode node, T target) where T : class
        {
            var parentNode = node.ParentNode;

            return null;
        }

        public virtual T Map<T>(XmlNode node) where T : class
        {
            var parentNode = node.ParentNode;

            return null;
        }
        public T Map<T>(IEnumerable<XmlNode> nodes) where T : class
        {
            return null;
        }

        public virtual object Map(XmlNode node) 
        {
            return Activator.CreateInstance(ClassType);
        }
    }
    
    public class PropertyMapping : Mapping
    {
        public override T Map<T>(XmlNode node, T instance)
        {
            return Map<T>(new List<XmlNode>() { node }, instance);
        }

        public virtual T Map<T>(IEnumerable<XmlNode> nodes, T instance) where T : class
        {
            return null;
        }
     
    }
    
    public class ClassMapping : Mapping 
    {
        protected List<PropertyMapping> OwnPropertyMappings = new List<PropertyMapping>();

        public List<PropertyMapping> PropertyMappings
        {
            get {
                var mappings = new List<PropertyMapping>();
                mappings.AddRange(OwnPropertyMappings);
                Type basetype = this.ClassType.BaseType;
                while (basetype != null && basetype!=typeof(Object))
                {
                    var basemapping = (ClassMapping)Mappings.CurrentMapping.GetMapping(basetype);
                    if (basemapping != null)
                    {
                        mappings.AddRange(basemapping.PropertyMappings);
                    }
                    basetype = basetype.BaseType;
                }
                return mappings;
            }
        }

        public override object Map(XmlNode node)
        {
            var instance =  Activator.CreateInstance(ClassType);
            return instance;
        }
    }
    
    public class ClassMapping<TClass> : ClassMapping where TClass:class
    {

        //public List<PropertyMapping<TClass>> PropertyMappings = new List<PropertyMapping<TClass>>();
        public ClassMapping(string XmlSelector) 
        {
            this.XmlSelector = XmlSelector;
            this.ClassType = typeof(TClass);
        }

        public ClassMapping(string XmlSelector, List<PropertyMapping<TClass>> PropertyMappings)
        {
            this.XmlSelector = XmlSelector;
            this.ClassType = typeof(TClass);
            this.OwnPropertyMappings = PropertyMappings.Select(i => (PropertyMapping)i).ToList();
        }

        public override T Map<T>(XmlNode node, T target)
        {
            foreach (var pm in PropertyMappings)
            {
                pm.Map(node, target);
            }
            return target;
        }

        public override T Map<T>(XmlNode node)
        {
            var instance =  Activator.CreateInstance<T>();
            foreach (var pm in PropertyMappings) 
            {
                pm.Map(node, instance);
            }
            return instance;
        }

        public override object Map(XmlNode node)
        {
            var instance = Activator.CreateInstance<TClass>();
            var basepropertymappings = new List<PropertyMapping>();
            
            foreach (var pm in PropertyMappings)
            {
                pm.Map(node, instance);
            }
            return instance;
        }

    }

    public class PropertyMapping<TClass> : PropertyMapping
    {
        public Type PropertyType = null;
        public Type EnumerableType = null;
     

    }

    public class PropertyMapping<TClass, PropertyType> : PropertyMapping<TClass> where TClass : class
    {
     

        public Expression<Func<TClass, PropertyType>> PropertyAccessor = null;
        public Func<TClass, PropertyType> Getter = null;
        public Action<TClass, PropertyType> Setter = null;


        public PropertyMapping()
        {
        }
        public PropertyMapping(string XmlSelector, Expression<Func<TClass, PropertyType>> PropertyAccessor)
        {
            this.XmlSelector = XmlSelector;
            this.PropertyAccessor = PropertyAccessor;
            this.Getter = PropertyAccessor.Compile();
            this.Setter = GetSetter(PropertyAccessor);
            this.EnumerableType = Utilities.Objects.GetEnumerableType(typeof(PropertyType));
            this.PropertyType = typeof(PropertyType);
        }

        private Action<TClass, PropertyType> GetSetter(Expression<Func<TClass, PropertyType>> getter)
        {
            var member = (MemberExpression)getter.Body;
            var param = Expression.Parameter(typeof(PropertyType), "value");
            var set = Expression.Lambda<Action<TClass, PropertyType>>(
                Expression.Assign(member, param), getter.Parameters[0], param);

            // compile it
            var action = set.Compile();
          // var inst = new Foo();
           // action(inst, "abc");
           // Console.WriteLine(inst.Bar); // show it working

            //==== reflection
            MethodInfo setMethod = ((PropertyInfo)member.Member).GetSetMethod();
           // setMethod.Invoke(inst, new object[] { "def" });
          //  Console.WriteLine(inst.Bar); // show it working

            //==== Delegate.CreateDelegate
            action = (Action<TClass, PropertyType>)
                Delegate.CreateDelegate(typeof(Action<TClass, PropertyType>), setMethod);
           // action(inst, "ghi");
           // Console.WriteLine(inst.Bar); // show it working

            return action;
        }

        public override T Map<T>(IEnumerable<XmlNode> nodes, T instance)
        {
            var prop = (PropertyInfo)((MemberExpression)PropertyAccessor.Body).Member;
            var IsComplexType = false;
            var propertyTypeName = prop.PropertyType.Name;
            if (prop.PropertyType.BaseType!= typeof(StringEnum)
                && prop.PropertyType.IsClass && propertyTypeName != "String")
            {
                IsComplexType=true;
            }
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propertyTypeName = prop.PropertyType.GetGenericArguments()[0].Name;
            }
            object existingvalue = Getter(instance as TClass);
            var node = nodes.FirstOrDefault();

            LoadNameSpaces(node.OwnerDocument);
            Object value = null;
            var tagname = Utilities.Strings.TextBetween(XmlSelector, "<", ">");

            if (XmlSelector.Contains("df:typedDimension"))
            {
                var c = Utilities.Xml.SelectChildNodes(node, tagname).Count;
                if (c > 0) 
                {

                }
            }
 
            if (!String.IsNullOrEmpty(tagname) && IsComplexType)
            {

                foreach (XmlNode childnode in Utilities.Xml.SelectChildNodes(node,tagname))
                {
                    var mapping = Mappings.CurrentMapping.GetMapping(childnode, null);
                    if (mapping != null)
                    {
                        value = mapping.Map(childnode);
                        if (this.EnumerableType != null)
                        {
                            var list = existingvalue as IList;
                            list.Add(value);
                            //prop.PropertyType.GetMethod("Add").Invoke(existingvalue, new[] { value });
                        }
                        else
                        {
                            Setter(instance as TClass, (PropertyType)value);

                        }
                    }
                    else 
                    {
                        Console.WriteLine("Can't Find mapping for node " + childnode.Name);
                    }
                    value = null;
                }
            }
            else
            {
                var stringval = "";
                if (!String.IsNullOrEmpty(tagname) && !IsComplexType)
                {
                    //var childnode = Utilities.Xml.SelectChildNode(node, "//" + tagname);
                    var childnode = Utilities.Xml.SelectChildNode(node, tagname);
                    if (childnode == null && tagname.IndexOf(":")==-1)
                    {
                        childnode = Utilities.Xml.SelectChildNode(node, String.Format("ns:{0}",tagname));
                    }
                    if (childnode != null)
                    {
                        stringval = Utilities.Xml.Attr(childnode, "@content");
                    }

                }
                if (XmlSelector.StartsWith("/")) 
                {
                    XmlNode firstchild = Utilities.Xml.FirstChild(node);
                    if (firstchild != null) 
                    {
                        var selector = XmlSelector.Substring(1);
                        stringval = Utilities.Xml.Attr(firstchild, selector);

                    }
                }
                //else
                if (String.IsNullOrEmpty(stringval)) //default case
                {
                    stringval = Utilities.Xml.Attr(node, XmlSelector);
                }
   
                if (!String.IsNullOrEmpty(stringval))
                {
                    value = stringval;
                    if (propertyTypeName == "Int32")
                    {
                        int intval;
                        if (int.TryParse(stringval, out intval))
                        {
                            value = intval;
                        }

                    }

                    if (prop.PropertyType.BaseType == typeof(StringEnum))
                    {
                        value = StringEnum.Get(prop.PropertyType, stringval);

                    }

                    if (propertyTypeName == "Boolean" || propertyTypeName == "bool")
                    {
                        bool intval;
                        if (bool.TryParse(stringval, out intval))
                        {
                            value = intval;
                        }
                    }

                    if (propertyTypeName == "DateTime")
                    {
                        value = Utilities.Converters.StringToDateTime(stringval, "yyyy-MM-dd");
                    }

                }
                else
                {

                    if (!string.IsNullOrEmpty(stringval) && propertyTypeName == "String")
                    {
                        value = stringval;
                    }

                }
                
                if (value != null)
                {
                    try
                    {
                        Setter(instance as TClass, (PropertyType)value);
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine("Mapping: " + ex.Message);
                    }
                }
                //prop.SetValue(instance, value, null);

            }

            return instance;
        }

        public override string ToString()
        {
            return String.Format("{0} to {1}",XmlSelector,Utilities.Linq.GetPropertyName(PropertyAccessor) );
        }
    }
}
