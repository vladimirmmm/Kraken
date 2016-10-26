using BaseModel;
using LogicalModel;
using LogicalModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{

    public class ContextContainer 
    {
        private Dictionary<String, Dimension> _Parts = new Dictionary<String, Dimension>();
        [JsonIgnore]
        public Dictionary<String, Dimension> Parts { get { return _Parts; } set { _Parts = value; } }

        private Dictionary<string, Entity> _Entitites = new Dictionary<string, Entity>();
        [JsonProperty]
        public Dictionary<string, Entity> Entitites { get { return _Entitites; } set { _Entitites = value; } }
       
        private Dictionary<string, Period> _Periods = new Dictionary<string, Period>();
        [JsonProperty]
        public Dictionary<string, Period> Periods { get { return _Periods; } set { _Periods = value; } }
        
        private Dictionary<string, InstanceContext> _Items = new Dictionary<string, InstanceContext>();
        [JsonProperty]
        public Dictionary<string, InstanceContext> Items { get { return _Items; } set { _Items = value; } }
 

        public void Clear()
        {
            this.Items.Clear();
        }
    }
}
