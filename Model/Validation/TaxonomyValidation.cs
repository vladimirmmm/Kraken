using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel.Validation
{
    public class TaxonomyValidation
    {
        private Taxonomy taxonomy=null;
        private string[] boolvalues = new string[]{"true", "false", "0", "1"};
        private string tabstr = "     ";

        private List<SimpleValidationRule> TaxonomyRules = new List<SimpleValidationRule>();

        public TaxonomyValidation(Taxonomy taxonomy) 
        {
            this.taxonomy = taxonomy;
            ConceptHints.Add("bi", Utilities.Strings.ArrayToString(boolvalues));
            TaxonomyRules = GetValidationRules();
        }
        private Dictionary<string, string> ConceptHints = new Dictionary<string, string>();

        public List<SimpleValidationRule> GetValidationRules() 
        {           
            var rules = new List<SimpleValidationRule>();
            var concepts = taxonomy.Concepts.Select(i => i.Value).ToList();
            foreach (var conceptelement in concepts)
            {
                var conceptrule = new ConceptValidationRule();
                conceptrule.ID = "v_tax_" + conceptelement.Content;


                if (conceptelement.Content.Contains(":ei"))
                {
                    var hierarchies = taxonomy.Hierarchies.Where(i => i.Item.Role == conceptelement.HierarchyRole).ToList();
                    if (hierarchies.Count==0) 
                    { 
                        hierarchies = taxonomy.Hierarchies;
                    }
                    var hierarchy = hierarchies.FirstOrDefault(i => i.Item.Content == conceptelement.Domain.Content);
                    if (hierarchy != null)
                    {
                        //if (conceptelement.Content.Contains("218")) 
                        //{
                        //}
                        var possibilities = hierarchy.Where(i=>i!=hierarchy).Select(i => String.Format("{0} [{1}]", i.Item.Content, i.Item.LabelContent)).ToArray();
                        var possibilitystr = Utilities.Strings.ArrayToString(possibilities, ", ");
                        conceptrule.DisplayText = "Fact value for concept " + conceptelement.Content + " based on " + conceptelement.HierarchyRole + " should be " + possibilitystr;
                        conceptrule.IsOk = (fact) => hierarchy.FirstOrDefault(i => i.Item.Content == fact.Value) != null;
                    }
                    else 
                    {
                        var domainstr = taxonomy.GetDomainID(conceptelement.Domain);
              
                        var domainmembers = taxonomy.SchemaElements.Where(i => i.Namespace == domainstr && i.Type == "nonnum:domainItemType").Select(i=>new LogicalModel.Base.QualifiedName( i.Namespace+":"+i.Name)).ToList();
                        var possibilities = domainmembers.Select(i => String.Format("{0} [{1}]",i.Content, i.Name)).ToArray();
                        var possibilitystr = Utilities.Strings.ArrayToString(possibilities, ", ");
                        conceptrule.DisplayText = "Fact value for concept " + conceptelement.Content + "  should be " + possibilitystr;
                        conceptrule.IsOk = (fact) => domainmembers.FirstOrDefault(i => i.Content == fact.Value) != null;
               
                    }

                    rules.Add(conceptrule);

                }
                if (conceptelement.Content.Contains(":bi"))
                {
                    conceptrule.DisplayText = "Fact value with concept " + conceptelement.Content + "  should be " + ConceptHints["bi"];
                    conceptrule.IsOk = (fact) => {
                        return fact.Value.In(boolvalues);

                    };
                    rules.Add(conceptrule);

                }
                if (conceptelement.Content.Contains(":di"))
                {
                    conceptrule.DisplayText = "Fact value for concept " + conceptelement.Content + " should be a date formatted, as yyyy-mm-dd ";
                    conceptrule.IsOk = (fact) => Utilities.Converters.IsDate(fact.Value, "yyyy-MM-dd");
                    rules.Add(conceptrule);

                }
                conceptrule.Concept = conceptelement.Content;

            }
            var typedelements = taxonomy.SchemaElements.Where(i => i.FileName == "typ.xsd");
            foreach (var typedelement in typedelements) 
            {
                var fullname = typedelement.Namespace + ":" + typedelement.Name;

                var typedrule = new TypedDimensionValidationRule();
                typedrule.ID = "v_tax_" + fullname;
                if (typedelement.Type == "xs:date") 
                {
                    typedrule.DisplayText = "Typed Dimension Member for Domain " + fullname + " should be a date";
                    typedrule.IsOk = (s) => Utilities.Converters.IsDate(s, "yyyy-MM-dd");
                    rules.Add(typedrule);

                }
                if (typedelement.Type == "xs:integer") 
                {
                    typedrule.DisplayText = "Typed Dimension Member for Domain " + fullname + " should be an integer";
                    typedrule.IsOk = (s) => Utilities.Converters.IsInteger(s);
                    rules.Add(typedrule);

                }
                typedrule.TypedDimension = fullname;

            }
            return rules;
        }
        
        public string ValidateByTypedDimension(InstanceFact fact, List<ValidationRuleResult> results, StringBuilder sb)
        {

            var typeddimensions = fact.Dimensions.Where(i => i.IsTyped).ToList();
            foreach (var typeddimension in typeddimensions) 
            {
                var member = typeddimension.DomainMember;
                var fulldomain = typeddimension.Domain;
                var ruleid = "v_tax_" + fulldomain;

                var rule = TaxonomyRules.FirstOrDefault(i => i.ID == ruleid) as TypedDimensionValidationRule;
                if (rule != null)
                {
                    if (!rule.IsOk(member))
                    {
                        var result = new ValidationRuleResult();
                        result.ID = rule.ID;
                        var p = new SimpleValidationParameter();
                        p.Name = String.Format("{0}, context {1}", typeddimension.Domain, fact.ContextID);
                        //p.Facts.Add(fact.FactString);
                        p.FactIDs.Add(String.Format("I:{0}", fact.IX));
                        p.Value = member;
                 
                        result.Parameters.Add(p);
                        results.Add(result);

                    }
                }
            }
            

            return sb.ToString();
        }
        
        public string ValidateByConcept(InstanceFact fact, List<ValidationRuleResult> results, StringBuilder sb)
        {
            var concept = fact.Concept;
            var conceptkey = fact.Concept.Content.ToLower();

            var conceptstr=fact.Concept.Name.ToLower();
            var cells = Utilities.Strings.ArrayToString(fact.Cells.ToArray());
            var ruleid= "v_tax_"+fact.Concept.Content;
            var rule = TaxonomyRules.FirstOrDefault(i => i.ID == ruleid) as ConceptValidationRule;
            if (rule != null) 
            {
                if (!rule.IsOk(fact))
                {
                    var result = new ValidationRuleResult();
                    result.ID = rule.ID;
                    var p = new SimpleValidationParameter();
                    p.Name = "a";
                    p.Value = fact.Value;
                    //p.Facts.Add(fact.FactString);
                    p.FactIDs.Add(String.Format("I:{0}", fact.IX));

                    
                    var cellidlist = new List<String>();
                    foreach (var cell in fact.Cells) 
                    {
                        cellidlist.Add(TaxonomyEngine.CurrentEngine.CurrentInstance.GetDynamicCellID(cell, fact));
                    }
                    p.Cells.Add(cellidlist);
                    result.Parameters.Add(p);
                    results.Add(result);
                }
            }
            
            return sb.ToString();
        }

        public void AddMessage(StringBuilder sb, string message)
        {
            sb.AppendLine(message);
            //Logger.WriteLine(message);
        }
    }
}
