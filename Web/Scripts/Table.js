/// <reference path="Interfaces.ts" />
/// <reference path="Utils.ts" />
/// <reference path="JqueryUtils.ts" />
/// <reference path="Models.ts" />
/// <reference path="Linq.ts" />
/// <reference path="TableManager.ts" />
/// <reference path="Binding.ts" />
var UI;
(function (UI) {
    var Table = (function () {
        function Table() {
            this.Taxonomy = null;
            this.Cells = [];
            //public Extensions: Model.Hierarchy<Model.LayoutItem> = null;
            this.ExtensionsRoot = null;
            this.Extensions = null;
            this.FactMap = {};
            this.CurrentExtension = null;
            this.Instance = null;
            this.TemplateRow = null;
            this.TemplateCol = null;
            this.UITable = null;
            this.HtmlTemplate = "";
            this.HtmlTemplatePath = "";
            this.Current_CellID = "";
            this.Current_ExtensionCode = "";
            this.Current_ReportID = "";
            this.IsInstanceLoaded = false;
            this.changes = {};
        }
        Table.prototype.LoadTable = function (reportid) {
            var me = this;
            var fload = function (data) {
                var jsonobj = JSON.parse(data);
                me.HtmlTemplatePath = jsonobj["HtmlTemplatePath"];
                me.ExtensionsRoot = jsonobj["ExtensionsRoot"];
                ShowNotification("Getting Html");
                AjaxRequest(me.HtmlTemplatePath, "get", "text/html", null, function (data) {
                    me.SaveInstance();
                    _Html(_SelectFirst("#ReportContainer"), data);
                    me.Current_ReportID = reportid;
                    me.SetExternals();
                    me.Load();
                    me.GetData();
                }, function (error) {
                    console.log(error);
                });
            };
            AjaxRequest("Taxonomy/Table", "get", "text/html", { item: "factmap", reportid: reportid }, function (data) {
                fload(data);
            }, function (error) {
                console.log(error);
            });
        };
        Table.prototype.SetExternals = function () {
            var extensions = this.ExtensionsRoot.Children;
            var dynamiccells = this.Instance.DynamicReportCells[this.Current_ReportID];
            if (!IsNull(dynamiccells)) {
                if (!IsNull(dynamiccells.Extensions)) {
                    extensions = dynamiccells.Extensions.Children;
                }
            }
            this.Extensions = extensions.AsLinq().Select(function (i) { return i.Item; });
            this.CurrentExtension = this.ExtensionsRoot.Item;
        };
        Table.prototype.GetData = function () {
            var me = this;
            me.IsInstanceLoaded = false;
            //me.LoadToUI();
            me.SetNavigation();
        };
        Table.prototype.SetNavigation = function () {
            var me = this;
            if (IsNull(me.Current_ExtensionCode)) {
                var firstextension = this.Extensions.FirstOrDefault();
                if (!IsNull(firstextension)) {
                    this.Current_ExtensionCode = this.Extensions.FirstOrDefault().LabelCode;
                }
            }
            me.SetExtensionByCode(this.Current_ExtensionCode);
            me.LoadToUI();
            me.HighlightCell();
        };
        Table.prototype.LoadToUI = function () {
            var me = this;
            if (!this.IsInstanceLoaded) {
                this.LoadInstance(this.Instance);
                this.IsInstanceLoaded = true;
            }
        };
        Table.prototype.Load = function () {
            var me = this;
            me.UITable = Factories.GetTablewithManager();
            me.UITable.OnCellChanged = function (cell, value) {
                me.UITable.Manager.OnCellChanged();
            };
            me.UITable.LoadfromHtml(_SelectFirst("#ReportContainer > table.report"));
            this.LoadCellsFromHtml();
            var hash = window.location.hash;
        };
        Table.prototype.HighlightCell = function () {
            _RemoveClass(_Select(".highlight"), "highlight");
            var cellselector = this.Current_CellID.replace(/_/g, "\\|").toUpperCase();
            var cells = _Select("#" + cellselector);
            _AddClass(cells, "highlight");
            _Focus(cells);
        };
        Table.prototype.LoadCellsFromHtml = function () {
            var me = this;
            var s_ix = 1;
            me.Cells = [];
            var datacells = _Select(".report .data");
            datacells.forEach(function (cellelement, index) {
                var layoutid = _Attribute(cellelement, "id");
                var row = layoutid.split("|")[0];
                var col = layoutid.split("|")[1];
                var factstring = _Attribute(cellelement, "factstring");
                row = row.substring(1);
                col = col.substring(1);
                var cell = new Model.Cell();
                cell.IsBlocked = _HasClass(cellelement, "blocked");
                if (!cell.IsBlocked) {
                    cell.Row = row;
                    cell.Column = col;
                    cell.FactString = factstring;
                    me.Cells.push(cell);
                }
                _Attribute(cellelement, "title", layoutid + "\r\n" + _Attribute(cellelement, "factstring"));
            });
        };
        Table.prototype.LoadCells = function (cells) {
            this.Cells = cells;
        };
        Table.prototype.LoadExtension = function (li) {
            this.CurrentExtension = li;
            var extensionscell = _SelectFirst("#Extension");
            var typeddimensionsofext = this.CurrentExtension.Dimensions.AsLinq().Where(function (i) { return i.IsTyped; }).ToArray();
            this.Cells.forEach(function (cell, ix) {
                //var 
                //cell.FactString
            });
            _Html(extensionscell, Format("{0} <br /> {1}", li.LabelCode, li.LabelContent));
            _Attribute(extensionscell, "title", li.FactString);
        };
        Table.prototype.LoadInstance = function (instance) {
            var me = this;
            me.SaveInstance();
            ShowNotification("Loading Instance to UI");
            if (IsNull(me.Instance)) {
                me.Instance = instance;
                if (IsNull(me.Instance.FactDictionary)) {
                    me.Instance.FactDictionary = {};
                }
            }
            me.UITable.Manager.Clear(me.UITable);
            me.SetDynamicRows();
            var c = 0;
            var cells = this.UITable.Cells; //this.Cells;
            cells.forEach(function (cell, index) {
                if (!_HasClass(cell, "blocked")) {
                    var cellelement = cell.UIElement;
                    var cell_layoutid = _Attribute(cellelement, "id");
                    var cell_factstring = _Attribute(cellelement, "factstring");
                    var cellfb = new Model.FactBase();
                    cellfb.FactString = cell_factstring; //  cell.FactString;
                    var factstring = cell_factstring;
                    Model.FactBase.LoadFromFactString(cellfb);
                    Model.FactBase.LoadFromFactString(me.CurrentExtension);
                    Model.FactBase.Merge(cellfb, me.CurrentExtension);
                    factstring = cellfb.GetFactString();
                    if (!IsNull(factstring)) {
                        var fact = Model.Instance.GetFactFor(me.Instance, cellfb, cell_layoutid);
                        if (!IsNull(fact)) {
                            _Html(cellelement, fact.Value);
                            c++;
                        }
                    }
                }
            });
            ShowNotification(Format("{0} cells were populated!", c));
        };
        Table.prototype.SaveInstance = function () {
            var me = this;
            if (me.UITable == null) {
                return null;
            }
            var instance = me.Instance;
            ShowNotification("Saving Instance to UI");
            var c = 0;
            var cells = me.UITable.Cells; //this.Cells;
            var facts = [];
            cells.forEach(function (cell, index) {
                if (!_HasClass(cell, "blocked") && !IsNull(cell.ColID) && !IsNull(cell.RowID)) {
                    var row = me.UITable.GetRowOfCell(cell);
                    var col = me.UITable.GetColOfCell(cell);
                    var celluielement = cell.UIElement;
                    var value = _Text(celluielement);
                    var dynamicfact = new Model.InstanceFact();
                    if (_HasClass(row.UIElement, "dynamicdata")) {
                        var keys = "";
                        row.Cells.forEach(function (c) {
                            if (_HasClass(c.UIElement, "key")) {
                                dynamicfact.FactString += _Attribute(c.UIElement, "factstring");
                                dynamicfact.FactString += _Value(c.UIElement);
                            }
                        });
                    }
                    if (_HasClass(col.UIElement, "dynamicdata")) {
                        var keys = "";
                        col.Cells.forEach(function (c) {
                            if (_HasClass(c.UIElement, "key")) {
                                dynamicfact.FactString += _Attribute(c.UIElement, "factstring");
                                dynamicfact.FactString += _Value(c.UIElement);
                            }
                        });
                    }
                    var cellfact = new Model.InstanceFact();
                    cellfact.FactString = _Attribute(cell.UIElement, "factstring");
                    Model.FactBase.Merge(cellfact, dynamicfact, true);
                    Model.FactBase.Merge(cellfact, me.CurrentExtension, true);
                    cellfact.Value = value;
                    Model.Instance.SaveFact(me.Instance, cellfact);
                }
            });
        };
        Table.prototype.SetDynamicRows = function () {
            var me = this;
            var cellobj = me.Cells[0];
            var url = window.location.pathname;
            var reportid = me.Current_ReportID;
            var extensioncode = IsNull(me.Current_ExtensionCode) ? this.ExtensionsRoot.Item.LabelCode : me.Current_ExtensionCode;
            var tbody = _SelectFirst("tbody", me.UITable.UIElement);
            var dynamicdatacontainer = me.Instance.DynamicReportCells[reportid];
            var rows = IsNull(dynamicdatacontainer) ? [] : GetProperties(dynamicdatacontainer.RowDictionary);
            var cols = IsNull(dynamicdatacontainer) ? [] : GetProperties(dynamicdatacontainer.ColDictionary);
            var exts = IsNull(dynamicdatacontainer) ? [] : GetProperties(dynamicdatacontainer.ExtDictionary);
            var templaterow = me.UITable.Manager.TemplateRow;
            var templatecol = me.UITable.Manager.TemplateColumn;
            if (!IsNull(templaterow)) {
                me.UITable.Manager.ClearDynamicItems(me.UITable);
                var templatefacts = [];
                //s
                me.UITable.CanManageRows = false;
                templaterow.Cells.forEach(function (cell, ix) {
                    var factstring = _Attribute(cell.UIElement, "factstring");
                    var fact = Model.FactBase.GetFactFromString(factstring);
                    templatefacts.push(fact);
                });
                rows.forEach(function (rowitem) {
                    var row = me.UITable.AddRow(-1, rowitem.Value);
                    me.SetDataCells(row, rowitem, templatefacts);
                });
                //me.UITable.CanManageRows = true;
                me.UITable.Manager.ManageRows(me.UITable);
            }
            if (!IsNull(templatecol)) {
                var templatefacts = [];
                //s
                me.UITable.CanManageColumns = false;
                templatecol.Cells.forEach(function (cell, ix) {
                    var factstring = _Attribute(cell.UIElement, "factstring");
                    var fact = Model.FactBase.GetFactFromString(factstring);
                    templatefacts.push(fact);
                });
                cols.forEach(function (colitem) {
                    var col = me.UITable.AddColumn(-1, colitem.Value);
                    //me.SetCellIDs(row, null);
                    me.SetDataCells(col, colitem, templatefacts);
                });
                //me.UITable.CanManageColumns = true;
                me.UITable.Manager.ManageColumns(me.UITable);
            }
            if (exts.length > 0) {
                var extitem = Model.Hierarchy.FirstOrDefault(me.ExtensionsRoot, function (i) { return i.Item.LabelContent == me.CurrentExtension.LabelContent; });
                var extix = me.ExtensionsRoot.Children.indexOf(extitem);
                var extdictitem = exts[extix];
                me.CurrentExtension.FactString = extdictitem.Key;
            }
        };
        Table.prototype.SetDataCells = function (cellcontainer, ditem, templatefacts) {
            var me = this;
            var containerfact = new Model.FactBase();
            containerfact.FactString = ditem.Key;
            Model.FactBase.LoadFromFactString(containerfact);
            //Dynamic Attempt 6
            Model.FactBase.Merge(containerfact, me.CurrentExtension);
            var containerfactdimensionsquery = containerfact.Dimensions.AsLinq();
            var containerfactdict = {};
            _Attribute(cellcontainer.UIElement, "factstring", ditem.Key);
            //var cells = _Select("td", row.UIElement);
            cellcontainer.Cells.forEach(function (cell, ix) {
                var cellelement = cell.UIElement;
                var templatefact = templatefacts[ix];
                if (templatefact.Concept == null && templatefact.Dimensions.length == 1) {
                    var celldimension = templatefact.Dimensions[0];
                    var dim = containerfactdimensionsquery.FirstOrDefault(function (i) { return i.DimensionItem == celldimension.DimensionItem && i.Domain == celldimension.Domain; });
                    if (dim != null) {
                        var text = dim.DomainMember;
                        _Html(cellelement, text);
                    }
                }
                else {
                    var fact = new Model.FactBase();
                    Model.FactBase.Merge(fact, templatefact, true);
                    Model.FactBase.Merge(fact, containerfact, true);
                    var fs = fact.GetFactString();
                    _Attribute(cellelement, "factstring", fs);
                }
            });
        };
        Table.prototype.GetDynamicRowID = function (cellid, fact) {
            var me = this;
            var newrowneeded = false;
            var rownum = 1;
            var row = null;
            var factkey = "";
            if (fact != null) {
                factkey = fact.GetFactString();
                var rows = _Select("tr", _Parent(me.TemplateRow));
                row = _SelectFirst("tr[factstring='" + factkey + "']");
            }
            if (!IsNull(row)) {
                return _Attribute(row, "id");
            }
            return "";
        };
        Table.prototype.SetExtensionByCode = function (code) {
            if (this.Extensions.Count() > 0) {
                var ext = this.Extensions.FirstOrDefault();
                if (!IsNull(code)) {
                    ext = this.Extensions.FirstOrDefault(function (i) { return i.LabelCode == code; });
                }
                if (!IsNull(ext)) {
                    this.LoadExtension(ext);
                }
            }
        };
        Table.prototype.SetCells = function (item) {
            var cells = JSON.parse(item);
            this.LoadCells(cells);
        };
        Table.prototype.HashChanged = function () {
            ShowNotification("Navigation occured: " + window.location.hash);
            var me = this;
            var hash = window.location.hash;
            if (hash.length > 0) {
                if (hash[hash.length - 1] != ";") {
                    hash = hash + ";";
                }
                var reportid = TextBetween(hash, "report=", ";");
                var cellid = TextBetween(hash, "cell=", ";");
                var extcode = TextBetween(hash, "ext=", ";");
                if (!IsNull(reportid)) {
                    if (extcode == "000") {
                        extcode = "000";
                    }
                    me.Current_CellID = cellid;
                    if (me.Current_ExtensionCode != extcode) {
                        me.IsInstanceLoaded = false;
                    }
                    me.Current_ExtensionCode = extcode;
                    if (me.Current_ReportID != reportid) {
                        me.Current_ReportID = reportid;
                        me.LoadTable(reportid);
                    }
                    else {
                        me.SetNavigation();
                    }
                }
            }
            var currentextensioncode = this.Current_ExtensionCode;
        };
        return Table;
    })();
    UI.Table = Table;
})(UI || (UI = {}));
function SetExtension(extjson) {
}
//# sourceMappingURL=Table.js.map