using Model.DefinitionModel;
using Model.DefinitionModel.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Enums;
using XBRLProcessor.Model;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.DefinitionModel.Filter;
using XBRLProcessor.Model.DefinitionModel.Formula;

namespace XBRLProcessor.Mapping
{
    public partial class Mappings
    {
        public Mappings()
        {
            MappingCollection = new List<ClassMapping>() 
            {
                Mappings.Map<Element>("<xs:element>",
                    Mappings.PropertyMap("id", (Element i) => i.ID),
                    Mappings.PropertyMap("name", (Element i) => i.Name),
                    Mappings.PropertyMap("type", (Element i) => i.Type),
                    Mappings.PropertyMap("substitutionGroup", (Element i) => i.SubstitutionGroup),
                    Mappings.PropertyMap("nillable", (Element i) => i.Nullable),
                    Mappings.PropertyMap("model:isDefaultMember", (Element i) => i.IsDefaultMember),
                    Mappings.PropertyMap("xbrli:periodType", (Element i) => i.PeriodType),
                    Mappings.PropertyMap("model:domain", (Element i) => i.Domain),
                    Mappings.PropertyMap("model:hierarchy", (Element i) => i.Hierarchy),
                    Mappings.PropertyMap("model:fromDate", (Element i) => i.FromDate),
                    Mappings.PropertyMap("model:creationDate", (Element i) => i.CreationDate)
                ),

                Mappings.Map<Link>("<xlink>",
                    Mappings.PropertyMap("xlink:type", (Link i) => i.Type),
                    Mappings.PropertyMap("xlink:role", (Link i) => i.Role)
                ),
                
                Mappings.Map<Identifiable>("<identifiable>", 
                    Mappings.PropertyMap("xlink:label", (Identifiable i) => i.LabelID),
                    Mappings.PropertyMap("id", (Identifiable i) => i.ID)
                ),

                Mappings.Map<Arc>("<arc>", 
                    Mappings.PropertyMap("xlink:arcrole", (Arc i) => i.ArcRole),
                    Mappings.PropertyMap("xlink:from", (Arc i) => i.From),
                    Mappings.PropertyMap("xlink:to", (Arc i) => i.To),
                    Mappings.PropertyMap("order", (Arc i) => i.Order)
                ),

                Mappings.Map<Locator>("<link:loc>",
                    Mappings.PropertyMap("xlink:label", (Locator i) => i.LabelID),
                    Mappings.PropertyMap("xlink:href", (Locator i) => i.Href)
                ),

                Mappings.Map<BreakDown>("<table:breakdown>", 
                    Mappings.PropertyMap("parentChildOrder", (BreakDown i) => i.ParentChildOrder)
                ),

                Mappings.Map<Table>("<table:table>",
                    Mappings.PropertyMap("aspectModel", (Table i) => i.AspectModel)
                ),

                Mappings.Map<RuleNode>("<table:ruleNode>", 
                    Mappings.PropertyMap("abstract", (RuleNode i) => i.Abstract),
                    Mappings.PropertyMap("<formula:concept>", (RuleNode i) => i.Concept),
                    Mappings.PropertyMap("<formula:explicitDimension>", (RuleNode i) => i.Dimensions)
                ),

                Mappings.Map<AspectNode>("<table:aspectNode>", 
                    Mappings.PropertyMap("<table:dimensionAspect>", (AspectNode i) => i.DimensionAspect)
                ),

                Mappings.Map<ExplicitDimension>("<formula:explicitDimension>",
                    Mappings.PropertyMap("dimension", (ExplicitDimension i) => i.Dimension),
                    Mappings.PropertyMap("<formula:member>", (ExplicitDimension i) => i.Members)
                ),

                Mappings.Map<Member>("<formula:member>", 
                    Mappings.PropertyMap("<formula:qname>", (Member i) => i.QName)
                ),
                Mappings.Map<Concept>("<formula:concept>", 
                    Mappings.PropertyMap("<formula:qname>", (Concept i) => i.QName)
                ),

                Mappings.Map<QName>("<qname>", 
                    Mappings.PropertyMap(">Content<", (QName i) => i.Content)
                ),

                  Mappings.Map<DimensionAspect>("<table:dimensionAspect>", 
                    Mappings.PropertyMap("includeUnreportedValue", (DimensionAspect i) => i.IncludeUnreportedValue)
                ),


                Mappings.Map<DefinitionNodeSubTreeArc>("<table:definitionNodeSubtreeArc>"
                ),

                Mappings.Map<TableBreakDownArc>("<table:tableBreakdownArc>", 
                    Mappings.PropertyMap("axis", (TableBreakDownArc i) => i.Axis)
                 ),

                Mappings.Map<BreakdownTreeArc>("<table:breakdownTreeArc>"
                ),

                Mappings.Map<DefinitionLink>("<link:definitionLink>", 
                    Mappings.PropertyMap("<link:loc>", (DefinitionLink i) => i.Locators),
                    Mappings.PropertyMap("<link:definitionArc>", (DefinitionLink i) => i.DefinitionArcs)
                 ),

                Mappings.Map<DefinitionArc>("<link:definitionArc>",
                    Mappings.PropertyMap("xbrldt:closed", (DefinitionArc i) => i.Closed),
                    Mappings.PropertyMap("xbrldt:usable", (DefinitionArc i) => i.Usable),
                    Mappings.PropertyMap("xbrldt:targetRole", (DefinitionArc i) => i.TargetRole),
                    Mappings.PropertyMap("xbrldt:contextElement", (DefinitionArc i) => i.ContextElement)
                 ),

                Mappings.Map<XbrlTable>("<gen:link>", 
                    Mappings.PropertyMap("<table:table>", (XbrlTable i) => i.Tables),
                    Mappings.PropertyMap("<table:breakdown>", (XbrlTable i) => i.BreakDowns),
                    Mappings.PropertyMap("<table:breakdownTreeArc>", (XbrlTable i) => i.BreakdownTrees),
                    Mappings.PropertyMap("<table:tableBreakdownArc>", (XbrlTable i) => i.TableBreakDowns),
                    Mappings.PropertyMap("<table:definitionNodeSubtreeArc>", (XbrlTable i) => i.DefinitionNodeSubTrees),
                    Mappings.PropertyMap("<table:ruleNode>", (XbrlTable i) => i.RuleNodes),                    
                    Mappings.PropertyMap("<table:aspectNode>", (XbrlTable i) => i.AspectNodes),                    
                    Mappings.PropertyMap("<df:explicitDimension>", (XbrlTable i) => i.DimensionFilters),                    
                    Mappings.PropertyMap("<link:definitionLink>", (XbrlTable i) => i.DefinitionLinks)                  
                ),

                 Mappings.Map<DimensionFilter>("<DimensionFilter>",
                    Mappings.PropertyMap("<df:dimension>", (DimensionFilter i) => i.Dimension)
                 ),

                 Mappings.Map<DimensionQName>("<df:dimension>", 
                    Mappings.PropertyMap("<df:qname>", (DimensionQName i) => i.QName),
                    Mappings.PropertyMap("<df:qnameExpression>", (DimensionQName i) => i.QNameExpression)
                 ),             

                 Mappings.Map<DimensionMember>("<df:member>", 
                    Mappings.PropertyMap("<df:qname>", (DimensionMember i) => i.QName),
                    Mappings.PropertyMap("<df:qnameExpression>", (DimensionMember i) => i.QNameExpression),
                    Mappings.PropertyMap("<df:linkrole>", (DimensionMember i) => i.LinkRole),
                    Mappings.PropertyMap("<df:arcrole>", (DimensionMember i) => i.ArcRole),
                    Mappings.PropertyMap("<df:axis>", (DimensionMember i) => i.Axis)
                 ),

                 Mappings.Map<ExplicitDimensionFilter>("<df:explicitDimension>",
                    Mappings.PropertyMap("<df:member>", (ExplicitDimensionFilter i) => i.Member)
                 ),

                 Mappings.Map<TypedDimensionFilter>("<df:explicitDimension>",
                    Mappings.PropertyMap("<df:test>", (TypedDimensionFilter i) => i.Test)
                 ),

  
            };

           
        }
        
        public static LogicalModel.Base.Element ToLogical(Element item) 
        {
            var toitem = new LogicalModel.Base.Element();
            toitem.CreationDate = item.CreationDate;
            toitem.Domain = item.Domain;
            toitem.FromDate = item.FromDate;
            toitem.Hierarchy = item.Hierarchy;
            toitem.ID = item.ID;
            toitem.Name = item.Name;
            toitem.Nullable = item.Nullable;
            toitem.IsDefaultMember = item.IsDefaultMember;
            toitem.PeriodType = item.PeriodType;
            toitem.SubstitutionGroup = item.SubstitutionGroup;
            toitem.Type = item.Type;
            toitem.Namespace = item.Namespace;
            return toitem;
        }
        
    }
}
