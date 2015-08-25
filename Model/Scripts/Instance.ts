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
        public TableStructure: Model.Hierarchy<Model.TableInfo> = null;

      

        private s_fact_id: string = "I_Facts";
        private s_validation_id: string = "I_Validations";
        private s_units_id: string = "I_Units";
        private s_find_id: string = "I_Filing";
        private s_general_id: string = "I_General";

        private s_main_id: string = "InstanceContainer";

        private s_fact_selector: string = "";
        private s_validation_selector: string = "";
        private s_unit_selector: string = "";
        private s_find_selector: string = "";
        private s_general_selector: string = "";

        constructor() {
            this.s_fact_selector = "#" + this.s_fact_id;
            this.s_validation_selector = "#" + this.s_validation_id;
            this.s_unit_selector = "#" + this.s_units_id;
            this.s_find_selector = "#" + this.s_find_id;
            this.s_general_selector = "#" + this.s_general_id;
        }

        public Sel(selector: any): JQuery
        {
            var me = this;
            return $(selector, "#" + me.s_main_id);
        }

        public SelFrom(parentselector: any, selector: any): JQuery {
            var me = this;
            return $(selector, me.Sel(parentselector));
        }
        public SelFromFact(selector: any): JQuery {
            var me = this;
            return $(selector, me.Sel(me.s_fact_selector));
        }
        public SelFromValidation(selector: any): JQuery {
            var me = this;
            return $(selector, me.Sel(me.s_validation_selector));
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
            AjaxRequest("Table/List", "get", "json", null, function (data) {
                me.TableStructure = data;
                BindX($("#tabletreeview"), me.TableStructure);

            }, function (error) { console.log(error); });
        }
        public LoadInstance(instancejson: string) {
            var item: Model.Instance = <Model.Instance>JSON.parse(instancejson);
            this.Instance = item;
        }

        public LoadToUI()
        {
            var me = this;
            S_Bind_End
            me.Sel(s_detail_selector).hide();
            ShowContent("#" + me.s_fact_id, $("#TaxCommands"));
            ShowContent("#" + me.s_fact_id, $("#InstCommands"));
            ShowContent("#TaxonomyContainer", $("#MainCommands"));

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
            LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.Instance.Facts, 0, me.PageSize);

        }

        public ClearFilterFacts()
        {
            $("input[type=text]","#FactFilter ").val("");
            $("textarea","#FactFilter ").val("");
            this.FilterFacts();
        }
        public FilterFacts()
        {
            var me = this;
            var f_label: string = me.SelFromFact(s_listfilter_selector + " #F_Label").val().toLowerCase().trim();
            var f_concept: string = me.SelFromFact(s_listfilter_selector + " #F_Concept").val().toLowerCase().trim();
            var f_context: string = me.SelFromFact(s_listfilter_selector + " #F_Context").val().toLowerCase().trim();
            var f_dimension: string = me.SelFromFact(s_listfilter_selector + " #F_Dimension").val().toLowerCase().trim();
            var f_value: string = me.SelFromFact(s_listfilter_selector + " #F_Value").val().toLowerCase().trim();
            var context_id = "";
            var context_xml = "";
            var query = me.Instance.Facts.AsLinq<Model.InstanceFact>();
            if (f_context.indexOf(">") > -1 || f_context.indexOf("\n") > -1) {

            }
            else
            {
                context_id = f_context;
                query = query.Where(i=> i.ContextID.toLowerCase().indexOf(context_id) > -1);

            }
            if (!IsNull(f_concept))
            {
                query = query.Where(i=> i.Concept.FullName.toLowerCase().indexOf(f_concept) > -1);
            }
            if (!IsNull(f_dimension)) {
                query = query.Where(i=> i.FactString.toLowerCase().indexOf(f_dimension) > -1);
            }
            if (!IsNull(f_value)) {
                query = query.Where(i=> i.Value.toLowerCase()==f_value);
            }
      
            LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), query.ToArray(), 0, me.PageSize);

        }





        public ShowDetails(factkey:string, factstring:string)
        {
            var me = this;
            var facts: Model.InstanceFact[] = this.Instance.FactDictionary[factkey];
          
            if (facts != null && facts.length>0)
            {
           
                var fact = facts[0];
                Model.FactBase.LoadFromFactString(fact);
                var $factdetail = me.SelFromFact(s_detail_selector);
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
            LoadPage(me.SelFromValidation(s_list_selector), me.SelFromValidation(s_listpager_selector), me.ValidationErrors, 0, me.PageSize);
            $(".trimmed").click(function () {
                if ($(this).hasClass("hmax30")) {
                    $(this).removeClass("hmax30");
                } else {
                    $(this).addClass("hmax30");

                }
            });
  

        }

     

        public LoadContentToUI(contentid: string, sender: any)
        {
            var me = this;
            var text = $(sender).text();
            if (contentid == "Taxonomy")
            {
                ShowContent('#TaxonomyContainer', sender);

            }
            if (contentid == "Instance")
            {
                ShowContent('#InstanceContainer', sender);

            }
            if (contentid == me.s_fact_id && text.toLowerCase().indexOf("invalid") == -1){
                ShowContent('#'+contentid, sender);
                LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.Instance.Facts, 0, me.PageSize);

            }
            if (contentid == me.s_fact_id && text.toLowerCase().indexOf("invalid")>-1) {
                ShowContent('#' + contentid, sender);
                var invalidfacts = me.Instance.Facts.AsLinq<Model.InstanceFact>().Where(i=> i.Cells.length == 0).ToArray();
                LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), invalidfacts, 0, me.PageSize);

            }
            if (contentid == me.s_validation_id) {
                ShowContent('#'+contentid, sender);
                instancecontainer.ShowValidationResults()
            }


            if (contentid == me.s_units_id) {
                ShowContent('#' + contentid, sender);

            }
            if (contentid == me.s_find_id) {
                ShowContent('#'+contentid, sender);

            }
            if (contentid == me.s_general_id) {
                BindX(me.Sel(me.s_general_selector), me.Instance);
                ShowContent('#'+contentid, sender);

            }
        }

        public ShowRuleDetail(ruleid: string)
        {
            var me = this;
            var rule = this.ValidationErrors.AsLinq<Model.ValidationRule>().FirstOrDefault(i=> i.ID == ruleid);

            LoadPage($("#validationruleresults"), $("#validationddetailpager"), rule.Results, 0, 1);

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


        public NavigateTo(cell: string) {
            //Notify(Format("navigatetocell:{0}", cell));
            
            AjaxRequest("Table/Get", "get", "text/html", {cell: cell}, function (data) {
                $("#tableframe").attr("src", data);
                $("#tableframe").css("height", $("#tableframe").parent().parent().height());
            }, function (error) { console.log(error); });
        }

    }
}


var instancecontainer = new Control.InstanceContainer();