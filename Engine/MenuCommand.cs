using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class MenuCommand
    {
        public static Action<Object> DefaultAction = null;
        public static Func<object[]> DefaultContextAccessor = () => { return new object[] { }; };
        public string ID = "";
        public string DisplayName = "";
        public MenuCommand Parent = null;
        public List<MenuCommand> Children = new List<MenuCommand>();
        public Action<object[]> Action = null;
        public Func<object[]> ContextAccessor = null;
        public Boolean Enabled = true;
        public MenuCommand(string id, string displayname, params MenuCommand[] commands)
        {
            this.DisplayName = String.IsNullOrEmpty(displayname) ? id : displayname;
            this.ID = id;
            //this.ID = Guid.NewGuid().ToString();
            this.Children = commands.ToList();
        }

        public MenuCommand(string id, string displayname, Action<object[]> action, Func<object[]> ContextAccessor) 
        {
            this.Action = action;
            this.ContextAccessor = ContextAccessor;
            this.DisplayName = String.IsNullOrEmpty(displayname)? id : displayname;
            //this.ID = Guid.NewGuid().ToString();
            this.ID = id;
        }

        public void AddSubCommand(MenuCommand command) 
        {
            command.Parent = this;
            this.Children.Add(command);
        }

        public void Execute() 
        {
            var context = ContextAccessor != null ? ContextAccessor() : DefaultContextAccessor();
            if (Action != null) { Action(context); }
            if (DefaultAction != null) { DefaultAction(context); }

            //try
            //{
            //    var context = ContextAccessor != null ? ContextAccessor() : this;
            //    if (Action != null) { Action(context); }
            //    if (DefaultAction != null) { DefaultAction(context); }
            //}
            //catch (Exception ex) 
            //{
            //    Utilities.Logger.WriteLine(ex);
            //}
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
