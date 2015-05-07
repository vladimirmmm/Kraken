using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class LayoutItem 
    {
        private Table _Table = null;
        public int Order = 0;

        private string _Axis = "";
        public string Axis { get { return _Axis; } set { _Axis = value; } }

        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }
    
        private string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }

        private bool _IsAbstract = false;
        public bool IsAbstract { get { return _IsAbstract; } set { _IsAbstract = value; } }

        private bool _IsAspect = false;
        public bool IsAspect { get { return _IsAspect; } set { _IsAspect = value; } }

        public Label Label = null;

        private String _LabelContent = "";
        public String LabelContent 
        {
            get 
            {
                if (string.IsNullOrEmpty(_LabelContent))
                {
                    if (Label != null)
                    {
                        _LabelContent = Label.Content;
                    }
                }
                return _LabelContent;
            }
            set 
            {
                _LabelContent = value;
            }
        }
        
        public String LabelCode
        {
            get
            {
                var strvalue = "";
                if (Label != null)
                {
                    strvalue = Label.Code;
                }
                return strvalue;
            }
        }

        private List<Dimension> _Dimensions = new List<Dimension>();
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }
        
        public String DimensionString 
        {
            get 
            {
                var sb = new StringBuilder();
                for (int i = 0; i < Dimensions.Count;i++ )
                {
                    sb.Append(Dimensions[i]);
                    if (i < Dimensions.Count - 1) 
                    {
                        sb.Append("|");
                    }
                }
                return sb.ToString();
            }
        }

        public String _Concept = "";
        public Concept Concept { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}", ID, Label == null ? LabelID : LabelContent);
        }

        private string _Factidentifier = "";
        public string Factidentifier 
        {
            get 
            {
                if (string.IsNullOrEmpty(_Factidentifier))
                {
                    //if (!String.IsNullOrEmpty(Concept))
                    if (Concept!=null)
                    {
                        _Factidentifier = String.Format("Concept<{0}>", Concept);
                    }
                    if (!string.IsNullOrEmpty(DimensionString))
                    {
                        _Factidentifier = _Factidentifier + String.Format(" Dimensions<{0}>", DimensionString);

                    }
                }
                return _Factidentifier;
            }
            set { _Factidentifier = value; }
        }

        private string _FactString = "";
        public string FactString
        {
            get
            {
                if (string.IsNullOrEmpty(_FactString))
                {
                    _FactString = "";
                    //if (!String.IsNullOrEmpty(Concept))
                    if (Concept!=null)
                    {
                        _FactString += Concept + ">";
                    }
                    if (!string.IsNullOrEmpty(DimensionString))
                    {
                        _FactString += DimensionString;

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
        
        [JsonIgnore]
        public Table Table 
        {
            get { return _Table; }
            set { _Table = value; }
        }


        public LayoutItem()
        {
        }



        public void LoadLabel(Taxonomy Taxonomy)
        {
            var folder = _Table.FolderName;
            var key = Label.GetKey(folder, this.LabelID);
            this.Label = Taxonomy.FindLabel(key);
            //this.Label = Taxonomy.TaxonomyLabels.FirstOrDefault(i => i.LocalID == this.LabelID);

        }
    }
}
