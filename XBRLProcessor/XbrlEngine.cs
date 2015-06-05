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

        public override void LoadTaxonomy(string filepath) 
        {
            Console.WriteLine("Loading Taxonomy ");
            CurrentTaxonomy = new XbrlTaxonomy(filepath);
  
            CurrentTaxonomy.LoadAllReferences();
            CurrentTaxonomy.LoadLabels();
            CurrentTaxonomy.LoadSchemaElements();
            CurrentTaxonomy.LoadConcepts();
            CurrentTaxonomy.LoadTables();
            CurrentTaxonomy.LoadFacts();
            CurrentTaxonomy.LoadValidations();
            CurrentTaxonomy.LoadHierarchy();

            Console.WriteLine("Loading Taxonomy finished");
            base.LoadTaxonomy(filepath);
        }
        public override void LoadInstance(string filepath)
        {
            Console.WriteLine("Loading Instance started");
            CurrentInstance = new XbrlInstance(filepath);
            CurrentInstance.Load();
            base.LoadInstance(filepath);
            Console.WriteLine("Loading Instance finished");
            var taxonomyloadneeded = true;
            if (CurrentTaxonomy!=null){
                var localmoduleentry = Utilities.Strings.GetLocalPath(CurrentTaxonomy.LocalFolder, CurrentInstance.SchemaRef.Href);
                taxonomyloadneeded = CurrentTaxonomy.EntryDocument.LocalPath != localmoduleentry;
            }
            if (taxonomyloadneeded)
            {
                LoadTaxonomy(CurrentInstance.SchemaRef.Href);
            
            }
            CurrentInstance.Taxonomy = CurrentTaxonomy;
            CurrentInstance.ModulePath = Utilities.Strings.GetLocalPath(CurrentTaxonomy.LocalFolder, CurrentInstance.TaxonomyModuleReference);
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
