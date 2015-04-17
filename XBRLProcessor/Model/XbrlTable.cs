using BaseModel;
using Model.DefinitionModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Models;

namespace XBRLProcessor.Model
{
    public class XbrlTable : Identifiable
    {
        private string _HtmlPath = "";
        public string HtmlPath
        {
            get { return _HtmlPath; }
            set { _HtmlPath = value; }
        }
        private string _XsdPath = "";
        public string XsdPath
        {
            get { return _XsdPath; }
            set { _XsdPath = value; }
        }
        private string _LayoutPath = "";
        public string LayoutPath
        {
            get { return _LayoutPath; }
            set { _LayoutPath = value; }
        }
        private string _DefinitionPath = "";
        public string DefinitionPath
        {
            get { return _DefinitionPath; }
            set { _DefinitionPath = value; }
        }


        public XbrlTaxonomy Taxonomy = null;

        private List<BreakDown> _BreakDowns = new List<BreakDown>();
        [JsonIgnore]
        public List<BreakDown> BreakDowns { get { return _BreakDowns; } set { _BreakDowns = value; } }

        private List<TableBreakDownArc> _TableBreakDowns = new List<TableBreakDownArc>();
        [JsonIgnore]
        public List<TableBreakDownArc> TableBreakDowns { get { return _TableBreakDowns; } set { _TableBreakDowns = value; } }

        private List<RuleNode> _RuleNodes = new List<RuleNode>();
        [JsonIgnore]
        public List<RuleNode> RuleNodes { get { return _RuleNodes; } set { _RuleNodes = value; } }

        private List<BreakdownTreeArc> _BreakdownTrees = new List<BreakdownTreeArc>();
        [JsonIgnore]
        public List<BreakdownTreeArc> BreakdownTrees { get { return _BreakdownTrees; } set { _BreakdownTrees = value; } }

        private List<DefinitionNodeSubTreeArc> _DefinitionNodeSubTrees = new List<DefinitionNodeSubTreeArc>();
        [JsonIgnore]
        public List<DefinitionNodeSubTreeArc> DefinitionNodeSubTrees { get { return _DefinitionNodeSubTrees; } set { _DefinitionNodeSubTrees = value; } }

        private List<Table> _Tables = new List<Table>();
        [JsonIgnore]
        public List<Table> Tables { get { return _Tables; } set { _Tables = value; } }

        private List<DefinitionLink> _DefinitionLinks = new List<DefinitionLink>();
        public List<DefinitionLink> DefinitionLinks { get { return _DefinitionLinks; } set { _DefinitionLinks = value; } }


        //Model.DefinitionModel.Table
        [JsonIgnore]
        public List<Identifiable> Identifiables = new List<Identifiable>();
        [JsonIgnore]
        public List<Arc> Arcs = new List<Arc>();

        public string FilingIndicator;

        public void LoadLayoutHierarchy(LogicalModel.Table logicaltable)
        {
            Identifiables.AddRange(Tables);
            Identifiables.AddRange(BreakDowns);
            Identifiables.AddRange(RuleNodes);
            Arcs.AddRange(BreakdownTrees);
            Arcs.AddRange(TableBreakDowns);
            Arcs.AddRange(DefinitionNodeSubTrees);

            foreach (var identifiable in Identifiables)
            {
                var li = new LogicalModel.LayoutItem();
                var hi = new Hierarchy<LogicalModel.LayoutItem>(li);
                li.ID = identifiable.ID;
                li.LabelID = identifiable.LabelID;
                li.Label = Taxonomy.TaxonomyLabels.FirstOrDefault(i => i.LocalID == identifiable.LabelID);

                var rule = identifiable as RuleNode;
                if (rule != null)
                {
                    if (rule.Concept != null)
                    {
                        li.Concept = rule.Concept.QName.Content;
                    }
                    li.Dimensions = rule.Dimensions.OrderBy(i => i.ToString()).Select(i => i.ToString()).ToList();
                    //dim an concepts
                }
                logicaltable.LayoutItems.Add(hi);

            }

            logicaltable.LayoutRoot = Hierarchy<LogicalModel.LayoutItem>.GetHierarchy(Arcs, logicaltable.LayoutItems,
                (i, a) => i.Item.ID == a.From, (i, a) => i.Item.ID == a.To,
                (i, a) => i.Item.Order = a.Order);
            logicaltable.ID = logicaltable.LayoutRoot.Item.LabelContent;
            logicaltable.SetHtmlPath();

        }

        public void LoadDefinitionHierarchy(LogicalModel.Table logicaltable)
        {
            foreach (var definitionlink in DefinitionLinks)
            {
                definitionlink.LoadHierarchy();
            }


        }

        public override string ToString()
        {
            return base.ToString();
        }


        

    }


}
