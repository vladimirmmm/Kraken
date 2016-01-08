using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Validation
{
    public class ValidationRuleResult
    {
        public bool IsOk = true;
        public string ID { get; set; }
        public string HasAllFind { get; set; }
        public FactGroup FactGroup = null;
        private List<SimpleValidationParameter> _Parameters = new List<SimpleValidationParameter>();
        public List<SimpleValidationParameter> Parameters { get { return _Parameters; } set { _Parameters = value; } }
        public string Message = "";

        public string GetDetails()
        {
            var sb = new StringBuilder();
            //sb.AppendLine(Message);
            sb.Append("    ");
            foreach (var p in Parameters)
            {
                sb.AppendFormat("{0}({1}), ", p.Name, p.Value);

            }
            sb.AppendLine();
            sb.Append("    ");

            foreach (var p in Parameters)
            {
                sb.AppendFormat("{0}[", p.Name);
                foreach (var item in p.Facts)
                {
                    sb.Append(item + ", ");
                }
                sb.Append("], ");

            }
            sb.AppendLine();

            return sb.ToString();
        }

        public static List<ValidationRuleResult> GetResultsForConceptOnly(ValidationRule rule)
        {
            var results = new List<ValidationRuleResult>();
            //return results;
            var NonGeneralParameters = rule.Parameters.Where(i => !i.IsGeneral).ToList();
            if (NonGeneralParameters.Count == 1)
            {
                var theparameter = NonGeneralParameters.FirstOrDefault();
                var factgroup = theparameter.FactGroups.Values.FirstOrDefault();

                var firstfactkey = theparameter.FactGroups.FirstOrDefault().Key;
                if (firstfactkey.IndexOf("[") < 0 && !string.IsNullOrEmpty(firstfactkey))
                {
                    factgroup.LoadObjects();
                    var factsofconcept = rule.Taxonomy.Facts.Where(i => i.Key.StartsWith(firstfactkey)).ToList();
                    foreach (var instancefactkvp in factsofconcept)
                    {
                        var instancefact = new FactBase();
                        instancefact.SetFromString(instancefactkvp.Key);
                        if (!factgroup.Not(instancefact))
                        {
                            var dynamicresult = new ValidationRuleResult();

                            dynamicresult.ID = rule.ID;
                            dynamicresult.Parameters.AddRange(rule.Parameters.Select(i =>
                            {

                                var sp = new SimpleValidationParameter();
                                sp.Name = i.Name;
                                sp.BindAsSequence = i.BindAsSequence;
                                return sp;
                            }));
                            dynamicresult.Message = rule.DisplayText;
                            foreach (var p in dynamicresult.Parameters)
                            {
                                var rp = rule.Parameters.FirstOrDefault(i => i.Name == p.Name);
                                var factstring = instancefact.GetFactString();
                                var factkey = instancefact.GetFactKey();
                                p.Facts.Clear();
                                p.Facts.Add(factstring);
                                //TODO

                                if (rule.Taxonomy.Facts.ContainsKey(factkey))
                                {
                                    p.CellsOfFacts.Add(factkey, new List<string>());
                                    var cells = rule.Taxonomy.Facts[factkey];
                                    foreach (var cell in cells)
                                    {
                                        p.CellsOfFacts[factkey].AddRange(cells);
                                    }
                                }
                                else
                                {
                                }


                            }

                            var fg = new FactGroup();
                            if (instancefact.Dimensions.Count == 0)
                            {
                                instancefact.SetFromString(instancefact.FactString);
                            }
                            fg.Dimensions.AddRange(instancefact.Dimensions);
                            fg.Concept = factgroup.Concept;
                            fg.AddFact(instancefact);
                            dynamicresult.FactGroup = fg;
                            results.Add(dynamicresult);
                        }
                        else
                        {

                        }
                    }
                   
                }
            }
            return results;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", this.GetType().Name, this.ID);
        }
    }

}
