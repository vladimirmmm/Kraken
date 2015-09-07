using LogicalModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach (var dimension in Dimensions)
            {
                sb.Append(dimension.DomainMemberFullName.Trim() + ",");//|
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
            if (String.IsNullOrEmpty(_FactKey))
            {
                var sb = new StringBuilder();
                if (Concept != null)
                {
                    sb.Append(Concept + ",");
                }
                foreach (var dimension in Dimensions)
                {
                    sb.Append(dimension.ToStringForKey() + ",");
                }
                _FactKey = sb.ToString();
            }

            return _FactKey;

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
            foreach (var dimpart in dimparts)
            {
                var dimitem = Utilities.Strings.TextBetween(dimpart, "[", "]");
                var domainpart = dimpart.Substring(dimitem.Length + 2);
                var domain = domainpart;
                var member = "";
                var dim = new Dimension();

                if (domainpart.Contains(":"))
                {
                    var domainparts = domainpart.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (domainparts.Length == 2)
                    {
                        domain = domainparts[0];
                        member = domainparts[1];
                        if (domain == "eba_typ") 
                        {
                            dim.IsTyped = true;
                        }
                    }
                    if (domainparts.Length == 3)
                    {
                        domain = String.Format("{0}:{1}", domainparts[0], domainparts[1]);
                        member = domainparts[2];
                        dim.IsTyped = true;
                    }
                }
                dim.DimensionItem = dimitem;
                dim.Domain = domain;
                dim.DomainMember = member;
                this.Dimensions.Add(dim);
            }
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
      
        public override string ToString()
        {
            return GetFactString();
        }

     
    }
    [JsonObject(MemberSerialization.OptIn)]
    //public class FactGroup : FactBase 
    public class FactGroup : FactBase 
    {
        private List<FactBase> _Facts = new List<FactBase>();
        [JsonProperty]
        public List<FactBase> Facts { get { return _Facts; } set { _Facts = value; } }

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

    public class Identifiable
    {
        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }

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
        public string Domain { get { return _Domain; } set { _Domain = value; } }

        private string _Hierarchy = "";
        public string Hierarchy { get { return _Hierarchy; } set { _Hierarchy = value; } }


        private string _Namespace = "";
        public string Namespace { get { return _Namespace; } set { _Namespace = value; } }

        private string _TypedDomainRef = "";
        public string TypedDomainRef { get { return _TypedDomainRef; } set { _TypedDomainRef = value; } }


        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public DateTime? CreationDate { get; set; }

        public string Key { get { return String.Format("{0}:{1}",this.Namespace,this.ID); } }

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
