using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using Utilities;
using XBRLProcessor.Mapping;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.InstanceModel;

namespace Model.InstanceModel
{
    public class XbrlInstance: LogicalModel.Instance
    {
        protected string TemplateFileName = "InstanceTemplate.xml";

        public Link SchemaRef { get; set; }


        //private Dictionary<string, Context> _ContextDictionary = new Dictionary<string, Context>();
        //[JsonProperty]
        //public Dictionary<string, Context> ContextDictionary
        //{
        //    get { return _ContextDictionary; }
        //    set { _ContextDictionary = value; }
        //}

        //private List<Context> _Contexts = new List< Context>();
        //[JsonProperty]
        //public List<Context> Contexts
        //{
        //    get { return _Contexts; }
        //    set { _Contexts = value; }
        //}

        private ContextContainer _Contexts = new ContextContainer();
        [JsonProperty]
        public ContextContainer Contexts
        {
            get { return _Contexts; }
            set { _Contexts = value; }
        }

        private List<XbrlFact> _XbrlFacts = new List<XbrlFact>();
        public List<XbrlFact> XbrlFacts
        {
            get { return _XbrlFacts; }
            set { _XbrlFacts = value; }
        }

        //private List<XbrlUnit> _XbrlFacts = new List<XbrlFact>();
        //public List<XbrlFact> XbrlFacts
        //{
        //    get { return _XbrlFacts; }
        //    set { _XbrlFacts = value; }
        //}

        private List<FilingIndicator> _XbrlFilingIndicators = new List<FilingIndicator>();
        public List<FilingIndicator> XbrlFilingIndicators
        {
            get { return _XbrlFilingIndicators; }
            set { _XbrlFilingIndicators = value; }
        }


        private XmlDocument _XmlDocument = null;
        public XmlDocument XmlDocument
        {
            get
            {
                if (_XmlDocument == null && System.IO.File.Exists(this.FullPath))
                {
                    _XmlDocument = new XmlDocument();
                    _XmlDocument.XmlResolver = null;
                    _XmlDocument.Load(this.FullPath);
                }
                return _XmlDocument;
            }
        }
        public XbrlInstance()
        {

        }
        public XbrlInstance(string filepath) 
        {
            this.FullPath = filepath;
        }
        
        public void LoadSimple()
        {
            var xbrlnode = Utilities.Xml.SelectSingleNode(XmlDocument.DocumentElement, "//*[ local-name() = 'xbrl']");

            Mappings.CurrentMapping.Map<XbrlInstance>(xbrlnode, this);
            var cmap = Mappings.CurrentMapping.MappingCollection.FirstOrDefault(i => i.ClassType == typeof(Context));
            var contextxpath = Utilities.Strings.TextBetween(cmap.XmlSelector, "<", ">");
            var contextndoes = Utilities.Xml.SelectChildNodes(xbrlnode, contextxpath);
            xbrlnode = null;
            var ctcontainer = this.Contexts;

            foreach (var cnode in contextndoes) 
            {
               Context ct = new Context();
               ct = cmap.Map(cnode,ct);
               ctcontainer.Add(ct);
            }
            this.TaxonomyModuleReference = this.SchemaRef.Href;

        }
        private void FixContextNamespaces(LogicalModel.InstanceContext ct, XmlNamespaceManager nsm, Dictionary<string, string> domainnsdictionary, List<string> dimensionnamespaces)
        {


            foreach (var dim in ct.FactParts)
            {
                var dparts = dim.DimensionItem.Split(':');
                var dimnsuri = Utilities.Xml.Namespaces[dparts[0]];
                var dimelement = Taxonomy.DimensionItems.FirstOrDefault(i => i.NamespaceURI == dimnsuri);
                dim.DimensionItem = string.Format("{0}:{1}", dimelement.Namespace, dparts[1]);

                if (dim.IsTyped)
                {
                    var parts = dim.Domain.Split(':');

                    var nsuri = Utilities.Xml.Namespaces[parts[0]];
                    var tdecontainer = this.Taxonomy.TypedDimensions.FirstOrDefault(i => i.Value.FirstOrDefault(t => t.NamespaceURI == nsuri) != null);
                    var tde = tdecontainer.Value.FirstOrDefault(t => t.NamespaceURI == nsuri);
                    dim.Domain = string.Format("{0}:{1}", tde.Namespace, parts[1]);

                }
                else
                {
                    if (!domainnsdictionary.ContainsKey(dim.Domain))
                    {
                        var domainelement = this.Taxonomy.SchemaElements.FirstOrDefault(i => i.NamespaceURI == Utilities.Xml.Namespaces[dim.Domain]);
                        domainnsdictionary.Add(dim.Domain, domainelement.Namespace);
                    }
                    dim.Domain = domainnsdictionary[dim.Domain];
                }

            }

        }
      
        public void LoadComplex() 
        {
            var XbrlTaxonomy= (XBRLProcessor.Models.XbrlTaxonomy)this.Taxonomy;
            Clear();
            var allnodes = Utilities.Xml.AllNodes(XmlDocument);
            var nsm = Utilities.Xml.GetTaxonomyNamespaceManager(XmlDocument);
            var dimensionnamespaces = this.Taxonomy.DimensionItems.Select(i => i.NamespaceURI).Distinct().ToList();
            var domainnsdictionary = new Dictionary<string, string>();
            foreach (var ct in Contexts.Items.Values) 
            {
                FixContextNamespaces(ct, nsm, domainnsdictionary, dimensionnamespaces);
            }
            var conceptnsurilist = Taxonomy.Concepts.Select(i => i.Value).Select(i => i.NamespaceURI).Distinct().ToList();
            foreach (var conceptnsuri in conceptnsurilist)
            {
                var sampleconcept = Taxonomy.Concepts.FirstOrDefault(i => i.Value.NamespaceURI == conceptnsuri);
                var factnodes = allnodes.Where(i => i.NamespaceURI == conceptnsuri).ToList();
                foreach (var factnode in factnodes)
                {
                    var item = new XbrlFact();
                    Mappings.CurrentMapping.Map<XbrlFact>(factnode, item);
                    var parts = item.Concept.Split(':');
                    item.Concept = String.Format("{0}:{1}", sampleconcept.Value.Namespace, parts[1]);
                    this.XbrlFacts.Add(item);
                }
            }

            this.XbrlFilingIndicators.Clear();
            var filingnodes = allnodes.Where(i => i.Name.Equals("find:filingIndicator", StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var filingnode in filingnodes)
            {
                var item = new FilingIndicator();
                Mappings.CurrentMapping.Map<FilingIndicator>(filingnode, item);
                this.XbrlFilingIndicators.Add(item);
            }
            LoadLogicalData();
        }

        public override void Clear()
        {
            base.Clear();
            this.XbrlFacts.Clear();
        }

        public void LoadLogicalData() 
        {
     
            this.TaxonomyModuleReference = this.SchemaRef.Href;
            this.FilingIndicators = this.XbrlFilingIndicators.Select(i=> {
                var find = new LogicalModel.FilingIndicator();
                find.ID = i.Value;
                find.Filed = i.Filed;
                return find;
            }).ToList();
            var ix = 0;
            foreach (var xbrlfact in XbrlFacts) 
            {
                var xbrlcontext = this.Contexts.Items[xbrlfact.ContextRef];
                var logicalfact = new LogicalModel.InstanceFact();
                logicalfact.IX = ix;
                ix++;
                logicalfact.UnitID = xbrlfact.UnitRef;
                logicalfact.ContextID = xbrlfact.ContextRef;
                logicalfact.ID = xbrlfact.ID;
                logicalfact.Decimals = xbrlfact.Decimals;
                logicalfact.Unit = this.Units.FirstOrDefault(i => i.ID == xbrlfact.UnitRef);
                if (xbrlcontext!=null){
                    logicalfact.Entity = xbrlcontext.Entity;
                    logicalfact.Period = xbrlcontext.Period;
                }
                var factstring = "";
                factstring = String.Format("{0},",xbrlfact.Concept);
                var factkey = factstring;
  
                logicalfact.Concept = new LogicalModel.Concept();
               
                logicalfact.Concept.Content = xbrlfact.Concept;
                //var conceptns = logicalfact.Concept.Namespace;
                //var uri = Utilities.Xml.Namespaces[conceptns];
                //var taxconcept = Taxonomy.Concepts.FirstOrDefault(i => i.Value.NamespaceURI == uri && i.Value.Name == logicalfact.Concept.Name);
                //logicalfact.Concept = taxconcept.Value;
                if (xbrlcontext.FactParts != null)
                {
                    var dimensions = xbrlcontext.FactParts.OrderBy(i => i.DomainMemberFullName, StringComparer.Ordinal);
                    foreach (var dimension in dimensions)
                    {
                        var dimitem = String.Format("{0},", dimension);
           
                        var dimitemforkey = String.Format("{0},", dimension.DimensionItemWithDomain.Trim());         

                        factstring += dimitem;
                        factkey += dimension.IsTyped ? dimitemforkey : dimitem;
                    }
                }
                logicalfact.SetFromString(factstring);

                //logicalfact.FactString = factstring;
                logicalfact.FactKey = logicalfact.GetFactKey();
                logicalfact.FactString = logicalfact.GetFactString();
                logicalfact.Value = xbrlfact.Value;
                this.Facts.Add(logicalfact);
                if (!this.FactDictionary.ContainsKey(logicalfact.FactKey))
                {
                    this.FactDictionary.Add(logicalfact.FactKey, new List<LogicalModel.InstanceFact>() { logicalfact });
                }
                else 
                {
                    var factlist = this.FactDictionary[logicalfact.FactKey];
                    factlist.Add(logicalfact);
                }
            }

            var miconceptfact = Facts.FirstOrDefault(i => i.Concept.Name.StartsWith("mi"));
            if (miconceptfact != null)
            {
                var micontext = Contexts.Items[miconceptfact.ContextID];
                var miunit = Units.FirstOrDefault(i => i.ID == miconceptfact.UnitID);
                if (miunit != null)
                {
                    var measurestring = miunit.Measure.Content.ToLower();
                    var taxunit = Taxonomy.Units.FirstOrDefault(i => i.Measure.Content.ToLower() == measurestring);
                    ReportingMonetaryUnit = taxunit;
                }
            }
            if (Contexts.Items.Count>0)
            {
                var firstcontext = Contexts.Items.Values.FirstOrDefault();
                this.Entity = firstcontext.Entity;
                this.ReportingDate = firstcontext.Period.Instant;
                this.ReportingPeriod = firstcontext.Period;
            }

            SetCells();
            SetExtensions();
            SaveToJson();
        }

        public override List<LogicalModel.Validation.ValidationRuleResult> Validate(List<String> messages)
        {
            var results = new List<LogicalModel.Validation.ValidationRuleResult>();
            messages = messages == null ? new List<String>() : messages;

            if (Taxonomy != null)
            {
                Logger.WriteLine("Validating Instance started");
                messages.Add(String.Format("Validation started at {0:" + Utilities.Converters.DateTimeFormat + "}", DateTime.Now));
                var schemaset = new XmlSchemaSet();
                var nsmanager = Utilities.Xml.GetTaxonomyNamespaceManager(this.XmlDocument);
                IDictionary<string, string> dic = nsmanager.GetNamespacesInScope(XmlNamespaceScope.All);
      

                results.AddRange(base.Validate(messages));

                messages.Add(String.Format("Validation finished at {0:" + Utilities.Converters.DateTimeFormat + "}", DateTime.Now));

                var json_validationresults = Utilities.Converters.ToJson(results);
                //Utilities.FS.WriteAllText(Taxonomy.CurrentInstanceValidationResultPath, "var currentvalidationresults = " + json_validationresults + ";");
                Utilities.FS.WriteAllText(Taxonomy.CurrentInstanceValidationResultPath, json_validationresults);

         
                var sb = new StringBuilder();
                foreach (var message in messages)
                {
                    sb.AppendLine(message);
                }
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("Validation Errors JSON:");
                sb.AppendLine(json_validationresults);
                if (System.IO.File.Exists(this.FullPath))
                {
                    var validationresultfilepath = this.FullPath.Remove(this.FullPath.LastIndexOf("."));
                    validationresultfilepath = validationresultfilepath + ".ValidationResults.txt";
                    Utilities.FS.WriteAllText(validationresultfilepath, sb.ToString());
                }


                Logger.WriteLine("Validating Instance finished");
            }
            else
            {
                messages.Add(String.Format("Can't load Taxonomy {0}", this.SchemaRef));
            }
            return results;
        }

        public void OnValidated(Object sender,ValidationEventArgs e) 
        {
            Logger.WriteLine("Xml Validation:");
            Logger.WriteLine(e.Message);
        }

        private void Test() 
        {
            var instancepath = @"C:\Users\vladimir.balacescu\Desktop\Delivery\C08 reports_10_28_2014.xbrl";
            var instance = new XbrlInstance(instancepath);
            instance.LoadSimple();
            instance.LoadComplex();

        }

        public override void SetTaxonomy(LogicalModel.Taxonomy xbrlTaxonomy)
        {
            base.SetTaxonomy(xbrlTaxonomy);
        }

        #region Save

        public override void Save(string filepath)
        {
            //base.Save(filepath);
            var namespaces = GetNameSpaces();
            SetContexts();
            SetUnits();
            SetFilingIndicators();
            SetInstanceFacts();

            if (Utilities.FS.FileExists(TemplateFileName))
            {
                var templatecontent = System.IO.File.ReadAllText(TemplateFileName);
                var xmlstring = templatecontent;
                var sb_ns = new StringBuilder();
                foreach (var ns in namespaces)
                {
                    sb_ns.Append(String.Format("xmlns:{0}=\"{1}\" ", ns.Key, ns.Value));
                }
                xmlstring = xmlstring.Replace("#namespaces#", sb_ns.ToString());
                xmlstring = xmlstring.Replace("#taxonomymodulereference#", this.SchemaRef.Href);

                var sb_xml = new StringBuilder();
                foreach (var unit in Units)
                {
                    sb_xml.AppendLine(unit.ToXmlString());

                }

                foreach (var context in Contexts.Items.Values)
                {
                    sb_xml.AppendLine(context.ToXmlString());

                }
                sb_xml.AppendLine("<find:fIndicators>");
                foreach (var find in this.XbrlFilingIndicators)
                {
                    sb_xml.AppendLine(find.ToXmlString());
                }
                sb_xml.AppendLine("</find:fIndicators>");
                foreach (var fact in Facts)
                {
                    sb_xml.AppendLine(fact.ToXmlString());

                }

                xmlstring = xmlstring.Replace("#content#", sb_xml.ToString());
                Utilities.FS.WriteAllText(filepath, xmlstring);
                Utilities.Logger.WriteLine(String.Format("Instance was saved as {0}", filepath));
            }
            else 
            {
                Logger.WriteLine("Error: " + "Template not found: " + TemplateFileName);
            }
        }

        protected Dictionary<String,String> GetNameSpaces() 
        {
            var namespaces = new Dictionary<String, String>();

            foreach (var fact in Facts) 
            {
          
                AddNamespace(fact.Concept.Namespace,namespaces);
                foreach (var dim in fact.Dimensions) 
                {
                    AddNamespace(GetNamespace(dim.DimensionItem), namespaces);
                    AddNamespace(GetNamespace(dim.Domain), namespaces);
                    //AddNamespace(dim.Domain, namespaces);
                }

            }
            return namespaces;
        }
        
        public void AddNamespace(string ns,Dictionary<String, String> dictionary)
        {
            if (!dictionary.ContainsKey(ns))
            {
                var uri = Utilities.Xml.Namespaces.ContainsKey(ns) ? Utilities.Xml.Namespaces[ns] : "";
                dictionary.Add(ns, uri);
            }
        }
       
        private string GetNamespace(string qname) 
        {
            var ns=qname;
            if (qname.Contains(":")) 
            {
                ns = qname.Remove(qname.IndexOf(":"));
            }
            return ns;
        }

        protected void SetInstanceFacts() 
        {
            foreach (var fact in Facts) 
            {
                var concept = this.Taxonomy.Concepts.FirstOrDefault(i => i.Key == fact.Concept.Content);
                var itemtype = concept.Value.ItemType;
                var setting = Taxonomy.Module.UserSettings.ItemTypeSettings.FirstOrDefault(i => i.ItemType == itemtype);
                if (!String.IsNullOrEmpty(setting.UnitID))
                {
                    fact.Decimals = String.Format("{0}", setting.Decimals);
                }

            }
        }

        protected void SetUnits() 
        {
            this.Units.Clear();
            var dict = new Dictionary<string, string>();
            foreach (var fact in Facts)
            {
                var concept = fact.Concept;
                var taxconcept = Taxonomy.Concepts[concept.Content];

                var itemtypesetting = Taxonomy.Module.UserSettings.ItemTypeSettings.FirstOrDefault(i => i.ItemType == taxconcept.ItemType);
                var unitid = itemtypesetting.UnitID;
                
                if (!dict.ContainsKey(taxconcept.ItemType))
                {
                    LogicalModel.InstanceUnit unit = Taxonomy.Units.FirstOrDefault(i => i.ID == unitid);
                  
                    if (taxconcept.ItemType == "monetaryItemType") 
                    {
                        unit = this.ReportingMonetaryUnit;
                    }
                    if (unit != null)
                    {
                        var xbrlunit = new LogicalModel.InstanceUnit();
                        xbrlunit.ID = GetNewID(this.Units, "U_{0}");
                        xbrlunit.Measure = unit.Measure;
                        this.Units.Add(xbrlunit);
                        dict.Add(taxconcept.ItemType, xbrlunit.ID);
                        fact.UnitID = xbrlunit.ID;
                    }
                }
                else 
                {
                    fact.UnitID = dict[taxconcept.ItemType];
                }
                if (itemtypesetting.Type == LogicalModel.TypeEnum.Numeric) 
                {
                    fact.Decimals = String.Format("{0}", itemtypesetting.Decimals);
                }
            }
        }

        protected void SetContexts()
        {
            this.Contexts.Clear();
            var dict = new Dictionary<string, string>();
            foreach (var fact in Facts)
            {
                var dimfact = new LogicalModel.Base.FactBase();
                dimfact.Dimensions.AddRange(fact.Dimensions);
           
                if (!dict.ContainsKey(dimfact.FactString)) 
                { 
                    var context = GetContext(fact);
                    dict.Add(dimfact.FactString,context.ID);
                    this.Contexts.Add(context);
                }
                fact.ContextID = dict[dimfact.FactString];
             
            }
            var filingcontext = GetContext(new LogicalModel.InstanceFact());
            filingcontext.ID = FilingIndicator.DefaultContextID;
            this.Contexts.Add(filingcontext);
        }

        protected void SetFilingIndicators()
        {
            var finds = new Dictionary<string, string>();
            this.FilingIndicators.Clear();
            this.XbrlFilingIndicators.Clear();
            foreach (var fact in Facts)
            {
                var cells = fact.Cells;
                foreach (var cell in cells)
                {
                    var tableid = cell.Remove(cell.IndexOf("<"));

                    if (!finds.ContainsKey(tableid))
                    {
                        var table = this.Taxonomy.Tables.FirstOrDefault(i => i.ID == tableid);
                        var find = table.FilingIndicator;

                        finds.Add(tableid, find);
                        var filingndicator = new FilingIndicator();
                        filingndicator.ContextID = FilingIndicator.DefaultContextID;
                        
                        filingndicator.Value = find;
                        this.XbrlFilingIndicators.Add(filingndicator);
                        var logicalFind = new LogicalModel.FilingIndicator();
                        logicalFind.ID = find;
                        this.FilingIndicators.Add(logicalFind);
                       
                    }
                }
            }
            //return finds.Select(i=>i.Key).ToList();
        }
        
        protected string GetNewID(IEnumerable<BaseModel.Identifiable> items, string format)
        {
            var nr = items.Count();
            var newid = "";
            while (String.IsNullOrEmpty(newid))
            {
                nr++;
                var tempid = String.Format(format, nr);
                var existing = items.FirstOrDefault(i => i.ID == tempid);
                if (existing == null)
                {
                    newid = tempid;
                }
            }
            return newid;
        }

        private Context GetContext(LogicalModel.InstanceFact fact) 
        {
            var contextcount = this.Contexts.Items.Count;
            var contextid = String.Format("CT_{0}", contextcount + 1);

            Context ct = null;
            ct = new Context();
      
            ct.ID = contextid;

            ct.Entity = this.Entity;

            ct.Period = this.ReportingPeriod;

            var scenario = new Scenario();
            foreach (var dimension in fact.Dimensions)
            {
                scenario.Dimensions.Add(dimension);
            }
            ct.Scenario = scenario;
   

        
            return ct;
        }
       

        protected string ToXml()
        {
            return "";
        }

        #endregion

        internal void SetDocument(System.Xml.XmlDocument doc)
        {
            _XmlDocument = doc;
        }
    }
}
