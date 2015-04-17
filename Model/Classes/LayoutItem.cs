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
        public int Order = 0;

        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }
    
        private string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }

        //public LayoutItem Parent = null;
        //public List<LayoutItem> Children = new List<LayoutItem>();
    
        public Label Label = null;
        
        public String LabelContent 
        {
            get 
            {
                var strvalue = "";
                if (Label != null) 
                {
                    strvalue = Label.Content;
                }
                return strvalue;
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
        
        public List<String> Dimensions = new List<String>();
        
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
        
        public String Concept = "";

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
                    if (!String.IsNullOrEmpty(Concept))
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

        public LayoutItem()
        {
        }

        
    }
}
