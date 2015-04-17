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

        public static PropertyMapping<C, P> Map<C, P>(string XmlTag, Expression<Func<C, P>> PropertyAccessor) where C : class
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
            if (ClassType == null)
            {
                return MappingCollection.FirstOrDefault(i => i.XmlSelector == identifier);
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
                var basemapping = (ClassMapping)Mappings.CurrentMapping.GetMapping(this.ClassType.BaseType);
                if (basemapping != null) 
                {
                    mappings.AddRange(basemapping.PropertyMappings);
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
            var ok = true;
            
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
            object existingvalue = Getter(instance as TClass);
            var node = nodes.FirstOrDefault();
            LoadNameSpaces(node.OwnerDocument);
            Object value = null;
            if (XmlSelector.StartsWith("<"))
            {
                var tagname = Utilities.Strings.TextBetween(XmlSelector, "<", ">");

                //var tagnodes = Utilities.Xml.SelectNodes(node).Where(i => i.Name == tagname);
                //foreach (XmlNode childnode in node.SelectNodes(tagname, manager))
                foreach (XmlNode childnode in Utilities.Xml.SelectNodes(node,tagname))
                //foreach (XmlNode childnode in tagnodes)
                {
                    var mapping = Mappings.CurrentMapping.GetMapping(childnode, null);
                    value = mapping.Map(childnode);
                    if (this.EnumerableType != null)
                    {

                        prop.PropertyType.GetMethod("Add").Invoke(existingvalue, new[] { value });
                    }
                    else 
                    {
                        Setter(instance as TClass, (PropertyType)value);

                    }
                    value = null;
                }
            }
            else
            {

                var attr = node.Attributes[XmlSelector];
                if (attr != null)
                {
                    value = attr.Value;
                    if (prop.PropertyType.Name == "Int32")
                    {
                        int intval;
                        if (int.TryParse(attr.Value, out intval))
                        {
                            value = intval;
                        }

                    }

                    //
                    if (prop.PropertyType.FullName.StartsWith("XBRLProcessor.Model.StringEnums."))
                    {
                        value = StringEnum.Get(prop.PropertyType, attr.Value);

                    }

                    if (prop.PropertyType.Name == "Boolean" || prop.PropertyType.Name == "bool")
                    {
                        bool intval;
                        if (bool.TryParse(attr.Value, out intval))
                        {
                            value = intval;
                        }
                    }

                    if (prop.PropertyType.Name == "DateTime")
                    {
                        value = Utilities.Converters.StringToDateTime(attr.Value, "yyyy-MM-dd");
                    }

                }
                else 
                {
                    if (node.ChildNodes.Count > 0)
                    {
                        var content = node.ChildNodes[0].Value;
                        if (!string.IsNullOrEmpty(content))
                        {
                            value = node.InnerText.Trim();
                        }
                    }
                }
                if (value != null)
                {
                    Setter(instance as TClass, (PropertyType)value);
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
