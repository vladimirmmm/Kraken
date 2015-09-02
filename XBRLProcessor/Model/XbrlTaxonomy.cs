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
using LogicalModel.Expressions;
using System.CodeDom.Compiler;
using System.Reflection;
using Model.InstanceModel;

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
            if (!this.TaxonomyDocumentDictionary.ContainsKey(document.LocalRelPath))
            {
                this.TaxonomyDocumentDictionary.Add(document.LocalRelPath.ToLower(), document);
                return true;
            }
            else
            {
                this.TaxonomyDocumentDictionary[document.LocalRelPath] = document;
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

        public bool AddFactToDictionary(LogicalModel.Base.FactBase fact)
        {
            return false;
        }
        
        public void AddLabel(LogicalModel.Label label)
        {
            if (AddLabelToDictionary(label)) 
            {
                this.TaxonomyLabels.Add(label);

            }

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
            FactsOfConcepts.Clear();
            foreach (var fact in this.Facts)
            {
                //AddFactToDictionary(fact);
                var conceptkey = fact.Key.Remove(fact.Key.IndexOf(","));
                List<string> keylist = null;
                if (!FactsOfConcepts.ContainsKey(conceptkey))
                {
                    keylist = new List<string>();
                    FactsOfConcepts.Add(conceptkey, keylist);
                }
                else 
                {
                    keylist = FactsOfConcepts[conceptkey];
                }
                keylist.Add(fact.Key);
            }
        }
        
        public string FindDimensionDomain(string dimensionitem) 
        {
            var domain = "";
    
            LogicalModel.Base.Element se = null;
            if (!this.SchemaElementDictionary.ContainsKey(dimensionitem))
            {
                var ebadimensionkey = "";
                if (dimensionitem.Contains(":"))
                {
                    var items = dimensionitem.Split(':');
                    if (!items[1].StartsWith(Prefix))
                    {
                        items[1] = Prefix + items[1];
                    }
                    ebadimensionkey = String.Format("{0}:{1}", items[0], items[1]);
                }
                if (this.SchemaElementDictionary.ContainsKey(ebadimensionkey))
                {
                    se = this.SchemaElementDictionary[ebadimensionkey];
                }
            }
            else 
            {
                se = this.SchemaElementDictionary[dimensionitem];

            }
            if (se!=null)
            {
                var domainref = se.TypedDomainRef;
                var se_dim_doc = this.TaxonomyDocuments.FirstOrDefault(i => i.TargetNamespace == se.Namespace);
                //TODO
                var path = Utilities.Strings.ResolveRelativePath(se_dim_doc.LocalFolder, domainref);
                var se_domain_doc = FindDocument(path);
                var refid = domainref.Substring(domainref.IndexOf("#") + 1);
                var se_domain_key = se_domain_doc.TargetNamespace + ":" + refid;
                var se_domain = SchemaElementDictionary[se_domain_key];
                //domain = se_domain.ID;
                domain = String.Format("{0}:{1}",se_domain.Namespace, se_domain.Name);
            }
            return domain;
        }
        
        public XbrlTaxonomyDocument FindDocument(string localpath) 
        {
            localpath = localpath.ToLower();
            var localtaxfolder = LogicalModel.TaxonomyEngine.LocalFolder.ToLower();
            var localrelpath = Utilities.Strings.GetRelativePath(localtaxfolder, localpath);
            if (TaxonomyDocumentDictionary.ContainsKey(localrelpath)) 
            {
                return TaxonomyDocumentDictionary[localrelpath];
            }
            return null;
        }

        public override void LoadAllReferences()
        {
            Console.WriteLine("Load References");

            if (!System.IO.File.Exists(TaxonomyStructurePath) || LogicalModel.Settings.Current.ReloadFullTaxonomy)
            {
                EntryDocument.LoadReferences();

                var taxpath = SourceTaxonomyFolder + "tax.xsd";
                EntryDocument.LoadTaxonomyDocument(taxpath);
                //EntryDocument.ReferencedFiles
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
                var existing_entry = this.FindDocument(EntryDocument.LocalRelPath);
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

        public override void LoadUnits()
        {
            Console.WriteLine("Load Units started");

            if (!System.IO.File.Exists(TaxonomyUnitPath) || LogicalModel.Settings.Current.ReloadFullTaxonomyButStructure)
            {
                this.Units.Clear();
                var docs = this.TaxonomyDocuments.Where(i => i.TagNames.Contains("unit")).ToList();
                var utrpath = "www.xbrl.org/utr/utr.xml";
                if (docs.FirstOrDefault(i => i.SourcePath == utrpath) == null) 
                {
                    var utrdoc = new XbrlTaxonomyDocument(this, utrpath);
                    docs.Add(utrdoc);
                }

                var selector = "//*[local-name()='unit']";
                var items = new List<XbrlUnit>();
                foreach (var doc in docs)
                {
                    var ns = GetTargetNamespace(doc.XmlDocument);

                    var nodes = Utilities.Xml.SelectNodes(doc.XmlDocument.DocumentElement, selector);
                    foreach (var node in nodes) 
                    {
                        var xbrlunit = new XbrlUnit();
                        Mappings.CurrentMapping.Map<XbrlUnit>(node, xbrlunit);
                        var logicalunit = Mappings.ToLogical(xbrlunit);
                        this.Units.Add(logicalunit);

                    }

                }

               

                var jsoncontent = Utilities.Converters.ToJson(this.Units);
                Utilities.FS.WriteAllText(TaxonomyUnitPath, jsoncontent);

            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyUnitPath);
                this.Units = Utilities.Converters.JsonTo<List<LogicalModel.Unit>>(jsoncontent);
            }

            Console.WriteLine("Load Units completed");

        }
       
        public override void LoadGeneral()
        {
            var taxelement = this.SchemaElements.FirstOrDefault(i => i.Type == "model:taxonomyType");
         
            if (taxelement != null) 
            {
                var taxfoldername = Utilities.Strings.GetFolderName(this.SourceTaxonomyFolder);
                var l = this.FindLabel(LogicalModel.Label.GetKey(taxfoldername, taxelement.ID));

                this.Module.FromDate = taxelement.FromDate;
                this.Module.ToDate = taxelement.ToDate;
                this.Module.TaxonomyID = taxelement.Name;
                this.Module.TaxonomyName = l != null ? l.Content : taxelement.Name;
  
            }
            var modulelement = this.SchemaElements.FirstOrDefault(i => i.Type == "model:moduleType");
            if (modulelement != null)
            {
                var l = this.FindLabel(LogicalModel.Label.GetKey("mod", modulelement.ID));

                this.Module.ID = modulelement.Name;
                this.Module.Name = l != null ? l.Content : modulelement.Name;
                this.Module.SchemaRef = this.SourceTaxonomyPath;

            }
        }
        public override void LoadHierarchy()
        {
            Console.WriteLine("Load Hierarchies");

            if (!System.IO.File.Exists(TaxonomyHierarchyPath) || LogicalModel.Settings.Current.ReloadFullTaxonomyButStructure)
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
         
                    }

                }

                var sb = new StringBuilder();
                foreach (var hier in hierarchies)
                {
                    var nsdoc = this.TaxonomyDocuments.FirstOrDefault(i => i.TargetNamespace == hier.Item.Namespace);
                    var foldername = Utilities.Strings.GetFolderName(nsdoc.LocalPath);
                    hier.Item.NamespaceFolder = foldername;
                    //var hierchildrens = hier.All();
                    //foreach (var hierchildren in hierchildrens) 
                    //{
                    //    hierchildren.Item.NamespaceFolder = foldername;
                    //}
                    var logicalhierarchy = hier.Cast<LogicalModel.Base.QualifiedItem>(Mappings.ToQualifiedItem);
                    this.Hierarchies.Add(logicalhierarchy);
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

        public override void PopulateTableGroups()
        {
            var filingindicators = new List<String>();
            var filingindicatorsdoc = this.TaxonomyDocuments.FirstOrDefault(i => i.FileName == "find-params.xml");
            if (filingindicatorsdoc != null)
            {
                var genlinknode = Utilities.Xml.SelectSingleNode(filingindicatorsdoc.XmlDocument.DocumentElement, "//gen:link");
                if (genlinknode != null)
                {
                    foreach (XmlNode node in genlinknode) 
                    {
                        if (node.Name.ToLower() == "variable:parameter") 
                        {
                            filingindicators.Add(Utilities.Xml.Attr(node, "id"));
                        }
                    }
                }
            }
            var predocpath = this.EntryDocument.LocalRelPath.Replace(".xsd","-pre.xml");
            var predoc = this.TaxonomyDocumentDictionary[predocpath];
            if (predoc != null) 
            {
                var genlinknode = Utilities.Xml.SelectSingleNode(predoc.XmlDocument.DocumentElement, "//gen:link");
                if (genlinknode != null) 
                {
                    var hier = new XbrlHierarchy<LogicalModel.Base.IdentifiablewithLabel>();
                    hier.LoadFromXml(genlinknode);
                    foreach (var h in hier.Items) 
                    {
                        var folder = Utilities.Strings.GetFolderName(predoc.LocalFolder);
                        folder = "tab";
                        var labelid = h.LabelID;
                        if (labelid.StartsWith("loc_"))
                        {
                            labelid = labelid.Substring(4);
                        }
                        h.ID = labelid;
                        var labelkey = LogicalModel.Label.GetKey(folder, labelid);
                        var label = FindLabel(labelkey);
                        if (label != null) 
                        {
                            h.Label = label;
                        }
                    }
                    var hierarchy = hier.GetHierarchy();
                    var leafs = hierarchy.GetLeafs();
                    this.Module.TableGroups.Clear();

                    foreach (var leaf in leafs) 
                    {
                        var parent = leaf.Parent.Item;
                        var parentID = parent.LabelID.ToLower();
                        var tableID= leaf.Item.ID;

                        LogicalModel.TableGroup tablegroup = null;
                        tablegroup = this.Module.TableGroups.FirstOrDefault(i => i.ID == parent.ID);
                        if (tablegroup == null) 
                        {
                            tablegroup = new LogicalModel.TableGroup();
                            tablegroup.ID = parent.ID;
                            tablegroup.LabelID = parent.LabelID;
                            tablegroup.Label = parent.Label;
                            var find = filingindicators.FirstOrDefault(i => parentID.Contains(i.ToLower()));
                            tablegroup.FilingIndicator = find;
                            this.Module.TableGroups.Add(tablegroup);

                        }
                        tablegroup.TableIDs.Add(leaf.Item.ID);

                    }

                }
            }
        } 

        public override void LoadValidationFunctions()
        {
            Console.WriteLine("Loading Validations started");
            if (!System.IO.File.Exists(TaxonomyValidationPath) || LogicalModel.Settings.Current.ReloadFullTaxonomyButStructure)
            {
                var validationdocuments = this.TaxonomyDocuments.Where(i => i.LocalFolder.EndsWith("\\val\\")).ToList();
                var validations = new List<XbrlValidation>();
                var sb = new StringBuilder();
                var parser = new XbrlFormulaParser();
                var csparser = new CSharpParser();
    
                var expressionfile = this.ModuleFolder + "expressions.txt";
                Utilities.FS.WriteAllText(TaxonomyTestPath, "");

                foreach (var validdoc in validationdocuments)
                {
                    var node = Utilities.Xml.SelectSingleNode(validdoc.XmlDocument.DocumentElement, "//gen:link");
                    var validation = new XbrlValidation();
                    validation.Taxonomy = this;
                    Mappings.CurrentMapping.Map<XbrlValidation>(node, validation);
                    if (validation.ValueAssertion != null)
                    {
          
                        validation.LoadValidationHierarchy();
                      
                        var logicalrule = validation.GetLogicalRule();
                        validations.Add(validation);
                        logicalrule.RootExpression = parser.ParseExpression(validation.ValueAssertion.Test);
                        var translated = csparser.GetFunction(logicalrule);
                        logicalrule.FunctionString = translated;
                        this.ValidationRules.Add(logicalrule);
           
                        var tree = parser.GetTreeString(validation.ValueAssertion.Test);
                        sb.AppendLine(validation.ID);
                        sb.AppendLine(validation.ValueAssertion.Test);
                        sb.AppendLine(validation.ValidationRoot.ToHierarchyString(i => i.ToString()));

                    }
                }

                this.ValidationRules = this.ValidationRules.OrderBy(i => i.ID).ToList();

                if (!System.IO.File.Exists(expressionfile))
                {
                    Utilities.FS.WriteAllText(expressionfile, sb.ToString());
                }
                var jsoncontent = Utilities.Converters.ToJson(this.ValidationRules);
                Utilities.FS.WriteAllText(this.TaxonomyValidationPath, jsoncontent);

            }
            else 
            {
                var jsoncontent = System.IO.File.ReadAllText(this.TaxonomyValidationPath);
                this.ValidationRules = Utilities.Converters.JsonTo<List<LogicalModel.Validation.ValidationRule>>(jsoncontent);
                foreach (var rule in this.ValidationRules) 
                {
                    rule.SetTaxonomy(this);
                    foreach (var p in rule.Parameters) 
                    {
                        p.SetMyFactBase();
                    }
                }
                
           
            }
            if (!System.IO.File.Exists(TaxonomySimpleValidationPath) || LogicalModel.Settings.Current.ReloadFullTaxonomyButStructure)
            {
                var jsoncontent = Utilities.Converters.ToJson(this.SimpleValidationRules);
                Utilities.FS.WriteAllText(this.TaxonomySimpleValidationPath, jsoncontent);
            }
            //GenerateCSFile();
            //CompileCSFile();
            base.GenerateValidationFunctions();
            base.LoadValidationFunctions();
            //fore
            Console.WriteLine("Loading Validations completed");

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
                                localhref = Utilities.Strings.GetLocalPath(XbrlEngine.LocalFolder, locator.Href);
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

                var tablegroup = this.Module.TableGroups.FirstOrDefault(i => i.TableIDs.Contains(logicaltable.ID));
                if (tablegroup != null)
                {
                    logicaltable.FilingIndicator = tablegroup.FilingIndicator;
                }
                else 
                {

                }
             
                //for debug
                Utilities.FS.WriteAllText(logicaltable.DefPath, table.DefinitionRoot.ToHierarchyString(i => i.ToString()));
                //Utilities.FS.WriteAllText(logicaltable.LayoutPath, logicaltable.LayoutRoot.ToHierarchyString(i => i.ID+"<"+i.FactString+">"));
               
                logicaltable.LoadDefinitions();
                logicaltable.LoadLayout();
                //var s = Utilities.Converters.ToJson(logicaltable);

                this.Module.TablePaths.Add(logicaltable.JsonFileName);
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
            if (!String.IsNullOrEmpty(Utilities.Xml.Attr(node, Literals.Attributes.ID)))
            {
                Mappings.CurrentMapping.Map(node, element);

                element.Namespace = GetTargetNamespace(node.OwnerDocument);

                var logicalelement = Mappings.ToLogical(element);
                this.SchemaElements.Add(logicalelement);
                if (logicalelement.Key.Contains("541")) 
                {

                }
                this.SchemaElementDictionary.Add(logicalelement.Key, logicalelement);
            }
            else 
            {
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

        internal void ClearObjects()
        {
            this.Module.Clear();
            this.Facts.Clear();
            this.ValidationRules.Clear();
            this.Tables.Clear();
            this.TaxonomyDocumentDictionary.Clear();
            this.TaxFiles.Clear();
            this.SchemaElements.Clear();
            this.SchemaElementDictionary.Clear();
            this.TaxonomyLabels.Clear();
            this.TaxonomyLabelDictionary.Clear();
            this.ValidationFunctionContainer = null;
            this.FactsOfConcepts.Clear();
            this.TaxonomyDocuments.Clear();
            this.Cells.Clear();
            this.Concepts.Clear();
            this.Hierarchies.Clear();
            this.SimpleValidationRules.Clear();
            GC.Collect();
        }

        public override LogicalModel.Instance GetNewInstance()
        {
            var instance = new XbrlInstance();
            var link = new Link();
            link.Href = this.SourceTaxonomyPath;

            instance.SchemaRef = link;
            instance.Taxonomy = this;
            return instance;
        }
    }
}
