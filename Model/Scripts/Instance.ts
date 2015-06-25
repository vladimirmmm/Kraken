module Control
{
    export class InstanceContainer {
        public Instance: Model.Instance = null;
        public Taxonomy: Model.Taxonomy = null;
        public ValidationResults: Model.ValidationRuleResult[] = [];
        public ValidationErrors: Model.ValidationRule[] = [];
        public FactsNr: number = 0;
        public Page: number = 0;
        public PageSize: number = 20;
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
            var me = this;
            //this.SetExternals();
            this.ShowContent("#Facts");
            console.log("Loading validationlist" + new Date());
            var facts: Model.FactBase[] = [];
            var dict = this.Instance.FactDictionary;
            if (IsNull(this.Instance.Facts))
            {       
                this.Instance.Facts = [];
            }
            for (var key in dict)
            {
                if (dict.hasOwnProperty(key)) {
                    var factlist = <Model.InstanceFact[]>dict[key];
                    for (var i = 0; i < factlist.length; i++)
                    {
                        var fact = factlist[i];
                        var parts = fact.FactString.split(',');
                        if (parts.length > 0) {
                            var conceptstring = parts[0].indexOf("[") == -1 ? parts[0] : "";
                            fact.Concept = new Model.Concept();
                            fact.Concept.FullName = conceptstring;
                        }
                        this.Instance.Facts.push(fact);
                    }
                }
            }
            me.FactsNr = this.Instance.Facts.length;
            $("#factpager").pagination(me.FactsNr,
                {
                    items_per_page: me.PageSize,
                    current_page: me.Page ? me.Page : 0,
                    //link_to: "#/Article/Index?page=__id__",
                    prev_text: "Prev",
                    next_text: "Next",
                    ellipse_text: "...",
                    prev_show_always: true,
                    next_show_always: true,
                    callback: function (pageix) {
                        me.LoadPage($("#factlist"), me.Instance.Facts, pageix,me.PageSize);
                    },
                });

            $("#validationerrorpager").pagination(me.ValidationErrors.length,
                {
                    items_per_page: me.PageSize,
                    current_page: me.Page ? me.Page : 0,
                    //link_to: "#/Article/Index?page=__id__",
                    prev_text: "Prev",
                    next_text: "Next",
                    ellipse_text: "...",
                    prev_show_always: true,
                    next_show_always: true,
                    callback: function (pageix) {
                        me.LoadPage($("#validationlist"), me.ValidationErrors,  pageix,me.PageSize);
                    },
                });
      

            me.LoadPage($("#factlist"), me.Instance.Facts, 0,  me.PageSize);
            console.log(new Date());

        }

        public LoadPage($bindtarget: JQuery, items:any[], page: number, pagesize: number)
        {
            var me = this;
            var startix = pagesize * page;
            var endix = startix + pagesize;
            var itemspart = items.slice(startix, endix);
            BindX($bindtarget, itemspart);
            /*
            if ($bindtarget == "facts") {
                BindX($("#factlist"), itemspart);
            }
            if ($bindtarget == "validations")
            {
                BindX($("#validationlist"), itemspart);

            }
            if ($bindtarget == "validationruleresults")
            {
                BindX($("#validationruleresults"), itemspart);
            }
            */
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
            me.LoadPage($("#validationlist"), me.ValidationErrors, 0, me.PageSize);
            //console.log("Loading validationlist" + new Date());
            //console.log(new Date());

        }

        public ShowContent(selector: string)
        {
            $("#Contents>div").hide();
            $(selector).show();

        }

        public LoadContentToUI(contentid: string)
        {
            var me = this;
            if (contentid == "Facts"){
                me.ShowContent('#Facts');
                me.LoadPage($("#factlist"), me.Instance.Facts, 0, me.PageSize);

            }
            if (contentid == "InvalidFacts") {
                me.ShowContent('#Facts');
                var invalidfacts = me.Instance.Facts.AsLinq<Model.InstanceFact>().Where(i=> i.Cells.length == 0).ToArray();
                me.LoadPage($("#factlist"), me.Instance.Facts, 0, me.PageSize);

            }
            if (contentid == "ValidationErrors") {
                me.ShowContent('#Validation');
                instancecontainer.ShowValidationResults()
            }
            if (contentid == "ValidationRules") {
                me.ShowContent('#Validation');

            }
        }

        public ShowRuleDetail(ruleid: string)
        {
            var me = this;
            var rule = this.ValidationErrors.AsLinq<Model.ValidationRule>().FirstOrDefault(i=> i.ID == ruleid);
            //BindX($("#validationdetail"), rule);

            $("#validationddetailpager").pagination(rule.Results.length,
                {
                    items_per_page: 1,
                    current_page: me.Page ? me.Page : 0,
                    //link_to: "#/Article/Index?page=__id__",
                    prev_text: "Prev",
                    next_text: "Next",
                    ellipse_text: "...",
                    prev_show_always: true,
                    next_show_always: true,
                    callback: function (pageix) {
                        me.LoadPage($("#validationruleresults"), rule.Results, pageix,1);
                    },
                });
            me.LoadPage($("#validationruleresults"), rule.Results, 0, 1);

            $("#validationrule_results_" + ruleid).append($("#validationdetail"));
            $("#validationdetail").show();
        }

        public CloseRuleDetail()
        {
            $("#validationdetail").hide();

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