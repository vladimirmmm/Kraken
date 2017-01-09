using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBRLProcessor.Model.DefinitionModel.Filter
{
    public class TupleFilter : Filter
    {


    }

    public class ParentFilter : TupleFilter 
    {
        private List<QName> _Parents = new List<QName>();
        public List<QName> Parents { get { return _Parents; } set { _Parents = value; } }
    }
    public class AncestorFilter : TupleFilter
    {
        private List<QName> _Ancestors = new List<QName>();
        public List<QName> Ancestors { get { return _Ancestors; } set { _Ancestors = value; } }
    }
    public class SiblingFilter : TupleFilter
    {
        public QName _Variable = new QName();
        public QName Variable { get { return _Variable; } set { _Variable = value; } }
    }
    public class LocationFilter : TupleFilter
    {
        public QName _Variable = new QName();
        public QName Variable { get { return _Variable; } set { _Variable = value; } }

        public string _Location = "";
        public string Location { get { return _Location; } set { _Location = value; } }
    }
}
