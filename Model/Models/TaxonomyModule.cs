using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public class TaxonomyModule
    {
        public Taxonomy Taxonomy = null;

        public String TaxonomyID { get; set; }
        public String TaxonomyName { get; set; }
        public String TaxonomyFolder { get; set; }
        public String ID { get; set; }
        public String Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public String SchemaRef { get; set; }
        public String LocalPath { get; set; }

        private List<String> _TablePaths = new List<string>();
        public List<String> TablePaths 
        {
            get { return _TablePaths; }
            set { _TablePaths = value; }
        }
        private Hierarchy<TableGroup> _TableGroups = new Hierarchy<TableGroup>(new TableGroup());
        public Hierarchy<TableGroup> TableGroups
        {
            get { return _TableGroups; }
            set { _TableGroups = value; }
        }
        private TaxonomyProperties _GeneralProperties = new TaxonomyProperties();
        public TaxonomyProperties GeneralProperties
        {
            get { return _GeneralProperties; }
            set { _GeneralProperties = value; }
        }

        private Dictionary<string, int> _FactParts = new Dictionary<string, int>();
        public Dictionary<string, int> FactParts
        {
            get { return _FactParts; }
            set { _FactParts = value; }
        }

        private Dictionary<int, int> _TypedDimensions = new Dictionary<int, int>();
        public Dictionary<int, int> TypedDimensions
        {
            get { return _TypedDimensions; }
            set { _TypedDimensions = value; }
        }

        private Dictionary<int, int> _Concepts = new Dictionary<int, int>();
        public Dictionary<int, int> Concepts
        {
            get { return _Concepts; }
            set { _Concepts = value; }
        }
        private Dictionary<int, int> _DimensionDomainsOfMembers = new Dictionary<int, int>();
        public Dictionary<int, int> DimensionDomainsOfMembers
        {
            get { return _DimensionDomainsOfMembers; }
            set { _DimensionDomainsOfMembers = value; }
        }
        private TaxonomySettings _UserSettings = new TaxonomySettings();
        public TaxonomySettings UserSettings
        {
            get { return _UserSettings; }
            set { _UserSettings = value; }
        }
        public void Load() 
        {
            LoadSettings();
        }
        public void LoadSettings() 
        {
            var itemtypes = this.Taxonomy.Concepts.Select(i => i.Value.ItemType).Distinct().ToList();
            foreach (var itemtype in itemtypes)
            {
                var itsetting = new ItemTypeSetting();
                itsetting.Set(itemtype, this.Taxonomy);
                var unit = this.Taxonomy.Units.FirstOrDefault(i => i.ItemType == itsetting.ItemType);
                if (unit != null)
                {
                    itsetting.UnitID = unit.ID;
                }
                var existing = UserSettings.ItemTypeSettings.FirstOrDefault(i => i.ItemType == itsetting.ItemType);
                if (existing == null)
                {
                    UserSettings.ItemTypeSettings.Add(itsetting);
                }
            }
        }

        public void Clear()
        {
            TablePaths.Clear();
            TableGroups.Clear();
        }
    }
}
