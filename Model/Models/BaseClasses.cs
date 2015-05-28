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

        public Concept Concept { get; set; }

        private List<Dimension> _Dimensions = new List<Dimension>();
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }

        public string GetFactString() 
        {
            var sb = new StringBuilder();
            if (Concept != null)
            {
                sb.Append(Concept + ">");
            }
            foreach (var dimension in Dimensions)
            {
                sb.Append(dimension.DomainMemberFullName + "|");
            }
            return sb.ToString();
        }

        public void SetFromString(string item) 
        {
            var cix = item.IndexOf(">");
            if (cix > -1)
            {
                var concept = new Concept();
                concept.Content = item.Remove(cix);
                item = item.Substring(cix + 1);
                this.Concept = concept;

            }
            var dimparts = item.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var dimpart in dimparts)
            {
                var dimitem = Utilities.Strings.TextBetween(dimpart, "[", "]");
                var domainpart = dimpart.Substring(dimitem.Length + 2);
                var domain = domainpart;
                var member = "";
                if (domainpart.Contains(":"))
                {
                    var domainparts = domainpart.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    domain = domainparts[0];
                    member = domainparts[1];
                }
                var dim = new Dimension();
                dim.DimensionItem = dimitem;
                dim.Domain = domain;
                dim.DomainMember = member;
                this.Dimensions.Add(dim);
            }
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


        public DateTime FromDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string Key { get { return String.Format("{0}:{1}",this.Namespace,this.ID); } }

    }

    public class QualifiedName:Identifiable
    {
        private string _Content = "";
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
                    ID = _Content.Substring(six + 1);
                }
            }
        }

        private string _Namespace = "";
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
    }
}
