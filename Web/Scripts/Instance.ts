﻿module Control
{
    export class InstanceContainer {
        public Instance: Model.Instance = null;
        public Taxonomy: Model.Taxonomy = null;
        public ValidationResults: Model.ValidationRuleResult[] = [];
        public ValidationErrors: Model.ValidationRule[] = [];
        public FactsNr: number = 0;
        public Page: number = 0;
        public PageSize: number = 20;
        public VPageSize: number = 20;

      

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

        private ui_factdetail: Element = null;
        private ui_vruledetail: Element = null;

        private ValidationResultsServiceFunction: General.FunctionWithCallback = null;

        constructor() {
            this.s_fact_selector = "#" + this.s_fact_id;
            this.s_validation_selector = "#" + this.s_validation_id;
            this.s_unit_selector = "#" + this.s_units_id;
            this.s_find_selector = "#" + this.s_find_id;
            this.s_general_selector = "#" + this.s_general_id;
        }

        public Sel(selector: any): Element
        {
            var me = this;
            return _SelectFirst(selector, _SelectFirst("#" + me.s_main_id));
        }

        public SelFrom(parentselector: any, selector: any): Element {
            var me = this;
            return _SelectFirst(selector, me.Sel(parentselector));
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
            me.Taxonomy = app.taxonomycontainer.Taxonomy;

            me.LoadInstance(null);

            me.LoadValidationResults(null);

            me.ValidationResultsServiceFunction = new General.FunctionWithCallback(
                (fwc: General.FunctionWithCallback, args: any) => {
                    var p = <Model.Dictionary<any>>args[0];
                    AjaxRequest("Instance/Validation", "get", "json", p,
                        function (data: DataResult) {
                            if (!IsNull(data.Items)) {
                                me.ValidationResults = <Model.ValidationRuleResult[]>data.Items;
                                fwc.Callback(data);
                            }
                        }, null);
                });
        }

        public HandleAction(msg: General.Message)
        {
            var me = this;
            var action = msg.Data.toLowerCase();
            if (action == "instancevalidated")
            {
                me.LoadValidationResults(function () {
                    var item = "#" + me.s_validation_id;
                    LoadTab("#MainContainer", "#InstanceContainer");
                    LoadTab("#InstanceContainer", item);
                    me.LoadContentToUIX(me.s_validation_id, null);
                });
            }
        }

        public LoadValidationResults(onloaded:Function)
        {
            var me = this;
            AjaxRequest("Instance/Validation", "get", "json", null, function (data) {
                me.ValidationResults = data;
                CallFunction(onloaded);
            }, function (error) { console.log(error); });
        }

        public LoadInstance(onloaded: Function) {
            var me = this;
            AjaxRequest("Instance/Get", "get", "json", null, function (data) {
                me.Instance = data;
                var instdata = <Model.Instance>data;

                var ifd = new Model.InstanceFactDictionary();
                ifd.FactsByIndex = instdata.FactDictionary.FactsByIndex;
                ifd.FactsByInstanceKey = instdata.FactDictionary.FactsByInstanceKey;
                ifd.FactsByTaxonomyKey = instdata.FactDictionary.FactsByTaxonomyKey;
                ifd.GetIntKey = (s) => me.GetFactKeyStringFromFactString(s);
                me.Instance.FactDictionary = ifd;
                me.LoadToUI();
                CallFunction(onloaded);

            }, function (error) { console.log(error); });
        }
        public GetFactKeyStringFromFactString(factstring: string): string
        {
            var keys = this.GetFactKeyFromFactString(factstring);
            return keys.join();
        }
        public GetFactKeyFromFactString(factstring: string): number[] {
            var me = this;
            var result = [];
            var parts = factstring.split(',');
            parts.forEach(
                (part) => {
                    var val = me.GetFactPartKeyFromString(part);
                    if (!IsNull(val)) {
                        result.push(val);
                    }
                });
            return result;
        }
        public GetFactKeyFromFact(fact: Model.FactBase): number[]
        {
            var me = this;
            var result = [];
            result.push(me.GetFactPartKeyFromString(fact.Concept.Content));
            fact.Dimensions.forEach(
                (dimension) => {
                    result.push(me.GetFactPartKeyFromString(dimension.DomainMember));
                });
            return result;
        }

        public GetFactKeyFromString(parts: string[]): number[]
        {
            var me = this;
            var result = [];
            parts.forEach(
                (part) => {
                    result.push(me.GetFactPartKeyFromString(part));
                });
            return result;
        }
        public GetFactStringFromKey(parts: string[]): string[] {
            var me = this;
            var result = [];
            parts.forEach(
                (part) => {
                    result.push(me.GetFactPartFromKey(part));
                });
            return result;
        }
        public GetFactPartKeyFromString(part:string):number
        {
            var me = this;
            var strpart = me.Taxonomy.FactParts[part];
            if (IsNull(strpart)) {
                strpart = me.Instance.FactParts[part];
            }
            return strpart;
        }
        public GetFactPartFromKey(part: string): string {
            var me = this;
            var strpart = me.Taxonomy.CounterFactParts[part];
            if (IsNull(strpart)) {
                strpart = me.Instance.CounterFactParts[part];
            }
            return strpart;
        }
        public GetFactFor(cellfact: Model.FactBase, cellid: string): Model.InstanceFact {
            var me = this;
            var facts: Model.InstanceFact[] = [];
            var fact: Model.InstanceFact = null;
            var factintkey = me.GetFactKeyFromFactString(cellfact.FactString).join();
            var factkey = Model.FactBase.GetFactKey(cellfact);
            var factstring = cellfact.FactString;
            if ( me.Instance.FactDictionary.ContainsKey(factintkey)) {
                facts = me.Instance.FactDictionary.GetFacts(factintkey);
                if (facts.length > 0) {
                    fact = facts.AsLinq<Model.InstanceFact>().FirstOrDefault(i=> i.FactString == factstring);
                }
            }
            return fact;
        }
        public LoadToUI()
        {
            var me = this;
            //me.Sel(s_detail_selector).hide();
            //me.LoadTab("#MainContainer", "#InstanceContainer");
            //me.LoadTab("#InstanceContainer", "#" + me.s_fact_id);

            me.LoadContentToUIX(me.s_fact_id, null);
            
            //ShowContentByID("#TaxonomyContainer");

            var facts: Model.FactBase[] = [];
            var dict = me.Instance.FactDictionary.FactsByInstanceKey;
            if (IsNull(me.Instance.Facts))
            {       
                me.Instance.Facts = [];
            }
            me.Instance.CounterFactParts = {};
            GetProperties(me.Instance.FactParts).forEach(
                (item, ix) => {
                    me.Instance.CounterFactParts[item.Value] = item.Key;
                });

         
            for (var key in dict)
            {
                if (dict.hasOwnProperty(key)) {
                    var factlist = me.Instance.FactDictionary.GetFacts(key);
                    for (var i = 0; i < factlist.length; i++)
                    {
                        var fact = factlist[i];
                        fact.FactString = "";
                        var parts:string[] = key.split(',');
                        parts.forEach((part,ix) =>
                        {
                            var strpart = me.GetFactPartFromKey(part);
                             
                            if (ix == 0)
                            {
                                var conceptstring = strpart.indexOf("[") == -1 ? strpart : "";
                                fact.Concept = new Model.Concept();
                                fact.Concept.FullName = conceptstring;
                            }
                            fact.FactString += strpart + ",";
                        });
                        fact.FactKey = key;
            
                        this.Instance.Facts.push(fact);
                    }
                }
            }

            if (app.taxonomycontainer.Table != null)
            {
                app.taxonomycontainer.Table.Instance = me.Instance;
            }
            me.FactsNr = this.Instance.Facts.length;
            LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.Instance.Facts, 0, me.PageSize);

        }

        public ClearFilterFacts()
        {
            _Value(this.SelFromFact(s_listfilter_selector + " " + "input[type=text]"), "");
            _Value(this.SelFromFact(s_listfilter_selector + " " + "textarea"), "");
            this.FilterFacts();
        }

        private GetFilterValue(selector: string): string {
            var element = this.SelFromFact(s_listfilter_selector + " " + selector);
            if (!IsNull(element)) {
                return _Value(element).toLowerCase().trim();
            }
            return "";
        }

        public FilterFacts()
        {
            var me = this;
            var f_context: string = me.GetFilterValue("#F_Context");
            var f_factstring: string = me.GetFilterValue("#F_FactString");
            var f_value: string = me.GetFilterValue("#F_Value");
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

            if (!IsNull(f_factstring)) {
                var factparts = f_factstring.split(" ");
                factparts.forEach(function (factpart, ix) {
                    if (!IsNull(factpart)) {
                        query = query.Where(i=> i.FactString.toLowerCase().indexOf(factpart) > -1);
                    }
                });
            }
            if (!IsNull(f_value)) {
                query = query.Where(i=> i.Value.toLowerCase()==f_value);
            }
            var eventhandlers = { onpaging: () => { me.CloseFactDetails(); } };
      
            LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), query.ToArray(), 0, me.PageSize, eventhandlers);
        }

        public ShowFactDetails(factkey:string, factstring:string)
        {
            var me = this;
            if (!IsNull(me.Instance)) {
                var facts: Model.InstanceFact[] = this.Instance.FactDictionary.GetFacts(factkey);

                if (facts != null && facts.length > 0) {

                    var instancefact = facts.AsLinq<Model.InstanceFact>().FirstOrDefault(i=> i.FactString == factstring);
                    Model.FactBase.LoadFromFactString(instancefact);
                    var fullfactstring = Model.FactBase.GetFullFactString(instancefact);
                    for (var i = 0; i < instancefact.Cells.length; i++){
                        var cell = instancefact.Cells[i];

                        var cellobj = new Model.Cell();
                        cellobj.SetFromCellID(cell);
                        var reportid = cellobj.Report;
                        var dynmicdata = me.Instance.DynamicReportCells[reportid];
                        if (IsNull(cellobj.Column) || IsNull(cellobj.Row) || IsNull(cellobj.Extension))
                        {
                            var celldictionary = IsNull(dynmicdata) ? null : dynmicdata.CellOfFact;
                            if (!IsNull(celldictionary)) {
                                cellobj.SetFromCellID(celldictionary[instancefact.FactString]);

                            }
                        }
        
                        instancefact.Cells[i] = cellobj.CellID;

                    };
                    Model.FactBase.LoadFromFactString(instancefact);
                    instancefact.Dimensions.forEach(function (dimension, ix) {
                        SetProperty(dimension, "DomainMemberFullName", Model.Dimension.DomainMemberFullName(dimension));
                    });
                    if (me.ui_factdetail == null) {
                        me.ui_factdetail = me.SelFromFact(s_detail_selector);
                    }
                    BindX(me.ui_factdetail, instancefact);
                    _Show(me.ui_factdetail);
                    app.ShowOnBottomTab(me.ui_factdetail, "#tab_fact");
                }
            }

        }

        public CloseFactDetails()
        {
            var me = this;
            _Hide(me.ui_factdetail);

        }

        public ShowValidationResults()
        {

            var me = this;
            var rule: Model.ValidationRule = null;
            this.ValidationErrors = [];
            me.CloseRuleDetail();
            if (!IsNull(me.ValidationResults)) {
                me.ValidationResults.forEach(function (v) {
                    if (IsNull(rule) || rule.ID != v.ID) {
                        var tax_rule = me.Taxonomy.ValidationRules.AsLinq<Model.ValidationRule>().FirstOrDefault(i=> i.ID == v.ID);
                        rule = new Model.ValidationRule();
                        rule.ID = tax_rule.ID;
                        rule.FunctionName = tax_rule.FunctionName;
                        rule.Title = Truncate(tax_rule.DisplayText, 100)
                        rule.DisplayText = tax_rule.DisplayText;
                        rule.OriginalExpression = tax_rule.OriginalExpression;
                        rule.HasAllFind = v.HasAllFind;
                        me.ValidationErrors.push(rule);
                    }
                    if (!IsNull(rule)) {
                        rule.Results.push(v);
                        //TaxonomyContainer.SetValues(v);

                    }

                });
                var eventhandlers = {
                    onpaging: () => { me.CloseRuleDetail(); },
                    onloading: (data: Model.ValidationRuleResult[]) => {
                        //data.forEach(function (ruleresult: Model.ValidationRuleResult) {
                        //    TaxonomyContainer.SetValues(ruleresult);
                        //});
                    }
                }
                LoadPage(me.SelFromValidation(s_list_selector), me.SelFromValidation(s_listpager_selector), me.ValidationErrors, 0, me.VPageSize, eventhandlers);
         
            }

        }

        public LoadContentToUI(sender: any)
        {
            var me = this;        
            var target = GetHashPart(_Attribute(sender, "href"));

            me.LoadContentToUIX(target, sender);
        }

        public LoadContentToUIX(contentid: string, sender: any)
        {
            var me = this;
            var eventhandlers = {};
            var text = $(sender).text();
            _AddClass(_SelectFirst("#colgroup1"), "wide");
       
            if (contentid == "Instance")
            {

            }
            if (contentid == me.s_fact_id && text.toLowerCase().indexOf("invalid") == -1){
                eventhandlers = { onpaging: () => { me.CloseFactDetails(); } };
                LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.Instance.Facts, 0, me.PageSize, eventhandlers);

            }
            if (contentid == me.s_fact_id && text.toLowerCase().indexOf("invalid")>-1) {
                var invalidfacts = me.Instance.Facts.AsLinq<Model.InstanceFact>().Where(i=> i.Cells.length == 0).ToArray();
                eventhandlers = { onpaging: () => { me.CloseFactDetails(); } };
                LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), invalidfacts, 0, me.PageSize, eventhandlers);

            }
            if (contentid == me.s_validation_id) {
                me.ShowValidationResults()
            }


            if (contentid == me.s_units_id) {

                LoadPage(_SelectFirst(s_list_selector, me.s_unit_selector), _SelectFirst(s_listpager_selector, me.s_unit_selector),
                    me.Instance.Units, 0, me.PageSize, eventhandlers);

            }
            if (contentid == me.s_find_id) {
                LoadPage(_SelectFirst(s_list_selector, me.s_find_selector), _SelectFirst(s_listpager_selector, me.s_find_selector),
                    me.Instance.FilingIndicators, 0, me.PageSize, eventhandlers);
            }
            if (contentid == me.s_general_id) {
                BindX(me.Sel(me.s_general_selector), me.Instance);

            }
        }

        public ShowRuleDetail(ruleid: string)
        {
     
            var me = this;
            var previousruleid = _Attribute(me.SelFromValidation(s_parent_selector + " .rule"), "rule-id");
            if (me.ui_vruledetail == null) {
                me.ui_vruledetail = me.SelFromValidation(s_detail_selector);
            }

            if (ruleid == previousruleid) {
                _Show(me.ui_vruledetail);
            }
            if (ruleid != previousruleid) {
                var rule = this.ValidationErrors.AsLinq<Model.ValidationRule>().FirstOrDefault(i=> i.ID == ruleid);

                var parent = $(s_parent_selector, me.ui_vruledetail);

                BindX(parent, rule);
                var list = _SelectFirst(s_sublist_selector, me.ui_vruledetail);
                var listpager = _SelectFirst(s_sublistpager_selector, me.ui_vruledetail);
                //LoadPage(list, listpager, rule.Results, 0, 1);

                LoadPageAsync(list, listpager, app.taxonomycontainer.ValidationResultServiceFunction, 0, 1, { ruleid: ruleid });
                

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

        public CloseRuleDetail()
        {
            var me = this;
            _Hide(me.ui_vruledetail);

        }

        public HashChanged()
        {
       
        }

        
    

    }
}
