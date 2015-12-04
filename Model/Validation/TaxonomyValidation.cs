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
        public TaxonomyValidation(Taxonomy taxonomy) 
        {
            this.taxonomy = taxonomy;
            ConceptHints.Add("bi", Utilities.Strings.ArrayToString(boolvalues));
        }
        private Dictionary<string, string> ConceptHints = new Dictionary<string, string>();

        public string ValidateByConcept(InstanceFact fact, StringBuilder sb)
        {
            var concept = fact.Concept;
            var conceptkey = fact.Concept.Content.ToLower();

            var conceptstr=fact.Concept.Name.ToLower();
            var cells = Utilities.Strings.ArrayToString(fact.Cells.ToArray());

            if (conceptstr.StartsWith("bi"))
            {
          
                if (!fact.Value.In(boolvalues)) 
                {
                    AddMessage(sb, String.Format("Value \"{0}\" of Fact {1} - {2} Cell: {3} is not correct. Hint: {{{4}}}", 
                        fact.Value, fact.Concept, fact.ContextID, cells, ConceptHints["bi"]));
                }
            }
            if (conceptstr.StartsWith("ei"))
            {
                var conceptelement = taxonomy.Concepts.ContainsKey(concept.Content)?taxonomy.Concepts[concept.Content]:null;
                if (conceptelement!=null)
                {
                    var hierarchy = taxonomy.Hierarchies.Where(i => i.Item.Role == conceptelement.HierarchyRole)
                        .FirstOrDefault(i=> i.Item.Content==conceptelement.Domain.Content );
                    if (hierarchy != null) 
                    {
           
                        if (hierarchy.FirstOrDefault(i => i.Item.Content == fact.Value) == null)
                        {
                            if (!ConceptHints.ContainsKey(conceptkey))
                            {
                                var possibilities = hierarchy.Children.Select(i => String.Format("{0} - {1}", i.Item.Content, i.Item.LabelContent)).ToArray();
                                var possibilitystr = Utilities.Strings.ArrayToString(possibilities, ",\r\n" + tabstr);
                                possibilitystr = "";
                                ConceptHints.Add(conceptkey,tabstr+ "\r\n"+possibilitystr);
                            }
                            AddMessage(sb, String.Format("Value \"{0}\" of Fact {1} - {2} Cell: {3} is not correct. Hint: {{{4}}}", fact.Value, fact.Concept, fact.ContextID, cells, ConceptHints[conceptkey]));
                        }

                    }
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
