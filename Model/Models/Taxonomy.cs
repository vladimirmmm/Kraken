using BaseModel;
using LogicalModel.Base;
using LogicalModel.Helpers;
using LogicalModel;
using LogicalModel.Validation;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities;

namespace LogicalModel
{
    public class TaxonomySettings
    {
        private List<ItemTypeSetting> _ItemTypeSettings = new List<ItemTypeSetting>();
        public List<ItemTypeSetting> ItemTypeSettings { get { return _ItemTypeSettings; } set { _ItemTypeSettings = value; } }
    }
    public class TaxonomyProperties 
    {
   
    }
    public class Taxonomy : DocumentCollection
    {

        public new TaxonomyDocument EntryDocument 
        {
            get { return (TaxonomyDocument)base.EntryDocument; }
            set { base.EntryDocument=value;}
        }
        public string ConceptNameSpace = "";
        public string Prefix = "";

        public List<Table> Tables = new List<Table>();
        public TaxonomyModule Module = new TaxonomyModule();
        public List<TaxonomyDocument> TaxonomyDocuments = new List<TaxonomyDocument>();
        public List<Label> TaxonomyLabels = new List<Label>();
        public List<Concept> Concepts = new List<Concept>();
        public Dictionary<string, Label> TaxonomyLabelDictionary = new Dictionary<string, Label>();

        public List<Element> SchemaElements = new List<Element>();

        public List<Hierarchy<QualifiedItem>> Hierarchies = new List<Hierarchy<QualifiedItem>>();

        public List<Unit> Units = new List<Unit>();

        public Dictionary<string, Element> SchemaElementDictionary = new Dictionary<string, Element>();

        private Dictionary<string, List<String>> _Facts = new Dictionary<string, List<String>>();
        public Dictionary<string, List<String>> Facts 
        {
            get { return _Facts; }
            set { _Facts = value; }
        }
        public Dictionary<string, List<String>> FactsOfConcepts = new Dictionary<string, List<string>>();
   
        public Dictionary<string, List<String>> Cells = new Dictionary<string, List<String>>();

        private List<ValidationRule> _ValidationRules = new List<ValidationRule>();
        public List<ValidationRule> ValidationRules { get { return _ValidationRules; } set { _ValidationRules = value; } }

        private List<SimpleValidationRule> _SimpleValidationRules = new List<SimpleValidationRule>();
        [JsonIgnore]
        public List<SimpleValidationRule> SimpleValidationRules
        {
            get
            {
                if (_SimpleValidationRules.Count == 0) 
                {
                    foreach (var rule in ValidationRules)
                    {
                        _SimpleValidationRules.Add(new SimpleValidationRule(rule));
                    }
                }
                return _SimpleValidationRules; 
            }
            set { _SimpleValidationRules = value; }
        }

        //public static Action<string> Console = null;

        public ValidationFunctionContainer ValidationFunctionContainer = null;

        public TaxHandler TableHandler = new TaxHandler();
        public TaxHandler LabelHandler = new TaxHandler();
        public TaxHandler ElementHandler = new TaxHandler();

        public string ModuleFolder { get; set; }

        public string SourceTaxonomyPath = "";
        public string SourceTaxonomyModuleFolder
        {
            get { return Utilities.Strings.GetFolder(SourceTaxonomyPath); }
        }
        public string SourceTaxonomyFolder
        {
            get 
            {
                var strfolder = "mod";
                var str1 = "\\"+strfolder+"\\"; 
                var str2 = "/"+strfolder+"/";
                var taxpath = SourceTaxonomyModuleFolder;
                if (taxpath.ToLower().EndsWith(str1)) 
                {
                    taxpath = taxpath.Remove(taxpath.Length - (str1.Length - 1));
                }
                if (taxpath.ToLower().EndsWith(str2))
                {
                    taxpath = taxpath.Remove(taxpath.Length - (str2.Length - 1));
                }

                return Utilities.Strings.GetFolder(taxpath); 
            }
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
        public string TaxonomyUnitPath
        {
            get { return ModuleFolder + "Unit.json"; }
        }
        public string TaxonomyLabelPath
        {
            get { return ModuleFolder + "Labels.json"; }
        }
        public string TaxonomyModulePath
        {
            get { return ModuleFolder + "Module.json"; }
        }
        public string TaxonomySchemaElementsPath
        {
            get { return ModuleFolder + "SchemaElements.json"; }
        }
        public string TaxonomyValidationPath
        {
            get { return ModuleFolder + "Validations.json"; }
        }
        public string TaxonomySettingsPath
        {
            get { return ModuleFolder + "ConceptSettings.json"; }
        }
        public string TaxonomyValidationCsPath
        {
            get { return ModuleFolder + "Validations.cs"; }
        }

        public string TaxonomyValidationDotNetLibPath
        {
            get { return ModuleFolder + "Validations.dll"; }
        }

        public string CurrentInstancePath
        {
            get { return TaxonomyLayoutFolder + "currentinstance.js"; }
        }

        public string CurrentInstanceValidationResultPath
        {
            get { return TaxonomyLayoutFolder + "validationresult.js"; }
        }

        public string TaxonomyLayoutFolder
        {
            get { return ModuleFolder + "Layout\\"; }
        }

        public List<string> TaxFiles = new List<string>();

        public Taxonomy(string entrypath) 
        {
            var localentrypath = Utilities.Strings.GetLocalPath(TaxonomyEngine.LocalFolder, entrypath);
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
            if (!System.IO.File.Exists(TaxonomyLabelPath) || Settings.Current.ReloadFullTaxonomyButStructure)
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

            if (!System.IO.File.Exists(TaxonomyFactsPath) || Settings.Current.ReloadFullTaxonomyButStructure)
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

            if (!System.IO.File.Exists(TaxonomyModulePath) || Settings.Current.ReloadFullTaxonomyButStructure)
            {
                PopulateTableGroups();
                LoadGeneral();
                TableHandler.HandleTaxonomy(this);

                var jsoncontent = Utilities.Converters.ToJson(Module);
                foreach (var Table in Tables)
                {
                    var tjson = Utilities.Converters.ToJson(Table);
                    Utilities.FS.WriteAllText(Table.JsonPath, tjson);

                }
                Utilities.FS.WriteAllText(TaxonomyModulePath, jsoncontent);

            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyModulePath);
                this.Module = Utilities.Converters.JsonTo<TaxonomyModule>(jsoncontent);

                foreach (var tablepath in Module.TablePaths)
                {
                    var fulltablepath = Utilities.Strings.ResolveRelativePath(this.TaxonomyLayoutFolder, tablepath);
                    var tjson = Utilities.FS.ReadAllText(fulltablepath);
                    var table = Utilities.Converters.JsonTo<Table>(tjson);
                    table.Taxonomy = this;
                    table.Reload();
                    table.LoadLayout();
                    this.Tables.Add(table);
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

            var scriptfiles = System.IO.Directory.GetFiles("Scripts");
            foreach (var scriptfile in scriptfiles)
            {
                var scriptpath = scriptfile.Substring(scriptfile.IndexOf("\\Scripts") + 1);
                ManageUIFile(scriptpath);
            }

            ManageUIFile(@"Table.css");
            ManageUIFile(@"UI.html");
         
        }

        public void TaxonomyToUI()
        {
            TaxFiles.Clear();
            //TaxFiles.Add(JsonFileToJsVarible(TaxonomyFactsPath, "", "tax_facts"));
            TaxFiles.Add(JsonFileToJsVarible(TaxonomyHierarchyPath, "", "tax_hierarchies"));
            TaxFiles.Add(JsonFileToJsVarible(TaxonomyConceptPath, "", "tax_concepts"));
            var jsonsimplifiedvalidation = "";
            if (this.SimpleValidationRules.Count > 0) 
            {
                jsonsimplifiedvalidation = Utilities.Converters.ToJson(this.SimpleValidationRules);
            }
            TaxFiles.Add(JsonToJsVarible(jsonsimplifiedvalidation, "tax_validations"));
            TaxFiles.Add(JsonFileToJsVarible(TaxonomyLabelPath, "", "tax_labels"));
        }

        public string JsonFileToJsVarible(string path, string jsonvalue, string variablename, bool overwrite = false) 
        {
            var filename = Utilities.Strings.GetFileName(path);
            if (String.IsNullOrEmpty(variablename))
            {
                variablename = Utilities.Strings.AlfaNumericOnly(filename.Remove(filename.LastIndexOf(".")));
            }
            var jsfilename = variablename+".js";
            var jsfilepath = this.TaxonomyLayoutFolder + jsfilename;
            if (!System.IO.File.Exists(jsfilepath) || overwrite)
            {
                if (System.IO.File.Exists(path))
                {
                    if (String.IsNullOrEmpty(jsonvalue))
                    {
                        jsonvalue = System.IO.File.ReadAllText(path);
                    }
                    var sb = new StringBuilder();
                    sb.Append(String.Format("var {0} = {1};", variablename, jsonvalue));
                    Utilities.FS.WriteAllText(jsfilepath, sb.ToString());
                }
            }
            return jsfilename;
        }

        public string JsonToJsVarible(string jsonvalue, string variablename, bool overwrite = false)
        {

            var jsfilename = variablename + ".js";
            var jsfilepath = this.TaxonomyLayoutFolder + jsfilename;
            if (!System.IO.File.Exists(jsfilepath) || overwrite)
            {

                if (!String.IsNullOrEmpty(jsonvalue))
                {
                    var sb = new StringBuilder();
                    sb.Append(String.Format("var {0} = {1};", variablename, jsonvalue));
                    Utilities.FS.WriteAllText(jsfilepath, sb.ToString());
                }
            }
            return jsfilename;
        }

        public void GenerateValidationFunctions() 
        {
            GenerateCSFile();
            CompileCSFile();
        }

        private void GenerateCSFile()
        {
            if (!System.IO.File.Exists(this.TaxonomyValidationCsPath) || LogicalModel.Settings.Current.ReloadFullTaxonomyButStructure)
            {
                var sb_functions = new StringBuilder();
                var sb_dictionary = new StringBuilder();
                var sb_cs = new StringBuilder();
                var tab = "  ";
                foreach (var val in ValidationRules)
                {
                    if (!string.IsNullOrEmpty(val.FunctionString))
                    {
                        sb_dictionary.AppendFormat("{0}{0}this.FunctionDictionary.Add(\"{1}\", this.{1});\r\n", tab, val.FunctionName);
                        sb_functions.AppendLine("       //" + val.OriginalExpression);
                        sb_functions.AppendLine(val.FunctionString);
                    }

                }
                sb_cs.AppendLine("using System;");
                sb_cs.AppendLine("using System.Collections.Generic;");
                sb_cs.AppendLine("using System.Linq;");
                sb_cs.AppendLine("using System.Text;");
                sb_cs.AppendLine("using LogicalModel.Expressions;");
                sb_cs.AppendLine("using LogicalModel.Base;");
                sb_cs.AppendLine("using LogicalModel.Validation;");
                sb_cs.AppendLine("using Utilities;");

                sb_cs.AppendLine("namespace LogicalModel.Validation");
                sb_cs.AppendLine("{");

                sb_cs.AppendLine("  public class ValidationsX: ValidationFunctionContainer");
                sb_cs.AppendLine("  {");

                sb_cs.AppendLine("      public ValidationsX()");
                sb_cs.AppendLine("      {");

                sb_cs.AppendLine(sb_dictionary.ToString());

                sb_cs.AppendLine("      }");

                sb_cs.AppendLine(sb_functions.ToString());

                sb_cs.AppendLine("  }");
                sb_cs.AppendLine("}");




                Utilities.FS.WriteAllText(this.TaxonomyValidationCsPath, sb_cs.ToString());
            }
        }

        private void CompileCSFile()
        {
            if (!System.IO.File.Exists(this.TaxonomyValidationDotNetLibPath) || LogicalModel.Settings.Current.ReloadFullTaxonomyButStructure)
            {
                var content = System.IO.File.ReadAllText(this.TaxonomyValidationCsPath);

                CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
                string Output = this.TaxonomyValidationDotNetLibPath;

                System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
                //Make sure we generate an EXE, not a DLL
                parameters.GenerateExecutable = false;
                parameters.OutputAssembly = this.TaxonomyValidationDotNetLibPath;
                parameters.IncludeDebugInformation = true;
                parameters.TempFiles = new TempFileCollection(".", true);
                var logicalmodelassembly = typeof(LogicalModel.Base.FactBase).Assembly;
                var logicalreferences = logicalmodelassembly.GetReferencedAssemblies();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                parameters.ReferencedAssemblies.Add(logicalmodelassembly.Location);

                foreach (var assemblyname in logicalreferences)
                {
                    Assembly asm = assemblies.FirstOrDefault(i => i.GetName().Name == assemblyname.Name);
                    if (asm != null)
                    {
                        parameters.ReferencedAssemblies.Add(asm.Location);
                    }
                    else
                    {
                        Console.WriteLine("Assembly " + assemblyname.Name + " was not found!");
                    }
                }

                CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, content);

                if (results.Errors.Count > 0)
                {
                    foreach (CompilerError CompErr in results.Errors)
                    {
                        var error = "Line number " + CompErr.Line +
                                    ", Error Number: " + CompErr.ErrorNumber +
                                    ", '" + CompErr.ErrorText + ";" +
                                    Environment.NewLine + Environment.NewLine;
                        Console.WriteLine(error);
                    }
                }
                else
                {
                    //Successful Compile
                    Console.WriteLine(String.Format("File {0} was successfully compiled to {1}",
                        this.TaxonomyValidationCsPath, this.TaxonomyValidationDotNetLibPath));

                }

            }
        }
        

        public virtual void LoadHierarchy() 
        {

        }


        public virtual void LoadUnits()
        {

        }

        public virtual void LoadValidationFunctions() {
            if (this.ValidationFunctionContainer == null)
            {
                if (System.IO.File.Exists(this.TaxonomyValidationDotNetLibPath))
                {
                    var assembly = Assembly.LoadFile(this.TaxonomyValidationDotNetLibPath);
                    var type = assembly.GetTypes().FirstOrDefault(i => i.BaseType == typeof(ValidationFunctionContainer));
                    if (type != null)
                    {
                        ValidationFunctionContainer vfc = (ValidationFunctionContainer)Activator.CreateInstance(type);
                        this.ValidationFunctionContainer = vfc;
                    }
                    else
                    {
                        Console.WriteLine(String.Format("There is no {0} in assembly {1}", typeof(ValidationFunctionContainer).Name, this.TaxonomyValidationDotNetLibPath));
                    }
                }
            }
        }
        
        public virtual void LoadSchemaElements()
        {
            Console.WriteLine("Load Elements");

            if (!System.IO.File.Exists(TaxonomySchemaElementsPath) || Settings.Current.ReloadFullTaxonomyButStructure)
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

            if (!System.IO.File.Exists(TaxonomyConceptPath) || Settings.Current.ReloadFullTaxonomyButStructure)
            {

                var conceptelements = SchemaElements.Where(i => i.Namespace == ConceptNameSpace).ToList();
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
                    concept.ItemType = conceptelement.Type.IndexOf(":") > -1 ? conceptelement.Type.Substring(conceptelement.Type.IndexOf(":") + 1) : conceptelement.Type;

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

        public virtual void PopulateTableGroups() 
        {

        }
        public virtual void LoadGeneral() 
        {

        }
        //public virtual void LoadTableGroups()
        //{
        //    Console.WriteLine("Load TableGroups");

        //    if (!System.IO.File.Exists(TaxonomyTableGroupPath) || Settings.Current.ReloadFullTaxonomyButStructure)
        //    {
        //        PopulateTableGroups();
        //        var jsoncontent = Utilities.Converters.ToJson(this.Module.TableGroups);
        //        Utilities.FS.WriteAllText(TaxonomyTableGroupPath, jsoncontent);
        //    }
        //    else
        //    {

        //        var jsoncontent = System.IO.File.ReadAllText(TaxonomyTableGroupPath);
        //        this.Module.TableGroups = Utilities.Converters.JsonTo<List<TableGroup>>(jsoncontent);

        //    }
        //    Console.WriteLine("Load TableGroups completed");
        //}


        public virtual void LoadInstance(string filepath)
        {

        }

        public virtual void AfterRefrencesLoadedFromCache()
        {

        }

        public virtual void ClearCache()
        {

        }
        public virtual Instance GetNewInstance()
        {
            return new Instance();
        }

        #region Clear

        public void Clear_All_But_Structure()
        {
            Clear_Concepts();
            Clear_Hierarchies();
            Clear_Tables();
            Clear_Layout();
            Clear_Labels();
            Clear_SchemaElements();
            Clear_Facts();
            Clear_Validations();

        }
        
        public void Clear_All()
        {
            Clear_Structure();
            Clear_All_But_Structure();
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
            Utilities.FS.DeleteFile(TaxonomyModulePath);
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

        public void Clear_Validations()
        {
            Utilities.FS.DeleteFile(TaxonomyValidationPath);
            Utilities.FS.DeleteFile(TaxonomyValidationCsPath);
            Utilities.FS.DeleteFile(TaxonomyValidationDotNetLibPath);
        }
        
        #endregion
    }
}
