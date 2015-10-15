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

        LoadLayoutFromData(data: any);
        LoadLayoutFromHtml(element: Element);
        Validate(): boolean;
        Save(): boolean;
        EditCell(cell: Cell);

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

        LoadLayoutFromData(data: any)
        {
        }

        LoadLayoutFromHtml(element: Element)
        {
            var me = this;
            var table = this.Table;
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
                        rowcells = _Select("th", rawrow);
                    }
                    var rowcell = rawheadercells[rawheadercells.length - 1];
                    rowcells.push(rowcell);
                    var row = new Row();
                    row.UIElement = rawrow;
                    rawdatacells.forEach(function (cell, ix) {
                        var rowcode = _Html(rowcell).trim();

                        var colcell = rowcells[ix];
                        var colcode = _Html(colcell).trim();

                        var cellid = Format("{0}|{1}", rowcode, colcode);

                        var cellobj = new Cell();
                        cellobj.Type = CellType.Data;
                        cellobj.RowID = rowcode;
                        cellobj.ColID = colcode;
                        cellobj.Value = _Html(cell).trim();
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
    }

    export class Row {
        public RowID: string = "";
        public Cells: Cell[] = [];
        public He
        public UIElement: Element;
    }

    export class Column {
        public ColID: string = "";
        public Cells: Cell[] = [];
        public UIElement: Element;

    }

    export class Table {
        public Manager: ITableManager = null;

        public ColumnHeader: Row = null;
        public RowHeader: Column = null;
        public Rows: Row[] = [];
        public Columns: Column[] = [];
        public Cells: Cell[] = [];

        public HeaderRowCount: number = 0;
        public HeaderColCount: number = 0;

        public Keys: string[] = [];

        public static RowID_Format: string="R{0:D4}";
        public static ColumnID_Format: string="C{0:D4}";

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
            this.Manager.LoadLayoutFromHtml(element);
        }

        public AddRow(index:number=-1):Row
        {
            var me = this;

            var templaterow = this.Rows[0];
            var newrow = me.GetNewRow();
            var newelement = _Clone(templaterow.UIElement);
            this.SetRow(newrow, newelement);
            
            
            var newrowHeaderCell = newrow.Cells.AsLinq<Cell>().Last(i=> i.Type == CellType.Header);
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
            var ix = this.Rows.indexOf(row);

            row.Cells.forEach(function (cell, ix) {
                RemoveFrom(cell, me.Cells);
            });
            RemoveFrom(row, me.RowHeader.Cells);
            RemoveFrom(row, me.Rows);
            CallFunctionVariable(me.Manager.OnRowRemoved, [row]);
            return true;
        }
        private SetRow(row: Row, element: Element)
        {
            var headercells = _Select("th", element);
            var datacells = _Select("td", element);
            row.Cells = [];

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