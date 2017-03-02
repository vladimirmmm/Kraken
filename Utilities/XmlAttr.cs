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

        public static string MapAttr(XmlNode node, string attributename) 
        {
       
            if (!NodeAttributeMap.ContainsKey(node.Name))
            {
                NodeAttributeMap.Add(node.Name, new Dictionary<string, string>());
            }
            var attributemap = NodeAttributeMap[node.Name];
            if (attributemap.ContainsKey(attributename)){return attributemap[attributename];}
            var attributes = node.Attributes.Cast<XmlAttribute>();
            foreach (XmlAttribute attr in attributes) 
            {
                Ensure(attributemap, attr.Name.ToLower(), attr.Name);
                if (!String.IsNullOrEmpty(attr.Prefix)) 
                {
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
