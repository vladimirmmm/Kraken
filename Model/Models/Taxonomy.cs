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
    public partial class Taxonomy : DocumentCollection
    {

        public new TaxonomyDocument EntryDocument 
        {
            get { return (TaxonomyDocument)base.EntryDocument; }
            set { base.EntryDocument=value;}
        }
        //public string ConceptNameSpace = "";
        public string Prefix = "";
        public static string Lang = "en";

        public List<Table> Tables = new List<Table>();
        public TaxonomyModule Module = new TaxonomyModule();
        public List<TaxonomyDocument> TaxonomyDocuments = new List<TaxonomyDocument>();
        public List<Label> TaxonomyLabels = new List<Label>();
        public Dictionary<string, Concept> Concepts = new Dictionary<string, Concept>();
        public List<Element> DimensionItems = new List<Element>();
        public List<Element> Domains = new List<Element>();
        public Dictionary<string, Label> TaxonomyLabelDictionary = new Dictionary<string, Label>();
        public Dictionary<string, int> FactParts = new Dictionary<string, int>();
        //public IndexDictionary FactsOfParts = new IndexDictionary();
        public FactsPartsDictionary FactsOfParts = null;

        public Dictionary<int, string> CounterFactParts = new Dictionary<int, string>();
        //public SortedDictionary<int, List<int>> MembersOfDimensionDomains = new SortedDictionary<int, List<int>>();
        public Dictionary<int, int> DimensionDomainsOfMembers = new Dictionary<int, int>();
        public Dictionary<string, string> DomainAliases = new Dictionary<string, string>();
        public Dictionary<string, List<string>> DomainsofDimensions = new Dictionary<string, List<string>>();

        public Dictionary<int, string> CellIndexDictionary = new Dictionary<int, string>();

        public List<Element> SchemaElements = new List<Element>();

        public List<Hierarchy<QualifiedItem>> Hierarchies = new List<Hierarchy<QualifiedItem>>();

        public List<InstanceUnit> Units = new List<InstanceUnit>();

        public Dictionary<string, Element> SchemaElementDictionary = new Dictionary<string, Element>();

        private IntervalList _TypedIntervals = null;
        [JsonIgnore]
        public IntervalList TypedIntervals
        {
            get 
            {
                if (_TypedIntervals == null)
                {
                    _TypedIntervals = new IntervalList();
                    var typedfactparts = this.Module.TypedDimensions.Keys.ToList();
                    foreach (var typedfactpart in typedfactparts)
                    {
                        if (this.FactsOfParts.ContainsKey(typedfactpart))
                        {
                            var typedinterval = this.FactsOfParts[typedfactpart];
                            foreach (var interval in typedinterval.Intervals)
                            {
                                foreach (var ix in interval.AsEnumerable())
                                {
                                    _TypedIntervals.Add(ix);

                                }
                            }
                        }
                    }
                }
                return _TypedIntervals;
            }  
        }

        /*
       private Dictionary<int[], List<Int64>> _Facts = new Dictionary<int[], List<Int64>>(new Utilities.IntArrayEqualityComparer());
       public Dictionary<int[], List<Int64>> Facts 
       {
           get { return _Facts; }
           set { _Facts = value; }
       }
         */

       private Dictionary<int[], List<String>> _Facts = new Dictionary<int[], List<String>>(new Utilities.IntArrayEqualityComparer());
       public Dictionary<int[], List<String>> Facts 
       {
           get { return _Facts; }
           set { _Facts = value; }
       }
       
        //public Dictionary<string, List<int[]>> FactsOfConcepts = new Dictionary<string, List<int[]>>();
        //public Dictionary<string, List<int>> FactsOfDimensions = new Dictionary<string, List<int>>();
        //public Dictionary<string, Dictionary<int, bool>> FactsOfDimensions = new Dictionary<string, Dictionary<int, bool>>();
        //public Dictionary<string, HashSet<int>> FactsOfDimensions = new Dictionary<string, HashSet<int>>();
        //public Dictionary<int, int[]> FactsIndex = new Dictionary<int, int[]>();
        
        //public List<int[]> FactsIndex = new List<int[]>();

        //public Dictionary<int[], int> FactKeyIndex = new Dictionary<int[], int>(new Utilities.IntArrayEqualityComparer());

        //public List<int> FactsLengths = new List<int>();
   
        public Dictionary<string, List<String>> Cells = new Dictionary<string, List<String>>();

        private List<ValidationRule> _ValidationRules = new List<ValidationRule>();
        public List<ValidationRule> ValidationRules { get { return _ValidationRules; } set { _ValidationRules = value; } }
        private Dictionary<string, GeneralValidationParameter> _ValidationParameters = new Dictionary<string, GeneralValidationParameter>();
        public Dictionary<string, GeneralValidationParameter> ValidationParameters { get { return _ValidationParameters; } set { _ValidationParameters = value; } }

        public IEnumerable<int> FactIndexEnumerable() 
        {
            return this.FactsManager.IndexesAsEnumerable();
        }


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
        public Dictionary<string, List<string>> domdict = new Dictionary<string, List<string>>();


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
        public static string StructureFileName = "Structure.json";
        public string TaxonomyStructurePath
        {
            get { return ModuleFolder + StructureFileName; }
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
        public string TaxonomyCellIndexPath
        {
            get { return ModuleFolder + "CellIndex.json"; }
        }
        public string TaxonomySchemaElementsPath
        {
            get { return ModuleFolder + "SchemaElements.json"; }
        }
        //public string TaxonomyValidationPath
        //{
        //    get { return ModuleFolder + "Validations.json"; }
        //}
        public string TaxonomyValidationFolder
        {
            get { return ModuleFolder + "Validations\\"; }
        }
        public string TaxonomyFactsFolder
        {
            get { return ModuleFolder + "Facts\\"; }
        }
        public string TaxonomyValidationPathFormat
        {
            get { return ModuleFolder + "Validations\\Validations_{0}.json"; }
        }
        public string TaxonomyFactsPathFormat
        {
            get { return ModuleFolder + "Facts\\Facts_{0}.json"; }
        }
        public string TaxonomyFactDictionaryPath
        {
            get { return ModuleFolder + "Facts\\FactDictionary.json"; }
        }
        public string TaxonomySimpleValidationPath
        {
            get { return ModuleFolder + "Validations\\SimpleValidations.json"; }
        }
        public string TaxonomySettingsPath
        {
            get { return ModuleFolder + "ConceptSettings.json"; }
        }
        public string TaxonomyValidationCsPath
        {
            get { return ModuleFolder + "Validations\\Validations.cs"; }
        }

        public string TaxonomyValidationDotNetLibPath
        {
            get { return ModuleFolder + "Validations\\Validations.dll"; }
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

        public List<string> numericxbrltypes = new List<string>() { 
                                "xbrli:decimalItemType", 
                                "xbrli:floatItemType", 
                                "xbrli:doubleItemType", 
                                "xbrli:monetaryItemType", 
                                "xbrli:pureItemType", 
                                "xbrli:integerItemType",
                                "xbrli:nonPositiveIntegerItemType",
                                "xbrli:negativeIntegerItemType",
                                "xbrli:longItemType",
                                "xbrli:intItemType",
                                "xbrli:shortItemType",
                                "xbrli:byteItemType",
                                "xbrli:nonNegativeIntegerItemType",
                                "xbrli:unsignedLongItemType",
                                "xbrli:unsignedIntItemType",
                                "xbrli:unsignedShortItemType",
                                "xbrli:unsignedByteItemType",
                                "xbrli:positiveIntegerItemType",
                            };
        public List<string> datexbrltypes = new List<string>() { "xbrli:dateItemType" };
        public List<string> stringxbrltypes = new List<string>() { "xbrli:stringItemType" };
        public List<string> booleanxbrltypes = new List<string>() { "xbrli:booleanItemType" };
        public List<string> enumerationxbrltypes = new List<string>() { "xbrli:QNameItemType" };

        public Taxonomy(string entrypath) 
        {
            var localentrypath = Utilities.Strings.GetLocalPath(TaxonomyEngine.LocalFolder, entrypath);
            ModuleFolder = localentrypath.Replace(".xsd", "\\");
            this.TaxonomyDocuments.Clear();
            SourceTaxonomyPath = entrypath;
            LogicalModel.Table.LabelAccessor = FindLabel;
            FactsOfParts=RelationDictionary.GetFactsOfParts(this);
            FactsManager.FactsOfPages.Folder = () => this.TaxonomyFactsFolder;
           // var path =
            //FactsOfParts.AccessHashset = (key) => Utilities.FS.ReadAllText(this.TaxonomyFactsFolder + key.ToString() + ".ix");
            //FactsOfParts.SaveHashSet = (key,str) => Utilities.FS.WriteAllText(this.TaxonomyFactsFolder + key.ToString() + ".ix",str);
        
        }
        private LogicalModel.Label GetLabel(string key) 
        {
            var label = TaxonomyLabelDictionary[key];
            if (String.IsNullOrEmpty(label.Code))
            {
                var ix = key.IndexOf("[");
                var engkey = key.Remove(key.IndexOf("["), 4).Insert(ix, "[en]");
                if (TaxonomyLabelDictionary.ContainsKey(engkey))
                {
                    var englabel = TaxonomyLabelDictionary[engkey];
                    label.Code = englabel.Code;
                }
            }
            return label;
        }
        public LogicalModel.Label FindLabel(string key, bool log=true)
        {
            key = key.ToLower();
           
            if (TaxonomyLabelDictionary.ContainsKey(key))
            {
                return GetLabel(key);
            }
            else
            {
                key = key.Replace(LogicalModel.Label.labelprefix, "");
                if (TaxonomyLabelDictionary.ContainsKey(key))
                {
                    return GetLabel(key);
                }
                else
                {
                    if (log)
                    {
                        //Logger.WriteLine(String.Format("Label {0} was not found!", key));
                    }
                }
            }
            return null;
        }

        public Dictionary<string, List<Element>> TypedDimensions = new Dictionary<string, List<Element>>();
        public virtual void LoadTypedDimensions() 
        {
            TypedDimensions.Clear();
            var elements = this.SchemaElements.Where(i => i.Namespace.EndsWith("_typ")).ToList();
            foreach (var element in elements)
            {
                if (!TypedDimensions.ContainsKey(element.Namespace)) 
                {
                    TypedDimensions.Add(element.Namespace, new List<Element>());
                }
                TypedDimensions[element.Namespace].Add(element);
            }
        }

        public Dictionary<string, List<Element>> DomainMembers = new Dictionary<string, List<Element>>();
        public virtual void LoadDomainMembers()
        {
            DomainMembers.Clear();
            var elements = this.SchemaElements.Where(i => i.Type == "nonnum:domainItemType").ToList();
            foreach (var element in elements)
            {
                var key = String.Format("{0}:{1}", element.Namespace, element.Name);
                if (!DomainMembers.ContainsKey(key))
                {
                    DomainMembers.Add(key, new List<Element>());
                }
                DomainMembers[key].Add(element);
            }
        }

        public static bool IsTyped(string domain)
        {
            var result = false;
            var ix = domain.IndexOf(":", StringComparison.Ordinal);
            var ns = ix > -1 ? domain.Remove(ix) : domain;
            if (TaxonomyEngine.CurrentEngine != null &&
                TaxonomyEngine.CurrentEngine.CurrentTaxonomy != null &&
                TaxonomyEngine.CurrentEngine.CurrentTaxonomy.TypedDimensions.ContainsKey(ns))
            {
                result = true;
            }
            else 
            {
                return false;
                //return Utilities.Strings.ContainsCount(":", domain) > 1;
            }
            return result;
        }
        public static bool IsTyped(Taxonomy taxonomy, int key)
        {
            return taxonomy.IsTyped(key);
        }
        public bool IsTyped(int key) 
        {
            var result = this.Module.TypedDimensions.ContainsKey(key);
            return result;
        }
        public virtual TaxonomyDocument GetDocumentByTargetNamespace(string targetnamespace) 
        {
            return null;
        } 
        public int GetDimensionDomainPart(int factpartkey) 
        {
            //var maxKey = Array.BinarySearch(MembersOfDimensionDomainsIndex, factpartkey);
            //var minix = maxKey >= 0 ? maxKey : ~maxKey - 1;
            //return MembersOfDimensionDomainsIndex[minix];
            return DimensionDomainsOfMembers.ContainsKey(factpartkey) ? DimensionDomainsOfMembers[factpartkey] : -1;
            
            //var keys = MembersOfDimensionDomains.Keys.ToList();
            //for (int i =0;i<keys.Count;i++)
            //{
            //    if (factpartkey<keys[i])
            //    {
            //        return keys[i - 1];
            //    }
            //}
            //return keys[keys.Count-1];
        
        }


        public class FactFilter
        {
            public List<int> MemberFilters = new List<int>();
            public List<List<int>> DomainFilterCombinations = new List<List<int>>();

            public List<int> GetIndexes(FactsPartsDictionary factsofparts)
            {
                var memebrindexes = GetIndexes(MemberFilters, factsofparts);
                var result =new List<int>();
                var actions = new List<Action>();
                var locker = new object();
                foreach (var domainfilter in DomainFilterCombinations)
                {
                    actions.Add(() =>
                    {
                        var partialresult = GetIndexes(domainfilter, factsofparts);
                        lock (locker)
                        {
                            result.AddRange(partialresult);
                        }
                    });
                }
             
                Task.WaitAll(actions.Select(i => Task.Factory.StartNew(i)).ToArray());
                return result;
            }

            public IEnumerable<int> GetIndexes(List<int> parts, FactsPartsDictionary factsofparts)
            {
                if (parts.Count == 0) { return new List<int>(); }
                if (parts.Count == 1) { return factsofparts[parts[0]]; }
                var first2 = parts.Take(2).ToList();
                var remaining = parts.Skip(2).ToList();

                var result = IntersectParts(first2[0], first2[1], factsofparts);
                foreach (var part in remaining)
                {
                    result = Utilities.Objects.IntersectSorted(result, factsofparts[part], null);
                }
                return result;

            }

            private Dictionary<int[], IEnumerable<int>> InterSectCache = new Dictionary<int[], IEnumerable<int>>();
            private int maxcount = 20;
            private IEnumerable<int> IntersectParts(int part1, int part2, FactsPartsDictionary factsofparts)
            {
                var key = new int[] { part1, part2 };
                if (InterSectCache.ContainsKey(key))
                {
                    return InterSectCache[key];
                }
                else
                {
                    var pool1 = factsofparts[part1];
                    var pool2 = factsofparts[part2];
                    var result = Utilities.Objects.IntersectSorted(pool1, pool2, null);
                    InterSectCache.Add(key, result);
                    return result;
                }

            }
        }


        public FactFilter GetFactPartPool(int[] fact, FactsPartsDictionary factsofparts) 
        {
            var filter = new FactFilter();
            var domainkeys = fact.Where(i => this.DimensionDomainsOfMembers.ContainsKey(i)).ToList();
            var memberkeys = fact.Except(domainkeys).ToList();


            var domainpool = new List<List<int>>();
            foreach (var domainkey in domainkeys)
            {
                domainpool.Add(new List<int>() { -1, domainkey });
            }

            var domainparts = MathX.CartesianProduct(domainpool).ToList();
            filter.MemberFilters.AddRange(memberkeys.OrderBy(i => factsofparts[i].Count));
            foreach (var domainpart in domainparts) 
            {

                var domainpartpool = domainpart.Where(i => i != -1).OrderBy(i => factsofparts[i].Count).ToList();
                filter.DomainFilterCombinations.Add(domainpartpool);
       
            }
            return filter;
        }

        public List<int> FilterFacts(int[] key, FactsPartsDictionary factsofparts)
        {
            var filter = GetFactPartPool(key, factsofparts);
            return filter.GetIndexes(factsofparts);

        }

        
        public List<int[]> SearchFacts3(FactsPartsDictionary factsOfParts, int[] factkey)
        {
            var resultfacts = factsOfParts.SearchFactsIndexByKey(factkey, null);
            return resultfacts.Select(i => FactsManager.GetFactKey(i)).ToList();
        }
        public IList<int> SearchFactsGetIndex3(int[] factkey, FactsPartsDictionary factsOfParts, IList<int> facts, int PartNr)
        {
            var memberkeys = factkey.ToList();
            var memberfactspool = new List<IList<int>>();
            foreach (var memberkey in memberkeys)
            {
                if (factsOfParts.ContainsKey(memberkey))
                {
                    memberfactspool.Add(factsOfParts[memberkey]);
                }
            }
            var partcount = memberfactspool.Count;
            if (facts!=null)
            {
                memberfactspool.Add(facts);

            }
            memberfactspool = memberfactspool.OrderBy(i => i.Count).ToList();
            var result = memberfactspool.FirstOrDefault();
            //if ( facts!=null && (result==null || result.Count > facts.Count)) 
            //{
            //    result = facts;
            //}
            for (int i = 1; i < memberfactspool.Count; i++)
            {
                result = Utilities.Objects.IntersectSorted(result, memberfactspool[i], null);
            }
            if (PartNr != 0)
            {
                result = IntervalList.GetIntervals(result.Where(i => FactsManager.FactsOfPages.FactKeyCountOfIndexes[i] == PartNr));
            }
            if (result == null)
            {
                result = new List<int>();
            }
            return result;
        }

     
        public IList<int> SearchFactsGetIndex4(int[] factkey, FactsPartsDictionary factsOfParts, IList<int> facts, bool ensurefactnr=true,bool skip=false)
        {
            if (factkey.Length == 0) 
            {
                return facts;
            }
            var result = new IntervalList();// new List<int>();
            var domainkeys = factkey.Where(i => this.DimensionDomainsOfMembers.ContainsKey(i) && this.DimensionDomainsOfMembers[i] == i).ToList();
            var memberkeys = factkey.Except(domainkeys).ToList();
            var memberfactspool = new List<IList<int>>();
            var domainpool = new List<List<int>>();
            var qry = new FactBaseQuery();
            foreach (var memberkey in memberkeys) 
            {
                qry.DictFilterIndexes.Add(memberkey);
            }
            foreach (var domainkey in domainkeys)
            {
                var domainquery = new FactPoolQuery();
                qry.AddChildQuery(domainquery);

                var negativequery = new FactBaseQuery();
                domainquery.AddChildQuery(negativequery);
                negativequery.NegativeDictFilterIndexes.Add(domainkey);

                var positivequery = new FactBaseQuery();
                domainquery.AddChildQuery(positivequery);
                positivequery.DictFilterIndexes.Add(domainkey);
         
            }
            var resultquery = qry.EnumerateResults(factsOfParts, 0, facts, null,skip);//null null
            foreach (var qr in resultquery) 
            {
                IEnumerable<int> data = qr.Items;
                if (ensurefactnr) 
                {
                    data = data.Where(i => this.FactsManager.FactsOfPages.FactKeyCountOfIndexes[i] == qr.Query.DictFilterIndexes.Count);
                }
                result.AddRange(data);
            }
            //result = resultquery.ToList();
            return result;
        }

        public IList<int> SearchFactsGetIndexX(int[] factkey, FactsPartsDictionary factsOfParts, IList<int> facts)
        {
            if (factkey.Length == 0)
            {
                return facts;
            }
            var result = factsOfParts.ContainsKey(factkey[0]) ? factsOfParts[factkey[0]] : new IntervalList();
            for (int i = 1; i < factkey.Length; i++)
            {
                var keyresult = factsOfParts.ContainsKey(factkey[i]) ? factsOfParts[factkey[i]] : new IntervalList();
                result = Utilities.Objects.IntersectSorted(result, keyresult, null);
            }

            return result;
        }


        private List<Tuple<int,int>> GetIndexlength(IList<int> items)
        {
            var l = new List<Tuple<int, int>>();
            foreach (var item in items) 
            {
                var t = new Tuple<int, int>(this.FactsManager.GetFactKey(item).Length,this.FactsManager.FactsOfPages.FactKeyCountOfIndexes[item]);
                l.Add(t);
            }
            return l;
        }
        public IList<int> SearchFactsGetIndex3(int[] factkey, FactsPartsDictionary factsOfParts, IList<int> facts)
        {
            var result = new List<int>();
            var domainkeys = factkey.Where(i => this.DimensionDomainsOfMembers.ContainsKey(i) && this.DimensionDomainsOfMembers[i] == i).ToList();
            var memberkeys = factkey.Except(domainkeys).ToList();
            var memberfactspool = new List<IList<int>>();
            foreach (var memberkey in memberkeys)
            {
                if (factsOfParts.ContainsKey(memberkey))
                {
                    memberfactspool.Add(factsOfParts[memberkey]);
                }
                else 
                {
                    return new List<int>();
                }
            }
            var partcount = memberfactspool.Count;
            if (facts != null)
            {
                memberfactspool.Add(facts);
            }
            memberfactspool = memberfactspool.OrderBy(i => i.Count).ToList();
            var memberresults = memberfactspool.FirstOrDefault();

            for (int i = 1; i < memberfactspool.Count; i++)
            {
                memberresults = Utilities.Objects.IntersectSorted(memberresults, memberfactspool[i], null);
            }
            if (memberresults == null)
            {
                memberresults = new List<int>();
            }
            var memberresultsbycount = new Dictionary<int, IList<int>>();
            var lastkeylength = -1;
            IList<int> lastkeycontainer = null;
            var keylength = 0;

            foreach (var item in memberresults)
            {
                //key = FactsManager.GetFactKey(item);
                keylength = FactsManager.FactsOfPages.FactKeyCountOfIndexes[item];// key.Length;
                if (lastkeylength != keylength)
                {
                    if (!memberresultsbycount.ContainsKey(keylength))
                    {
                        var keycontainer = new IntervalList();
                        memberresultsbycount.Add(keylength, keycontainer);

                    }
                    lastkeylength = keylength;
                    lastkeycontainer = memberresultsbycount[keylength];
                }
                lastkeycontainer.Add(item);

            }

            //var memberresultscounts = memberresults.Select((i) => FactsManager.GetFactKey(i).Length).ToList();
            //setting up the items for the combination
            var vpool = new List<List<int>>();
            foreach (var domainkey in domainkeys)
            {
                vpool.Add(new List<int>() { -1, domainkey });
            }
            var combinations = MathX.CartesianProduct(vpool).ToList();
            if (combinations.Count == 0)
            {
                result = memberresults.ToList();
            }
            else
            {


                var actions = new List<Action>();
                var locker = new object();
                var cc = combinations.Count;
                var partialresults = new List<IEnumerable<int>>(cc);
                partialresults.AddRange(combinations.Select(i => new List<int>()));
                //getting the facts for each combination
                for (int ix = 0; ix < cc; ix++)
                {
                    var combination = combinations[ix];

                    actions.Add(() =>
                    {
                        var index = ix;
                        var ccc = cc;
                        var factpartpool = new List<int>();
                        var factspool = new List<IList<int>>();
                        var i_domainkeys = combination.ToList();

                        factpartpool.AddRange(i_domainkeys.Where(i => i != -1));
                        foreach (var factpart in factpartpool)
                        {
                            if (factsOfParts.ContainsKey(factpart))
                            {
                                factspool.Add(factsOfParts[factpart]);
                            }
                        }
                        var pcount = partcount + factspool.Count;



                        //factspool.Add(memberresults.Where(i => FactsManager.GetFactKey(i).Length == pcount).ToList());
                        //factspool.Add(memberresults.Where((int i,int index )=> {
                        //    return memberresultscounts[index]==pcount;
                        //}).ToList());

                        factspool.Add(memberresultsbycount.ContainsKey(pcount) ? memberresultsbycount[pcount] : new List<int>());

                        factspool = factspool.OrderBy(i => i.Count()).ToList();

                        var partialresult = factspool.FirstOrDefault();

                        for (int i = 1; i < factspool.Count; i++)
                        {

                            partialresult = Utilities.Objects.IntersectSorted(partialresult, factspool[i], null);

                        }
                        if (memberresults.Count == 0)
                        {

                            partialresult = partialresult.Where(i => FactsManager.GetFactKey(i).Length == pcount).ToList();

                        }
                        lock (locker)
                        {
                            result.AddRange(partialresult);
                        }

                    });
                }

                //foreach (var a in actions) 
                //{
                //    a();
                //}
                Task.WaitAll(actions.Select(i => Task.Factory.StartNew(i)).ToArray());
                foreach (var partialresult in partialresults)
                {
                    result.AddRange(partialresult);
                }
            }
            return result;
        }
        public int GetMapID(String part)
        {
            if (!this.FactParts.ContainsKey(part))
            {
                var msg = String.Format("Can't find FactPart {0}", part);
                Utilities.Logger.WriteLine(msg);
            }
            return this.FactParts[part];
        }

        public void SetMapID(Dimension dimension)
        {
            dimension.MapID = GetMapID(dimension.DomainMemberFullName);
            dimension.DomMapID = GetMapID(dimension.DimensionDomain);
        }

        public virtual void FixNamespace(QualifiedName qname, string namespaceuri) 
        {
            
        }
        public virtual string FindNamespacePrefix(string namespaceuri,string namespaceprefix)
        {
            return "";
        }
        public void SetMapID(Concept concept)
        {
            concept.MapID = GetMapID(concept.Content);
        }
        
        public string GetFactIntStringKey(string key) 
        {
            var parts = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var part in parts) 
            {
                sb.Append(this.FactParts[part] + ",");
            }
            return sb.ToString();
        }
        //public string GetFactIntStringKey(string key)
        //{
        //    var parts = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        //    var sb = new StringBuilder();
        //    foreach (var part in parts)
        //    {
        //        sb.Append(this.FactParts[part] + ",");
        //    }
        //    return sb.ToString();
        //}
        private static char[] splitter = new char[] { ',' };
        public List<int> GetFactIntKey(string key)
        {
            var intlist = new List<int>();
            //var parts = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //var parts = Utilities.Strings.FactSplit(key, ',', 8);
            if (key.Contains(":"))
            {
                var parts = key.IndexOf(",") == -1 ? new List<string>() { key } : Utilities.Strings.FactSplit(key, ',', 8);

                foreach (var part in parts)
                {
                    if (this.FactParts.ContainsKey(part))
                    {
                        intlist.Add(this.FactParts[part]);
                    }
                    else 
                    {
                        intlist.Add(-1);

                    }

                }
            }
            else 
            {
                var parts = key.Split(Taxonomy.splitter, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in parts)
                {
                    intlist.Add(Utilities.Converters.FastParse(part));
                    //intlist.Add(int.Parse(part));

                }
            }
            return intlist;
        }

        public List<string> GetFactKeyStringParts(IEnumerable<int> key) 
        {
            var parts =new List<string>();
            foreach (var part in key)
            {
               parts.Add( this.CounterFactParts[part]);
            }
            return parts;
        }
        public string GetFactStringKeysFromIndexes(params int[] indexes)
        {
            var keys = indexes.Select(i => FactsManager.GetFactKey(i));
            return GetFactStringKeys(keys);
        }
        public string GetFactStringKeys(IEnumerable<int> indexes)
        {
            var keys = indexes.Select(i => FactsManager.GetFactKey(i));
            return GetFactStringKeys(keys);
        }
        public string GetFactStringKeys(IEnumerable<IEnumerable<int>> group)
        {
            var sb = new StringBuilder();
            foreach (var indexes in group)
            {
                var keys = indexes.Select(i => FactsManager.GetFactKey(i));
                sb.AppendLine(GetFactStringKeys(keys));
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public string GetFactStringKeys(IEnumerable<int[]> keys) 
        {
            var sb = new StringBuilder();
            foreach (var key in keys) 
            {
                sb.AppendLine(GetFactStringKey(key));
            }
            return sb.ToString();
        }
        public string GetFactStringKeys(IEnumerable<IEnumerable<int[]>> group)
        {
            var sb = new StringBuilder();
            foreach (var keys in group)
            {
                foreach (var key in keys)
                {
                    sb.AppendLine(GetFactStringKey(key));
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public string GetFactStringKeyFromIntKey(IEnumerable<int> key)
        {
            var sb = new StringBuilder();
            foreach (var part in key)
            {
                sb.Append(this.CounterFactParts[part] + ",");
            }
            return sb.ToString();
        }

        public string GetFactStringKeyFromIntKey(string key)
        {
            var parts = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var part in parts)
            {
                var intpart = int.Parse(part);
                sb.Append(this.CounterFactParts[intpart] + ",");
            }
            return sb.ToString();
        }
        public string GetFactStringKeyFromStringKey(string key)
        {
            return GetFactStringKeyFromStringKey(key, this.FactParts);
        }
        public string GetFactStringKeyFromStringKey(string key, Dictionary<string,int> dict)
        {
            //var parts = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var parts = Utilities.Strings.FactSplit(key, ',', 8);
            var sb = new StringBuilder();
            foreach (var part in parts)
            {
                if (dict.ContainsKey(part))
                {
                    sb.Append(dict[part]);
                    sb.Append(",");
                }
                else 
                {
                    sb.Append("-1,");

                    //Logger.WriteLine(String.Format("FactPart {0} was not found! Key: {1}", part, key));
                }
            }
            return sb.ToString();
        }

        public List<int> GetIDsFromStrings(List<string> parts) 
        {
            var idlist = new List<int>();
            foreach (var part in parts) 
            {
                idlist.Add(FactParts[part]);
            }
            return idlist;
        }

    

        public string GetFactStringKey(IEnumerable<int> idlist) 
        {
            var sb = new StringBuilder();

            foreach (var id in idlist) 
            {
                if (CounterFactParts.ContainsKey(id))
                {
                    sb.Append(CounterFactParts[id] + ",");
                }
                else 
                {
                    sb.Append(" ,");
                }
            }
            return sb.ToString();
        }


        public virtual void LoadLabels()
        {
            Logger.WriteLine("Load Labels");

            //Save
            if (!System.IO.File.Exists(TaxonomyLabelPath) || Settings.Current.ReloadFullTaxonomyButStructure)
            {

                LabelHandler.HandleTaxonomy(this);

                if (this.TaxonomyLabels.Count > 0)
                {
                    Lang = this.TaxonomyLabels.FirstOrDefault().Lang;
                }
                else 
                {
                    Logger.WriteLine("No Labels Found");

                }

                var jsoncontent = Utilities.Converters.ToJson(TaxonomyLabels);
                Utilities.FS.WriteAllText(TaxonomyLabelPath, jsoncontent);
            }
            else
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyLabelPath);
                this.TaxonomyLabels = Utilities.Converters.JsonTo<List<LogicalModel.Label>>(jsoncontent);
                LoadLabelDictionary();
            }
            Lang = this.TaxonomyLabels.FirstOrDefault().Lang;
            

            Logger.WriteLine("Load Labels Completed");

        }

        public virtual void LoadLabelDictionary() { }
      
        public virtual void LoadFactDictionary() { }

        public virtual void LoadFacts()
        {
            Logger.WriteLine("Load Facts");
            //refresh files
            ManageUIFiles();
            //

            //if (!Utilities.FS.FileExists(TaxonomyFactsPathFormat) || Settings.Current.ReloadFullTaxonomyButStructure)
            //{
            //    //if (this.Facts.Count == 0)
            //    //{
            //        //foreach (var table in Tables)
            //        //{
            //        //    table.LoadDefinitions2();
            //        //}
            //    //}
            //    LoadCells();

            //    //this.LoadFactDictionary();

            //    //var jsoncontent = Utilities.Converters.ToJson(Facts);
            //    //Utilities.FS.WriteAllText(TaxonomyFactsPath, jsoncontent);
                
            //    //LogicalModel.Helpers.FileManager.SaveToJson(this.Facts, this.TaxonomyFactsPathFormat, 50000);
                
                
                
            //    //SerializeFacts(50000);
            //}
            //else
            //{

            //    //var jsoncontent = System.IO.File.ReadAllText(TaxonomyFactsPath);
            //    //this.Facts = Utilities.Converters.JsonTo<Dictionary<string, List<string>>>(jsoncontent);
                
                
                
            //    //DeSerializeFacts();
                
                
                
            //    //this.Facts = new Dictionary<int[], List<string>>(2000000, new Utilities.IntArrayEqualityComparer());
            //    //LogicalModel.Helpers.FileManager.SetFromJson(this.Facts, this.TaxonomyFactsPathFormat, DeserialzieFacts);

      
            //}
            if (this.FactsManager.FactsOfPages.FactKeyCountOfIndexes.Count == 0)
            {
                this.LoadFactDictionary();
            }

            LoadCells();
            Logger.WriteLine("Load Facts completed");
        }
        
        public void DeSerializeFacts()
        {
            
            var pathformat = TaxonomyFactsPathFormat;
            var folder = Utilities.Strings.GetFolder(pathformat);
            var filename = Utilities.Strings.GetFileName(pathformat);
            var searchpattern = filename.Replace("{0}", "*");
            var files = System.IO.Directory.GetFiles(folder, searchpattern).ToList();
            var sp_pipe = new string[] { Literals.PipeSeparator };
            var sp_coma = new string[] { Literals.Coma };
            ClearFacts();
            Facts = new Dictionary<int[], List<string>>(50000 * files.Count, new Utilities.IntArrayEqualityComparer());
            //Facts = new Dictionary<int[], List<Int64>>(50000 * files.Count, new Utilities.IntArrayEqualityComparer());
            foreach (var file in files)
            {
                var content = Utilities.FS.ReadAllText(file);
                var items = content.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                content = null;
                string[] parts = null;
                string[] keys = null;
                string[] values = null;
                int[] intkeys = null;
                List<string> cells = null;
                foreach (var item in items) 
                {
                    parts = item.Split(sp_pipe, StringSplitOptions.None);
                    keys = parts[0].Split(sp_coma, StringSplitOptions.RemoveEmptyEntries);
                    intkeys = new int[keys.Length];
   
                    for (int i = 0; i < keys.Length;i++ )
                    {
                        intkeys[i] = Utilities.Converters.FastParse(keys[i]);
                    }
           
                    values = parts[1].Split(sp_coma, StringSplitOptions.RemoveEmptyEntries);

                    cells = new List<string>(values);
                    Facts.Add(intkeys, cells);
                }
                items = null;
            }
        }
        
        public void SerializeFacts(int pagesize) 
        {
            /*
            var ix = 1;
            var sb = new StringBuilder();
            int page = 0;
            Action<StringBuilder,int> a = (StringBuilder i,int p) => {
                Utilities.FS.WriteAllText(string.Format(TaxonomyFactsPathFormat, p), i.ToString());
                sb.Clear();
            };
            foreach (var x in this.FactsAsEnumerable()) 
            {
                foreach(var id in x.Key)
                {
                    sb.Append(id + ",");
                }
                sb.Append(" | ");
                foreach (var id in x.Value)
                {

                    sb.Append(id + ",");
                }
                sb.Append("\n");
                ix++;
                if (ix % pagesize == 0) 
                {
                    a(sb, page);
                    page++;
                }

            }
            a(sb, page);
            var dictionarypath = TaxonomyFactDictionaryPath;
            Utilities.FS.WriteAllText(dictionarypath, "");
            var sb_dict = new StringBuilder();
            foreach (var x in this.FactsAsEnumerable())
            {
                var fs = GetFactStringKeyFromIntKey(x.Key);
                var item = String.Format("{0} [{1}]\n", fs, "@cells");
                var cells = "";
                foreach (var cell in x.Value) 
                {
                    cells += cell + ", ";
                }
                cells = cells.TrimEnd(',', ' ');
                item = item.Replace("@cells", cells);
                sb_dict.Append(item);
            }
            Utilities.FS.WriteAllText(dictionarypath, sb_dict.ToString());

            Logger.WriteLine("Dictionary was created at " + dictionarypath);
             */
        }

        private void LoadCells()
        {
            Logger.WriteLine("Load Cells started");
            Cells.Clear();
            //foreach (var fact in Facts) 
            //{
            //    var cells = fact.Value;
            //    foreach (var cell in cells) 
            //    {
            //        if (!Cells.ContainsKey(cell))
            //        {
            //            Cells.Add(cell, new List<string>() { GetFactStringKeyFromIntKey(fact.Key) });
            //        }
            //    }
            //}
            Logger.WriteLine("Load Cells completed");

        }

        public virtual void LoadTables()
        {
            Logger.WriteLine("Load Tables");
            //refresh files
            ManageUIFiles();
            //
            var shouldload = !System.IO.File.Exists(TaxonomyModulePath) || Settings.Current.ReloadFullTaxonomyButStructure;
            if (!shouldload)
            {
                var jsoncontent = System.IO.File.ReadAllText(TaxonomyModulePath);
                this.Module = Utilities.Converters.JsonTo<TaxonomyModule>(jsoncontent);
                foreach (var tablepath in Module.TablePaths)
                {
                    var fulltablepath = Utilities.Strings.ResolveRelativePath(this.TaxonomyLayoutFolder, tablepath);
                    if (!Utilities.FS.FileExists(fulltablepath)) 
                    {
                        shouldload = true;
                    }

                }
            }
            if (shouldload)
            {
                this.Tables.Clear();

                PopulateTableGroups();
               
                LoadGeneral();

                TableHandler.HandleTaxonomy(this);

                var sbt = new StringBuilder();
                foreach (var t in this.Tables) 
                {
                    sbt.AppendLine(Utilities.FS.ReadAllText(t.MappingTextPath));
                }

                Utilities.FS.WriteAllText(this.TaxonomyLayoutFolder + "Mappings.txt",sbt.ToString());
                sbt.Clear();

                GC.Collect();

                Module.FactParts = this.FactParts;
                Module.DimensionDomainsOfMembers = this.DimensionDomainsOfMembers;

                var jsoncontent = Utilities.Converters.ToJson(Module);
                Utilities.FS.WriteAllText(TaxonomyModulePath, jsoncontent);
                jsoncontent = null;


       

                PopulateValidationSets();

                foreach (var Table in Tables)
                {
                    var tjson = Utilities.Converters.ToJson(Table);
                    Utilities.FS.WriteAllText(Table.JsonPath, tjson);

                }
                ClearTablesAfterLoad();

                FactsManager.SaveAll();

                FactsOfParts.Save();

                //Utilities.FS.WriteAllText(TaxonomyCellIndexPath, Utilities.Converters.ToJson(this.CellIndexDictionary));

                Utilities.FS.DictionaryToFile(TaxonomyCellIndexPath, this.CellIndexDictionary);
            }
            else
            {
                var jsoncontent = Utilities.FS.ReadAllText(TaxonomyModulePath);
                this.Module = Utilities.Converters.JsonTo<TaxonomyModule>(jsoncontent);
                jsoncontent = null;
                this.Module.Taxonomy = this;
                this.Module.Load();

                Utilities.FS.DictionaryFromFile(TaxonomyCellIndexPath, this.CellIndexDictionary);
                
                //var jsoncellix = Utilities.FS.ReadAllText(TaxonomyCellIndexPath);
                //this.CellIndexDictionary = Utilities.Converters.JsonTo<Dictionary<int, string>>(jsoncellix);

                var jsoncontent2 = Utilities.Converters.ToJson(this.Module);
                if (jsoncontent != jsoncontent2) 
                {
                    Utilities.FS.WriteAllText(TaxonomyModulePath, jsoncontent2);
                }
                jsoncontent2 = null;
                this.Tables.Clear();
                foreach (var tablepath in Module.TablePaths)
                {
                    var fulltablepath = Utilities.Strings.ResolveRelativePath(this.TaxonomyLayoutFolder, tablepath);
                    var tjson = Utilities.FS.ReadAllText(fulltablepath);
                    var table = Utilities.Converters.JsonTo<Table>(tjson);
                    table.Taxonomy = this;
                    table.Reload();
                    //table.LoadLayout();

                    this.Tables.Add(table);
                }

                FactsOfParts.Load();
            }
            Logger.WriteLine("Load Tables completed");

        }
        
        
        public void ManageUIFiles() 
        {

         
        }

        public void TaxonomyToUI()
        {
     
   
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
                string pdbfilename = Utilities.Strings.GetFileName(Output).Replace(".dll", ".pdb");
                System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
                //Make sure we generate an EXE, not a DLL
                parameters.GenerateExecutable = false;
                parameters.OutputAssembly = this.TaxonomyValidationDotNetLibPath;
                //parameters.IncludeDebugInformation = true;
                parameters.TempFiles = new TempFileCollection(".", false);
                var logicalmodelassembly = typeof(LogicalModel.Base.FactBase).Assembly;
                var logicalreferences = logicalmodelassembly.GetReferencedAssemblies();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                parameters.ReferencedAssemblies.Add(logicalmodelassembly.Location);
                //parameters.CompilerOptions = " /pdb:" + pdbfilename;

                foreach (var assemblyname in logicalreferences)
                {
                    Assembly asm = assemblies.FirstOrDefault(i => i.GetName().Name == assemblyname.Name);
                    if (asm != null)
                    {
                        parameters.ReferencedAssemblies.Add(asm.Location);
                    }
                    else
                    {
                        Logger.WriteLine("Assembly " + assemblyname.Name + " was not found!");
                    }
                }

                CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, content);

                if (results.Errors.Count > 0)
                {
                    var sb_err = new StringBuilder();
                    foreach (CompilerError CompErr in results.Errors)
                    {
                        var error = "Line number " + CompErr.Line +
                                    ", Error Number: " + CompErr.ErrorNumber +
                                    ", '" + CompErr.ErrorText + ";" +
                                    Environment.NewLine + Environment.NewLine;
                        sb_err.Append(error);
                    }
                    Logger.WriteLine(sb_err.ToString());

                }
                else
                {
                    //Successful Compile
                    Logger.WriteLine(String.Format("File {0} was successfully compiled to {1}",
                        this.TaxonomyValidationCsPath, this.TaxonomyValidationDotNetLibPath));

                }

            }
        }
        public List<int> GetPartsOfDomains(int[] key, List<int> domains) 
        {
            Dictionary<int,int> result = domains.ToDictionary(i=>i,i=>i );
            foreach (var keypart in key) 
            {
                var domkey = GetDimensionDomainPart(keypart);
                if (domains.Contains(domkey))
                {
                    result[domkey]=keypart;
                }
            }
            return result.Select(i=>i.Value).ToList();
        }
        public virtual string GetDomainID(QualifiedName domain) 
        {
            return "";
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
                    var dlltempfolder =TaxonomyEngine.LocalFolder+@"Temp\";
                    Utilities.FS.EnsurePath(dlltempfolder);
                    var templibs = System.IO.Directory.GetFiles(dlltempfolder, "*.dll", System.IO.SearchOption.AllDirectories);
                    var temppdbs = System.IO.Directory.GetFiles(dlltempfolder, "*.pdb", System.IO.SearchOption.AllDirectories);
                    var tempfiles = templibs.Concat(temppdbs);
                    foreach (var tempfile in tempfiles) 
                    {
                        Utilities.FS.DeleteFile(tempfile, false);
                    }
                    var guid = Guid.NewGuid();
                    var originalfolder = Utilities.Strings.GetFolder(this.TaxonomyValidationDotNetLibPath);
                    var templibpath = this.TaxonomyValidationDotNetLibPath.Replace(".dll", String.Format("{0}.dll", guid));
                    var pdbpath = this.TaxonomyValidationDotNetLibPath.Replace(".dll", ".pdb");
                    var temppdbpath = templibpath.Replace(".dll", ".pdb");
                    templibpath = templibpath.Replace(originalfolder, dlltempfolder);
                    temppdbpath = temppdbpath.Replace(originalfolder, dlltempfolder);

                    Utilities.FS.Copy(this.TaxonomyValidationDotNetLibPath, templibpath);
                    Utilities.FS.Copy(pdbpath, temppdbpath);
  

                    var assembly = Assembly.LoadFile(templibpath);
                    var type = assembly.GetTypes().FirstOrDefault(i => i.BaseType == typeof(ValidationFunctionContainer));
                    if (type != null)
                    {
                        ValidationFunctionContainer vfc = (ValidationFunctionContainer)Activator.CreateInstance(type);
                        this.ValidationFunctionContainer = vfc;
                        this.ValidationFunctionContainer.AddInstance(TaxonomyEngine.CurrentEngine.CurrentInstance);
                    }
                    else
                    {
                        Logger.WriteLine(String.Format("There is no {0} in assembly {1}", typeof(ValidationFunctionContainer).Name, this.TaxonomyValidationDotNetLibPath));
                    }
                }
            }
        }
        
        public virtual void LoadSchemaElements()
        {
            Logger.WriteLine("Load Elements");

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
                    if (!this.SchemaElementDictionary.ContainsKey(schemaelement.Key))
                    {
                        this.SchemaElementDictionary.Add(schemaelement.Key, schemaelement);
                    }
                }

            }
            LoadTypedDimensions();
            LoadDomainMembers();
            Logger.WriteLine("Load Elements completed");

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
            Logger.WriteLine("Load Concepts");

            if (!System.IO.File.Exists(TaxonomyConceptPath) || Settings.Current.ReloadFullTaxonomyButStructure)
            {

                var conceptelements = SchemaElements.Where(i => i.FileName.EndsWith("met.xsd")).ToList();
                foreach (var conceptelement in conceptelements) 
                {
                    var concept = new Concept();
                    if (!String.IsNullOrEmpty(conceptelement.Domain))
                    {
                        concept.Domain = new QualifiedName();
                        //GetDomainID(conceptelement);
                        //if (conceptelement.Hierarchy)
                        concept.Domain.Content = conceptelement.Domain;
                        concept.HierarchyRole = String.IsNullOrEmpty(conceptelement.Hierarchy) ? conceptelement.LinkRole : conceptelement.Hierarchy;
                    }
                    concept.Name = conceptelement.Name;
                    concept.Namespace = conceptelement.Namespace;
                    concept.NamespaceURI = conceptelement.NamespaceURI;
                    concept.ItemType = conceptelement.Type.IndexOf(":") > -1 ? conceptelement.Type.Substring(conceptelement.Type.IndexOf(":") + 1) : conceptelement.Type;
                    if (!this.Concepts.ContainsKey(concept.Content))
                    {
                        this.Concepts.Add(concept.Content, concept);
                    }
                }


                var jsoncontent = Utilities.Converters.ToJson(this.Concepts);
                Utilities.FS.WriteAllText(TaxonomyConceptPath, jsoncontent);
                Utilities.FS.WriteAllText(TaxonomyLayoutFolder+"concepts.js", "var concepts = " + jsoncontent+ ";");
            }
            else
            {

                var jsoncontent = System.IO.File.ReadAllText(TaxonomyConceptPath);
                this.Concepts = Utilities.Converters.JsonTo<Dictionary<string,Concept>>(jsoncontent);

            }
            Logger.WriteLine("Load Concepts completed");
        }

        public virtual void PopulateTableGroups() 
        {

        }
        public virtual void PopulateValidationSets()
        {

        }
        public virtual void LoadGeneral() 
        {

        }

        public virtual void LoadInstance(string filepath)
        {

        }

        public virtual void AfterRefrencesLoadedFromCache()
        {

        }
        public virtual void ClearTablesAfterLoad() 
        {

        }
    
        public virtual void ClearCache()
        {

        }

        public virtual void ClearValidationObjects() 
        {
            foreach (var rule in this.ValidationRules)
            {
                rule.ClearObjects();
            }
        }

        public virtual void Clear_Dimensions() 
        {
            this.FactParts.Clear();
            this.CounterFactParts.Clear();
            this.DimensionItems.Clear();
            //this.FactsOfDimensions.Clear();

            Utilities.FS.DeleteFile(this.TaxonomyDimensionPath);
        }

        public virtual Instance GetNewInstance()
        {
            return new Instance();
        }

        public List<String> GetAllDimensions() 
        {
            var dimensions = new Dictionary<string,string>();
            var factkeys = this.FactKeysAsEnumerable().Select(i => i).ToList();
            foreach (var factkey in factkeys) 
            {
                var fact = new FactBase();
                var str_factkey = GetFactStringKey(factkey);
                fact.SetFromString(str_factkey);
                foreach (var dimension in fact.Dimensions) 
                {
                    if (!dimensions.ContainsKey(dimension.DimensionItem)) 
                    {
                        var dim_text = GetLabelForDimensionItem(dimension.DimensionItem);
                        var dom_text = GetLabelForDomain(dimension.Domain);
                        var mem_text = GetLabelForMember(dimension.DomainAndMember);
                        dimensions.Add(dimension.DimensionItem, String.Format("{0} = {1}", dimension.DimensionItem, dim_text));
                    }
                }
            }
            return dimensions.Select(i => i.Value).ToList();
        }

        public Label GetLabelForDimension(Dimension dimension) 
        {
            var l = new Label();
            var sb = new StringBuilder();
            sb.Append(String.Format("{0}: {1} | ", dimension.DimensionItem, GetLabelForDimensionItem(dimension.DimensionItem)));
            sb.Append(String.Format("{0}: {1} | ", dimension.Domain, GetLabelForDomain(dimension.Domain)));
            sb.Append(String.Format("{0}: {1}", dimension.DomainAndMember, GetLabelForMember(dimension.DomainAndMember)));

            l.Content = sb.ToString();
            return l;
        }
        public Label GetLabelForDimensionDomainMember(Dimension dimension)
        {
            Label l = new Label();
            if (dimension.IsTyped || String.IsNullOrEmpty(dimension.DomainMember))
            {
                l = GetLabelForDomain(dimension.Domain);
            }
            else
            {
                l = GetLabelForMember(dimension.DomainAndMember);

            }
            return l;
        }
        public Label GetLabelForDimensionItem(string dimensionitem)
        {
            var dimparts = dimensionitem.Split(":");

            var ns = dimparts[0];
            var name = dimparts[1];
            var element = this.SchemaElements.FirstOrDefault(i => i.Namespace == ns && i.Name == name);
            //if (element!=null)
            var labelkey = Label.GetKey("dim", element.ID);
            var label = this.FindLabel(labelkey);
            var content = label != null ? label : new Label();
            return content;
        }
        public virtual Element FindDimensionDomain(string dimensionitem) 
        {
            return null;
        }
        public List<String> GetMembersOf(string domain, List<string> exceptmembers) 
        {
            var dimensiondomainitems = SchemaElements.Where(i => i.Namespace == domain && i.Type == "nonnum:domainItemType").Select(i => i.Namespace + ":" + i.Name).Distinct().ToList();

            dimensiondomainitems = dimensiondomainitems.Except(exceptmembers).ToList();
            return dimensiondomainitems;
        }
        public List<LogicalModel.Dimensions.DimensionMember> GetMembersOf(string domain)
        {
            var result = new List<LogicalModel.Dimensions.DimensionMember>();
            var members = SchemaElements.Where(i => i.Namespace == domain && i.Type == "nonnum:domainItemType").ToList();//(i => i.Namespace + ":" + i.Name).Distinct().ToList();

            foreach (var member in members)
            {
                var qi = new LogicalModel.Dimensions.DimensionMember();
                qi.Content = member.Namespace + ":" + member.Name;
                qi.IsDefaultMember = member.IsDefaultMember;
                qi.Label = GetLabelForMember(qi.Content);
                result.Add(qi);
            }
            return result;
        }
        public List<QualifiedItem> GetMembersOfOld(string domain)
        {
            var result = new List<QualifiedItem>();
            var members = SchemaElements.Where(i => i.Namespace == domain && i.Type == "nonnum:domainItemType").ToList();//(i => i.Namespace + ":" + i.Name).Distinct().ToList();

            foreach (var member in members) 
            {
                var qi = new QualifiedItem();
                qi.Content = member.Namespace + ":" + member.Name;
                qi.Label = Label.GetSimpleLabel(GetLabelForMember(qi.Content));
                result.Add(qi);
            }
            return result;
        }
        //public string GetDomainOfDimension(string item)
        //{
        //    if (item.Contains(":"))
        //    {
        //           var parts = item.Split(":");

        //        var ns = parts[0];
        //        var name = parts[1];
        //        var staff = this.FactsOfDimensions.Where(i => i.Key.StartsWith(":" + name)).Select(i=>i.Key).ToList();
        //        var domain = Utilities.Strings.TextBetween(staff.FirstOrDefault(), "]", ":");
        //        return domain;
        //    }
        //    return "";
        //}
        public Element GetDomain(string item)
        {
            Element element = null;
            if (item.Contains(Literals.QNameSeparator))
            {
                var parts = item.Split(Literals.QNameSeparator);

                var ns = parts[0];
                var name = parts[1];
                element = this.SchemaElements.FirstOrDefault(i => i.Namespace == ns && i.Name == name);
            }
            else
            {
                var domains = this.SchemaElements.Where(i => i.Type == "model:explicitDomainType");
                element = domains.FirstOrDefault(i => i.ID == item);
                if (element == null)
                {
                }
            }
            return element;
        }

        public Label GetLabelForDomain(string domain)
        {
            var labelkey = Label.GetKey("dom", domain);
            var label = this.FindLabel(labelkey);
            if (label == null)
            {
                var element = GetDomain(domain);
                if (element != null) 
                {
                    labelkey = Label.GetKey("dom", element.ID);
                    label = this.FindLabel(labelkey);
                }
            }
            var content = label != null ? label : new Label();
            return content;
        }

        public Label GetLabelForAspect(int aspectid) 
        {
            Label result=null;
            var str = this.CounterFactParts[aspectid];
            if (this.Concepts.ContainsKey(str))
            {

            }
            else 
            {
                var domainid = DimensionDomainsOfMembers[aspectid];
                if (domainid == aspectid)
                {
                    result = GetLabelForDomain(str);
                }
                else 
                {
                    result = GetLabelForMember(str);
                }
            }
            return result;

        }

        public Label GetLabelForMember(string domainandmember)
        {
            var dimparts = domainandmember.Split(new string[] { ":" }, StringSplitOptions.None);
            if (dimparts.Length > 2) { return new Label(); }
            var domain = dimparts[0];
            var member = dimparts[1];
            if (String.IsNullOrEmpty(member)) 
            {
                member = Literals.DefaultMember;
                //return new Label();
            }
            var domainelement = GetDomain(domain);
            if (domainelement == null)
            {
                var realdomainname = this.DomainAliases.ContainsKey(domain) ? this.DomainAliases[domain] : null;
                if (realdomainname != null) 
                {
                    domainelement = GetDomain(realdomainname);
                }
            }
            var memberelement = this.SchemaElements.FirstOrDefault(i => i.Name == member && i.Namespace == domain);

            var labelkey = Label.GetKey(domainelement.Name, memberelement.ID);
            var label = this.FindLabel(labelkey);
            var content = label != null ? label : new Label();
            return content;
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
            Clear_Dimensions();
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
            //Utilities.FS.DeleteFile(TaxonomyFactsPath);
            var folder = Utilities.Strings.GetFolder(TaxonomyFactsPathFormat);
            var files = System.IO.Directory.GetFiles(folder);
            foreach (var file in files)
            {
                Utilities.FS.DeleteFile(file);
            }
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
            Clear_Layout();
            Clear_Facts();
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
            this.ValidationFunctionContainer = null;
            GC.Collect();
            var validationFolder = Utilities.Strings.GetFolder(TaxonomySimpleValidationPath);
            var valfiles = System.IO.Directory.GetFiles(validationFolder);
            foreach (var valfile in valfiles) 
            {
                Utilities.FS.DeleteFile(valfile);
            }
        }
        
        #endregion

        public bool KeyContains(int[] p, string fs)
        {
            var factstring = GetFactStringKey(p);
            return factstring.IndexOf(fs,StringComparison.OrdinalIgnoreCase)>=0;
        }


        public bool IsDefaultMember(Dimensions.DimensionMember i)
        {
            return IsDefaultMember(i.Namespace, i.Name);
        }
        public bool IsDefaultMember(string ns,string name)
        {
            var key = String.Format("{0}:{1}", ns, name);
            if (this.DomainMembers.ContainsKey(key)) 
            {
                var se = this.DomainMembers[key];
                var firstmember = se.FirstOrDefault();
                //if (firstmember != null)
                //{
                    return firstmember.IsDefaultMember;
                //}
            }
            return false;
        }
    }
}
