using LogicalModel;
using Model.DefinitionModel;
using Model.InstanceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities;
using XBRLProcessor.Models;

namespace XBRLProcessor
{
    public partial class XbrlEngine: TaxonomyEngine
    {
      
        public XbrlTaxonomy CurrentXbrlTaxonomy 
        {
            get{return CurrentTaxonomy as XbrlTaxonomy;}
            set{CurrentTaxonomy =value;}
        }
        public XbrlInstance CurrentXbrlInstance{
            get{return CurrentInstance as XbrlInstance;}
            set{CurrentInstance =value;}
        }
        /*
        public static XbrlEngine CurrentEngine = null;
        */
        private TaxonomyEventHandler CurrentInstanceTaxonomyLoaded = null;
        private TaxonomyEventHandler CurrentInstanceTaxonomyLoadFailed = null;

        public XbrlEngine() 
        {
            if (CurrentEngine == null) 
            {
                CurrentEngine = this;
            }
            this.InstanceLoad += XbrlEngine_InstanceLoad;
            this.InstanceLoaded += XbrlEngine_InstanceLoaded;
            this.InstanceLoadFailed += XbrlEngine_InstanceLoadFailed;
            this.TaxonomyLoadFailed += XbrlEngine_TaxonomyLoadFailed;
            this.TaxonomyLoaded += XbrlEngine_TaxonomyLoaded;
        }

        private void XbrlEngine_InstanceLoadFailed(object sender, TaxonomyEventArgs e)
        {
            IsInstanceLoading = false;
        }

        private void XbrlEngine_InstanceLoaded(object sender, TaxonomyEventArgs e)
        {
            IsInstanceLoading = false;


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
                var localmoduleentry = Utilities.Strings.GetLocalPath(XbrlEngine.LocalFolder,
                    CurrentInstance.TaxonomyModuleReference);
                taxonomyloadneeded = CurrentTaxonomy.EntryDocument.LocalPath != localmoduleentry;
            }
            if (taxonomyloadneeded)
            {
                LoadTaxonomy(CurrentXbrlInstance.SchemaRef.Href);
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


            foreach (var doc in CurrentXbrlTaxonomy.TaxonomyDocuments)
            {
                foreach (var tagname in doc.TagNames)
                {
                    var decoratedtagname = String.Format("<{0}>", tagname);
                    var decoratedlocaltagname="";
                    if (tagname.Contains(LogicalModel.Literals.QNameSeparator)) 
                    {
                        decoratedlocaltagname = Utilities.Strings.TextBetween(decoratedtagname, LogicalModel.Literals.QNameSeparator, ">");
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
            Logger.WriteLine("Tags not mapped: ");
            Logger.WriteLine(sb.ToString());
        }

        public override bool LoadTaxonomy(string filepath) 
        {
            if (CurrentTaxonomy != null) 
            {
                CurrentXbrlTaxonomy.ClearObjects();
                CurrentTaxonomy = null;
            }
            var isloaded = false;
            Logger.WriteLine("Loading Taxonomy ");
            var taxonomydocument = new XbrlTaxonomyDocument();
            if (taxonomydocument.LoadTaxonomyDocument(filepath, null))
            {
                taxonomydocument.ClearDocument();
                Trigger_TaxonomyLoad(filepath);

                CurrentTaxonomy = new XbrlTaxonomy(filepath);
                CurrentTaxonomy.TaxonomyToUI();
                LogicalModel.Helpers.FileManager.Clear();

                ////Test
                //LogicalModel.Settings.Current.ReloadFullTaxonomy = true;
                //LogicalModel.Settings.Current.ReDownloadFiles= true;
                ////End test
                CurrentTaxonomy.FactsManager.maxdictnr = 1000;
                CurrentTaxonomy.FactsManager.SaveToFile = true;
                CurrentTaxonomy.FactsManager.DataFolder = CurrentTaxonomy.TaxonomyFactsFolder;
                //CurrentTaxonomy.FactsManager.ManageLoadedFacts = CurrentTaxonomy.ManageLoadedFacts;
                CurrentTaxonomy.LoadAllReferences();

                CheckMapping();
                /*
                var metdoc = CurrentXbrlTaxonomy.TaxonomyDocuments.FirstOrDefault(i => i.FileName == "met.xsd");
                CurrentTaxonomy.ConceptNameSpace = metdoc.TargetNamespacePrefix;
                 * */
                //CurrentTaxonomy.Prefix = CurrentTaxonomy.ConceptNameSpace.Remove(CurrentTaxonomy.ConceptNameSpace.LastIndexOf("_")) + "_";

                CurrentTaxonomy.LoadLabels();
                CurrentTaxonomy.LoadSchemaElements();
                CurrentTaxonomy.LoadConcepts();
                CurrentTaxonomy.LoadDimensions();
                CurrentTaxonomy.LoadHierarchy();
                CurrentTaxonomy.LoadTables();
                CurrentXbrlTaxonomy.ClearXmlObjects();
                CurrentTaxonomy.LoadFacts();
                //CurrentTaxonomy.LoadHierarchy();
                CurrentTaxonomy.LoadUnits();
                CurrentTaxonomy.LoadValidationFunctions();


                CurrentXbrlTaxonomy.ClearXmlObjects();
                
                CurrentTaxonomy.TaxonomyToUI();
                isloaded = true;
                Logger.WriteLine("Loading Taxonomy finished");
                Utilities.FS.DeleteFile(CurrentTaxonomy.CurrentInstancePath);
                Utilities.FS.DeleteFile(CurrentTaxonomy.CurrentInstanceValidationResultPath);
                if (!IsInstanceLoading)
                {
                    this.CurrentInstance = (XbrlInstance)this.CurrentTaxonomy.GetNewInstance();
                    Utilities.FS.DeleteFile(CurrentTaxonomy.CurrentInstancePath);
                    Utilities.FS.DeleteFile(CurrentTaxonomy.CurrentInstanceValidationResultPath);
                    //Utilities.FS.DeleteFile(CurrentTaxonomy.CurrentInstancePath);
                    this.CurrentInstance.SaveToJson();
                }
                Trigger_TaxonomyLoaded(filepath);
            }
            else 
            {
                Logger.WriteLine("Can't Load Taxonomy");
                Trigger_TaxonomyLoadFailed(filepath);

            }
            return isloaded;
        }       

        public override bool LoadInstance(string filepath)
        {
            IsInstanceLoading = true;
            Logger.WriteLine("Loading Instance started");
            CurrentInstance = new XbrlInstance(filepath);
            CurrentInstanceTaxonomyLoaded = null;
            CurrentInstanceTaxonomyLoadFailed = null;
            CurrentXbrlInstance.LoadSimple();
        
            CurrentInstanceTaxonomyLoaded = (object o, TaxonomyEventArgs e) =>
            {
                //Trigger_TaxonomyLoaded(CurrentTaxonomy.EntryDocument.LocalPath);
                
                CurrentInstance.SetTaxonomy(CurrentTaxonomy);
               
                Task.Factory.StartNew(()=>{
                     CurrentXbrlInstance.LoadComplex();
                    Trigger_InstanceLoaded(filepath);
                    Logger.WriteLine("Loading Instance finished");

                });

            };
            CurrentInstanceTaxonomyLoadFailed = (object o, TaxonomyEventArgs e) =>
            {
                Logger.WriteLine("Loading Instance failed");
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
