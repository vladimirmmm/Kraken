using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{

    public class TableGroup :Identifiable, ILabeled
    {
        private List<string> _TableIDs = new List<string>(); 
        public List<string> TableIDs 
        {
            get { return _TableIDs; }
            set { _TableIDs = value; }
        }
        public List<Table> Tables = new List<Table>();
        public string FilingIndicator { get; set; }
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
                    return _Label.Content;
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
                    return _Label.Code;
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
    }

}
