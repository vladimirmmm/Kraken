using BaseModel;
using LogicalModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Cell:FactBase
    {
        private string _Report = "";
        public string Report { get { return _Report; } set { _Report = value; } }

        //z
        private string _Extension = "";
        public string Extension { get { return _Extension; } set { _Extension = value; } }

        //y
        private string _Row = "";
        [JsonProperty]
        public string Row { get { return _Row; } set { _Row = value; } }

        //x
        private string _Column = "";
        [JsonProperty]
        public string Column { get { return _Column; } set { _Column = value; } }

        public Hierarchy<LayoutItem> LayoutRow = null;
        public Hierarchy<LayoutItem> LayoutColumn = null;
     
        [JsonProperty]
        public Boolean IsBlocked { get; set; }

        private string _Formula = "";
        public string Formula { get { return _Formula; } set { _Formula = value; } }

        private string _FactString = "";
        [JsonProperty]
        public string FactString
        {
            get
            {
                if (string.IsNullOrEmpty(_FactString))
                {
                    _FactString = GetFactString();
               
                }
                return _FactString;
            }
            set
            {
                _FactString = value;
                SetFromString(_FactString);
            }
        }

        public string CellID 
        {
            get
            {
                return string.Format("{0}<{1}|{2}|{3}>", Report, Extension, Row, Column);
            }
        }

        public string LayoutID
        {
            get
            {
                return string.Format("R{0}|C{1}",Row, Column);
            }
        }

        public string FactKey 
        {
            get {
                var sb = new StringBuilder();
                sb.AppendFormat("{0},", this.Concept);
                var dimensions = Dimensions.OrderBy(i => i.DomainMemberFullName).ToList();
                foreach (var dim in dimensions)
                {
                    sb.AppendFormat("{0},", dim.DomainMemberFullName);
                }
                return sb.ToString();
            }
        }
        public Cell() 
        {

        }
        public void SetFromCellID(string CellID)
        {
            var reportpart = CellID.Remove(CellID.IndexOf("<"));
            var cellpart = Utilities.Strings.TextBetween(CellID, "<", ">");
            var cellparts = cellpart.Split(new string[] { "|" }, StringSplitOptions.None);
            if (cellparts.Length == 3) 
            {
                this.Report = reportpart;
                this.Extension = cellparts[0];
                this.Row = cellparts[1];
                this.Column = cellparts[2];
            }
        }
        public override string ToString()
        {
            return String.Format("{0} - R{1}|C{2}",Report, Row, Column);
        }
        
    }
}
