using LogicalModel;
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

        private TaxonomyEventHandler CurrentInstanceTaxonomyLoaded = null;
        private TaxonomyEventHandler CurrentInstanceTaxonomyLoadFailed = null;

        public XbrlEngine() 
        {
            if (CurrentEngine == null) 
            {
                CurrentEngine = this;
            }
            this.InstanceLoad += XbrlEngine_InstanceLoad;
            this.TaxonomyLoadFailed += XbrlEngine_TaxonomyLoadFailed;
            this.TaxonomyLoaded += XbrlEngine_TaxonomyLoaded;
        }

        void XbrlEngine_TaxonomyLoaded(object sender, TaxonomyEventArgs e)
        {
            if (CurrentInstanceTaxonomyLoaded != null) 
            {
                CurrentInstanceTaxonomyLoaded(sender, e);
            }
        }

        void XbrlEngine_TaxonomyLoadFailed(object sender, TaxonomyEventArgs e)
        {
            if (CurrentInstanceTaxonomyLoadFailed != null)
            {
                CurrentInstanceTaxonomyLoadFailed(sender, e);
            }
        }

        void XbrlEngine_InstanceLoad(object sender, TaxonomyEventArgs e)
        {
            var taxonomyloadneeded = true;
            if (CurrentTaxonomy != null)
            {
                var localmoduleentry = Utilities.Strings.GetLocalPath(Taxonomy.LocalFolder,
                    CurrentInstance.TaxonomyModuleReference);
                taxonomyloadneeded = CurrentTaxonomy.EntryDocument.LocalPath != localmoduleentry;
            }
            if (taxonomyloadneeded)
            {
                LoadTaxonomy(CurrentInstance.SchemaRef.Href);
                //LoadTaxonomy();
            }
            else 
            {
                Trigger_TaxonomyLoaded(CurrentTaxonomy.EntryDocument.LocalPath);
            }
         
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
                    var decoratedlocaltagname="";
                    if (tagname.Contains(":")) 
                    {
                        decoratedlocaltagname = Utilities.Strings.TextBetween(decoratedtagname,":",">");
                        decoratedlocaltagname = String.Format("<{0}>", decoratedlocaltagname);
                    }
                    if (!mapdict.ContainsKey(decoratedtagname))
                    {
                        if (!tagsmapped.Contains(decoratedtagname))
                        {

                            if (tagsmapped.Contains(decoratedlocaltagname))
                            {

                            }
                            else
                            {
                                mapdict.Add(decoratedtagname, doc.LocalPath);
                                sb.AppendLine(String.Format("{0} - {1}", decoratedtagname, doc.LocalPath));
                            }
                        }
                    }

                }
            }
            Console.WriteLine("Tags not mapped: ");
            Console.WriteLine(sb.ToString());
        }

        public override bool LoadTaxonomy(string filepath) 
        {
            if (CurrentTaxonomy != null) 
            {
                Utilities.Xml.NamespaceDictionary.Clear();
                CurrentTaxonomy.ClearObjects();
                CurrentTaxonomy = null;
            }
            var isloaded = false;
            Console.WriteLine("Loading Taxonomy ");
            var taxonomydocument = new XbrlTaxonomyDocument();
            if (taxonomydocument.LoadTaxonomyDocument(filepath, null))
            {
                Trigger_TaxonomyLoad(filepath);

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
                CurrentTaxonomy.LoadTableGroups();
                CurrentTaxonomy.LoadTables();
                CurrentTaxonomy.LoadFacts();
                CurrentTaxonomy.LoadValidationFunctions();
                CurrentTaxonomy.LoadHierarchy();
                CurrentTaxonomy.LoadUnits();
                CurrentTaxonomy.LoadGeneral();
                CurrentTaxonomy.TaxonomyToUI();
                isloaded = true;
                Console.WriteLine("Loading Taxonomy finished");
                Trigger_TaxonomyLoaded(filepath);
            }
            else 
            {
                Console.WriteLine("Can't Load Taxonomy");
                Trigger_TaxonomyLoadFailed(filepath);

            }
            return isloaded;
        }       

        public override bool LoadInstance(string filepath)
        {
            Console.WriteLine("Loading Instance started");
            CurrentInstance = new XbrlInstance(filepath);
            CurrentInstanceTaxonomyLoaded = null;
            CurrentInstanceTaxonomyLoadFailed = null;
            CurrentInstance.LoadSimple();

            CurrentInstanceTaxonomyLoaded = (object o, TaxonomyEventArgs e) =>
            {
                CurrentInstance.SetTaxonomy(CurrentTaxonomy);
                CurrentInstance.LoadComplex();
                Console.WriteLine("Loading Instance finished");
            };
            CurrentInstanceTaxonomyLoadFailed = (object o, TaxonomyEventArgs e) =>
            {
                Console.WriteLine("Loading Instance failed");
                Trigger_InstanceLoadFailed(filepath);

            };

            Trigger_InstanceLoad(filepath);
        
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
