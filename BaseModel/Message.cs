using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BaseModel
{
    public class Message
    {
        public string Id { get; set; }
        public string Url { get; set; }
        private Dictionary<string, string> _Parameters =new Dictionary<string,string>();
        public Dictionary<string, string> Parameters { get { return _Parameters; } set { _Parameters = value; } }
        public string Category { get; set; }
        public string ContentType { get; set; }
        public string Error { get; set; }
        public string Data { get; set; }

        public string GetParameter(string name)
        {
            var result = "";
            if (Parameters!=null && Parameters.ContainsKey(name))
            {
                return String.Format("{0}", Parameters[name]);
            }
            return result;
        }
        public T GetParameter<T>(string name)
        {
            var stringval = GetParameter(name);
            T result = default(T);
            if (typeof(T)==typeof(bool))
            {
                var val = false;
                if (stringval.ToLower().In("true", "on", "1")) {
                    val = true;
                }
                return (T)(Object)val;
            }
            return result;
        }
        public override string ToString()
        {
            return String.Format("Message{{ Url: {0}}}", Url);
        }
    }
}
