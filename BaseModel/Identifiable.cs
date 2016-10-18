using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class Identifiable
    {
        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }

    }
}
