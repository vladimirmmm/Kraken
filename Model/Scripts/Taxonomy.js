var Control;
(function (Control) {
    var TaxonomyContainer = (function () {
        function TaxonomyContainer() {
            this.Taxonomy = new Model.Taxonomy();
            //this.SetExternals(this.Taxonomy);
        }
        TaxonomyContainer.prototype.SetExternals = function (Taxonomy) {
            if (IsNull(Taxonomy)) {
                Taxonomy = this.Taxonomy;
            }
            AjaxRequest("Taxonomy/Concepts", "get", "json", null, function (data) {
                Taxonomy.Concepts = data;
            }, function (error) {
                console.log(error);
            });
            AjaxRequest("Taxonomy/ValidationRules", "get", "json", null, function (data) {
                Taxonomy.ValidationRules = data;
            }, function (error) {
                console.log(error);
            });
            AjaxRequest("Taxonomy/Hierarchies", "get", "json", null, function (data) {
                Taxonomy.Hierarchies = data;
            }, function (error) {
                console.log(error);
            });
            AjaxRequest("Taxonomy/Labels", "get", "json", null, function (data) {
                Taxonomy.Labels = data;
            }, function (error) {
                console.log(error);
            });
            /*
            Taxonomy.Concepts = window[var_tax_concepts] == null ? [] : window[var_tax_concepts];
            Taxonomy.ValidationRules = window[var_tax_validations] == null ? [] : window[var_tax_validations];
            Taxonomy.Hierarchies = window[var_tax_hierarchies] == null ? [] : window[var_tax_hierarchies];
            Taxonomy.Labels = window[var_tax_labels] == null ? [] : window[var_tax_labels];
            */
        };
        return TaxonomyContainer;
    })();
    Control.TaxonomyContainer = TaxonomyContainer;
})(Control || (Control = {}));
var taxonomycontainer = new Control.TaxonomyContainer();
//# sourceMappingURL=Taxonomy.js.map