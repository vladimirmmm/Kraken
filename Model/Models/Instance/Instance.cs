﻿using LogicalModel.Base;
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
        private List<Unit> _Units = new List<Unit>();
        [JsonProperty]
        public List<Unit> Units
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
        public Unit ReportingMonetaryUnit { get; set; }

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

        public virtual List<ValidationRuleResult> Validate(List<String> messages) 
        {
            var results = new List<ValidationRuleResult>();
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

            foreach (var fact in Facts) 
            {
                if (this.Taxonomy.Facts.ContainsKey(fact.FactKey))
                {
                    var cellvalues = this.Taxonomy.Facts[fact.FactKey];
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
                    results.AddRange(ruleresults);
                }
                results = results.OrderBy(i => i.HasAllFind).ThenBy(i => i.ID).ToList();
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

            return results;
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
            this.ModulePath = Utilities.Strings.GetLocalPath(TaxonomyEngine.LocalFolder, this.TaxonomyModuleReference);


       

            
        }

        public void SaveToJson() 
        {
            //var json_instance = Utilities.Converters.ToJson<Instance>(this);
            var json_instance = Utilities.Converters.ToJson(this);
            //Utilities.FS.WriteAllText(this.Taxonomy.CurrentInstancePath, "var currentinstance = " + json_instance + ";");
            Utilities.FS.WriteAllText(this.Taxonomy.CurrentInstancePath, json_instance);

        }
        //C 27.00 (LE 1)<000||040>

        private Dictionary<string, Dictionary<string, string>> _DynamicCellDictionary = new Dictionary<string, Dictionary<string, string>>();
        [JsonProperty]
        public Dictionary<string, Dictionary<string, string>> DynamicCellDictionary 
        {
            get { return _DynamicCellDictionary; }
            set { _DynamicCellDictionary = value; }
        }

        public virtual void Clear() 
        {
            this.Facts.Clear();
            this.DynamicCellDictionary.Clear();
            this.FactDictionary.Clear();
        }

        public void SetCells() 
        {
            var reportdict = new Dictionary<string, int>();
            foreach (var fact in this.Facts)
            {
                if (this.Taxonomy.Facts.ContainsKey(fact.FactKey))
                {
                    fact.Cells = this.Taxonomy.Facts[fact.FactKey];
                    foreach (var cell in fact.Cells)
                    {
                        var cellobj = new Cell();
                        cellobj.SetFromCellID(cell);
                        if (string.IsNullOrEmpty(cellobj.Row))
                        {
                            var typedfact = new FactBase();
                            typedfact.Dimensions = fact.Dimensions.Where(i => i.IsTyped).ToList();
                            var typedfactkey = typedfact.FactString.Trim();
                            var reportkey = String.Format("{0}|{1}", cellobj.Report, cellobj.Extension);
                            if (!DynamicCellDictionary.ContainsKey(reportkey))
                            {
                                DynamicCellDictionary.Add(reportkey, new Dictionary<string, string>());
                            }
                            var cellsofreportdict = DynamicCellDictionary[reportkey];
                            if (!cellsofreportdict.ContainsKey(typedfactkey))
                            {

                                cellsofreportdict.Add(typedfactkey, cellobj.Row);
                            }

                        }
                        var reportid = cellobj.Report;
                        if (!reportdict.ContainsKey(reportid))
                        {
                            reportdict.Add(reportid, 0);
                        }

                        reportdict[reportid] = reportdict[reportid] + 1;

                    }


                }
                else 
                {
                }
            }
            foreach (var cellsofreport in DynamicCellDictionary)
            {
                var rowix = 0;
                for (var i = 0; i < cellsofreport.Value.Count; i++)
                {
                    var itemkey = cellsofreport.Value.Keys.ElementAt(i);
                    rowix++;
                    cellsofreport.Value[itemkey] = String.Format("{0}", rowix);
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

            var typedfact = new FactBase();
            typedfact.Dimensions = fact.Dimensions.Where(i => i.IsTyped).ToList();
            if (typedfact.Dimensions.Count == 0) 
            {
                return cellID;
            }
            var typedfactkey = typedfact.FactString;
            var reportkey = String.Format("{0}|{1}", cellobj.Report, cellobj.Extension);

            if (DynamicCellDictionary.ContainsKey(reportkey))
            {
                var cellsofreportdict = DynamicCellDictionary[reportkey];
                if (cellsofreportdict.ContainsKey(typedfactkey)) 
                {
                    var rowid = cellsofreportdict[typedfactkey];
                    cellobj.Row = rowid;
                }
            }

            return cellobj.CellID;
        }
    }
}