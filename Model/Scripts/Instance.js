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
        }
        InstanceContainer.prototype.SetExternals = function () {
            this.Taxonomy = taxonomycontainer.Taxonomy;
            this.Instance = window[var_currentinstance] == null ? {} : window[var_currentinstance];
            this.ValidationResults = window[var_currentinstancevalidationresults] == null ? {} : window[var_currentinstancevalidationresults];
        };
        InstanceContainer.prototype.LoadInstance = function (instancejson) {
            var item = JSON.parse(instancejson);
            this.Instance = item;
        };
        InstanceContainer.prototype.LoadToUI = function () {
            var me = this;
            //this.SetExternals();
            this.ShowContent("#Facts");
            console.log("Loading validationlist" + new Date());
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
            me.FactsNr = this.Instance.Facts.length;
            $("#factpager").pagination(me.FactsNr, {
                items_per_page: me.PageSize,
                current_page: me.Page ? me.Page : 0,
                //link_to: "#/Article/Index?page=__id__",
                prev_text: "Prev",
                next_text: "Next",
                ellipse_text: "...",
                prev_show_always: true,
                next_show_always: true,
                callback: function (pageix) {
                    me.LoadPage("facts", me.Instance.Facts, pageix, me.PageSize);
                },
            });
            $("#validationerrorpager").pagination(me.ValidationErrors.length, {
                items_per_page: me.PageSize,
                current_page: me.Page ? me.Page : 0,
                //link_to: "#/Article/Index?page=__id__",
                prev_text: "Prev",
                next_text: "Next",
                ellipse_text: "...",
                prev_show_always: true,
                next_show_always: true,
                callback: function (pageix) {
                    me.LoadPage("validations", me.ValidationErrors, pageix, me.PageSize);
                },
            });
            me.LoadPage("facts", me.Instance.Facts, 0, me.PageSize);
            console.log(new Date());
        };
        InstanceContainer.prototype.LoadPage = function (specifier, items, page, pagesize) {
            var me = this;
            var startix = pagesize * page;
            var endix = startix + pagesize;
            var itemspart = items.slice(startix, endix);
            if (specifier == "facts") {
                BindX($("#factlist"), itemspart);
            }
            if (specifier == "validations") {
                BindX($("#validationlist"), itemspart);
            }
            if (specifier == "validationruleresults") {
                BindX($("#validationruleresults"), itemspart);
            }
        };
        InstanceContainer.prototype.ShowDetails = function (factkey, factstring) {
            var facts = this.Instance.FactDictionary[factkey];
            if (facts != null && facts.length > 0) {
                var fact = facts[0];
                //fact.Cells = this.Taxonomy.FactMap[factkey];
                Bind("#factdetail", fact);
            }
        };
        InstanceContainer.prototype.ShowValidationResults = function () {
            var me = this;
            var rule = null;
            this.ValidationErrors = [];
            this.ValidationResults.forEach(function (v) {
                if (IsNull(rule) || rule.ID != v.ID) {
                    var tax_rule = me.Taxonomy.ValidationRules.AsLinq().FirstOrDefault(function (i) { return i.ID == v.ID; });
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
            me.LoadPage("validations", me.ValidationErrors, 0, me.PageSize);
            //console.log("Loading validationlist" + new Date());
            //console.log(new Date());
        };
        InstanceContainer.prototype.ShowContent = function (selector) {
            $("#Contents>div").hide();
            $(selector).show();
        };
        InstanceContainer.prototype.ShowRuleDetail = function (ruleid) {
            var me = this;
            var rule = this.ValidationErrors.AsLinq().FirstOrDefault(function (i) { return i.ID == ruleid; });
            //BindX($("#validationdetail"), rule);
            $("#validationddetailpager").pagination(rule.Results.length, {
                items_per_page: 1,
                current_page: me.Page ? me.Page : 0,
                //link_to: "#/Article/Index?page=__id__",
                prev_text: "Prev",
                next_text: "Next",
                ellipse_text: "...",
                prev_show_always: true,
                next_show_always: true,
                callback: function (pageix) {
                    me.LoadPage("validationruleresults", rule.Results, pageix, 1);
                },
            });
            me.LoadPage("validationruleresults", rule.Results, 0, 1);
            $("#validationrule_results_" + ruleid).append($("#validationdetail"));
            $("#validationdetail").show();
        };
        InstanceContainer.prototype.CloseRuleDetail = function () {
            $("#validationdetail").hide();
        };
        InstanceContainer.prototype.HashChanged = function () {
        };
        InstanceContainer.prototype.Test = function () {
            this.ShowValidationResults();
            console.log("Loading validationlist" + new Date());
            BindX($("#validationlist"), this.ValidationErrors);
            console.log(new Date());
        };
        InstanceContainer.prototype.NavigateToCell = function (cell) {
            Notify(Format("navigatetocell:{0}", cell));
        };
        return InstanceContainer;
    })();
    Control.InstanceContainer = InstanceContainer;
})(Control || (Control = {}));
var instancecontainer = new Control.InstanceContainer();
//# sourceMappingURL=Instance.js.map