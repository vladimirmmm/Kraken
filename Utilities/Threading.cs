using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static List<TOut> ExecuteParalell<TIn, TOut>(IEnumerable<TIn> items, Func<TIn, TOut> func) 
        {
            var results = new List<TOut>();
            results.Capacity = items.Count();
            var tasks = new List<Task>();
            var actions = new List<Action>();
            foreach (var item in items) 
            {
                actions.Add(() =>
                {
                    results.Add(func(item));
                });
                //var task = Task.Factory.StartNew(() =>
                //{
                //    results.Add(func(item));
                //});
                //tasks.Add(task);
                //Thread newThread = new Thread(() => {
                //    results.Add(func(item));
                //});
                //newThread.Start();
            }
            //System.Threading.Tasks.ParallelOptions = new 
            System.Threading.Tasks.Parallel.Invoke(new ParallelOptions() { MaxDegreeOfParallelism = 20, }, actions.ToArray());
            //Task.WaitAll(tasks.ToArray());
            return results;
        }
    }
}
