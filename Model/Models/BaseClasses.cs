using BaseModel;
using LogicalModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel.Base
{
 
    public class QualifiedItem : QualifiedName, ILabeled 
    {
        private Label _Label = null;
        [JsonProperty]
        public Label Label
        {
            get
            {
                return _Label;
            }
            set
            {
                _Label = value;
            }
        }

        public string LabelContent
        {
            get 
            {
                if (_Label != null) 
                {
                    return Label.Content;
                }
                return "";
            }
        }

        public string LabelCode
        {
            get
            {
                if (_Label != null)
                {
                    return Label.Code;
                }
                return "";
            }
        }

        private string _LabelID = "";
        public string LabelID
        {
            get
            {
                return _LabelID;
            }
            set
            {
                _LabelID = value;
            }
        }

        private string _Role = "";
        [JsonProperty]
        public string Role
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
            }
        }
    }
    public class FactBaseQuery 
    {
        public string FalseFilters = "";
        public string TrueFilters = "";
        public string DictFilters = "";
        public Func<string, bool> Filter = (s) => true;
        public List<FactBaseQuery> ChildQueries = new List<FactBaseQuery>();

        public List<String> ToList(IQueryable<String> queryable) 
        {
            return queryable.Where(i => Filter(i)).ToList();
        }


        public List<string> GetDimensions()
        {
            var filterparts = new List<string>();
            if (ChildQueries.Count > 0)
            {
                var commonchildfilters = new List<string>();

                var isfilteradded = false;
                foreach (var childquery in ChildQueries)
                {
                    var dimensions = childquery.GetDimensions();
                    if (!isfilteradded)
                    {
                        isfilteradded = true;
                        commonchildfilters.AddRange(dimensions);
                    }
                    else
                    {
                        commonchildfilters = commonchildfilters.Intersect(dimensions).ToList();
                    }
                }
                filterparts.AddRange(commonchildfilters);
            }

            filterparts.AddRange(DictFilters.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
            

            //var conceptfilterparts = filterparts.Where(i => i.IndexOf("]") == -1).ToList();
            //var dimparts = filterparts.Where(i => i.IndexOf("]") > -1).ToList();
            return filterparts.ToList();

        }

        public IEnumerable<String> ToQueryable(IEnumerable<String> queryable)
        {
            var result = queryable.Where(i => Filter(i));
            if (ChildQueries.Count > 0)
            {
                var items = new List<string>();
                foreach (var childquery in ChildQueries) 
                {
                    items.AddRange(childquery.ToQueryable(result));
                }
                return items;
            }
            return result;
        }
        public IEnumerable<String> ToQueryable(IEnumerable<String> queryable, IEnumerable<int> idqueryable)
        {
            var queryablecount = queryable.Count();
            for (int i = 0; i < queryablecount; i++) 
            {
                var str = queryable.ElementAt(i);
                var id = idqueryable.ElementAt(i);
                if (Filter(str)) 
                { 

                }
            }
            var result = queryable.Where(i => Filter(i));
            if (ChildQueries.Count > 0)
            {
                var items = new List<string>();
                foreach (var childquery in ChildQueries)
                {
                    items.AddRange(childquery.ToQueryable(result));
                }
                return items;
            }
            return result;
        }
        public override string ToString()
        {
            return String.Format("{0} NOT: {1}", TrueFilters, FalseFilters);
        }
    }
    public class FactBase 
    {

        [JsonIgnore]
        public Concept Concept { get; set; }

        private List<Dimension> _Dimensions = new List<Dimension>();
        [JsonIgnore]
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }

        internal string _FactString = "";
        
        [JsonProperty]
        public virtual string FactString
        {
            get 
            {
                if (String.IsNullOrEmpty(_FactString))
                {
                    _FactString = GetFactString();
                }
                return _FactString;
            }
            set
            {
                _FactString = value;
                SetFromString(value);
            }
        }
        public static FactBase GetFactFrom(string factstring)
        {
            var fact = new FactBase();
            fact.FactString = factstring;
            return fact;
        }
        public static void MergeFact(FactBase target, FactBase source)
        {
            if (target.Concept == null) 
            {
                target.Concept = source.Concept;
            }
            Dimension.MergeDimensions(target.Dimensions, source.Dimensions);
        }
        
        public void SetFactString()
        {
            Dimensions = Dimensions.OrderBy(i => i.DomainMemberFullName).ToList();
            _FactString = "";
        }
        
        public string GetFactString() 
        {
            var sb = new StringBuilder();
            if (Concept != null)
            {
                sb.Append(Concept + ",");//>
            }
            var lastdimns = "";
            foreach (var dimension in Dimensions)
            {
                var dimstr = dimension.DomainMemberFullName.Trim();
                sb.Append(Format(dimstr, ref lastdimns) + ",");
            }
            return sb.ToString();
        }

        public void ClearFactKey()
        {
            _FactKey = "";
        }
        private string _FactKey = "";
        
        public string GetFactKey()
        {
            if (this.Dimensions.Count == 0 && this.Concept == null)
            {
                LoadObjects();
            }
            if (String.IsNullOrEmpty(_FactKey))
            {
                var sb = new StringBuilder();
                if (Concept != null)
                {
                    sb.Append(Concept + ",");
                }
                var lastdimns = "";
                foreach (var dimension in Dimensions)
                {
                    var dimstr = dimension.ToStringForKey().Trim();
                    sb.Append(Format(dimstr, ref lastdimns) + ",");
                }
                _FactKey = sb.ToString();
                //ClearObjects();
            }

            return _FactKey;

        }

        private void test() 
        {
            var fact = new LogicalModel.Base.FactBase();
            //fact.SetFromString("eba_met:mi235,[eba_dim:BAS]eba_BA:x9,[eba_dim:LEC]eba_typ:LE,[eba_dim:MCY]eba_MC:x27,[eba_dim:TRI]eba_TR:x6,");
            fact.SetFromString("eba_met:mi235,[eba_dim:BAS]eba_BA:x9,[*:LEC]eba_typ:LE:1234,[*:MCY]eba_MC:x27,[*:TRI]eba_TR:x6,");
            var k1 = fact.GetFactKey();
            var s1 = fact.GetFactString();
            fact.SetFromString("eba_met:mi235,[eba_dim:BAS]eba_BA:x9,[eba_dim:LEC]eba_typ:LE:1234,[eba_dim:MCY]eba_MC:x27,[eba_dim:TRI]eba_TR:x6,");
            var k2 = fact.GetFactKey();
            var s2 = fact.GetFactString();
        }

        private static string Format(string item, ref string lastdimns)
        {
            var dimns = item.TextBetween("[", ":");
            if (lastdimns != dimns)
            {
                lastdimns = dimns;
            }
            else
            {
                //item = item.Replace(dimns, "*");

            }
            return item;

        }

        public void SetFromString(string item) 
        {
            this.Dimensions.Clear();
            this.Concept = null;
            var parts = item.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var toskip = 0;
            if (parts.Length > 0) 
            {
                if (parts[0].IndexOf("[")==-1) 
                {
                    toskip = 1;
                    var concept = new Concept();
                    concept.Content = parts[0];
                    this.Concept = concept;
                }
            }
            var dimparts = parts.Skip(toskip).ToList();
            var lastdimns = "";
            foreach (var dimpart in dimparts)
            {
                var dimitem = Utilities.Strings.TextBetween(dimpart, "[", "]");
                var domainpart = dimpart.Substring(dimitem.Length + 2);

                var dimitemns = dimitem.Remove(dimitem.IndexOf(":"));
                if (dimitemns == "*")
                {
                    dimitem = dimitem.Replace("*", lastdimns);
                }
                else 
                {
                    lastdimns = dimitemns;
                }
                var domain = domainpart;
                var member = "";
                var dim = new Dimension();

                if (domainpart.Contains(":"))
                {
                    var domainparts = domainpart.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    switch (domainparts.Length){
                        case 2:
                            dim.IsTyped = Taxonomy.IsTyped(domain);
                            if (dim.IsTyped)
                            {
                                domain = domainpart;
                            }
                            else 
                            {
                                domain = domainparts[0];
                                member = domainparts[1];
                            }
                            break;
                        case 3:
                            domain = String.Format("{0}:{1}", domainparts[0], domainparts[1]);
                            member = domainparts[2];
                            dim.IsTyped = true;
                            break;
                        default:
                            break;
                    }
                    //if (domainparts.Length == 2)
                    //{
                    //    if (Taxonomy.IsTyped(domainpart))
                    //    {
                    //        domain = domainpart;

                    //    }
                    //    else
                    //    {
                    //        domain = domainparts[0];
                    //        member = domainparts[1];

                    //    }
                    //    dim.IsTyped = Taxonomy.IsTyped(domain);

                   
                    //}
                    //if (domainparts.Length == 3)
                    //{
                    //    domain = String.Format("{0}:{1}", domainparts[0], domainparts[1]);
                    //    member = domainparts[2];
                    //    dim.IsTyped = true;
                    //}
                }
                dim.DimensionItem = dimitem;
                dim.Domain = domain;
                dim.DomainMember = member;
                this.Dimensions.Add(dim);
            }
            this._FactString = item;
            this._FactKey = "";
        }

        public bool HasTypedDimension() 
        {
            foreach (var dimension in Dimensions) 
            {
                if (dimension.IsTyped) 
                {
                    return true;
                }
            }
            return false;
        }
        
        public void SetTyped() 
        {
            foreach (var dim in Dimensions) {
                dim.SetTyped();
            }
        }
        
        public override string ToString()
        {
            return GetFactString();
        }



        public void ClearObjects()
        {
            var fs = this.FactString;
            this.Concept = null;
            this.Dimensions.Clear();
        }

        public void LoadObjects() 
        {
            SetFromString(_FactString);
        }

    }
    [JsonObject(MemberSerialization.OptIn)]
    //public class FactGroup : FactBase 
    public class FactGroup : FactBase 
    {
        private List<FactBase> _Facts = new List<FactBase>();
        [JsonProperty]
        public List<FactBase> Facts { get { return _Facts; } set { _Facts = value; } }
        public List<FactBase> FullFacts {
            get { return GetFullFacts(); } 
           // set { _Facts = value; } 
        }
        public Func<FactBase, bool> Not = (f) => false;
        public void SetFacts(List<FactBase> facts) 
        {
            _Facts = facts;
        }

        public void AddFact(FactBase fact) 
        {
            _Facts.Add(fact);
        }

        protected List<FactBase> GetFullFacts() 
        {
            var newfacts = new List<FactBase>();
            if (this.Dimensions.Count == 0 && this.Concept == null)
            {
                LoadObjects();
            }
            foreach (var fact in _Facts)
            {
                if (fact.Dimensions.Count == 0 && fact.Concept == null)
                {
                    fact.LoadObjects();
                }
                var newfact = new FactBase();
                newfacts.Add(newfact);
                newfact.Dimensions.AddRange(fact.Dimensions);
                newfact.Concept=fact.Concept;

                Dimension.MergeDimensions(newfact.Dimensions, Dimensions);
                if (newfact.Concept == null)
                {
                    newfact.Concept = Concept;
                }
                newfact.Dimensions = newfact.Dimensions.OrderBy(i => i.DomainMemberFullName).ToList();
            }
            return newfacts;
        }


        [JsonIgnore]
        public override string FactString
        {
            get
            {
                if (String.IsNullOrEmpty(_FactString))
                {
                    _FactString = GetFactString();
                }
                return _FactString;
            }
            set
            {
                _FactString = value;
                SetFromString(value);
            }
        }

        public FactGroup Copy() 
        {
            var item = new FactGroup();
            item.Concept = this.Concept;
            item.Dimensions.AddRange(this.Dimensions);
            return item;
        }
    }



    public class Link
    {
        private string _Type = "";
        public string Type { get { return _Type; } set { _Type = value; } }

        private string _Href = "";
        public string Href { get { return _Href; } set { _Href = value; } }
    }

    public interface ILink
    {
        string Href { get; set; }
    }

    public interface ILabeled
    {
        Label Label { get; set; }
        string LabelContent { get; }
        string LabelCode { get; }
        string LabelID { get; set; }
    }

    public class IdentifiablewithLabel : Identifiable, ILabeled 
    {

        private Label _Label;
        public Label Label { get { return _Label; } set { _Label = value; } }


        public string LabelContent
        {
            get { return _Label != null ? _Label.Content : ""; }
        }

        public string LabelCode
        {
            get { return _Label != null ? _Label.Code : ""; }
        }

        public string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }
        public string ParentRole { get; set; }
        public string HRef { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1} - {2}",this.GetType().Name, this.ID,this.LabelID);
        }
    }
    
    public class Element
    {
        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }

        private string _Name = "";
        public string Name { get { return _Name; } set { _Name = value; } }

        private string _Type = "";
        public string Type { get { return _Type; } set { _Type = value; } }

        private string _SubstitutionGroup = "";
        public string SubstitutionGroup { get { return _SubstitutionGroup; } set { _SubstitutionGroup = value; } }

        private string _PeriodType = "";
        public string PeriodType { get { return _PeriodType; } set { _PeriodType = value; } }

        public Boolean Nullable { get; set; }

        private Boolean _IsDefaultMember = false;
        public Boolean IsDefaultMember { get { return _IsDefaultMember; } set { _IsDefaultMember = value; } }


        private string _Domain = "";
        [DefaultValue("")]
        public string Domain { get { return _Domain; } set { _Domain = value; } }

        private string _Hierarchy = "";
        [DefaultValue("")]
        public string Hierarchy { get { return _Hierarchy; } set { _Hierarchy = value; } }


        private string _Namespace = "";
        public string Namespace { get { return _Namespace; } set { _Namespace = value; } }

        private string _TypedDomainRef = "";
        public string TypedDomainRef { get { return _TypedDomainRef; } set { _TypedDomainRef = value; } }

        private string _LinkRole = "";
        [DefaultValue("")]
        public string LinkRole { get { return _LinkRole; } set { _LinkRole = value; } }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public DateTime? CreationDate { get; set; }

        public string Key { get { return String.Format("{0}:{1}",this.Namespace,this.ID); } }

        private string _FileName = "";
        public string FileName { get { return _FileName; } set { _FileName = value; } }


    }
    [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
    public class QualifiedName:Identifiable
    {
        private string _Content = "";
        [JsonProperty]
        public string Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                var six = _Content.IndexOf(":");
                if (six > -1)
                {
                    _Namespace = _Content.Remove(six);
                    _Name = _Content.Substring(six + 1);
                    ID = _Name;
                    SetFullName();

                }
            }
        }

        private string _Namespace = "";
        [JsonIgnore]
        public string Namespace
        {
            get { return _Namespace; }
            set
            {
                _Namespace = value;
                SetFullName();
            }
        }

        private string _Name = "";
        [JsonIgnore]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                SetFullName();
            }
        }

        private void SetFullName() 
        {
            _FullName = String.Format("{0}:{1}", _Namespace, _Name);
            _Content = _FullName;
        }
        private string _FullName = "";
        public virtual string FullName 
        {
            get 
            {
                return _FullName;
            }

        }

        public QualifiedName()
        {

        }
        public QualifiedName(string content)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            return String.Format("{0}", this.Content);
        }

        public void testc()
        {
            var files = System.IO.Directory.GetFiles(@"C:\My\Tasks\97532\20150606_19");
            foreach (var file in files) 
            {
                var content = System.IO.File.ReadAllText(file);
                System.IO.File.WriteAllText(file, content);
            }
        }
    }


}
