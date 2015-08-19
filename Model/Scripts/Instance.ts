﻿module Control
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
            var me = this;
            me.Taxonomy = taxonomycontainer.Taxonomy;
            AjaxRequest("Instance/Get", "get", "json", null, function (data) {
                me.Instance = data;
                me.LoadToUI();
            }, function (error) { console.log(error); });
            AjaxRequest("Instance/Validation", "get", "json", null, function (data) {
                me.ValidationResults = data;
            }, function (error) { console.log(error); });
         
        }
        public LoadInstance(instancejson: string) {
            var item: Model.Instance = <Model.Instance>JSON.parse(instancejson);
            this.Instance = item;
        }

        public LoadToUI()
        {
            var me = this;
            $("#factdetail").hide();
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
            me.LoadPage($("#factlist"), $("#factpager"), me.Instance.Facts, 0, me.PageSize);

            console.log(new Date());

     
        }

        public FilterItems()
        {
            var me = this;
            var f_concept: string = $("#FactFilter #F_Concept").val();
            var f_context: string = $("#FactFilter #F_Context").val();
            var f_value: string = $("#FactFilter #F_Value").val();
            var context_id = "";
            var context_xml = "";
            if (f_context.indexOf(" ") > -1 || f_context.indexOf("\n") > -1)
            {

            }
            //var context_id = f_context.indexOf(" ") > -1 || f_context.indexOf("\n") > -1 ? "" : f_context.trim();

            me.LoadPage($("#factlist"), $("#factpager"), me.Instance.Facts, 0, me.PageSize);

        }

        public LoadPage($bindtarget: JQuery, $pager:JQuery, items:any[], page: number, pagesize: number)
        {
            var me = this;
            var startix = pagesize * page;
            var endix = startix + pagesize;
            var itemspart = items.slice(startix, endix);
            BindX($bindtarget, itemspart);
            if ($pager.length == 0 || 1==1)
            {
                $pager.pagination(items.length,
                    {
                        items_per_page: pagesize,
                        current_page: page ? page : 0,
                        link_to: "",
                        prev_text: "Prev",
                        next_text: "Next",
                        ellipse_text: "...",
                        prev_show_always: true,
                        next_show_always: true,
                        callback: function (pageix) {
                            me.LoadPage($bindtarget, $pager, items, pageix, pagesize);
                        },
                    });
            }
        
        }

        public ShowDetails(factkey:string, factstring:string)
        {
            var facts: Model.InstanceFact[] = this.Instance.FactDictionary[factkey];
          
            if (facts != null && facts.length>0)
            {
           
                var fact = facts[0];
                Model.FactBase.LoadFromFactString(fact);
                var $factdetail = $("#factdetail");
                BindX($factdetail, fact);
                $factdetail.show();
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
                    });
                }
                
            });
            me.LoadPage($("#validationlist"), $("#validationerrorpager"), me.ValidationErrors, 0, me.PageSize);
            $(".trimmed").click(function () {
                if ($(this).hasClass("hmax30")) {
                    $(this).removeClass("hmax30");
                } else {
                    $(this).addClass("hmax30");

                }
            });
             //console.log("Loading validationlist" + new Date());
            //console.log(new Date());

        }

        public ShowContent(selector: string)
        {
            $(".subcontent", "#Contents").hide();
            $(selector).show();

        }

        public LoadContentToUI(contentid: string)
        {
            var me = this;
            if (contentid == "Facts"){
                me.ShowContent('#Facts');
                me.LoadPage($("#factlist"), $("#factpager"), me.Instance.Facts, 0, me.PageSize);

            }
            if (contentid == "InvalidFacts") {
                me.ShowContent('#Facts');
                var invalidfacts = me.Instance.Facts.AsLinq<Model.InstanceFact>().Where(i=> i.Cells.length == 0).ToArray();
                me.LoadPage($("#factlist"), $("#factpager"), invalidfacts, 0, me.PageSize);

            }
            if (contentid == "ValidationErrors") {
                me.ShowContent('#Validation');
                instancecontainer.ShowValidationResults()
            }
            if (contentid == "ValidationRules") {
                me.ShowContent('#Validation');

            }

            if (contentid == "Contexts") {
                me.ShowContent('#Context');

            }
            if (contentid == "Units") {
                me.ShowContent('#Unit');

            }
            if (contentid == "FilingIndicators") {
                me.ShowContent('#FilingIndicator');

            }
            if (contentid == "General") {
                var $target = $("#Details");
                BindX($target, me.Instance);
                me.ShowContent('#Details');

            }
        }

        public ShowRuleDetail(ruleid: string)
        {
            var me = this;
            var rule = this.ValidationErrors.AsLinq<Model.ValidationRule>().FirstOrDefault(i=> i.ID == ruleid);

            me.LoadPage($("#validationruleresults"), $("#validationddetailpager"), rule.Results, 0, 1);

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
            //Notify(Format("navigatetocell:{0}", cell));
            
            AjaxRequest("Table/Get", "get", "text/html", {cell: cell}, function (data) {
                $("#tableframe").attr("src", data);
            }, function (error) { console.log(error); });
        }
    }
}


var instancecontainer = new Control.InstanceContainer();