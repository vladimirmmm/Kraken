using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities;
using XBRLProcessor.Models;

namespace XBRLProcessor.Helpers
{

    public class XmlNodeHandler:LogicalModel.Helpers.TaxHandler
    {
        public String Name = "";
        public String[] XmlTagNames = { };
        public Func<XmlNode, XbrlTaxonomyDocument, bool> Handler = null;

        public XmlNodeHandler() { }

        //public XmlNodeHandler(string Name, Func<XmlNode, XbrlTaxonomyDocument, bool> Handler)
        //{
        //    this.Name=Name;
        //    this.Handler = Handler;
        //}

        public XmlNodeHandler(string[] XmlTagNames, Func<XmlNode, XbrlTaxonomyDocument, bool> Handler)
        {
            var sb = new StringBuilder();
            var tags = new List<string>();
            this.XmlTagNames = XmlTagNames.Select(i=>i.ToLower()).ToArray();
            foreach (var tagname in XmlTagNames) 
            {
                var tagnamewithoutnamespace = tagname;
                if (tagname.Contains(":")) 
                {
                    tagnamewithoutnamespace = tagname.Substring(tagname.IndexOf(":") + 1);
                }
                var existing = tags.FirstOrDefault(i => i == tagnamewithoutnamespace);
                if (existing == null) 
                {
                    existing = tagnamewithoutnamespace;
                    tags.Add(existing);
                    sb.Append(existing);
                    sb.Append(",");
                }
            }
            this.Name = sb.ToString();
            this.Handler = Handler;
        }

        public bool Handle(XmlNode node, XbrlTaxonomyDocument taxonomydocument) 
        {
            if (node.Name.ToLower().In(XmlTagNames))
            {
                return Handler(node, taxonomydocument);
            }
            return false;
        }

        public bool HandleDocument(XbrlTaxonomyDocument taxonomydocument)
        {
            var alltags = taxonomydocument.XmlDocument.GetElementsByTagName("*").Cast<XmlNode>();
            var tags = alltags.Where(i => i.Name.ToLower().In(XmlTagNames));

            foreach (var tag in tags)
            {
                Handle(tag, taxonomydocument);
            }
            return true;
        }

        public override bool HandleTaxonomy(LogicalModel.Taxonomy taxonomy)
        {
            var tax = (XbrlTaxonomy)taxonomy;
            var documents = tax.TaxonomyDocuments.Where(i => i.TagNames.Intersect(XmlTagNames).Any());

            foreach (var doc in documents) 
            {
                HandleDocument(doc);
            }
            return true;
        }
    }

}
