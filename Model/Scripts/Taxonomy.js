var Control;
(function (Control) {
    var TaxonomyContainer = (function () {
        function TaxonomyContainer() {
            this.Taxonomy = new Model.Taxonomy();
            this.ValidationRules = [];
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
                }, 500, "retek");
            });
        }
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
            AjaxRequest("Taxonomy/Get", "get", "json", null, function (data) {
                me.Taxonomy.Module = data;
                BindX($("#TaxonomyInfo"), me.Taxonomy.Module);
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
        };
        TaxonomyContainer.prototype.LoadValidationResults = function (onloaded) {
            var me = this;
            AjaxRequest("Taxonomy/ValidationRules", "get", "json", null, function (data) {
                me.Taxonomy.ValidationRules = data;
                me.Taxonomy.ValidationRules.forEach(function (v) {
                    v.Title = Truncate(v.DisplayText, 100);
                });
                CallFunctionVariable(onloaded);
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
        TaxonomyContainer.prototype.LoadContentToUI = function (contentid, sender) {
            var me = this;
            ShowContent('#TaxonomyContainer', $("#MainCommands"));
            if (contentid == "Taxonomy") {
                ShowContent('#TaxonomyContainer', sender);
            }
            if (contentid == me.s_label_id) {
                ShowContent('#' + contentid, sender);
                LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), me.Taxonomy.Labels, 0, me.LPageSize);
            }
            if (contentid == me.s_validation_id) {
                ShowContent('#' + contentid, sender);
                me.ShowValidationResults();
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
        TaxonomyContainer.prototype.TableInfoSelected = function (id, ttype, sender) {
            var me = this;
            var $sender = $(sender);
            var $parent = $(sender).parent("li");
            var $childrencontainer = $parent.children("ul").first();
            var $extensioncontainers = $(".extension").parent();
            var $tablecontainers = $(".table").parent();
            var childidentifier = $childrencontainer.parent().attr("title");
            //$tablecontainers.hide();
            if (In(ttype, "table", "tablegroup")) {
                if ($childrencontainer.css("display") == "none") {
                    $extensioncontainers.hide();
                    $childrencontainer.show();
                }
                else {
                    $childrencontainer.hide();
                }
            }
            if (In(ttype, "table", "extension")) {
                me.NavigateTo(id);
            }
        };
        TaxonomyContainer.prototype.ShowRuleDetail = function (ruleid) {
            var me = this;
            var rule = me.Taxonomy.ValidationRules.AsLinq().FirstOrDefault(function (i) { return i.ID == ruleid; });
            BindX(me.SelFromValidation(s_parent_selector), rule);
            AjaxRequest("Taxonomy/Validationrule", "get", "json", { id: ruleid }, function (data) {
                var results = data;
                results.forEach(function (ruleresult) {
                    ruleresult.Parameters.forEach(function (parameter) {
                        var strvalue = "";
                        var numericval = 0;
                        if (parameter.BindAsSequence) {
                            var factsnr = parameter.Facts.length;
                            var ix = 0;
                            strvalue += "(";
                            parameter.Facts.forEach(function (cell) {
                                var strval = me.GetFactValue(cell);
                                numericval += Number(strval);
                                strvalue += strval;
                                strvalue += ix < factsnr - 1 ? ", " : ")";
                                ix++;
                            });
                            strvalue = numericval.toFixed(2).toString() + "  " + strvalue;
                        }
                        else {
                            if (parameter.Facts.length == 1) {
                                strvalue = me.GetFactValue(parameter.Facts[0]);
                            }
                        }
                        parameter.Value = strvalue;
                    });
                });
                LoadPage(me.SelFromValidation(s_sublist_selector), me.SelFromValidation(s_sublistpager_selector), results, 0, 1);
            }, function (error) {
                console.log(error);
            });
            $("#validationrule_results_" + ruleid).append(me.SelFromValidation(s_detail_selector));
            me.SelFromValidation(s_detail_selector).show();
        };
        TaxonomyContainer.prototype.GetCellValue = function (cellid) {
            return "";
        };
        TaxonomyContainer.prototype.GetFactValue = function (factkey) {
            var facts = instancecontainer.Instance.FactDictionary[factkey];
            if (!IsNull(facts) && facts.length > 0) {
                var fact = facts[0];
                return fact.Value;
            }
            else {
            }
            return "";
            //return "";
        };
        TaxonomyContainer.prototype.CloseRuleDetail = function () {
            var me = this;
            me.SelFromValidation(s_detail_selector).hide();
            $(me.s_validation_selector).append(me.SelFromValidation(s_detail_selector));
        };
        TaxonomyContainer.prototype.NavigateTo = function (cell) {
            var me = this;
            AjaxRequest("Table/Get", "get", "text/html", { cell: cell }, function (data) {
                $(s_tableframe_selector).attr("src", data);
            }, function (error) {
                console.log(error);
            });
        };
        return TaxonomyContainer;
    })();
    Control.TaxonomyContainer = TaxonomyContainer;
})(Control || (Control = {}));
var s_tableframe_selector = "#tableframe";
var taxonomycontainer = new Control.TaxonomyContainer();
//# sourceMappingURL=Taxonomy.js.map