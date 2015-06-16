var Control;
(function (Control) {
    var InstanceContainer = (function () {
        function InstanceContainer() {
            this.Instance = null;
            this.Taxonomy = null;
            this.ValidationResults = [];
            this.ValidationErrors = [];
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
            //this.SetExternals();
            this.ShowContent("#Facts");
            console.log("Loading validationlist" + new Date());
            BindX($("#factlist"), this.Instance.Facts);
            console.log(new Date());
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
            //console.log("Loading validationlist" + new Date());
            BindX($("#validationlist"), this.ValidationErrors);
            //console.log(new Date());
        };
        InstanceContainer.prototype.ShowContent = function (selector) {
            $("#Contents>div").hide();
            $(selector).show();
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