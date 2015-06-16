module Control
{
    export class InstanceContainer {
        public Instance: Model.Instance = null;
        public Taxonomy: Model.Taxonomy = null;
        public ValidationResults: Model.ValidationRuleResult[] = [];
        public ValidationErrors: Model.ValidationRule[] = [];

        constructor() {
 
        }
        public SetExternals() {
            this.Taxonomy = taxonomycontainer.Taxonomy;
            this.Instance = window[var_currentinstance] == null ? {} : window[var_currentinstance];
            this.ValidationResults = window[var_currentinstancevalidationresults] == null ? {} : window[var_currentinstancevalidationresults];
         
        }
        public LoadInstance(instancejson: string) {
            var item: Model.Instance = <Model.Instance>JSON.parse(instancejson);
            this.Instance = item;
        }

        public LoadToUI()
        {
            //this.SetExternals();
            this.ShowContent("#Facts");
            console.log("Loading validationlist" + new Date());
            BindX($("#factlist"), this.Instance.Facts );
            console.log(new Date());

        }

        public ShowDetails(factkey:string, factstring:string)
        {
            var facts: Model.InstanceFact[] = this.Instance.FactDictionary[factkey];
          
            if (facts != null && facts.length>0)
            {
           
                var fact = facts[0];
                //fact.Cells = this.Taxonomy.FactMap[factkey];
                Bind("#factdetail", fact);

            }

        }

        public ShowValidationResults()
        {
            var me = this;
            var rule: Model.ValidationRule = null;
            this.ValidationErrors = [];
            this.ValidationResults.forEach(function (v) {
                if (IsNull(rule) || rule.ID != v.ID) {
                    var tax_rule = me.Taxonomy.ValidationRules.AsLinq<Model.ValidationRule>().FirstOrDefault(i=> i.ID == v.ID);
                    rule = new Model.ValidationRule();
                    rule.ID = tax_rule.ID;
                    rule.FunctionName = tax_rule.FunctionName;
                    rule.DisplayText = tax_rule.DisplayText;
                    rule.OriginalExpression = tax_rule.OriginalExpression;
                    me.ValidationErrors.push(rule);
                }
                if (!IsNull(rule)) {
                    rule.Results.push(v);
                    v.Parameters.forEach(function (p) {
                        var fact = p.Facts[0];
                        //p.Cells = me.Taxonomy.FactMap[fact];
                    });
                }
                
            });
            //console.log("Loading validationlist" + new Date());
            BindX($("#validationlist"), this.ValidationErrors);
            //console.log(new Date());

        }

        public ShowContent(selector: string)
        {
            $("#Contents>div").hide();
            $(selector).show();

        }
        public HashChanged()
        {
       
        }
        public Test()
        {
            this.ShowValidationResults();
            console.log("Loading validationlist" + new Date());
            BindX($("#validationlist"), this.ValidationErrors);
            console.log(new Date());

        }

        public NavigateToCell(cell: string) {
            Notify(Format("navigatetocell:{0}", cell));
        }
    }
}


var instancecontainer = new Control.InstanceContainer();