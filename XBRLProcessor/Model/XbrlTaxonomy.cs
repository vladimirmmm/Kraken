﻿using System;
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
using XBRLProcessor.Model.Base;
using Model.DefinitionModel;

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

        protected Dictionary<string, XbrlTaxonomyDocument> TaxonomyDocumentDictionary = new Dictionary<string, XbrlTaxonomyDocument>();

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
           
            this.AddTaxonomyDocument(EntryDocument);

            LabelHandler = new XmlNodeHandler(Tags.Labels, HandleLabel);

            TableHandler = new XmlNodeHandler(Tags.TableContainers, HandleTables);

            ElementHandler = new XmlNodeHandler(Tags.SchemaElements, HandleElements);

            Locator.LocatorFunction = Locate;
            LogicalModel.Label.SetLabelPrefix(Literal.LabelPrefix);
        }
        
        public bool AddTaxonomyDocumentToDictionary(XbrlTaxonomyDocument document)
        {
            if (!this.TaxonomyDocumentDictionary.ContainsKey(document.LocalPath))
            {
                this.TaxonomyDocumentDictionary.Add(document.LocalPath.ToLower(), document);
                return true;
            }
            else
            {
                this.TaxonomyDocumentDictionary[document.LocalPath] = document;
            }
            return false;
        }
        
        public void AddTaxonomyDocument(XbrlTaxonomyDocument document)
        {
            if (AddTaxonomyDocumentToDictionary(document))
            {
                this.TaxonomyDocuments.Add(document);

            }
        }
        
        public bool AddLabelToDictionary(LogicalModel.Label label)
        {
            if (!this.TaxonomyLabelDictionary.ContainsKey(label.Key))
            {
                this.TaxonomyLabelDictionary.Add(label.Key, label);
                return true;
            }
            else 
            {
                int z = 0;
            }
            return false;
        }
        
        public void AddLabel(LogicalModel.Label label)
        {
            if (AddLabelToDictionary(label)) 
            {
                this.TaxonomyLabels.Add(label);

            }
            //if (this.TaxonomyLabels.FirstOrDefault(i => i.Key == label.Key) == null)
            //{
            //    this.TaxonomyLabels.Add(label);

            //}
        }
        
        public override void LoadLabelDictionary()
        {
            foreach (var label in this.TaxonomyLabels)
            {
                AddLabelToDictionary(label);
            }
        }
        
        public XbrlTaxonomyDocument FindDocument(string localpath) 
        {
            localpath = localpath.ToLower();
            if (TaxonomyDocumentDictionary.ContainsKey(localpath)) 
            {
                return TaxonomyDocumentDictionary[localpath];
            }
            return null;
        }

        public override void LoadAllReferences()
        {
            Console.WriteLine("Load References");

            if (!System.IO.File.Exists(TaxonomyStructurePath))
            {
                EntryDocument.LoadReferences();
                var jsoncontent = Utilities.Converters.ToJson(TaxonomyDocuments);
                Utilities.FS.WriteAllText(TaxonomyStructurePath, jsoncontent);
            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyStructurePath);
                this.TaxonomyDocuments = Utilities.Converters.JsonTo<List<XbrlTaxonomyDocument>>(jsoncontent);
                foreach (var doc in this.TaxonomyDocuments)
                {
                    this.AddTaxonomyDocumentToDictionary(doc);
                }
                var existing_entry = this.FindDocument(EntryDocument.LocalPath);
                EntryDocument.ReferencedFiles = existing_entry.ReferencedFiles;
                EntryDocument.TagNames = existing_entry.TagNames;
                this.TaxonomyDocuments.Remove(existing_entry);
                this.TaxonomyDocuments.Insert(0, EntryDocument);

                foreach (var td in this.TaxonomyDocuments)
                {
                    foreach (var file in td.ReferencedFiles)
                    {
                        var referenced = this.FindDocument(file);
                        if (referenced != null)
                        {
                            td.References.Add(referenced);
                        }
                    }
                }

            }
            Console.WriteLine("Load References Completed");

        }

        public override void LoadHierarchy()
        {
            var hierdocs = this.TaxonomyDocuments.Where(i => i.FileName.ToLower() == "hier.xsd");
            foreach (var hierdoc in hierdocs) 
            {
                var ns = GetTargetNamespace(hierdoc.XmlDocument);
                var hierdefdoc = hierdoc.References.FirstOrDefault(i=>i.FileName=="hier-def.xml");

                var node = hierdefdoc.XmlDocument.DocumentElement;
                var hier = new Hier();
                Mappings.CurrentMapping.Map<Hier>(node, hier);
                foreach (var deflink in hier.DefinitionLinks) 
                {
                    deflink.LoadHierarchy();
                    foreach (var loc in deflink.Locators) 
                    {
                        var path = Utilities.Strings.ResolveRelativePath(hierdefdoc.LocalFolder, loc.Href);
                        var loc_doc = FindDocument(path);
                        ns = GetTargetNamespace(loc_doc.XmlDocument);
                        loc.Namespace = ns;
                        loc.Locate();
                    }
                    int z = 0;
                }

            }
        }

        public override void LoadConcepts()
        {
            var conceptelements = this.SchemaElements.Where(i => i.Namespace == "eba_met");
            foreach (var conceptelement in conceptelements)
            {
                var concept = new LogicalModel.Concept();
                concept.Namespace = conceptelement.Namespace;
                concept.Name = conceptelement.Name;
                //var 
            }
        }

        #region Handlers

        public bool HandleLabel(XmlNode node, LogicalModel.TaxonomyDocument taxonomydocument)
        {
            var result = true;
            var labelid = Utilities.Xml.Attr(node,Attributes.LabelID);
            var labeltext = node.InnerText;
            var role = Utilities.Xml.Attr(node,Attributes.LabelRole);
            var lang = Utilities.Xml.Attr(node,Attributes.Language);
            var FileID = Utilities.Strings.GetFolderName(taxonomydocument.LocalPath);
            if (FileID.Contains("-lab-")) 
            {
                FileID = FileID.Remove(FileID.IndexOf("-lab-") + 5);
            }
            var newlabel = new LogicalModel.Label();
            newlabel.LabelID = labelid;
            newlabel.Lang = lang;
            newlabel.FileName = FileID;

            //var label = this.TaxonomyLabels.FirstOrDefault(i => i.LabelID == labelid && i.Lang == lang
            //    && i.FileName == FileID
            //    );
            var label = FindLabel(newlabel.Key);

            if (label == null)
            {
                label = newlabel;
                AddLabel(label);
                //label.LocalID = labelid.StartsWith(Literal.LabelPrefix) ? labelid.Substring(6) : labelid;

            }
            else 
            {
                //label=
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
                logicaltable.FolderName = Utilities.Strings.GetFolderName(table.XsdPath);

                var layoutdocument = taxonomydocument.References.FirstOrDefault(i => i.FileName.ToLower() == layoutfilename);
                table.LayoutPath = layoutdocument.LocalPath;
                var definitiondocument = taxonomydocument.References.FirstOrDefault(i => i.FileName.ToLower() == definitionfilename);
                table.DefinitionPath = definitiondocument.LocalPath;

                MapDefinition(definitiondocument.XmlDocument.ChildNodes[0], table);

                foreach (var definitionlink in table.DefinitionLinks) 
                {
                    foreach (var locator in definitionlink.Locators)
                    {
                        if (!string.IsNullOrEmpty(locator.Href)) 
                        {
                            //var path = Utilities.Strings.GetLocalPath(this.LocalFolder, locator.Href);
                            var localhref = locator.Href;
                            if (!Utilities.Strings.IsRelativePath(localhref))
                            {
                                localhref = Utilities.Strings.GetLocalPath(this.LocalFolder, locator.Href);
                            }
                            var path = Utilities.Strings.ResolveRelativePath(definitiondocument.LocalFolder, localhref);
                            var defdoc = definitiondocument.References.FirstOrDefault(i => i.LocalPath == path);
                            locator.Namespace = GetTargetNamespace(defdoc.XmlDocument); 
     
                        }

                    }
                }
              

                MapLayout(layoutdocument.XmlDocument.ChildNodes[0], table);

                //table.LoadLayoutHierarchy(logicaltable);
                table.LoadDefinitionHierarchy(logicaltable);
                table.LoadLayoutHierarchy(logicaltable);

                logicaltable.LoadDefinitions();
                logicaltable.LoadLayout();

                this.Tables.Add(logicaltable);
                this.TaxonomyTables.Add(table);
            }

            return result;

        }

        private LogicalModel.Base.Element Locate(string key) 
        {
            LogicalModel.Base.Element element = null;
            if (this.SchemaElementDictionary.ContainsKey(key))
            {
                element = this.SchemaElementDictionary[key];
            }
            else 
            {
                
            }
            return element;
        }
        
        public string GetTargetNamespace(XmlDocument doc) 
        {
            var ns = "";
            var targetnamespace = Utilities.Xml.Attr(doc.DocumentElement, Literals.Attributes.TargetNamespace);
            if (targetnamespace != null)
            {
                foreach (XmlAttribute attr in doc.DocumentElement.Attributes)
                {
                    if (attr.Name != Literals.Attributes.TargetNamespace && String.Equals(attr.Value, targetnamespace, StringComparison.InvariantCultureIgnoreCase))
                    {
                        ns = attr.LocalName;
                        break;
                    }

                }
            }
            return ns;
        }
        
        public bool HandleElements(XmlNode node, XbrlTaxonomyDocument taxonomydocument) 
        {
            var element = new Element();
            if (!String.IsNullOrEmpty(Utilities.Xml.Attr(node,Literals.Attributes.ID)))
            {
                Mappings.CurrentMapping.Map(node, element);

                element.Namespace = GetTargetNamespace(node.OwnerDocument);

                var logicalelement = Mappings.ToLogical(element);
                this.SchemaElements.Add(logicalelement);
                this.SchemaElementDictionary.Add(logicalelement.Key, logicalelement);
            }
            return true;
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
