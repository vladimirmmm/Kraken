using Model.DefinitionModel;
using Model.InstanceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XBRLProcessor.Models;

namespace XBRLProcessor
{
    public partial class XbrlEngine: LogicalModel.TaxonomyEngine
    {

        public XbrlTaxonomy CurrentTaxonomy = null;
        public XbrlInstance CurrentInstance = null;
        public static XbrlEngine CurrentEngine = null;
     

        public XbrlEngine() 
        {
            if (CurrentEngine == null) 
            {
                CurrentEngine = this;
            }
        }

        public override bool LoadTaxonomy(string filepath) 
        {
            if (CurrentTaxonomy != null) 
            {
                Utilities.Xml.NamespaceDictionary.Clear();
                CurrentTaxonomy.ClearObjects();
            }
            var isloaded = false;
            Console.WriteLine("Loading Taxonomy ");
            var taxonomydocument = new XbrlTaxonomyDocument();
            if (taxonomydocument.LoadTaxonomyDocument(filepath, null))
            {

                CurrentTaxonomy = new XbrlTaxonomy(filepath);
                CurrentTaxonomy.TaxonomyToUI();
                CurrentTaxonomy.LoadAllReferences();

                CheckMapping();

                var metdoc = CurrentTaxonomy.TaxonomyDocuments.FirstOrDefault(i => i.FileName == "met.xsd");
                CurrentTaxonomy.ConceptNameSpace = metdoc.TargetNamespace;
                CurrentTaxonomy.Prefix = CurrentTaxonomy.ConceptNameSpace.Remove(CurrentTaxonomy.ConceptNameSpace.LastIndexOf("_")) + "_";

                CurrentTaxonomy.LoadLabels();
                CurrentTaxonomy.LoadSchemaElements();
                CurrentTaxonomy.LoadConcepts();
                CurrentTaxonomy.LoadTables();
                CurrentTaxonomy.LoadFacts();
                CurrentTaxonomy.LoadValidations();
                CurrentTaxonomy.LoadHierarchy();
                CurrentTaxonomy.TaxonomyToUI();
                isloaded = true;
                Console.WriteLine("Loading Taxonomy finished");
                base.LoadTaxonomy(filepath);
            }
            else 
            {
                Console.WriteLine("Can't Load Taxonomy");
            }
            return isloaded;
        }

        protected void CheckMapping()
        {
            var mapdict = new Dictionary<string, string>();
            var tagsmapped = XBRLProcessor.Mapping.Mappings.GetTagsCovered();
            var sb = new StringBuilder();


            foreach (var doc in CurrentTaxonomy.TaxonomyDocuments) 
            {
                foreach (var tagname in doc.TagNames) 
                {
                    var decoratedtagname = String.Format("<{0}>", tagname);
                    if (!mapdict.ContainsKey(decoratedtagname)) 
                    {
                        if (!tagsmapped.Contains(decoratedtagname)) 
                        {
                            mapdict.Add(decoratedtagname, doc.LocalPath);
                            sb.AppendLine(String.Format("{0} - {1}", decoratedtagname, doc.LocalPath));
                        }
                    }
                }
            }
            Console.WriteLine("Tags not mapped: ");
            Console.WriteLine(sb.ToString());
        }

        public override bool LoadInstance(string filepath)
        {
            Console.WriteLine("Loading Instance started");
            CurrentInstance = new XbrlInstance(filepath);
            CurrentInstance.LoadSimple();
            base.LoadInstance(filepath);
            CurrentInstance.LoadComplex();
            
            Console.WriteLine("Loading Instance finished");
            return true;
        }

        public void LoadLabels()
        {
            CurrentTaxonomy.LoadLabels();
        }

        public static void TraverseNodes(XmlNodeList nodes, Action<XmlNode> handler)
        {
            foreach (XmlNode node in nodes)
            {
                handler(node);
                // Do something with the node.
                TraverseNodes(node.ChildNodes, handler);
            }
        }

    }
}
