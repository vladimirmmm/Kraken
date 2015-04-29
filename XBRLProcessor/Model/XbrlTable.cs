using BaseModel;
using Model.DefinitionModel;
using Model.DefinitionModel.Formula;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using XBRLProcessor.Enums;
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

        private List<BreakdownTreeArc> _BreakdownTrees = new List<BreakdownTreeArc>();
        [JsonIgnore]
        public List<BreakdownTreeArc> BreakdownTrees { get { return _BreakdownTrees; } set { _BreakdownTrees = value; } }

        private List<DefinitionNodeSubTreeArc> _DefinitionNodeSubTrees = new List<DefinitionNodeSubTreeArc>();
        [JsonIgnore]
        public List<DefinitionNodeSubTreeArc> DefinitionNodeSubTrees { get { return _DefinitionNodeSubTrees; } set { _DefinitionNodeSubTrees = value; } }

        private List<AspectNodeFilterArc> _AspectNodeFilters = new List<AspectNodeFilterArc>();
        [JsonIgnore]
        public List<AspectNodeFilterArc> AspectNodeFilters { get { return _AspectNodeFilters; } set { _AspectNodeFilters = value; } }


        private List<RuleNode> _RuleNodes = new List<RuleNode>();
        [JsonIgnore]
        public List<RuleNode> RuleNodes { get { return _RuleNodes; } set { _RuleNodes = value; } }

        private List<AspectNode> _AspectNodes = new List<AspectNode>();
        [JsonIgnore]
        public List<AspectNode> AspectNodes { get { return _AspectNodes; } set { _AspectNodes = value; } }

        private List<Model.DefinitionModel.Filter.DimensionFilter> _DimensionFilters = new List<Model.DefinitionModel.Filter.DimensionFilter>();
        [JsonIgnore]
        public List<Model.DefinitionModel.Filter.DimensionFilter> DimensionFilters { get { return _DimensionFilters; } set { _DimensionFilters = value; } }



        private List<Table> _Tables = new List<Table>();
        [JsonIgnore]
        public List<Table> Tables { get { return _Tables; } set { _Tables = value; } }

        private List<DefinitionLink> _DefinitionLinks = new List<DefinitionLink>();
        public List<DefinitionLink> DefinitionLinks { get { return _DefinitionLinks; } set { _DefinitionLinks = value; } }

        public Hierarchy<Locator> DefinitionRoot { get; set; }
        public List<Hierarchy<Locator>> DefinitionItems = new List<Hierarchy<Locator>>();


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
            Identifiables.AddRange(AspectNodes);
            Identifiables.AddRange(DimensionFilters);
            Arcs.AddRange(BreakdownTrees);
            Arcs.AddRange(TableBreakDowns);
            Arcs.AddRange(DefinitionNodeSubTrees);
            Arcs.AddRange(AspectNodeFilters);

            foreach (var identifiable in Identifiables)
            {
                var li = new LogicalModel.LayoutItem();
                li.Table = logicaltable;
                var hi = new Hierarchy<LogicalModel.LayoutItem>(li);
                li.ID = identifiable.ID;
                li.LabelID = identifiable.LabelID;
                li.LoadLabel(Taxonomy);


                var rule = identifiable as RuleNode;
                if (rule != null)
                {
                    if (rule.Concept != null)
                    {
                        li.Concept = rule.Concept.QName.Content;
                    }
                    if (rule.Abstract) 
                    {
                        li.IsAbstract = rule.Abstract;
                    }
                    foreach (var dimension in rule.Dimensions) 
                    {
                        var logicaldimension = new LogicalModel.Dimension();
                        var explicitDimension = dimension as ExplicitDimension;
                        var typedDimension = dimension as TypedDimension;
                        if (explicitDimension!=null){
                            logicaldimension.Value = explicitDimension.Dimension;
                            if (explicitDimension.Members.Count == 1)
                            {
                                var member = explicitDimension.Members.FirstOrDefault();
                                logicaldimension.Domain = member.QName.Domain;
                                logicaldimension.DomainMember = member.QName.Value;
                            }
                            else
                            {
                                Console.WriteLine("Multiple Members Detected");
                            }
                        }
                        if (typedDimension != null) 
                        {
                            Console.WriteLine("Typed Dimension Detected");

                        }
                        if (logicaldimension != null) 
                        {
                            li.Dimensions.Add(logicaldimension);

                        }
                    }
            
                }

                var aspect = identifiable as AspectNode;
                if (aspect != null)
                {

                    li.IsAspect = true;
                    var logicaldimension = new LogicalModel.Dimension();
                    logicaldimension.Value = aspect.DimensionAspect.Value;
                    logicaldimension.Domain = aspect.DimensionAspect.Domain;
                    logicaldimension.DomainMember = aspect.DimensionAspect.Value;

                    if (logicaldimension != null)
                    {
                        li.Dimensions.Add(logicaldimension);
                    }

                }
                logicaltable.LayoutItems.Add(hi);

            }

            logicaltable.LayoutRoot = Hierarchy<LogicalModel.LayoutItem>.GetHierarchy(Arcs, logicaltable.LayoutItems,
                (i, a) => i.Item.ID == a.From, (i, a) => i.Item.ID == a.To,
                (i, a) => { i.Item.Order = a.Order; i.Item.Axis = a is TableBreakDownArc ? ((TableBreakDownArc)a).Axis.ToString() : ""; });
            logicaltable.ID = logicaltable.LayoutRoot.Item.LabelCode;
            logicaltable.SetHtmlPath();

        }
        
       
        public void LoadDefinitionHierarchy(LogicalModel.Table logicaltable)
        {
            var rootlocator = new Locator();
            rootlocator.ID = "Root";
            var rootNode = new Hierarchy<Locator>(rootlocator);
            DefinitionItems.Add(rootNode);
            Console.WriteLine(this.XsdPath);
            
            foreach (var definitionlink in DefinitionLinks)
            {
                var hascube = definitionlink.DefinitionArcs.Any(i => i.RoleType == ArcRoleType.hypercube_dimension);
                if (hascube)
                {
                    definitionlink.LoadHierarchy();
                    if (definitionlink.DefinitionRoot.Find(i => i.Item.RoleType == ArcRoleType.domain_member) != null)
                    {
                        rootNode.Children.Add(definitionlink.DefinitionRoot);
                        definitionlink.DefinitionRoot.Parent = rootNode;
                        DefinitionItems.AddRange(definitionlink.DefinitionItems);

                    }
                    else
                    {
                        //no domain member found
                    }
                }
            

            }          

            DefinitionRoot = rootNode;

            LocateDefinitions();

            var hypercubenodes = DefinitionItems.Where(i => i.Item.RoleType.In(ArcRoleType.all, ArcRoleType.notAll)).ToList();
            foreach (var hypercubenode in hypercubenodes) 
            {
                var hypercube = new LogicalModel.HyperCube();
                //map the concepts
                var concepts = new List<Hierarchy<Locator>>();
                var parent = hypercubenode.Parent;
                concepts.Add(parent);
                if (parent.Children.Count > 1)
                {
                    var parentconcepts = parent.Children.Where(i => i.Item.RoleType != ArcRoleType.all);
                    concepts.AddRange(parentconcepts);
                }

                foreach (var conceptnode in concepts) 
                {
                    var logicalconcept = new LogicalModel.Concept();
                    logicalconcept.Content = conceptnode.Item.Element.Key;
                    logicalconcept.Name = conceptnode.Item.Element.Name;
                    hypercube.Concepts.Add(logicalconcept);

                }

                //map the dimensions
                var domains = hypercubenode.Where(i => i.Item.RoleType == ArcRoleType.dimension_domain).Where(i => i.Children.Count > 0).ToList();
                domains = domains.Where(i => i.Children.FirstOrDefault(j => !j.Item.Element.IsDefaultMember) != null).ToList();

                var hypercubeitems = hypercubenode.Where(i => i.Item.RoleType == ArcRoleType.hypercube_dimension).Where(i => i.Children.Count > 0).ToList();
                foreach (var hypercubeitem in hypercubeitems) 
                {
                    var logicalhypercubeitem = new LogicalModel.Dimensions.DimensionItem();
                    logicalhypercubeitem.Content = hypercubeitem.Item.Element.Key;
                    logicalhypercubeitem.Name = hypercubeitem.Item.Element.Name;

                    var completedomains = hypercubeitem.Children.Where(i => i.Children.FirstOrDefault(j => !j.Item.Element.IsDefaultMember) != null).ToList();
                    if (completedomains.Count > 0) 
                    {
                        logicalhypercubeitem.LabelID = hypercubeitem.Item.LabelID;
                        hypercube.DimensionItems.Add(logicalhypercubeitem);
                    }
                    foreach (var domain in completedomains) 
                    {
                        var logicaldomain= new LogicalModel.Dimensions.DimensionDomain();
                        logicaldomain.Content = domain.Item.Element.Key;
                        logicaldomain.Name = domain.Item.Element.Name;
                        logicaldomain.DimensionItem = logicalhypercubeitem;
                        logicaldomain.LabelID = domain.Item.LabelID;
                        logicalhypercubeitem.Domains.Add(logicaldomain);
                        
                        foreach (var domainmember in domain.Children) 
                        {

                                var logicalmember = new LogicalModel.Dimensions.DimensionMember();
                                logicalmember.Content = domainmember.Item.Element.Key;
                                logicalmember.Name = domainmember.Item.Element.Name;
                                logicalmember.Domain = logicaldomain;
                                logicalmember.LabelID = domainmember.Item.LabelID;
                                logicalmember.IsDefaultMember = domainmember.Item.Element.IsDefaultMember;
                                logicaldomain.DomainMembers.Add(logicalmember);
      
                        }
                    }

                }

                logicaltable.HyperCubes.Add(hypercube);
            
            }
           
        }
        public void LocateDefinitions()
        {
            foreach (var definition in this.DefinitionItems)
            {
                definition.Item.Locate();
            }
        }  
        public override string ToString()
        {
            return base.ToString();
        }


        

    }


}
