module Control {
    export class TaxonomyContainer {
        public Taxonomy: Model.Taxonomy = new Model.Taxonomy();
        public Table: UI.Table = new UI.Table();
        public ValidationRules: Model.ValidationRule[] = [];

        public ConceptValues: Model.ConceptLookUp[] = [];

        public TableStructure: Model.Hierarchy<Model.TableInfo> = null;
        public CurrentFacts: Model.KeyValuePair<string, string[]>[] = null;
        public LPageSize: number = 10;
        public PageSize: number = 20;


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
        private s_validation_selector: string = "";
        private s_units_selector: string = "";
        private s_find_selector: string = "";
        private s_general_selector: string = "";

        private FactServiceFunction: General.FunctionWithCallback = null;

        constructor() {
            this.s_fact_selector = "#" + this.s_fact_id;
            this.s_label_selector = "#" + this.s_label_id;
            this.s_validation_selector = "#" + this.s_validation_id;
            this.s_units_selector = "#" + this.s_units_id;
            this.s_find_selector = "#" + this.s_find_id;
            this.s_general_selector = "#" + this.s_general_id;
            var me = this;

            //$(window).resize(function () {
            //    waitForFinalEvent(function () {
            //        me.SetHeight();
            //    }, 200, "retek");
            //});

            $(window).on('hashchange', function () {
                me.HashChanged();
            });
        }

        public HashChanged() {
            var me = this;
            me.Table.HashChanged();

        }


        private SetHeight() {
            //var bodyheight = $(window).height();
            //var pivotheight = (bodyheight - 50) + "px";
            //$(".pivotitem").css("max-height", pivotheight);
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
        public SelFromValidation(selector: any): JQuery {
            var me = this;
            return $(selector, me.Sel(me.s_validation_selector));
        }

        public SetExternals() {
            var me = this;
            me.SetHeight();
            me.Table.Taxonomy = me.Taxonomy;
            AjaxRequest("Taxonomy/Get", "get", "json", null, function (data) {
                me.Taxonomy.Module = data;
                BindX($("#TaxonomyInfo"), me.Taxonomy.Module)
                BindX($("#TaxonomyGeneral"), me.Taxonomy.Module)
            }, function (error) { console.log(error); });

            AjaxRequest("Taxonomy/Tables", "get", "json", null, function (data) {
                me.TableStructure = data;
                ForAll(me.TableStructure, "Children", function (item: Model.Hierarchy<Model.TableInfo>) {
                    if (!IsNull(item) && !IsNull(item.Item)) {
                        item.Item.CssClass = item.Item.Type == "table" ? "hidden" : "";
                        item.Item.ExtensionText = item.Item.Type == "table" && item.Children.length > 0 ? Format("({0})", item.Children.length) : "";
                    }
                });
                BindX($("#tabletreeview"), me.TableStructure);

            }, function (error) { console.log(error); });

            AjaxRequest("Taxonomy/Concepts", "get", "json", null, function (data) {
                me.Taxonomy.Concepts = data;
            }, function (error) { console.log(error); });

            me.LoadValidationResults(null);

            AjaxRequest("Taxonomy/Hierarchies", "get", "json", null, function (data) {
                me.Taxonomy.Hierarchies = data;

                me.LoadConceptValues();

            }, function (error) { console.log(error); });

            AjaxRequest("Taxonomy/Labels", "get", "json", null, function (data) {
                me.Taxonomy.Labels = data;
            }, function (error) { console.log(error); });

            me.FactServiceFunction = new General.FunctionWithCallback(
                (fwc: General.FunctionWithCallback, args: any) => {
                    var p = <Model.Dictionary<any>>args[0];
                    AjaxRequest("Taxonomy/Facts", "get", "json", p,
                        function (data: DataResult) {
                            me.CurrentFacts = <Model.KeyValuePair<string, string[]>[]>data.Items;
                            fwc.Callback(data);
                        }, null);
                });

        }

        public LoadValidationResults(onloaded: Function) {
            var me = this;
            AjaxRequest("Taxonomy/ValidationRules", "get", "json", null, function (data) {
                me.Taxonomy.ValidationRules = data;
                me.Taxonomy.ValidationRules.forEach(function (v: Model.ValidationRule) {
                    v.Title = Truncate(v.DisplayText, 100)
                });
                CallFunction(onloaded);
            }, function (error) { console.log(error); });
        }
        public ShowValidationResults() {
            var me = this;
            var eventhandlers = { onpaging: () => { me.CloseRuleDetail(); } };
            me.CloseRuleDetail();
            LoadPage(me.SelFromValidation(s_list_selector), me.SelFromValidation(s_listpager_selector), me.Taxonomy.ValidationRules, 0, me.PageSize, eventhandlers);

            $(".trimmed").click(function () {
                if ($(this).hasClass("hmax30")) {
                    $(this).removeClass("hmax30");
                } else {
                    $(this).addClass("hmax30");

                }
            });
        }

        public LoadContentToUI(sender: any) {
            var me = this;
            var target = _Attribute(sender, "href");
            var target = target.length > 0 ? target.substring(1) : "";
            me.LoadContentToUIX(target, sender);
        }

        public LoadContentToUIX(contentid: string, sender: any) {
            var me = this;

            if (contentid == "Taxonomy") {

            }
            if (contentid == me.s_label_id) {
                LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), me.Taxonomy.Labels, 0, me.LPageSize);

            }
            if (contentid == me.s_validation_id) {
                me.ShowValidationResults();
            }
            if (contentid == me.s_fact_id) {
                var eventhandlers = {
                    onloading: (data: any) => {
                        //EnumerateObject(data, me,(item, itemname) => { item["PropertyName"] = itemname; });
                    }

                };
                LoadPageAsync(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.FactServiceFunction, 0, me.PageSize, eventhandlers);

            }
        }

        public ClearFilterLabels() {
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

            LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), query.ToArray(), 0, me.LPageSize);

        }

        public ClearFilterValidations() {
            var me = this;
            var $filtercontainer = me.SelFromValidation(s_listfilter_selector);
            $("input[type=text]", $filtercontainer).val("");
            $("textarea", $filtercontainer).val("");
            me.FilterValidations();
        }

        public FilterValidations() {
            var me = this;
            var f_ruleid: string = me.SelFromValidation(s_listfilter_selector + " #F_RuleID").val().toLowerCase().trim();
            var f_ruletext: string = me.SelFromValidation(s_listfilter_selector + " #F_RuleText").val().toLowerCase().trim();
            var query = me.Taxonomy.ValidationRules.AsLinq<Model.ValidationRule>();
            if (!IsNull(f_ruletext)) {
                query = query.Where(i=> i.DisplayText.toLowerCase().indexOf(f_ruletext) > -1);
            }
            if (!IsNull(f_ruleid)) {
                query = query.Where(i=> i.ID.indexOf(f_ruleid) > -1);
            }
      
            var eventhandlers = { onpaging: () => { me.CloseRuleDetail(); } };
            me.CloseRuleDetail();
            LoadPage(me.SelFromValidation(s_list_selector), me.SelFromValidation(s_listpager_selector), query.ToArray(), 0, me.LPageSize, eventhandlers);

        }

        public ClearFilterFacts() {
            this.SelFromFact(s_listfilter_selector + " " + "input[type=text]").val("");
            this.SelFromFact(s_listfilter_selector + " " + "textarea").val("");
            this.FilterFacts();
        }

        private GetFilterValue(selector: string): string
        {
            var element = this.SelFromFact(s_listfilter_selector + " " + selector);
            if (!IsNull(element))
            {
                return _Value(element).toLowerCase().trim();
            }
            return "";
        }

        public FilterFacts() {
            var me = this;
            var f_factstring: string = me.GetFilterValue("#F_FactString");
            var f_cellid: string = me.GetFilterValue("#F_CellID");
            var query: any = me.Taxonomy.Facts;

            var parameters = { factstring: f_factstring, cellid: f_cellid };
            LoadPageAsync(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.FactServiceFunction, 0, me.PageSize, parameters, null);
       

            //LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), query, 0, me.PageSize);
            me.HideFactDetails();
        }

        public ShowFactDetails(factkey: string, factstring: string) {
            var me = this;
            var factx = me.CurrentFacts.AsLinq<Model.KeyValuePair<string, string[]>>().FirstOrDefault(i=> i.Key == factkey);
            if (!IsNull(factx)) {
                var cells: string[] = factx.Value;

                var fact = new Model.InstanceFact();
                fact.FactString = factkey;
                Model.FactBase.LoadFromFactString(fact);
                fact.Dimensions.forEach(function (dimension, ix) {
                    SetProperty(dimension, "DomainMemberFullName", Model.Dimension.DomainMemberFullName(dimension));
                });
                fact.Cells = cells;
                var $factdetail = me.SelFromFact(s_detail_selector);
                BindX($factdetail, fact);
                $factdetail.show();
            } else {
                Notify(Format("Fact {0} was not found!", factkey));
            }


        }

        public HideFactDetails() {
            var me = this;
            var $factdetail = me.SelFromFact(s_detail_selector);
            $factdetail.hide();

        }

        public CloseRuleDetail() {
            var me = this;
            me.SelFromValidation(s_detail_selector).hide();
            $(me.s_validation_selector).append(me.SelFromValidation(s_detail_selector));

        }

        public ShowRuleDetail(ruleid: string) {
            var me = this;
            var previousruleid = me.SelFromValidation(s_parent_selector + " .rule").attr("rule-id");
            var $valdetail = me.SelFromValidation(s_detail_selector);
            if ($valdetail.is(':visible')) {
                $valdetail.hide();
            }
            else {
                if (ruleid == previousruleid) {
                    $valdetail.show();
                }
            }
            if (ruleid != previousruleid) {
                var rule = me.Taxonomy.ValidationRules.AsLinq<Model.ValidationRule>().FirstOrDefault(i=> i.ID == ruleid);

                BindX(me.SelFromValidation(s_parent_selector), rule);

                AjaxRequest("Taxonomy/Validationrule", "get", "json", { id: ruleid }, function (data) {
                    var results = <Model.ValidationRuleResult[]>data;

                    var eventhandlers = {
                        onloading: (data: Model.ValidationRuleResult[]) => {
                            data.forEach(function (ruleresult: Model.ValidationRuleResult) {
                                TaxonomyContainer.SetValues(ruleresult);
                            });
                        }
                    };

                    LoadPage(me.SelFromValidation(s_sublist_selector), me.SelFromValidation(s_sublistpager_selector), results, 0, 1, eventhandlers);

                }, function (error) { console.log(error); });

                me.SelFromValidation(".validationrule_results_" + ruleid).append($valdetail);
                $(".trimmed").click(function () {
                    if ($(this).hasClass("hmax30")) {
                        $(this).removeClass("hmax30");
                    } else {
                        $(this).addClass("hmax30");

                    }
                });
                $valdetail.show();
            }
        }

        public TableInfoSelected(id: string, ttype: string, sender: any) {
            var me = this;
            var $sender = $(sender);
            var $parent = $(sender).parent("li");
            var $childrencontainer = $parent.children("ul").first();
            var $extensioncontainers = $(".table>.treeview");
            var $extensioncontainer = $parent.children(".treeview").first();
            var $tablecontainers = $(".table").parent();
            var childidentifier = $childrencontainer.parent().attr("title");

            if (In(ttype, "table", "tablegroup")) {
                $extensioncontainers.hide();

                if ($extensioncontainer.css("display") == "none") {
                    $extensioncontainer.show();
                }
                else {
                    $extensioncontainer.hide();

                }
            }
            if (In(ttype, "table", "extension")) {
                me.NavigateTo(id);

            }
        }

        public static SetValues(ruleresult: Model.ValidationRuleResult) {
            ruleresult.Parameters.forEach(function (parameter: Model.SimlpeValidationParameter) {
                var strvalue = "";
                var numericval = 0;
                if (parameter.BindAsSequence) {
                    var factsnr = parameter.Facts.length;
                    var ix = 0;
                    strvalue += "(";
                    parameter.Facts.forEach(function (factstring: string) {
                        var strval = TaxonomyContainer.GetFactValue(factstring);
                        numericval += Number(strval);
                        strvalue += strval;
                        strvalue += ix < factsnr - 1 ? ", " : ")";
                        ix++;
                    });
                    strvalue = numericval.toFixed(2).toString() + "  " + strvalue;
                } else {
                    if (parameter.Facts.length == 1) {
                        strvalue = TaxonomyContainer.GetFactValue(parameter.Facts[0]);
                    }
                }
                parameter.Value = strvalue;
            });
        }

        public GetCellValue(cellid: string): string {
            return "";
        }

        public static GetFactValue(factstring: string): string {
            var tmpfact = new Model.FactBase();
            tmpfact.FactString = factstring;
            Model.FactBase.LoadFromFactString(tmpfact);
            var factkey = tmpfact.GetFactKey()
            var facts: Model.InstanceFact[] = app.instancecontainer.Instance.FactDictionary[factkey];
            if (!IsNull(facts) && facts.length > 0) {
                var fact: Model.InstanceFact = facts.AsLinq<Model.InstanceFact>().FirstOrDefault(i=> i.FactString == factstring);
                //var fact: Model.InstanceFact = facts[0];
                if (IsNull(fact)) {
                    //Notify(Format("FactString {1} was not found in the instance", factstring));
                    return "";
                } else {
                    return fact.Value;
                }
            } else {

                //Notify(Format("FactKey {0} was not found in the instance", factkey));
            }
            return "";
            //return "";
        }

        public NavigateTo(cell: string) {
            var me = this;
            AjaxRequest("Taxonomy/Table", "get", "text/html", { cell: cell }, function (data) {
                var itemparts = <string>data.split("#");

                if (itemparts.length > 1) {
                    var url = itemparts[0];
                    var hash = itemparts[1];

                    window.location.hash = hash;
               
                }
            },
            function (error) { console.log(error); });
        }
        public LoadConceptValues() {
            var me = this;
            var concepts: Model.Concept[] = <Model.Concept[]>GetPropertiesArray(me.Taxonomy.Concepts);

            if (me.Taxonomy.Hierarchies.length > 0 && concepts.length > 0) {

                me.ConceptValues = [];
                var htemp = new Model.Hierarchy<Model.QualifiedItem>();
                me.Taxonomy.Hierarchies.forEach(function (hierarchy) {
                    Model.QualifiedItem.Set(hierarchy.Item);

                });
                concepts.forEach(function (concept) {
                    Model.QualifiedName.Set(concept);
                    if (!IsNull(concept.Domain)) {
                        Model.QualifiedName.Set(concept.Domain);
                        concept.Domain.Name = IsNull(concept.Domain.Name) ? concept.Domain.ID : concept.Domain.Name;
                        /*
                        i.Item.Name == concept.Domain.Name
                            && i.Item.Namespace == concept.Domain.Namespace
                            && 
                        */
                        var hiers = me.Taxonomy.Hierarchies.AsLinq<Model.Hierarchy<Model.QualifiedItem>>()
                            .Where(i=>
                                i.Item.Role == concept.HierarchyRole
                            );
                        var hier = hiers.FirstOrDefault();
                        if (!IsNull(hier)) {
                            var clkp = new Model.ConceptLookUp();
                            clkp.Concept = Format("{0}:{1}", concept.Namespace, concept.Name);
                            
                            var subhier = Model.Hierarchy.FirstOrDefault(hier, (i:Model.Hierarchy<Model.QualifiedItem>) =>i.Item.Content == concept.Domain.Content)
                            var items = Model.Hierarchy.ToArray(IsNull(subhier) ? hier : subhier);

                            items.forEach(function (item, index) {
                                Model.QualifiedItem.Set(item);
                                if (item.Name!="x0") {
                                    var v = {};
                                    var id = Format("{0}:{1}", item.Namespace, item.Name);
                                    clkp.Values[id] = Format("({0}) {1}", id, item.Label == null ? "" : item.Label.Content);
                                }
                            });
                            clkp.OptionsHTML = ToOptionList(clkp.Values, true);
                            me.ConceptValues.push(clkp);
                        }
                        else
                        {
                            Log("Hierarchy not found for " + concept.Content);
                        }
                    }
                });
            }
        }
        public GetConcepOptions(concept: string): string {
            var clkp = this.ConceptValues.AsLinq<Model.ConceptLookUp>().FirstOrDefault(i=> i.Concept == concept);
            if (clkp != null) {
                return clkp.OptionsHTML;
            }
            return "";
        }
    }
}
var s_tableframe_selector: string = "#ReportContainer";

