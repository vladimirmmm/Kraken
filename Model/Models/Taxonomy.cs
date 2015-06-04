﻿using BaseModel;
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
        public List<Concept> Concepts = new List<Concept>();
        public Dictionary<string, Label> TaxonomyLabelDictionary = new Dictionary<string, Label>();

        public List<Element> SchemaElements = new List<Element>();

        public List<Hierarchy<QualifiedItem>> Hierarchies = new List<Hierarchy<QualifiedItem>>();

        public Dictionary<string, Element> SchemaElementDictionary = new Dictionary<string, Element>();

        private Dictionary<string, List<String>> _Facts = new Dictionary<string, List<String>>();
        public Dictionary<string, List<String>> Facts 
        {
            get { return _Facts; }
            set { _Facts = value; }
        }
        public Dictionary<string, List<String>> FactsOfConcepts = new Dictionary<string, List<string>>();
   
        public Dictionary<string, List<String>> Cells = new Dictionary<string, List<String>>();
        
        //public static Action<string> Console = null;

        public TaxHandler TableHandler = new TaxHandler();
        public TaxHandler LabelHandler = new TaxHandler();
        public TaxHandler ElementHandler = new TaxHandler();

        public string ModuleFolder { get; set; }

        public string SourceTaxonomyPath = "";
        public string SourceTaxonomyFolder
        {
            get { return Utilities.Strings.GetFolder(SourceTaxonomyPath); }
        }
        public string TaxonomyTestPath
        {
            get { return ModuleFolder + "Test.txt"; }
        }
        public string TaxonomyStructurePath
        {
            get { return ModuleFolder + "Structure.json"; }
        }
        public string TaxonomyFactsPath
        {
            get { return ModuleFolder + "Facts.json"; }
        }
        public string TaxonomyDimensionPath
        {
            get { return ModuleFolder + "Dimension.json"; }
        }
        public string TaxonomyConceptPath
        {
            get { return ModuleFolder + "Concept.json"; }
        }
        public string TaxonomyHyperCuberPath
        {
            get { return ModuleFolder + "Hypercube.json"; }
        }
        public string TaxonomyHierarchyPath
        {
            get { return ModuleFolder + "Hierarchy.json"; }
        }
        public string TaxonomyLabelPath
        {
            get { return ModuleFolder + "Labels.json"; }
        }
        public string TaxonomyTablesPath
        {
            get { return ModuleFolder + "Tabels.json"; }
        }
        public string TaxonomySchemaElementsPath
        {
            get { return ModuleFolder + "SchemaElements.json"; }
        }

        public string TaxonomyCsPath
        {
            get { return ModuleFolder + "Validations.cs"; }
        }

        public string TaxonomyDotNetLibPath
        {
            get { return ModuleFolder + "Validations.dll"; }
        }

        public string TaxonomyLayoutFolder
        {
            get { return ModuleFolder + "Layout\\"; }
        }


        public Taxonomy(string entrypath) 
        {
            var localentrypath = Utilities.Strings.GetLocalPath(LocalFolder, entrypath);
            ModuleFolder = localentrypath.Replace(".xsd", "\\");
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
            else
            {
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
                Utilities.FS.WriteAllText(TaxonomyLabelPath, jsoncontent);
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
        public virtual void LoadFactDictionary() { }

        public virtual void LoadFacts()
        {
            Console.WriteLine("Load Facts");
            //refresh files
            ManageUIFiles();
            //

            if (!System.IO.File.Exists(TaxonomyFactsPath))
            {
                foreach (var table in Tables)
                {
                    table.LoadDefinitions();
                }
                LoadCells();

                this.LoadFactDictionary();

                var jsoncontent = Utilities.Converters.ToJson(Facts);
                Utilities.FS.WriteAllText(TaxonomyFactsPath, jsoncontent);
            }
            else
            {

                var jsoncontent = System.IO.File.ReadAllText(TaxonomyFactsPath);
                this.Facts = Utilities.Converters.JsonTo<Dictionary<string, List<string>>>(jsoncontent);

                this.LoadFactDictionary();

                LoadCells();
            }
            Console.WriteLine("Load Facts completed");
        }

        private void LoadCells()
        {
            Console.WriteLine("Load Cells started");
            Cells.Clear();
            foreach (var fact in Facts) 
            {
                var cells = fact.Value;
                foreach (var cell in cells) 
                {
                    if (!Cells.ContainsKey(cell))
                    {
                        Cells.Add(cell, new List<string>() { fact.Key });
                    }
                }
            }
            Console.WriteLine("Load Cells completed");

        }

        public virtual void LoadTables()
        {
            Console.WriteLine("Load Tables");
            //refresh files
            ManageUIFiles();
            //

            if (!System.IO.File.Exists(TaxonomyTablesPath))
            {
                TableHandler.HandleTaxonomy(this);

                var jsoncontent = Utilities.Converters.ToJson(Tables);
                Utilities.FS.WriteAllText(TaxonomyTablesPath, jsoncontent);

            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyTablesPath);
                this.Tables = Utilities.Converters.JsonTo<List<LogicalModel.Table>>(jsoncontent);


                foreach (var table in this.Tables)
                {
                    table.Taxonomy = this;
                    table.Reload();
                    //table.LoadDefinitions();
                    table.LoadLayout();


                }
            }
            Console.WriteLine("Load Tables completed");

        }
        
        public void ManageUIFile(string filename)
        {
            var devfolder = @"C:\My\Developement\XTree-M\XTree-M\Model\";
            var taxpath = TaxonomyLayoutFolder + filename;
            var devpath = devfolder + filename;
            var buildpath = filename;
            var finfo_dev = new System.IO.FileInfo(devpath);
            var finfo_build = new System.IO.FileInfo(buildpath);

            if (finfo_dev.LastWriteTime > finfo_build.LastWriteTime) 
            {
                System.IO.File.Copy(finfo_dev.FullName, finfo_build.FullName, true);
                finfo_build = new System.IO.FileInfo(finfo_build.FullName);

            }

            if (!System.IO.File.Exists(taxpath))
            {
                Utilities.FS.EnsurePath(taxpath);
                System.IO.File.Copy(finfo_build.FullName, taxpath, true);
            }
            else 
            {
                var finfo_tax = new System.IO.FileInfo(taxpath);
                if (finfo_build.LastWriteTime > finfo_tax.LastWriteTime)
                {
                    System.IO.File.Copy(finfo_build.FullName, finfo_tax.FullName, true);
                }
            }


        }
        
        public void ManageUIFiles() 
        {
            if (!System.IO.Directory.Exists(TaxonomyLayoutFolder))
            {
                System.IO.Directory.CreateDirectory(TaxonomyLayoutFolder);
            }

            ManageUIFile(@"Scripts\jquery-2.1.3.js");
            ManageUIFile(@"Scripts\Linq.js");
            ManageUIFile(@"Scripts\Instance.js");
            ManageUIFile(@"Scripts\Models.js");
            ManageUIFile(@"Scripts\Table.js");
            ManageUIFile(@"Scripts\Utils.js");
            ManageUIFile(@"Table.css");           
            
        }

        public virtual void LoadHierarchy() 
        {

        }

        public virtual void LoadValidations() { }
        
        public virtual void LoadSchemaElements()
        {
            Console.WriteLine("Load Elements");

            if (!System.IO.File.Exists(TaxonomySchemaElementsPath))
            {
                ElementHandler.HandleTaxonomy(this);

                var jsoncontent = Utilities.Converters.ToJson(SchemaElements);
                Utilities.FS.WriteAllText(TaxonomySchemaElementsPath, jsoncontent);
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
            Console.WriteLine("Load Concepts");

            if (!System.IO.File.Exists(TaxonomyConceptPath))
            {

                var conceptelements = SchemaElements.Where(i => i.Namespace == "eba_met").ToList();
                foreach (var conceptelement in conceptelements) 
                {
                    var concept = new Concept();
                    if (!String.IsNullOrEmpty(conceptelement.Domain))
                    {
                        concept.Domain = new QualifiedName();
                        concept.Domain.Content = conceptelement.Domain;
                        concept.HierarchyRole = conceptelement.Hierarchy;
                    }
                    concept.Name = conceptelement.Name;
                    concept.Namespace = conceptelement.Namespace;

                    this.Concepts.Add(concept);
                }


                var jsoncontent = Utilities.Converters.ToJson(this.Concepts);
                Utilities.FS.WriteAllText(TaxonomyConceptPath, jsoncontent);
                Utilities.FS.WriteAllText(TaxonomyLayoutFolder+"concepts.js", "var concepts = " + jsoncontent+ ";");
            }
            else
            {

                var jsoncontent = System.IO.File.ReadAllText(TaxonomyConceptPath);
                this.Concepts = Utilities.Converters.JsonTo<List<Concept>>(jsoncontent);

            }
            Console.WriteLine("Load Concepts completed");
        }

        public virtual void LoadInstance(string filepath)
        {

        }


        public virtual void AfterRefrencesLoadedFromCache()
        {

        }

        public virtual void ClearCache()
        {

        }
        public void Clear_All_But_Structure()
        {
            Clear_Concepts();
            Clear_Hierarchies();
            Clear_Tables();
            Clear_Layout();
            Clear_Labels();
            Clear_SchemaElements();
            Clear_Facts();
        }
        public void Clear_All()
        {
            Clear_Concepts();
            Clear_Hierarchies();
            Clear_Structure();
            Clear_Tables();
            Clear_Layout();
            Clear_Labels();
            Clear_SchemaElements();
            Clear_Facts();
        }
        public void Clear_Facts()
        {
            Utilities.FS.DeleteFile(TaxonomyFactsPath);
        }
        public void Clear_Structure() 
        {
            Utilities.FS.DeleteFile(TaxonomyStructurePath);
        }
        public void Clear_Concepts()
        {
            Utilities.FS.DeleteFile(TaxonomyConceptPath);
        }
        public void Clear_Hierarchies()
        {
            Utilities.FS.DeleteFile(TaxonomyHierarchyPath);
        }
        public void Clear_Tables()
        {
            Utilities.FS.DeleteFile(TaxonomyTablesPath);
        }

        public void Clear_Layout()
        {
            Utilities.FS.DeleteFolder(TaxonomyLayoutFolder);
        }
        public void Clear_Labels()
        {
            Utilities.FS.DeleteFile(TaxonomyLabelPath);
        }
        public void Clear_SchemaElements()
        {
            Utilities.FS.DeleteFile(TaxonomySchemaElementsPath);
        }
    }
}
