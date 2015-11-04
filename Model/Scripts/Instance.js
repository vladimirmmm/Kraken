var Control;
(function (Control) {
    var InstanceContainer = (function () {
        function InstanceContainer() {
            this.Instance = null;
            this.Taxonomy = null;
            this.ValidationResults = [];
            this.ValidationErrors = [];
            this.FactsNr = 0;
            this.Page = 0;
            this.PageSize = 20;
            this.VPageSize = 20;
            this.s_fact_id = "I_Facts";
            this.s_validation_id = "I_Validations";
            this.s_units_id = "I_Units";
            this.s_find_id = "I_Filing";
            this.s_general_id = "I_General";
            this.s_main_id = "InstanceContainer";
            this.s_fact_selector = "";
            this.s_validation_selector = "";
            this.s_unit_selector = "";
            this.s_find_selector = "";
            this.s_general_selector = "";
            this.s_fact_selector = "#" + this.s_fact_id;
            this.s_validation_selector = "#" + this.s_validation_id;
            this.s_unit_selector = "#" + this.s_units_id;
            this.s_find_selector = "#" + this.s_find_id;
            this.s_general_selector = "#" + this.s_general_id;
        }
        InstanceContainer.prototype.Sel = function (selector) {
            var me = this;
            return $(selector, "#" + me.s_main_id);
        };
        InstanceContainer.prototype.SelFrom = function (parentselector, selector) {
            var me = this;
            return $(selector, me.Sel(parentselector));
        };
        InstanceContainer.prototype.SelFromFact = function (selector) {
            var me = this;
            return $(selector, me.Sel(me.s_fact_selector));
        };
        InstanceContainer.prototype.SelFromValidation = function (selector) {
            var me = this;
            return $(selector, me.Sel(me.s_validation_selector));
        };
        InstanceContainer.prototype.SetExternals = function () {
            var me = this;
            me.Taxonomy = app.taxonomycontainer.Taxonomy;
            me.LoadInstance(null);
            me.LoadValidationResults(null);
        };
        InstanceContainer.prototype.HandleAction = function (msg) {
            var me = this;
            var action = msg.Data.toLowerCase();
            if (action == "instancevalidated") {
                me.LoadValidationResults(function () {
                    var $activator = ShowContentByID("#" + me.s_validation_id);
                    me.LoadContentToUI($activator);
                });
            }
        };
        InstanceContainer.prototype.LoadValidationResults = function (onloaded) {
            var me = this;
            AjaxRequest("Instance/Validation", "get", "json", null, function (data) {
                me.ValidationResults = data;
                CallFunction(onloaded);
            }, function (error) {
                console.log(error);
            });
        };
        InstanceContainer.prototype.LoadInstance = function (onloaded) {
            var me = this;
            AjaxRequest("Instance/Get", "get", "json", null, function (data) {
                me.Instance = data;
                me.LoadToUI();
                CallFunction(onloaded);
            }, function (error) {
                console.log(error);
            });
        };
        InstanceContainer.prototype.LoadToUI = function () {
            var me = this;
            S_Bind_End;
            me.Sel(s_detail_selector).hide();
            //ShowContent("#" + me.s_fact_id, $("#TaxCommands"));
            ShowContentByID("#" + me.s_fact_id);
            //ShowContentByID("#TaxonomyContainer");
            var facts = [];
            var dict = this.Instance.FactDictionary;
            if (IsNull(this.Instance.Facts)) {
                this.Instance.Facts = [];
            }
            for (var key in dict) {
                if (dict.hasOwnProperty(key)) {
                    var factlist = dict[key];
                    for (var i = 0; i < factlist.length; i++) {
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
            if (app.taxonomycontainer.Table != null) {
                app.taxonomycontainer.Table.Instance = me.Instance;
            }
            me.FactsNr = this.Instance.Facts.length;
            LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.Instance.Facts, 0, me.PageSize);
        };
        InstanceContainer.prototype.ClearFilterFacts = function () {
            $("input[type=text]", "#FactFilter ").val("");
            $("textarea", "#FactFilter ").val("");
            this.FilterFacts();
        };
        InstanceContainer.prototype.FilterFacts = function () {
            var me = this;
            var f_label = me.SelFromFact(s_listfilter_selector + " #F_Label").val().toLowerCase().trim();
            var f_concept = me.SelFromFact(s_listfilter_selector + " #F_Concept").val().toLowerCase().trim();
            var f_context = me.SelFromFact(s_listfilter_selector + " #F_Context").val().toLowerCase().trim();
            var f_dimension = me.SelFromFact(s_listfilter_selector + " #F_Dimension").val().toLowerCase().trim();
            var f_value = me.SelFromFact(s_listfilter_selector + " #F_Value").val().toLowerCase().trim();
            var context_id = "";
            var context_xml = "";
            var query = me.Instance.Facts.AsLinq();
            if (f_context.indexOf(">") > -1 || f_context.indexOf("\n") > -1) {
            }
            else {
                context_id = f_context;
                query = query.Where(function (i) { return i.ContextID.toLowerCase().indexOf(context_id) > -1; });
            }
            if (!IsNull(f_concept)) {
                query = query.Where(function (i) { return i.Concept.FullName.toLowerCase().indexOf(f_concept) > -1; });
            }
            if (!IsNull(f_dimension)) {
                query = query.Where(function (i) { return i.FactString.toLowerCase().indexOf(f_dimension) > -1; });
            }
            if (!IsNull(f_value)) {
                query = query.Where(function (i) { return i.Value.toLowerCase() == f_value; });
            }
            var eventhandlers = { onpaging: function () {
                me.CloseFactDetails();
            } };
            LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), query.ToArray(), 0, me.PageSize, eventhandlers);
        };
        InstanceContainer.prototype.ShowFactDetails = function (factkey, factstring) {
            var me = this;
            if (!IsNull(me.Instance)) {
                var facts = this.Instance.FactDictionary[factkey];
                if (facts != null && facts.length > 0) {
                    var fact = facts[0];
                    Model.FactBase.LoadFromFactString(fact);
                    var $factdetail = me.SelFromFact(s_detail_selector);
                    BindX($factdetail, fact);
                    $factdetail.show();
                }
            }
        };
        InstanceContainer.prototype.CloseFactDetails = function () {
            var me = this;
            var $factdetail = me.SelFromFact(s_detail_selector);
            $factdetail.hide();
        };
        InstanceContainer.prototype.ShowValidationResults = function () {
            var me = this;
            var rule = null;
            this.ValidationErrors = [];
            me.CloseRuleDetail();
            if (!IsNull(me.ValidationResults)) {
                me.ValidationResults.forEach(function (v) {
                    if (IsNull(rule) || rule.ID != v.ID) {
                        var tax_rule = me.Taxonomy.ValidationRules.AsLinq().FirstOrDefault(function (i) { return i.ID == v.ID; });
                        rule = new Model.ValidationRule();
                        rule.ID = tax_rule.ID;
                        rule.FunctionName = tax_rule.FunctionName;
                        rule.Title = Truncate(tax_rule.DisplayText, 100);
                        rule.DisplayText = tax_rule.DisplayText;
                        rule.OriginalExpression = tax_rule.OriginalExpression;
                        rule.HasAllFind = v.HasAllFind;
                        me.ValidationErrors.push(rule);
                    }
                    if (!IsNull(rule)) {
                        rule.Results.push(v);
                        Control.TaxonomyContainer.SetValues(v);
                    }
                });
                var eventhandlers = {
                    onpaging: function () {
                        me.CloseRuleDetail();
                    },
                    onloading: function (data) {
                        //data.forEach(function (ruleresult: Model.ValidationRuleResult) {
                        //    TaxonomyContainer.SetValues(ruleresult);
                        //});
                    }
                };
                LoadPage(me.SelFromValidation(s_list_selector), me.SelFromValidation(s_listpager_selector), me.ValidationErrors, 0, me.VPageSize, eventhandlers);
            }
        };
        InstanceContainer.prototype.LoadContentToUI = function (sender) {
            var me = this;
            ShowContentBySender(sender);
            var target = $(sender).attr("activator-for");
            me.LoadContentToUIX(target, sender);
        };
        InstanceContainer.prototype.LoadContentToUIX = function (contentid, sender) {
            var me = this;
            //var $command = $(sender);
            //var $commands = $command.parent().children("a");
            //$commands.removeClass("selected");
            //$command.addClass("selected");
            var text = $(sender).text();
            if (contentid == "Instance") {
            }
            if (contentid == me.s_fact_id && text.toLowerCase().indexOf("invalid") == -1) {
                var eventhandlers = { onpaging: function () {
                    me.CloseFactDetails();
                } };
                LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.Instance.Facts, 0, me.PageSize, eventhandlers);
            }
            if (contentid == me.s_fact_id && text.toLowerCase().indexOf("invalid") > -1) {
                var invalidfacts = me.Instance.Facts.AsLinq().Where(function (i) { return i.Cells.length == 0; }).ToArray();
                var eventhandlers = { onpaging: function () {
                    me.CloseFactDetails();
                } };
                LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), invalidfacts, 0, me.PageSize, eventhandlers);
            }
            if (contentid == me.s_validation_id) {
                me.ShowValidationResults();
            }
            if (contentid == me.s_units_id) {
            }
            if (contentid == me.s_find_id) {
            }
            if (contentid == me.s_general_id) {
                BindX(me.Sel(me.s_general_selector), me.Instance);
            }
        };
        InstanceContainer.prototype.ShowRuleDetail = function (ruleid) {
            var showvaldetail = function () {
                $valdetail.show();
                var $rulecontainer = me.SelFromValidation(".validationrule_results_" + ruleid);
                $rulecontainer.append($valdetail);
                $valdetail.focus();
            };
            var me = this;
            var previousruleid = me.SelFromValidation(s_parent_selector + " .rule").attr("rule-id");
            var $valdetail = me.SelFromValidation(s_detail_selector);
            if ($valdetail.is(':visible')) {
                $valdetail.hide();
            }
            else {
                if (ruleid == previousruleid) {
                    showvaldetail();
                }
            }
            if (ruleid != previousruleid) {
                var rule = this.ValidationErrors.AsLinq().FirstOrDefault(function (i) { return i.ID == ruleid; });
                BindX(me.SelFromValidation(s_parent_selector), rule);
                LoadPage(me.SelFromValidation(s_sublist_selector), me.SelFromValidation(s_sublistpager_selector), rule.Results, 0, 1);
                $(".trimmed").click(function () {
                    if ($(this).hasClass("hmax30")) {
                        $(this).removeClass("hmax30");
                    }
                    else {
                        $(this).addClass("hmax30");
                    }
                });
                showvaldetail();
            }
        };
        InstanceContainer.prototype.CloseRuleDetail = function () {
            var me = this;
            me.SelFromValidation(s_detail_selector).hide();
            $(me.s_validation_selector).append(me.SelFromValidation(s_detail_selector));
        };
        InstanceContainer.prototype.HashChanged = function () {
        };
        return InstanceContainer;
    })();
    Control.InstanceContainer = InstanceContainer;
})(Control || (Control = {}));
//# sourceMappingURL=Instance.js.map