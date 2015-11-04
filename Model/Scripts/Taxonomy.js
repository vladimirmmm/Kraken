var Control;
(function (Control) {
    var TaxonomyContainer = (function () {
        function TaxonomyContainer() {
            this.Taxonomy = new Model.Taxonomy();
            this.Table = new UI.Table();
            this.ValidationRules = [];
            this.TableStructure = null;
            this.CurrentFacts = null;
            this.LPageSize = 10;
            this.PageSize = 20;
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
            this.s_validation_selector = "";
            this.s_units_selector = "";
            this.s_find_selector = "";
            this.s_general_selector = "";
            this.FactServiceFunction = null;
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
            $(window).on('hashchange', function () {
                me.HashChanged();
            });
        }
        TaxonomyContainer.prototype.HashChanged = function () {
            var me = this;
            me.Table.HashChanged();
        };
        TaxonomyContainer.prototype.SetHeight = function () {
            var bodyheight = $(window).height();
            var pivotheight = (bodyheight - 50) + "px";
            $(".pivotitem").css("max-height", pivotheight);
        };
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
        TaxonomyContainer.prototype.SelFromValidation = function (selector) {
            var me = this;
            return $(selector, me.Sel(me.s_validation_selector));
        };
        TaxonomyContainer.prototype.SetExternals = function () {
            var me = this;
            me.SetHeight();
            me.Table.Taxonomy = me.Taxonomy;
            AjaxRequest("Taxonomy/Get", "get", "json", null, function (data) {
                me.Taxonomy.Module = data;
                BindX($("#TaxonomyInfo"), me.Taxonomy.Module);
                BindX($("#TaxonomyGeneral"), me.Taxonomy.Module);
            }, function (error) {
                console.log(error);
            });
            AjaxRequest("Taxonomy/Tables", "get", "json", null, function (data) {
                me.TableStructure = data;
                ForAll(me.TableStructure, "Children", function (item) {
                    if (!IsNull(item) && !IsNull(item.Item)) {
                        item.Item.CssClass = item.Item.Type == "table" ? "hidden" : "";
                        item.Item.ExtensionText = item.Item.Type == "table" && item.Children.length > 0 ? Format("({0})", item.Children.length) : "";
                    }
                });
                BindX($("#tabletreeview"), me.TableStructure);
            }, function (error) {
                console.log(error);
            });
            AjaxRequest("Taxonomy/Concepts", "get", "json", null, function (data) {
                me.Taxonomy.Concepts = data;
            }, function (error) {
                console.log(error);
            });
            me.LoadValidationResults(null);
            AjaxRequest("Taxonomy/Hierarchies", "get", "json", null, function (data) {
                me.Taxonomy.Hierarchies = data;
            }, function (error) {
                console.log(error);
            });
            AjaxRequest("Taxonomy/Labels", "get", "json", null, function (data) {
                me.Taxonomy.Labels = data;
            }, function (error) {
                console.log(error);
            });
            me.FactServiceFunction = new General.FunctionWithCallback(function (fwc, args) {
                var p = args[0];
                AjaxRequest("Taxonomy/Facts", "get", "json", p, function (data) {
                    me.CurrentFacts = data.Items;
                    fwc.Callback(data);
                }, null);
            });
        };
        TaxonomyContainer.prototype.LoadValidationResults = function (onloaded) {
            var me = this;
            AjaxRequest("Taxonomy/ValidationRules", "get", "json", null, function (data) {
                me.Taxonomy.ValidationRules = data;
                me.Taxonomy.ValidationRules.forEach(function (v) {
                    v.Title = Truncate(v.DisplayText, 100);
                });
                CallFunction(onloaded);
            }, function (error) {
                console.log(error);
            });
        };
        TaxonomyContainer.prototype.ShowValidationResults = function () {
            var me = this;
            var eventhandlers = { onpaging: function () {
                me.CloseRuleDetail();
            } };
            me.CloseRuleDetail();
            LoadPage(me.SelFromValidation(s_list_selector), me.SelFromValidation(s_listpager_selector), me.Taxonomy.ValidationRules, 0, me.PageSize, eventhandlers);
            $(".trimmed").click(function () {
                if ($(this).hasClass("hmax30")) {
                    $(this).removeClass("hmax30");
                }
                else {
                    $(this).addClass("hmax30");
                }
            });
        };
        TaxonomyContainer.prototype.LoadContentToUI = function (sender) {
            var me = this;
            ShowContentBySender(sender);
            var target = $(sender).attr("activator-for");
            me.LoadContentToUIX(target, sender);
        };
        TaxonomyContainer.prototype.LoadContentToUIX = function (contentid, sender) {
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
                    onloading: function (data) {
                        //EnumerateObject(data, me,(item, itemname) => { item["PropertyName"] = itemname; });
                    }
                };
                LoadPageAsync(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.FactServiceFunction, 0, me.PageSize, eventhandlers);
            }
        };
        TaxonomyContainer.prototype.ClearFilterLabels = function () {
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
            LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), query.ToArray(), 0, me.LPageSize);
        };
        TaxonomyContainer.prototype.ClearFilterValidations = function () {
            var me = this;
            var $filtercontainer = me.SelFromValidation(s_listfilter_selector);
            $("input[type=text]", $filtercontainer).val("");
            $("textarea", $filtercontainer).val("");
            me.FilterValidations();
        };
        TaxonomyContainer.prototype.FilterValidations = function () {
            var me = this;
            var f_ruleid = me.SelFromValidation(s_listfilter_selector + " #F_RuleID").val().toLowerCase().trim();
            var f_ruletext = me.SelFromValidation(s_listfilter_selector + " #F_RuleText").val().toLowerCase().trim();
            var query = me.Taxonomy.ValidationRules.AsLinq();
            if (!IsNull(f_ruletext)) {
                query = query.Where(function (i) { return i.DisplayText.toLowerCase().indexOf(f_ruletext) > -1; });
            }
            if (!IsNull(f_ruleid)) {
                query = query.Where(function (i) { return i.ID.indexOf(f_ruleid) > -1; });
            }
            var eventhandlers = { onpaging: function () {
                me.CloseRuleDetail();
            } };
            me.CloseRuleDetail();
            LoadPage(me.SelFromValidation(s_list_selector), me.SelFromValidation(s_listpager_selector), query.ToArray(), 0, me.LPageSize, eventhandlers);
        };
        TaxonomyContainer.prototype.ClearFilterFacts = function () {
            var me = this;
            $("input[type=text]", me.SelFromFact(s_listfilter_selector)).val("");
            $("textarea", me.SelFromFact(s_listfilter_selector)).val("");
            this.FilterFacts();
        };
        TaxonomyContainer.prototype.FilterFacts = function () {
            var me = this;
            var f_factstring = me.SelFromFact(s_listfilter_selector + " #F_FactString").val().toLowerCase().trim();
            var f_cellid = me.SelFromFact(s_listfilter_selector + " #F_CellID").val().toLowerCase().trim();
            var query = me.Taxonomy.Facts;
            if (!IsNull(f_factstring)) {
                var results = [];
                EnumerateObject(query, me, function (value, key) {
                    if (key.toLowerCase().indexOf(f_factstring) > -1) {
                        results.push(value);
                    }
                });
                query = results;
            }
            if (!IsNull(f_cellid)) {
            }
            var parameters = { factstring: f_factstring, cellid: f_cellid };
            LoadPageAsync(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.FactServiceFunction, 0, me.PageSize, parameters, null);
            //LoadPage(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), query, 0, me.PageSize);
            me.HideFactDetails();
        };
        TaxonomyContainer.prototype.ShowFactDetails = function (factkey, factstring) {
            var me = this;
            var factx = me.CurrentFacts.AsLinq().FirstOrDefault(function (i) { return i.Key == factkey; });
            if (!IsNull(factx)) {
                var cells = factx.Value;
                var fact = new Model.InstanceFact();
                fact.FactString = factkey;
                Model.FactBase.LoadFromFactString(fact);
                fact.Cells = cells;
                var $factdetail = me.SelFromFact(s_detail_selector);
                BindX($factdetail, fact);
                $factdetail.show();
            }
            else {
                Notify(Format("Fact {0} was not found!", factkey));
            }
        };
        TaxonomyContainer.prototype.HideFactDetails = function () {
            var me = this;
            var $factdetail = me.SelFromFact(s_detail_selector);
            $factdetail.hide();
        };
        TaxonomyContainer.prototype.CloseRuleDetail = function () {
            var me = this;
            me.SelFromValidation(s_detail_selector).hide();
            $(me.s_validation_selector).append(me.SelFromValidation(s_detail_selector));
        };
        TaxonomyContainer.prototype.ShowRuleDetail = function (ruleid) {
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
                var rule = me.Taxonomy.ValidationRules.AsLinq().FirstOrDefault(function (i) { return i.ID == ruleid; });
                BindX(me.SelFromValidation(s_parent_selector), rule);
                AjaxRequest("Taxonomy/Validationrule", "get", "json", { id: ruleid }, function (data) {
                    var results = data;
                    var eventhandlers = {
                        onloading: function (data) {
                            data.forEach(function (ruleresult) {
                                TaxonomyContainer.SetValues(ruleresult);
                            });
                        }
                    };
                    LoadPage(me.SelFromValidation(s_sublist_selector), me.SelFromValidation(s_sublistpager_selector), results, 0, 1, eventhandlers);
                }, function (error) {
                    console.log(error);
                });
                me.SelFromValidation(".validationrule_results_" + ruleid).append($valdetail);
                $(".trimmed").click(function () {
                    if ($(this).hasClass("hmax30")) {
                        $(this).removeClass("hmax30");
                    }
                    else {
                        $(this).addClass("hmax30");
                    }
                });
                $valdetail.show();
            }
        };
        TaxonomyContainer.prototype.TableInfoSelected = function (id, ttype, sender) {
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
        };
        TaxonomyContainer.SetValues = function (ruleresult) {
            ruleresult.Parameters.forEach(function (parameter) {
                var strvalue = "";
                var numericval = 0;
                if (parameter.BindAsSequence) {
                    var factsnr = parameter.Facts.length;
                    var ix = 0;
                    strvalue += "(";
                    parameter.Facts.forEach(function (factstring) {
                        var strval = TaxonomyContainer.GetFactValue(factstring);
                        numericval += Number(strval);
                        strvalue += strval;
                        strvalue += ix < factsnr - 1 ? ", " : ")";
                        ix++;
                    });
                    strvalue = numericval.toFixed(2).toString() + "  " + strvalue;
                }
                else {
                    if (parameter.Facts.length == 1) {
                        strvalue = TaxonomyContainer.GetFactValue(parameter.Facts[0]);
                    }
                }
                parameter.Value = strvalue;
            });
        };
        TaxonomyContainer.prototype.GetCellValue = function (cellid) {
            return "";
        };
        TaxonomyContainer.GetFactValue = function (factstring) {
            var tmpfact = new Model.FactBase();
            tmpfact.FactString = factstring;
            Model.FactBase.LoadFromFactString(tmpfact);
            var factkey = tmpfact.GetFactKey();
            var facts = app.instancecontainer.Instance.FactDictionary[factkey];
            if (!IsNull(facts) && facts.length > 0) {
                var fact = facts.AsLinq().FirstOrDefault(function (i) { return i.FactString == factstring; });
                //var fact: Model.InstanceFact = facts[0];
                if (IsNull(fact)) {
                    //Notify(Format("FactString {1} was not found in the instance", factstring));
                    return "";
                }
                else {
                    return fact.Value;
                }
            }
            else {
            }
            return "";
            //return "";
        };
        TaxonomyContainer.prototype.NavigateTo = function (cell) {
            var me = this;
            AjaxRequest("Taxonomy/Table", "get", "text/html", { cell: cell }, function (data) {
                var itemparts = data.split("#");
                if (itemparts.length > 1) {
                    var url = itemparts[0];
                    var hash = itemparts[1];
                    window.location.hash = hash;
                }
            }, function (error) {
                console.log(error);
            });
        };
        return TaxonomyContainer;
    })();
    Control.TaxonomyContainer = TaxonomyContainer;
})(Control || (Control = {}));
var s_tableframe_selector = "#ReportContainer";
//# sourceMappingURL=Taxonomy.js.map