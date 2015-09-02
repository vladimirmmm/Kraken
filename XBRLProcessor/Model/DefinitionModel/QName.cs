using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XBRLProcessor.Model.DefinitionModel
{

    public class QName
    {
        private string _Content= "";
        public string Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                var six = _Content.IndexOf(":");
                if (six>-1) 
                {
                    _Domain = _Content.Remove(six);
                    _Value = _Content.Substring(six+1);
                }
            }
        }

        private string _Domain = "";
        public string Domain { get { return _Domain; } set { _Domain = value; } }

        private string _Value = "";
        public string Value { get { return _Value; } set { _Value = value; } }

        public QName()
        {

        }
        public QName(string content) 
        {
            this.Content = content;
        }

        public override string ToString()
        {
            return String.Format("{0}", this.Content);
        }
    }
}
