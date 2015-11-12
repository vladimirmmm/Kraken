using LogicalModel;
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

namespace Model.InstanceModel
{
    public class XbrlInstance: LogicalModel.Instance
    {
        protected string TemplateFileName = "InstanceTemplate.xml";

        public Link SchemaRef { get; set; }


        private List<Context> _Contexts = new List<Context>();
        [JsonProperty]
        public List<Context> Contexts
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
            this.TaxonomyModuleReference = this.SchemaRef.Href;

        }
        public void LoadComplex() 
        {
            var XbrlTaxonomy= (XBRLProcessor.Models.XbrlTaxonomy)this.Taxonomy;
            Clear();
            var allnodes = Utilities.Xml.AllNodes(XmlDocument);

            this.Contexts.Clear();
            var contextnodes = allnodes.Where(i => i.Name.ToLower()=="context").ToList();
            foreach (var contextnode in contextnodes)
            {
                var item = new Context();
                Mappings.CurrentMapping.Map<Context>(contextnode, item);
                this.Contexts.Add(item);
            }
            var conceptnslist = Taxonomy.Concepts.Select(i => i.Value).Select(i => i.Namespace).Distinct().ToList();
            foreach (var conceptns in conceptnslist)
            {
                var factnodes = allnodes.Where(i => i.Name.StartsWith(conceptns + ":")).ToList();
                foreach (var factnode in factnodes)
                {
                    var item = new XbrlFact();
                    Mappings.CurrentMapping.Map<XbrlFact>(factnode, item);
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
            this.FilingIndicators = this.XbrlFilingIndicators.Select(i=>i.Value).ToList();
            foreach (var xbrlfact in XbrlFacts) 
            {
                var xbrlcontext = this.Contexts.FirstOrDefault(i => i.ID == xbrlfact.ContextRef);
                var logicalfact = new InstanceFact();
                logicalfact.UnitID = xbrlfact.UnitRef;
                logicalfact.ContextID = xbrlfact.ContextRef;
                logicalfact.Decimals = xbrlfact.Decimals;
                logicalfact.Unit = this.Units.FirstOrDefault(i => i.ID == xbrlfact.UnitRef);
                
                var factstring = "";
                factstring = String.Format("{0},",xbrlfact.Concept);
                var factkey = factstring;
                if (xbrlcontext.ID=="c-108")
                {
                }
                logicalfact.Concept = new Concept();
                logicalfact.Concept.Content = xbrlfact.Concept;
                if (xbrlcontext.Scenario != null)
                {
                    var dimensions = xbrlcontext.Scenario.Dimensions.OrderBy(i => i.DomainMemberFullName);
                    foreach (var dimension in dimensions)
                    {
                        var dimitem = String.Format("{0},", dimension.DomainMemberFullName.Trim());
                        if (dimitem.Contains("STA")) 
                        {

                        }
                        var dimitemforkey = String.Format("{0},", dimension.DimensionItemWithDomain.Trim());
                        factstring += dimitem;
                        factkey += dimension.IsTyped ? dimitemforkey : dimitem;
                    }
                }
                logicalfact.SetFromString(factstring);
 
                if (logicalfact.Concept == null) 
                {
                }
                //logicalfact.FactString = factstring;
                logicalfact.FactKey = logicalfact.GetFactKey();
                logicalfact.FactString = logicalfact.GetFactString();
                logicalfact.Value = xbrlfact.Value;
                this.Facts.Add(logicalfact);
                if (!this.FactDictionary.ContainsKey(logicalfact.FactKey))
                {
                    this.FactDictionary.Add(logicalfact.FactKey, new List<InstanceFact>() { logicalfact });
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
                var micontext = Contexts.FirstOrDefault(i => i.ID == miconceptfact.ContextID);
                var miunit = Units.FirstOrDefault(i => i.ID == miconceptfact.UnitID);
                var measurestring = miunit.Measure.Content.ToLower();
                var taxunit = Taxonomy.Units.FirstOrDefault(i => i.Measure.Content.ToLower() == measurestring);
                ReportingMonetaryUnit = taxunit;
            }
            var firstcontext = Contexts.FirstOrDefault();
            if (firstcontext != null)
            {
                this.Entity = firstcontext.Entity;
                this.ReportingDate = firstcontext.Period.Instant;
                this.ReportingPeriod = firstcontext.Period;
            }

            SetCells();

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

        public override void SetTaxonomy(Taxonomy xbrlTaxonomy)
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

            foreach (var context in Contexts)
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
           System.IO.File.WriteAllText(filepath, xmlstring);
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
                    AddNamespace(dim.Domain, namespaces);
                }

            }
            AddNamespace("find", namespaces);
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
            var ns="";
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
                var itemtype = fact.Concept.ItemType;
                var setting = Taxonomy.Module.UserSettings.ItemTypeSettings.FirstOrDefault(i => i.ItemType == itemtype);
                if (!String.IsNullOrEmpty(setting.UnitID))
                { 

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
                    InstanceUnit unit = Taxonomy.Units.FirstOrDefault(i => i.ID == unitid);
                  
                    if (taxconcept.ItemType == "monetaryItemType") 
                    {
                        unit = this.ReportingMonetaryUnit;
                    }
                    if (unit != null)
                    {
                        var xbrlunit = new InstanceUnit();
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
                if (itemtypesetting.Type == TypeEnum.Numeric) 
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
            var filingcontext = GetContext(new InstanceFact());
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
                        
                        this.FilingIndicators.Add(find);
                       
                    }
                }
            }
            //return finds.Select(i=>i.Key).ToList();
        }
        
        protected string GetNewID(IEnumerable<LogicalModel.Base.Identifiable> items, string format)
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

        private Context GetContext(InstanceFact fact) 
        {
            var contextcount = this.Contexts.Count;
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
