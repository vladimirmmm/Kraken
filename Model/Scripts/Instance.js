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
            $("#factdetail").hide();
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
            me.LoadPage($("#factlist"), $("#factpager"), me.Instance.Facts, 0, me.PageSize);
            console.log(new Date());
        };
        InstanceContainer.prototype.LoadPage = function ($bindtarget, $pager, items, page, pagesize) {
            var me = this;
            var startix = pagesize * page;
            var endix = startix + pagesize;
            var itemspart = items.slice(startix, endix);
            BindX($bindtarget, itemspart);
            if ($pager.length == 0 || 1 == 1) {
                $pager.pagination(items.length, {
                    items_per_page: pagesize,
                    current_page: page ? page : 0,
                    //link_to: "#/Article/Index?page=__id__",
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
        };
        InstanceContainer.prototype.ShowDetails = function (factkey, factstring) {
            var facts = this.Instance.FactDictionary[factkey];
            if (facts != null && facts.length > 0) {
                var fact = facts[0];
                Model.FactBase.LoadFromFactString(fact);
                var $factdetail = $("#factdetail");
                BindX($factdetail, fact);
                $factdetail.show();
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
                    });
                }
            });
            me.LoadPage($("#validationlist"), $("#validationerrorpager"), me.ValidationErrors, 0, me.PageSize);
            //console.log("Loading validationlist" + new Date());
            //console.log(new Date());
        };
        InstanceContainer.prototype.ShowContent = function (selector) {
            $("#Contents>div").hide();
            $(selector).show();
        };
        InstanceContainer.prototype.LoadContentToUI = function (contentid) {
            var me = this;
            if (contentid == "Facts") {
                me.ShowContent('#Facts');
                me.LoadPage($("#factlist"), $("#factpager"), me.Instance.Facts, 0, me.PageSize);
            }
            if (contentid == "InvalidFacts") {
                me.ShowContent('#Facts');
                var invalidfacts = me.Instance.Facts.AsLinq().Where(function (i) { return i.Cells.length == 0; }).ToArray();
                me.LoadPage($("#factlist"), $("#factpager"), invalidfacts, 0, me.PageSize);
            }
            if (contentid == "ValidationErrors") {
                me.ShowContent('#Validation');
                instancecontainer.ShowValidationResults();
            }
            if (contentid == "ValidationRules") {
                me.ShowContent('#Validation');
            }
        };
        InstanceContainer.prototype.ShowRuleDetail = function (ruleid) {
            var me = this;
            var rule = this.ValidationErrors.AsLinq().FirstOrDefault(function (i) { return i.ID == ruleid; });
            me.LoadPage($("#validationruleresults"), $("#validationddetailpager"), rule.Results, 0, 1);
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