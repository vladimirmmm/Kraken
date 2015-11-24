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
                System.Threading.ThreadPool.QueueUserWorkItem((ab) =>
               {
                   UIAction(value);
               });
            }

        }


    }

  
}
