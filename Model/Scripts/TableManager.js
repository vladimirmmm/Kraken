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
                    var rowcell = rawheadercells[rawheadercells.length - 1];
                    rowcells.push(rowcell);
                    var row = new Row();
                    row.UIElement = rawrow;
                    rawdatacells.forEach(function (cell, ix) {
                        var rowcode = _Html(rowcell).trim();
                        var colcell = columncells[ix]; //rowcells[ix];
                        var colcode = _Html(colcell).trim();
                        var cellid = Format("{0}|{1}", rowcode, colcode);
                        var cellobj = new Cell();
                        cellobj.Type = 1 /* Data */;
                        cellobj.RowID = rowcode;
                        cellobj.ColID = colcode;
                        cellobj.Value = _Html(cell).trim();
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
        return Cell;
    })();
    Controls.Cell = Cell;
    var Row = (function () {
        function Row() {
            this.RowID = "";
            this.Cells = [];
        }
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
            this.ColumnHeader = null;
            this.RowHeader = null;
            this.Rows = [];
            this.Columns = [];
            this.Cells = [];
            this.HeaderRowCount = 0;
            this.HeaderColCount = 0;
            this.Keys = [];
            this.Manager = manager;
            manager.Table;
        }
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
            this.Manager.LoadLayoutFromHtml(element, this);
        };
        Table.prototype.AddRow = function (index) {
            if (index === void 0) { index = -1; }
            var me = this;
            var templaterow = this.Rows[0];
            var newrow = me.GetNewRow();
            var newelement = _Clone(templaterow.UIElement);
            this.SetRow(newrow, newelement);
            var newrowHeaderCell = newrow.Cells.AsLinq().Last(function (i) { return i.Type == 2 /* Header */; });
            var rowid = Format(Table.RowID_Format, me.RowHeader.Cells.length);
            newrowHeaderCell.RowID = rowid;
            newrow.RowID = rowid;
            _Html(newrowHeaderCell.UIElement, rowid);
            this.Cells.concat(newrow.Cells);
            this.RowHeader.Cells.push(newrowHeaderCell);
            this.Rows.push(newrow);
            CallFunctionVariable(me.Manager.OnRowRemoved, [newrow]);
            this.LoadEventHandlers();
            return newrow;
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
            var ix = this.Rows.indexOf(row);
            row.Cells.forEach(function (cell, ix) {
                RemoveFrom(cell, me.Cells);
            });
            RemoveFrom(row, me.RowHeader.Cells);
            RemoveFrom(row, me.Rows);
            CallFunctionVariable(me.Manager.OnRowRemoved, [row]);
            return true;
        };
        Table.prototype.SetRow = function (row, element) {
            var headercells = _Select("th", element);
            var datacells = _Select("td", element);
            row.Cells = [];
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