using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Label
    {
        public string _LocalID = "";
        public string _LabelID = "";
        public string _Content = "";
        public string _Code = "";
        public string _Lang = "";
        public string _FileName = "";

        public string DisplayName 
        {
            get { return String.Format("{0} [{1}] {2}", _LabelID, _Code, _Content);  }
        }

        public string LocalID { get { return _LocalID; } set { _LocalID = value; } }
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }
        public string Content { get { return _Content; } set { _Content = value; } }
        public string Code { get { return _Code; } set { _Code = value; } }
        public string Lang { get { return _Lang; } set { _Lang = value; } }
        public string FileName { get { return _FileName; } set { _FileName = value; } }

        public override string ToString()
        {
            return String.Format("{0}:{1}", _LabelID, _Content);
        }
    }
}
