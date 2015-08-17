using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class MenuCommand
    {
        public static Action<Object> DefaultAction = null;
        public static Func<Object> ContextAccessor = null;
        public string ID = "";
        public string DisplayName = "";
        public MenuCommand Parent = null;
        public List<MenuCommand> Children = new List<MenuCommand>();
        public Action<object> Action = null;
        public Boolean Enabled = true;
        public MenuCommand(string id, string displayname, params MenuCommand[] commands)
        {
            this.DisplayName = String.IsNullOrEmpty(displayname) ? id : displayname;
            this.ID = id;
            this.Children = commands.ToList();
        }

        public MenuCommand(string id, string displayname, Action<object> action) 
        {
            this.Action = action;
            this.DisplayName = String.IsNullOrEmpty(displayname)? id : displayname;
            this.ID = id;
        }

        public void AddSubCommand(MenuCommand command) 
        {
            command.Parent = this;
            this.Children.Add(command);
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
            worker.RunWorkerAsync(10000);
        }
        private void X() 
        {
           
        }

        public List<MenuCommand> Where(Func<MenuCommand, bool> expr) 
        {
            var results = new List<MenuCommand>();
            if (expr(this))
            {
                results.Add(this);
            }
            else
            {
                foreach (var child in this.Children)
                {
                    results.AddRange(child.Where(expr));
                }
            }
            return results;

        }

    }
}
