using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Logger
    {
        public static Action<string> action = null;
        public static string LogPath = "";
        private static StringBuilder logbuilder = new StringBuilder();
        private static StreamWriter _logwriter = null;
        public static StreamWriter logwriter
        {
            get 
            {
                if (_logwriter == null) 
                {
                    _logwriter = new StreamWriter(LogPath, false, Encoding.UTF8);
                }
                return _logwriter;
            }
        }
  
        public static void WriteLine(Exception item)
        {
            var sb = new StringBuilder();

            sb.Append(String.Format("Error: {0}\r\n StackTrace:{1}\r\n", item.Message, item.StackTrace));
            if (item.InnerException != null) 
            {
                sb.Append(String.Format("Inner Error: {0}\r\n Inner StackTrace:{1}\r\n", item.InnerException.Message, item.InnerException.StackTrace));

            }
            WriteLine(sb.ToString());
            Flush();
        }
        public static void WriteLine(string item)
        {
            //item = Utilities.Strings.HtmlEncode(item);
            var text = String.Format("{0:yyyy-MM-dd hh:mm:ss} {1}\r\n", DateTime.Now, item);
            logbuilder.Append(text);
            if (logbuilder.Length > 100000) 
            {
                Flush();
            }
            if (action != null)
            {
                //if (logbuilder.Length > 0)
                //{
                //    text = logbuilder.ToString() + text;
                //    Flush();
                //}

                action(Utilities.Strings.HtmlEncode(text));

            }

      
        }

        public static void Flush() 
        {
            Utilities.FS.AppendAllText(LogPath, logbuilder.ToString());
            logbuilder.Clear();
        }
        public static void WriteToFile(string item)
        {
            var text = String.Format("{0:yyyy-MM-dd hh:mm:ss} {1}\r\n", DateTime.Now, item);
            var path = @"C:\Users\vladimir.balacescu\Desktop\log.txt";
            Utilities.FS.AppendAllText(path, text + "\r\n");
        }

    }
}
