class HtmlElements {
    public Elements: HTMLElement[] = [];
    constructor(items?: any) {
        if (IsArray(items)) {
            this.Elements = items;
        } else {
            this.Elements.push(items);
        }
    }

}



module Controls {
    export interface ITableManager {
        RowID_Format: string;
        ColumnID_Format: string;
        ExtensionID_Format: string;

        TemplateRow: Row;
        TemplateColumn: Column;

        LoadToUI(data: Object);
        LoadPage(page: number, asyncdatagetter: Function, callback: Function);

        LoadLayoutFromData(data: any, table: Table);
        LoadLayoutFromHtml(element: Element, table: Table);
        Validate(): boolean;
        Save(): boolean;
        EditCell(cell: Cell);

        ManageRows(table: Table);
        Clear(table: Table);

        CellEditorAssigner: Function;

        OnLoaded: Function;
        OnCellSelected: Function;
        OnRowSelected: Function;
        OnColumnSelected: Function;
        OnCellChanged: Function;
        OnRowAdded: Function;
        OnRowRemoved: Function;
        OnColumnAdded: Function;
        OnColumnRemoved: Function;
        OnLayoutChanged: Function;
    }

    export enum CellType {
        Unknown = 0,
        Data = 1,
        Header = 2,
        Placeholder = 3,
        Breakdown = 4,
    }

    export class Cell {
        public RowID: string = "";
        public ColID: string = "";
        public Value: string = "";
        public Type: CellType = CellType.Unknown;
        public UIElement: Element = null;
        public static Clear(cell: Cell) {
            _Html(cell.UIElement, "");
        }
        public static ConvertFrom(element: Element): Cell {
            var cell = new Cell();
            var tag = _Property(element, "tagName");
            if (tag.toLowerCase() == "td") {
                cell.Type = CellType.Data;
            }
            if (tag.toLowerCase() == "th") {
                cell.Type = CellType.Header;
            }
            cell.Value = _Html(element).trim();
            cell.UIElement = element;
            return cell;
        }

        public HasData(): boolean {
            return !IsNull(_Html(this.UIElement).trim());
        }
    }

    export class Row {
        public RowID: string = "";
        public HeaderCell: Cell = null;
        public Cells: Cell[] = [];
        public He
        public UIElement: Element;

        public HasData(): boolean {
            var cellswithData = this.Cells.AsLinq<Cell>().FirstOrDefault(i=> i.HasData());
            return cellswithData != null;
        }

        public static SetRowID(row: Controls.Row, ID: string) {
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
        }

        public static SetRowFromElement(row: Row, element: Element) {
            var headercells = _Select("th", element);
            var datacells = _Select("td", element);
            row.Cells = [];
            var headercellelement = headercells.AsLinq<Element>().LastOrDefault();
            if (!IsNull(headercellelement)) {
                row.HeaderCell = Cell.ConvertFrom(headercellelement);
            }
            row.UIElement = element;
            datacells.forEach(function (cellelement, ix) {
                var cell = Cell.ConvertFrom(cellelement);
                row.Cells.push(cell);

            });

        }

        public static ClearDataCells(row: Row) {
            row.Cells.forEach(function (cell, ix) {
                _Html(cell.UIElement, "");
            });
        }
    }

    export class Column {
        public ColID: string = "";
        public Cells: Cell[] = [];
        public UIElement: Element;

    }

    export class Table {
        public Manager: ITableManager = null;
        public UIElement: Element = null;
        public ColumnHeader: Row = null;
        public RowHeader: Column = null;
        public Rows: Row[] = [];
        public Columns: Column[] = [];
        public Cells: Cell[] = [];

        public HeaderRowCount: number = 0;
        public HeaderColCount: number = 0;

        public Keys: string[] = [];
        public CanManageRows: boolean = true;

        //public static RowID_Format: string="R{0:D4}";
        //public static ColumnID_Format: string = "C{0:D4}";

        public OnRowRemoved: Function = function (row: Row) {
            var me = <Table>(this);
            //me.ManageRows();

            CallFunction(me.Manager.OnRowRemoved, [row]);
            //me.Manager.OnRowRemoved(row);
        }
        public OnRowAdded: Function = function (row: Row) {
            var me = <Table>(this);
            //me.ManageRows();
         
            CallFunction(me.Manager.OnRowAdded, [row]);
      
            //me.Manager.OnRowAdded(row);
        }

        public OnLayoutChanged: Function = function (row: Row) {
            var me = <Table>(this);
            CallFunction(me.Manager.OnLayoutChanged, [row]);

        }

        constructor(manager: ITableManager) {
            this.Manager = manager;
        }

        public GetRowOfCell(cellelement: Element): Row {
            var me = this;
            var result: Row = null;
            var rowelement = _Parent(cellelement);
            me.Rows.forEach(function (row, ix) {
                if (rowelement == row.UIElement) {
                    result = row;
                }
            });
            return result;
        }

        public ValidateRow(rowid: string): boolean {
            return true;
        }
        public GetNewRow(): Row {
            var newrow = new Row();
            return newrow
        }

        private LoadEventHandlers() {
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
        }
        private LoadRowEventHandlers(row: Row) {
            var me = this;
            _EnsureEventHandler(row.UIElement, "click", me.Manager.OnRowSelected);
            row.Cells.forEach(function (cell, ix) {
                //_EnsureEventHandler(cell.UIElement, "click", me.Manager.OnCellSelected);
            });
        }

        public LoadfromHtml(element: Element) {
            this.UIElement = element;
            this.Manager.LoadLayoutFromHtml(element, this);
            this.LoadEventHandlers();
        }

        public AddRow(index: number = -1, id: string = ""): Row {
            var me = this;

            var templaterow = this.Manager.TemplateRow;
            var indexedrow = (index > -1 && index < this.Rows.length) ? this.Rows[0] : null;
            var lastrow = this.Rows.AsLinq<Row>().LastOrDefault()
            var referencerow = (index == -1) ? IsNull(lastrow) ? templaterow.UIElement : lastrow.UIElement : indexedrow.UIElement;
            var newrow = me.GetNewRow();
            var newelement = _Clone(templaterow.UIElement);
            Row.SetRowFromElement(newrow, newelement);
            Controls.Row.SetRowID(newrow, id);
            Row.ClearDataCells(newrow);

            var newrowHeaderCell = newrow.Cells.AsLinq<Cell>().LastOrDefault(i=> i.Type == CellType.Header);

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

            me.LoadRowEventHandlers(newrow);
            //ShowNotification(Format("Row {0} was added!", newrow.RowID));

            return newrow;
        }



        public GetRowByID(id: string): Row {
            var row = this.Rows.AsLinq<Row>().FirstOrDefault(i=> i.RowID == id);
            return row;
        }
        public GetRowByElement(element: Element): Row {
            var row = this.Rows.AsLinq<Row>().FirstOrDefault(i=> i.UIElement == element);
            return row;
        }

        public RemoveRowByID(rowid: string) {
       
            var rowtoremove = this.GetRowByID(rowid);
            if (!IsNull(rowtoremove) && rowtoremove.RowID!="emptyrow") {
                this.RemoveRow(rowtoremove);
            }

       
        }

        public RemoveRow(row: Row): boolean {
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
                //ShowNotification(Format("Row {0} was removed!", row.RowID));

            } else {
                //ShowNotification(Format("Row {0} was NOT removed! The last row can't be removed!", row.RowID));

            }
            return true;
        }

    }

    export class ReportTable extends Table {
        public Extensions: Table = null;
    }
} 