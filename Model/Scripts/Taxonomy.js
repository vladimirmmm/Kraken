var Control;
(function (Control) {
    var TaxonomyContainer = (function () {
        function TaxonomyContainer() {
            this.Taxonomy = new Model.Taxonomy();
            this.LPageSize = 10;
            this.s_fact_id = "T_Facts";
            this.s_label_id = "T_Labels";
            this.s_concept_id = "T_Concepts";
            this.s_hierarchy_id = "T_Hierarchies";
            this.s_dimension_id = "T_Dimensions";
            this.s_validation_id = "T_Validations";
            this.s_units_id = "T_Units";
            this.s_find_id = "T_Filing";
            this.s_general_id = "T_General";
            this.s_main_id = "TaxonomyContainer";
            this.s_fact_selector = "";
            this.s_label_selector = "";
            this.s_validations_selector = "";
            this.s_units_selector = "";
            this.s_find_selector = "";
            this.s_general_selector = "";
            this.s_fact_selector = "#" + this.s_fact_id;
            this.s_label_selector = "#" + this.s_label_id;
            this.s_validations_selector = "#" + this.s_validation_id;
            this.s_units_selector = "#" + this.s_units_id;
            this.s_find_selector = "#" + this.s_find_id;
            this.s_general_selector = "#" + this.s_general_id;
        }
        TaxonomyContainer.prototype.Sel = function (selector) {
            var me = this;
            return $(selector, "#" + me.s_main_id);
        };
        TaxonomyContainer.prototype.SelFrom = function (parentselector, selector) {
            var me = this;
            return $(selector, me.Sel(parentselector));
        };
        TaxonomyContainer.prototype.SelFromLabel = function (selector) {
            var me = this;
            return $(selector, me.Sel(me.s_label_selector));
        };
        TaxonomyContainer.prototype.SelFromFact = function (selector) {
            var me = this;
            return $(selector, me.Sel(me.s_fact_selector));
        };
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
        };
        TaxonomyContainer.prototype.LoadContentToUI = function (contentid, sender) {
            var me = this;
            if (contentid == me.s_label_id) {
                ShowContent('#' + contentid, sender);
                LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), me.Taxonomy.Labels, 0, me.LPageSize);
            }
        };
        TaxonomyContainer.prototype.ClearFilterLabelss = function () {
            $("input[type=text]", "#LabelFilter ").val("");
            $("textarea", "#LabelFilter ").val("");
            this.FilterLabels();
        };
        TaxonomyContainer.prototype.FilterLabels = function () {
            var me = this;
            var f_key = me.SelFromLabel(s_listfilter_selector + " #F_Key").val().toLowerCase().trim();
            var f_code = me.SelFromLabel(s_listfilter_selector + " #F_Code").val().toLowerCase().trim();
            var f_content = me.SelFromLabel(s_listfilter_selector + " #F_Content").val().toLowerCase().trim();
            var query = me.Taxonomy.Labels.AsLinq();
            if (!IsNull(f_content)) {
                query = query.Where(function (i) { return i.Content.toLowerCase().indexOf(f_content) > -1; });
            }
            if (!IsNull(f_code)) {
                query = query.Where(function (i) { return i.Code.toLowerCase() == f_code; });
            }
            if (!IsNull(f_key)) {
                query = query.Where(function (i) { return i.LabelID.toLowerCase().indexOf(f_key) == i.LabelID.length - f_key.length; });
            }
            //var context_id = f_context.indexOf(" ") > -1 || f_context.indexOf("\n") > -1 ? "" : f_context.trim();
            LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), query.ToArray(), 0, me.LPageSize);
        };
        return TaxonomyContainer;
    })();
    Control.TaxonomyContainer = TaxonomyContainer;
})(Control || (Control = {}));
var taxonomycontainer = new Control.TaxonomyContainer();
//# sourceMappingURL=Taxonomy.js.map