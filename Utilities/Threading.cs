using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Threading
    {
        public static void ExecuteAsync(Action action) 
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s, e) => { action(); };
            worker.RunWorkerAsync();
        }
    }
}
