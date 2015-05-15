using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class Hierarchy<TClass> where TClass:class
    {
        
        private TClass _Item = null;
        public TClass Item
        {
            get { return _Item; }
            set { _Item = value; }
        }
        

        private  Hierarchy<TClass> _Parent = null;
        [JsonIgnore]
        public Hierarchy<TClass> Parent 
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        private List<Hierarchy<TClass>> _Children = new List<Hierarchy<TClass>>();
        public List<Hierarchy<TClass>> Children 
        {
            get { return _Children; }
            set { _Children = value; }
        }

        private int _Order = 0;
        public int Order
        {
            get { return _Order; }
            set { _Order = value; }
        }
        public Hierarchy()
        {

        }
        public Hierarchy(TClass Item)
        {
            _Item = Item;
        }

        public Hierarchy(Hierarchy<TClass> Item)
        {
            _Item = Item.Item;
            _Order = Item.Order;
            foreach (var child in Item.Children) 
            {
                var newchild = new Hierarchy<TClass>(child);
                newchild.Parent = this;
                this.Children.Add(newchild);
            }
        }

        public int GetTotalChildCount(Hierarchy<TClass> item)
        {
            item = item == null ? this : item;
            int count = item.Children.Count;
            foreach (var child in item.Children)
            {
                count += GetTotalChildCount(child);
            }
            return count;
        }

        public int GetLevel()
        {
            int count = 1;
            if (this.Parent != null)
            {
                count = count + this.Parent.GetLevel();
            }
            else
            {
                count = 0;
            }

            return count;
        }

        public int GetLeafCount(Hierarchy<TClass> item, int level = 0)
        {
            item = item == null ? this : item;
            var leafcount = 0;
            foreach (var child in item.Children)
            {
                leafcount += GetLeafCount(child, level + 1);

            }
            if (item.Children.Count == 0)
            {
                return 1;
            }
            if (level == 0)
            {
                return leafcount;
            }
            return leafcount;
        }

        public Hierarchy<TClass> Find(Func<Hierarchy<TClass>, bool> func)
        {
            if (func(this))
            {
                return this;
            }
            else
            {
                foreach (var child in this.Children)
                {
                    var result = child.Find(func);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        public List<Hierarchy<TClass>> Where(Func<Hierarchy<TClass>, bool> func)
        {
            var results = new List<Hierarchy<TClass>>();
            if (func(this))
            {
                results.Add(this);
            }
            else
            {
                foreach (var child in this.Children)
                {
                    results.AddRange(child.Where(func));
                }
            }
            return results;
        }

        public static Hierarchy<TClass> GetHierarchy<TArc>(List<TArc> Arcs, List<Hierarchy<TClass>> Items,
            Func<Hierarchy<TClass>, TArc, Boolean> FromExpression,
            Func<Hierarchy<TClass>, TArc, Boolean> ToExpression,
            Action<Hierarchy<TClass>, TArc> OrderExpression) 
        {
            Hierarchy<TClass> root = Items.FirstOrDefault();
            foreach (var arc in Arcs)
            {
                var parents = Items.Where(i => FromExpression(i, arc));
                var children = Items.Where(i => ToExpression(i, arc));
                var childtemplate = children.FirstOrDefault();

                var nullchild = children.FirstOrDefault(i => i.Parent == null);
                var child = childtemplate.Parent == null ? childtemplate :
                    nullchild != null ? nullchild :
                    new Hierarchy<TClass>(childtemplate);
                if (parents.ToList().Count > 1 || children.ToList().Count > 1)
                {
                    int x = 0;
                }
                foreach (var parent in parents)
                {
                    if (parent != null && child != null)
                    {
                        child.Parent = parent;
                        OrderExpression(child, arc);
                        parent.Children.Add(child);

                    }
                    else
                    {
                        if (child != null)
                        {
                            root = child;
                        }
                    }
                }

            }

            /*
            foreach (var arc in Arcs)
            {
                var parent = Items.FirstOrDefault(i => FromExpression(i, arc));
                var child = Items.FirstOrDefault(i => ToExpression(i, arc));
                if (parent != null && child != null)
                {
                    child.Parent = parent;
                    OrderExpression(child, arc);
                    parent.Children.Add(child);

                }
                else
                {
                    if (child != null)
                    {
                        root = child;
                    }
                }
         
            }
            */
            return root;
        }

        public int GetSubLevelCount(Hierarchy<TClass> item, int level = 0)
        {
            item = item == null ? this : item;
            int max = level;
            foreach (var child in item.Children)
            {
                var clevel = GetSubLevelCount(child, level + 1);
                if (clevel > max)
                {
                    max = clevel;
                }
            }
            return max;
        }

        public List<Hierarchy<TClass>> All() 
        {
            var list = new List<Hierarchy<TClass>>();
            list.Add(this);
            foreach (var child in this.Children)
            {
                list.AddRange(child.All());
            }
            return list;
        }

        public List<Hierarchy<TClass>> GetLeafs()
        {
            var list = new List<Hierarchy<TClass>>();
            foreach (var child in this.Children)
            {
                list.AddRange(child.GetLeafs());
            }
            if (this.Children.Count == 0)
            {
                list.Add(this);
            }
            return list;
        }

        public List<List<Hierarchy<TClass>>> GetArcs() 
        {
            var items = new List<List<Hierarchy<TClass>>>();
            var leafs = this.GetLeafs();
            foreach (var leaf in leafs) 
            {
                var subitems = new List<Hierarchy<TClass>>();
                var currentleaf= leaf;
                subitems.Add(currentleaf);

                while (currentleaf.Parent != null) 
                {
                    subitems.Add(currentleaf.Parent);
                    currentleaf = currentleaf.Parent;
                }
                subitems.Reverse();
                items.Add(subitems);
            }
            return items;
        }

        public string ToHierarchyString(Func<TClass,string> tostringexpression,string tag="") 
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("{0}{1}", tag, tostringexpression(this.Item)));
            foreach (var child in this.Children)
            {
                sb.Append(child.ToHierarchyString(tostringexpression, tag + "    "));
            }
            return sb.ToString();
        }

        public string ToHierarchyString2(string tag = "")
        {
            var sb = new StringBuilder();
            foreach (var child in this.Children)
            {
                sb.Append(child.ToHierarchyString2(tag + "    "));
            }
            sb.AppendLine(String.Format("{0}{1}", tag, this.Item));

            return sb.ToString();
        }

        public List<Hierarchy<TClass>> ToHierarchy(bool childfirst=false)
        {
            var list = new List<Hierarchy<TClass>>();
            if (!childfirst) { list.Add(this); }
            foreach (var child in this.Children)
            {
                list.AddRange(child.ToHierarchy(childfirst));
            }
            if (childfirst) { list.Add(this); }

            return list;
        }

        public void SetParents()
        {
            foreach (var child in this.Children)
            {
                child.Parent = this;
                child.SetParents();
            }
        }

        public override string ToString()
        {
            return String.Format("H<{0}> - {1}", typeof(TClass).Name, this.Item);
        }

    }
}
