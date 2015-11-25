using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Logger
    {
        public static Action<string> action = null;
        public static void WriteLine(Exception item)
        {
            var sb = new StringBuilder();
            sb.Append(String.Format("Error: {0}\r\n StackTrace:{1}\r\n", item.Message, item.StackTrace));
            if (item.InnerException != null) 
            {
                sb.Append(String.Format("Inner Error: {0}\r\n Inner StackTrace:{1}\r\n", item.InnerException.Message, item.InnerException.StackTrace));

            }
            WriteLine(sb.ToString());
        }
        public static void WriteLine(string item) 
        {

            if (action != null)
            {
                var text = String.Format("{0:yyyy-MM-dd hh:mm:ss} {1}", DateTime.Now, item);
                action(text);
            }
        }

    }
}
