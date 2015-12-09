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
        ManageColumns(table: Table);

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
        public static ConvertFrom(element: Element, celltype:Controls.CellType): Cell {
            var cell = new Cell();
            cell.Type = celltype;

            cell.Value = _Html(element).trim();
            cell.UIElement = element;
            return cell;
        }

        public static SetID(cell: Cell, id: string = "") {
            if (!IsNull(id))
            {
               
            }
            var cellid = Format("{0}|{1}", cell.RowID, cell.ColID);
            _Attribute(cell.UIElement, "id", cellid);
        }

        public HasData(): boolean {
            return !IsNull(_Html(this.UIElement).trim());
        }
    }

    export class CellContainer implements IHasUI, IIdentifiable
    {
        public UIElement: Element;
        public ID: string = "";
        public HeaderCell: Cell = null;

        public Cells: Cell[] = [];
        public static SetID(container: CellContainer, id: string)
        {
            container.ID = id;
            var headercell = container.HeaderCell;
            var existingrowid = _Html(headercell.UIElement).trim();
            _Html(headercell.UIElement, id);
            _Attribute(container.UIElement, "id", id);
        }
        public HasData(): boolean {
            var cellswithData = this.Cells.AsLinq<Cell>().FirstOrDefault(i=> i.HasData());
            return cellswithData != null;
        }
    }
    interface IHasUI
    {
        UIElement: Element
    }
    interface IIdentifiable {
        ID: string
    }

    export class Row extends CellContainer {
        //public RowID: string = "";
        public UIElement: Element;

        public static SetRowFromElement(row: Row, element: Element) {
            var headercells = _Select("th", element);
            var datacells = _Select("td", element);
            row.Cells = [];
            var headercellelement = headercells.AsLinq<Element>().LastOrDefault();
            if (!IsNull(headercellelement)) {
                row.HeaderCell = Cell.ConvertFrom(headercellelement, Controls.CellType.Header);
            }
            row.UIElement = element;
            datacells.forEach(function (cellelement, ix) {
                var cell = Cell.ConvertFrom(cellelement, Controls.CellType.Data);

                row.Cells.push(cell);

            });

        }

        public static SetHeaderRowFromElement(row: Row, element: Element) {
            var headercells = _Select("th", element);
            row.Cells = [];
            row.UIElement = element;
            headercells.forEach(function (cellelement, ix) {
                var cell = Cell.ConvertFrom(cellelement, Controls.CellType.Header);
                row.Cells.push(cell);

            });

        }

        public static ClearDataCells(row: Row) {
            row.Cells.forEach(function (cell, ix) {
                _Html(cell.UIElement, "");
            });
        }
    }

    export class Column extends CellContainer {
        //public ColID: string = "";
        public static RemoveFromUI(col:Column)
        {

        }
    }

    export class Table {
        public Manager: ITableManager = null;
        public UIElement: Element = null;
        public BodyUIElement: Element = null;
        public ColumnHeader: Row = null;
        public RowHeader: Column = null;
        public Rows: Row[] = [];
        public Columns: Column[] = [];
        public Cells: Cell[] = [];

        public HeaderRows: Row[] = [];

        public HeaderRowCount: number = 0;
        public HeaderColCount: number = 0;

        public Keys: string[] = [];
        public CanManageRows: boolean = true;
        public CanManageColumns: boolean = true;

        //public static RowID_Format: string="R{0:D4}";
        //public static ColumnID_Format: string = "C{0:D4}";

        public OnRowRemoved: Function = function (row: Row) {
            var me = <Table>(this);
            //me.ManageRows();

            CallFunction(me.Manager.OnRowRemoved, [row]);
            //me.Manager.OnRowRemoved(row);
        }
        public OnColumnRemoved: Function = function (col: Column) {
            var me = <Table>(this);
            //me.ManageRows();

            CallFunction(me.Manager.OnColumnRemoved, [col]);
            //me.Manager.OnRowRemoved(row);
        }
        public OnRowAdded: Function = function (row: Row) {
            var me = <Table>(this);
            //me.ManageRows();
         
            CallFunction(me.Manager.OnRowAdded, [row]);
      
            //me.Manager.OnRowAdded(row);
        }

        public OnColumnAdded: Function = function (col: Column) {
            var me = <Table>(this);
            //me.ManageRows();
         
            CallFunction(me.Manager.OnColumnAdded, [col]);
      
            //me.Manager.OnRowAdded(row);
        }

        public OnLayoutChanged: Function = function (row: Row) {
            var me = <Table>(this);
            CallFunction(me.Manager.OnLayoutChanged, [row]);

        }
        public DeleteFunction: Function = null;

        constructor(manager: ITableManager) {
            this.Manager = manager;
            var me = this;
            this.DeleteFunction = function (e) {
                if (e.which == 46) {
                    var rowtodelete: Controls.Row = null;
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
        public GetCellByID(id: string): Cell {
            var parts = id.split("|");
            var rowid = parts[0];
            var colid = parts[1];
            var row = this.GetRowByID(rowid);
            var cell: Cell = null;
            if (IsNull(row)) {
                Log("Row not found " + id);
            } else {
                cell = row.Cells.AsLinq<Cell>().FirstOrDefault(i=> i.ColID == colid);
                if (IsNull(cell)) {
                    Log("Cell not found " + id);
                }
            }
            return cell;

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

        public GetColOfCell(cellelement: Element): Column {
            var me = this;
            var result: Column = null;
            var rowelement = _Parent(cellelement);
            var ix = Property(cellelement, "cellIndex") - _Select("th", rowelement).length;
            if (ix > -1 && ix < me.Columns.length)
            {
                result = me.Columns[ix];
            }
            return result;
        }

        public ValidateRow(rowid: string): boolean {
            return true;
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
        public LoadRowEventHandlers(row: Row) {
            var me = this;
            _EnsureEventHandler(row.UIElement, "click",() => me.Manager.OnRowSelected(row));
            row.Cells.forEach(function (cell, ix) {
                //_EnsureEventHandler(cell.UIElement, "click", me.Manager.OnCellSelected);
            });
        }

        public LoadColEventHandlers(col: Column) {
            var me = this;
            //_EnsureEventHandler(row.UIElement, "click",() => me.Manager.OnRowSelected(row));
            //row.Cells.forEach(function (cell, ix) {
            //    //_EnsureEventHandler(cell.UIElement, "click", me.Manager.OnCellSelected);
            //});
        }

        public LoadfromHtml(element: Element) {
            this.UIElement = element;
            this.BodyUIElement = _SelectFirst("tbody", this.UIElement);

            this.Manager.LoadLayoutFromHtml(element, this);
            this.LoadEventHandlers();
        }

        public AddRow(index: number = -1, id: string = ""): Row {
            var me = this;
            var templaterow = this.Manager.TemplateRow;
            var indexedrow = (index > -1 && index < this.Rows.length) ? this.Rows[0] : null;
            var lastrow = this.Rows.AsLinq<Row>().LastOrDefault()
            var referencerow = (index == -1) ? IsNull(lastrow) ? null : lastrow.UIElement : indexedrow.UIElement;
            var newrow = new Row();
            var newelement = _Clone(templaterow.UIElement);
            Row.SetRowFromElement(newrow, newelement);
    
            Controls.Row.SetID(newrow, id);
            for (var i = 0; i < me.Columns.length; i++) {
                var cell = newrow.Cells[i];
                var column = me.Columns[i];
                cell.ColID = column.ID;
                cell.RowID = id;
                Controls.Cell.SetID(cell);
            }
            //Row.ClearDataCells(newrow);

            var newrowHeaderCell = newrow.Cells.AsLinq<Cell>().LastOrDefault(i=> i.Type == CellType.Header);

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
            else
            {
                _Append(me.BodyUIElement, newrow.UIElement);
            }
            CallFunctionWithContext(me, me.OnRowAdded, [newrow]);
            CallFunctionWithContext(me, me.OnLayoutChanged, [newrow]);

            me.LoadRowEventHandlers(newrow);
            //ShowNotification(Format("Row {0} was added!", newrow.RowID));

            return newrow;
        }

        public AddColumn(index: number = -1, id: string = ""): Column {
            var me = this;
            var templatecol = this.Manager.TemplateColumn;
            var indexedcol = (index > -1 && index < this.Columns.length) ? this.Columns[0] : null;
            var lastcol = this.Columns.AsLinq<Column>().LastOrDefault()
            var referencecol = (index == -1) ? IsNull(lastcol) ? null : lastcol.UIElement : indexedcol.UIElement;

            var newcol = new Column();

            newcol.UIElement = _Clone(templatecol.UIElement);
            _Append(me.ColumnHeader.UIElement, newcol.UIElement);
            var headercell = Cell.ConvertFrom(newcol.UIElement, CellType.Header);
            newcol.HeaderCell = headercell;
            var extensioncolspan = Number(_Attribute(_SelectFirst("th#Extension"), "colspan"));
            for (var i = 0; i < me.HeaderRows.length - 1; i++)
            {
                var lastcell = <Cell>LastFrom(me.HeaderRows[i].Cells);
                var existingcolspan = _Attribute(lastcell.UIElement, "colspan");
                if (IsNull(existingcolspan))
                {
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
                Controls.Cell.SetID(cell);
            }
            //Row.ClearDataCells(newrow);

            var newcolHeaderCell = newcol.Cells.AsLinq<Cell>().LastOrDefault(i=> i.Type == CellType.Header);

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
        }

        public GetRowByID(id: string): Row {
            var row = this.Rows.AsLinq<Row>().FirstOrDefault(i=> i.ID == id);
            return row;
        }
        public GetColumnByID(id: string): Column {
            var column = this.Columns.AsLinq<Column>().FirstOrDefault(i=> i.ID == id);
            return column;
        }
        public GetRowByElement(element: Element): Row {
            var row = this.Rows.AsLinq<Row>().FirstOrDefault(i=> i.UIElement == element);
            return row;
        }

        public RemoveRowByID(rowid: string) {
       
            var rowtoremove = this.GetRowByID(rowid);
            if (!IsNull(rowtoremove) && rowtoremove.ID!="emptyrow") {
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
        public RemoveColumnByID(colid: string) {

            var coltoremove = this.GetColumnByID(colid);
            if (!IsNull(coltoremove) && coltoremove.ID != "emptycolumn") {
                this.RemoveColumn(coltoremove);
            }


        }
        public RemoveColumn(col: Column): boolean {
            var me = this;
            if (me.Columns.length > 1) {
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