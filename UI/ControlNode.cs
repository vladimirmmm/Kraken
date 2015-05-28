using BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    //public interface IH<T> 
    //{
    //    public List<T> Children { get; set; }
    //    public T Parent { get; set; }
    //    public List<T> Where(Func<T, bool> func);

    //}




    public class ControlNode : Hier<ControlNode> 
    {
        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }

        private string _Title = "";
        public string Title { get { return _Title; } set { _Title = value; } }

        private ControlTarget _ControlTarget = ControlTarget.NotSpecified;
        public ControlTarget ControlTarget { get { return _ControlTarget; } set { _ControlTarget = value; } }
        
        public static Action<Object> DefaultAction = null;
        public static Func<Object> ContextAccessor = null;
        public Action<object> Action {get;set;}
        
        public ControlNode(string id, string title, Action<object> action)
        {
            this.ID = id;
            this.Title = title;
            this.Action = action;
        }
        
        public ControlNode(string id, string title, Action<object> action,params ControlNode[] children) 
        {
            this.ID = id;
            this.Title = title;
            this.Action = action;
            foreach (var child in children) 
            {
                child.Parent = this;
                this.Children.Add(child);
            }
        }

        public void Execute()
        {
            var context = ContextAccessor != null ? ContextAccessor() : this;
            if (Action != null) { Action(context); }
            if (DefaultAction != null) { DefaultAction(context); }
        }
        public void ExecuteAsync()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s, e) => { Execute(); };
            //worker.ProgressChanged += worker_ProgressChanged;
            //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync(0);
        }

        private void test() 
        {

        }


    }

    public enum ControlTarget 
    {
        NotSpecified,
        Instance,
        Taxonomy,
        XML,
        Table,
        Html,
        Extension,
        Fact,
        Hypercube,
        Cells,
        Labels,

    }

    #region Test
    public class Test
    {
        public void test()
        {
            var n = new Cn("a");
            n.AddNode("a1").AddNode("a11");
            n.AddNode("a2");
            var f = n.Where(i => i.name == "a11").FirstOrDefault();
            int z = 0;
        }
    }

    public class Cn : Hier<Cn>
    {
        public string name = "";
        public Cn()
        {

        }
        public Cn(string name)
        {
            this.name = name;
        }
        public Cn AddNode(string name)
        {
            var n = new Cn(name);
            n.Parent = this;
            this.Children.Add(n);
            return n;
        }
        public void test()
        {

        }
    }
#endregion
}
