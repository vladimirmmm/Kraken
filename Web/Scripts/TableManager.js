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
            this.CurrentFactKey = [];
        }
        Cell.Clear = function (cell) {
            _Html(cell.UIElement, "");
        };
        Cell.ConvertFrom = function (element, celltype) {
            var cell = new Cell();
            cell.Type = celltype;
            cell.Value = _Html(element).trim();
            cell.UIElement = element;
            return cell;
        };
        Cell.SetID = function (cell, id) {
            if (id === void 0) { id = ""; }
            var cellid = Format("{0}|{1}", cell.RowID, cell.ColID);
            _Attribute(cell.UIElement, "id", cellid);
        };
        Cell.prototype.HasData = function () {
            return !IsNull(_Html(this.UIElement).trim());
        };
        Cell.prototype.IsKey = function () {
            return _HasClass(this.UIElement, "key");
        };
        Cell.prototype.IsBlocked = function () {
            return _HasClass(this.UIElement, "blocked");
        };
        Cell.GetKeysFromElement = function (element) {
            var result = [];
            var keystr = _Attribute(element, "factkeys");
            if (IsNull(keystr)) {
                return [];
            }
            var keystrarray = keystr.split(",");
            keystrarray.forEach(function (item) {
                result.push(parseInt(item));
            });
            return result;
        };
        Cell.GetColumn = function (cell, table) {
            var column = table.GetColumnByID(cell.ColID);
            if (IsNull(column)) {
                column = table.Manager.TemplateColumn.ID == cell.ColID ? table.Manager.TemplateColumn : null;
            }
            return column;
        };
        Cell.GetRow = function (cell, table) {
            var row = table.GetRowByID(cell.RowID);
            if (IsNull(row)) {
                row = table.Manager.TemplateRow.ID == cell.RowID ? table.Manager.TemplateRow : null;
            }
            return row;
        };
        Cell.prototype.GetAxisKeys = function (table) {
            var me = this;
            var result = [];
            var column = Cell.GetColumn(me, table);
            var row = Cell.GetRow(me, table);
            var sheet = table.Sheets[0];
            var colkeys = Cell.GetKeysFromElement(column.HeaderCell.UIElement);
            var rowkeys = Cell.GetKeysFromElement(row.HeaderCell.UIElement);
            var sheetkeys = Cell.GetKeysFromElement(sheet.HeaderCell.UIElement);
            result = rowkeys.concat(colkeys).concat(sheetkeys);
            return result;
        };
        Cell.prototype.GetFactKey_Old = function () {
            var result = [];
            var strfactkeys = _Attribute(this.UIElement, "factkeys");
            strfactkeys = IsNull(strfactkeys) ? "" : strfactkeys;
            var strkeysarray = strfactkeys.split(",");
            strkeysarray.forEach(function (item) {
                result.push(parseInt(item));
            });
            return result;
        };
        Cell.prototype.SetFactKeyByString = function (key) {
            _Attribute(this.UIElement, "factkeys", key);
        };
        Cell.prototype.SetFactKey = function (key) {
            _Attribute(this.UIElement, "factkeys", key.join(","));
        };
        Cell.prototype.GetFactString = function () {
            var strfactkeys = _Attribute(this.UIElement, "factstring");
            strfactkeys = IsNull(strfactkeys) ? "" : strfactkeys;
            return strfactkeys;
        };
        Cell.prototype.GetFactID = function () {
            var strfactkeys = _Attribute(this.UIElement, "factid");
            strfactkeys = IsNull(strfactkeys) ? "" : strfactkeys;
            return strfactkeys;
        };
        Cell.prototype.SetFactString = function (key) {
            _Attribute(this.UIElement, "factstring", key);
        };
        return Cell;
    })();
    Controls.Cell = Cell;
    var CellContainer = (function () {
        function CellContainer() {
            this.ID = "";
            this.HeaderCell = null;
            this.Cells = [];
        }
        CellContainer.SetID = function (container, id) {
            container.ID = id;
            var headercell = container.HeaderCell;
            var existingrowid = _Html(headercell.UIElement).trim();
            _Html(headercell.UIElement, id);
            _Attribute(container.UIElement, "id", id);
        };
        CellContainer.prototype.HasData = function () {
            var cellswithData = this.Cells.AsLinq().FirstOrDefault(function (i) { return i.HasData(); });
            return cellswithData != null;
        };
        return CellContainer;
    })();
    Controls.CellContainer = CellContainer;
    var Sheet = (function (_super) {
        __extends(Sheet, _super);
        function Sheet() {
            _super.apply(this, arguments);
        }
        return Sheet;
    })(CellContainer);
    Controls.Sheet = Sheet;
    var Row = (function (_super) {
        __extends(Row, _super);
        function Row() {
            _super.apply(this, arguments);
        }
        //public RowID: string = "";
        //public UIElement: Element;
        Row.SetRowFromElement = function (row, element) {
            var headercells = _Select("th", element);
            var datacells = _Select("td", element);
            row.Cells = [];
            var headercellelement = headercells.AsLinq().LastOrDefault();
            if (!IsNull(headercellelement)) {
                row.HeaderCell = Cell.ConvertFrom(headercellelement, 2 /* Header */);
            }
            row.UIElement = element;
            datacells.forEach(function (cellelement, ix) {
                var cell = Cell.ConvertFrom(cellelement, 1 /* Data */);
                row.Cells.push(cell);
            });
        };
        Row.SetHeaderRowFromElement = function (row, element) {
            var headercells = _Select("th", element);
            row.Cells = [];
            row.UIElement = element;
            headercells.forEach(function (cellelement, ix) {
                var cell = Cell.ConvertFrom(cellelement, 2 /* Header */);
                row.Cells.push(cell);
            });
        };
        Row.ClearDataCells = function (row) {
            row.Cells.forEach(function (cell, ix) {
                _Html(cell.UIElement, "");
            });
        };
        return Row;
    })(CellContainer);
    Controls.Row = Row;
    var Column = (function (_super) {
        __extends(Column, _super);
        function Column() {
            _super.apply(this, arguments);
        }
        //public ColID: string = "";
        Column.RemoveFromUI = function (col) {
        };
        return Column;
    })(CellContainer);
    Controls.Column = Column;
    var Table = (function () {
        function Table(manager) {
            this.Manager = null;
            this.UIElement = null;
            this.BodyUIElement = null;
            this.ColumnHeader = null;
            this.RowHeader = null;
            this.Rows = [];
            this.Columns = [];
            this.Sheets = [];
            this.Cells = [];
            this.HeaderRows = [];
            this.HeaderRowCount = 0;
            this.HeaderColCount = 0;
            this.Keys = [];
            this.CanManageRows = true;
            this.CanManageColumns = true;
            //public static RowID_Format: string="R{0:D4}";
            //public static ColumnID_Format: string = "C{0:D4}";
            this.OnRowRemoved = function (row) {
                var me = (this);
                //me.ManageRows();
                CallFunction(me.Manager.OnRowRemoved, [row]);
                //me.Manager.OnRowRemoved(row);
            };
            this.OnColumnRemoved = function (col) {
                var me = (this);
                //me.ManageRows();
                CallFunction(me.Manager.OnColumnRemoved, [col]);
                //me.Manager.OnRowRemoved(row);
            };
            this.OnRowAdded = function (row) {
                var me = (this);
                //me.ManageRows();
                CallFunction(me.Manager.OnRowAdded, [row]);
                //me.Manager.OnRowAdded(row);
            };
            this.OnColumnAdded = function (col) {
                var me = (this);
                //me.ManageRows();
                CallFunction(me.Manager.OnColumnAdded, [col]);
                //me.Manager.OnRowAdded(row);
            };
            this.OnCellChanged = function (cell, value) {
                var me = (this);
                //me.ManageRows();
                CallFunction(me.Manager.OnCellChanged, [cell, value]);
                //me.Manager.OnRowAdded(row);
            };
            this.OnLayoutChanged = function (row) {
                var me = (this);
                CallFunction(me.Manager.OnLayoutChanged, [row]);
            };
            this.DeleteFunction = null;
            this.Manager = manager;
            var me = this;
            this.DeleteFunction = function (e) {
                if (e.which == 46) {
                    var rowtodelete = null;
                    me.Rows.forEach(function (row, ix) {
                        if (_HasClass(row.UIElement, selectedclass)) {
                            rowtodelete = row;
                        }
                    });
                    if (!IsNull(rowtodelete)) {
                        me.RemoveRow(rowtodelete);
                    }
                }
            };
        }
        Table.prototype.GetCellByElement = function (element) {
            var me = this;
            var cell = me.Cells.AsLinq().FirstOrDefault(function (i) { return i.UIElement == element; });
            ;
            return cell;
        };
        Table.prototype.GetCellByID = function (id) {
            var parts = id.split("|");
            var rowid = parts[0];
            var colid = parts[1];
            var row = this.GetRowByID(rowid);
            var cell = null;
            if (IsNull(row)) {
                Log("Row not found " + id);
            }
            else {
                cell = row.Cells.AsLinq().FirstOrDefault(function (i) { return i.ColID == colid; });
                if (IsNull(cell)) {
                    Log("Cell not found " + id);
                }
            }
            return cell;
        };
        Table.prototype.GetRowOfCell = function (cell) {
            var me = this;
            var result = null;
            cell.RowID;
            result = me.Rows.AsLinq().FirstOrDefault(function (i) { return i.ID == cell.RowID; });
            return result;
        };
        Table.prototype.GetColOfCell = function (cell) {
            var me = this;
            var result = null;
            result = me.Columns.AsLinq().FirstOrDefault(function (i) { return i.ID == cell.ColID; });
            return result;
        };
        Table.prototype.ValidateRow = function (rowid) {
            return true;
        };
        Table.prototype.LoadEventHandlers = function () {
            var me = this;
            me.Rows.forEach(function (row, ix) {
                me.LoadRowEventHandlers(row);
                //_EnsureEventHandler(row.UIElement, "click", me.Manager.OnRowSelected);
            });
            //me.Cells.forEach(function (cell, ix) {
            //    //_EnsureEventHandler(cell.UIElement, "click", me.Manager.OnCellSelected);
            //});
            //me.Columns.forEach(function (column, ix) {
            //    _EnsureEventHandler(column.UIElement, "click", me.Manager.OnColumnSelected);
            //});
        };
        Table.prototype.LoadRowEventHandlers = function (row) {
            var me = this;
            _EnsureEventHandler(row.UIElement, "click", function () { return me.Manager.OnRowSelected(row); });
            row.Cells.forEach(function (cell, ix) {
                //_EnsureEventHandler(cell.UIElement, "click", me.Manager.OnCellSelected);
            });
        };
        Table.prototype.LoadColEventHandlers = function (col) {
            var me = this;
            //_EnsureEventHandler(row.UIElement, "click",() => me.Manager.OnRowSelected(row));
            //row.Cells.forEach(function (cell, ix) {
            //    //_EnsureEventHandler(cell.UIElement, "click", me.Manager.OnCellSelected);
            //});
        };
        Table.prototype.LoadfromHtml = function (element) {
            this.UIElement = element;
            this.BodyUIElement = _SelectFirst("tbody", this.UIElement);
            this.Manager.LoadLayoutFromHtml(element, this);
            this.LoadEventHandlers();
        };
        Table.prototype.SetSheet = function (element) {
            var me = this;
            if (me.Sheets.length == 0) {
                var sh = new Sheet();
                var hcell = new Cell();
                hcell.UIElement = element;
                sh.HeaderCell = hcell;
                me.Sheets.push(sh);
            }
            me.Sheets[0].HeaderCell.UIElement = element;
        };
        Table.prototype.AddRow = function (index, id) {
            if (index === void 0) { index = -1; }
            if (id === void 0) { id = ""; }
            var me = this;
            var templaterow = this.Manager.TemplateRow;
            var indexedrow = (index > -1 && index < this.Rows.length) ? this.Rows[0] : null;
            var lastrow = this.Rows.AsLinq().LastOrDefault();
            var referencerow = (index == -1) ? IsNull(lastrow) ? null : lastrow.UIElement : indexedrow.UIElement;
            var newrow = new Row();
            var newelement = _Clone(templaterow.UIElement);
            Row.SetRowFromElement(newrow, newelement);
            Controls.Row.SetID(newrow, id);
            newrow.Cells.forEach(function (cell, ix) {
                var column = me.Columns[ix];
                //var cellid = Format("{0}|{1}", id, column.ID);
                //_Attribute(cell.UIElement, "id", cellid);
            });
            for (var i = 0; i < me.Columns.length; i++) {
                var cell = newrow.Cells[i];
                var column = me.Columns[i];
                cell.ColID = column.ID;
                cell.RowID = id;
            }
            //Row.ClearDataCells(newrow);
            var newrowHeaderCell = newrow.Cells.AsLinq().LastOrDefault(function (i) { return i.Type == 2 /* Header */; });
            if (!IsNull(newrowHeaderCell)) {
                me.RowHeader.Cells.push(newrowHeaderCell);
            }
            me.Cells = me.Cells.concat(newrow.Cells);
            me.Rows.push(newrow);
            if ($.contains(me.BodyUIElement, referencerow)) {
                if (index == -1) {
                    _After(referencerow, newrow.UIElement);
                }
                else {
                    _Before(referencerow, newrow.UIElement);
                }
            }
            else {
                _Append(me.BodyUIElement, newrow.UIElement);
            }
            CallFunctionWithContext(me, me.OnRowAdded, [newrow]);
            CallFunctionWithContext(me, me.OnLayoutChanged, [newrow]);
            me.LoadRowEventHandlers(newrow);
            //ShowNotification(Format("Row {0} was added!", newrow.RowID));
            return newrow;
        };
        Table.prototype.AddColumn = function (index, id) {
            if (index === void 0) { index = -1; }
            if (id === void 0) { id = ""; }
            var me = this;
            var templatecol = this.Manager.TemplateColumn;
            var indexedcol = (index > -1 && index < this.Columns.length) ? this.Columns[0] : null;
            var lastcol = this.Columns.AsLinq().LastOrDefault();
            var referencecol = (index == -1) ? IsNull(lastcol) ? null : lastcol.UIElement : indexedcol.UIElement;
            var newcol = new Column();
            newcol.UIElement = _Clone(templatecol.UIElement);
            _Append(me.ColumnHeader.UIElement, newcol.UIElement);
            var headercell = Cell.ConvertFrom(newcol.UIElement, 2 /* Header */);
            newcol.HeaderCell = headercell;
            var extensioncolspan = Number(_Attribute(_SelectFirst("th#Extension"), "colspan"));
            for (var i = 0; i < me.HeaderRows.length - 1; i++) {
                var lastcell = LastFrom(me.HeaderRows[i].Cells);
                var existingcolspan = _Attribute(lastcell.UIElement, "colspan");
                if (IsNull(existingcolspan)) {
                    existingcolspan = "1";
                }
                var newcolspan = Number(existingcolspan) + 1;
                //var colspan = 
                _Attribute(lastcell.UIElement, "colspan", Format("{0}", newcolspan));
            }
            var templatecolumncontainer = _Append(_Parent(templatecol.UIElement), newcol.UIElement);
            this.Rows.forEach(function (row, ix) {
                var cell = new Cell();
                cell.UIElement = _Clone(templatecol.Cells[ix].UIElement);
                _Append(row.UIElement, cell.UIElement);
                cell.RowID = row.ID;
                cell.ColID = id;
                newcol.Cells.push(cell);
            });
            //Row.SetRowFromElement(newrow, newelement);
            Controls.Column.SetID(newcol, id);
            for (var i = 0; i < me.Rows.length; i++) {
                var cell = newcol.Cells[i];
                var row = me.Rows[i];
                cell.ColID = id;
                cell.RowID = row.ID;
            }
            //Row.ClearDataCells(newrow);
            var newcolHeaderCell = newcol.Cells.AsLinq().LastOrDefault(function (i) { return i.Type == 2 /* Header */; });
            if (!IsNull(newcolHeaderCell)) {
                me.ColumnHeader.Cells.push(newcolHeaderCell);
            }
            me.Cells = me.Cells.concat(newcol.Cells);
            me.Columns.push(newcol);
            CallFunctionWithContext(me, me.OnColumnAdded, [newcol]);
            CallFunctionWithContext(me, me.OnLayoutChanged, [newcol]);
            me.LoadColEventHandlers(newcol);
            //ShowNotification(Format("Row {0} was added!", newrow.RowID));
            return newcol;
        };
        Table.prototype.GetRowByID = function (id) {
            var row = this.Rows.AsLinq().FirstOrDefault(function (i) { return i.ID == id; });
            return row;
        };
        Table.prototype.GetColumnByID = function (id) {
            var column = this.Columns.AsLinq().FirstOrDefault(function (i) { return i.ID == id; });
            return column;
        };
        Table.prototype.GetRowByElement = function (element) {
            var rowelement = _TagName(element) == "TR" ? element : _Parent(element);
            var row = this.Rows.AsLinq().FirstOrDefault(function (i) { return i.UIElement == element; });
            return row;
        };
        Table.prototype.GetColByElement = function (element) {
            var rowelement = _Parent(element);
            var me = this;
            var result = null;
            var ix = Property(element, "cellIndex") - _Select("th", rowelement).length;
            if (ix > -1 && ix < me.Columns.length) {
                result = me.Columns[ix];
            }
            return result;
        };
        Table.prototype.RemoveRowByID = function (rowid) {
            var rowtoremove = this.GetRowByID(rowid);
            if (!IsNull(rowtoremove) && rowtoremove.ID != "emptyrow") {
                this.RemoveRow(rowtoremove);
            }
        };
        Table.prototype.RemoveRow = function (row) {
            var me = this;
            //if (me.Rows.length > 1) {
            var ix = this.Rows.indexOf(row);
            row.Cells.forEach(function (cell, ix) {
                RemoveFrom(cell, me.Cells);
            });
            RemoveFrom(row, me.RowHeader.Cells);
            RemoveFrom(row, me.Rows);
            _Remove(row.UIElement);
            CallFunctionWithContext(me, me.OnRowRemoved, [row]);
            CallFunctionWithContext(me, me.OnLayoutChanged, [row]);
            //} else {
            //
            //}
            return true;
        };
        Table.prototype.RemoveColumnByID = function (colid) {
            var coltoremove = this.GetColumnByID(colid);
            if (!IsNull(coltoremove) && coltoremove.ID != "emptycolumn") {
                this.RemoveColumn(coltoremove);
            }
        };
        Table.prototype.RemoveColumn = function (col) {
            var me = this;
            //if (me.Columns.length > 1) {
            var ix = this.Columns.indexOf(col);
            col.Cells.forEach(function (cell, ix) {
                _Remove(cell.UIElement);
                RemoveFrom(cell, me.Cells);
            });
            col.Cells = [];
            RemoveFrom(col, me.ColumnHeader.Cells);
            RemoveFrom(col, me.Columns);
            _Remove(col.UIElement);
            CallFunctionWithContext(me, me.OnColumnRemoved, [col]);
            CallFunctionWithContext(me, me.OnLayoutChanged, [col]);
            //} else {
            //}
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