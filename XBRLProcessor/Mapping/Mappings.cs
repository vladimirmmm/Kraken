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
using Utilities;
using System.Globalization;

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
        public Dictionary<string, ClassMapping> MappingDictionary = new Dictionary<string, ClassMapping>();

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
                Logger.WriteLine("Mapping not found for " + node.Name);
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
                Logger.WriteLine("Mapping not found for " + node.Name);
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
                Logger.WriteLine("Mapping not found for " + node.Name);
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

        public static ClassMapping<C> Map<C>(string XmlTag, Action<XmlNode,Object> CustomMapping) where C : class
        {
            var cm = new ClassMapping<C>(XmlTag);
            cm.CustomMapping = CustomMapping;
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
            Mapping nodemapping = null;
            Mapping typemapping = null;
            var identifier = "<" + node.Name + ">";
            var localidentifier = "<" + node.LocalName + ">";
            var namespaceinvariantidentifier = "<*:" + node.LocalName + ">";

            if (ClassType == typeof(XBRLProcessor.Model.DefinitionModel.Filter.DimensionFilter)) 
            { 
            }

            //nodemapping = MappingCollection.FirstOrDefault(i => i.XmlSelector == identifier);
            nodemapping = MappingDictionary.ContainsKey(identifier) ? MappingDictionary[identifier] : null;
            if (nodemapping == null)
            {

                //nodemapping = MappingCollection.FirstOrDefault(i => i.XmlSelector == localidentifier);
                nodemapping = MappingDictionary.ContainsKey(localidentifier) ? MappingDictionary[localidentifier] : null;
                if (nodemapping == null)
                {
                    //nodemapping = MappingCollection.FirstOrDefault(i => i.XmlSelector == namespaceinvariantidentifier);
                    nodemapping = MappingDictionary.ContainsKey(namespaceinvariantidentifier) ? MappingDictionary[namespaceinvariantidentifier] : null;

                }
            }
        

            if (ClassType != null)
            {

                typemapping = MappingCollection.FirstOrDefault(i => i.ClassType == ClassType);
          
  
            }

            if (typemapping != null) 
            {
                //if (!nodemapping.ClassType.IsAssignableFrom(typemapping.ClassType))
                if (!typemapping.ClassType.IsAssignableFrom(nodemapping.ClassType))
                {
                    return typemapping;
                }
            }
            return nodemapping;
        }
    }
    
    public class Mapping
    {
        public Type ClassType = null;
        public String XmlSelector = "";
        public XPathContainer XPathContainer = new XPathContainer();

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

        public Action<XmlNode,Object> CustomMapping = null;
        //public List<PropertyMapping<TClass>> PropertyMappings = new List<PropertyMapping<TClass>>();
        public ClassMapping(string XmlSelector) 
        {
            this.XmlSelector = XmlSelector;
            this.XPathContainer = Utilities.Xml.GetXPath(XmlSelector);
            this.ClassType = typeof(TClass);
        }

        public ClassMapping(string XmlSelector, List<PropertyMapping<TClass>> PropertyMappings)
        {
            this.XmlSelector = XmlSelector;
            this.XPathContainer = Utilities.Xml.GetXPath(XmlSelector);
            this.ClassType = typeof(TClass);
            this.OwnPropertyMappings = PropertyMappings.Select(i => (PropertyMapping)i).ToList();
        }

        public override T Map<T>(XmlNode node, T target)
        {
            if (CustomMapping != null) 
            {
                CustomMapping(node, target);
                return target;
            }
            foreach (var pm in PropertyMappings)
            {
                pm.Map(node, target);
            }
            return target;
        }

        public override T Map<T>(XmlNode node)
        {
            var instance =  Activator.CreateInstance<T>();
            if (CustomMapping != null)
            {
                CustomMapping(node, instance);
                return instance;
            }
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
            if (CustomMapping != null)
            {
                CustomMapping(node, instance);
                return instance;
            }
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

        public AttributeSelector AttributeSelector = null;
        public Expression<Func<TClass, PropertyType>> PropertyAccessor = null;
        public Func<TClass, PropertyType> Getter = null;
        public Action<TClass, PropertyType> Setter = null;
        private bool? _IsComplex;
        private PropertyInfo _Property=null;
        public PropertyInfo Property 
        {
            get 
            {
                if (_Property == null) 
                {
                    _Property= (PropertyInfo)((MemberExpression)PropertyAccessor.Body).Member;
                }
                return _Property;
            }
        }
        public bool IsComplex 
        {
            get {
                if (!_IsComplex.HasValue) 
                {
                    _IsComplex =
                   (Property.PropertyType.BaseType != typeof(StringEnum)
                    && Property.PropertyType.IsClass
                    && Property.PropertyType.Name != "String");
                   
                }
                return _IsComplex.Value;
            }
        }


        public PropertyMapping()
        {
        }
        public PropertyMapping(string XmlSelector, Expression<Func<TClass, PropertyType>> PropertyAccessor)
        {
            this.XmlSelector = XmlSelector;
            this.XPathContainer = Utilities.Xml.GetXPath(XmlSelector);
            if (!this.XPathContainer.IsComplex)
            {
                this.AttributeSelector = new AttributeSelector(this.XmlSelector);
            }
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
           // Logger.WriteLine(inst.Bar); // show it working

            //==== reflection
            MethodInfo setMethod = ((PropertyInfo)member.Member).GetSetMethod();
           // setMethod.Invoke(inst, new object[] { "def" });
          //  Logger.WriteLine(inst.Bar); // show it working

            //==== Delegate.CreateDelegate
            action = (Action<TClass, PropertyType>)
                Delegate.CreateDelegate(typeof(Action<TClass, PropertyType>), setMethod);
           // action(inst, "ghi");
           // Logger.WriteLine(inst.Bar); // show it working

            return action;
        }
        private AttributeSelector conentattributeselector = new AttributeSelector("@content");
        public override T Map<T>(IEnumerable<XmlNode> nodes, T instance)
        {
            //var prop = (PropertyInfo)((MemberExpression)PropertyAccessor.Body).Member;
            //var IsComplexType = false;
            var propertyTypeName = Property.PropertyType.Name;
            //if (prop.PropertyType.BaseType!= typeof(StringEnum)
            //    && prop.PropertyType.IsClass && propertyTypeName != "String")
            //{
            //    IsComplexType=true;
            //}
            if (Property.PropertyType.IsGenericType && Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propertyTypeName = Property.PropertyType.GetGenericArguments()[0].Name;
            }
            object existingvalue = Getter(instance as TClass);
            var node = nodes.FirstOrDefault();     

            LoadNameSpaces(node.OwnerDocument);
            Object value = null;
            var tagname = XPathContainer.TagName;// Utilities.Strings.TextBetween(XmlSelector, "<", ">");


 
            if (!String.IsNullOrEmpty(tagname) && IsComplex)
            {
 
                foreach (XmlNode childnode in Utilities.Xml.SelectChildNodes(node,XPathContainer))
                {
                    //var mapping = Mappings.CurrentMapping.GetMapping(childnode, null);
                    var mapping = Mappings.CurrentMapping.GetMapping(childnode, this.EnumerableType);
                    
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
                        Logger.WriteLine("Can't Find mapping for node " + childnode.Name);
                    }
                    value = null;
                }
            }
            else
            {
                var stringval = "";
                if (!String.IsNullOrEmpty(tagname) && !IsComplex)
                {
                    //var childnode = Utilities.Xml.SelectChildNode(node, "//" + tagname);
                    var childnode = Utilities.Xml.SelectChildNode(node, XPathContainer);
                    if (childnode == null && tagname.IndexOf(":")==-1)
                    {
                        var xpath =Utilities.Xml.GetXPath(String.Format("ns:{0}",tagname));
                        childnode = Utilities.Xml.SelectChildNode(node, xpath);
                    }
                    if (childnode != null)
                    {
                        stringval = Utilities.Xml.Attr(childnode, conentattributeselector);
                    }

                }
                //if (XmlSelector.StartsWith("/")) 
                if (!XPathContainer.IsComplex && AttributeSelector.FirtstChildSpecifier)
                {
                    XmlNode firstchild = Utilities.Xml.FirstChild(node);
                    if (firstchild != null) 
                    {
                        var selector = XmlSelector.Substring(1);
                        stringval = Utilities.Xml.Attr(firstchild, AttributeSelector);

                    }
                }
                //else
                if (String.IsNullOrEmpty(stringval) && AttributeSelector!=null) //default case
                {
                    stringval = Utilities.Xml.Attr(node, AttributeSelector);
                }
   
                if (!String.IsNullOrEmpty(stringval))
                {
                    value = stringval;
                    if (propertyTypeName == "Decimal")
                    {
                        decimal decval;
                        if (decimal.TryParse(stringval, NumberStyles.Number, CultureInfo.InvariantCulture, out decval))
                        {
                            value = decval;
                        }
                    }
                    if (propertyTypeName == "Int32")
                    {
                        int intval;
                        if (int.TryParse(stringval, out intval))
                        {
                            value = intval;
                        }

                    }

                    if (Property.PropertyType.BaseType == typeof(StringEnum))
                    {
                        value = StringEnum.Get(Property.PropertyType, stringval);

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
                        Logger.WriteLine("Mapping: " + ex.Message);
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
