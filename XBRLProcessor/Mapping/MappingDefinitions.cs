using Model.DefinitionModel;
using Model.DefinitionModel.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model;
using XBRLProcessor.Model.Base;

namespace XBRLProcessor.Mapping
{
    public partial class Mappings
    {
        public Mappings()
        {
            MappingCollection = new List<ClassMapping>() 
            {
                Mappings.Map<Element>("<xs:element>", new List<PropertyMapping<Element>>(){
                    Mappings.Map("id", (Element i) => i.ID),
                    Mappings.Map("name", (Element i) => i.Name),
                    Mappings.Map("type", (Element i) => i.Type),
                    Mappings.Map("substitutionGroup", (Element i) => i.SubstitutionGroup),
                    Mappings.Map("nillable", (Element i) => i.Nullable),
                    Mappings.Map("xbrli:periodType", (Element i) => i.PeriodType),
                    Mappings.Map("model:domain", (Element i) => i.Domain),
                    Mappings.Map("model:hierarchy", (Element i) => i.Hierarchy),
                    Mappings.Map("model:fromDate", (Element i) => i.FromDate),
                    Mappings.Map("model:creationDate", (Element i) => i.CreationDate),
                }),

                Mappings.Map<Link>("<xlink>", new List<PropertyMapping<Link>>(){
                    Mappings.Map("xlink:type", (Link i) => i.Type),
                    Mappings.Map("xlink:role", (Link i) => i.Role),
                }),
                
                Mappings.Map<Identifiable>("<identifiable>", new List<PropertyMapping<Identifiable>>(){
                    Mappings.Map("xlink:label", (Identifiable i) => i.LabelID),
                    Mappings.Map("id", (Identifiable i) => i.ID),
                }),

                Mappings.Map<Arc>("<arc>", new List<PropertyMapping<Arc>>(){
                    Mappings.Map("xlink:arcrole", (Arc i) => i.ArcRole),
                    Mappings.Map("xlink:from", (Arc i) => i.From),
                    Mappings.Map("xlink:to", (Arc i) => i.To),
                    Mappings.Map("order", (Arc i) => i.Order),
                }),

                Mappings.Map<Locator>("<link:loc>", new List<PropertyMapping<Locator>>(){
                    Mappings.Map("xlink:label", (Locator i) => i.LabelID),
                    Mappings.Map("xlink:href", (Locator i) => i.Href),
                }),

                Mappings.Map<BreakDown>("<table:breakdown>", new List<PropertyMapping<BreakDown>>(){
                    Mappings.Map("parentChildOrder", (BreakDown i) => i.ParentChildOrder),
                }),

                Mappings.Map<Table>("<table:table>", new List<PropertyMapping<Table>>(){
                    Mappings.Map("aspectModel", (Table i) => i.AspectModel),
                }),

                Mappings.Map<RuleNode>("<table:ruleNode>", new List<PropertyMapping<RuleNode>>(){
                    Mappings.Map("abstract", (RuleNode i) => i.Abstract),
                    Mappings.Map("<formula:concept>", (RuleNode i) => i.Concept),
                    Mappings.Map("<formula:explicitDimension>", (RuleNode i) => i.Dimensions),
                }),

                Mappings.Map<ExplicitDimension>("<formula:explicitDimension>", new List<PropertyMapping<ExplicitDimension>>(){
                    Mappings.Map("dimension", (ExplicitDimension i) => i.Dimension),
                    Mappings.Map("<formula:member>", (ExplicitDimension i) => i.Members),
                }),

                Mappings.Map<Member>("<formula:member>", new List<PropertyMapping<Member>>(){
                    Mappings.Map("<formula:qname>", (Member i) => i.QName),
                }),
                Mappings.Map<Concept>("<formula:concept>", new List<PropertyMapping<Concept>>(){
                    Mappings.Map("<formula:qname>", (Concept i) => i.QName),
                }),

                Mappings.Map<QName>("<formula:qname>", new List<PropertyMapping<QName>>(){
                    Mappings.Map(">Content<", (QName i) => i.Content),
                }),


                Mappings.Map<DefinitionNodeSubTreeArc>("<table:definitionNodeSubtreeArc>", new List<PropertyMapping<DefinitionNodeSubTreeArc>>(){
                }),

                Mappings.Map<TableBreakDownArc>("<table:tableBreakdownArc>", new List<PropertyMapping<TableBreakDownArc>>(){
                    Mappings.Map("axis", (TableBreakDownArc i) => i.Axis),
                 }),

                Mappings.Map<BreakdownTreeArc>("<table:breakdownTreeArc>", new List<PropertyMapping<BreakdownTreeArc>>(){
                }),

                Mappings.Map<DefinitionLink>("<link:definitionLink>", new List<PropertyMapping<DefinitionLink>>(){
                    Mappings.Map("<link:loc>", (DefinitionLink i) => i.Locators),
                    Mappings.Map("<link:definitionArc>", (DefinitionLink i) => i.DefinitionArcs),
                 }),

                Mappings.Map<DefinitionArc>("<link:definitionArc>", new List<PropertyMapping<DefinitionArc>>(){
                    Mappings.Map("xbrldt:closed", (DefinitionArc i) => i.Closed),
                    Mappings.Map("xbrldt:usable", (DefinitionArc i) => i.Usable),
                    Mappings.Map("xbrldt:targetRole", (DefinitionArc i) => i.TargetRole),
                    Mappings.Map("xbrldt:contextElement", (DefinitionArc i) => i.ContextElement),
                 }),

                Mappings.Map<XbrlTable>("<gen:link>", new List<PropertyMapping<XbrlTable>>(){
                    Mappings.Map("<table:table>", (XbrlTable i) => i.Tables),
                    Mappings.Map("<table:breakdown>", (XbrlTable i) => i.BreakDowns),
                    Mappings.Map("<table:breakdownTreeArc>", (XbrlTable i) => i.BreakdownTrees),
                    Mappings.Map("<table:tableBreakdownArc>", (XbrlTable i) => i.TableBreakDowns),
                    Mappings.Map("<table:definitionNodeSubtreeArc>", (XbrlTable i) => i.DefinitionNodeSubTrees),
                    Mappings.Map("<table:ruleNode>", (XbrlTable i) => i.RuleNodes),                    
                    Mappings.Map("<link:definitionLink>", (XbrlTable i) => i.DefinitionLinks),                    
                }),

  
            };

            /*
             MappingCollection = new List<ClassMapping>() 
            {
                Mappings.Map<Xlink>("<table:definitionNodeSubtreeArc>", new List<PropertyMapping<Xlink>>(){
                    Mappings.Map("xlink:type", (Xlink i) => i.Type),
                }),
                
                Mappings.Map<Identifiable>("<table:breakdown>", new List<PropertyMapping<Identifiable>>(){
                    Mappings.Map("xlink:label", (Identifiable i) => i.LabelID),
                    Mappings.Map("id", (Identifiable i) => i.ID),
                }),

                Mappings.Map<Arc>("<table:definitionNodeSubtreeArc>", new List<PropertyMapping<Arc>>(){
                    Mappings.Map("xlink:arcrole", (Arc i) => i.ArcRole),
                    Mappings.Map("xlink:from", (Arc i) => i.From),
                    Mappings.Map("xlink:to", (Arc i) => i.To),
                    Mappings.Map("order", (Arc i) => i.Order),
                }),

                Mappings.Map<BreakDown>("<table:breakdown>", new List<PropertyMapping<BreakDown>>(){
                    Mappings.Map("xlink:type", (BreakDown i) => i.Type),
                    Mappings.Map("xlink:label", (BreakDown i) => i.LabelID),
                    Mappings.Map("parentChildOrder", (BreakDown i) => i.ParentChildOrder),
                    Mappings.Map("id", (BreakDown i) => i.ID),
                }),

                Mappings.Map<Table>("<table:table>", new List<PropertyMapping<Table>>(){
                    Mappings.Map("xlink:type", (Table i) => i.Type),
                    Mappings.Map("xlink:label", (Table i) => i.LabelID),
                    Mappings.Map("aspectModel", (Table i) => i.AspectModel),
                    Mappings.Map("id", (Table i) => i.ID),
                }),

                Mappings.Map<RuleNode>("<table:ruleNode>", new List<PropertyMapping<RuleNode>>(){
                    Mappings.Map("xlink:type", (RuleNode i) => i.Type),
                    Mappings.Map("xlink:label", (RuleNode i) => i.LabelID),
                    Mappings.Map("abstract", (RuleNode i) => i.Abstract),
                    Mappings.Map("id", (RuleNode i) => i.ID),
                    Mappings.Map("<formula:concept>", (RuleNode i) => i.Concept),
                    Mappings.Map("<formula:explicitDimension>", (RuleNode i) => i.Dimensions),
                }),

                Mappings.Map<Formula.ExplicitDimension>("<formula:explicitDimension>", new List<PropertyMapping<Formula.ExplicitDimension>>(){
                    Mappings.Map("dimension", (Formula.ExplicitDimension i) => i.Dimension),
                    Mappings.Map("<formula:member>", (Formula.ExplicitDimension i) => i.Members),
                }),

                Mappings.Map<Formula.Member>("<formula:member>", new List<PropertyMapping<Formula.Member>>(){
                    Mappings.Map("<formula:qname>", (Formula.Member i) => i.QName),
                }),
                Mappings.Map<Formula.Concept>("<formula:concept>", new List<PropertyMapping<Formula.Concept>>(){
                    Mappings.Map("<formula:qname>", (Formula.Concept i) => i.QName),
                }),

                Mappings.Map<Formula.QName>("<formula:qname>", new List<PropertyMapping<Formula.QName>>(){
                    Mappings.Map(">Content<", (Formula.QName i) => i.Content),
                }),


                Mappings.Map<DefinitionNodeSubTree>("<table:definitionNodeSubtreeArc>", new List<PropertyMapping<DefinitionNodeSubTree>>(){
                    Mappings.Map("xlink:type", (DefinitionNodeSubTree i) => i.Type),
                    Mappings.Map("xlink:arcrole", (DefinitionNodeSubTree i) => i.ArcRole),
                    Mappings.Map("xlink:from", (DefinitionNodeSubTree i) => i.From),
                    Mappings.Map("xlink:to", (DefinitionNodeSubTree i) => i.To),
                    Mappings.Map("order", (DefinitionNodeSubTree i) => i.Order),
                }),

                Mappings.Map<TableBreakDown>("<table:tableBreakdownArc>", new List<PropertyMapping<TableBreakDown>>(){
                    Mappings.Map("xlink:type", (TableBreakDown i) => i.Type),
                    Mappings.Map("xlink:arcrole", (TableBreakDown i) => i.ArcRole),
                    Mappings.Map("xlink:from", (TableBreakDown i) => i.From),
                    Mappings.Map("xlink:to", (TableBreakDown i) => i.To),
                    Mappings.Map("order", (TableBreakDown i) => i.Order),
                    Mappings.Map("axis", (TableBreakDown i) => i.Axis),
                 }),

                Mappings.Map<BreakdownTree>("<table:breakdownTreeArc>", new List<PropertyMapping<BreakdownTree>>(){
                    Mappings.Map("xlink:type", (BreakdownTree i) => i.Type),
                    Mappings.Map("xlink:arcrole", (BreakdownTree i) => i.ArcRole),
                    Mappings.Map("xlink:from", (BreakdownTree i) => i.From),
                    Mappings.Map("xlink:to", (BreakdownTree i) => i.To),
                    Mappings.Map("order", (BreakdownTree i) => i.Order),
                }),

                Mappings.Map<Model.Logical.Table>("<gen:link>", new List<PropertyMapping<Model.Logical.Table>>(){
                    Mappings.Map("xlink:type", (Model.Logical.Table i) => i.Type),
                    Mappings.Map("xlink:label", (Model.Logical.Table i) => i.LabelID),
                    Mappings.Map("id", (Model.Logical.Table i) => i.ID),
                    Mappings.Map("<table:table>", (Model.Logical.Table i) => i.Tables),
                    Mappings.Map("<table:breakdown>", (Model.Logical.Table i) => i.BreakDowns),
                    Mappings.Map("<table:breakdownTreeArc>", (Model.Logical.Table i) => i.BreakdownTrees),
                    Mappings.Map("<table:tableBreakdownArc>", (Model.Logical.Table i) => i.TableBreakDowns),
                    Mappings.Map("<table:definitionNodeSubtreeArc>", (Model.Logical.Table i) => i.DefinitionNodeSubTrees),
                    Mappings.Map("<table:ruleNode>", (Model.Logical.Table i) => i.RuleNodes),
                    
                }),

  
            };
             */
        }

    }
}
