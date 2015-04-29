using BaseModel;
using LogicalModel.Base;
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
        public Dictionary<string, Label> TaxonomyLabelDictionary = new Dictionary<string, Label>();

        public List<Element> SchemaElements = new List<Element>();
        public Dictionary<string, Element> SchemaElementDictionary = new Dictionary<string, Element>();
        
        //public static Action<string> Console = null;

        public TaxHandler TableHandler = new TaxHandler();
        public TaxHandler LabelHandler = new TaxHandler();
        public TaxHandler ElementHandler = new TaxHandler();

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
        public string TaxonomySchemaElementsPath
        {
            get { return EntryDocument.LocalFolder + "SchemaElements.json"; }
        }

        public string TaxonomyLayoutFolder
        {
            get { return EntryDocument.LocalFolder + "Layout\\"; }
        }


        public Taxonomy(string entrypath) 
        {
            this.TaxonomyDocuments.Clear();
            SourceTaxonomyPath = entrypath;
            LogicalModel.Table.LabelAccessor = FindLabel;
        
        }

        public LogicalModel.Label FindLabel(string key)
        {
            key = key.ToLower();
            if (TaxonomyLabelDictionary.ContainsKey(key))
            {
                return TaxonomyLabelDictionary[key];
            }
            return null;
        }

        public virtual void LoadLabels()
        {
            Console.WriteLine("Load Labels");

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
                LoadLabelDictionary();
            }
            Console.WriteLine("Load Labels Completed");

        }

        public virtual void LoadLabelDictionary() { }

        public virtual void LoadTables()
        {
            Console.WriteLine("Load Tables");
            //refresh files
            if (!System.IO.Directory.Exists(TaxonomyLayoutFolder))
            {
                System.IO.Directory.CreateDirectory(TaxonomyLayoutFolder);
            }

            var jquerypath = TaxonomyLayoutFolder + "jquery-2.0.3.js";

            if (!System.IO.File.Exists(jquerypath))
            {
                System.IO.File.Copy("jquery-2.0.3.js", jquerypath);
            }
            var tablejsfilename = "Table.js";
            var tablejsoriginalpath = @"C:\My\Developement\XTree-M\XTree-M\Model\" + tablejsfilename;
            var fi_out = new System.IO.FileInfo(tablejsfilename);

            if (System.IO.File.Exists(tablejsoriginalpath)) 
            {
                var fi_dev = new System.IO.FileInfo(tablejsoriginalpath);
                if (fi_dev.LastWriteTime > fi_out.LastWriteTime) 
                {
                    System.IO.File.Copy(fi_dev.FullName, fi_out.FullName, true);
                }

            }
            var jstablepath = TaxonomyLayoutFolder + tablejsfilename;
            var fi_layout = new System.IO.FileInfo(jstablepath);
            var shouldcopy = false;
            if (System.IO.File.Exists(jstablepath))
            {
                var fi_main2 = new System.IO.FileInfo(jstablepath);
                shouldcopy = fi_out.LastWriteTime > fi_layout.LastWriteTime;
            }
            else
            {
                shouldcopy = true;
            }
            if (shouldcopy)
            {
                System.IO.File.Copy(tablejsfilename, jstablepath, true);

            }
            //

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
                    table.Reload();
                    table.LoadDefinitions();
                    table.LoadLayout();


                }
            }
            Console.WriteLine("Load Tables completed");

        }

        public virtual void LoadSchemaElements()
        {
            Console.WriteLine("Load Elements");

            if (!System.IO.File.Exists(TaxonomySchemaElementsPath))
            {
                ElementHandler.HandleTaxonomy(this);

                var jsoncontent = Utilities.Converters.ToJson(SchemaElements);
                System.IO.File.WriteAllText(TaxonomySchemaElementsPath, jsoncontent);
            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomySchemaElementsPath);
                this.SchemaElements = Utilities.Converters.JsonTo<List<Element>>(jsoncontent);
                foreach (var schemaelement in this.SchemaElements)
                {
                    this.SchemaElementDictionary.Add(schemaelement.Key, schemaelement);
                }

            }
            Console.WriteLine("Load Elements completed");

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
