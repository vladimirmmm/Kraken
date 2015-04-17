using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model.DefinitionModel.Formula
{

    public class QName
    {
        private string _Content= "";
        public string Content { get { return _Content; } set { _Content = value; } }
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
