using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Classes.Table
{
    public class Cell
    {
        public Concept Concept { get; set; }
        public List<Dimension> Dimensions { get; set; }

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
    }
}
