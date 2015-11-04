class HtmlElements {
    public Elements: HTMLElement[] = [];
    constructor(items?:any)
    {
        if (IsArray(items)) {
            this.Elements = items;
        } else
        {
            this.Elements.push(items);
        }
    }

}



module Controls
{
    export interface ITableManager
    {
        Table: Table;

        LoadToUI(data: Object);
        LoadPage(page: number, asyncdatagetter: Function, callback: Function);

        LoadLayoutFromData(data: any, table: Table);
        LoadLayoutFromHtml(element: Element, table: Table);
        Validate(): boolean;
        Save(): boolean;
        EditCell(cell: Cell);

        CellEditorAssigner: Function;

        OnLoaded: Function;
        OnCellSelected: Function;
        OnRowSelected: Function;
        OnColumnSelected: Function;
        OnCellChanged: Function;
        OnRowAdded: Function;
        OnRowRemoved: Function;
        OnColumnAdded: Function;
        OncolumnRemoved: Function;
    }

    export class TableManager implements ITableManager
    {
        public Table: Table;

        public LoadToUI(data: Object)
        {

        }
        public LoadPage(page: number, asyncdatagetter: Function, callback: Function)
        {

        }

        LoadLayoutFromData(data: any, table: Table)
        {
        }

        LoadLayoutFromHtml(element: Element, table:Table)
        {
            var me = this;
            var rawrows = _Select("tr", element);
            var headerix = 0;
            var columncells: Element[] = []; 
            var rowcells: Element[] = []; 
            table.HeaderRowCount = _Select("thead tr", element).length;

            rawrows.forEach(function (rawrow, ix) {
                var rawdatacells = _Select("td", rawrow);
                var rawheadercells = _Select("th", rawrow);
                if (rawdatacells.length > 0)
                {
                    if (columncells.length < 1)
                    {
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
                        cellobj.Type = CellType.Data;
                        cellobj.RowID = rowcode;
                        cellobj.ColID = colcode;
                        cellobj.Value = _Html(cell).trim();
                        cellobj.UIElement = cell;
                        row.Cells.push(cellobj);
                    });
                    table.Rows.push(row);
                    if (rawheadercells.length > table.HeaderColCount)
                    {
                        table.HeaderColCount = rawheadercells.length;
                    }
                }
            });

            var rowheader: Row = new Row();
            rowheader.Cells = rowcells.AsLinq<Element>().Select(i=> Cell.ConvertFrom(i)).ToArray();

            var colheader: Column = new Column();
            colheader.Cells = columncells.AsLinq<Element>().Select(i=> Cell.ConvertFrom(i)).ToArray();

            table.RowHeader = colheader;
            table.ColumnHeader = rowheader;
            CallFunction(me.OnLoaded, [me]);

        }

        public Validate(): boolean
        {
            return true;
        }

        public Save(): boolean
        {
            return true;
        }

        public EditCell(cell: Cell)
        {
            return true;
        }

        public CellEditorAssigner: Function = null;

        public OnLoaded: Function = null;
        public OnCellSelected: Function = null;
        public OnRowSelected: Function = null;
        public OnColumnSelected: Function = null;
        public OnCellChanged: Function = null;
        public OnRowAdded: Function = null;
        public OnRowRemoved: Function = null;
        public OnColumnAdded: Function = null;
        public OncolumnRemoved: Function = null;
  
    }

    export enum CellType
    {
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

        public HasData(): boolean
        {
            var cellswithData = this.Cells.AsLinq<Cell>().FirstOrDefault(i=> i.HasData());
            return cellswithData != null;
        } 

        public IsDynamic(): boolean {
            return _HasClass(this.UIElement, "dynamic");
        } 

        public static ClearDataCells(row: Row)
        {
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

        public static RowID_Format: string="R{0:D4}";
        public static ColumnID_Format: string = "C{0:D4}";

        public OnRowRemoved: Function = function (row: Row)
        {
            var me = <Table>(this);
            me.ManageRows();
            CallFunction(me.Manager.OnRowRemoved, [row]);
            //me.Manager.OnRowRemoved(row);
        }
        public OnRowAdded: Function = function (row: Row) {
            var me = <Table>(this);
            me.ManageRows();
            CallFunction(me.Manager.OnRowAdded, [row]);
            //me.Manager.OnRowAdded(row);
        }

        constructor(manager: ITableManager)
        {
            this.Manager = manager;
            manager.Table
        }

        public ManageRows()
        {
            var me = this;
            var rowsquery = me.Rows.AsLinq<Row>();
            var dynamicrow = rowsquery.FirstOrDefault(i=>i.IsDynamic());
            if (!IsNull(dynamicrow || 1==1))
            {
                var emptyrow = rowsquery.FirstOrDefault(i=> !i.HasData());
                if (IsNull(emptyrow))
                {
                    me.AddRow(-1);
                }
            }
            me.SetRowIds();
        }

        public GetRowOfCell(cellelement: Element): Row {
            var me = this;
            var result: Row = null;
            var rowelement = _Parent(cellelement);
            me.Rows.forEach(function (row, ix) {
                if (rowelement == row.UIElement)
                {
                    result = row;
                }
            });
            return result;
        }

        public ValidateRow(rowid: string): boolean
        {
            return true;
        }
        public GetNewRow(): Row
        {
            var newrow = new Row();
            return newrow
        }

        private LoadEventHandlers()
        {
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
        }

        public LoadfromHtml(element:Element)
        {
            this.UIElement = element;
            this.Manager.LoadLayoutFromHtml(element, this);
            this.ManageRows()
        }

        public AddRow(index:number=-1):Row
        {
            var me = this;

            var templaterow = this.Rows[0];
            var indexedrow = (index > -1 && index < this.Rows.length) ? this.Rows[0] : null;
            var referencerow = (index == -1) ? this.Rows.AsLinq<Row>().LastOrDefault().UIElement : indexedrow.UIElement;
            var newrow = me.GetNewRow();
            var newelement = _Clone(templaterow.UIElement);
            me.SetRow(newrow, newelement);
            Row.ClearDataCells(newrow);
            
            var newrowHeaderCell = newrow.Cells.AsLinq<Cell>().LastOrDefault(i=> i.Type == CellType.Header);
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
            else
            {
                _Before(referencerow, newrow.UIElement);
            }
            CallFunctionWithContext(me, me.OnRowAdded, [newrow]);
            me.LoadEventHandlers();
            return newrow;
        }

        private SetRowIds()
        {
            var me = this;
            this.Rows.forEach(function (row, ix) {
                row.RowID = Format(Table.RowID_Format, ix);
                var headercell = row.HeaderCell;
                var existingrowid= _Html(headercell.UIElement).trim();
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
                        var rowtodelete: Row = null; 
                        me.Rows.forEach(function (row, ix) {
                            if (_HasClass( row.UIElement, "selected"))
                            {
                                rowtodelete = row;
                            }
                        });
                        if (!IsNull(rowtodelete))
                        {
                            me.RemoveRow(rowtodelete);
                        }
                    }
                });
            });
        }

        public GetRowByID(id: string): Row
        {
            var row = this.Rows.AsLinq<Row>().FirstOrDefault(i=> i.RowID == id);
            return row;
        }

        public RemoveRowByID(rowid: string)
        {
            if (this.Rows.length > 1) {
                var rowtoremove = this.GetRowByID(rowid);
                this.RemoveRow(rowtoremove);

            }
        }

        public RemoveRow(row: Row):boolean
        {
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
            } else
            {
                console.log("The last row can't be removed!");
            }
            return true;
        }

        private SetRow(row: Row, element: Element)
        {
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
    }

    export class ReportTable extends Table {
        public Extensions: Table = null;
    }
} 