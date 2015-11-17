var var_currentinstance = "currentinstance";
var var_currentinstancevalidationresults = "currentvalidationresults";
var var_tax_concepts = "tax_concepts";
var var_tax_facts = "tax_facts";
var var_tax_labels = "tax_labels";
var var_tax_validations = "tax_validations";
var var_tax_hierarchies = "tax_hierarchies";

var s_list_selector: string = ".list";
var s_listpager_selector: string = ".listpager";
var s_listfilter_selector: string = ".listfilter";
var s_sublist_selector: string = ".sublist";
var s_sublistpager_selector: string = ".sublistpager";
var s_detail_selector: string = ".detail";
var s_parent_selector: string = ".parent";
var s_contentcontainer_selector: string = ".contentcontainer";
var s_content_selector: string = ".subcontent";

var StopProgress: F_Progress = function (id: string) { return null; };
var StartProgress: F_Progress = function (id: string) { return null; };
var ResultFormatter: F_ResultFormatter = function (rawdata) { return rawdata };


var requests: General.KeyValue[] = [];

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
        var rawrows = _Select("tr", element);
        var headerix = 0;
        var columncells: Element[] = [];
        var rowcells: Element[] = [];
        table.HeaderRowCount = _Select("thead tr", element).length;

        rawrows.forEach(function (rawrow, ix) {
            var rawdatacells = _Select("td", rawrow);
            var rawheadercells = _Select("th", rawrow);
            var isdynamic = _HasClass(rawrow, "dynamic");
            if (rawdatacells.length > 0) {

                if (columncells.length < 1) {
                    headerix = ix - 1;
                    var headerrow = rawrows[headerix];
                    columncells = _Select("th", headerrow);
                }
                var rowheadercell = rawheadercells[rawheadercells.length - 1];
                rowcells.push(rowheadercell);
                var row = new Controls.Row();
                row.HeaderCell = Controls.Cell.ConvertFrom(rowheadercell);
                row.UIElement = rawrow;
                rawdatacells.forEach(function (cell, ix) {
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
                    row.Cells.push(cellobj);
                    if (!isdynamic) {
                        table.Cells.push(cellobj);
                    }
                });
                if (isdynamic) {
                    me.TemplateRow = row;
                    _Hide(me.TemplateRow.UIElement);
                } else {
                    table.Rows.push(row);

                }
                if (rawheadercells.length > table.HeaderColCount) {
                    table.HeaderColCount = rawheadercells.length;
                }
            }
        });

        var rowheader: Controls.Row = new Controls.Row();
        rowheader.Cells = rowcells.AsLinq<Element>().Select(i=> Controls.Cell.ConvertFrom(i)).ToArray();

        var colheader: Controls.Column = new Controls.Column();
        colheader.Cells = columncells.AsLinq<Element>().Select(i=> Controls.Cell.ConvertFrom(i)).ToArray();

        table.RowHeader = colheader;
        table.ColumnHeader = rowheader;
        CallFunctionWithContext(me, me.OnLoaded, [table]);

    }

    public ManageRows(table: Controls.Table) {
        Notify("ManageRows");
        var me = this;

        if (!IsNull(me.TemplateRow)) {
            var rowsquery = table.Rows.AsLinq<Controls.Row>().Where(i=> _HasClass(i.UIElement, "dynamicdata"));

            var emptyrow = rowsquery.FirstOrDefault(i=> !i.HasData());
            if (IsNull(emptyrow)) {
                emptyrow = table.AddRow(-1);
                this.SetRowID("emptyrow", emptyrow);


            }
        }
        //me.SetDynamicRowIds(table);
    }

    public SetRowID(ID: string, row: Controls.Row) {
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

    private SetDynamicRowIds(table: Controls.Table) {
        ShowNotification("SetDynamicRowIds");
        var me = this;
        var fdyndata = (row: Controls.Row) => _HasClass(row.UIElement, "dynamicdata");
        var dynamicrows = table.Rows.AsLinq<Controls.Row>().Where(i=> fdyndata(i)).ToArray();
        dynamicrows.forEach(function (row, ix) {
            var rowid = row.RowID; //Format(me.RowID_Format, ix);
            this.SetRowID(rowid, row);

        });
    }
    public Clear(table: Controls.Table) {
        table.Cells.forEach(function (cell, ix) {
            Controls.Cell.Clear(cell);
            //_Attribute(cell.UIElement, "factstring", "");
        });
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

        this.CellEditorAssigner(row.UIElement);
        row.Cells.forEach(function (cell, ix) {
            _Attribute(cell.UIElement, "title", _Attribute(cell.UIElement, "factstring"));
        });
        _EnsureEventHandler(row.UIElement, "click", function (e) {
            _Focus(this);
            _RemoveClass(_Select("tr", _Parent(row.UIElement)), "selected");
            _AddClass(this, "selected");

        });
    }

    public AddEventHandlers() {
        var me = this;
        this.CellEditorAssigner = function (element) {
            AssignEditor(
                //_Select("td:not(.blocked)", element),
                _Select("td.data:not(.blocked)", element),
                GetXbrlCellEditor,
                me.OnCellChanged);

        };

        this.OnLoaded = function (table: Controls.Table) {
            me.CellEditorAssigner(table.UIElement);
            table.Rows.forEach(function (row, ix) {
                me.SetCellsOfRow(row);
            });
            _EnsureEventHandler(window, "keyup", function (e) {
                if (e.which == 46) {
                    var rowtodelete: Controls.Row = null;
                    table.Rows.forEach(function (row, ix) {
                        if (_HasClass(row.UIElement, "selected")) {
                            rowtodelete = row;
                        }
                    });
                    if (!IsNull(rowtodelete)) {
                        table.RemoveRow(rowtodelete);
                    }
                }
            });
        };

        this.OnRowAdded = function (row: Controls.Row) {
            var rowelement = row.UIElement;
            _AddClass(rowelement, "dynamicdata");
            _RemoveClass(rowelement, "dynamic");
            _Attribute(rowelement, "style", "");

            me.SetCellsOfRow(row);

            _Show(row.UIElement);

        };

        this.OnRowRemoved = function (row) {

        };

        this.OnLayoutChanged = function (table: Controls.Table) {
            var me = <UITableManager>this;
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
            this.ManageRows(table);
            //OnCellChanged(cell, value);
        };
    }
}

function GetXbrlCellEditor(target: Element) {
    var typeclass = "";

    var factitems = _Attribute(target, "factstring").split(",");
    var concept = "";
    if (factitems[0].indexOf("[") == -1) {
        concept = factitems[0];
    }
    if (concept.indexOf(":ei") > -1) {
        typeclass = "ei";
    }
    if (concept.indexOf(":bi") > -1) {
        typeclass = "bi";
    }
    if (concept.indexOf(":di") > -1) {
        typeclass = "di";
    }
    var editor: Editor = null;
    if (typeclass == "bi") {
        editor = new Editor('<select class="celleditor"><option>true</option><option>false</option></select>',
            (i: JQuery) => i.val(),
            (i: JQuery, val: any) => { i.val(val); });

    }
    if (typeclass == "ei") {
        editor = new Editor(Format('<select class="celleditor">{0}</select>', app.taxonomycontainer.GetConcepOptions(concept)),
            (i: JQuery) => i.val(),
            (i: JQuery, val: any) => { i.val(val); });

    }
    if (typeclass == "di") {
        editor = new Editor('<input type="text" class="celleditor datepicker" value="" />',
            (i: JQuery) => {
                return i.val();
            },
            (i: JQuery, val: any) => {
                i.datepicker({
                    dateFormat: "yy-mm-dd",
                    onSelect: function () {
                        editor.Save();
                    }
                });
                i.val(val);
            });
        editor.CustomTrigger = () => { };

    }
    if (typeclass == "") {
        editor = new Editor('<input type="text" class="celleditor " value="" />',
            (i: JQuery) => i.val(),
            (i: JQuery, val: any) => i.val(val));
    }
    return editor;
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
        var containerwidth = t_width - 2;// - (t_l_padding + t_r_padding);

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
    }
}

function CreateMsg(category: string): General.Message {
    var msg = new General.Message();
    msg.Category = category;
    return msg;
}

function CreateNotificationMsg(message: string): General.Message {
    var msg = CreateMsg("notification");
    msg.Data = message;
    return msg
}

function CreateAjaxMsg(): General.Message {
    var msg = CreateMsg("ajax");
    return msg
}

function CreateErrorMsg(errormessage: string): General.Message {
    var msg = CreateMsg("error");
    msg.Error = errormessage;
    return msg;
}

function ErrorHandler(errorMsg, url, lineNumber) {
    var errortext = 'UI Error: ' + errorMsg + ' Script: ' + url + ' Line: ' + lineNumber;
    ShowError(errortext);
    return true;
}

function ShowHideChild(selector: any, sender: any) {
    _Hide(_Select(selector));
    var item = _SelectFirst(selector, _Parent(sender));
    _Show(item);
}

function SetPivots() {

    $("#maintable").resizableColumns();

}

function Notify(message: string) {
    ShowNotification(message);
}

function ShowNotification(message: string) {
    var msg = CreateNotificationMsg(message);
    Communication_ToApp(msg);
}

function ShowError(message: string) {
    var msg = CreateErrorMsg(message);
    Communication_ToApp(msg);

}

function Communication_ToApp(message: General.Message) {
    var strdata = JSON.stringify(message);
    if ('Notify' in window.external) {
        window.external.Notify(strdata);
    } else {
        console.log(strdata);
    }
}

function Communication_Listener(data: string) {
    //Notify("Communication_Listener_Start");
    var message: General.Message = <General.Message>JSON.parse(data);
    data = "";

    //Notify("Communication_Listener Parsed");

    data = null;
    if (message.Category == "ajax") {
        asyncFunc(() => {
            //Notify("Calling AjaxResponse");
            AjaxResponse(message);
        });
        //clearobject(message);
        //AjaxResponse(message);
    }
    if (message.Category == "notfication") {

    }
    if (message.Category == "error") {

    }
    if (message.Category == "action") {
        if (message.Url.toLowerCase() == "instance") {
            app.instancecontainer.HandleAction(message);
        }
    }
    if (message.Category == "debug") {
        debugger;
    }
    //Notify("Communication_Listener_End");
}

//if (typeof console === "undefined") {

window.onerror = ErrorHandler;
//}