var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var HtmlElements = (function () {
    function HtmlElements(items) {
        this.Elements = [];
        if (IsArray(items)) {
            this.Elements = items;
        }
        else {
            this.Elements.push(items);
        }
    }
    return HtmlElements;
})();
var Controls;
(function (Controls) {
    var TableManager = (function () {
        function TableManager() {
            this.CellEditorAssigner = null;
            this.OnLoaded = null;
            this.OnCellSelected = null;
            this.OnRowSelected = null;
            this.OnColumnSelected = null;
            this.OnCellChanged = null;
            this.OnRowAdded = null;
            this.OnRowRemoved = null;
            this.OnColumnAdded = null;
            this.OncolumnRemoved = null;
        }
        TableManager.prototype.LoadToUI = function (data) {
        };
        TableManager.prototype.LoadPage = function (page, asyncdatagetter, callback) {
        };
        TableManager.prototype.LoadLayoutFromData = function (data, table) {
        };
        TableManager.prototype.LoadLayoutFromHtml = function (element, table) {
            var me = this;
            var rawrows = _Select("tr", element);
            var headerix = 0;
            var columncells = [];
            var rowcells = [];
            table.HeaderRowCount = _Select("thead tr", element).length;
            rawrows.forEach(function (rawrow, ix) {
                var rawdatacells = _Select("td", rawrow);
                var rawheadercells = _Select("th", rawrow);
                if (rawdatacells.length > 0) {
                    if (columncells.length < 1) {
                        headerix = ix - 1;
                        var headerrow = rawrows[headerix];
                        columncells = _Select("th", headerrow);
                    }
                    var rowheadercell = rawheadercells[rawheadercells.length - 1];
                    rowcells.push(rowheadercell);
                    var row = new Row();
                    row.HeaderCell = Cell.ConvertFrom(rowheadercell);
                    row.UIElement = rawrow;
                    rawdatacells.forEach(function (cell, ix) {
                        var rowcode = _Html(rowheadercell).trim();
                        var colcell = columncells[ix]; //rowcells[ix];
                        var colcode = _Html(colcell).trim();
                        var cellid = Format("{0}|{1}", rowcode, colcode);
                        var cellobj = new Cell();
                        cellobj.Type = 1 /* Data */;
                        cellobj.RowID = rowcode;
                        cellobj.ColID = colcode;
                        cellobj.Value = _Html(cell).trim();
                        cellobj.UIElement = cell;
                        row.Cells.push(cellobj);
                    });
                    table.Rows.push(row);
                    if (rawheadercells.length > table.HeaderColCount) {
                        table.HeaderColCount = rawheadercells.length;
                    }
                }
            });
            var rowheader = new Row();
            rowheader.Cells = rowcells.AsLinq().Select(function (i) { return Cell.ConvertFrom(i); }).ToArray();
            var colheader = new Column();
            colheader.Cells = columncells.AsLinq().Select(function (i) { return Cell.ConvertFrom(i); }).ToArray();
            table.RowHeader = colheader;
            table.ColumnHeader = rowheader;
            CallFunction(me.OnLoaded, [me]);
        };
        TableManager.prototype.Validate = function () {
            return true;
        };
        TableManager.prototype.Save = function () {
            return true;
        };
        TableManager.prototype.EditCell = function (cell) {
            return true;
        };
        return TableManager;
    })();
    Controls.TableManager = TableManager;
    (function (CellType) {
        CellType[CellType["Unknown"] = 0] = "Unknown";
        CellType[CellType["Data"] = 1] = "Data";
        CellType[CellType["Header"] = 2] = "Header";
        CellType[CellType["Placeholder"] = 3] = "Placeholder";
        CellType[CellType["Breakdown"] = 4] = "Breakdown";
    })(Controls.CellType || (Controls.CellType = {}));
    var CellType = Controls.CellType;
    var Cell = (function () {
        function Cell() {
            this.RowID = "";
            this.ColID = "";
            this.Value = "";
            this.Type = 0 /* Unknown */;
            this.UIElement = null;
        }
        Cell.ConvertFrom = function (element) {
            var cell = new Cell();
            var tag = _Property(element, "tagName");
            if (tag.toLowerCase() == "td") {
                cell.Type = 1 /* Data */;
            }
            if (tag.toLowerCase() == "th") {
                cell.Type = 2 /* Header */;
            }
            cell.Value = _Html(element).trim();
            cell.UIElement = element;
            return cell;
        };
        Cell.prototype.HasData = function () {
            return !IsNull(_Html(this.UIElement).trim());
        };
        return Cell;
    })();
    Controls.Cell = Cell;
    var Row = (function () {
        function Row() {
            this.RowID = "";
            this.HeaderCell = null;
            this.Cells = [];
        }
        Row.prototype.HasData = function () {
            var cellswithData = this.Cells.AsLinq().FirstOrDefault(function (i) { return i.HasData(); });
            return cellswithData != null;
        };
        Row.prototype.IsDynamic = function () {
            return _HasClass(this.UIElement, "dynamic");
        };
        Row.ClearDataCells = function (row) {
            row.Cells.forEach(function (cell, ix) {
                _Html(cell.UIElement, "");
            });
        };
        return Row;
    })();
    Controls.Row = Row;
    var Column = (function () {
        function Column() {
            this.ColID = "";
            this.Cells = [];
        }
        return Column;
    })();
    Controls.Column = Column;
    var Table = (function () {
        function Table(manager) {
            this.Manager = null;
            this.UIElement = null;
            this.ColumnHeader = null;
            this.RowHeader = null;
            this.Rows = [];
            this.Columns = [];
            this.Cells = [];
            this.HeaderRowCount = 0;
            this.HeaderColCount = 0;
            this.Keys = [];
            this.OnRowRemoved = function (row) {
                var me = (this);
                me.ManageRows();
                CallFunction(me.Manager.OnRowRemoved, [row]);
                //me.Manager.OnRowRemoved(row);
            };
            this.OnRowAdded = function (row) {
                var me = (this);
                me.ManageRows();
                CallFunction(me.Manager.OnRowAdded, [row]);
                //me.Manager.OnRowAdded(row);
            };
            this.Manager = manager;
            manager.Table;
        }
        Table.prototype.ManageRows = function () {
            var me = this;
            var rowsquery = me.Rows.AsLinq();
            var dynamicrow = rowsquery.FirstOrDefault(function (i) { return i.IsDynamic(); });
            if (!IsNull(dynamicrow || 1 == 1)) {
                var emptyrow = rowsquery.FirstOrDefault(function (i) { return !i.HasData(); });
                if (IsNull(emptyrow)) {
                    me.AddRow(-1);
                }
            }
            me.SetRowIds();
        };
        Table.prototype.GetRowOfCell = function (cellelement) {
            var me = this;
            var result = null;
            var rowelement = _Parent(cellelement);
            me.Rows.forEach(function (row, ix) {
                if (rowelement == row.UIElement) {
                    result = row;
                }
            });
            return result;
        };
        Table.prototype.ValidateRow = function (rowid) {
            return true;
        };
        Table.prototype.GetNewRow = function () {
            var newrow = new Row();
            return newrow;
        };
        Table.prototype.LoadEventHandlers = function () {
            var me = this;
            me.Rows.forEach(function (row, ix) {
                _EnsureEventHandler(row.UIElement, "click", me.Manager.OnRowSelected);
            });
            me.Cells.forEach(function (cell, ix) {
                _EnsureEventHandler(cell.UIElement, "click", me.Manager.OnCellSelected);
            });
            me.Columns.forEach(function (column, ix) {
                _EnsureEventHandler(column.UIElement, "click", me.Manager.OnColumnSelected);
            });
        };
        Table.prototype.LoadfromHtml = function (element) {
            this.UIElement = element;
            this.Manager.LoadLayoutFromHtml(element, this);
            this.ManageRows();
        };
        Table.prototype.AddRow = function (index) {
            if (index === void 0) { index = -1; }
            var me = this;
            var templaterow = this.Rows[0];
            var indexedrow = (index > -1 && index < this.Rows.length) ? this.Rows[0] : null;
            var referencerow = (index == -1) ? this.Rows.AsLinq().LastOrDefault().UIElement : indexedrow.UIElement;
            var newrow = me.GetNewRow();
            var newelement = _Clone(templaterow.UIElement);
            me.SetRow(newrow, newelement);
            Row.ClearDataCells(newrow);
            var newrowHeaderCell = newrow.Cells.AsLinq().LastOrDefault(function (i) { return i.Type == 2 /* Header */; });
            var rowid = Format(Table.RowID_Format, me.RowHeader.Cells.length);
            newrow.RowID = rowid;
            if (!IsNull(newrowHeaderCell)) {
                newrowHeaderCell.RowID = rowid;
                _Html(newrowHeaderCell.UIElement, rowid);
                me.RowHeader.Cells.push(newrowHeaderCell);
            }
            me.Cells.concat(newrow.Cells);
            me.Rows.push(newrow);
            if (index == -1) {
                _After(referencerow, newrow.UIElement);
            }
            else {
                _Before(referencerow, newrow.UIElement);
            }
            CallFunctionWithContext(me, me.OnRowAdded, [newrow]);
            me.LoadEventHandlers();
            return newrow;
        };
        Table.prototype.SetRowIds = function () {
            var me = this;
            this.Rows.forEach(function (row, ix) {
                row.RowID = Format(Table.RowID_Format, ix);
                var headercell = row.HeaderCell;
                var existingrowid = _Html(headercell.UIElement).trim();
                if (IsNull(existingrowid)) {
                    _Html(headercell.UIElement, row.RowID);
                }
                _EnsureEventHandler(row.UIElement, "click", function (e) {
                    _Focus(this);
                    _RemoveClass(_Select("tr", me.UIElement), "selected");
                    _AddClass(this, "selected");
                });
                //_EnsureEventHandler(row.UIElement, "keyup", function (e) {
                _EnsureEventHandler(window, "keyup", function (e) {
                    if (e.which == 46) {
                        var rowtodelete = null;
                        me.Rows.forEach(function (row, ix) {
                            if (_HasClass(row.UIElement, "selected")) {
                                rowtodelete = row;
                            }
                        });
                        if (!IsNull(rowtodelete)) {
                            me.RemoveRow(rowtodelete);
                        }
                    }
                });
            });
        };
        Table.prototype.GetRowByID = function (id) {
            var row = this.Rows.AsLinq().FirstOrDefault(function (i) { return i.RowID == id; });
            return row;
        };
        Table.prototype.RemoveRowByID = function (rowid) {
            if (this.Rows.length > 1) {
                var rowtoremove = this.GetRowByID(rowid);
                this.RemoveRow(rowtoremove);
            }
        };
        Table.prototype.RemoveRow = function (row) {
            var me = this;
            if (me.Rows.length > 1) {
                var ix = this.Rows.indexOf(row);
                row.Cells.forEach(function (cell, ix) {
                    RemoveFrom(cell, me.Cells);
                });
                RemoveFrom(row, me.RowHeader.Cells);
                RemoveFrom(row, me.Rows);
                _Remove(row.UIElement);
                CallFunctionWithContext(me, me.OnRowRemoved, [row]);
            }
            else {
                console.log("The last row can't be removed!");
            }
            return true;
        };
        Table.prototype.SetRow = function (row, element) {
            var headercells = _Select("th", element);
            var datacells = _Select("td", element);
            row.Cells = [];
            var headercellelement = headercells.AsLinq().LastOrDefault();
            if (!IsNull(headercellelement)) {
                row.HeaderCell = Cell.ConvertFrom(headercellelement);
            }
            row.UIElement = element;
            datacells.forEach(function (cellelement, ix) {
                var cell = Cell.ConvertFrom(cellelement);
                row.Cells.push(cell);
            });
        };
        Table.RowID_Format = "R{0:D4}";
        Table.ColumnID_Format = "C{0:D4}";
        return Table;
    })();
    Controls.Table = Table;
    var ReportTable = (function (_super) {
        __extends(ReportTable, _super);
        function ReportTable() {
            _super.apply(this, arguments);
            this.Extensions = null;
        }
        return ReportTable;
    })(Table);
    Controls.ReportTable = ReportTable;
})(Controls || (Controls = {}));
//# sourceMappingURL=TableManager.js.map