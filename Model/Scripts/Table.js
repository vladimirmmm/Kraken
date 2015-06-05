var UI;
(function (UI) {
    var Table = (function () {
        function Table() {
            this.Cells = [];
            this.Extensions = [];
            this.FactMap = {};
            this.CurrentExtension = null;
            this.Instance = null;
            this.$TemplateRow = null;
            this.Concepts = [];
            this.Hierarchies = [];
            this.ConceptValues = [];
            this.GetFactMap();
            this.SetExtension("");
            this.SetExternals();
        }
        Table.prototype.SetExternals = function () {
            this.Concepts = window["concepts"] == null ? [] : window["concepts"];
            this.Concepts = this.Concepts.AsLinq().Where(function (i) { return i.Domain != null; }).ToArray();
            this.Hierarchies = window["hierarchies"] == null ? [] : window["hierarchies"];
            this.LoadConceptValues();
            this.SetCellEditors();
        };
        Table.prototype.LoadConceptValues = function () {
            var me = this;
            me.ConceptValues = [];
            var htemp = new Model.Hierarchy();
            me.Concepts.forEach(function (concept) {
                concept.Domain.Name = IsNull(concept.Domain.Name) ? concept.Domain.ID : concept.Domain.Name;
                var hier = me.Hierarchies.AsLinq().FirstOrDefault(function (i) { return i.Item.Name == concept.Domain.Name && i.Item.Namespace == concept.Domain.Namespace && i.Item.Role == concept.HierarchyRole; });
                if (hier != null) {
                    var clkp = new Model.ConceptLookUp();
                    clkp.Concept = Format("{0}:{1}", concept.Namespace, concept.Name);
                    hier["ToArray"] = htemp.ToArray; //() => htemp.ToArray.apply(hier);
                    var items = hier.ToArray();
                    items.forEach(function (item, index) {
                        if (index > 0) {
                            var v = {};
                            var id = Format("{0}:{1}", item.Namespace, item.Name);
                            clkp.Values[id] = Format("({0}) {1}", id, item.Label == null ? "" : item.Label.Content);
                        }
                    });
                    clkp.OptionsHTML = ToOptionList(clkp.Values, true);
                    me.ConceptValues.push(clkp);
                }
            });
        };
        Table.prototype.GetConcepOptions = function (concept) {
            var clkp = this.ConceptValues.AsLinq().FirstOrDefault(function (i) { return i.Concept == concept; });
            if (clkp != null) {
                return clkp.OptionsHTML;
            }
            return "";
        };
        Table.prototype.SetCellEditors = function () {
            var me = this;
            var cellselector = ".data";
            $(cellselector).off("click");
            $(cellselector).each(function (ix, item) {
                var $target = $(item);
                var factitems = $target.attr("factstring").split(",");
                var concept = "";
                if (factitems[0].indexOf("eba_met:") > -1) {
                    concept = factitems[0];
                }
                if (!$target.parent().hasClass("dynamic")) {
                    $target.click(function () {
                        if (!$target.hasClass(Editor.editclass)) {
                            var editor = null;
                            if (factitems[0].indexOf("eba_met:ei") == 0) {
                                editor = new Editor(Format('<select class="celleditor">{0}</select>', me.GetConcepOptions(concept)), function (i) { return i.val(); }, function (i, val) {
                                    i.val(val);
                                });
                            }
                            else {
                                editor = new Editor('<input type="text" class="celleditor" value="" />', function (i) { return i.val(); }, function (i, val) { return i.val(val); });
                            }
                            editor.Load($target, function () { return $target.html(); }, function () { return $target.html(editor.ValueGetter(editor.$Me)); });
                        }
                    });
                }
            });
        };
        Table.prototype.LoadCellsFromHtml = function () {
            var me = this;
            var s_ix = 1;
            me.Cells = [];
            $(".data").each(function (index, item) {
                var layoutid = $(item).attr("id");
                var row = layoutid.split("|")[0];
                var col = layoutid.split("|")[1];
                row = row.substring(1);
                col = col.substring(1);
                var cell = new Model.Cell();
                cell.IsBlocked = $(item).hasClass("blocked");
                if (!cell.IsBlocked) {
                    cell.Row = row;
                    cell.Column = col;
                    me.Cells.push(cell);
                }
            });
            var templaterow = $(".dynamic");
            if (templaterow.length > 0) {
                me.$TemplateRow = $(templaterow[0]);
                me.AddRow("newrow", false, "");
            }
        };
        Table.prototype.LoadCells = function (cells) {
            this.Cells = cells;
        };
        Table.prototype.GetFactMap = function () {
            this.FactMap = window["FactMap"];
        };
        Table.prototype.LoadExtension = function (li) {
            this.CurrentExtension = li;
            $("#Extension").html(li.LabelContent);
            $("#Extension").attr("title", li.FactString);
        };
        Table.prototype.LoadInstance = function (instance) {
            var me = this;
            $("dynamicdata").remove();
            if (IsNull(me.Instance)) {
                if (IsNull(instance.FactDictionary)) {
                    Notify("Loading Fact Dictionary");
                    instance.FactDictionary = {};
                    instance.Facts.forEach(function (fact, index) {
                        instance.FactDictionary[fact.FactString] = fact;
                    });
                }
                me.Instance = instance;
            }
            if (!IsNull(me.FactMap)) {
                var c = 0;
                var extfacts = me.FactMap[me.CurrentExtension.LabelCode];
                if (extfacts != null) {
                    this.Cells.forEach(function (cell, index) {
                        if (!cell.IsBlocked) {
                            if (cell.LayoutID in extfacts) {
                                var factstring = extfacts[cell.LayoutID];
                                if (!IsNull(factstring)) {
                                    if (!(factstring in me.Instance.FactDictionary)) {
                                    }
                                    else {
                                        var facts = me.Instance.FactDictionary[factstring];
                                        if (facts.length == 1 && facts[0].FactKey == facts[0].FactString) {
                                            var fact = facts[0];
                                            if (!IsNull(fact)) {
                                                var selector = "#" + cell.LayoutID.replace("|", "\\|");
                                                if ($(selector).length == 0) {
                                                    Notify(Format("No cell found with selector {0}", selector));
                                                }
                                                c++;
                                                $(selector).html(fact.Value);
                                            }
                                        }
                                        else {
                                            //dynamic
                                            facts.forEach(function (factobj, index) {
                                                var fact = Model.InstanceFact.Convert(factobj);
                                                fact.Load();
                                                var opendimension = fact.Dimensions.AsLinq().FirstOrDefault();
                                                if (!IsNull(opendimension)) {
                                                    var idvalue = opendimension.DomainMember;
                                                    var cellid = cell.LayoutID;
                                                    var r_ix = cellid.indexOf("R");
                                                    if (r_ix > -1) {
                                                        cellid = cellid.replace("R", "R" + idvalue);
                                                    }
                                                    var selector = "#" + cellid.replace("|", "\\|");
                                                    if ($(selector).length == 0) {
                                                        var $newrow = me.AddRow("dynamicdata", true, idvalue);
                                                    }
                                                    $(selector).html(fact.Value);
                                                    c++;
                                                }
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    });
                    me.SetCellEditors();
                    Notify(Format("{0} cells were populated!", c));
                }
            }
        };
        Table.prototype.AddRow = function (rowclass, beforelast, idvalue) {
            var me = this;
            var $newrow = me.$TemplateRow.clone();
            me.$TemplateRow.find("td").html("");
            var lastrow = me.$TemplateRow.parent().find("tr:last");
            if (beforelast) {
                $(lastrow).before($newrow);
            }
            else {
                $(lastrow).after($newrow);
            }
            $newrow.attr("id", Format("R{0}", idvalue));
            $newrow.addClass(rowclass);
            $newrow.removeClass("dynamic");
            $("td", $newrow).each(function (index, td) {
                var $td = $(td);
                var cellid = $td.attr("id");
                var r_ix = cellid.indexOf("R");
                if (r_ix > -1) {
                    cellid = cellid.replace("R", "R" + idvalue);
                    $td.attr("id", cellid);
                }
            });
            $newrow.find(".code").html(idvalue);
            if (IsNull(idvalue.trim())) {
                $newrow.find(".title").html("new row");
            }
            return $newrow;
        };
        Table.prototype.SetExtension = function (item) {
            var li = null;
            if (!IsNull(item)) {
                li = JSON.parse(item);
            }
            if (IsNull(li)) {
                li = new Model.LayoutItem();
                var label = new Model.Label();
                label.Code = "000";
                li.Label = label;
            }
            Notify(Format("Setting Extension to {0}", li.LabelCode));
            this.LoadExtension(li);
        };
        Table.prototype.SetCells = function (item) {
            var cells = JSON.parse(item);
            this.LoadCells(cells);
        };
        Table.prototype.SetInstance = function (item) {
            Notify("Instance recieved");
            var instance = JSON.parse(item);
            //f
            this.LoadInstance(instance);
        };
        Table.prototype.TestInstance = function () {
            this.LoadInstance(window["currentinstance"]);
        };
        return Table;
    })();
    UI.Table = Table;
})(UI || (UI = {}));
var Table = null;
function SetExtension(extjson) {
    Table.SetExtension(extjson);
}
function LoadInstance(instancejson) {
    Table.LoadInstance(window["currentinstance"]);
}
//# sourceMappingURL=Table.js.map