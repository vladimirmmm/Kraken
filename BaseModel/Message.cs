using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class Message
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Category { get; set; }
        public string ContentType { get; set; }
        public string Error { get; set; }
        public string Data { get; set; }

        public string GetParameter(string name)
        {
            var result = "";
            if (Parameters.ContainsKey(name))
            {
                return Parameters[name];
            }
            return result;
        }
        public override string ToString()
        {
            return String.Format("Message{{ Url: {0}}}", Url);
        }
    }
}
