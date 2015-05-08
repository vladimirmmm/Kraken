using System;
using System.Collections.Generic;
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
    public class Hier<T> where T:class 
    {
    
        //private List<H<T>> HChildren = new List<H<T>>();
        //public List<T> Children 
        //{
        //    get 
        //    {
        //        return HChildren.Select(i => i.Item).ToList();
        //    }
        //}
        public List<T> Children = new List<T>();

        public T Parent;
        private T Item 
        {
            get { return this as T; }
        }


        public List<T> Where(Func<T, bool> func)
        {
            var results = new List<T>();
            if (func(this.Item))
            {
                results.Add(this.Item);
            }
            else
            {
                foreach (var child in this.Children)
                {
                    results.AddRange((child as Hier<T>).Where(func));
                }
            }
            return results;
        }


    }

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

    public class ControlNode : Hier<ControlNode> 
    {
        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }

        private string _Title = "";
        public string Title { get { return _Title; } set { _Title = value; } }

        private ControlTarget _ControlTarget = ControlTarget.NotSpecified;
        public ControlTarget ControlTarget { get { return _ControlTarget; } set { _ControlTarget = value; } }

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
}
