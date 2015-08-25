module Control {
    export class TaxonomyContainer
    {
        public Taxonomy: Model.Taxonomy = new Model.Taxonomy();
        public LPageSize: number = 10;


        private s_fact_id: string = "T_Facts";
        private s_label_id: string = "T_Labels";
        private s_concept_id: string = "T_Concepts";
        private s_hierarchy_id: string = "T_Hierarchies";
        private s_dimension_id: string = "T_Dimensions";
        private s_validation_id: string = "T_Validations";
        private s_units_id: string = "T_Units";
        private s_find_id: string = "T_Filing";
        private s_general_id: string = "T_General";

        private s_main_id: string = "TaxonomyContainer";


        private s_fact_selector: string = "";
        private s_label_selector: string = "";
        private s_validations_selector: string = "";
        private s_units_selector: string = "";
        private s_find_selector: string = "";
        private s_general_selector: string = "";

        constructor() {
            this.s_fact_selector = "#" + this.s_fact_id;
            this.s_label_selector = "#" + this.s_label_id;
            this.s_validations_selector = "#" + this.s_validation_id;
            this.s_units_selector = "#" + this.s_units_id;
            this.s_find_selector = "#" + this.s_find_id;
            this.s_general_selector = "#" + this.s_general_id;
        }
        public Sel(selector: any): JQuery {
            var me = this;
            return $(selector, "#" + me.s_main_id);
        }

        public SelFrom(parentselector: any, selector: any): JQuery {
            var me = this;
            return $(selector, me.Sel(parentselector));
        }
        public SelFromLabel(selector: any): JQuery {
            var me = this;
            return $(selector, me.Sel(me.s_label_selector));
        }
        public SelFromFact(selector: any): JQuery {
            var me = this;
            return $(selector, me.Sel(me.s_fact_selector));
        }

        public SetExternals(Taxonomy: Model.Taxonomy) {
            if (IsNull(Taxonomy))
            {
                Taxonomy = this.Taxonomy;
            }

            AjaxRequest("Taxonomy/Concepts", "get", "json", null, function (data) {
                Taxonomy.Concepts = data;
            }, function (error) { console.log(error); });
            AjaxRequest("Taxonomy/ValidationRules", "get", "json", null, function (data) {
                Taxonomy.ValidationRules = data;
            }, function (error) { console.log(error); });
            AjaxRequest("Taxonomy/Hierarchies", "get", "json", null, function (data) {
                Taxonomy.Hierarchies = data;
            }, function (error) { console.log(error); });
            AjaxRequest("Taxonomy/Labels", "get", "json", null, function (data) {
                Taxonomy.Labels = data;
            }, function (error) { console.log(error); });
        
        }

        public LoadContentToUI(contentid: string, sender: any) {
            var me = this;
            if (contentid == me.s_label_id) {
                ShowContent('#'+contentid, sender);
                LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), me.Taxonomy.Labels, 0, me.LPageSize);

            }
        }

        public ClearFilterLabelss() {
            $("input[type=text]", "#LabelFilter ").val("");
            $("textarea", "#LabelFilter ").val("");
            this.FilterLabels();
        }
        public FilterLabels() {
            var me = this;
            var f_key: string = me.SelFromLabel(s_listfilter_selector + " #F_Key").val().toLowerCase().trim();
            var f_code: string = me.SelFromLabel(s_listfilter_selector + " #F_Code").val().toLowerCase().trim();
            var f_content: string = me.SelFromLabel(s_listfilter_selector + " #F_Content").val().toLowerCase().trim();
            var query = me.Taxonomy.Labels.AsLinq<Model.Label>();
            if (!IsNull(f_content)) {
                query = query.Where(i=> i.Content.toLowerCase().indexOf(f_content) > -1);
            }
            if (!IsNull(f_code)) {
                query = query.Where(i=> i.Code.toLowerCase() == f_code);
            }
            if (!IsNull(f_key)) {


                query = query.Where(i=> i.LabelID.toLowerCase().indexOf(f_key) == i.LabelID.length - f_key.length);
            }
            //var context_id = f_context.indexOf(" ") > -1 || f_context.indexOf("\n") > -1 ? "" : f_context.trim();

            LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), query.ToArray(), 0, me.LPageSize);

        }
    }
}

var taxonomycontainer = new Control.TaxonomyContainer();