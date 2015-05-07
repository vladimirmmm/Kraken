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
    public partial class XbrlEngine
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

        public void LoadTaxonomy(string filepath) 
        {
            Console.WriteLine("Loading Taxonomy ");
            CurrentTaxonomy = new XbrlTaxonomy(filepath);
  
            CurrentTaxonomy.LoadAllReferences();
            CurrentTaxonomy.LoadLabels();
            CurrentTaxonomy.LoadSchemaElements();
            CurrentTaxonomy.LoadTables();
            CurrentTaxonomy.LoadFacts();
            Console.WriteLine("Loading Taxonomy finished");
    
        }
        public void LoadInstance(string filepath)
        {
            var instance = new XbrlInstance(filepath);
            instance.Load();
            LoadTaxonomy(instance.SchemaRef.Href);
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
