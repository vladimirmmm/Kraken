var Control;
(function (Control) {
    var TaxonomyContainer = (function () {
        function TaxonomyContainer() {
            this.Taxonomy = new Model.Taxonomy();
            this.Table = new UI.Table();
            this.ValidationRules = [];
            this.ConceptValues = [];
            this.TableStructure = null;
            this.CurrentFacts = [];
            this.CurrentValidationResults = [];
            this.LPageSize = 10;
            this.PageSize = 10;
            this.s_fact_id = "T_Facts";
            this.s_label_id = "T_Labels";
            this.s_xml_id = "Xml";
            this.s_concept_id = "T_Concepts";
            this.s_hierarchy_id = "T_Hierarchies";
            this.s_dimension_id = "T_Dimensions";
            this.s_validation_id = "T_Validations";
            this.s_units_id = "T_Units";
            this.s_find_id = "T_Filing";
            this.s_general_id = "T_General";
            this.s_main_id = "TaxonomyContainer";
            this.s_concept_selector = "";
            this.s_hierarchy_selector = "";
            this.s_fact_selector = "";
            this.s_label_selector = "";
            this.s_validation_selector = "";
            this.s_units_selector = "";
            this.s_find_selector = "";
            this.s_general_selector = "";
            this.s_xml_selector = "";
            this.ui_factdetail = null;
            this.ui_vruledetail = null;
            this.FactServiceFunction = null;
            this.ValidationResultServiceFunction = null;
            this.s_concept_selector = "#" + this.s_concept_id;
            this.s_hierarchy_selector = "#" + this.s_hierarchy_id;
            this.s_fact_selector = "#" + this.s_fact_id;
            this.s_label_selector = "#" + this.s_label_id;
            this.s_validation_selector = "#" + this.s_validation_id;
            this.s_units_selector = "#" + this.s_units_id;
            this.s_find_selector = "#" + this.s_find_id;
            this.s_general_selector = "#" + this.s_general_id;
            this.s_xml_selector = "#" + this.s_xml_id;
            var me = this;
            $(window).resize(function () {
                waitForFinalEvent(function () {
                    me.SetHeight();
                }, 200, "retek");
            });
            me.SetHeight();
            $(window).on('hashchange', function () {
                var hash = window.location.hash;
                var doc = TextBetween(hash, "doc=", ";");
                if (!IsNull(doc)) {
                    me.LoadDoc(doc);
                }
                else {
                    me.HashChanged();
                }
            });
        }
        TaxonomyContainer.prototype.HashChanged = function () {
            var me = this;
            me.Table.HashChanged();
        };
        TaxonomyContainer.prototype.LoadDoc = function (doc) {
            AjaxRequest(doc, "get", "text/html", null, function (data) {
                var xml = data;
                var encoded = '<textarea>' + xml + "</textarea>";
                _Html(_SelectFirst("#ReportContainer"), encoded);
            }, function (error) {
                console.log(error);
            });
        };
        TaxonomyContainer.prototype.SetHeight = function () {
            var bodyheight = $("td.th2").height();
            var pivotheight1 = (bodyheight - 70) + "px";
            var pivotheight2 = (bodyheight - 110) + "px";
            $("#Contents > .ui-tabs > .ui-tabs-panel").css("max-height", pivotheight1);
            $("#Contents > .ui-tabs > .ui-tabs-panel > .ui-tabs-panel").css("max-height", pivotheight2);
        };
        TaxonomyContainer.prototype.Sel = function (selector) {
            var me = this;
            return _SelectFirst(selector, _SelectFirst("#" + me.s_main_id));
        };
        TaxonomyContainer.prototype.SelFrom = function (parentselector, selector) {
            var me = this;
            return _SelectFirst(selector, me.Sel(parentselector));
        };
        TaxonomyContainer.prototype.SelFromLabel = function (selector) {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_label_selector));
        };
        TaxonomyContainer.prototype.SelFromConcept = function (selector) {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_concept_selector));
        };
        TaxonomyContainer.prototype.SelFromHierarchy = function (selector) {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_hierarchy_selector));
        };
        TaxonomyContainer.prototype.SelFromFact = function (selector) {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_fact_selector));
        };
        TaxonomyContainer.prototype.SelFromValidation = function (selector) {
            var me = this;
            return _SelectFirst(selector, me.Sel(me.s_validation_selector));
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
                ForAll(me.TableStructure, "Children", function (item, parent) {
                    if (!IsNull(item) && !IsNull(item.Item)) {
                        item.Item.CssClass = ""; // item.Item.Type == "table" ? "hidden" : "";
                        item.Item.CssClass += item.Item.HasData > 0 ? " hasdata" : " empty";
                        item.Item.ExtensionText = item.Item.Type == "table" && item.Children.length > 0 ? Format("({0})", item.Children.length) : "";
                        //item["uid"] = IsNull(parent) ? item.Item.ID : parent["uid"] + ">" + item.Item.ID;
                        item["uid"] = Guid();
                    }
                });
                BindX($("#tabletreeview"), me.TableStructure, 5);
            }, function (error) {
                console.log(error);
            });
            AjaxRequest("Taxonomy/Concepts", "get", "json", null, function (data) {
                me.Taxonomy.Concepts = data;
            }, function (error) {
                console.log(error);
            });
            AjaxRequest("Taxonomy/Documents", "get", "json", null, function (data) {
                me.Taxonomy.TaxonomyDocuments = data;
            }, function (error) {
                console.log(error);
            });
            me.LoadValidationResults(null);
            AjaxRequest("Taxonomy/Hierarchies", "get", "json", null, function (data) {
                var hierarchy = new Model.Hierarchy();
                hierarchy.Item = new Model.QualifiedItem();
                hierarchy.Item.ID = "Root";
                hierarchy.Item.Label = new Model.Label();
                hierarchy.Item.Label.Content = "Hierarchies";
                hierarchy.Children = data;
                me.Taxonomy.Hierarchies = hierarchy;
                me.LoadConceptValues();
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
                    if (!IsNull(data.Items)) {
                        me.CurrentFacts = data.Items;
                        fwc.Callback(data);
                    }
                }, null);
            });
            me.ValidationResultServiceFunction = new General.FunctionWithCallback(function (fwc, args) {
                var p = args[0];
                AjaxRequest("Instance/ValidationResults", "get", "json", p, function (data) {
                    if (!IsNull(data.Items)) {
                        me.CurrentValidationResults = data.Items;
                        me.CurrentValidationResults.forEach(function (v) {
                            TaxonomyContainer.SetValues(v);
                        });
                        fwc.Callback(data);
                    }
                }, null);
            });
        };
        TaxonomyContainer.prototype.LoadValidationResults = function (onloaded) {
            var me = this;
            AjaxRequest("Taxonomy/ValidationRules", "get", "json", null, function (data) {
                me.Taxonomy.ValidationRules = IsNull(data) ? [] : data;
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
            var target = GetHashPart(_Attribute(sender, "href"));
            me.LoadContentToUIX(target, sender);
        };
        TaxonomyContainer.prototype.LoadContentToUIX = function (contentid, sender) {
            var me = this;
            if (contentid == "Taxonomy") {
            }
            if (contentid == "Taxonomy") {
            }
            if (contentid == me.s_concept_id) {
                LoadPage(me.SelFromConcept(s_list_selector), me.SelFromConcept(s_listpager_selector), me.Taxonomy.Concepts, 0, me.LPageSize);
            }
            if (contentid == me.s_hierarchy_id) {
                ForAll(me.Taxonomy.Hierarchies, "Children", function (item, parent, level) {
                    if (!IsNull(item) && !IsNull(item.Item)) {
                        item.Item["CssClass"] = level > 2 ? "hidden" : "";
                    }
                    if (!("uid" in item)) {
                        item["uid"] = Guid();
                    }
                });
                StartProgress("hierarhcy");
                BindX(me.SelFromHierarchy("#hierarchytreeview"), me.Taxonomy.Hierarchies, 2);
                StopProgress("hierarhcy");
            }
            if (contentid == me.s_label_id) {
                LoadPage(me.SelFromLabel(s_list_selector), me.SelFromLabel(s_listpager_selector), me.Taxonomy.Labels, 0, me.LPageSize);
            }
            if (contentid == me.s_xml_id) {
                LoadPage(_SelectFirst(s_list_selector, me.s_xml_selector), _SelectFirst(s_listpager_selector, me.s_xml_selector), me.Taxonomy.TaxonomyDocuments, 0, me.LPageSize);
            }
            if (contentid == me.s_validation_id) {
                me.ShowValidationResults();
            }
            if (In(contentid, me.s_fact_id, me.s_main_id)) {
                var eventhandlers = {
                    onloading: function (data) {
                        //EnumerateObject(data, me,(item, itemname) => { item["PropertyName"] = itemname; });
                    }
                };
                if (me.CurrentFacts.length == 0) {
                    LoadPageAsync(me.SelFromFact(s_list_selector), me.SelFromFact(s_listpager_selector), me.FactServiceFunction, 0, me.PageSize, eventhandlers);
                }
            }
        };
        TaxonomyContainer.prototype.LoadHierarchyToUI = function (selector, data) {
            var me = this;
            var target = event.target;
            if (_TagName(target).toLowerCase() != "li") {
                target = _Parents(target).AsLinq().FirstOrDefault(function (i) { return _TagName(i).toLowerCase() == "li"; });
            }
            var uid = _Attribute(target, "uid");
            var data = Model.Hierarchy.FirstOrDefault(data, function (i) { return i["uid"] == uid; });
            if (data.Children.length > 0) {
                var template = GetBindingTemplateX(_SelectFirst(selector));
                if (template != null) {
                    var html = template.Bind(data, 0, 2);
                    var elements = $.parseHTML(html);
                    var ulelement = _Select("ul", target);
                    _Remove(ulelement);
                    elements.forEach(function (e) {
                        _Append(target, e);
                    });
                }
            }
        };
        //public LoadHierarchy()
        //{
        //    var me = this;
        //    var target = <Element>event.target;
        //    if (_TagName(target).toLowerCase() != "li")
        //    {
        //        target = _Parents(target).AsLinq<Element>().FirstOrDefault(i=> _TagName(i).toLowerCase() == "li");
        //    }
        //    var uid = _Attribute(target, "uid");
        //    var data = Model.Hierarchy.FirstOrDefault(me.Taxonomy.Hierarchies,(i: Model.Hierarchy<Model.QualifiedItem>) => i["uid"] == uid);
        //    if (data.Children.length > 0) {
        //        var template = GetBindingTemplateX(me.SelFromHierarchy("#hierarchytreeview"));
        //        if (template != null) {
        //            var html = template.Bind(data, 0, 2);
        //            var elements = $.parseHTML(html);
        //            var ulelement = _Select("ul", target);
        //            _Remove(ulelement);
        //            elements.forEach((e) => {
        //                _Append(target, e);
        //            });
        //        }
        //    }
        //}
        //public LoadTableInfo() {
        //    var me = this;
        //    var target = <Element>event.target;
        //    if (_TagName(target).toLowerCase() != "li") {
        //        target = _Parents(target).AsLinq<Element>().FirstOrDefault(i=> _TagName(i).toLowerCase() == "li");
        //    }
        //    var uid = _Attribute(target, "uid");
        //    var data = Model.Hierarchy.FirstOrDefault(me.TableStructure, (i: Model.Hierarchy<Model.TableInfo>) => i["uid"] == uid);
        //    if (!IsNull(data) && data.Children.length > 0) {
        //        var template = GetBindingTemplateX(_SelectFirst("#tabletreeview"));
        //        if (template != null) {
        //            var html = template.Bind(data, 0, 2);
        //            var elements = $.parseHTML(html);
        //            var ulelement = _Select("ul", target);
        //            _Remove(ulelement);
        //            //_Html(target, "");
        //            elements.forEach((e) => {
        //                _Append(target, e);
        //            });
        //        }
        //    }
        //}
        TaxonomyContainer.prototype.ClearFilterLabels = function () {
            $("input[type=text]", "#LabelFilter ").val("");
            $("textarea", "#LabelFilter ").val("");
            this.FilterLabels();
        };
        TaxonomyContainer.prototype.FilterLabels = function () {
            var me = this;
            var f_key = _Value(me.SelFromLabel(s_listfilter_selector + " #F_Key")).toLowerCase().trim();
            var f_code = _Value(me.SelFromLabel(s_listfilter_selector + " #F_Code")).toLowerCase().trim();
            var f_content = _Value(me.SelFromLabel(s_listfilter_selector + " #F_Content")).toLowerCase().trim();
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
        TaxonomyContainer.prototype.ClearFilterDocuments = function () {
            $("input[type=text]", "#LabelFilter ").val("");
            $("textarea", "#LabelFilter ").val("");
            this.FilterLabels();
        };
        TaxonomyContainer.prototype.FilterDocuments = function () {
            var me = this;
            var f_filename = _Value(_SelectFirst("#F_FileName", me.s_xml_selector)).toLowerCase().trim();
            var query = me.Taxonomy.TaxonomyDocuments.AsLinq();
            if (!IsNull(f_filename)) {
                query = query.Where(function (i) { return i.SourcePath.toLowerCase().indexOf(f_filename) > -1; });
            }
            LoadPage(_SelectFirst(s_list_selector, me.s_xml_selector), _SelectFirst(s_listpager_selector, me.s_xml_selector), query.ToArray(), 0, me.LPageSize);
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
            var f_ruleid = _Value(me.SelFromValidation(s_listfilter_selector + " #F_RuleID")).toLowerCase().trim();
            var f_ruletext = _Value(me.SelFromValidation(s_listfilter_selector + " #F_RuleText")).toLowerCase().trim();
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
            _Value(this.SelFromFact(s_listfilter_selector + " " + "input[type=text]"), "");
            _Value(this.SelFromFact(s_listfilter_selector + " " + "textarea"), "");
            this.FilterFacts();
        };
        TaxonomyContainer.prototype.GetFilterValue = function (selector) {
            var element = this.SelFromFact(s_listfilter_selector + " " + selector);
            if (!IsNull(element)) {
                return _Value(element).toLowerCase().trim();
            }
            return "";
        };
        TaxonomyContainer.prototype.FilterFacts = function () {
            var me = this;
            var f_factstring = me.GetFilterValue("#F_FactString");
            var f_cellid = me.GetFilterValue("#F_CellID");
            var query = me.Taxonomy.Facts;
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
                fact.Dimensions.forEach(function (dimension, ix) {
                    SetProperty(dimension, "DomainMemberFullName", Model.Dimension.DomainMemberFullName(dimension));
                });
                fact.Cells = cells;
                if (me.ui_factdetail == null) {
                    me.ui_factdetail = me.SelFromFact(s_detail_selector);
                }
                BindX(me.ui_factdetail, fact);
                _Show(me.ui_factdetail);
                app.ShowOnBottomTab(me.ui_factdetail, "#tab_fact");
            }
            else {
                Notify(Format("Fact {0} was not found!", factkey));
            }
        };
        TaxonomyContainer.prototype.HideFactDetails = function () {
            var me = this;
            _Hide(me.ui_factdetail);
        };
        TaxonomyContainer.prototype.CloseRuleDetail = function () {
            var me = this;
            _Hide(me.ui_vruledetail);
        };
        TaxonomyContainer.prototype.ShowRuleDetail = function (ruleid) {
            var me = this;
            var previousruleid = _Attribute(me.SelFromValidation(s_parent_selector + " .rule"), "rule-id");
            if (me.ui_vruledetail == null) {
                me.ui_vruledetail = me.SelFromValidation(s_detail_selector);
            }
            if (ruleid == previousruleid) {
                _Show(me.ui_vruledetail);
            }
            if (ruleid != previousruleid) {
                var rule = me.Taxonomy.ValidationRules.AsLinq().FirstOrDefault(function (i) { return i.ID == ruleid; });
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
                LoadPageAsync(list, listpager, app.taxonomycontainer.ValidationResultServiceFunction, 0, 1, { ruleid: ruleid, full: 1 });
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
                parameter.FactItems = IsNull(parameter.FactItems) ? [] : parameter.FactItems;
                parameter.FactIDs.forEach(function (factrefrence, ix) {
                    var fi = new Model.FactItem();
                    var factstring = parameter.Facts[ix];
                    var strval = TaxonomyContainer.GetFactValue(factstring);
                    fi.FactString = factstring;
                    fi.Value = strval;
                    fi.Cells = parameter.Cells[ix];
                    var strval = TaxonomyContainer.GetFactValue(factstring);
                    parameter.FactItems.push(fi);
                });
                parameter.FactItems = parameter.FactItems.AsLinq().OrderByDescending(function (i) { return i.Value; }).ToArray();
                if (parameter.BindAsSequence) {
                    var total = 0;
                    parameter.FactItems.forEach(function (fi) {
                        total += Number(fi.Value);
                    });
                    parameter.Value = total.toFixed(2).toString();
                }
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
        };
        TaxonomyContainer.GetFactValueByID = function (factid) {
            var fact = app.instancecontainer.Instance.Facts[factid];
            if (fact != null) {
                return fact.Value;
            }
            return "";
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
        TaxonomyContainer.prototype.LoadConceptValues = function () {
            var me = this;
            var concepts = GetPropertiesArray(me.Taxonomy.Concepts);
            if (!IsNull(me.Taxonomy.Hierarchies) && concepts.length > 0) {
                me.ConceptValues = [];
                var htemp = new Model.Hierarchy();
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
                        var hiers = me.Taxonomy.Hierarchies.Children.AsLinq().Where(function (i) { return i.Item.Role == concept.HierarchyRole; });
                        var hier = hiers.FirstOrDefault();
                        if (!IsNull(hier)) {
                            var clkp = new Model.ConceptLookUp();
                            clkp.Concept = Format("{0}:{1}", concept.Namespace, concept.Name);
                            var subhier = Model.Hierarchy.FirstOrDefault(hier, function (i) { return i.Item.Content == concept.Domain.Content; });
                            var items = Model.Hierarchy.ToArray(IsNull(subhier) ? hier : subhier);
                            if (subhier == hier) {
                                removeFromArray(items, hier.Item);
                            }
                            items.forEach(function (item, index) {
                                Model.QualifiedItem.Set(item);
                                if (item.Name != "x0") {
                                    var v = {};
                                    var id = Format("{0}:{1}", item.Namespace, item.Name);
                                    clkp.Values[id] = Format("({0}) {1}", id, item.Label == null ? "" : item.Label.Content);
                                }
                            });
                            clkp.OptionsHTML = ToOptionList(clkp.Values, true);
                            me.ConceptValues.push(clkp);
                        }
                        else {
                            Log("Hierarchy not found for " + concept.Content);
                        }
                    }
                });
            }
        };
        TaxonomyContainer.prototype.GetConcepOptions = function (concept) {
            var clkp = this.ConceptValues.AsLinq().FirstOrDefault(function (i) { return i.Concept == concept; });
            if (clkp != null) {
                return clkp.OptionsHTML;
            }
            return "";
        };
        TaxonomyContainer.prototype.GetMembersOfHierarchy = function (layoutrole) {
            Log("layoutrole: " + layoutrole);
            var role = TextBetween(layoutrole, "Role:", ";");
            var axis = TextBetween(layoutrole, "Axis:", ";");
            var h = this.Taxonomy.Hierarchies.Children.AsLinq().FirstOrDefault(function (i) { return i.Item.Role == role; });
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
        };
        TaxonomyContainer.prototype.GetMembersOfDomainOptions = function (dom) {
            Log("dom: " + dom);
            var h = this.Taxonomy.Hierarchies.Children.AsLinq().FirstOrDefault(function (i) { return i.Item.Label.LocalID == dom; });
            if (!IsNull(h)) {
                Log("hierarchy found for DDL!");
                var items = h.Children.length > 0 ? h.Children[0].Children : h.Children;
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
        };
        return TaxonomyContainer;
    })();
    Control.TaxonomyContainer = TaxonomyContainer;
})(Control || (Control = {}));
var s_tableframe_selector = "#ReportContainer";
//# sourceMappingURL=Taxonomy.js.map