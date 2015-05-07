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
    public class XbrlInstance
    {
        private String _FullPath = "";
        public String FullPath { get { return _FullPath; } set { _FullPath = value; } }

        public Link SchemaRef { get; set; }

        private List<Unit> _Units = new List<Unit>();
        public List<Unit> Units
        {
            get { return _Units; }
            set { _Units = value; }
        }

        private List<Context> _Contexts = new List<Context>();
        public List<Context> Contexts
        {
            get { return _Contexts; }
            set { _Contexts = value; }
        }

        private List<XbrlFact> _Facts = new List<XbrlFact>();
        public List<XbrlFact> Facts
        {
            get { return _Facts; }
            set { _Facts = value; }
        }

        private List<FilingIndicator> _FilingIndicators = new List<FilingIndicator>();
        public List<FilingIndicator> FilingIndicators
        {
            get { return _FilingIndicators; }
            set { _FilingIndicators = value; }
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
                this.Facts.Add(item);
            }

            this.FilingIndicators.Clear();
            var filingnodes = Utilities.Xml.AllNodes(XmlDocument).Where(i => i.Name.Equals("find:filingIndicator", StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var filingnode in filingnodes)
            {
                var item = new FilingIndicator();
                Mappings.CurrentMapping.Map<FilingIndicator>(filingnode, item);
                this.FilingIndicators.Add(item);
            }
        }

        public Instance GetInstance() 
        {
            var instance = new Instance();
            instance.Entity = Contexts.FirstOrDefault().Entity;
            instance.ReportingDate = Contexts.FirstOrDefault().Period.Instant;
            instance.TaxonomyModuleReference = this.SchemaRef.Href;
            instance.FilingIndicators = this.FilingIndicators.Select(i=>i.Value).ToList();
            foreach (var xbrlfact in Facts) 
            {
                var xbrlcontext = this.Contexts.FirstOrDefault(i => i.ID == xbrlfact.ContextRef);
                var logicalfact = new Fact();
                logicalfact.UnitID = xbrlfact.UnitRef;
                logicalfact.ContextID = xbrlfact.ContextRef;
                logicalfact.Unit = this.Units.FirstOrDefault(i => i.ID == xbrlfact.UnitRef);
                
                var factkey = "";
                factkey = String.Format("{0},",xbrlfact.Concept);
                foreach (var dimension in xbrlcontext.Scenario.Dimensions) 
                {
                    var dimitem = String.Format("{0},", dimension.DomainMemberFullName);
                    factkey += dimitem;
                }

                logicalfact.FactKey = factkey;
                logicalfact.Value = xbrlfact.Value;
                instance.Facts.Add(logicalfact);

            }
            return instance;
        }

        private void Test() 
        {
            var instancepath = @"C:\Users\vladimir.balacescu\Desktop\Delivery\C08 reports_10_28_2014.xbrl";
            var instance = new XbrlInstance(instancepath);
            instance.Load();

            var inst = instance.GetInstance();
            int z = 0;
        }
    }
}
