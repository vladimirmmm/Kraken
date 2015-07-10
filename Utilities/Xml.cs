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

        public static Dictionary<XmlDocument, XmlNamespaceManager> NamespaceDictionary = new Dictionary<XmlDocument, XmlNamespaceManager>();
        private static Object DictionaryLocker = new Object();
        public static XmlNamespaceManager GetTaxonomyNamespaceManager(XmlDocument doc)
        {
            XmlNamespaceManager manager = null;

            if (!NamespaceDictionary.ContainsKey(doc))
            {
                lock (DictionaryLocker)
                {
                    if (!NamespaceDictionary.ContainsKey(doc))
                    {
                        manager = new XmlNamespaceManager(doc.NameTable);
                        manager.AddNamespace("link", "http://www.xbrl.org/2003/linkbase");
                        //var linkbasenode = doc.SelectSingleNode("link:linkbase", manager);
                        var linkbasenode = doc.DocumentElement;
                        var content = XmlToString(doc);
                        var nss = Utilities.Strings.TextsBetween(content, "xmlns:", "\" ");
                        foreach (var ns in nss) 
                        {
                            var parts = ns.Split(new string[] {"="," ","\"" }, StringSplitOptions.RemoveEmptyEntries);
                            var name = parts[0].Trim();
                            var uri = parts[1].Trim();
                            manager.AddNamespace(name, uri);

                        }
                        string s = doc.DocumentElement.GetNamespaceOfPrefix("");
                        manager.AddNamespace("ns", s);
                        //manager.AsQueryable().
                        NamespaceDictionary.Add(doc, manager);
                    }
                }
            }
            else
            {
                manager = NamespaceDictionary[doc];
            }
            return manager;
        }
        public static XmlNamespaceManager GetTaxonomyNamespaceManagerx(XmlDocument doc)
        {
            XmlNamespaceManager manager = null;
            
            if (!NamespaceDictionary.ContainsKey(doc))
            {
                lock (DictionaryLocker)
                {
                    if (!NamespaceDictionary.ContainsKey(doc))
                    {
                        manager = new XmlNamespaceManager(doc.NameTable);
                        manager.AddNamespace("link", "http://www.xbrl.org/2003/linkbase");
                        //var linkbasenode = doc.SelectSingleNode("link:linkbase", manager);
                        var linkbasenode = doc.DocumentElement;

                        //var namespaces = doc.SelectNodes("/*/namespace::*[name()='']");
                        var hasnamespace = false;


                        foreach (XmlAttribute attr in linkbasenode.Attributes)
                        {
                            if (attr.Prefix.ToLower() == "xmlns")
                            {
                                manager.AddNamespace(attr.LocalName, attr.Value);
                                hasnamespace = true;
                            }
                        }
                        if (!hasnamespace && linkbasenode.ChildNodes.Count>0)
                        {
                            foreach (XmlAttribute attr in linkbasenode.ChildNodes[0].Attributes)
                            {
                                if (attr.Prefix.ToLower() == "xmlns")
                                {
                                    manager.AddNamespace(attr.LocalName, attr.Value);
                                    hasnamespace = true;
                                }
                            }
                        }
                        NamespaceDictionary.Add(doc, manager);
                    }
                }
            }
            else 
            {
                manager = NamespaceDictionary[doc];
            }
            return manager;
        }

        public static string Attr(XmlNode node, string Name)
        {
            var lowername = Name.ToLower();
            if (lowername.StartsWith("#"))
            {
                return lowername.Substring(1);
            }
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
                if (lowername.StartsWith("*:"))
                {
                    var localname = lowername.Substring(2);
                    var attr = attributes.FirstOrDefault(i => i.LocalName.Equals(localname, StringComparison.OrdinalIgnoreCase));
                    if (attr != null)
                    {
                        return attr.Value;
                    }
                }
                else
                {
                    //var attr = node.Attributes[Name];
                    var attr = attributes.FirstOrDefault(i => i.Name.Equals(lowername, StringComparison.OrdinalIgnoreCase));
                    if (attr != null)
                    {
                        return attr.Value;
                    }
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
            var nodes = node.SelectNodes(XPath, manager); //.SelectNodes(XPath, manager);
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
