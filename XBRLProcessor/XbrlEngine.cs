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
            CurrentTaxonomy.TaxonomyToUI();
            CurrentTaxonomy.LoadAllReferences();
            CurrentTaxonomy.LoadLabels();
            CurrentTaxonomy.LoadSchemaElements();
            CurrentTaxonomy.LoadConcepts();
            CurrentTaxonomy.LoadTables();
            CurrentTaxonomy.LoadFacts();
            CurrentTaxonomy.LoadValidations();
            CurrentTaxonomy.LoadHierarchy();
            CurrentTaxonomy.TaxonomyToUI();
           

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
            //var taxonomyloadneeded = true;
            //if (CurrentTaxonomy != null)
            //{
            //    var localmoduleentry = Utilities.Strings.GetLocalPath(CurrentTaxonomy.LocalFolder, CurrentInstance.SchemaRef.Href);
            //    taxonomyloadneeded = CurrentTaxonomy.EntryDocument.LocalPath != localmoduleentry;
            //}
            //if (taxonomyloadneeded)
            //{
            //    LoadTaxonomy(CurrentInstance.SchemaRef.Href);

            //}
            //CurrentInstance.Taxonomy = CurrentTaxonomy;
            //CurrentInstance.ModulePath = Utilities.Strings.GetLocalPath(CurrentTaxonomy.LocalFolder, CurrentInstance.TaxonomyModuleReference);
            //CurrentInstance.HtmlPath = CurrentTaxonomy.TaxonomyLayoutFolder + "Instance.html";
            //var html = System.IO.File.ReadAllText(CurrentTaxonomy.TaxonomyLayoutFolder + "InstanceTemplate.html");
            //var taxscriptincludes = "";
            //foreach (var taxfile in CurrentTaxonomy.TaxFiles)
            //{
            //    taxscriptincludes += String.Format("<script src=\"{0}\" type=\"text/javascript\" ></script>\r\n", taxfile);
            //}
            //html = html.Replace("#TaxScripts#", taxscriptincludes);
            //Utilities.FS.WriteAllText(CurrentInstance.HtmlPath, html);

            //foreach (var fact in CurrentInstance.Facts) 
            //{
            //    if (CurrentTaxonomy.Facts.ContainsKey(fact.FactKey))
            //    {
            //        fact.Cells = CurrentTaxonomy.Facts[fact.FactKey];
            //    }
            //}
            //var json_instance = Utilities.Converters.ToJson(CurrentInstance);
            //Utilities.FS.WriteAllText(CurrentTaxonomy.CurrentInstancePath, "var currentinstance = " + json_instance + ";");

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
