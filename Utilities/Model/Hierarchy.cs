using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    //public interface ITree<T>
    //{
    //    public T Item { get; set; }
    //    public ITree<T> Parent { get; set; }
    //    public List<ITree<T>> Children { get; set; }
    //}
    public class testx
    {
        public void testdx()
        {
            var t = new Tree<String>("r");
            var l1=   new Tree<String>("l1");
            var l11=   new Tree<String>("l11");
            var l12=   new Tree<String>("l12");
            var l2=   new Tree<String>("l2");
            t.Add(l1);
            l1.Add(l11);
            l1.Add(l12);
            t.Add(l2);
            var z = t.ToList();
        }
    }

    public class Tree<T> : IEnumerable<Tree<T>>
    {
        private T _Item;
        public T Item { get { return _Item; } set { _Item = value; } }

        private List<Tree<T>> _Children = new List<Tree<T>>();
        public List<Tree<T>> Children
        {
            get
            {
                return _Children;
            }
            set
            {
                _Children = value;
            }
        }

        private Tree<T> _Parent;
        public Tree<T> Parent { get { return _Parent; } set { _Parent = value; } }

        public Tree()
        {

        }
        public Tree(T item)
        {
            _Item = item;
        }

        public IEnumerator<Tree<T>> GetEnumerator()
        {
            yield return this;
            foreach (var h in Children)
            {

                foreach (var hc in h)
                {
                    yield return hc;
                }
            }
        }
        public void Add(Tree<T> item)
        {
            this.Children.Add(item);
            item.Parent = this;
        }
        public void Add(T item) 
        {
            var h = new Tree<T>(item);
            this.Children.Add(h);
            h.Parent = this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ToHierarchyString(Func<T, string> tostringexpression, string tag = "")
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("{0}{1}", tag, tostringexpression(this.Item)));
            foreach (var child in Children)
            {
                sb.Append(child.ToHierarchyString(tostringexpression, tag + "    "));
            }
            return sb.ToString();
        }
        public string ToHierarchyString(string tag = "")
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("{0}{1}", tag, this.Item));
            foreach (var child in Children)
            {
                sb.Append(child.ToHierarchyString(tag + "    "));
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            return String.Format("{0}<{1}>", "Tree", typeof(T).Name);
        }


        public void Remove(Tree<T> t)
        {
            t.Parent = null;
            this.Children.Remove(t);
        }
    }
}
