using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Cell
    {
        public Concept Concept { get; set; }
        private List<Dimension> _Dimensions = new List<Dimension>();
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }

        private string _Report = "";
        public string Report { get { return _Report; } set { _Report = value; } }

        //y
        private string _Row = "";
        public string Row { get { return _Row; } set { _Row = value; } }

        //x
        private string _Column = "";
        public string Column { get { return _Column; } set { _Column = value; } }

        //z
        private string _Extension = "";
        public string Extension { get { return _Extension; } set { _Extension = value; } }

        public Boolean IsBlocked { get; set; }

        private string _Formula = "";
        public string Formula { get { return _Formula; } set { _Formula = value; } }

        private string _FactString = "";
        public string FactString
        {
            get
            {
                if (string.IsNullOrEmpty(_FactString))
                {
                    _FactString = "";
                    //if (!String.IsNullOrEmpty(Concept))
                    if (Concept != null)
                    {
                        _FactString += Concept + ">";
                    }
                    foreach (var dimension in Dimensions)
                    {

                        _FactString += dimension.DomainMemberFullName+" ";

                    }
                }
                return _FactString;
            }
            set
            {
                _FactString = value;
                var s = _FactString;
                var cix = s.IndexOf(">");
                if (cix > -1)
                {
                    if (Concept == null) { Concept = new Concept(); }
                    Concept.Content = s.Remove(cix);
                    s = s.Substring(cix + 1);
                }
                //Dimensions = s.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        public string CellID 
        {
            get
            {
                return string.Format("{0}.{1}<{2}|{3}>", Report, Extension, Row, Column);
            }
        }

        public string FactKey 
        {
            get {
                var sb = new StringBuilder();
                sb.AppendFormat("{0},", this.Concept);
                foreach (var dim in Dimensions)
                {
                    sb.AppendFormat("{0},", dim.DomainMemberFullName);
                }
                return sb.ToString();
            }
        }
        public override string ToString()
        {
            return String.Format("{0} - R{1}|C{2}",Report, Row, Column);
        }
        
    }
}
