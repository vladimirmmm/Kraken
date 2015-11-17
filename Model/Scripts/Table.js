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
        }
        Table.prototype.LoadTable = function (reportid) {
            var me = this;
            var fload = function (data) {
                var jsonobj = JSON.parse(data);
                me.HtmlTemplatePath = jsonobj["HtmlTemplatePath"];
                me.ExtensionsRoot = jsonobj["ExtensionsRoot"];
                ShowNotification("Getting Html");
                AjaxRequest(me.HtmlTemplatePath, "get", "text/html", null, function (data) {
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
            this.LoadInstance(this.Instance);
            /*
            if (me.Instance != null && !me.IsInstanceLoaded)
            {
                if (!IsNull(this.Instance)) {
                    this.LoadInstance(this.Instance);
                }
                me.IsInstanceLoaded = true;
            }
            */
        };
        Table.prototype.Load = function () {
            var me = this;
            me.UITable = Factories.GetTablewithManager();
            me.UITable.LoadfromHtml(_SelectFirst("#ReportContainer > table.report"));
            _EnsureEventHandler(_Select("table.report tr"), "click", function () {
                _RemoveClass(_Select("table.report tr"), "selected");
                _AddClass(this, "selected");
            });
            this.LoadCellsFromHtml();
            //this.SetCellEditors();
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
                    Model.FactBase.LoadFromFactString(cellfb);
                    Model.FactBase.Merge(cellfb, me.CurrentExtension);
                    var factstring = cellfb.GetFactString();
                    var factkey = cellfb.GetFactKey();
                    if (!IsNull(factstring)) {
                        var fact = Model.Instance.GetFactFor(me.Instance, cellfb, cell_layoutid);
                        if (!IsNull(fact)) {
                            var selector = "#" + cell_layoutid.replace("|", "\\|");
                            var cellelement = _SelectFirst(selector);
                            if (IsNull(cellelement)) {
                                ShowNotification(Format("No cell found with selector {0}", selector));
                            }
                            else {
                                _Html(cellelement, fact.Value);
                            }
                            c++;
                        }
                        if (!(factstring in me.Instance.FactDictionary)) {
                        }
                        else {
                            var facts = me.Instance.FactDictionary[factstring];
                            if (facts.length == 1 && facts[0].FactKey == facts[0].FactString) {
                                var fact = facts[0];
                                if (!IsNull(fact)) {
                                    var selector = "#" + cell_layoutid.replace("|", "\\|");
                                    var cellelement = _SelectFirst(selector);
                                    if (IsNull(cellelement)) {
                                        ShowNotification(Format("No cell found with selector {0}", selector));
                                    }
                                    else {
                                        _Html(cellelement, fact.Value);
                                    }
                                    c++;
                                }
                            }
                            else {
                                facts.forEach(function (factobj, index) {
                                    var fact = Model.InstanceFact.Convert(factobj);
                                    Model.FactBase.LoadFromFactString(fact);
                                    var typeddimensions = fact.Dimensions.AsLinq().Where(function (i) { return i.IsTyped; }).ToArray();
                                    var typedfacts = new Model.FactBase();
                                    typedfacts.Dimensions = typeddimensions;
                                    var rowid = me.GetDynamicRowID(cell_layoutid, typedfacts);
                                    var cellid = cell_layoutid;
                                    var r_ix = cellid.indexOf("R");
                                    if (r_ix > -1) {
                                        cellid = cellid.replace("R", rowid);
                                    }
                                    var selector = "#" + cellid.replace("|", "\\|");
                                    var cellelement = _SelectFirst(selector, _SelectFirst("#" + rowid));
                                    _Html(cellelement, fact.Value);
                                });
                            }
                        }
                    }
                }
            });
            ShowNotification(Format("{0} cells were populated!", c));
        };
        Table.prototype.SetRowID = function (row) {
        };
        Table.prototype.SetDynamicRows = function () {
            var me = this;
            //var datarows = _Select("tr.dynamicdata");
            //_Remove(datarows);
            var uirows = me.UITable.Rows.AsLinq().Where(function (i) { return _HasClass(i.UIElement, "dynamicdata"); }).Select(function (i) { return i.RowID; }).ToArray();
            me.UITable.CanManageRows = false;
            uirows.forEach(function (rowid, ix) {
                var count = uirows.length;
                //ShowNotification(Format("removing row {0} ...", rowid));
                me.UITable.RemoveRowByID(rowid);
            });
            me.UITable.CanManageRows = true;
            var cellobj = me.Cells[0];
            var url = window.location.pathname;
            var reportid = me.Current_ReportID;
            var extensioncode = IsNull(me.Current_ExtensionCode) ? this.ExtensionsRoot.Item.LabelCode : me.Current_ExtensionCode;
            //var reportkey = Format("{0}|{1}", reportid, extensioncode);
            //var rowidcontainer = me.Instance.DynamicCellDictionary[reportkey];
            var rowidcontainer = me.Instance.DynamicReportCells[reportid];
            var rows = GetProperties(rowidcontainer.RowDictionary);
            me.UITable.CanManageRows = false;
            rows.forEach(function (rowitem) {
                var row = me.UITable.AddRow(-1, rowitem.Value);
                //row.RowID = rowitem.Value;
                me.SetCellIDs(row);
                var rowfact = new Model.FactBase();
                rowfact.FactString = rowitem.Key;
                Model.FactBase.LoadFromFactString(rowfact);
                _Attribute(row.UIElement, "factstring", rowitem.Key);
                var cells = _Select("td", row.UIElement);
                cells.forEach(function (cellelement, index) {
                    var cellfactstring = _Attribute(cellelement, "factstring");
                    var cfs = Replace(cellfactstring.trim(), ",", "");
                    if (!IsNull(cfs)) {
                        var dim = rowfact.Dimensions.AsLinq().FirstOrDefault(function (i) { return Model.Dimension.DomainMemberFullName(i).indexOf(cfs) == 0; });
                        if (dim != null) {
                            var text = dim.DomainMember;
                            _Text(cellelement, text);
                        }
                        else {
                            var fact = Model.FactBase.GetFactFromString(cellfactstring);
                            Model.FactBase.Merge(fact, rowfact, true);
                            var fs = fact.GetFactString();
                            _Attribute(cellelement, "factstring", fs);
                        }
                    }
                });
            });
            me.UITable.CanManageRows = true;
            me.UITable.Manager.ManageRows(me.UITable);
            //me.AddRow("newrow", false, 0);
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
            //if (fact != null)
            //{
            //    _Attribute(row, "factstring", factkey);
            //   var cells = _Select("td", row);
            //   cells.forEach(function (cellelement, index) {
            //        var cellfactstring = _Attribute(cellelement,"factstring");
            //        cellfactstring = Replace(cellfactstring.trim(), ",", "");
            //        if (!IsNull(cellfactstring)) {
            //            var dim = fact.Dimensions.AsLinq<Model.Dimension>().FirstOrDefault(i=> i.DomainMemberFullName.indexOf(cellfactstring) == 0);
            //            if (dim != null) {
            //                var text = dim.DomainMember;
            //                _Text(cellelement,text);
            //            }
            //        }
            //    });
            //}
            return "";
        };
        Table.prototype.SetCellIDs = function (row) {
            var rowid = _Attribute(row.UIElement, "id");
            row.Cells.forEach(function (cell, index) {
                var cellid = _Attribute(cell.UIElement, "id");
                cellid = cellid.substring(cellid.indexOf("|"));
                cellid = rowid + cellid;
                _Attribute(cell.UIElement, "id", cellid);
                _Attribute(cell.UIElement, "title", cellid);
            });
        };
        Table.prototype.SetCellID = function (row) {
            var cells = _Select("td", row);
            var rowid = _Attribute(row, "id");
            cells.forEach(function (cellelement, index) {
                var cellid = _Attribute(cellelement, "id");
                cellid = cellid.substring(cellid.indexOf("|"));
                cellid = rowid + cellid;
                _Attribute(cellelement, "id", cellid);
                _Attribute(cellelement, "title", cellid);
            });
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
            var currentextensioncode = this.Current_ExtensionCode;
            //this.SetNavigation();
            //this.SetExtensionByCode(this.Current_ExtensionCode);  
            //this.HighlightCell();   
            //if (currentextensioncode != this.Current_ExtensionCode) {
            //    this.GetData();
            //}
        };
        return Table;
    })();
    UI.Table = Table;
})(UI || (UI = {}));
//var UITable: UI.Table = null;
function SetExtension(extjson) {
}
//function LoadInstance(instancejson:string) {
//    Table.LoadInstance(window[var_currentinstance]);
//} 
//# sourceMappingURL=Table.js.map