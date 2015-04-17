using BaseModel;
using LogicalModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities;

namespace LogicalModel
{
    public class Taxonomy : DocumentCollection
    {

        public new TaxonomyDocument EntryDocument 
        {
            get { return (TaxonomyDocument)base.EntryDocument; }
            set { base.EntryDocument=value;}
        }
        public List<Table> Tables = new List<Table>();
        public List<TaxonomyDocument> TaxonomyDocuments = new List<TaxonomyDocument>();
        public List<Label> TaxonomyLabels = new List<Label>();
        public static Action<string> Console = null;

        public TaxHandler TableHandler = new TaxHandler();
        public TaxHandler LabelHandler = new TaxHandler();

        public string SourceTaxonomyPath = "";
        public string SourceTaxonomyFolder
        {
            get { return Utilities.Strings.GetFolder(SourceTaxonomyPath); }
        }
        public string TaxonomyStructurePath
        {
            get { return EntryDocument.LocalFolder + "Structure.json"; }
        }
        public string TaxonomyDimensionPath
        {
            get { return EntryDocument.LocalFolder + "Dimension.json"; }
        }
        public string TaxonomyConceptPath
        {
            get { return EntryDocument.LocalFolder + "Concept.json"; }
        }
        public string TaxonomyHyperCuberPath
        {
            get { return EntryDocument.LocalFolder + "Hypercube.json"; }
        }
        public string TaxonomyLabelPath
        {
            get { return EntryDocument.LocalFolder + "Labels.json"; }
        }
        public string TaxonomyTablesPath
        {
            get { return EntryDocument.LocalFolder + "Tabels.json"; }
        }
        public string TaxonomyLayoutFolder
        {
            get { return EntryDocument.LocalFolder + "Layout\\"; }
        }


        public Taxonomy(string entrypath) 
        {
            this.TaxonomyDocuments.Clear();
            SourceTaxonomyPath = entrypath;

        }

        public virtual void LoadLabels()
        {
            Console("Load Labels");

            //Save
            if (!System.IO.File.Exists(TaxonomyLabelPath))
            {

                LabelHandler.HandleTaxonomy(this);

                var jsoncontent = Utilities.Converters.ToJson(TaxonomyLabels);
                System.IO.File.WriteAllText(TaxonomyLabelPath, jsoncontent);
            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyLabelPath);
                this.TaxonomyLabels = Utilities.Converters.JsonTo<List<LogicalModel.Label>>(jsoncontent);
            }
            Console("Load Labels Completed");

        }

        public virtual void LoadTables()
        {
            Console("Load Tables");

            if (!System.IO.File.Exists(TaxonomyTablesPath))
            {
                TableHandler.HandleTaxonomy(this);

                var jsoncontent = Utilities.Converters.ToJson(Tables);
                System.IO.File.WriteAllText(TaxonomyTablesPath, jsoncontent);
            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyTablesPath);
                this.Tables = Utilities.Converters.JsonTo<List<LogicalModel.Table>>(jsoncontent);

                foreach (var table in this.Tables)
                {
                    table.Taxonomy = this;
                }
            }
            Console("Load Tables completed");

        }

        public virtual void AfterRefrencesLoadedFromCache() 
        {

        }

        public virtual void ClearCache() 
        {

        }


        public virtual void LoadLayouts()
        {

        }

        public virtual void LoadHyperCubes()
        {

        }

        public virtual void LoadDimensions()
        {

        }

        public virtual void LoadConcepts()
        {

        }

        public virtual void LoadInstance(string filepath)
        {

        }

    
    }
}
