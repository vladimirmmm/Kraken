using BaseModel;
using LogicalModel.Base;
using LogicalModel.Models;
using LogicalModel.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Instance
    {
        private String _FullPath = "";
        [JsonProperty]
        public String FullPath { get { return _FullPath; } set { _FullPath = value; } }

        private String _ModulePath = "";
        [JsonProperty]
        public String ModulePath { get { return _ModulePath; } set { _ModulePath = value; } }

        private string _HtmlPath = "";
        public string HtmlPath
        {
            get { return _HtmlPath; }
            set { _HtmlPath = value; }
        }
        private List<InstanceUnit> _Units = new List<InstanceUnit>();
        [JsonProperty]
        public List<InstanceUnit> Units
        {
            get { return _Units; }
            set { _Units = value; }
        } 
        public string JsonValue = "";
        [JsonIgnore]
        public Taxonomy Taxonomy { get; set; }

        [JsonProperty]
        public string TaxonomyModuleReference { get; set; }

        private List<InstanceFact> _Facts = new List<InstanceFact>();
        public List<InstanceFact> Facts { get { return _Facts; } set { _Facts = value; } }

        public List<ValidationRuleResult> ValidationRuleResults = new List<ValidationRuleResult>();

        private Dictionary<string, List<InstanceFact>> _FactDictionary = new Dictionary<string, List<InstanceFact>>();
        [JsonProperty]
        public Dictionary<string, List<InstanceFact>> FactDictionary { get { return _FactDictionary; } set { _FactDictionary = value; } }

        [JsonProperty]
        public List<String> FilingIndicators = new List<string>();

        [JsonProperty]
        public Entity Entity { get; set; }
        [JsonProperty]
        public DateTime? ReportingDate { get; set; }
        [JsonProperty]
        public String ReportingCurrency { get; set; }

        [JsonProperty]
        public Period ReportingPeriod { get; set; }
        [JsonProperty]
        public InstanceUnit ReportingMonetaryUnit { get; set; }

        private Dictionary<string, DynamicCellDictionary> _DynamicReportCells = new Dictionary<string, DynamicCellDictionary>();
        [JsonProperty]
        public Dictionary<string, DynamicCellDictionary> DynamicReportCells
        {
            get { return _DynamicReportCells; }
            set { _DynamicReportCells = value; }
        }


        public void CreateHtml() 
        {
            var folder = Utilities.Strings.GetFolder(HtmlPath);
            var htmlbuilder = new StringBuilder();
            var template = System.IO.File.ReadAllText("InstanceTemplate.html");
            var html = template;
            htmlbuilder.AppendLine(html);

            Utilities.FS.WriteAllText(HtmlPath, htmlbuilder.ToString());
        }

        public List<InstanceFact> GetFacts(string key) 
        {
            var facts = new List<InstanceFact>();
            if (this.FactDictionary.ContainsKey(key)) 
            {
                facts.AddRange(FactDictionary[key]);
            }
            return facts;
        }

        public int GetFactIndex(string factstring) 
        {
            var fact = this.Facts.FirstOrDefault(i => i.FactString == factstring);
            if (fact != null) 
            {
                return fact.IX;
            }
            return -1;
        }

        public InstanceFact GetFactByIDString(string factstring)
        {
            if (factstring.StartsWith("I:"))
            {
                var id = Utilities.Converters.FastParse(factstring.Substring(2));
                if (id > -1 && id < this.Facts.Count)
                {
                    return this.Facts[id];
                }
            }
            if (factstring.StartsWith("T:"))
            {
                var id = Utilities.Converters.FastParse(factstring.Substring(2));
                var key = Taxonomy.FactsIndex[id];
                var stringkey = Taxonomy.GetFactStringKey(key);
                if (FactDictionary.ContainsKey(stringkey))
                {
                    var facts = FactDictionary[stringkey];
                    if (facts.Count == 1)
                    {
                        return facts.FirstOrDefault();
                    }
                }

            }
            return null;
        }

        public FactBase GetFactBaseByIndexString(string factstring)
        {
            if (factstring.StartsWith("I:"))
            {
                var id = Utilities.Converters.FastParse(factstring.Substring(2));
                if (id > -1 && id < this.Facts.Count)
                {
                    return this.Facts[id];
                }
            }
            if (factstring.StartsWith("T:"))
            {
                var id = Utilities.Converters.FastParse(factstring.Substring(2));
                if (Taxonomy.FactsIndex.ContainsKey(id))
                {
                    var fact = new FactBase();
                    var key = Taxonomy.FactsIndex[id];
                    var stringkey = Taxonomy.GetFactStringKey(key);
                    fact.SetFromString(stringkey);
                    return fact;
                }

            }
            return null;
        }


        public List<string> GetFactStringsByFactIdStrings(List<string> factindexes)
        {
            var result = new List<string>();
            foreach (var factindex in factindexes)
            {
                var fact = GetFactBaseByIndexString(factindex);
                result.Add(fact.FactString);

            }
            return result;
        }
        public virtual List<ValidationRuleResult> Validate(List<String> messages) 
        {
            ValidationRuleResults.Clear();
            var sb_invalidfacts = new StringBuilder();
            var factwithoutcells = new List<String>();
            var invalidfacts = new List<String>();
            var reportsdictionary = new Dictionary<string,int>();
            if (ReportingPeriod.Instant.HasValue) 
            {
                if (ReportingPeriod.Instant.Value < Taxonomy.Module.FromDate)
                {
                    AddMessage(messages, String.Format("Taxonomy requires Reporting Date greater than {0:yyyy-MM-dd}. Failed Date: {1:yyyy-MM-dd}", this.Taxonomy.Module.FromDate, ReportingPeriod.Instant));
                }
                else 
                {
                    if (Taxonomy.Module.FromDate.HasValue && ReportingPeriod.Instant.Value > Taxonomy.Module.ToDate) 
                    {
                        AddMessage(messages, String.Format("Taxonomy requires Reporting Date less than {0:yyyy-MM-dd}. Failed Date: {1:yyyy-MM-dd}", this.Taxonomy.Module.FromDate, ReportingPeriod.Instant));

                    }
                }
            }
            var TaxValidation = new TaxonomyValidation(this.Taxonomy);

            var invalidtypevalues = new StringBuilder();
            foreach (var fact in Facts) 
            {
                if (this.Taxonomy.HasFact(fact.FactKey))
                {
                    var cellvalues = this.Taxonomy.GetCellsOfFact(fact.FactKey);
                    var cells ="";
                    foreach(var cellvalue in cellvalues)
                    {
                        var reportid = cellvalue.Remove(cellvalue.IndexOf("<")).Trim();

                        if (!reportsdictionary.ContainsKey(reportid)) 
                        {
                            reportsdictionary.Add(reportid, 0);
                        }

                        cells+=cellvalue +", ";
                    }
                    if (cellvalues.Count == 0)
                    {
                        //sb.AppendFormat("Fact has no cell {0}!\n", fact.FactString);
                        factwithoutcells.Add(fact.FactKey);
                    }
                }
                else 
                {
                    //sb.AppendFormat(String.Format("Invalid Fact ({0}, {1}) {2}!\n", fact.Concept, fact.ContextID, fact.FactString));
                    invalidfacts.Add(fact.FactKey);
                    sb_invalidfacts.Append(String.Format("<{0}|{1}|{2}>, ", fact.Concept.FullName, fact.ContextID, fact.Value));
                }
                TaxValidation.ValidateByTypedDimension(fact, ValidationRuleResults, invalidtypevalues);
                TaxValidation.ValidateByConcept(fact, ValidationRuleResults, invalidtypevalues);
            }
          
            var reports = reportsdictionary.Keys.OrderBy(i=>i).ToList();
            var MissingFilingindicators =new List<string>();
            var sb_missingfind = new StringBuilder();
            foreach (var report in reports) 
            {
                var table = this.Taxonomy.Tables.FirstOrDefault(i => i.ID == report);
                var find = this.FilingIndicators.FirstOrDefault(i => i == table.FilingIndicator);
                if (find == null)
                {
                    if (!MissingFilingindicators.Contains(table.FilingIndicator))
                    {
                        MissingFilingindicators.Add(table.FilingIndicator);
                        sb_missingfind.Append(table.FilingIndicator + ", ");
                    }
                }
            }
            AddMessage(messages, String.Format("Taxonomy Module Reference: {0}\r\n", this.TaxonomyModuleReference));
            AddMessage(messages, String.Format("Nr of invalid facts: {0}\r\n", invalidfacts.Count));
            AddMessage(messages, String.Format("Invalid facts: {0}\r\n", sb_invalidfacts.ToString()));
            AddMessage(messages, String.Format("Facts with no cells: {0}\r\n", factwithoutcells.Count));
            AddMessage(messages, String.Format("Type Validation: \r\n{0}", invalidtypevalues));

            var rsb = new StringBuilder();
            var rsb_full = new StringBuilder();
            var rsbf = new StringBuilder();
            var rsbf_full = new StringBuilder();
            var failedrules = 0;
            var failedfrules = 0;
            if (Taxonomy.ValidationFunctionContainer != null)
            {
                foreach (var rule in Taxonomy.ValidationRules)
                {
                    rule.Function = Taxonomy.ValidationFunctionContainer.FunctionDictionary[rule.FunctionName];
                    var ruleresults = rule.Validate(this);
                    if (ruleresults.Count > 0) 
                    {
                        if (ruleresults.FirstOrDefault().HasAllFind == "2")
                        {
                            rsbf.Append(String.Format("{0}, ", rule.ID));
                            rsbf_full.Append(String.Format("{0} : {1} \r\n", rule.ID, rule.DisplayText));
                            failedfrules++;
                        }
                        else
                        {
                            rsb.Append(String.Format("{0}, ", rule.ID));
                            rsb_full.Append(String.Format("{0} : {1} \r\n", rule.ID, rule.DisplayText));
                            failedrules++;
                        }
                    }
                    ValidationRuleResults.AddRange(ruleresults);
                }
                ValidationRuleResults = ValidationRuleResults.OrderBy(i => i.HasAllFind).ThenBy(i => i.ID).ToList();
                AddMessage(messages, String.Format("Nr of validation rules not passed: {0}", failedrules));
                
                var val_console = String.Format("Validation rules not passed: \r\n{0}", rsb.ToString());
                var val_file = String.Format("Validation rules not passed: \r\n{0}", rsb_full.ToString());
                Logger.WriteLine(val_console);
                messages.Add(val_file);
                AddMessage(messages, String.Format("Nr of validation rules not passed (not considering filing): {0}", failedfrules));

                var valf_console = String.Format("Validation rules not passed (not considering filing): \r\n{0}", rsbf.ToString());
                var valf_file = String.Format("Validation rules not passed (not considering filing): \r\n{0}", rsbf_full.ToString());
                Logger.WriteLine(valf_console);
                messages.Add(valf_file);
            }
            else 
            {
                AddMessage(messages, String.Format("There is no {0} specified for this Taxonomy", typeof(ValidationFunctionContainer).Name));
            }
            AddMessage(messages, String.Format("Nr of missing Filing Indicators: {0}\r\n", MissingFilingindicators.Count));
            AddMessage(messages, String.Format("Missing Filing Indicators: {0}\r\n", sb_missingfind.ToString()));

            return ValidationRuleResults;
        }

        public virtual void Save(string filepath) 
        {

        }
        
        public void AddMessage(List<String> messagecontainer, string message)
        {
            messagecontainer.Add(message);
            Logger.WriteLine(message);
        }
        
        public virtual void SetTaxonomy(Taxonomy xbrlTaxonomy)
        {
            this.Taxonomy = xbrlTaxonomy;
            this.TaxonomyModuleReference = String.IsNullOrEmpty(this.TaxonomyModuleReference) ? xbrlTaxonomy.TaxonomyModulePath : this.TaxonomyModuleReference;
            this.ModulePath = Utilities.Strings.GetLocalPath(TaxonomyEngine.LocalFolder, this.TaxonomyModuleReference);


       

            
        }

        public void SaveToJson() 
        {
            foreach (var table in Taxonomy.Tables) 
            {
                GetTableExtensions(table);
            }
            //var json_instance = Utilities.Converters.ToJson<Instance>(this);
            var json_instance = Utilities.Converters.ToJson(this);
            //Utilities.FS.WriteAllText(this.Taxonomy.CurrentInstancePath, "var currentinstance = " + json_instance + ";");
            Utilities.FS.WriteAllText(this.Taxonomy.CurrentInstancePath, json_instance);

        }
        //C 27.00 (LE 1)<000||040>


        public virtual void Clear() 
        {
            this.Facts.Clear();
            this.DynamicReportCells.Clear();
            this.FactDictionary.Clear();
        }

        public void SetTypedCells()
        {



        }
        private Dictionary<string, int> reportdatadict = new Dictionary<string, int>();

        public void SetCells()
        {
            var reportdict = new Dictionary<string, int>();
            foreach (var fact in this.Facts)
            {
                if (this.Taxonomy.HasFact(fact.FactKey))
                {
                    fact.Cells = this.Taxonomy.GetCellsOfFact(fact.FactKey);
                    foreach (var cell in fact.Cells)
                    {
                

                        var cellobj = new Cell();
                        cellobj.SetFromCellID(cell);

                        if (!DynamicReportCells.ContainsKey(cellobj.Report)) 
                        {
                            DynamicReportCells.Add(cellobj.Report, new DynamicCellDictionary());
                        }
                        var dynamiccellsofreport = DynamicReportCells[cellobj.Report];
                        var table = Taxonomy.Tables.FirstOrDefault(i=>i.ID==cellobj.Report);
                        var dynamicCell = dynamiccellsofreport.AddCells(cellobj, fact, table);
                        if (dynamicCell.CellID != cellobj.CellID) 
                        {

                        }


       
                        var reportid = cellobj.Report;
                        if (!reportdict.ContainsKey(reportid))
                        {
                            reportdict.Add(reportid, 0);
                        }
                        reportdict[reportid] = reportdict[reportid] + 1;

                        var rdatakey = cellobj.Report + "___" + cellobj.Extension;
                        if (!reportdatadict.ContainsKey(rdatakey))
                        {
                            reportdatadict.Add(rdatakey, 1);
                        }
                        reportdatadict[rdatakey] = reportdatadict[rdatakey] + 1;

                    }
                }
                else
                {
                }
            }

            foreach (var key in reportdict.Keys)
            {
                var table = Taxonomy.Tables.FirstOrDefault(i => i.ID == key);
                table.InstanceFactsCount = reportdict[key];
            }
        }

       
   
        public string GetDynamicCellID(string cellID, FactBase fact) 
        {
            var dynamiccellID = cellID;

            var cellobj = new Cell();
            cellobj.SetFromCellID(cellID);

            var reportid = cellobj.Report;

            if (DynamicReportCells.ContainsKey(reportid))
            {
                var dynamicdata = DynamicReportCells[reportid];
                if (dynamicdata != null)
                {
                    if (dynamicdata.CellOfFact.ContainsKey(fact.FactString))
                    {
                        var cellid = dynamicdata.CellOfFact[fact.FactString];
                        cellobj.SetFromCellID(cellid);

                    }
                    else 
                    {
                        //Logger.WriteLine("No Cell found for " + fact.FactString);

                    }
                }
                else 
                {
                    Logger.WriteLine("No Dynamic Data found for " + reportid);
                }
                //var cellsofreportdict = DynamicCellDictionary_Old[reportid];

                //if (cellsofreportdict.ContainsKey(cellobj.FactString)) 
                //{
                //    var cellid = cellsofreportdict[cellobj.FactString];
                //    cellobj.SetFromCellID(cellid);
                //}
            }

            return cellobj.CellID;
        }

        public List<TableInfo> GetTableExtensions(Table table) 
        {
            var result = new List<TableInfo>();
            var extensions = new Hierarchy<LayoutItem>();
            if (this.DynamicReportCells.ContainsKey(table.ID))
            {
                extensions= this.DynamicReportCells[table.ID].GetInstanceExtensions(table);
            }
            else 
            {
                extensions= table.Extensions;

            }
            foreach (var ext in extensions.Children) 
            {
                var ti = new TableInfo();
                ti.ID = String.Format("{0}<{1}>", table.ID, ext.Item.LabelCode);
                ti.Name = ext.Item.LabelContent;
                ti.Description = ext.Item.LabelContent;
                ti.Type = "extension";
                var extkey = table.ID + "___" + ext.Item.LabelCode;
                ti.HasData = reportdatadict.ContainsKey(extkey) ? reportdatadict[extkey] : 0;
                result.Add(ti);
            }
            result = result.OrderByDescending(i => i.HasData > 0).ToList();
            return result;
        }
    
    }
}
