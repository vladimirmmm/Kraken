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

    public class Request 
    {
        public string url { get; set; }
        public Dictionary<string, string> parameters { get; set; }
        public string requestid { get; set; }
        public string contenttype { get; set; }

    }
}
