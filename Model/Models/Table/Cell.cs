using BaseModel;
using LogicalModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private bool _IsKey = false;
        [JsonProperty]
        [DefaultValue(false)]
        public bool IsKey { get { return _IsKey; } set { _IsKey = value; } }

        private string _Role = "";
        [JsonProperty]
        [DefaultValue("")]
        public string Role { get { return _Role; } set { _Role = value; } }


        public Hierarchy<LayoutItem> LayoutRow = null;
        public Hierarchy<LayoutItem> LayoutColumn = null;
     
        [JsonProperty]
        public Boolean IsBlocked { get; set; }

        private string _Formula = "";
        public string Formula { get { return _Formula; } set { _Formula = value; } }

        private string _FactString = "";
        [JsonProperty]
        public new string FactString
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
                return this.GetFactKey();
            }
        }
        public int[] FactIntKey
        {
            get
            {
                int[] keys = new int[this.Dimensions.Count + 1];
                keys[0] = this.Concept == null ? -2 : this.Concept.MapID;
                for (int i = 0; i < this.Dimensions.Count;i++ )
                {
                    keys[i + 1] = this.Dimensions[i].MapID;
                }
                return keys;
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
