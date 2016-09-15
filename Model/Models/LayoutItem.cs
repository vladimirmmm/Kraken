using LogicalModel.Base;
using LogicalModel.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel
{
    public enum LayoutItemCategory
    {
        Unknown = 0,
        Aspect = 1,
        Rule = 2,
        BreakDown = 3,
        Dynamic = 4,
        Filter = 5,
        Key = 6,
    }
    public class LayoutItem 
    {
        public string RowSpan = "";
        public string ColSpan = "";
        public string CssClass = "";
        private Table _Table = null;
        public int Order = 0;   

        private string _Axis = "";
        public string Axis { get { return _Axis; } set { _Axis = value; } }

        private string _Role = "";
        public string Role { get { return _Role; } set { _Role = value; } }

        private string _RoleAxis = "";
        public string RoleAxis { get { return _RoleAxis; } set { _RoleAxis = value; } }

        private string _ID = "";
        public string ID { get { return _ID; } set { _ID = value; } }
    
        private string _LabelID = "";
        public string LabelID { get { return _LabelID; } set { _LabelID = value; } }

        private bool _IsAbstract = false;
        public bool IsAbstract { get { return _IsAbstract; } set { _IsAbstract = value; } }

        public bool IsPlaceholder { get; set; }
        //private bool _IsAbstract = false;
        //public bool IsAbstract { get { return _IsAbstract; } set { _IsAbstract = value; } }

        private LayoutItemCategory _Category = LayoutItemCategory.Unknown;
        public LayoutItemCategory Category { get { return _Category; } set { _Category = value; } }

        public Label Label = null;

        private String _LabelContent = "";
        public String LabelContent 
        {
            get 
            {
                if (string.IsNullOrEmpty(_LabelContent))
                {
                    if (Label != null)
                    {
                        _LabelContent = Label.Content;
                    }
                }
                return _LabelContent;
            }
            set 
            {
                _LabelContent = value;
            }
        }

        private String _LabelCode = "";
        public String LabelCode
        {
            get
            {

                if (string.IsNullOrEmpty(_LabelCode))
                {
                    if (Label != null)
                    {
                        _LabelCode = Label.Code;
                    }
                }
                return _LabelCode;
            }
            set
            {
                _LabelCode = value;
            }
        }

        private List<Dimension> _Dimensions = new List<Dimension>();
        public List<Dimension> Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }
        
        public Concept Concept { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - [{1}] {2}", ID, Label == null ? LabelID : LabelCode, Label == null ? LabelID : LabelContent);
        }

        private string _FactString = "";
        public string FactString
        {
            get
            {
                if (string.IsNullOrEmpty(_FactString))
                {
                    _FactString = QNameHelpers.GetFactString(Concept, Dimensions);
                }
                return _FactString;
            }
            set
            {
                _FactString = value;
                var fb = QNameHelpers.GetFactBase(_FactString);
                this.Concept = fb.Concept;
                this.Dimensions = fb.Dimensions;
            }
        }
        
        [JsonIgnore]
        public Table Table 
        {
            get { return _Table; }
            set { _Table = value; }
        }


        public LayoutItem()
        {
        }

        public LayoutItem(LayoutItem item)
        {
            this.ID = item.ID;
            this.IsAbstract = item.IsAbstract;
            this.Category = item.Category;
            this.Axis = item.Axis;
            this.Concept = item.Concept;
            this.Dimensions = item.Dimensions;
            this.Label = item.Label;
            this.Order = item.Order;
            this.Role = item.Role;
            this.RoleAxis = item.RoleAxis;
        }



        public void LoadLabel(Taxonomy Taxonomy)
        {
            var folder = _Table.FolderName;
            var key = Label.GetKey(folder, this.LabelID);
            this.Label = Taxonomy.FindLabel(key);
            //this.Label = Taxonomy.TaxonomyLabels.FirstOrDefault(i => i.LocalID == this.LabelID);

        }

        public bool IsAspect
        {
            get
            {
                return Category == LayoutItemCategory.Aspect;
            }
        }

        public bool IsVisible
        {
            get
            {
                //return !IsAbstract && Category.In(LayoutItemCategory.Rule, LayoutItemCategory.Aspect);
                return !IsAbstract && Category.In(LayoutItemCategory.Rule, LayoutItemCategory.Aspect, LayoutItemCategory.Dynamic, LayoutItemCategory.Key);
            }
        }
        public static bool IsLayoutLeaf(BaseModel.Hierarchy<LayoutItem> hli) 
        {
            var sublayoutitems = hli.Where(i => i.Item.IsLayout && i != hli).ToList();
            return hli.Item.IsLayout && sublayoutitems.Count == 0;
  
        }

        public bool IsLayout
        {
            get
            {
                return Category.In(LayoutItemCategory.Rule, LayoutItemCategory.Aspect, LayoutItemCategory.Dynamic, LayoutItemCategory.Key);
            }
        }
        public bool IsStructural
        {
            get
            {
                return Category.In(LayoutItemCategory.Rule, LayoutItemCategory.Aspect, 
                    LayoutItemCategory.BreakDown, LayoutItemCategory.Dynamic, LayoutItemCategory.Key);
            }
        }

        public bool IsDynamic
        {
            get
            {
                return Category == LayoutItemCategory.Dynamic;
            }
        }

        public static LayoutItem Copy(LayoutItem li_original)
        {
            var li_new = new LayoutItem();
            li_new._Axis = li_original._Axis;
            li_new._Category = li_original._Category;
            li_new.FactString = li_original.FactString;

            var fact = new FactBase();
            fact.SetFromString(li_new.FactString);

            li_new.Dimensions = fact.Dimensions;
            li_new.Concept = fact.Concept;

            li_new._ID = li_original._ID;
            li_new._IsAbstract = li_original._IsAbstract;
            li_new.Label = li_original.Label;
            li_new._LabelCode = li_original._LabelCode;
            li_new._LabelContent = li_original._LabelContent;
            li_new._LabelID = li_original._LabelID;
            li_new._Role = li_original._Role;
            return li_new;
        }

        internal void SetTyped()
        {
            foreach (var d in Dimensions) 
            {
                d.SetTyped();
            }
        }
    }
}
