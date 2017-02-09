using BaseModel;
using LogicalModel.Base;
using LogicalModel;
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

        private ContextContainer _Contexts = new ContextContainer();
        [JsonIgnore]
        public ContextContainer Contexts
        {
            get { return _Contexts; }
            set { _Contexts = value; }
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

        private InstanceFactDictionary _FactDictionary = new InstanceFactDictionary();
        [JsonProperty]
        [JsonConverter(typeof(InstanceDictionaryConverter))]
        public InstanceFactDictionary FactDictionary { get { return _FactDictionary; } set { _FactDictionary = value; } }

        private List<FilingIndicator> _FilingIndicators = new List<FilingIndicator>();
        [JsonProperty]
        public List<FilingIndicator> FilingIndicators { get { return _FilingIndicators; } set { _FilingIndicators = value; } }

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

        private Dictionary<string, int> _FactParts = new Dictionary<string, int>();
        [JsonProperty]
        public Dictionary<string, int> FactParts { get { return _FactParts; } set { _FactParts = value; } }

        protected Dictionary<int, string> CounterFactParts = new Dictionary<int, string>();


        private Dictionary<int, HashSet<int>> _TypedFactMembers = new Dictionary<int, HashSet<int>>();
        [JsonProperty]
        public Dictionary<int, HashSet<int>> TypedFactMembers { get { return _TypedFactMembers; } set { _TypedFactMembers = value; } }
       
        public Dictionary<int, int> CounterTypedFactMembers = new Dictionary<int, int>();

        public void CreateHtml() 
        {
            var folder = Utilities.Strings.GetFolder(HtmlPath);
            var htmlbuilder = new StringBuilder();
            var template = System.IO.File.ReadAllText("InstanceTemplate.html");
            var html = template;
            htmlbuilder.AppendLine(html);

            Utilities.FS.WriteAllText(HtmlPath, htmlbuilder.ToString());
        }

        public List<InstanceFact> GetFactsByTaxKey(string stringkey) 
        {
            var facts = new List<InstanceFact>();
            var intkey = Taxonomy.GetFactIntKey(stringkey).ToArray();
            return GetFactsByTaxKey(intkey);
    
        }

        public List<InstanceFact> GetFactsByTaxKey(int[] key)
        {
            var facts = new List<InstanceFact>();
            if (this.FactDictionary.FactsByTaxonomyKey.ContainsKey(key))
            {
                var indexes = this.FactDictionary.FactsByTaxonomyKey[key];
                facts.AddRange(indexes.Select(i => this.FactDictionary.FactsByIndex[i]));
            }
            return facts;
        }
        public List<InstanceFact> GetFactsByInstKey(string stringkey)
        {
            var facts = new List<InstanceFact>();
            var intkey = Taxonomy.GetFactIntKey(stringkey).ToArray();
            return GetFactsByInstKey(intkey);

        }

        public List<InstanceFact> GetFactsByInstKey(int[] key)
        {
            var facts = new List<InstanceFact>();
            if (this.FactDictionary.FactsByInstanceKey.ContainsKey(key))
            {
                var ix = this.FactDictionary.FactsByInstanceKey[key];
                facts.Add(this.FactDictionary.FactsByIndex[ix]);
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
        public int[] GetFactIntKey(List<string> instancefactparts) 
        {
            var result = new List<int>();
            foreach (var part in instancefactparts) 
            {
                var id = GetPartID(part);
                result.Add(id);
            }
            return result.ToArray();
        }

        public int[] GetFactIntKey(string instancefactpartsstr)
        {
            var result = new List<int>();
            var instancefactparts = instancefactpartsstr.Split(new string[] { Literals.Coma }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in instancefactparts)
            {
                var id = GetPartID(part);
                result.Add(id);
            }
            return result.ToArray();
        }

        public int GetPartID(string part) 
        {
            var id = this.Taxonomy.FactParts.ContainsKey(part) ?
                    this.Taxonomy.FactParts[part] :
                    this.FactParts.ContainsKey(part) ?
                    this.FactParts[part] :
                    -1;
            if (id == -1) 
            {

            }
            return id;
        }
        public string GetPartString(int part)
        {
            var id = this.Taxonomy.CounterFactParts.ContainsKey(part) ?
                    this.Taxonomy.CounterFactParts[part] :
                    this.CounterFactParts.ContainsKey(part) ?
                    this.CounterFactParts[part] :
                    "";
            if (id == "")
            {

            }
            return id;
        }

        public int[] GetFactKeyByIdString(string factstring) 
        {
            if (factstring.StartsWith("I:"))
            {
                var id = Utilities.Converters.FastParse(factstring.Substring(2));
                if (id > -1 && id < this.Facts.Count)
                {
                    var instancefact = this.Facts[id];

                    return instancefact.TaxonomyKey.ToArray();
                }
            }
            if (factstring.StartsWith("T:"))
            {
                var id = Utilities.Converters.FastParse(factstring.Substring(2));
                var key = Taxonomy.FactsManager.GetFactKey(id);
                return key;

            }
            return null;
        }
        public int GetTaxFactID(string factidstring) 
        {
            if (factidstring.StartsWith("I:"))
            {
                var id = Utilities.Converters.FastParse(factidstring.Substring(2));
                if (id > -1 && id < this.Facts.Count)
                {
                    var fact = this.Facts[id];
                    var taxfactid = Taxonomy.FactsManager.GetFactIndex(fact.TaxonomyKey);
                    return taxfactid;
                }
            }
            if (factidstring.StartsWith("T:"))
            {
                var id = Utilities.Converters.FastParse(factidstring.Substring(2));
                return id;

            }
            return -2;
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
                var key = Taxonomy.FactsManager.GetFactKey(id);
                var stringkey = Taxonomy.GetFactStringKey(key);
                if (FactDictionary.FactsByTaxonomyKey.ContainsKey(key))
                {
                    var facts = FactDictionary.FactsByTaxonomyKey[key];
                    if (facts.Count == 1)
                    {
                        var ix = facts.FirstOrDefault();
                        return FactDictionary.FactsByIndex[ix];
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
                if (Taxonomy.FactsManager.Count > id)
                {
                    var fact = new FactBase();
                    var key = Taxonomy.FactsManager.GetFactKey(id);
                    var stringkey = Taxonomy.GetFactStringKey(key);
                    fact.SetFromString(stringkey);
                    return fact;
                }

            }
            return null;
        }

        public int[] GetFactKeyByIndexString(string factstring)
        {
            if (factstring.StartsWith("I:"))
            {
                var id = Utilities.Converters.FastParse(factstring.Substring(2));
                if (id > -1 && id < this.Facts.Count)
                {
                    return this.Facts[id].InstanceKey;
                }
            }
            if (factstring.StartsWith("T:"))
            {
                var id = Utilities.Converters.FastParse(factstring.Substring(2));
                if (Taxonomy.FactsManager.Count > id)
                {
                    var key = Taxonomy.FactsManager.GetFactKey(id);
                    return key;
                }

            }
            return null;
        }

        public List<InstanceFact> GetFactsByIdString(string idstring) 
        {
            var ix = Utilities.Converters.FastParse(idstring.Substring(2));
            var result = new List<InstanceFact>();
            if (idstring.StartsWith("I:"))
            {
                result.Add( this.FactDictionary.FactsByIndex[ix]);
            }
            if (idstring.StartsWith("T:"))
            {
                var key = Taxonomy.FactsManager.GetFactKey(ix);
                if (this.FactDictionary.FactsByTaxonomyKey.ContainsKey(key)) 
                {
                    var instanceindexes = this.FactDictionary.FactsByTaxonomyKey[key];
                    foreach (var instanceix in instanceindexes) 
                    {
                        result.Add(this.FactDictionary.FactsByIndex[instanceix]);
                    }

                }
                
            }
            return result;
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
                if (this.Taxonomy.HasFact(fact.TaxonomyKey))
                {
                    var cellvalues=fact.Cells;
                    if (fact.Cells.Count == 0)
                    {
                        //cellvalues = this.Taxonomy.GetCellsOfFact(fact.TaxonomyKey);
                    }
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
                    if (sb_invalidfacts.Length == 0) 
                    {
                        sb_invalidfacts.Append("Invalid facts: ");
                    }
                    sb_invalidfacts.Append(String.Format("<{0} id:\"{1}\" ct:\"{2}\" val:\"{3}\">, ", fact.Concept.FullName, fact.ID, fact.ContextID, fact.Value));
                }
                TaxValidation.ValidateByTypedDimension(fact, ValidationRuleResults, invalidtypevalues);
                TaxValidation.ValidateByConcept(fact, ValidationRuleResults, invalidtypevalues);
                if (invalidtypevalues.Length > 0) 
                {
                    invalidtypevalues.Insert(0, "Invalid Types: ");
                }
            }

            var reports = reportsdictionary.Keys.OrderBy(i => i, StringComparer.Ordinal).ToList();
            var MissingFilingindicators =new List<string>();
            var sb_missingfind = new StringBuilder();
            foreach (var report in reports) 
            {
                var table = this.Taxonomy.Tables.FirstOrDefault(i => i.ID == report);
                var find = this.FilingIndicators.FirstOrDefault(i => i.ID == table.FilingIndicator);
                if (find == null || !find.Filed)
                {
                    if (!MissingFilingindicators.Contains(table.FilingIndicator))
                    {
                        if (MissingFilingindicators.Count == 0) 
                        {
                            sb_missingfind.Append("Missing Filing Indicators: ");
                        }
                        MissingFilingindicators.Add(table.FilingIndicator);
                        sb_missingfind.Append(table.FilingIndicator + ", ");
                    }
                }
            }
            AddMessage(messages, String.Format("Taxonomy Module Reference: {0}\r\n", this.TaxonomyModuleReference));
            AddMessage(messages, String.Format("Nr of invalid facts: {0}\r\n", invalidfacts.Count));
            AddMessage(messages, sb_invalidfacts.ToString());
            //AddMessage(messages, String.Format("Facts with no cells: {0}\r\n", factwithoutcells.Count));
            AddMessage(messages, invalidtypevalues.ToString());

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
            AddMessage(messages, sb_missingfind.ToString());

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
            //foreach (var table in Taxonomy.Tables) 
            //{
            //    GetTableExtensions(table);
            //}
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
            var facttuples = this.Facts.Select(i =>new Tuple<int,InstanceFact> (this.Taxonomy.FactsManager.GetFactIndex(i.TaxonomyKey),i)).OrderBy(i=>i.Item1);
            foreach (var facttuple in facttuples)
            {
                var fact = facttuple.Item2;
                var factix = facttuple.Item1;
                if (this.Taxonomy.HasFact(fact.TaxonomyKey))
                {
                    fact.Cells = this.Taxonomy.GetCellsOfFact(factix);
                    var Cells = fact.Cells.ToList();
                    fact.Cells.Clear();
                    foreach (var cell in Cells)
                    {
                

                        var cellobj = new Cell();
                        cellobj.SetFromCellID(cell);

                        if (!DynamicReportCells.ContainsKey(cellobj.Report)) 
                        {
                            DynamicReportCells.Add(cellobj.Report, new DynamicCellDictionary(this));
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
                fact.SetContent();

            }

            foreach (var key in reportdict.Keys)
            {
                var table = Taxonomy.Tables.FirstOrDefault(i => i.ID == key);
                table.InstanceFactsCount = reportdict[key];
            }
        }

        public List<int> GetTypedPartDomainIds(InstanceFact fact) 
        {
            var result = new List<int>();
            for (int i = 0; i < fact.TaxonomyKey.Length; i++) 
            {
                if (TypedFactMembers.ContainsKey(fact.TaxonomyKey[i])) 
                {
                    result.Add(fact.TaxonomyKey[i]);
                }
            }
            return result;
        }
        public List<int> GetTypedPartIds(InstanceFact fact)
        {
            var result = new List<int>();
            for (int i = 0; i < fact.TaxonomyKey.Length; i++)
            {
                if (TypedFactMembers.ContainsKey(fact.TaxonomyKey[i]))
                {
                    result.Add(fact.InstanceKey[i]);
                }
            }
            return result;
        }

        public List<int> GetTypedPartIds(InstanceFact fact, List<int> typeddomains)
        {
            var result = new List<int>();
            for (int i = 0; i < fact.TaxonomyKey.Length; i++)
            {
              
                if (TypedFactMembers.ContainsKey(fact.TaxonomyKey[i]))
                {
                    if (typeddomains.Contains(fact.TaxonomyKey[i]))
                    {
                        result.Add(fact.InstanceKey[i]);
                    }
                }
            }
            return result;
        }
        
        public string GetDynamicCellID(string cellID, FactBase fact)
        {
            var intkeys = this.GetFactIntKey(fact.FactString);
            if (this.FactDictionary.FactsByInstanceKey.ContainsKey(intkeys))
            {
                var ix = this.FactDictionary.FactsByInstanceKey[intkeys];
                var instfact = this.FactDictionary.FactsByIndex[ix];
                return GetDynamicCellID(cellID, instfact);
            }
            else 
            {
            }
            return cellID;
        }
        
        public string GetDynamicCellID(string cellID, int[] instancefactkey)
        {
            if (this.FactDictionary.FactsByInstanceKey.ContainsKey(instancefactkey))
            {
                var ix = this.FactDictionary.FactsByInstanceKey[instancefactkey];
                var instfact = this.FactDictionary.FactsByIndex[ix];
                return GetDynamicCellID(cellID, instfact);
            }
            else
            {
            }
            return cellID;
        }
        
        public string GetDynamicCellID(string cellID, InstanceFact fact) 
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
                    if (this.FactDictionary.FactsByInstanceKey.ContainsKey(fact.InstanceKey))
                    {
                        var ifactix = this.FactDictionary.FactsByInstanceKey[fact.InstanceKey];
                        var ifact = this.FactDictionary.FactsByIndex[ifactix];
                        var cells = ifact.Cells;
                        var cellid = cells.FirstOrDefault();
                        if (string.IsNullOrEmpty(cellid)) 
                        {
                            return "";
                        }
                        cellobj.SetFromCellID(cellid);

                    }
              
                }
                else 
                {
                    Logger.WriteLine("No Dynamic Data found for " + reportid);
                }
        
            }
         
            return cellobj.CellID;
        }

        public List<string> GetCells(string factidstring) 
        {
            var taxfactid = GetTaxFactID(factidstring);
            var instancefact = GetFactByIDString(factidstring);
            var cells = Taxonomy.GetCellsOfFact(taxfactid);
            var result = new List<string>();
            foreach (var cell in cells) 
            {
                String instancecell = "";
                if (instancefact != null) 
                {
                    instancecell = GetDynamicCellID(cell, instancefact);
                }
            
                if (!String.IsNullOrEmpty(instancecell))
                {
                    result.Add(instancecell);
                }
                else 
                {
                    result.Add(cell);
                }
            }
            return result;
        }

        public void SetExtensions() 
        {
            foreach (var table in Taxonomy.Tables) 
            {
                GetTableExtensions(table);
            }
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

        public void SaveFacts(List<InstanceFact> factstosave)
        {
            if (factstosave == null) { factstosave = new List<InstanceFact>(); }
            foreach (var fact in factstosave)
            {
                fact.SetFromString(fact.FactString);
                InstanceFact factitem = null;
                var intkeys = this.GetFactIntKey(fact.FactString);
                if (FactDictionary.FactsByInstanceKey.ContainsKey(intkeys))
                {
                    var factsforkey = FactDictionary.FactsByInstanceKey[intkeys];

                }
                if (factitem == null)
                {
                    FactDictionary.AddFact(fact);
                    Facts.Add(fact);
                    factitem = fact;
                }
                else 
                {
                    factitem.Value = fact.Value;
                }
            }
            SaveToJson();
        }



        public string GetFactStringKey(int[] key)
        {
            var sb = new StringBuilder();
            foreach (var part in key) 
            {
                sb.Append(this.GetPartString(part));
                sb.Append(",");
            }
            return sb.ToString();
        }
    }
}
