using Model.DefinitionModel;
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
            CurrentTaxonomy = new XbrlTaxonomy(filepath);
  
            CurrentTaxonomy.LoadAllReferences();
            CurrentTaxonomy.LoadLabels();
            CurrentTaxonomy.LoadTables();
    
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
