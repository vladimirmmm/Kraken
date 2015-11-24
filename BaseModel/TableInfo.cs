using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class TableInfo : Identifiable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public List<string> Tables = new List<string>();

        public TableInfo()
        {

        }

    }
}
