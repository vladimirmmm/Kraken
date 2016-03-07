module Control {
    export class TaxonomyContainer {
        public Taxonomy: Model.Taxonomy = new Model.Taxonomy();
        public Table: UI.Table = new UI.Table();
        public ValidationRules: Model.ValidationRule[] = [];

        public ConceptValues: Model.ConceptLookUp[] = [];

        public TableStructure: Model.Hierarchy<Model.TableInfo> = null;
        public CurrentFacts: Model.KeyValuePair<string, string[]>[] = [];
        public CurrentValidationResults: Model.ValidationRuleResult[] = [];
        public LPageSize: number = 10;
        public PageSize: number = 10;


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


        private s_concept_selector: string = "";
        private s_hierarchy_selector: string = "";
        private s_fact_selector: string = "";
        private s_label_selector: string = "";
        private s_validation_selector: string = "";
        private s_units_selector: string = "";
        private s_find_selector: string = "";
        private s_general_selector: string = "";

        private ui_factdetail: Element = null;
        private ui_vruledetail: Element = null;

        private FactServiceFunction: General.FunctionWithCallback = null;
        public ValidationResultServiceFunction: General.FunctionWithCallback = null;

        constructor() {
            this.s_concept_selector = "#" + this.s_concept_id;
            this.s_hierarchy_selector = "#" + this.s_hierarchy_id;
            this.s_fact_selector = "#" + this.s_fact_id;
            this.s_label_selector = "#" + this.s_label_id;
            this.s_validation_selector = "#" + this.s_validation_id;
            this.s_units_selector = "#" + this.s_units_id;
            this.s_find_selector = "#" + this.s_find_id;
            this.s_general_selector = "#" + this.s_general_id;
            var me = this;

            $(window).resize(function () {
                waitForFinalEvent(function () {
                    me.SetHeight();
                }, 200, "retek");
            });
            me.SetHeight();

            $(window).on('hashchange', function () {
                me.HashChanged();
            });
        }

        public HashChanged() {
            var me = this;
            me.Table.HashChanged();

        }


        private SetHeight() {
            var bodyheight = $("td.th2").height();
            var pivotheight1 = (bodyheight - 70) + "px";
            var pivotheight2 = (bodyheight - 110) + "px";
            $("#Contents > .ui-tabs > .ui-tabs-panel").css("max-height", pivotheight1);
            $("#Contents > .ui-tabs > .ui-tabs-panel > .ui-tabs-panel").css("max-height", pivotheight2);
        }
        public Sel(selector: any): Element {
            var me = this;
            return _SelectFirst(selector, _SelectFirst("#" + me.s_main_id));
        }

        public SelFrom(parentselector: any, selector: any): Element {
            var me = this;
            return _SelectFirst(selector, me.Sel(parentselector));
        }
        public SelFromLabel(selector: any): Element {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_label_selector));
        }
        public SelFromConcept(selector: any): Element {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_concept_selector));
        }
        public SelFromHierarchy(selector: any): Element {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_hierarchy_selector));
        }
        public SelFromFact(selector: any): Element {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_fact_selector));
        }
        public SelFromValidation(selector: any): Element {
            var me = this;

            return _SelectFirst(selector, me.Sel(me.s_validation_selector));
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
                ForAll(me.TableStructure, "Children", function (item: Model.Hierarchy<Model.TableInfo>, parent: Model.Hierarchy<Model.TableInfo>) {
                    if (!IsNull(item) && !IsNull(item.Item)) {
                        item.Item.CssClass = "";// item.Item.Type == "table" ? "hidden" : "";
                        item.Item.CssClass += item.Item.HasData > 0 ? " hasdata" : " empty";
                        item.Item.ExtensionText = item.Item.Type == "table" && item.Children.length > 0 ? Format("({0})", item.Children.length) : "";
                        item["uid"] = IsNull(parent) ? item.Item.ID : parent["uid"] + ">" + item.Item.ID;

                    }
                });
                BindX($("#tabletreeview"), me.TableStructure, 5);

            }, function (error) { console.log(error); });

            AjaxRequest("Taxonomy/Concepts", "get", "json", null, function (data) {
                me.Taxonomy.Concepts = data;
            }, function (error) { console.log(error); });

            me.LoadValidationResults(null);

            AjaxRequest("Taxonomy/Hierarchies", "get", "json", null, function (data) {
                var hierarchy = new Model.Hierarchy<Model.QualifiedItem>();
                hierarchy.Item = new Model.QualifiedItem();
                hierarchy.Item.ID = "Root";
                hierarchy.Item.Label = new Model.Label();
                hierarchy.Item.Label.Content = "Hierarchies"
                hierarchy.Children = data;
                me.Taxonomy.Hierarchies = hierarchy;

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
                            if (!IsNull(data.Items)) {
                                me.CurrentFacts = <Model.KeyValuePair<string, string[]>[]>data.Items;
                                fwc.Callback(data);
                            }
                        }, null);
                });
            me.ValidationResultServiceFunction = new General.FunctionWithCallback(
                (fwc: General.FunctionWithCallback, args: any) => {
                    var p = <Model.Dictionary<any>>args[0];
                    AjaxRequest("Instance/ValidationResults", "get", "json", p,
                        function (data: DataResult) {
                            if (!IsNull(data.Items)) {
                                me.CurrentValidationResults = <Model.ValidationRuleResult[]>data.Items;
                                me.CurrentValidationResults.forEach((v) => {
                                    TaxonomyContainer.SetValues(v);
                                });

                                fwc.Callback(data);
                            }
                        }, null);
                });

        }

        public LoadValidationResults(onloaded: Function) {
            var me = this;
            AjaxRequest("Taxonomy/ValidationRules", "get", "json", null, function (data) {
                me.Taxonomy.ValidationRules = IsNull(data) ? [] : data;
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
            var target = GetHashPart(_Attribute(sender, "href"));
        
            me.LoadContentToUIX(target, sender);
        }

        public LoadContentToUIX(contentid: string, sender: any) {
            var me = this;

            if (contentid == "Taxonomy") {

            }
            if (contentid == "Taxonomy") {

            }
            if (contentid == me.s_concept_id) {
                LoadPage(me.SelFromConcept(s_list_selector), me.SelFromConcept(s_listpager_selector), me.Taxonomy.Concepts, 0, me.LPageSize);

            }
            if (contentid == me.s_hierarchy_id) {
                
           
               
                
                ForAll(me.Taxonomy.Hierarchies, "Children", function (item: Model.Hierarchy<Model.QualifiedItem>, parent: Model.Hierarchy<Model.QualifiedItem>, level:number) {
                    if (!IsNull(item) && !IsNull(item.Item)) {
                        item.Item["CssClass"] = level > 2 ? "hidden" : "";
                        item["uid"] = IsNull(parent)? item.Item.Content: parent["uid"] + ">" + item.Item.Content;
                    }
                });
                StartProgress("hierarhcy");
                BindX(me.SelFromHierarchy("#hierarchytreeview"), me.Taxonomy.Hierarchies, 2);
                StopProgress("hierarhcy");

            }
            if (contentid == me.s_label_id) {
                LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), me.Taxonomy.Labels, 0, me.LPageSize);

            }
            if (contentid == me.s_validation_id) {
                me.ShowValidationResults();
            }
            if (In(contentid, me.s_fact_id, me.s_main_id)) {
                var eventhandlers = {
                    onloading: (data: any) => {
                        //EnumerateObject(data, me,(item, itemname) => { item["PropertyName"] = itemname; });
                    }

                };
                if (me.CurrentFacts.length == 0) {
                    LoadPageAsync(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.FactServiceFunction, 0, me.PageSize, eventhandlers);
                }

            }
        }
        public LoadHierarchy()
        {
            var me = this;
            var target = <Element>event.target;
            if (_TagName(target).toLowerCase() != "li")
            {
                target = _Parents(target).AsLinq<Element>().FirstOrDefault(i=> _TagName(i).toLowerCase() == "li");
            }
            var uid = _Attribute(target, "uid");
            var data = Model.Hierarchy.FirstOrDefault(me.Taxonomy.Hierarchies,(i: Model.Hierarchy<Model.QualifiedItem>) => i["uid"] == uid);
            if (data.Children.length > 0) {
                var template = GetBindingTemplateX(me.SelFromHierarchy("#hierarchytreeview"));
                if (template != null) {
                    var html = template.Bind(data, 0, 2);
                    var elements = $.parseHTML(html)
                    elements.forEach((e) => {
                        _Append(target, e);

                    });
                }
            }
        }
        public LoadTableInfo() {
            var me = this;
            var target = <Element>event.target;
            if (_TagName(target).toLowerCase() != "li") {
                target = _Parents(target).AsLinq<Element>().FirstOrDefault(i=> _TagName(i).toLowerCase() == "li");
            }
            var uid = _Attribute(target, "uid");
            var data = Model.Hierarchy.FirstOrDefault(me.TableStructure, (i: Model.Hierarchy<Model.TableInfo>) => i["uid"] == uid);
            if (!IsNull(data) && data.Children.length > 0) {
                var template = GetBindingTemplateX(_SelectFirst("#tabletreeview"));
                if (template != null) {
                    var html = template.Bind(data, 0, 2);
                    var elements = $.parseHTML(html);
                    var ulelement = _Select("ul", target);
                    _Remove(ulelement);
                    //_Html(target, "");
                    elements.forEach((e) => {
                        _Append(target, e);

                    });
                }
            }
        }
        public ClearFilterLabels() {
            $("input[type=text]", "#LabelFilter ").val("");
            $("textarea", "#LabelFilter ").val("");
            this.FilterLabels();
        }

        public FilterLabels() {
            var me = this;
            var f_key: string = _Value( me.SelFromLabel(s_listfilter_selector + " #F_Key")).toLowerCase().trim();
            var f_code: string = _Value(me.SelFromLabel(s_listfilter_selector + " #F_Code")).toLowerCase().trim();
            var f_content: string = _Value(me.SelFromLabel(s_listfilter_selector + " #F_Content")).toLowerCase().trim();
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
            var f_ruleid: string = _Value(me.SelFromValidation(s_listfilter_selector + " #F_RuleID")).toLowerCase().trim();
            var f_ruletext: string = _Value(me.SelFromValidation(s_listfilter_selector + " #F_RuleText")).toLowerCase().trim();
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
            _Value( this.SelFromFact(s_listfilter_selector + " " + "input[type=text]"),"");
            _Value(this.SelFromFact(s_listfilter_selector + " " + "textarea"),"");
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
                if (me.ui_factdetail == null)
                {
                    me.ui_factdetail = me.SelFromFact(s_detail_selector);
                }
                BindX(me.ui_factdetail, fact);
                _Show(me.ui_factdetail);
                app.ShowOnBottomTab(me.ui_factdetail, "#tab_fact");
            } else {
                Notify(Format("Fact {0} was not found!", factkey));
            }


        }

        public HideFactDetails() {
            var me = this;
            _Hide(me.ui_factdetail);
       

        }

        public CloseRuleDetail() {
            var me = this;
            _Hide(me.ui_vruledetail);


        }

        public ShowRuleDetail(ruleid: string) {
            var me = this;
            var previousruleid = _Attribute(me.SelFromValidation(s_parent_selector + " .rule"),"rule-id");
            if (me.ui_vruledetail == null) {
                me.ui_vruledetail = me.SelFromValidation(s_detail_selector);
            }
    
            if (ruleid == previousruleid) {
                _Show(me.ui_vruledetail);
            }
            if (ruleid != previousruleid) {
                var rule = me.Taxonomy.ValidationRules.AsLinq<Model.ValidationRule>().FirstOrDefault(i=> i.ID == ruleid);

                var parent = $(s_parent_selector, me.ui_vruledetail);

                BindX(parent, rule);
                /*
                AjaxRequest("Taxonomy/Validationrule", "get", "json", { id: ruleid }, function (data) {
                    var results = <Model.ValidationRuleResult[]>data;

                    var eventhandlers = {
                        onloading: (data: Model.ValidationRuleResult[]) => {
                            data.forEach(function (ruleresult: Model.ValidationRuleResult) {
                                TaxonomyContainer.SetValues(ruleresult);
                            });
                        }
                    };

                    var list = _SelectFirst(s_sublist_selector, me.ui_vruledetail);
                    var listpager = _SelectFirst(s_sublistpager_selector, me.ui_vruledetail);
                    LoadPage(list, listpager, results, 0, 1, eventhandlers);


                }, function (error) { console.log(error); });
                */
                var list = _SelectFirst(s_sublist_selector, me.ui_vruledetail);
                var listpager = _SelectFirst(s_sublistpager_selector, me.ui_vruledetail);
                LoadPageAsync(list, listpager, app.taxonomycontainer.ValidationResultServiceFunction, 0, 1, { ruleid: ruleid, full:1 });

                $(".trimmed").click(function () {
                    if ($(this).hasClass("hmax30")) {
                        $(this).removeClass("hmax30");
                    } else {
                        $(this).addClass("hmax30");

                    }
                });
                _Show(me.ui_vruledetail);
                app.ShowOnBottomTab(me.ui_vruledetail, "#tab_vrule");

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

                parameter.FactItems = IsNull(parameter.FactItems) ? [] : parameter.FactItems;
                parameter.FactIDs.forEach(function (factrefrence: string, ix:number) {
                    var fi = new Model.FactItem();
                    var factstring = parameter.Facts[ix];
                    var strval = TaxonomyContainer.GetFactValue(factstring);
                    fi.FactString = factstring;
                    fi.Value = strval;
                    fi.Cells = parameter.Cells[ix];
                    var strval = TaxonomyContainer.GetFactValue(factstring);                 
                    parameter.FactItems.push(fi);           
                });
                parameter.FactItems = parameter.FactItems.AsLinq<Model.FactItem>().OrderByDescending(i=> i.Value).ToArray();
                if (parameter.BindAsSequence) {
                    var total = 0;
                    parameter.FactItems.forEach(function (fi: Model.FactItem) {
                        total += Number(fi.Value);
                    });
                    parameter.Value = total.toFixed(2).toString();
                }
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
        }

        public static GetFactValueByID(factid: number): string {
            var fact: Model.InstanceFact = app.instancecontainer.Instance.Facts[factid];
            if (fact != null)
            {
                return fact.Value;

            }
            return "";
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

            if (!IsNull(me.Taxonomy.Hierarchies) && concepts.length > 0) {

                me.ConceptValues = [];
                var htemp = new Model.Hierarchy<Model.QualifiedItem>();
                me.Taxonomy.Hierarchies.Children.forEach(function (hierarchy) {
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
                        var hiers = me.Taxonomy.Hierarchies.Children.AsLinq<Model.Hierarchy<Model.QualifiedItem>>()
                            .Where(i=>
                                i.Item.Role == concept.HierarchyRole
                            );
                        var hier = hiers.FirstOrDefault();
                        if (!IsNull(hier)) {
                            var clkp = new Model.ConceptLookUp();
                            clkp.Concept = Format("{0}:{1}", concept.Namespace, concept.Name);
                            
                            var subhier = Model.Hierarchy.FirstOrDefault(hier, (i:Model.Hierarchy<Model.QualifiedItem>) =>i.Item.Content == concept.Domain.Content)
                            var items = Model.Hierarchy.ToArray(IsNull(subhier) ? hier : subhier);
                            if (subhier == hier) { removeFromArray(items, hier.Item);}

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

        public GetMembersOfHierarchy(layoutrole: string): string
        {
            Log("layoutrole: " + layoutrole);
            var role = TextBetween(layoutrole, "Role:", ";");
            var axis = TextBetween(layoutrole, "Axis:", ";");
            var h = this.Taxonomy.Hierarchies.Children.AsLinq<Model.Hierarchy<Model.QualifiedItem>>().FirstOrDefault(i=> i.Item.Role == role);
            
            if (!IsNull(h)) {
                Log("hierarchy found for DDL!");
                var items = Model.Hierarchy.GetAxis(h, axis);
                var clkp = new Model.ConceptLookUp();

                items.forEach(function (h, index) {
                    var item = h;
                    Model.QualifiedItem.Set(item);
                    if (item.Name != "x0") {
                        var v = {};
                        var id = Format("{0}:{1}", item.Namespace, item.Name);
                        clkp.Values[id] = Format("({0}) {1}", id, item.Label == null ? "" : item.Label.Content);
                    }
                });
                clkp.OptionsHTML = ToOptionList(clkp.Values, true);
                return clkp.OptionsHTML;
            }
            return null;
        }

        public GetMembersOfDomainOptions(dom: string): string
        {
            Log("dom: " + dom);
            var h = this.Taxonomy.Hierarchies.Children.AsLinq<Model.Hierarchy<Model.QualifiedItem>>().FirstOrDefault(i=> i.Item.Label.LocalID == dom);
            if (!IsNull(h))
            {
                Log("hierarchy found for DDL!");
                var items = h.Children.length > 0 ? h.Children[0].Children:h.Children;
                var clkp = new Model.ConceptLookUp();

                items.forEach(function (h, index) {
                    var item = h.Item;
                    Model.QualifiedItem.Set(item);
                    if (item.Name != "x0") {
                        var v = {};
                        var id = Format("{0}:{1}", item.Namespace, item.Name);
                        clkp.Values[id] = Format("({0}) {1}", id, item.Label == null ? "" : item.Label.Content);
                    }
                });
                clkp.OptionsHTML = ToOptionList(clkp.Values, true);
                return clkp.OptionsHTML;
            }
            return null;

        }
    }
}
var s_tableframe_selector: string = "#ReportContainer";

