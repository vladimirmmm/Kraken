using LogicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XBRLProcessor.Mapping;
using XBRLProcessor.Model.Base;

namespace Model.InstanceModel
{
    public class XbrlInstance: LogicalModel.Instance
    {


        public Link SchemaRef { get; set; }


        private List<Context> _Contexts = new List<Context>();
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

        public void Load() 
        {
            Mappings.CurrentMapping.Map<XbrlInstance>(XmlDocument.DocumentElement, this);

            var factnodes = Utilities.Xml.AllNodes(XmlDocument).Where(i => i.Name.StartsWith("eba_met:")).ToList();
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
                var logicalfact = new Fact();
                logicalfact.UnitID = xbrlfact.UnitRef;
                logicalfact.ContextID = xbrlfact.ContextRef;
                logicalfact.Unit = this.Units.FirstOrDefault(i => i.ID == xbrlfact.UnitRef);
                
                var factkey = "";
                factkey = String.Format("{0},",xbrlfact.Concept);
                logicalfact.Concept = xbrlfact.Concept;
                var dimensions = xbrlcontext.Scenario.Dimensions.OrderBy(i => i.DomainMemberFullName);
                foreach (var dimension in dimensions) 
                {
                    var dimitem = String.Format("{0},", dimension.DomainMemberFullName);
                    factkey += dimitem;
                }

                logicalfact.FactString = factkey;
                logicalfact.Value = xbrlfact.Value;
                this.Facts.Add(logicalfact);

            }
        }

        public override bool Validate()
        {
            Console.WriteLine("Validating Instance started");

            var isvalid = true;

            var basevalidation = base.Validate();
            Console.WriteLine("Validating Instance finished");

            return isvalid && basevalidation;
        }

        private void Test() 
        {
            var instancepath = @"C:\Users\vladimir.balacescu\Desktop\Delivery\C08 reports_10_28_2014.xbrl";
            var instance = new XbrlInstance(instancepath);
            instance.Load();

            int z = 0;
        }
    }
}
