using LogicalModel;
using Model.DefinitionModel;
using Model.DefinitionModel.Formula;
using Model.InstanceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Enums;
using XBRLProcessor.Model;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.DefinitionModel;
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
                    Mappings.PropertyMap("model:creationDate", (Element i) => i.CreationDate),
                    Mappings.PropertyMap("xbrldt:typedDomainRef", (Element i) => i.TypedDomainRef)
                ),

                Mappings.Map<Link>("<xlink>",
                    Mappings.PropertyMap("xlink:type", (Link i) => i.Type),
                    Mappings.PropertyMap("xlink:role", (Link i) => i.Role),
                    Mappings.PropertyMap("xlink:href", (Link i) => i.Href)
                ),
                
                Mappings.Map<XbrlIdentifiable>("<identifiable>", 
                    Mappings.PropertyMap("xlink:label", (XbrlIdentifiable i) => i.LabelID),
                    Mappings.PropertyMap("id", (XbrlIdentifiable i) => i.ID)
                ),
                Mappings.Map<LogicalModel.Base.Identifiable>("<identifiable>", 
                    Mappings.PropertyMap("id", (LogicalModel.Base.Identifiable i) => i.ID)
                ),
                Mappings.Map<Arc>("<arc>", 
                    Mappings.PropertyMap("xlink:arcrole", (Arc i) => i.ArcRole),
                    Mappings.PropertyMap("xlink:from", (Arc i) => i.From),
                    Mappings.PropertyMap("xlink:to", (Arc i) => i.To),
                    Mappings.PropertyMap("order", (Arc i) => i.Order)
                ),

                Mappings.Map<Locator>("<link:loc>",
                    Mappings.PropertyMap("xlink:label", (Locator i) => i.LabelID)
                ),

                Mappings.Map<BreakDown>("<table:breakdown>", 
                    Mappings.PropertyMap("parentChildOrder", (BreakDown i) => i.ParentChildOrder)
                ),

                Mappings.Map<TableNode>("<table:table>",
                    Mappings.PropertyMap("aspectModel", (TableNode i) => i.AspectModel)
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
                     Mappings.PropertyMap("<formula:qname>", (Concept i) => i.Content)
                ),

                Mappings.Map<QName>("<qname>", 
                    Mappings.PropertyMap("@content", (QName i) => i.Content)
                ),
                Mappings.Map<LogicalModel.Base.QualifiedName>("<QualifiedName>", 
                    Mappings.PropertyMap("@content", (LogicalModel.Base.QualifiedName i) => i.Content)
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
                 Mappings.Map<VariableArc>("<variable:variableArc>",
                    Mappings.PropertyMap("name", (VariableArc i) => i.Name)
                 ),
                 Mappings.Map<Filter>("<bf:orFilter>"),

                 Mappings.Map<Filter>("<bf:andFilter>"),

                 Mappings.Map<ValueAssertion>("<va:valueAssertion>",
                    Mappings.PropertyMap("test", (ValueAssertion i) => i.Test),
                    Mappings.PropertyMap("aspectModel", (ValueAssertion i) => i.AspectModel),
                    Mappings.PropertyMap("implicitFiltering", (ValueAssertion i) => i.ImplicitFiltering)
                 ),
                 Mappings.Map<VariableSetFilterArc>("<variable:variableSetFilterArc>",
                    Mappings.PropertyMap("complement", (VariableSetFilterArc i) => i.Complement)
                 ),
                 Mappings.Map<VariableFilterArc>("<variable:variableFilterArc>",
                    Mappings.PropertyMap("complement", (VariableFilterArc i) => i.Complement),
                    Mappings.PropertyMap("cover", (VariableFilterArc i) => i.Cover)
                 ),

                 Mappings.Map<ConceptNameFilter>("<cf:conceptName>",
                    Mappings.PropertyMap("<cf:concept>", (ConceptNameFilter i) => i.Concept)
                 ),

                   Mappings.Map<AspectCoverFilter>("<acf:aspectCover>",
                    Mappings.PropertyMap("/@content", (AspectCoverFilter i) => i.Aspect)
                 ),

                  Mappings.Map<ConceptQName>("<cf:concept>",
                    Mappings.PropertyMap("<df:qname>", (ConceptQName i) => i.QName)
                 ),

                 Mappings.Map<ExplicitDimensionFilter>("<df:explicitDimension>",
                    Mappings.PropertyMap("<df:member>", (ExplicitDimensionFilter i) => i.Member)
                 ),

                  Mappings.Map<TypedDimensionFilter>("<df:typedDimension>"),

                  Mappings.Map<FactVariable>("<variable:factVariable>"),

                  Mappings.Map<Variable>("<variable>",
                    Mappings.PropertyMap("bindAsSequence", (Variable i) => i.BindAsSequence),
                    Mappings.PropertyMap("fallbackValue", (Variable i) => i.FallbackValue)
                 ),

                 Mappings.Map<TypedDimensionFilter>("<df:explicitDimension>",
                    Mappings.PropertyMap("<df:test>", (TypedDimensionFilter i) => i.Test)
                 ),
                 //Logical
                 Mappings.Map<Dimension>("<xbrldi:explicitMember>",
                    Mappings.PropertyMap("dimension", (Dimension i) => i.DimensionItem),
                    Mappings.PropertyMap("@content", (Dimension i) => i.DomainMember)
                 ),
                Mappings.Map<Dimension>("<xbrldi:typedMember>",
                    Mappings.PropertyMap("#true", (Dimension i) => i.IsTyped),
                    Mappings.PropertyMap("dimension", (Dimension i) => i.DimensionItem),
                    Mappings.PropertyMap("/@name", (Dimension i) => i.Domain),
                    Mappings.PropertyMap("/@content", (Dimension i) => i.DomainMember)
                 ),
                Mappings.Map<LogicalModel.Base.QualifiedName>("<qname>", 
                    Mappings.PropertyMap("@content", (LogicalModel.Base.QualifiedName i) => i.Content)
                ),
                 //Instance
                 Mappings.Map<Unit>("<xbrli:unit>",
                    Mappings.PropertyMap("<xbrli:measure>", (Unit i) => i.Measure)
                 ),
                 Mappings.Map<Entity>("<xbrli:entity>",
                    Mappings.PropertyMap("scheme", (Entity i) => i.Scheme)
                 ),
                 Mappings.Map<Period>("<xbrli:period>",
                    Mappings.PropertyMap("<xbrli:instant>", (Period i) => i.Instant),
                    Mappings.PropertyMap("<xbrli:forever>", (Period i) => i.Forever),
                    Mappings.PropertyMap("<xbrli:startdate>", (Period i) => i.StartDate),
                    Mappings.PropertyMap("<xbrli:enddate>", (Period i) => i.EndDate)
                 ),
                 Mappings.Map<Scenario>("<xbrli:scenario>",
                    Mappings.PropertyMap("<xbrldi:explicitMember>", (Scenario i) => i.Dimensions),
                    Mappings.PropertyMap("<xbrldi:typedMember>", (Scenario i) => i.Dimensions)
                 ),
                 Mappings.Map<FilingIndicator>("<find:filingIndicator>",
                    Mappings.PropertyMap("contextRef", (FilingIndicator i) => i.ContextRef),
                    Mappings.PropertyMap("@content", (FilingIndicator i) => i.Value)
                 ),
                 Mappings.Map<Context>("<xbrli:context>",
                    Mappings.PropertyMap("<xbrli:entity>", (Context i) => i.Entity),
                    Mappings.PropertyMap("<xbrli:period>", (Context i) => i.Period),
                    Mappings.PropertyMap("<xbrli:scenario>", (Context i) => i.Scenario)
                 ),

                 Mappings.Map<XbrlFact>("<XbrlFact>",
                    Mappings.PropertyMap("@name", (XbrlFact i) => i.Concept),
                    Mappings.PropertyMap("decimals", (XbrlFact i) => i.Decimals),
                    Mappings.PropertyMap("contextRef", (XbrlFact i) => i.ContextRef),
                    Mappings.PropertyMap("unitRef", (XbrlFact i) => i.UnitRef),
                    Mappings.PropertyMap("@content", (XbrlFact i) => i.Value)
                 ),
                 Mappings.Map<Link>("<link:schemaRef>",  
                    Mappings.PropertyMap("xlink:type", (Link i) => i.Type),
                    Mappings.PropertyMap("xlink:role", (Link i) => i.Role),
                    Mappings.PropertyMap("xlink:href", (Link i) => i.Href)
                 ),

                 Mappings.Map<XbrlInstance>("<xbrli:xbrl>",
                    Mappings.PropertyMap("<link:schemaRef>", (XbrlInstance i) => i.SchemaRef),
                    Mappings.PropertyMap("<xbrli:context>", (XbrlInstance i) => i.Contexts),
                    Mappings.PropertyMap("<xbrli:unit>", (XbrlInstance i) => i.Units)
                 ),
                 //End Instance
                //hier
                  Mappings.Map<Hier>("<hier>",
                    Mappings.PropertyMap("<link:definitionLink>", (Hier i) => i.DefinitionLinks)
                 ),
                 //validation
                 Mappings.Map<XbrlValidation>("<validation>",
                    Mappings.PropertyMap("<variable:variableArc>", (XbrlValidation i) => i.VariableArcs),
                    Mappings.PropertyMap("<variable:variableFilterArc>", (XbrlValidation i) => i.VariableFilterArcs),
                    Mappings.PropertyMap("<variable:variableSetFilterArc>", (XbrlValidation i) => i.VariableSetFilterArcs),
                    Mappings.PropertyMap("<df:explicitDimension>", (XbrlValidation i) => i.DimensionFilters),
                    Mappings.PropertyMap("<df:typedDimension>", (XbrlValidation i) => i.DimensionFilters),
                    Mappings.PropertyMap("<cf:conceptName>", (XbrlValidation i) => i.ConceptFilters),
                    Mappings.PropertyMap("<acf:aspectCover>", (XbrlValidation i) => i.AspectFilters),
                    Mappings.PropertyMap("<va:valueAssertion>", (XbrlValidation i) => i.ValueAssertion),
                    Mappings.PropertyMap("<bf:orFilter>", (XbrlValidation i) => i.Filters),
                    Mappings.PropertyMap("<bf:andFilter>", (XbrlValidation i) => i.Filters),
                    Mappings.PropertyMap("<variable:factVariable>", (XbrlValidation i) => i.FactVariables)
                    
                 ),
                 //end validation
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
            toitem.TypedDomainRef = item.TypedDomainRef;
            return toitem;
        }

        public static LogicalModel.Concept ToLogical(Concept item)
        {
            var toitem = new LogicalModel.Concept();
            toitem.Content = item.Content;
            return toitem;
        }

        public static LogicalModel.Base.QualifiedItem ToQualifiedItem(Locator item)
        {
            var toitem = new LogicalModel.Base.QualifiedItem();
            toitem.Namespace = item.Namespace;
            toitem.Name = item.Element.Name;
            toitem.LabelID = item.LabelID;
            toitem.ID = item.ID;
            toitem.Role = item.Role;
            toitem.Label = XbrlEngine.CurrentEngine.CurrentTaxonomy.FindLabel(LogicalModel.Label.GetKey(toitem.Namespace, toitem.ID));
            return toitem;
        }
        
        
    }
}
