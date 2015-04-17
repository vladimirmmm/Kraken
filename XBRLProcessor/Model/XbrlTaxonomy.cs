using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XBRLProcessor.Helpers;
using XBRLProcessor.Literals;
using XBRLProcessor.Mapping;
using Utilities;
using XBRLProcessor.Model;

namespace XBRLProcessor.Models
{
    public class XbrlTaxonomy : LogicalModel.Taxonomy
    {
        private List<XbrlTaxonomyDocument> _TaxonomyDocuments = new List<XbrlTaxonomyDocument>();
        public new List<XbrlTaxonomyDocument> TaxonomyDocuments
        {
            get 
            {
                return _TaxonomyDocuments;
            }
            set 
            {
                _TaxonomyDocuments=value;
            }
        }


        public new XbrlTaxonomyDocument EntryDocument
        {
            get { return (XbrlTaxonomyDocument)base.EntryDocument; }
            set { base.EntryDocument = value; }
        }
        public List<XbrlTable> TaxonomyTables = new List<XbrlTable>();

        public XbrlTaxonomy(string entrypath)
            : base(entrypath)
        {
            EntryDocument = new XbrlTaxonomyDocument(this, entrypath);
            this.TaxonomyDocuments.Add(EntryDocument);

            LabelHandler = new XmlNodeHandler(Tags.Labels, HandleLabel);

            TableHandler = new XmlNodeHandler(Tags.TableContainers, HandleTables);
        }

        public override void LoadAllReferences()
        {
            Console("Load References");

            if (!System.IO.File.Exists(TaxonomyStructurePath))
            {
                EntryDocument.LoadReferences();
                var jsoncontent = Utilities.Converters.ToJson(TaxonomyDocuments);
                System.IO.File.WriteAllText(TaxonomyStructurePath, jsoncontent);
            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyStructurePath);
                this.TaxonomyDocuments = Utilities.Converters.JsonTo<List<XbrlTaxonomyDocument>>(jsoncontent);

                var existing_entry = this.TaxonomyDocuments.FirstOrDefault(i => i.LocalPath == EntryDocument.LocalPath);
                EntryDocument.ReferencedFiles = existing_entry.ReferencedFiles;
                EntryDocument.TagNames = existing_entry.TagNames;
                this.TaxonomyDocuments.Remove(existing_entry);
                this.TaxonomyDocuments.Insert(0, EntryDocument);

                foreach (var td in this.TaxonomyDocuments)
                {
                    foreach (var file in td.ReferencedFiles)
                    {
                        var referenced = this.TaxonomyDocuments.FirstOrDefault(i => i.LocalPath == file);
                        if (referenced != null)
                        {
                            td.References.Add(referenced);
                        }
                    }
                }

            }
            Console("Load References Completed");

        }
        /*
        public override void LoadLabels()
        {
            Console("Load Labels");

            //Save
            if (!System.IO.File.Exists(TaxonomyLabelPath))
            {

                LabelHandler.HandleTaxonomy(this);

                var jsoncontent = Utilities.Converters.ToJson(TaxonomyLabels);
                System.IO.File.WriteAllText(TaxonomyLabelPath, jsoncontent);
            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyLabelPath);
                this.TaxonomyLabels = Utilities.Converters.JsonTo<List<LogicalModel.Label>>(jsoncontent);
            }
            Console("Load Labels Completed");

        }

        public void LoadTables()
        {
            Console("Load Tables");

            if (!System.IO.File.Exists(TaxonomyTablesPath))
            {
                TableHandler.HandleTaxonomy(this);

                var jsoncontent = Utilities.Converters.ToJson(TaxonomyTables);
                System.IO.File.WriteAllText(TaxonomyTablesPath, jsoncontent);
            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyTablesPath);
                this.Tables = Utilities.Converters.JsonTo<List<LogicalModel.Table>>(jsoncontent);

                foreach (var table in this.TaxonomyTables)
                {
                    table.Taxonomy = this;
                }
            }
            Console("Load Tables completed");

        }
        */

        #region Handlers

        public bool HandleLabel(XmlNode node, LogicalModel.TaxonomyDocument taxonomydocument)
        {
            var result = true;
            var labelid = node.Attributes[Attributes.LabelID].Value;
            var labeltext = node.InnerText;
            var role = node.Attributes[Attributes.LabelRole].Value;
            var lang = node.Attributes[Attributes.Language].Value;
            var label = this.TaxonomyLabels.FirstOrDefault(i => i.LabelID == labelid && i.Lang == lang);
            if (label == null)
            {
                label = new LogicalModel.Label();
                this.TaxonomyLabels.Add(label);
                label.LocalID = labelid.StartsWith(Literal.LabelPrefix) ? labelid.Substring(6) : labelid;
                label.LabelID = labelid;
                label.Lang = lang;

            }
            //label.FileName = this.FileName;
            if (role.In(Roles.LabelCodeRoles))
            {
                label.Code = labeltext;
            }
            if (role.In(Roles.LabelTextRoles))
            {
                label.Content = labeltext;
            }

            return result;
        }

        public bool HandleTables(XmlNode node, XbrlTaxonomyDocument taxonomydocument)
        {
            var result = false;

            //checking if this is a Table xsd
            var usedOnNodes = Utilities.Xml.SelectNodes(node, "//" + Tags.UsedOn);
            if (usedOnNodes == null || usedOnNodes.Count == 0)
            {
                return false;
            }
            var used = new List<String>();
            foreach (XmlNode unode in usedOnNodes)
            {
                var content = "";
                if (unode.ChildNodes.Count > 0)
                {
                    content = unode.ChildNodes[0].Value;
                    if (!string.IsNullOrEmpty(content))
                    {
                        content = unode.InnerText.Trim();
                    }
                }

                var existing = used.FirstOrDefault(i => i.ToLower() == content);
                if (existing == null)
                {
                    used.Add(content.ToLower());
                }
            }

            if (used.Contains(Tags.TableLayoutContainer) && used.Contains(Tags.TableDefinitionContainer))
            {
                var table = new XbrlTable();
                table.Taxonomy = this;

                var logicaltable = new LogicalModel.Table();
                logicaltable.Taxonomy = this;


                var linknodes = Utilities.Xml.SelectNodes(node, "//" + Tags.LinkBaseRef);
                var layoutfilename = taxonomydocument.FileName.Replace(".xsd", Literal.RenderFileSuffix).ToLower();
                var definitionfilename = taxonomydocument.FileName.Replace(".xsd", Literal.DefinitionFileSuffix).ToLower();

                table.XsdPath = taxonomydocument.LocalPath;
                var layoutdocument = taxonomydocument.References.FirstOrDefault(i => i.FileName.ToLower() == layoutfilename);
                table.LayoutPath = layoutdocument.LocalPath;
                var definitiondocument = taxonomydocument.References.FirstOrDefault(i => i.FileName.ToLower() == definitionfilename);
                table.DefinitionPath = definitiondocument.LocalPath;

                MapDefinition(definitiondocument.XmlDocument.ChildNodes[0], table);

                MapLayout(layoutdocument.XmlDocument.ChildNodes[0], table);

                table.LoadLayoutHierarchy(logicaltable);
                table.LoadDefinitionHierarchy(logicaltable);
                logicaltable.CreateHtmlLayout();

                this.Tables.Add(logicaltable);
                this.TaxonomyTables.Add(table);
            }

            return result;

        }

        public void MapLayout(XmlNode node, XbrlTable instance)
        {
            var rootnode = Utilities.Xml.SelectSingleNode(node, "//" + Tags.TableLayoutContainer);
            var table = Mappings.CurrentMapping.Map<XbrlTable>(rootnode, instance);
        }

        public void MapDefinition(XmlNode node, XbrlTable instance)
        {
            var rootnode = Utilities.Xml.SelectSingleNode(node, "//" + Tags.LinkBase);
            var table = Mappings.CurrentMapping.Map<XbrlTable>(rootnode, instance);
        }

        #endregion
    }
}
