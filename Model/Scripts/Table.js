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
            this.Concepts = window[var_tax_concepts] == null ? [] : window[var_tax_concepts];
            this.Concepts = this.Concepts.AsLinq().Where(function (i) { return i.Domain != null; }).ToArray();
            this.Hierarchies = window[var_tax_hierarchies] == null ? [] : window[var_tax_hierarchies];
            this.LoadConceptValues();
            this.Instance = window[var_currentinstance] == null ? null : window[var_currentinstance];
        };
        Table.prototype.Load = function () {
            var me = this;
            $(window).on('hashchange', function () {
                me.HashChanged();
            });
            $("table").on('click', 'tr', function () {
                $('tr', 'table').removeClass("selected");
                $(this).addClass("selected");
            });
            this.LoadCellsFromHtml();
            this.SetCellEditors();
            $(".highlight").removeClass("highlight");
            var hash = window.location.hash;
            //if (!IsNull(hash)) {
            //    this.HighlightCell();
            //}
            //else
            //{
            //    this.SetExtension(null);          
            //}
            if (!IsNull(this.Instance)) {
                this.LoadInstance(this.Instance);
            }
            if (!IsNull(hash)) {
                Notify("Navigation: " + hash);
                this.HighlightCell();
            }
        };
        Table.prototype.HighlightCell = function () {
            var hash = window.location.hash;
            var cellid = TextBetween(hash, "cell=", ";");
            var extcode = TextBetween(hash, "ext=", ";");
            if (extcode == "000") {
                extcode = "001";
            }
            this.SetExtensionByCode(extcode);
            cellid = cellid.replace("_", "\\|");
            //cellid = cellid.replace("|", "\\|");
            $("#" + cellid).addClass("highlight");
            $("#" + cellid).focus();
        };
        Table.prototype.LoadConceptValues = function () {
            var me = this;
            me.ConceptValues = [];
            var htemp = new Model.Hierarchy();
            me.Hierarchies.forEach(function (hierarchy) {
                Model.QualifiedItem.Set(hierarchy.Item);
            });
            me.Concepts.forEach(function (concept) {
                Model.QualifiedName.Set(concept);
                Model.QualifiedName.Set(concept.Domain);
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
                            Model.QualifiedItem.Set(item);
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
                if (factitems[0].indexOf("[") == -1) {
                    concept = factitems[0];
                }
                if (!$target.parent().hasClass("dynamic") && !$target.hasClass("blocked")) {
                    $target.click(function () {
                        if (!$target.hasClass(Editor.editclass)) {
                            var editor = null;
                            if (factitems[0].indexOf(":ei") > -1) {
                                editor = new Editor(Format('<select class="celleditor">{0}</select>', me.GetConcepOptions(concept)), function (i) { return i.val(); }, function (i, val) {
                                    i.val(val);
                                });
                            }
                            else {
                                editor = new Editor('<input type="text" class="celleditor" value="" />', function (i) { return i.val(); }, function (i, val) { return i.val(val); });
                            }
                            editor.Load($target, function () { return $target.html(); }, function () {
                                $target.html(editor.ValueGetter(editor.$Me));
                                me.AddNewRowIfNeeded();
                            });
                        }
                    });
                }
            });
        };
        Table.prototype.AddNewRowIfNeeded = function () {
            var me = this;
            if (!IsNull(me.$TemplateRow)) {
                var $lastrow = me.$TemplateRow.parent().find("tr:last");
                var $selectedrow = me.$TemplateRow.parent().find("tr.selected");
                if ($selectedrow.length > 0) {
                    var $cells = $("td", $selectedrow);
                    var isallnull = true;
                    $cells.each(function (ix, cell) {
                        if (!IsNull($(cell).html().trim())) {
                            isallnull = false;
                        }
                    });
                    if (isallnull && $lastrow != $selectedrow) {
                        $selectedrow.remove();
                    }
                }
            }
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
                //me.GetDynamicRowID(null);
                me.AddRowX("newrow", false, 0);
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
            $("#Extension").html(Format("{0} <br /> {1}", li.LabelCode, li.LabelContent));
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
                                                var typeddimensions = fact.Dimensions.AsLinq().Where(function (i) { return i.IsTyped; }).ToArray();
                                                var typedfacts = new Model.FactBase();
                                                typedfacts.Dimensions = typeddimensions;
                                                var rowid = me.GetDynamicRowID(typedfacts);
                                                var cellid = cell.LayoutID;
                                                var r_ix = cellid.indexOf("R");
                                                if (r_ix > -1) {
                                                    cellid = cellid.replace("R", rowid);
                                                }
                                                var selector = "#" + cellid.replace("|", "\\|");
                                                $(selector).html(fact.Value);
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
        Table.prototype.GetDynamicRowID = function (fact) {
            var me = this;
            var newrowneeded = false;
            var rownum = 1;
            var $row = null;
            var factkey = "";
            if (fact != null) {
                factkey = fact.GetFactString();
                var $rows = $("tr", me.$TemplateRow.parent());
                $row = $("tr[factkey='" + factkey + "']");
                rownum = $rows.length - 1;
                newrowneeded = $row.length == 0;
            }
            else {
                newrowneeded = true;
            }
            if (newrowneeded) {
                //var id = Format("R{0}", rownum);
                var $newrow = me.AddRowX("", true, rownum);
                $row = $newrow; //.prev();
            }
            if (fact != null) {
                $row.attr("factkey", factkey);
                var cells = $("td", $row);
                cells.each(function (index, cell) {
                    var $cell = $(cell);
                    var cellfactstring = $cell.attr("factstring");
                    cellfactstring = Replace(cellfactstring.trim(), ",", "");
                    if (!IsNull(cellfactstring)) {
                        var dim = fact.Dimensions.AsLinq().FirstOrDefault(function (i) { return i.DomainMemberFullName.indexOf(cellfactstring) == 0; });
                        if (dim != null) {
                            var text = dim.DomainMember;
                            $cell.text(text);
                        }
                    }
                });
            }
            return $row.attr("id");
        };
        Table.prototype.AddRowX = function (rowclass, beforelast, rownum) {
            var me = this;
            var id = Format("R{0}", rownum);
            var $newrow = me.$TemplateRow.clone();
            me.$TemplateRow.find("td").html("");
            var $lastrow = me.$TemplateRow.parent().find("tr:last");
            if (beforelast) {
                $lastrow.before($newrow);
            }
            else {
                $lastrow.after($newrow);
            }
            $newrow.attr("id", id);
            $newrow.addClass("dynamicdata");
            $newrow.removeClass("dynamic");
            me.SetCellID($newrow);
            if (!beforelast) {
                $newrow.find(".title").html("new row");
            }
            var $rows = $("tr", me.$TemplateRow.parent());
            /*
            if ($rows.length > 2) {
                $lastrow.attr("id", Format("R{0}", rownum + 1));
                me.SetCellID($lastrow);
            }
            */
            return $newrow;
        };
        Table.prototype.SetCellID = function ($row) {
            var cells = $("td", $row);
            var rowid = $row.attr("id");
            cells.each(function (index, cell) {
                var $cell = $(cell);
                var cellid = $cell.attr("id");
                cellid = cellid.substring(cellid.indexOf("|"));
                cellid = rowid + cellid;
                $cell.attr("id", cellid);
                $cell.attr("title", cellid);
            });
        };
        /*
        public AddRow(rowclass:string, beforelast:boolean, idvalue: string):JQuery
        {
            var me = this;
            var $newrow = me.$TemplateRow.clone();
            me.$TemplateRow.find("td").html("");
            var lastrow = me.$TemplateRow.parent().find("tr:last");
            if (beforelast) {
                $(lastrow).before($newrow);
            }
            else
            {
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
            if (IsNull(idvalue.trim()))
            {
                $newrow.find(".title").html("new row");
            }
            return $newrow;
        }
        */
        Table.prototype.SetExtensionByCode = function (code) {
            if (!IsNull(code)) {
                var ext = this.Extensions.AsLinq().FirstOrDefault(function (i) { return i.LabelCode == code; });
                if (!IsNull(ext)) {
                    this.LoadExtension(ext);
                }
            }
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
        Table.prototype.HashChanged = function () {
            this.HighlightCell();
        };
        Table.prototype.TestInstance = function () {
            this.LoadInstance(window[var_currentinstance]);
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
    Table.LoadInstance(window[var_currentinstance]);
}
//# sourceMappingURL=Table.js.map