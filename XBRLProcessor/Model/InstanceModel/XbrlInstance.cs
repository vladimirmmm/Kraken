using LogicalModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using XBRLProcessor.Mapping;
using XBRLProcessor.Model.Base;

namespace Model.InstanceModel
{
    public class XbrlInstance: LogicalModel.Instance
    {


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

            var factnodes = Utilities.Xml.AllNodes(XmlDocument).Where(i => i.Name.StartsWith(this.Taxonomy.ConceptNameSpace+":")).ToList();
            foreach (var factnode in factnodes) 
            {
                var item = new XbrlFact();
                Mappings.CurrentMapping.Map<XbrlFact>(factnode, item);
                this.XbrlFacts.Add(item);
            }

            this.XbrlFilingIndicators.Clear();
            var filingnodes = Utilities.Xml.AllNodes(XmlDocument).Where(i => i.Name.Equals("find:filingIndicator", StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var filingnode in filingnodes)
            {
                var item = new FilingIndicator();
                Mappings.CurrentMapping.Map<FilingIndicator>(filingnode, item);
                this.XbrlFilingIndicators.Add(item);
            }
            LoadLogicalData();
        }



        public void LoadLogicalData() 
        {
            this.Entity = Contexts.FirstOrDefault().Entity;
            this.ReportingDate = Contexts.FirstOrDefault().Period.Instant;
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
                logicalfact.Concept = new Concept();
                logicalfact.Concept.Content = xbrlfact.Concept;
                var dimensions = xbrlcontext.Scenario.Dimensions.OrderBy(i => i.DomainMemberFullName);
                foreach (var dimension in dimensions) 
                {
                    var dimitem = String.Format("{0},", dimension.DomainMemberFullName);
                    var dimitemforkey = String.Format("{0},", dimension.DimensionItemWithDomain);
                    factstring += dimitem;
                    factkey += dimension.IsTyped ? dimitemforkey : dimitem;
                }

                logicalfact.FactString = factstring;
                logicalfact.FactKey = factkey;
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
            SetCells();

            SaveToJson();
        }

        public override List<LogicalModel.Validation.ValidationRuleResult> Validate(List<String> messages)
        {
            var results = new List<LogicalModel.Validation.ValidationRuleResult>();
            messages = messages == null ? new List<String>() : messages;

            if (Taxonomy != null)
            {
                Console.WriteLine("Validating Instance started");
                messages.Add(String.Format("Validation started at {0:" + Utilities.Converters.DateTimeFormat + "}", DateTime.Now));
                var schemaset = new XmlSchemaSet();
                var nsmanager = Utilities.Xml.GetTaxonomyNamespaceManager(this.XmlDocument);
                IDictionary<string, string> dic = nsmanager.GetNamespacesInScope(XmlNamespaceScope.All);
                foreach (var dicitem in dic)
                {
                    //schemaset.Add(dicitem.Key, dicitem.Value);
                    //this.XmlDocument.Schemas.Add(dicitem.Key, dicitem.Value);
                }
                //this.XmlDocument.Validate(OnValidated);

                results.AddRange(base.Validate(messages));

                messages.Add(String.Format("Validation finished at {0:" + Utilities.Converters.DateTimeFormat + "}", DateTime.Now));

                var json_validationresults = Utilities.Converters.ToJson(results);
                Utilities.FS.WriteAllText(Taxonomy.CurrentInstanceValidationResultPath, "var currentvalidationresults = " + json_validationresults + ";");

                var validationresultfilepath = this.FullPath.Remove(this.FullPath.LastIndexOf("."));
                validationresultfilepath = validationresultfilepath + ".ValidationResults.txt";
                var sb = new StringBuilder();
                foreach (var message in messages)
                {
                    sb.AppendLine(message);
                }
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("Validation Errors JSON:");
                sb.AppendLine(json_validationresults);
                Utilities.FS.WriteAllText(validationresultfilepath, sb.ToString());


                Console.WriteLine("Validating Instance finished");
            }
            else
            {
                messages.Add(String.Format("Can't load Taxonomy {0}", this.SchemaRef));
            }
            return results;
        }

        public void OnValidated(Object sender,ValidationEventArgs e) 
        {
            Console.WriteLine("Xml Validation:");
            Console.WriteLine(e.Message);
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
    }
}
