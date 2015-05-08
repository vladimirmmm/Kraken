using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilities
{
    public class Xml
    {
        public static string XmlToString(XmlDocument doc)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            doc.WriteTo(tx);

            string str = sw.ToString();// 
            return str;
        }


        public static List<XmlNode> AllNodes(XmlDocument doc) 
        {
            return doc.GetElementsByTagName("*").Cast<XmlNode>().ToList();
        }

        public static XmlNamespaceManager GetTaxonomyNamespaceManager(XmlDocument doc)
        {
            var manager = new XmlNamespaceManager(doc.NameTable);
            manager.AddNamespace("link", "http://www.xbrl.org/2003/linkbase");
            //var linkbasenode = doc.SelectSingleNode("link:linkbase", manager);
            var linkbasenode = doc.DocumentElement;
            foreach (XmlAttribute attr in linkbasenode.Attributes)
            {
                if (attr.Prefix.ToLower() == "xmlns")
                {
                    manager.AddNamespace(attr.LocalName, attr.Value);

                }
            }
            return manager;
        }

        public static string Attr(XmlNode node, string Name)
        {
            var lowername = Name.ToLower();
            if (lowername.StartsWith("@")) 
            {
                if (lowername == "@content") 
                {
                    return Content(node);
                }
                if (lowername == "@name")
                {
                    return node.Name;
                }
            }

            if (node.Attributes != null)
            {
                var attributes = node.Attributes.Cast<XmlAttribute>();

                //var attr = node.Attributes[Name];
                var attr = attributes.FirstOrDefault(i=>i.Name.Equals(lowername,StringComparison.OrdinalIgnoreCase) );
                if (attr != null)
                {
                    return attr.Value;
                }
            }
            return "";
        }

        public static string Content(XmlNode node)
        {
            if (node.ChildNodes.Count > 0) 
            {
                var content = node.ChildNodes[0].Value;
                return content;
            }
            return null;
        }

        public static XmlNode SelectSingleNode(XmlNode node, string XPath)
        {
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);

            return node.OwnerDocument.SelectSingleNode(XPath, manager);
        }

        public static XmlNode SelectChildNode(XmlNode node, string XPath)
        {
            return SelectChildNodes(node, XPath).FirstOrDefault();
        }
        

        public static List<XmlNode> SelectChildNodes(XmlNode node, string XPath)
        {
            var result = new List<XmlNode>();
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);
            if (XPath.Contains(":")) 
            {
                var ns = XPath;
                if (XPath.StartsWith("//"))
                {
                    ns = XPath.Substring(2);
                }
                ns = ns.Remove(ns.IndexOf(":"));
                if (!manager.HasNamespace(ns)) 
                {
                    return result;
                    //return node.OwnerDocument.SelectNodes("xffgh");
                }
            }
            var nodes = node.SelectNodes(XPath, manager);
            foreach (XmlNode xnode in nodes) 
            {
                if (xnode.ParentNode == node) 
                {
                    result.Add(xnode);
                }
            }
            return result;
        }

        public static List<XmlNode> SelectNodes(XmlNode node, string XPath)
        {
            var result = new List<XmlNode>();
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);
            if (XPath.Contains(":"))
            {
                var ns = XPath;
                if (XPath.StartsWith("//"))
                {
                    ns = XPath.Substring(2);
                }
                ns = ns.Remove(ns.IndexOf(":"));
                if (!manager.HasNamespace(ns))
                {
                    return result;
                    //return node.OwnerDocument.SelectNodes("xffgh");
                }
            }
            var nodes = node.SelectNodes(XPath, manager);
            foreach (XmlNode xnode in nodes)
            {
                result.Add(xnode);
            }
            return result;
        }

        public static List<XmlNode> SelectNodes(XmlNode node)
        {
            var tags = node.OwnerDocument.GetElementsByTagName("*").Cast<XmlNode>().ToList();
            return tags;
        }

        public static XmlNode FirstChild(XmlNode node)
        {
            if (node.ChildNodes != null && node.ChildNodes.Count > 0)
            {
                return node.ChildNodes[0];
            }
            return null;
        }
    }
}
