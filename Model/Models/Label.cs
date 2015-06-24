using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class Label
    {
        private static string labelprefix = "";
        public string _LocalID = "";
        public string _LabelID = "";
        public string _Content = "";
        public string _Code = "";
        public string _Lang = "";
        public string _FileName = "";

        public static void SetLabelPrefix(string prefix) 
        {
            if (string.IsNullOrEmpty(labelprefix))
            {
                labelprefix = prefix;
            }
        }

        public string DisplayName 
        {
            get { return String.Format("{0} [{1}] {2}", _LabelID, _Code, _Content);  }
        }

        public string LocalID
        {
            get { return _LocalID; }
            set
            {
                _LocalID = value;
                _LabelID = _LocalID.StartsWith(labelprefix)? _LocalID: labelprefix + _LocalID;
            }
        }
        
        public string LabelID
        {
            get { return _LabelID; }
            set
            {
                _LabelID = value;
                _LocalID = _LabelID.StartsWith(labelprefix) ? _LabelID.Substring(labelprefix.Length) : _LabelID;

            }
        }
        public string Content { get { return _Content; } set { _Content = value; } }
        public string Code { get { return _Code; } set { _Code = value; } }
        public string Lang { get { return _Lang; } set { _Lang = value; } }
        public string FileName { get { return _FileName; } set { _FileName = value; } }

        public override string ToString()
        {
            return String.Format("{0}:{1}", _LabelID, _Content);
        }

        public string Key 
        {
            get
            {
                //return String.Format("{0}[{1}]{2}", _FileName, _Lang, _LabelID).ToLower();
                return String.Format("{0}[{1}]{2}", _FileName, _Lang, _LabelID).ToLower();
            }
        }
        public static string GetKeyByDomain(string domain, string localID)
        {
            var l = new Label();
            l.FileName = domain;
            l.Lang = "en";
            l.LocalID = localID;
            return l.Key;

        }
        public static string GetKeyByNamespace(string ns, string localID)
        {
            var l = new Label();
            l.FileName = ns;
            l.Lang = "en";
            l.LocalID = localID;
            return l.Key;

        }
        public static string GetKey(string fileid, string localID)
        {
            var l = new Label();
         
            l.FileName = fileid;
            l.Lang = "en";
            l.LocalID = localID;
            return l.Key;
        }
    }
}
