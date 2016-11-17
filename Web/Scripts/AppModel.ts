
class UITableManager implements Controls.ITableManager {
    public RowID_Format: string = "R{0:D4}";
    public ColumnID_Format: string = "C{0:D4}";
    public ExtensionID_Format: string = "Z{0:D4}";
    TemplateRow: Controls.Row = null;
    TemplateColumn: Controls.Column = null;

    constructor() {

        this.AddEventHandlers();
    }

    public LoadToUI(data: Object) {

    }
    public LoadPage(page: number, asyncdatagetter: Function, callback: Function) {

    }

    LoadLayoutFromData(data: any, table: Controls.Table) {
    }

    LoadLayoutFromHtml(element: Element, table: Controls.Table) {
        var me = this;
        var datarows = _Select("tbody>tr", element);
        var extensioncell = _SelectFirst("thead #Extension", element);
        var headerrowelements = _Select("thead>tr", element);
        var headerix = 0;
        var columncells: Element[] = [];
        var rowcells: Element[] = [];
        table.HeaderRowCount = _Select("thead tr", element).length;
        var rowheader: Controls.Row = new Controls.Row();
        var colheader: Controls.Column = new Controls.Column();

        headerrowelements.forEach(function (headerrowelement, ix) {
            var cellelements = _Select("th", headerrowelement);
            var row = new Controls.Row();
            Controls.Row.SetHeaderRowFromElement(row, headerrowelement);
            table.HeaderRows.push(row);
        });

        var lastheaderrow = <Controls.Row>LastFrom(table.HeaderRows);
        if (!IsNull(lastheaderrow))
        {
            rowheader.UIElement = lastheaderrow.UIElement;
            columncells = _Select("th", rowheader.UIElement);

            columncells.forEach(function (columnelement, ix) {
                //var t_columnelement = _Clone(columnelement);
                var t_columnelement = columnelement;
                var headercell = Controls.Cell.ConvertFrom(t_columnelement, Controls.CellType.Header);
                var colid = _Html(t_columnelement).trim()
                headercell.ColID = colid;
                var column = new Controls.Column();
                column.HeaderCell = headercell;
                column.ID = colid;
                column.UIElement = t_columnelement;
                //table.Columns.push(column);
                var isdynamic = _HasClass(t_columnelement, "dynamic");
                if (isdynamic) {
                    me.TemplateColumn = column;
                }
                else
                {
                    table.Columns.push(column);
                }


            });
        }
        datarows.forEach(function (datarow, ix) {
            var rawdatacells: Element[] = [];
            var rawheadercells: Element[] = [];
            var isdynamic = _HasClass(datarow, "dynamic");
            if (isdynamic) {
                var trow = _Clone(datarow);
                _Remove(datarow);
                datarow = trow;
            }
            rawdatacells = _Select("td", datarow);
            rawheadercells = _Select("th", datarow);

            var dynamiccol_ix = -1;


            var rowheadercell = rawheadercells[rawheadercells.length - 1];
            rowcells.push(rowheadercell);
            var row = new Controls.Row();
            row.HeaderCell = Controls.Cell.ConvertFrom(rowheadercell, Controls.CellType.Header);
            row.UIElement = datarow;
            row.ID = _Html(row.HeaderCell.UIElement).trim();
            rawdatacells.forEach(function (cell, ix) {
                var column = table.Columns[ix];

                var rowcode = _Html(rowheadercell).trim();

                var colcell = columncells[ix]; //rowcells[ix];
                var colcode = _Html(colcell).trim();

                var cellid = Format("{0}|{1}", rowcode, colcode);

                var cellobj = new Controls.Cell();
                cellobj.Type = Controls.CellType.Data;
                cellobj.RowID = rowcode;
                cellobj.ColID = colcode;
                cellobj.Value = _Html(cell).trim();
                cellobj.UIElement = cell;

                //add the cell to the column
                if (IsNull(column)) {
                    if (!IsNull(me.TemplateColumn)) {
                        me.TemplateColumn.Cells.push(cellobj);
                    }
                } else {
                    column.Cells.push(cellobj);
                }
                //add the cell to the row
                row.Cells.push(cellobj);
                if (!isdynamic) {
                    table.Cells.push(cellobj);
                }
            });
            if (isdynamic) {
                me.TemplateRow = row;
                Controls.Row.ClearDataCells(me.TemplateRow);
                //_Remove(me.TemplateRow.UIElement);
            } else {
                table.Rows.push(row);

            }
            if (rawheadercells.length > table.HeaderColCount) {
                table.HeaderColCount = rawheadercells.length;
            }
            if (!IsNull(me.TemplateColumn)) {
                for (var i = 0; i < me.TemplateColumn.Cells.length; i++) {
                    var t_cell = me.TemplateColumn.Cells[i];
                    var t_uielement = _Clone(t_cell.UIElement);
                    _Remove(t_cell.UIElement);
                    t_cell.UIElement = t_uielement;
                }
                var t_coluielement = _Clone(me.TemplateColumn.UIElement);
                _Remove(me.TemplateColumn.UIElement);
                me.TemplateColumn.UIElement = t_coluielement;
            }
            //}
        });

        rowheader.Cells = rowcells.AsLinq<Element>().Select(i=> Controls.Cell.ConvertFrom(i, Controls.CellType.Header)).ToArray();

        colheader.Cells = columncells.AsLinq<Element>().Select(i=> Controls.Cell.ConvertFrom(i, Controls.CellType.Header)).ToArray();

        table.RowHeader = colheader;
        table.ColumnHeader = rowheader;

        table.SetSheet(extensioncell);
        CallFunctionWithContext(me, me.OnLoaded, [table]);

    }

    public ManageRows(table: Controls.Table) {
        //Notify("ManageRows");
        var me = this;
        table.CanManageRows = true;
        if (!IsNull(me.TemplateRow)) {
            var rowsquery = table.Rows.AsLinq<Controls.Row>().Where(i=> _HasClass(i.UIElement, "dynamicdata"));

            var emptyrow = rowsquery.FirstOrDefault(i=> !i.HasData());
            if (IsNull(emptyrow)) {
                emptyrow = table.AddRow(-1);
                this.SetRowID("emptyrow", emptyrow);


            }
        }

    }

    public ManageColumns(table: Controls.Table) {
        //Notify("ManageColumns");
        var me = this;
        table.CanManageColumns = true;

        if (!IsNull(me.TemplateColumn)) {
            var columnsquery = table.Columns.AsLinq<Controls.Column>().Where(i=> _HasClass(i.UIElement, "dynamicdata"));

            var emptycol = columnsquery.FirstOrDefault(i=> !i.HasData());
            if (IsNull(emptycol)) {
                emptycol = table.AddColumn(-1);
                this.SetColID("emptycolumn", emptycol);


            }
        }
        //me.SetDynamicRowIds(table);
    }

    public SetRowID(ID: string, row: Controls.Row) {
        row.ID = ID;
        var headercell = row.HeaderCell;
        var existingrowid = _Html(headercell.UIElement).trim();
        _Html(headercell.UIElement, row.ID);
        _Attribute(row.UIElement, "id", row.ID);
        row.Cells.forEach(function (cell, ix) {
            var cellid = _Attribute(cell.UIElement, "id").trim();
            if (cellid.indexOf("|") == 0) {
                cellid = row.ID + cellid;
                _Attribute(cell.UIElement, "id", cellid);
            }
        });
    }
    public SetColID(ID: string, col: Controls.Column) {
        col.ID = ID;
        var headercell = col.HeaderCell;
        var existingrowid = _Html(headercell.UIElement).trim();
        _Html(headercell.UIElement, col.ID);
        _Attribute(col.UIElement, "id", col.ID);
        col.Cells.forEach(function (cell, ix) {
            var cellid = _Attribute(cell.UIElement, "id").trim();
            if (cellid.indexOf("|") == 0) {
                cellid = cellid + col.ID;
                _Attribute(cell.UIElement, "id", cellid);
            }
        });
    }

    private SetDynamicRowIds(table: Controls.Table) {
        ShowNotification("SetDynamicRowIds");
        var me = this;
        var fdyndata = (row: Controls.Row) => _HasClass(row.UIElement, "dynamicdata");
        var dynamicrows = table.Rows.AsLinq<Controls.Row>().Where(i=> fdyndata(i)).ToArray();
        dynamicrows.forEach(function (row, ix) {
            var rowid = row.ID; //Format(me.RowID_Format, ix);
            this.SetRowID(rowid, row);

        });
    }
    public Clear(table: Controls.Table) {
        var me = this;
        table.Cells.forEach(function (cell, ix) {
            Controls.Cell.Clear(cell);
            //_Attribute(cell.UIElement, "factstring", "");
        });


    }
    public ClearDynamicItems(table: Controls.Table) {
        var me = this;
        table.Cells.forEach(function (cell, ix) {
            Controls.Cell.Clear(cell);
            //_Attribute(cell.UIElement, "factstring", "");
        });
        var rowquery = table.Rows.AsLinq<Controls.Row>();
        var colquery = table.Columns.AsLinq<Controls.Column>();

        var staticrows = rowquery.Where(i=> !_HasClass(i.UIElement, "dynamicdata")).ToArray();
        var staticcols = colquery.Where(i=> !_HasClass(i.UIElement, "dynamicdata")).ToArray();

        var dynamicrows = rowquery.Where(i=> _HasClass(i.UIElement, "dynamicdata")).ToArray();
        var dynamiccols = colquery.Where(i=> _HasClass(i.UIElement, "dynamicdata")).ToArray();

        var emptyrow = rowquery.FirstOrDefault(i=> !i.HasData());
        var emptycol = colquery.FirstOrDefault(i=> !i.HasData());

        //_Html(table.BodyUIElement, "");
        //staticrows.forEach(function (row) {

        //});
        table.CanManageRows = false;
        dynamicrows.forEach(function (row) {
            table.RemoveRow(row);
        });
        //me.ManageRows(table);

        table.CanManageColumns = false;
        dynamiccols.forEach(function (col) {
            table.RemoveColumn(col);

        });
        //me.ManageColumns(table);



    }
    public Validate(): boolean {
        return true;
    }

    public Save(): boolean {
        return true;
    }

    public EditCell(cell: Controls.Cell) {
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
    public OnColumnRemoved: Function = null;
    public OnLayoutChanged: Function = null;

    private SetCellsOfRow(row: Controls.Row) {
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
    }
    private SetCellsOfColumn(col: Controls.Column) {
        var me = this;
        var table = app.taxonomycontainer.Table.UITable; 
        /*
        _EnsureEventHandler(col.UIElement, "click", function (e) {
            _Focus(this);

        });
        */
    }

    public AddEventHandlers() {
        var me = <UITableManager>this;
        this.CellEditorAssigner = function (element) {
            AssignEditor(
                () => _Select("td.data:not(.blocked)", element),
                GetXbrlCellEditor,
                me.OnCellChanged);

        };

        this.OnLoaded = function (table: Controls.Table) {
            var me = <UITableManager>this;
            //me.CellEditorAssigner(table.UIElement);
            table.Rows.forEach(function (row, ix) {
                me.SetCellsOfRow(row);
            });
            _EnsureEventHandler(table.UIElement, "click", function (e) {
                var element = <Element>e.target;
                if (element instanceof HTMLTableDataCellElement) {
                    me.OnCellSelected(element);
                    if (!_HasClass(element, "blocked")) {
                        EditCell(element, GetXbrlCellEditor, me.OnCellChanged)
                    }
                }
            });
            _RemoveEventHandler(window, "keyup", table.DeleteFunction)
            _AddEventHandler(window, "keyup", table.DeleteFunction);
            //table.Rows.forEach(function (row, ix) {
            //    _EnsureEventHandler(row.UIElement, "click", me.OnRowSelected);
            //});

 
            //table.Columns.forEach(function (column, ix) {
            //    _EnsureEventHandler(column.UIElement, "click", me.OnColumnSelected);
            //});
        };

        this.OnCellSelected = function (element: Element) {
            var cellid = _Attribute(element, "id");
            var cell = app.taxonomycontainer.Table.UITable.GetCellByID(cellid);
            if (cell != null) {
                var cellfs = _Attribute(cell.UIElement, "factstring");
                var mcell = new Model.Cell;
                //var cellfact = new Model.FactBase;
                //cellfact.FactString = cellfs + app.taxonomycontainer.Table.CurrentExtension.FactString;
                //Model.FactBase.LoadFromFactString(cellfact);

                mcell.Extension = app.taxonomycontainer.Table.Current_ExtensionCode;
                mcell.Report = app.taxonomycontainer.Table.Current_ReportID;
                mcell.Row = cell.RowID;
                mcell.Column = cell.ColID;
                mcell.FactString = app.taxonomycontainer.Table.TaxonomyService.GetFactStringKey(cell.CurrentFactKey);
                mcell.FactString = mcell.FactString.replace(",",",<br/>");
                mcell.CellID = _Attribute(cell.UIElement, "id");
                BindX($("#CellInfo"), mcell);
                _Show(_SelectFirst("#CellInfo"));
            }
        };

        this.OnRowAdded = function (row: Controls.Row) {
            var element = row.UIElement;
            _AddClass(element, "dynamicdata");
            _RemoveClass(element, "dynamic");
            //_Attribute(rowelement, "style", "");

            me.SetCellsOfRow(row);

            _Show(row.UIElement);

        };

        this.OnColumnAdded = function (col: Controls.Column) {
            var element = col.UIElement;
            _AddClass(element, "dynamicdata");
            _RemoveClass(element, "dynamic");

        };

        this.OnRowRemoved = function (row) {

        };

        this.OnLayoutChanged = function (table: Controls.Table) {
            var me = <UITableManager>this;
            if (table.CanManageRows) {
                me.ManageRows(table);
            }
            if (table.CanManageColumns) {
                me.ManageColumns(table);
            }
        };

        this.OnCellChanged = function (cell, value) {
            var table = app.taxonomycontainer.Table.UITable;
            var row = table.GetRowOfCell(cell);
            var col = table.GetColOfCell(cell);
            if (!IsNull(row) && _HasClass(row.UIElement, "dynamicdata")) {
                if (!row.HasData()) {
                    table.RemoveRow(row);
                }
                table.Manager.ManageRows(table);
            }
            if (!IsNull(col) && _HasClass(col.UIElement, "dynamicdata")) {
                if (!col.HasData()) {
                    table.RemoveColumn(col);
                }
                table.Manager.ManageColumns(table);
            }
            //OnCellChanged(cell, value);
        };

        this.OnRowSelected = function (row: Controls.Row) {
            _RemoveClass(_Select("table.report tbody tr"), selectedclass);
            _AddClass(row.UIElement, selectedclass);
        };
    }
}

class ProgressManager {
    public id_progress: String = "";

    public Progresses: String[] = [];

    constructor(progress_id: String) {
        this.id_progress = progress_id;
    }
    public StartProgress(id: string) {
        //Log(Format("StartProgress: {0}", id));
        this.Progresses.push(id);
        if (this.Progresses.length == 1) {
            $(this.id_progress).show()
        }
    }
    public StopProgress(id: string) {
        //Log(Format("StopProgress: {0}", id));

        var ix = 0;
        this.Progresses.forEach(
            function (item, i) {
                if (item == id) {
                    ix = i;
                }
            }
            );
        this.Progresses.splice(ix, 1);
        if (this.Progresses.length == 0) {
            $(this.id_progress).hide()
        }
    }
}

class Editor {
    public HtmlFormat: string = "";
    public ValueGetter: Function = null;
    public ValueSetter: Function = null;
    public TargetValueGetter: Function = null;
    public TargetValueSetter: Function = null;
    public CustomTrigger: Function = null;
    public $Target: JQuery = null;
    public $Me: JQuery = null;
    public Current_Value: string;
    public Original_Value: string;

    static editclass: string = "editing";

    constructor(HtmlFormat: string, ValueGetter: Function, ValueSetter: Function) {
        this.HtmlFormat = HtmlFormat;
        this.ValueGetter = ValueGetter;
        this.ValueSetter = ValueSetter;
    }

    public Save() {
        var new_value = this.ValueGetter(this.$Me);
        this.$Target.removeClass(Editor.editclass);
        this.$Me.remove();
        if (this.Original_Value != new_value) {
            this.TargetValueSetter(new_value)
        } else {
            _Html(this.$Target[0], this.Original_Value);
        }

    }

    public Load(TargetElement: Element, TargetValueGetter: Function, TargetValueSetter: Function) {
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
        //Log(Format("w: {0} h: {1} rpad: {2} lpad: {3}", t_width, t_height, t_r_padding, t_l_padding));
        var containerwidth = t_width - 2 - (t_r_padding + t_l_padding);// - (t_l_padding + t_r_padding);

        var containerheight:number = t_height - (t_t_padding + t_b_padding);
        var containerfontfamily = Target.css('font-family');
        var containerfontsize = Target.css('font-size');
        var containerlineheight = Target.css('line-height');
        var containerbackgroundcolor = Target.parent().css('background-color');

        this.$Me.width(containerwidth);
        //this.$Me.css("width", "100%");
        if (containerheight >0) {
            this.$Me.css("height",containerheight);
        }
        this.$Me.css('margin', "0px");

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
                if (!window["keepcelleditoropen"]) {
                    me.Save();
                }
                return true;
            });
        }
    }
}

module Engine {

    export class ActionCenter {
        private Selector: any = null;
        private CurrentSelector: any = null;
        private ListSelector: any = null;
        private ActionBarSelector: any = null;
        private class_Error: string = "n-error";
        private class_Warning: string = "n-warning";
        private class_Info: string = "n-info";
        private class_Success: string = "n-success";
        private format_Notification: string = "<div class=\"notification {1}\">{0}</div>";

        public SetSelectors(selector: any, currentselector: any, listselector: any, actionbarselector: any) {
            this.Selector = selector;
            this.CurrentSelector = currentselector;
            this.ListSelector = listselector;
            this.ActionBarSelector = actionbarselector;
        }

        public AddSuccess(content: string) {
            this.AddNotification(content, this.class_Success);
        }
        public AddInfo(content: string) {
            this.AddNotification(content, this.class_Info);
        }
        public AddWarning(content: string) {
            this.AddNotification(content, this.class_Warning);
        }
        public AddError(content: string) {
            this.AddNotification(content, this.class_Error);
        }

        public AddNotification(content: string, cssclass?: string) {
            content = Format(this.format_Notification, content, cssclass);
            var lastmessage = $(this.CurrentSelector).html();
            $(this.CurrentSelector).html(content);
            $(this.ListSelector).prepend(lastmessage);
            $(this.Selector).show();
        }

        public ClearAll() {
            this.ClearCurrent();
            this.ClearList();
            $(this.Selector).hide();

        }

        public ClearCurrent() {
            $(this.CurrentSelector).html("");

        }
        public ClearList() {
            $(this.ListSelector).html("");

        }
        public ToggleListVisibility() {
            if ($(this.ListSelector).is(":visible")) {
                $(this.ListSelector).hide();
            }
            else {
                $(this.ListSelector).show();

            }
        }
    }

    export class UIManager {
        private duration: number = 200;
        private min_width: number = 150;

        private GetMaxWidth(): number {
            var maxwidth = $("#main-content").width();
            return maxwidth;
        }

        public ActivateList() {

            $("#ListController").parent().animate({ "max-width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });
            $("#SaveController").parent().animate({ "width": this.min_width + "px" }, { duration: this.duration, queue: false });
        }
        public ActivateSave() {
            $("#ListController").parent().animate({ "max-width": this.min_width + "px" }, { duration: this.duration, queue: false });
            $("#SaveController").parent().animate({ "width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });

        }
    }


}