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
            this.ui_factdetail = null;
            this.ui_vruledetail = null;
            this.ValidationResultsServiceFunction = null;
            this.s_fact_selector = "#" + this.s_fact_id;
            this.s_validation_selector = "#" + this.s_validation_id;
            this.s_unit_selector = "#" + this.s_units_id;
            this.s_find_selector = "#" + this.s_find_id;
            this.s_general_selector = "#" + this.s_general_id;
        }
        InstanceContainer.prototype.Sel = function (selector) {
            var me = this;
            return _SelectFirst(selector, _SelectFirst("#" + me.s_main_id));
        };
        InstanceContainer.prototype.SelFrom = function (parentselector, selector) {
            var me = this;
            return _SelectFirst(selector, me.Sel(parentselector));
        };
        InstanceContainer.prototype.SelFromFact = function (selector) {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_fact_selector));
        };
        InstanceContainer.prototype.SelFromValidation = function (selector) {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_validation_selector));
        };
        InstanceContainer.prototype.SetExternals = function () {
            var me = this;
            me.Taxonomy = app.taxonomycontainer.Taxonomy;
            me.LoadInstance(null);
            me.LoadValidationResults(null);
            me.ValidationResultsServiceFunction = new General.FunctionWithCallback(function (fwc, args) {
                var p = args[0];
                AjaxRequest("Instance/Validation", "get", "json", p, function (data) {
                    if (!IsNull(data.Items)) {
                        me.ValidationResults = data.Items;
                        fwc.Callback(data);
                    }
                }, null);
            });
        };
        InstanceContainer.prototype.HandleAction = function (msg) {
            var me = this;
            var action = msg.Data.toLowerCase();
            if (action == "instancevalidated") {
                me.LoadValidationResults(function () {
                    var item = "#" + me.s_validation_id;
                    LoadTab("#MainContainer", "#InstanceContainer");
                    LoadTab("#InstanceContainer", item);
                    me.LoadContentToUIX(me.s_validation_id, null);
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
                var instdata = data;
                var ifd = new Model.InstanceFactDictionary();
                ifd.FactsByIndex = instdata.FactDictionary.FactsByIndex;
                ifd.FactsByInstanceKey = instdata.FactDictionary.FactsByInstanceKey;
                ifd.FactsByTaxonomyKey = instdata.FactDictionary.FactsByTaxonomyKey;
                ifd.GetIntKey = function (s) { return me.GetFactKeyStringFromFactString(s); };
                me.Instance.FactDictionary = ifd;
                me.LoadToUI();
                CallFunction(onloaded);
            }, function (error) {
                console.log(error);
            });
        };
        InstanceContainer.prototype.GetFactKeyStringFromFactString = function (factstring) {
            var keys = this.GetFactKeyFromFactString(factstring);
            return keys.join();
        };
        InstanceContainer.prototype.GetFactKeyFromFactString = function (factstring) {
            var me = this;
            var result = [];
            var parts = factstring.split(',');
            parts.forEach(function (part) {
                var val = me.GetFactPartKeyFromString(part);
                if (!IsNull(val)) {
                    result.push(val);
                }
            });
            return result;
        };
        InstanceContainer.prototype.GetFactKeyFromFact = function (fact) {
            var me = this;
            var result = [];
            result.push(me.GetFactPartKeyFromString(fact.Concept.Content));
            fact.Dimensions.forEach(function (dimension) {
                result.push(me.GetFactPartKeyFromString(dimension.DomainMember));
            });
            return result;
        };
        InstanceContainer.prototype.GetFactKeyFromString = function (parts) {
            var me = this;
            var result = [];
            parts.forEach(function (part) {
                result.push(me.GetFactPartKeyFromString(part));
            });
            return result;
        };
        InstanceContainer.prototype.GetFactStringFromKey = function (parts) {
            var me = this;
            var result = [];
            parts.forEach(function (part) {
                result.push(me.GetFactPartFromKey(part));
            });
            return result;
        };
        InstanceContainer.prototype.GetFactPartKeyFromString = function (part) {
            var me = this;
            var strpart = me.Taxonomy.FactParts[part];
            if (IsNull(strpart)) {
                strpart = me.Instance.FactParts[part];
            }
            return strpart;
        };
        InstanceContainer.prototype.GetFactPartFromKey = function (part) {
            var me = this;
            var strpart = me.Taxonomy.CounterFactParts[part];
            if (IsNull(strpart)) {
                strpart = me.Instance.CounterFactParts[part];
            }
            return strpart;
        };
        InstanceContainer.prototype.GetFactFor = function (cellfact, cellid) {
            var me = this;
            var facts = [];
            var fact = null;
            var factintkey = me.GetFactKeyFromFactString(cellfact.FactString).join();
            var factkey = Model.FactBase.GetFactKey(cellfact);
            var factstring = cellfact.FactString;
            if (me.Instance.FactDictionary.ContainsKey(factintkey)) {
                facts = me.Instance.FactDictionary.GetFacts(factintkey);
                if (facts.length > 0) {
                    fact = facts.AsLinq().FirstOrDefault(function (i) { return i.FactString == factstring; });
                }
            }
            return fact;
        };
        InstanceContainer.prototype.LoadToUI = function () {
            var me = this;
            //me.Sel(s_detail_selector).hide();
            //me.LoadTab("#MainContainer", "#InstanceContainer");
            //me.LoadTab("#InstanceContainer", "#" + me.s_fact_id);
            me.LoadContentToUIX(me.s_fact_id, null);
            //ShowContentByID("#TaxonomyContainer");
            var facts = [];
            var dict = me.Instance.FactDictionary.FactsByInstanceKey;
            if (IsNull(me.Instance.Facts)) {
                me.Instance.Facts = [];
            }
            me.Instance.CounterFactParts = {};
            GetProperties(me.Instance.FactParts).forEach(function (item, ix) {
                me.Instance.CounterFactParts[item.Value] = item.Key;
            });
            for (var key in dict) {
                if (dict.hasOwnProperty(key)) {
                    var factlist = me.Instance.FactDictionary.GetFacts(key);
                    for (var i = 0; i < factlist.length; i++) {
                        var fact = factlist[i];
                        fact.FactString = "";
                        var parts = key.split(',');
                        parts.forEach(function (part, ix) {
                            var strpart = me.GetFactPartFromKey(part);
                            if (ix == 0) {
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
            if (app.taxonomycontainer.Table != null) {
                app.taxonomycontainer.Table.Instance = me.Instance;
            }
            me.FactsNr = this.Instance.Facts.length;
            LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.Instance.Facts, 0, me.PageSize);
        };
        InstanceContainer.prototype.ClearFilterFacts = function () {
            _Value(this.SelFromFact(s_listfilter_selector + " " + "input[type=text]"), "");
            _Value(this.SelFromFact(s_listfilter_selector + " " + "textarea"), "");
            this.FilterFacts();
        };
        InstanceContainer.prototype.GetFilterValue = function (selector) {
            var element = this.SelFromFact(s_listfilter_selector + " " + selector);
            if (!IsNull(element)) {
                return _Value(element).toLowerCase().trim();
            }
            return "";
        };
        InstanceContainer.prototype.FilterFacts = function () {
            var me = this;
            var f_context = me.GetFilterValue("#F_Context");
            var f_factstring = me.GetFilterValue("#F_FactString");
            var f_value = me.GetFilterValue("#F_Value");
            var context_id = "";
            var context_xml = "";
            var query = me.Instance.Facts.AsLinq();
            if (f_context.indexOf(">") > -1 || f_context.indexOf("\n") > -1) {
            }
            else {
                context_id = f_context;
                query = query.Where(function (i) { return i.ContextID.toLowerCase().indexOf(context_id) > -1; });
            }
            if (!IsNull(f_factstring)) {
                var factparts = f_factstring.split(" ");
                factparts.forEach(function (factpart, ix) {
                    if (!IsNull(factpart)) {
                        query = query.Where(function (i) { return i.FactString.toLowerCase().indexOf(factpart) > -1; });
                    }
                });
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
                var facts = this.Instance.FactDictionary.GetFacts(factkey);
                if (facts != null && facts.length > 0) {
                    var instancefact = facts.AsLinq().FirstOrDefault(function (i) { return i.FactString == factstring; });
                    Model.FactBase.LoadFromFactString(instancefact);
                    var fullfactstring = Model.FactBase.GetFullFactString(instancefact);
                    for (var i = 0; i < instancefact.Cells.length; i++) {
                        var cell = instancefact.Cells[i];
                        var cellobj = new Model.Cell();
                        cellobj.SetFromCellID(cell);
                        var reportid = cellobj.Report;
                        var dynmicdata = me.Instance.DynamicReportCells[reportid];
                        if (IsNull(cellobj.Column) || IsNull(cellobj.Row) || IsNull(cellobj.Extension)) {
                            var celldictionary = IsNull(dynmicdata) ? null : dynmicdata.CellOfFact;
                            if (!IsNull(celldictionary)) {
                                cellobj.SetFromCellID(celldictionary[instancefact.FactString]);
                            }
                        }
                        instancefact.Cells[i] = cellobj.CellID;
                    }
                    ;
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
        };
        InstanceContainer.prototype.CloseFactDetails = function () {
            var me = this;
            _Hide(me.ui_factdetail);
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
            var target = GetHashPart(_Attribute(sender, "href"));
            me.LoadContentToUIX(target, sender);
        };
        InstanceContainer.prototype.LoadContentToUIX = function (contentid, sender) {
            var me = this;
            var eventhandlers = {};
            var text = $(sender).text();
            _AddClass(_SelectFirst("#colgroup1"), "wide");
            if (contentid == "Instance") {
            }
            if (contentid == me.s_fact_id && text.toLowerCase().indexOf("invalid") == -1) {
                eventhandlers = { onpaging: function () {
                    me.CloseFactDetails();
                } };
                LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.Instance.Facts, 0, me.PageSize, eventhandlers);
            }
            if (contentid == me.s_fact_id && text.toLowerCase().indexOf("invalid") > -1) {
                var invalidfacts = me.Instance.Facts.AsLinq().Where(function (i) { return i.Cells.length == 0; }).ToArray();
                eventhandlers = { onpaging: function () {
                    me.CloseFactDetails();
                } };
                LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), invalidfacts, 0, me.PageSize, eventhandlers);
            }
            if (contentid == me.s_validation_id) {
                me.ShowValidationResults();
            }
            if (contentid == me.s_units_id) {
                LoadPage(_SelectFirst(s_list_selector, me.s_unit_selector), _SelectFirst(s_listpager_selector, me.s_unit_selector), me.Instance.Units, 0, me.PageSize, eventhandlers);
            }
            if (contentid == me.s_find_id) {
                LoadPage(_SelectFirst(s_list_selector, me.s_find_selector), _SelectFirst(s_listpager_selector, me.s_find_selector), me.Instance.FilingIndicators, 0, me.PageSize, eventhandlers);
            }
            if (contentid == me.s_general_id) {
                BindX(me.Sel(me.s_general_selector), me.Instance);
            }
        };
        InstanceContainer.prototype.ShowRuleDetail = function (ruleid) {
            var me = this;
            var previousruleid = _Attribute(me.SelFromValidation(s_parent_selector + " .rule"), "rule-id");
            if (me.ui_vruledetail == null) {
                me.ui_vruledetail = me.SelFromValidation(s_detail_selector);
            }
            if (ruleid == previousruleid) {
                _Show(me.ui_vruledetail);
            }
            if (ruleid != previousruleid) {
                var rule = this.ValidationErrors.AsLinq().FirstOrDefault(function (i) { return i.ID == ruleid; });
                var parent = $(s_parent_selector, me.ui_vruledetail);
                BindX(parent, rule);
                var list = _SelectFirst(s_sublist_selector, me.ui_vruledetail);
                var listpager = _SelectFirst(s_sublistpager_selector, me.ui_vruledetail);
                //LoadPage(list, listpager, rule.Results, 0, 1);
                LoadPageAsync(list, listpager, app.taxonomycontainer.ValidationResultServiceFunction, 0, 1, { ruleid: ruleid });
                $(".trimmed").click(function () {
                    if ($(this).hasClass("hmax30")) {
                        $(this).removeClass("hmax30");
                    }
                    else {
                        $(this).addClass("hmax30");
                    }
                });
                _Show(me.ui_vruledetail);
                app.ShowOnBottomTab(me.ui_vruledetail, "#tab_vrule");
            }
        };
        InstanceContainer.prototype.CloseRuleDetail = function () {
            var me = this;
            _Hide(me.ui_vruledetail);
        };
        InstanceContainer.prototype.HashChanged = function () {
        };
        return InstanceContainer;
    })();
    Control.InstanceContainer = InstanceContainer;
})(Control || (Control = {}));
//# sourceMappingURL=Instance.js.map