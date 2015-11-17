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
        Cell.Clear = function (cell) {
            _Html(cell.UIElement, "");
        };
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
        Row.SetRowID = function (row, ID) {
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
        Row.SetRowFromElement = function (row, element) {
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
            this.CanManageRows = true;
            //public static RowID_Format: string="R{0:D4}";
            //public static ColumnID_Format: string = "C{0:D4}";
            this.OnRowRemoved = function (row) {
                var me = (this);
                //me.ManageRows();
                CallFunction(me.Manager.OnRowRemoved, [row]);
                //me.Manager.OnRowRemoved(row);
            };
            this.OnRowAdded = function (row) {
                var me = (this);
                //me.ManageRows();
                CallFunction(me.Manager.OnRowAdded, [row]);
                //me.Manager.OnRowAdded(row);
            };
            this.OnLayoutChanged = function (row) {
                var me = (this);
                CallFunction(me.Manager.OnLayoutChanged, [row]);
            };
            this.Manager = manager;
        }
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
        };
        Table.prototype.AddRow = function (index, id) {
            if (index === void 0) { index = -1; }
            if (id === void 0) { id = ""; }
            var me = this;
            var templaterow = this.Manager.TemplateRow;
            var indexedrow = (index > -1 && index < this.Rows.length) ? this.Rows[0] : null;
            var lastrow = this.Rows.AsLinq().LastOrDefault();
            var referencerow = (index == -1) ? IsNull(lastrow) ? templaterow.UIElement : lastrow.UIElement : indexedrow.UIElement;
            var newrow = me.GetNewRow();
            var newelement = _Clone(templaterow.UIElement);
            Row.SetRowFromElement(newrow, newelement);
            Controls.Row.SetRowID(newrow, id);
            Row.ClearDataCells(newrow);
            var newrowHeaderCell = newrow.Cells.AsLinq().LastOrDefault(function (i) { return i.Type == 2 /* Header */; });
            if (!IsNull(newrowHeaderCell)) {
                me.RowHeader.Cells.push(newrowHeaderCell);
            }
            me.Cells = me.Cells.concat(newrow.Cells);
            me.Rows.push(newrow);
            if (index == -1) {
                _After(referencerow, newrow.UIElement);
            }
            else {
                _Before(referencerow, newrow.UIElement);
            }
            CallFunctionWithContext(me, me.OnRowAdded, [newrow]);
            CallFunctionWithContext(me, me.OnLayoutChanged, [newrow]);
            me.LoadEventHandlers();
            //ShowNotification(Format("Row {0} was added!", newrow.RowID));
            return newrow;
        };
        Table.prototype.GetRowByID = function (id) {
            var row = this.Rows.AsLinq().FirstOrDefault(function (i) { return i.RowID == id; });
            return row;
        };
        Table.prototype.GetRowByElement = function (element) {
            var row = this.Rows.AsLinq().FirstOrDefault(function (i) { return i.UIElement == element; });
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
                CallFunctionWithContext(me, me.OnLayoutChanged, [row]);
            }
            else {
            }
            return true;
        };
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