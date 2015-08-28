using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class JsClass
    {
        public Action<object> UIAction = null; 
        public void Notify(string value)
        {
            if (UIAction != null) 
            {
                UIAction(value);
            }

        }


    }

    public class Message 
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Category { get; set; }
        public string ContentType { get; set; }
        public string Error { get; set; }
        public string Data { get; set; }

    }
}
