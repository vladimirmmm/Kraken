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
        public static void WriteLine(string item) 
        {
            if (action != null)
            {
                action(item);
            }
        }

    }
}
