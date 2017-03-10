using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilities
{
    public class XmlAttr
    {
        public static Dictionary<string, Dictionary<string, string>> NodeAttributeMap = new Dictionary<string, Dictionary<string, string>>();
        public static KeyValue<XmlNode, IEnumerable<XmlAttribute>> LastNode = new KeyValue<XmlNode, IEnumerable<XmlAttribute>>();

        public static string MapAttr(XmlNode node, string attributename) 
        {
       
            if (!NodeAttributeMap.ContainsKey(node.Name))
            {
                NodeAttributeMap.Add(node.Name, new Dictionary<string, string>());
            }
            var attributemap = NodeAttributeMap[node.Name];
            if (attributemap.ContainsKey(attributename)) { return attributemap[attributename]; }

            if (LastNode.Key != node)
            {
                LastNode.Key = node;
                LastNode.Value = node.Attributes.Cast<XmlAttribute>();
            }
            //    attributes;
            //    foreach (XmlAttribute attr in attributes)
            //    {
            //        var l_name=attr.Name.ToLower();
            //        Ensure(attributemap, l_name, attr.Name);
            //        LastNode.Value.Add(l_name, attr.Name);
            //        if (!String.IsNullOrEmpty(attr.Prefix))
            //        {
            //            var l_lname = attr.LocalName.ToLower();
            //            Ensure(attributemap, l_lname, attr.Name);
            //            LastNode.Value.Add(l_name, attr.Name);

            //        }
            //    }

            //}
            //if (LastNode.Value.ContainsKey(attributename)) 
            //{

            //}

            foreach (XmlAttribute attr in LastNode.Value) 
            {
                //Ensure(attributemap, attr.Name, attr.Name);
                Ensure(attributemap, attr.Name.ToLower(), attr.Name);
                if (!String.IsNullOrEmpty(attr.Prefix)) 
                {
                    //Ensure(attributemap, attr.LocalName, attr.Name);
                    Ensure(attributemap, attr.LocalName.ToLower(), attr.Name);

                }
            }

            if (attributemap.ContainsKey(attributename)) { return attributemap[attributename]; }

            return "";
        }

        public static void Ensure<Tkey, Tvalue>(Dictionary<Tkey, Tvalue> dictionary, Tkey key, Tvalue value) 
        {
            if (!dictionary.ContainsKey(key)) 
            {
                dictionary.Add(key, value);
            }

        }
    }
}
