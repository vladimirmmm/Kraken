﻿using BaseModel;
using Model.DefinitionModel;
using Model.DefinitionModel.Formula;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities;
using XBRLProcessor.Enums;
using XBRLProcessor.Mapping;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.DefinitionModel;
using XBRLProcessor.Model.DefinitionModel.Filter;
using XBRLProcessor.Model.DefinitionModel.Formula;
using XBRLProcessor.Models;

namespace XBRLProcessor.Model
{
    public class XbrlTable : XbrlIdentifiable
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

        private List<Model.DefinitionModel.Filter.DimensionRelationShip> _DimensionRelationShips = new List<Model.DefinitionModel.Filter.DimensionRelationShip>();
        [JsonIgnore]
        public List<Model.DefinitionModel.Filter.DimensionRelationShip> DimensionRelationShips { get { return _DimensionRelationShips; } set { _DimensionRelationShips = value; } }

        //DimensionRelationShip

        private List<TableNode> _Tables = new List<TableNode>();
        [JsonIgnore]
        public List<TableNode> Tables { get { return _Tables; } set { _Tables = value; } }

        private List<DefinitionLink> _DefinitionLinks = new List<DefinitionLink>();
        public List<DefinitionLink> DefinitionLinks { get { return _DefinitionLinks; } set { _DefinitionLinks = value; } }

        public Hierarchy<Locator> DefinitionRoot { get; set; }
        public List<Hierarchy<Locator>> DefinitionItems = new List<Hierarchy<Locator>>();


        [JsonIgnore]
        public List<XbrlIdentifiable> Identifiables = new List<XbrlIdentifiable>();
        [JsonIgnore]
        public List<Arc> Arcs = new List<Arc>();

        public string FilingIndicator;

        private List<LogicalModel.Base.QualifiedItem> GetMembers(DimensionMember FilterMember) 
        {
            var result = new List<Hierarchy<LogicalModel.Base.QualifiedItem>>();
            var resultx = new List<LogicalModel.Base.QualifiedItem>();
            var hierarchynode = Taxonomy.Hierarchies.FirstOrDefault(i => i.Item.Role == FilterMember.ArcRole);
            var membernode = hierarchynode.FirstOrDefault(i => i.Item.FullName == FilterMember.QName.Content);
            var axes = FilterMember.Axis.ToLower();
            switch (axes)
            {
                case "ancestor":
                    result = membernode.Parents();
                    break;
                case "ancestor-or-self":
                    result = membernode.Parents();
                    result.Add(membernode);
                    break;
                case "attribute":
                    throw new NotImplementedException("Dimension Filter Member axis " + axes + " is not implemented!");
                case "child":
                    result = membernode.Children;
                    break;
                case "descendant":
                    result = membernode.Descendant();
                    break;
                case "descendant-or-self":
                    result = membernode.All();
                    break;
                case "following":
                    throw new NotImplementedException("Dimension Filter Member axis " + axes + " is not implemented!");

                case "following-sibling":
                    throw new NotImplementedException("Dimension Filter Member axis " + axes + " is not implemented!");

                case "namespace":
                    throw new NotImplementedException("Dimension Filter Member axis " + axes + " is not implemented!");

                case "parent":
                    result.Add(membernode.Parent);
                    break;
                case "preceding":
                    throw new NotImplementedException("Dimension Filter Member axis " + axes + " is not implemented!");

                case "preceding-sibling":
                    throw new NotImplementedException("Dimension Filter Member axis " + axes + " is not implemented!");

                case "self":
                    result.Add(membernode);

                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            resultx=result.Select(i=>i.Item).ToList();
            return resultx;
        }

        protected Hierarchy<Link> GetXbrlRendering() 
        {
            var result = new Hierarchy<Link>();
            foreach (var arc in Arcs) 
            {
                var from = Identifiables.FirstOrDefault(i => i.LabelID == arc.From);
                var to = Identifiables.FirstOrDefault(i => i.LabelID == arc.To);
                var harc = new Hierarchy<Link>(arc);
                var hfrom = new Hierarchy<Link>(from);
                var hto = new Hierarchy<Link>(to);
                hfrom.AddChild(harc);
                harc.AddChild(hto);
                if (result.Item==null)
                {
                    result=hfrom;
                }
            }
            while (result.Parent != null) 
            {
                result = result.Parent;
            }
            return result;
        }

        public void LoadLayoutHierarchy(LogicalModel.Table logicaltable)
        {
            Identifiables.Clear();
            Arcs.Clear();
            Identifiables.AddRange(Tables);
            Identifiables.AddRange(BreakDowns);
            Identifiables.AddRange(RuleNodes);
            Identifiables.AddRange(AspectNodes);
            Identifiables.AddRange(DimensionFilters);
            Identifiables.AddRange(DimensionRelationShips);
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

                var df = identifiable as DimensionFilter;
                if (df != null) 
                {
                    li.Category = LogicalModel.LayoutItemCategory.Filter;

                    var dfexp = df as ExplicitDimensionFilter;
                    if (dfexp != null)
                    {
                        var member = dfexp.Members.FirstOrDefault();
                        if (member != null)
                        {
                            li.Role = member.LinkRole;
                            li.RoleAxis = member.Axis;

                        }
                    }
                    var drel = df as DimensionRelationShip;
                    if (drel != null)
                    {
                        var member = drel.Member;
                        if (member != null)
                        {
                            li.Role = drel.LinkRole;
                            li.RoleAxis = drel.Axis;
                            var dim = new LogicalModel.Dimension();
                            dim.DimensionItem = drel.Dimension.QName.Content;
                            dim.Domain = drel.Member.Domain;
                            //dim.DomainMember = member.Value;
                            dim.SetTyped();
                            li.Dimensions.Add(dim);
                            li.Category = LogicalModel.LayoutItemCategory.Aspect;
                        }
                       
                    }

                }

                var rule = identifiable as RuleNode;
                if (rule != null)
                {
                    li.Category = LogicalModel.LayoutItemCategory.Rule;

                    if (rule.Concept != null)
                    {
                        li.Concept = Mappings.ToLogical(rule.Concept); //rule.Concept.QName.Content;
                    }
                    if (rule.Abstract) 
                    {
                        li.IsAbstract = rule.Abstract;
                    }
                    var dimensions = rule.Dimensions;//.Where(i=>i.)
                    foreach (var dimension in dimensions)
                    {
                        var logicaldimension = new LogicalModel.Dimension();
                        var explicitDimension = dimension as ExplicitDimension;
                        var typedDimension = dimension as TypedDimension;
                        if (explicitDimension != null)
                        {
                            logicaldimension.DimensionItem = explicitDimension.Dimension;
                            if (explicitDimension.Members.Count == 1)
                            {
                                var member = explicitDimension.Members.FirstOrDefault();
                                logicaldimension.Domain = member.QName.Domain;
                                logicaldimension.DomainMember = member.QName.Value;
                            }
                            else
                            {
                                Logger.WriteLine("Multiple Members Detected");
                            }
                        }
                        if (typedDimension != null)
                        {
                            Logger.WriteLine("Typed Dimension Detected");

                        }
                        li.Dimensions.Add(logicaldimension);
                    }
            
                }

                var aspect = identifiable as AspectNode;
                if (aspect != null)
                {

                    li.Category = LogicalModel.LayoutItemCategory.Aspect;
                    var logicaldimension = new LogicalModel.Dimension();
                    logicaldimension.DimensionItem = aspect.DimensionAspect.Content;
                    var hc = logicaltable.HyperCubes.FirstOrDefault(i => i.DimensionItems.Any(j => j.FullName == logicaldimension.DimensionItem));
                    if (hc != null) 
                    {
                        var dimitem = hc.DimensionItems.FirstOrDefault(i=>i.FullName==logicaldimension.DimensionItem);
                        if (dimitem!=null && dimitem.Domains.Count>0)
                        {
                            //logicaldimension.Domain = dimitem.Domains.FirstOrDefault().FullName;
                            var domain = dimitem.Domains.FirstOrDefault();
                            logicaldimension.Domain = LogicalModel.Taxonomy.IsTyped(domain.FullName) ? domain.FullName : domain.ID;

                        }
                    }
                    logicaldimension.SetTyped();
                    li.Dimensions.Add(logicaldimension);


                }
                var breakdown = identifiable as BreakDown;

                if (breakdown != null)
                {
                    li.Category = LogicalModel.LayoutItemCategory.BreakDown;
                }

                logicaltable.LayoutItems.Add(hi);

            }

            var layoutfilename = Utilities.Strings.GetFileName(this.LayoutPath);
            XmlNamespaceManager nsmanager = null;
            var layoutdoc = this.Taxonomy.TaxonomyDocuments.FirstOrDefault(i => i.FileName == layoutfilename);
            if (layoutdoc != null) 
            {
                nsmanager = Utilities.Xml.GetTaxonomyNamespaceManager(layoutdoc.XmlDocument);
            }
            var d_con = new Dictionary<string,string>();
            foreach (var li in logicaltable.LayoutItems) 
            {
                var concept = li.Item.Concept;
                if (concept != null)
                {
                    
                    var nsuri = nsmanager.LookupNamespace(concept.Namespace);
                    concept.Namespace = Taxonomy.FindNamespacePrefix(nsuri, concept.Namespace);                


                }
                foreach(var dimension in li.Item.Dimensions)
                {
       
                    var dimqname =new QName( dimension.DimensionItem);
                    var nsuri = nsmanager.LookupNamespace(dimqname.Domain);
                    dimqname.Domain = Taxonomy.FindNamespacePrefix(nsuri, dimqname.Domain);                
                    dimension.DimensionItem = dimqname.Content;

                    string domnsuri;
                
                    if (dimension.IsTyped)
                    {
                        var domqname = new QName(dimension.Domain);
                        domnsuri = nsmanager.LookupNamespace(domqname.Domain);
                        domqname.Domain = Taxonomy.FindNamespacePrefix(domnsuri, domqname.Domain);
                        dimension.Domain = domqname.Content;
                    }
                    else 
                    {
                        domnsuri = nsmanager.LookupNamespace(dimension.Domain);
                        dimension.Domain = Taxonomy.FindNamespacePrefix(domnsuri, dimension.Domain);
                    }
                }
            }

            logicaltable.LayoutRoot = Hierarchy<LogicalModel.LayoutItem>.GetHierarchy(Arcs, logicaltable.LayoutItems,
                (i, a) => i.Item.LabelID == a.From, (i, a) => i.Item.LabelID == a.To,
                (i, a) => { i.Item.Order = a.Order; i.Item.Axis = a is TableBreakDownArc ? ((TableBreakDownArc)a).Axis.ToString() : ""; });
            logicaltable.ID = logicaltable.LayoutRoot.Item.ID;
            logicaltable.Name = logicaltable.LayoutRoot.Item.LabelContent;
            logicaltable.SetHtmlPath();
            Utilities.FS.WriteAllText(logicaltable.LayoutPath, logicaltable.LayoutRoot.ToHierarchyString(GetLayoutString));
            //var x = GetXbrlRendering();
            //Utilities.FS.WriteAllText(logicaltable.FullHtmlPath.Replace(".html", "_layout.txt"), x.ToHierarchyString(GetLayoutString));

        }
        private string GetLayoutString(Link li)
        {
            var result = li.ToXmlString();
            return result;
        }
        private string GetLayoutString(LogicalModel.LayoutItem li) 
        {
            var result = "";
            var axis = String.IsNullOrEmpty(li.Axis) ? "" : li.Axis + " ";
            var isabstract = li.IsAbstract ? "abstract " : "";
            result = String.Format("{0}{1} <{2}> {3} code{4} >> {5}", axis, li.ID, li.Category, isabstract, li.LabelCode, li.FactString);
            return result;
        }

        public void LoadDefinitionHierarchy(LogicalModel.Table logicaltable)
        {
            var rootlocator = new Locator();
            rootlocator.ID = "Root";
            var rootNode = new Hierarchy<Locator>(rootlocator);
            DefinitionItems.Clear();
            DefinitionItems.Add(rootNode);
            XmlNamespaceManager nsmanager = null;
            var refdoc = this.Taxonomy.TaxonomyDocuments.FirstOrDefault(i => i.LocalPath == this.DefinitionPath);
            if (refdoc != null)
            {
                nsmanager = Utilities.Xml.GetTaxonomyNamespaceManager(refdoc.XmlDocument);
            }

            foreach (var definitionlink in DefinitionLinks)
            {
                definitionlink.LoadHierarchy();
                DefinitionItems.AddRange(definitionlink.DefinitionItems);

            }
            foreach (var definitionlink in DefinitionLinks)
            {
                var locatorswithroles = definitionlink.DefinitionRoot.Where(i => !String.IsNullOrEmpty(i.Item.TargetRole));
                foreach (var l in locatorswithroles)
                {
                    var ss = DefinitionLinks.FirstOrDefault(i => i.Role == l.Item.TargetRole).DefinitionRoot;
                    ss.Item.RoleType = l.Item.RoleType;
                    l.Item = ss.Item;
                    l.Children = ss.Children;
                }
                var hascube = definitionlink.DefinitionArcs.Any(i => i.RoleType == ArcRoleType.hypercube_dimension);
                if (hascube)
                {

                    if (definitionlink.DefinitionRoot.Find(i => i.Item.RoleType == ArcRoleType.domain_member) != null)
                    {
                        rootNode.Children.Add(definitionlink.DefinitionRoot);
                        definitionlink.DefinitionRoot.Parent = rootNode;

                    }
                    else
                    {
                        //no domain member found
                    }
                }
                else
                {
                    var hasall = definitionlink.DefinitionArcs.Any(i => i.RoleType == ArcRoleType.all);
                    if (hasall)
                    {
                        rootNode.Children.Add(definitionlink.DefinitionRoot);
                        definitionlink.DefinitionRoot.Parent = rootNode;
                    }
                }


            }

            DefinitionRoot = rootNode;

            LocateDefinitions();

            var nontrivialdomains = new Dictionary<string, LogicalModel.Dimensions.DimensionDomain>();

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
                    if (conceptnode.Item.Element != null)
                    {
                        logicalconcept.Content = conceptnode.Item.Element.Key;
                        logicalconcept.Name = conceptnode.Item.Element.Name;
                        hypercube.Concepts.Add(logicalconcept);
                    }
                    else
                    {
                        Logger.WriteLine(String.Format("Concept Element not found for {0}!", conceptnode.Item.ID));
                    }

                }

                //map the dimensions
                var domains = hypercubenode.Where(i => i.Item.RoleType == ArcRoleType.dimension_domain).Where(i => i.Children.Count > 0);
                domains = domains.Where(i => i.Children.FirstOrDefault(j => !j.Item.Element.IsDefaultMember) != null).ToList();

                var hypercubeitems = hypercubenode.Where(i => i.Item.RoleType == ArcRoleType.hypercube_dimension); //.Where(i => i.Children.Count > 0)
                foreach (var hypercubeitem in hypercubeitems)
                {
                    var logicalhypercubeitem = new LogicalModel.Dimensions.DimensionItem();
                    //logicalhypercubeitem.Content = hypercubeitem.Item.Element.Key;
                    logicalhypercubeitem.Name = hypercubeitem.Item.Element.Name;
                    logicalhypercubeitem.Namespace = hypercubeitem.Item.Element.Namespace;
                    logicalhypercubeitem.LabelID = hypercubeitem.Item.LabelID;

                    var fixedns = Taxonomy.FindNamespacePrefix(hypercubeitem.Item.Element.NamespaceURI, hypercubeitem.Item.Element.Namespace);
                    if (fixedns != logicalhypercubeitem.Namespace)
                    {
                        logicalhypercubeitem.Namespace = fixedns;
                    }
                    //if (logicalhypercubeitem.Name.Contains("RCP")) 
                    //{

                    //}
                    if (hypercubeitem.Children.Count == 0)
                    {
                        var domainref = hypercubeitem.Item.Element.TypedDomainRef;
                        if (!String.IsNullOrEmpty(domainref))
                        {
                            var se_dim_doc = this.Taxonomy.TaxonomyDocuments.FirstOrDefault(i => i.TargetNamespacePrefix == hypercubeitem.Item.Element.Namespace);
                            //TODO
                            var localrelpath = Utilities.Strings.ResolveHref(LogicalModel.TaxonomyEngine.LocalFolder, se_dim_doc.LocalFolder, domainref);

                            //var path = Utilities.Strings.ResolveRelativePath(se_dim_doc.LocalFolder, domainref);
                            //var localrelpath = Utilities.Strings.GetRelativePath(LogicalModel.TaxonomyEngine.LocalFolder, path);
                            var se_domain_doc = this.Taxonomy.FindDocument(localrelpath);

                            var refid = domainref.Substring(domainref.IndexOf("#") + 1);
                            var se_domain_key = se_domain_doc.TargetNamespacePrefix + ":" + refid;
                            var se_domain = Taxonomy.SchemaElementDictionary[se_domain_key];

                            if (se_domain != null)
                            {
                                var logicaldomain = new LogicalModel.Dimensions.DimensionDomain();
                                logicaldomain.Content = se_domain_key;
                                logicaldomain.Name = se_domain.Name;
                                logicaldomain.DimensionItem = logicalhypercubeitem;
                                logicaldomain.LabelID = se_domain.ID;
                                logicalhypercubeitem.Domains.Add(logicaldomain);

                                var logicalmember = new LogicalModel.Dimensions.TypedDimensionMember();
                                logicalmember.Content = logicaldomain.Name + ":*";
                                logicalmember.Name = "*";
                                logicalmember.Domain = logicaldomain;
                                logicalmember.LabelID = "";
                                //logicalmember.IsDefaultMember = domainmember.Item.Element.IsDefaultMember;
                                logicaldomain.DomainMembers.Add(logicalmember);

                                hypercube.DimensionItems.Add(logicalhypercubeitem);

                            }
                            else
                            {
                            }
                        }
                        //var xs = Taxonomy.SchemaElementDictionary[hypercubeitem.Item.]
                    }

                    var completedomains = hypercubeitem.Children.Where(i => i.Children.FirstOrDefault(j => !j.Item.Element.IsDefaultMember) != null).ToList();
                    if (completedomains.Count > 0)
                    {
                        hypercube.DimensionItems.Add(logicalhypercubeitem);
                    }
                    else
                    {

                    }

                    foreach (var domain in completedomains)
                    {
                        var logicaldomain = new LogicalModel.Dimensions.DimensionDomain();
                        logicaldomain.Content = domain.Item.Element.Key;
                        logicaldomain.Name = domain.Item.Element.Name;
                        logicaldomain.DimensionItem = logicalhypercubeitem;
                        logicaldomain.LabelID = domain.Item.LabelID;
                        logicalhypercubeitem.Domains.Add(logicaldomain);

                        var domainmembers = domain.Children;//.Where(i=>!i.Item.Element.IsDefaultMember).ToList();
                        foreach (var domainmember in domainmembers)
                        {

                            var logicalmember = new LogicalModel.Dimensions.DimensionMember();
                            logicalmember.Content = domainmember.Item.Element.Key;
                            logicalmember.Name = domainmember.Item.Element.Name;

                            var memelement = domainmember.Item.Element;
                            var memfixedns = Taxonomy.FindNamespacePrefix(memelement.NamespaceURI, memelement.Namespace);
                            var memberdomain = logicaldomain; 
                            //if (memfixedns != logicaldomain.ID)
                            //{
                            //    LogicalModel.Dimensions.DimensionDomain ntdomain = null;
                            //    if (!nontrivialdomains.ContainsKey(memfixedns)) 
                            //    {
                            //        ntdomain = new LogicalModel.Dimensions.DimensionDomain();
                            //        var domelement = Taxonomy.GetDomain(memfixedns);
                            //        logicaldomain.Content = domelement.Key;
                            //        logicaldomain.Name = domelement.Name;
                            //        logicaldomain.DimensionItem = logicalhypercubeitem;
                            //        nontrivialdomains.Add(memfixedns, ntdomain);
                            //    }
                            //    ntdomain = nontrivialdomains[memfixedns];
                            //    memberdomain = ntdomain;
                            //}

                            logicalmember.Domain = memberdomain;
                            logicalmember.LabelID = domainmember.Item.LabelID;
                            logicalmember.IsDefaultMember = domainmember.Item.Element.IsDefaultMember;
                            logicaldomain.DomainMembers.Add(logicalmember);

                        }
                    }

                }

                logicaltable.HyperCubes.Add(hypercube);



            }
            /*
            foreach (var hc in logicaltable.HyperCubes)
            {
                foreach (var dimitem in hc.DimensionItems)
                {
                    foreach (var domitem in dimitem.Domains)
                    {
                        var nsuri = nsmanager.LookupNamespace(domitem.Namespace);
                        var fixedns = Taxonomy.FindNamespacePrefix(nsuri, domitem.Namespace);
                        if (fixedns != domitem.Namespace)
                        {
                            domitem.Namespace = fixedns;
                        }

                        foreach (var member in domitem.DomainMembers)
                        {
                            var memnsuri = nsmanager.LookupNamespace(member.Domain.Namespace);
                            var memfixedns = Taxonomy.FindNamespacePrefix(nsuri, member.Domain.Namespace);
                            if (memfixedns != member.Domain.Namespace)
                            {
                                domitem.Namespace = memfixedns;
                            }
                        }
                    }

                }
            }
            */
        }

        public void Clear() 
        {
            this.Identifiables.Clear();
            this.Arcs.Clear();
            this.AspectNodeFilters.Clear();
            this.AspectNodes.Clear();
            this.BreakDowns.Clear();
            this.BreakdownTrees.Clear();
            this.DefinitionLinks.Clear();
            this.DefinitionItems.Clear();
            this.DefinitionNodeSubTrees.Clear();
            this.DimensionFilters.Clear();
            this.RuleNodes.Clear();
            this.TableBreakDowns.Clear();
            this.Tables.Clear();
            this.DefinitionRoot = null;
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
