var UITableManager = (function () {
    function UITableManager() {
        this.RowID_Format = "R{0:D4}";
        this.ColumnID_Format = "C{0:D4}";
        this.ExtensionID_Format = "Z{0:D4}";
        this.TemplateRow = null;
        this.TemplateColumn = null;
        this.CellEditorAssigner = null;
        this.OnLoaded = null;
        this.OnCellSelected = null;
        this.OnRowSelected = null;
        this.OnColumnSelected = null;
        this.OnCellChanged = null;
        this.OnRowAdded = null;
        this.OnRowRemoved = null;
        this.OnColumnAdded = null;
        this.OnColumnRemoved = null;
        this.OnLayoutChanged = null;
        this.AddEventHandlers();
    }
    UITableManager.prototype.LoadToUI = function (data) {
    };
    UITableManager.prototype.LoadPage = function (page, asyncdatagetter, callback) {
    };
    UITableManager.prototype.LoadLayoutFromData = function (data, table) {
    };
    UITableManager.prototype.LoadLayoutFromHtml = function (element, table) {
        var me = this;
        var rawrows = _Select("tr", element);
        var headerix = 0;
        var columncells = [];
        var rowcells = [];
        table.HeaderRowCount = _Select("thead tr", element).length;
        rawrows.forEach(function (rawrow, ix) {
            var rawdatacells = [];
            var rawheadercells = [];
            var isdynamic = _HasClass(rawrow, "dynamic");
            if (isdynamic) {
                rawrow = _Clone(rawrow);
            }
            rawdatacells = _Select("td", rawrow);
            rawheadercells = _Select("th", rawrow);
            if (rawdatacells.length > 0) {
                if (columncells.length < 1) {
                    headerix = ix - 1;
                    var headerrow = rawrows[headerix];
                    columncells = _Select("th", headerrow);
                }
                var rowheadercell = rawheadercells[rawheadercells.length - 1];
                rowcells.push(rowheadercell);
                var row = new Controls.Row();
                row.HeaderCell = Controls.Cell.ConvertFrom(rowheadercell, 2 /* Header */);
                row.UIElement = rawrow;
                columncells.forEach(function (cell, ix) {
                    var column = new Controls.Column();
                    column.ColID = _Html(cell).trim();
                    table.Columns.push(column);
                });
                rawdatacells.forEach(function (cell, ix) {
                    var rowcode = _Html(rowheadercell).trim();
                    var colcell = columncells[ix]; //rowcells[ix];
                    var colcode = _Html(colcell).trim();
                    var cellid = Format("{0}|{1}", rowcode, colcode);
                    var cellobj = new Controls.Cell();
                    cellobj.Type = 1 /* Data */;
                    cellobj.RowID = rowcode;
                    cellobj.ColID = colcode;
                    cellobj.Value = _Html(cell).trim();
                    cellobj.UIElement = cell;
                    row.Cells.push(cellobj);
                    if (!isdynamic) {
                        table.Cells.push(cellobj);
                    }
                });
                if (isdynamic) {
                    me.TemplateRow = row;
                    Controls.Row.ClearDataCells(me.TemplateRow);
                    _Hide(me.TemplateRow.UIElement);
                }
                else {
                    table.Rows.push(row);
                }
                if (rawheadercells.length > table.HeaderColCount) {
                    table.HeaderColCount = rawheadercells.length;
                }
            }
        });
        var rowheader = new Controls.Row();
        rowheader.Cells = rowcells.AsLinq().Select(function (i) { return Controls.Cell.ConvertFrom(i, 2 /* Header */); }).ToArray();
        var colheader = new Controls.Column();
        colheader.Cells = columncells.AsLinq().Select(function (i) { return Controls.Cell.ConvertFrom(i, 2 /* Header */); }).ToArray();
        table.RowHeader = colheader;
        table.ColumnHeader = rowheader;
        CallFunctionWithContext(me, me.OnLoaded, [table]);
    };
    UITableManager.prototype.ManageRows = function (table) {
        Notify("ManageRows");
        var me = this;
        if (!IsNull(me.TemplateRow)) {
            var rowsquery = table.Rows.AsLinq().Where(function (i) { return _HasClass(i.UIElement, "dynamicdata"); });
            var emptyrow = rowsquery.FirstOrDefault(function (i) { return !i.HasData(); });
            if (IsNull(emptyrow)) {
                emptyrow = table.AddRow(-1);
                this.SetRowID("emptyrow", emptyrow);
            }
        }
        //me.SetDynamicRowIds(table);
    };
    UITableManager.prototype.SetRowID = function (ID, row) {
        row.RowID = ID;
        var headercell = row.HeaderCell;
        var existingrowid = _Html(headercell.UIElement).trim();
        _Html(headercell.UIElement, row.RowID);
        _Attribute(row.UIElement, "id", row.RowID);
        row.Cells.forEach(function (cell, ix) {
            var cellid = _Attribute(cell.UIElement, "id").trim();
            if (cellid.indexOf("|") == 0) {
                cellid = row.RowID + cellid;
                _Attribute(cell.UIElement, "id", cellid);
            }
        });
    };
    UITableManager.prototype.SetDynamicRowIds = function (table) {
        ShowNotification("SetDynamicRowIds");
        var me = this;
        var fdyndata = function (row) { return _HasClass(row.UIElement, "dynamicdata"); };
        var dynamicrows = table.Rows.AsLinq().Where(function (i) { return fdyndata(i); }).ToArray();
        dynamicrows.forEach(function (row, ix) {
            var rowid = row.RowID; //Format(me.RowID_Format, ix);
            this.SetRowID(rowid, row);
        });
    };
    UITableManager.prototype.Clear = function (table) {
        table.Cells.forEach(function (cell, ix) {
            Controls.Cell.Clear(cell);
            //_Attribute(cell.UIElement, "factstring", "");
        });
    };
    UITableManager.prototype.Validate = function () {
        return true;
    };
    UITableManager.prototype.Save = function () {
        return true;
    };
    UITableManager.prototype.EditCell = function (cell) {
        return true;
    };
    UITableManager.prototype.SetCellsOfRow = function (row) {
        var me = this;
        var table = app.taxonomycontainer.Table.UITable;
        //for (var i = 0; i < table.Columns.length; i++)
        //{
        //}
        //this.CellEditorAssigner(row.UIElement);
        //row.Cells.forEach(function (cell, ix) {
        //    //_Attribute(cell.UIElement, "title", _Attribute(cell.UIElement, "factstring"));
        //});
        _EnsureEventHandler(row.UIElement, "click", function (e) {
            _Focus(this);
            _RemoveClass(_Select("tr", _Parent(row.UIElement)), selectedclass);
            _AddClass(this, selectedclass);
        });
    };
    UITableManager.prototype.AddEventHandlers = function () {
        var me = this;
        this.CellEditorAssigner = function (element) {
            AssignEditor(function () { return _Select("td.data:not(.blocked)", element); }, GetXbrlCellEditor, me.OnCellChanged);
        };
        this.OnLoaded = function (table) {
            var me = this;
            //me.CellEditorAssigner(table.UIElement);
            table.Rows.forEach(function (row, ix) {
                me.SetCellsOfRow(row);
            });
            _EnsureEventHandler(table.UIElement, "click", function (e) {
                var element = e.target;
                if (element instanceof HTMLTableDataCellElement) {
                    me.OnCellSelected(element);
                    if (!_HasClass(element, "blocked")) {
                        EditCell(element, GetXbrlCellEditor, me.OnCellChanged);
                    }
                }
            });
            _RemoveEventHandler(window, "keyup", table.DeleteFunction);
            _AddEventHandler(window, "keyup", table.DeleteFunction);
            //table.Rows.forEach(function (row, ix) {
            //    _EnsureEventHandler(row.UIElement, "click", me.OnRowSelected);
            //});
            //table.Columns.forEach(function (column, ix) {
            //    _EnsureEventHandler(column.UIElement, "click", me.OnColumnSelected);
            //});
        };
        this.OnCellSelected = function (element) {
            var cellid = _Attribute(element, "id");
            var cell = app.taxonomycontainer.Table.UITable.GetCellByID(cellid);
            if (cell != null) {
                var mcell = new Model.Cell;
                mcell.Extension = app.taxonomycontainer.Table.Current_ExtensionCode;
                mcell.Report = app.taxonomycontainer.Table.Current_ReportID;
                mcell.Row = cell.RowID;
                mcell.Column = cell.ColID;
                mcell.FactString = _Attribute(cell.UIElement, "factstring");
                mcell.CellID = _Attribute(cell.UIElement, "id");
                BindX($("#CellInfo"), mcell);
                _Show(_SelectFirst("#CellInfo"));
            }
        };
        this.OnRowAdded = function (row) {
            var rowelement = row.UIElement;
            _AddClass(rowelement, "dynamicdata");
            _RemoveClass(rowelement, "dynamic");
            //_Attribute(rowelement, "style", "");
            me.SetCellsOfRow(row);
            _Show(row.UIElement);
        };
        this.OnRowRemoved = function (row) {
        };
        this.OnLayoutChanged = function (table) {
            var me = this;
            if (table.CanManageRows) {
                me.ManageRows(table);
            }
        };
        this.OnCellChanged = function (cell, value) {
            var table = app.taxonomycontainer.Table.UITable;
            var row = table.GetRowOfCell(cell);
            if (!IsNull(row) && !row.HasData()) {
                table.RemoveRow(row);
            }
            table.Manager.ManageRows(table);
            //OnCellChanged(cell, value);
        };
        this.OnRowSelected = function (row) {
            _RemoveClass(_Select("table.report tbody tr"), selectedclass);
            _AddClass(row.UIElement, selectedclass);
        };
    };
    return UITableManager;
})();
var ProgressManager = (function () {
    function ProgressManager(progress_id) {
        this.id_progress = "";
        this.Progresses = [];
        this.id_progress = progress_id;
    }
    ProgressManager.prototype.StartProgress = function (id) {
        //Log(Format("StartProgress: {0}", id));
        this.Progresses.push(id);
        if (this.Progresses.length == 1) {
            $(this.id_progress).show();
        }
    };
    ProgressManager.prototype.StopProgress = function (id) {
        //Log(Format("StopProgress: {0}", id));
        var ix = 0;
        this.Progresses.forEach(function (item, i) {
            if (item == id) {
                ix = i;
            }
        });
        this.Progresses.splice(ix, 1);
        if (this.Progresses.length == 0) {
            $(this.id_progress).hide();
        }
    };
    return ProgressManager;
})();
var Editor = (function () {
    function Editor(HtmlFormat, ValueGetter, ValueSetter) {
        this.HtmlFormat = "";
        this.ValueGetter = null;
        this.ValueSetter = null;
        this.TargetValueGetter = null;
        this.TargetValueSetter = null;
        this.CustomTrigger = null;
        this.$Target = null;
        this.$Me = null;
        this.HtmlFormat = HtmlFormat;
        this.ValueGetter = ValueGetter;
        this.ValueSetter = ValueSetter;
    }
    Editor.prototype.Save = function () {
        var new_value = this.ValueGetter(this.$Me);
        this.$Target.removeClass(Editor.editclass);
        this.$Me.remove();
        if (this.Original_Value != new_value) {
            this.TargetValueSetter(new_value);
        }
        else {
            _Html(this.$Target[0], this.Original_Value);
        }
    };
    Editor.prototype.Load = function (TargetElement, TargetValueGetter, TargetValueSetter) {
        var Target = $(TargetElement);
        var me = this;
        this.TargetValueGetter = TargetValueGetter;
        this.TargetValueSetter = TargetValueSetter;
        this.Original_Value = TargetValueGetter().trim();
        this.$Me = $(Format(this.HtmlFormat, this.Original_Value));
        //setting UI
        var t_width = Target.width();
        var t_height = Target.height();
        var t_l_padding = Target.padding("left");
        var t_r_padding = Target.padding("right");
        var t_t_padding = Target.padding("top");
        var t_b_padding = Target.padding("bottom");
        var t_tagname = Target.prop("tagName");
        var containerwidth = t_width - 2; // - (t_l_padding + t_r_padding);
        var containerheight = t_height - (t_t_padding + t_b_padding);
        var containerfontfamily = Target.css('font-family');
        var containerfontsize = Target.css('font-size');
        var containerlineheight = Target.css('line-height');
        var containerbackgroundcolor = Target.parent().css('background-color');
        this.$Me.width(containerwidth);
        this.$Me.height(containerheight);
        this.$Me.css('font-family', containerfontfamily);
        this.$Me.css('font-size', containerfontsize);
        this.$Me.css('line-height', containerlineheight);
        //end setting UI
        this.ValueSetter(this.$Me, this.Original_Value);
        this.$Target = Target;
        this.$Target.html('');
        this.$Me.appendTo(this.$Target);
        this.$Target.addClass(Editor.editclass);
        this.$Me.keypress(function (e) {
            if (e.which == 13) {
                me.Save();
            }
        });
        this.$Me.focus();
        if (IsNull(this.CustomTrigger)) {
            this.$Me.blur(function () {
                me.Save();
                return true;
            });
        }
    };
    Editor.editclass = "editing";
    return Editor;
})();
var Engine;
(function (Engine) {
    var ActionCenter = (function () {
        function ActionCenter() {
            this.Selector = null;
            this.CurrentSelector = null;
            this.ListSelector = null;
            this.ActionBarSelector = null;
            this.class_Error = "n-error";
            this.class_Warning = "n-warning";
            this.class_Info = "n-info";
            this.class_Success = "n-success";
            this.format_Notification = "<div class=\"notification {1}\">{0}</div>";
        }
        ActionCenter.prototype.SetSelectors = function (selector, currentselector, listselector, actionbarselector) {
            this.Selector = selector;
            this.CurrentSelector = currentselector;
            this.ListSelector = listselector;
            this.ActionBarSelector = actionbarselector;
        };
        ActionCenter.prototype.AddSuccess = function (content) {
            this.AddNotification(content, this.class_Success);
        };
        ActionCenter.prototype.AddInfo = function (content) {
            this.AddNotification(content, this.class_Info);
        };
        ActionCenter.prototype.AddWarning = function (content) {
            this.AddNotification(content, this.class_Warning);
        };
        ActionCenter.prototype.AddError = function (content) {
            this.AddNotification(content, this.class_Error);
        };
        ActionCenter.prototype.AddNotification = function (content, cssclass) {
            content = Format(this.format_Notification, content, cssclass);
            var lastmessage = $(this.CurrentSelector).html();
            $(this.CurrentSelector).html(content);
            $(this.ListSelector).prepend(lastmessage);
            $(this.Selector).show();
        };
        ActionCenter.prototype.ClearAll = function () {
            this.ClearCurrent();
            this.ClearList();
            $(this.Selector).hide();
        };
        ActionCenter.prototype.ClearCurrent = function () {
            $(this.CurrentSelector).html("");
        };
        ActionCenter.prototype.ClearList = function () {
            $(this.ListSelector).html("");
        };
        ActionCenter.prototype.ToggleListVisibility = function () {
            if ($(this.ListSelector).is(":visible")) {
                $(this.ListSelector).hide();
            }
            else {
                $(this.ListSelector).show();
            }
        };
        return ActionCenter;
    })();
    Engine.ActionCenter = ActionCenter;
    var UIManager = (function () {
        function UIManager() {
            this.duration = 200;
            this.min_width = 150;
        }
        UIManager.prototype.GetMaxWidth = function () {
            var maxwidth = $("#main-content").width();
            return maxwidth;
        };
        UIManager.prototype.ActivateList = function () {
            $("#ListController").parent().animate({ "max-width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });
            $("#SaveController").parent().animate({ "width": this.min_width + "px" }, { duration: this.duration, queue: false });
        };
        UIManager.prototype.ActivateSave = function () {
            $("#ListController").parent().animate({ "max-width": this.min_width + "px" }, { duration: this.duration, queue: false });
            $("#SaveController").parent().animate({ "width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });
        };
        return UIManager;
    })();
    Engine.UIManager = UIManager;
})(Engine || (Engine = {}));
//# sourceMappingURL=AppModel.js.map