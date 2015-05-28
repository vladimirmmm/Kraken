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
using XBRLProcessor.Model.Base;
using Model.DefinitionModel;
using BaseModel;
using LogicalModel.Expression;
using XBRLProcessor.Validation;

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
        public List<XbrlValidation> Validations = new List<XbrlValidation>();

      

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
        
            }
            return false;
        }

        public bool AddFactToDictionary(LogicalModel.Fact fact)
        {
            //if (!this.Facts.ContainsKey(fact.FactString))
            //{
            //    this.TaxonomyLabelDictionary.Add(fact.FactString, fact);
            //    return true;
            //}
            //else
            //{

            //}
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

        public override void LoadFactDictionary()
        {
            foreach (var fact in this.Facts)
            {
                //AddFactToDictionary(fact);
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
            Console.WriteLine("Load Hierarchies");

            if (!System.IO.File.Exists(TaxonomyHierarchyPath))
            {
                var hierdocs = this.TaxonomyDocuments.Where(i => i.FileName.ToLower() == "hier.xsd");
                var hierarchies = new List<BaseModel.Hierarchy<Locator>>();
                foreach (var hierdoc in hierdocs)
                {
                    var ns = GetTargetNamespace(hierdoc.XmlDocument);
                    var hierdefdoc = hierdoc.References.FirstOrDefault(i => i.FileName == "hier-def.xml");

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
                        deflink.DefinitionRoot.Item.Role = deflink.Role;
                        hierarchies.Add(deflink.DefinitionRoot);
                        int z = 0;
                    }

                }

                var sb = new StringBuilder();
                foreach (var hier in hierarchies)
                {
                    this.Hierarchies.Add(hier.Cast<LogicalModel.Base.QualifiedItem>(Mappings.ToQualifiedItem));
                }

                var jsoncontent = Utilities.Converters.ToJson(this.Hierarchies);
                Utilities.FS.WriteAllText(TaxonomyHierarchyPath, jsoncontent);
                Utilities.FS.WriteAllText(TaxonomyLayoutFolder + "hierarchies.js", "var hierarchies = " + jsoncontent + ";");

            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyHierarchyPath);
                this.Hierarchies = Utilities.Converters.JsonTo<List<Hierarchy<LogicalModel.Base.QualifiedItem>>>(jsoncontent);
            }

            Console.WriteLine("Load Hierarchies completed");

            //Utilities.FS.WriteAllText(this.ModuleFolder + "Hierarchy.json", Utilities.Converters.ToJson(hierarchies));
            //Utilities.FS.WriteAllText(this.ModuleFolder + "Hierarchy.txt", sb.ToString());
        }

        public override void LoadValidations()
        {
            var validationdocuments = this.TaxonomyDocuments.Where(i => i.LocalFolder.EndsWith("\\val\\")).ToList();
            var validations = new List<XbrlValidation>();
            var sb = new StringBuilder();
            var parser = new XbrlFormulaParser();
            FunctionExpression.FunctionProvider = Functions.Current.Find;
            Expression testobj = null;
            var expressionfile = this.ModuleFolder + "expressions.txt";
            foreach (var validdoc in validationdocuments) 
            {
                var node = Utilities.Xml.SelectSingleNode(validdoc.XmlDocument.DocumentElement,"//gen:link");
                var validation = new XbrlValidation();
                Mappings.CurrentMapping.Map<XbrlValidation>(node, validation);
                if (validation.ValueAssertion != null)
                {
                    validation.LoadValidationHierarchy();
                    validations.Add(validation);

                    var obj = parser.ParseExpression(validation.ValueAssertion.Test);
                    var tree = parser.GetTreeString(validation.ValueAssertion.Test);
                    sb.AppendLine(validation.ID);
                    sb.AppendLine(validation.ValueAssertion.Test);
                    sb.AppendLine(obj.Translate(parser));

                    sb.AppendLine(tree.ToHierarchyString(i => i));
                    sb.AppendLine("_______________________________________");
                }
            }
            if (!System.IO.File.Exists(expressionfile)||1==1)
            {
                Utilities.FS.WriteAllText(expressionfile, sb.ToString());
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

            var label = FindLabel(newlabel.Key);

            if (label == null)
            {
                label = newlabel;
                AddLabel(label);

            }
            else 
            {
            }
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

                table.LoadDefinitionHierarchy(logicaltable);
                table.LoadLayoutHierarchy(logicaltable);

             
                //for debug
                //Utilities.FS.WriteAllText(logicaltable.DefPath, table.DefinitionRoot.ToHierarchyString(i => i.ToString()));
                //Utilities.FS.WriteAllText(logicaltable.LayoutPath, logicaltable.LayoutRoot.ToHierarchyString(i => i.ID+"<"+i.FactString+">"));
               
                logicaltable.LoadDefinitions();
                logicaltable.LoadLayout();
                var s = Utilities.Converters.ToJson(logicaltable);
                Utilities.FS.WriteAllText(logicaltable.HtmlPath.Replace(".html", ".json"), s);
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
