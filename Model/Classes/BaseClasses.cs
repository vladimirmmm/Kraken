﻿using LogicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Base
{
    
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

    public class Locator : Link
    {
        private string _ID = "";
        public string ID
        {
            get
            {
                if (String.IsNullOrEmpty(_ID) && !String.IsNullOrEmpty(Href))
                {
                    var ix = Href.IndexOf("#");
                    if (ix > -1)
                    {
                        _ID = Href.Substring(ix + 1);
                    }
                }
                return _ID;
            }
            set { _ID = value; }
        }


        private string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }

        private string _RoleType = "";
        public string RoleType { get { return _RoleType; } set { _RoleType = value; } }

        public override string ToString()
        {
            return String.Format("{0}", ID);
        }
    }
    /*
    public class Link
    {
        private string _Type = "";
        public string Type { get { return _Type; } set { _Type = value; } }
        
        private string _Role = "";
        public string Role { get { return _Role; } set { _Role = value; } }
        
        private string _Href = "";
        public string Href { get { return _Href; } set { _Href = value; } }
    }

    public class Arc : Link
    {
        private string _ArcRole = "";
        public string ArcRole { get { return _ArcRole; } set { _ArcRole = value; } }

        private string _From = "";
        public string From { get { return _From; } set { _From = value; } }

        private string _To = "";
        public string To { get { return _To; } set { _To = value; } }

        private int _Order = 0;
        public int Order { get { return _Order; } set { _Order = value; } }

    }

    public class Identifiable : Link
    {
        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }

        private string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }

        private string _Label = "";
        public string Label { get { return _Label; } set { _Label = value; } }

        public override string ToString()
        {
            return String.Format("{0} - ID: {1}", this.GetType().Name, ID);
        }
    }

    public class Locator : Link
    {
        private string _ID = "";
        public string ID
        {
            get
            {
                if (String.IsNullOrEmpty(_ID) && !String.IsNullOrEmpty(Href))
                {
                    var ix = Href.IndexOf("#");
                    if (ix > -1)
                    {
                        _ID = Href.Substring(ix + 1);
                    }
                }
                return _ID;
            }
            set { _ID = value; }
        }


        private string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }

        private string _RoleType = "";
        public string RoleType { get { return _RoleType; } set { _RoleType = value; } }

        public override string ToString()
        {
            return String.Format("{0}", ID);
        }
    }
     */
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

        private string _Domain = "";
        public string Domain { get { return _Domain; } set { _Domain = value; } }

        private string _Hierarchy = "";
        public string Hierarchy { get { return _Hierarchy; } set { _Hierarchy = value; } }

        public DateTime FromDate { get; set; }

        public DateTime CreationDate { get; set; }

    }
  
}
