using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilities
{
    public enum AttributeSelectTypeEnum
    {
        Unspecified,
        Content,
        Tagname,
        Name,
        LocalName,
    }
    public class AttributeSelector 
    {
        public string Expression = "";
        public AttributeSelectTypeEnum Type = AttributeSelectTypeEnum.Name;
        public string Value = "";
        public bool FirtstChildSpecifier = false;
        public AttributeSelector(string selector) 
        {
            var lowername = selector.ToLower();
            if (lowername.StartsWith("/"))
            {
                FirtstChildSpecifier = true;
                lowername = lowername.Substring(1);
            }
            if (lowername.StartsWith("#"))
            {
                Type = AttributeSelectTypeEnum.Unspecified;
                Value = lowername.Substring(1);
            }
            if (lowername.StartsWith("@"))
            {
                if (lowername == "@content")
                {
                    Type = AttributeSelectTypeEnum.Content;

                }
                if (lowername == "@name")
                {
                    Type = AttributeSelectTypeEnum.Tagname;
                }
            }
            if (lowername.StartsWith("*:"))
            {
                Type = AttributeSelectTypeEnum.LocalName;
                Value = lowername.Substring(2);
            }
            else 
            {
                Value = lowername;
            }
        }
    }
    public class XPathContainer
    {
        public string XPath = "";
        public List<string> Namespaces = new List<string>();
        public bool IsComplex = false;
        public string TagName = "";
        public XPathContainer() { }

        public XPathContainer(string XPath)
        {
            this.XPath = XPath;
        }
        public XPathContainer(string XPath,List<string> Namespaces)
        {
            this.XPath = XPath;
            this.Namespaces = Namespaces;
        }

        public bool HasNamespace(XmlNamespaceManager manager) 
        {
            var ns = Namespaces.FirstOrDefault();
            if (!manager.HasNamespace(ns) && ns != "*")
            {
                return false;
                //return node.OwnerDocument.SelectNodes("xffgh");
            }
            return true;
        }
    }
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

        public static Dictionary<int, XmlNamespaceManager> NamespaceDictionary = new Dictionary<int, XmlNamespaceManager>();
        //public static Dictionary<XmlDocument, XmlNamespaceManager> NamespaceDictionary = new Dictionary<XmlDocument, XmlNamespaceManager>();
        public static Dictionary<String, String> Namespaces = new Dictionary<String, String>();
        public static Dictionary<String, String> NamespaceUris = new Dictionary<String, String>();

        public static void ClearNamespaceCache() 
        {
            Namespaces.Clear();
            NamespaceUris.Clear();
            NamespaceDictionary.Clear();
            XmlAttr.NodeAttributeMap.Clear();

        }


        public static void ClearDocument(XmlDocument document)
        {
            if (document != null)
            {
                var hashcode = document.GetHashCode();
                //var localpath = document.DocumentElement.GetAttribute("localpath");
                if (NamespaceDictionary.ContainsKey(hashcode))
                {
                    NamespaceDictionary.Remove(hashcode);
                }
            }
        }

        private static Object DictionaryLocker = new Object();
        public static XmlNamespaceManager GetTaxonomyNamespaceManager(XmlDocument doc)
        {
            XmlNamespaceManager manager = null;
            var hashcode = doc.GetHashCode();

            if (!NamespaceDictionary.ContainsKey(hashcode))
            {
                lock (DictionaryLocker)
                {
                    if (!NamespaceDictionary.ContainsKey(hashcode))
                    {
                        manager = new XmlNamespaceManager(doc.NameTable);
                        manager.AddNamespace("link", "http://www.xbrl.org/2003/linkbase");
                        //var linkbasenode = doc.SelectSingleNode("link:linkbase", manager);
                        //var linkbasenode = doc.DocumentElement;
                        //foreach (var item in doc.NameTable) 
                        //{
                        //    item
                        //}
                        /*
                        var attributes = doc.DocumentElement.Attributes;
                        var nss = new List<KeyValue>();
                        foreach (var attribute in attributes) 
                        {
                            var attr=(XmlAttribute)attribute;
                            if (attr.Prefix == "xmlns") 
                            {
                                nss.Add(new KeyValue(attr.LocalName, attr.Value));
                            }
                        }
                        */
                        //var content = XmlToString(doc);
                        var xml = String.IsNullOrEmpty(doc.OuterXml) ? doc.InnerXml : doc.OuterXml;
                        var nss = Utilities.Strings.TextsBetween(xml, "xmlns:", "\" ");
                        foreach (var ns in nss) 
                        {
                            var parts = ns.Split(new string[] { "=", " ", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                            var name = parts[0].Trim();
                            var uri = parts[1].Trim();

                            //var name = ns.Key;
                            //var uri =  ns.Value.ToString();
                            manager.AddNamespace(name, uri);
                            if (!Namespaces.ContainsKey(name)) 
                            {
                                Namespaces.Add(name,uri);
                            }
                            //if (!NamespaceUris.ContainsKey(uri))
                            //{
                            //    NamespaceUris.Add(uri, name);
                            //}
                            //else 
                            //{
                            //    if (NamespaceUris[uri] != name) 
                            //    {

                            //    }
                            //}
                        }
                 
                        //string s = doc.DocumentElement.GetNamespaceOfPrefix("");
                        string s = doc.GetNamespaceOfPrefix("");
                        manager.AddNamespace("ns", s);
                        //manager.AsQueryable().
                        NamespaceDictionary.Add(hashcode, manager);
                    }
                }
            }
            else
            {
                manager = NamespaceDictionary[hashcode];
            }
            return manager;
        }

      
        public static string Attr(XmlNode node, string Name)
        {
            var a1 = Attr_New(node, Name);
            //var a2 = Attr_Old(node, Name);
            //if (a1 != a2) 
            //{

            //}
            return a1;
        }
        public static string Attr(XmlNode node, AttributeSelector selector)
        {
            return Attr_New(node, selector);
        }
        public static string Attr_Old(XmlNode node, string Name)
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

                    //return GetValueFrom(localname, node);

                }
                else
                {
                    var attr = attributes.FirstOrDefault(i => i.Name.Equals(lowername, StringComparison.OrdinalIgnoreCase));
                    if (attr != null)
                    {
                        return attr.Value;
                    }
                   
                    //return GetValueFrom(lowername, node);

                }
            }
            return "";
        }

        public static string Attr_New(XmlNode node, AttributeSelector selector)
        {

            if (node.Attributes != null)
            {
                if (selector.Type == AttributeSelectTypeEnum.Content) 
                {
                    return Content(node);
                }
                if (selector.Type == AttributeSelectTypeEnum.Tagname)
                {
                    return node.Name;
                }
                if (selector.Type == AttributeSelectTypeEnum.LocalName)
                {
                    return GetValueFrom(selector.Value, node);

                }
                if (selector.Type == AttributeSelectTypeEnum.Name)
                {
                    return GetValueFrom(selector.Value, node);
                }
                if (selector.Type == AttributeSelectTypeEnum.Unspecified)
                {
                    return selector.Value.Substring(1);
                }
                return "";
            }
            return "";
        }


        public static string Attr_New(XmlNode node, string Name)
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
                //var attributes = node.Attributes.Cast<XmlAttribute>();
                if (lowername.StartsWith("*:"))
                {
                    var localname = lowername.Substring(2);
                    //var attr = attributes.FirstOrDefault(i => i.LocalName.Equals(localname, StringComparison.OrdinalIgnoreCase));
                    //if (attr != null)
                    //{

                    //    return attr.Value;
                    //}

                    return GetValueFrom(localname, node);
                    //return lastnode.Item2.ContainsKey(localname) ? lastnode.Item2[localname] : "";
                   
                }
                else
                {
                    //var attr = node.Attributes[Name];
                    //var attr = attributes.FirstOrDefault(i => i.Name.Equals(lowername, StringComparison.OrdinalIgnoreCase));
                    //if (attr != null)
                    //{
                    //    return attr.Value;
                    //}
                    return GetValueFrom(lowername,node);
                    //return lastnode.Item2.ContainsKey(lowername) ? lastnode.Item2[lowername] : "";
                 
                }
            }
            return "";
        }

        private static string GetValueFrom(string name, XmlNode node) 
        {
            var attrx = node.Attributes[name];
            if (attrx != null) { return attrx.Value; }
            var mappedattributename = XmlAttr.MapAttr(node, name);
            if (!String.IsNullOrEmpty(mappedattributename)) 
            {
                var attr = node.Attributes[mappedattributename];
                if (attr != null) { return attr.Value; }

            }
            return "";
           
            /*
            var mappedname =name;
            if (!mapcontainer.ContainsKey(name)) 
            {
                var xname = valuecontainer.Keys.FirstOrDefault(i => i.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(xname))
                {
                    mapcontainer.Add(name, xname);
                }
            }
            else
            {
                mappedname= mapcontainer[name];
            }

            //var mappedname = mapcontainer[name];
            return valuecontainer.ContainsKey(mappedname) ? valuecontainer[mappedname] : "";
             * */
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
        public static XmlNode SelectSingleNode(XmlNode node, XPathContainer XPathContainer)
        {
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);
            if (!XPathContainer.HasNamespace(manager))
            {
                return null;
            }

            return node.OwnerDocument.SelectSingleNode(XPathContainer.XPath, manager);
        }
        public static XmlNode SelectSingleNode(XmlNode node, string XPath)
        {
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);

            return node.OwnerDocument.SelectSingleNode(XPath, manager);
        }

        public static XmlNode SelectChildNode(XmlNode node, XPathContainer XPath)
        {
            return SelectChildNodes(node, XPath).FirstOrDefault();
        }

        public static XmlNode SelectChildNode(XmlNode node, string xpath)
        {
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);
            return node.SelectSingleNode(xpath, manager);
        }

        public static XPathContainer GetXPath(string XmlSelector) 
        {
            var xpathcontainer = new XPathContainer();
            xpathcontainer.IsComplex = XmlSelector.IndexOf("<") > -1;
            xpathcontainer.TagName = Utilities.Strings.TextBetween(XmlSelector, "<", ">");
            var xpath = xpathcontainer.IsComplex ? xpathcontainer.TagName : XmlSelector;
            var ix = xpath.IndexOf(":", StringComparison.Ordinal);
            if (ix > -1)
            {
                
                var ns = xpath;
                ns = ns.Remove(ix);
                if (ns.StartsWith("//", StringComparison.Ordinal))
                {
                    ns = ns.Substring(2);
                }
                var name = xpath.Substring(ix + 1);
                xpathcontainer.Namespaces.Add(ns);

                if (ns == "*")
                {
                    //XPath = "//*[local-name() = translate('" + name + "','abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')";
                    //XPath = "*[translate(local-name(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + name.ToUpper() + "']";
                    //XPath = "*[matches(local-name(),'" + name + "',i)]";
                    xpath = "*[local-name()='" + name + "']";
                }
           
            }
            xpathcontainer.XPath = xpath;
            return xpathcontainer;
        }
        public static List<XmlNode> SelectChildNodes(XmlNode node, string xpath)
        {
            var result = new List<XmlNode>();
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);

            var nodes = node.SelectNodes(xpath, manager); //.SelectNodes(XPath, manager);
            foreach (XmlNode xnode in nodes)
            {
                if (xnode.ParentNode == node)
                {
                    result.Add(xnode);
                }
            }
            return result;
        }
        public static List<XmlNode> SelectChildNodes(XmlNode node, XPathContainer XPathContainer)
        {
            var result = new List<XmlNode>();
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);
  
            //var ix = XPath.IndexOf(":", StringComparison.Ordinal);
            //if (ix>-1) 
            //{
            //    var ns = XPath;
            //    if (XPath.StartsWith("//", StringComparison.Ordinal))
            //    {
            //        ns = XPath.Substring(2);
            //    }
            //    ns = ns.Remove(ix);
            //    var name = XPath.Substring(ix + 1);
            //    if (ns == "*") 
            //    {
            //        //XPath = "//*[local-name() = translate('" + name + "','abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')";
            //        //XPath = "*[translate(local-name(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + name.ToUpper() + "']";
            //        //XPath = "*[matches(local-name(),'" + name + "',i)]";
            //        XPath = "*[local-name()='" + name + "']";
            //    }
            //    if (!manager.HasNamespace(ns) && ns!="*") 
            //    {
            //        return result;
            //        //return node.OwnerDocument.SelectNodes("xffgh");
            //    }
            //}
            //if (XPath.Contains("*")) 
            //{

            //}
            if (!XPathContainer.HasNamespace(manager)) 
            {
                return result;
            }
            var nodes = node.SelectNodes(XPathContainer.XPath, manager); //.SelectNodes(XPath, manager);
            foreach (XmlNode xnode in nodes) 
            {
                if (xnode.ParentNode == node) 
                {
                    result.Add(xnode);
                }
            }
            return result;
        }
        public static List<XmlNode> SelectNodes(XmlNode node, XPathContainer xpathcontainer)
        {
            var result = new List<XmlNode>();
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);
            foreach(var ns in xpathcontainer.Namespaces)
            {
                if (!manager.HasNamespace(ns))
                {
                    return result;
                }
            }
            //if (XPath.Contains(":"))
            //{
            //    var ns = XPath;
            //    if (XPath.StartsWith("//"))
            //    {
            //        ns = XPath.Substring(2);
            //    }
            //    ns = ns.Remove(ns.IndexOf(":"));
            //    if (!manager.HasNamespace(ns) )
            //    {
            //        return result;
            //        //return node.OwnerDocument.SelectNodes("xffgh");
            //    }
            //}
            //try
            //{
            var nodes = node.SelectNodes(xpathcontainer.XPath, manager);
            foreach (XmlNode xnode in nodes)
            {
                result.Add(xnode);
            }
            //}
            //catch (Exception ex) 
            //{

            //}
            return result;
        }
        public static List<XmlNode> SelectNodes(XmlNode node, string XPath)
        {
            var result = new List<XmlNode>();
            XmlNamespaceManager manager = Utilities.Xml.GetTaxonomyNamespaceManager(node.OwnerDocument);
            //if (XPath.Contains(":"))
            //{
            //    var ns = XPath;
            //    if (XPath.StartsWith("//"))
            //    {
            //        ns = XPath.Substring(2);
            //    }
            //    ns = ns.Remove(ns.IndexOf(":"));
            //    if (!manager.HasNamespace(ns) )
            //    {
            //        return result;
            //        //return node.OwnerDocument.SelectNodes("xffgh");
            //    }
            //}
            //try
            //{
                var nodes = node.SelectNodes(XPath, manager);
                foreach (XmlNode xnode in nodes)
                {
                    result.Add(xnode);
                }
            //}
            //catch (Exception ex) 
            //{

            //}
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
