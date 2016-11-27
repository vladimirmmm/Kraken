/// <reference path="Interfaces.ts" />
/// <reference path="Utils.ts" />
/// <reference path="JqueryUtils.ts" />
/// <reference path="Models.ts" />
/// <reference path="Linq.ts" />
/// <reference path="TableManager.ts" />
/// <reference path="Binding.ts" />
var UI;
(function (UI) {
    var CellMetaData = (function () {
        function CellMetaData() {
            this.InstanceFactKey = [];
            this.TaxonomyFactKey = [];
            this.Cell = null;
        }
        CellMetaData.prototype.SetFromControlCell = function (Cell) {
            this.Cell = Cell;
        };
        CellMetaData.prototype.IsBlocked = function () {
            return _HasClass(this.Cell.UIElement, "blocked");
        };
        CellMetaData.prototype.IsKey = function () {
            return _HasClass(this.Cell.UIElement, "key");
        };
        CellMetaData.prototype.FactString = function () {
            return _Attribute(this.Cell.UIElement, "factstring");
        };
        return CellMetaData;
    })();
    UI.CellMetaData = CellMetaData;
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
            this.TaxonomyService = null;
            this.templatefacts = [];
            this.templatedicts = [];
            this.changes = {};
        }
        Table.prototype.LoadTable = function (reportid) {
            var me = this;
            var fload = function (data) {
                var jsonobj = JSON.parse(data);
                me.HtmlTemplatePath = jsonobj["HtmlTemplatePath"];
                me.ExtensionsRoot = jsonobj["ExtensionsRoot"];
                Log("UI", "Getting Html");
                AjaxRequest(me.HtmlTemplatePath, "get", "text/html", null, function (data) {
                    me.SaveInstance();
                    _Html(_SelectFirst("#ReportContainer"), data);
                    me.Current_ReportID = reportid;
                    me.Load();
                    me.SetExternals();
                    me.GetData();
                    var table = Model.Hierarchy.FirstOrDefault(app.taxonomycontainer.TableStructure, function (i) {
                        var id = i.Item.ID;
                        id = id.indexOf("<") > -1 ? id.substr(0, id.indexOf("<")) : id;
                        //console.log(Format("{0}=={1}", id, reportid));
                        return id == reportid;
                    });
                    if (!IsNull(table) && !IsNull(table.Item)) {
                        _Html(_SelectFirst("#DetailTitle"), table.Item.Name);
                    }
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
            var me = this;
            var dynamiccells = this.Instance.DynamicReportCells[this.Current_ReportID];
            if (!IsNull(dynamiccells)) {
                if (!IsNull(dynamiccells.Extensions)) {
                    me.ExtensionsRoot.Children = dynamiccells.Extensions.Children;
                }
            }
            var extensions = me.ExtensionsRoot.Children;
            this.Extensions = extensions.AsLinq().Select(function (i) { return i.Item; });
            var current_extension = me.ExtensionsRoot.Item;
            if (this.ExtensionsRoot.Children.length > 0) {
                current_extension = me.ExtensionsRoot.Children[0].Item;
            }
            me.SetCellFactKeys();
            me.LoadExtension(current_extension);
            //this.CurrentExtension = current_extension;
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
        Table.prototype.LoadInstance = function (instance) {
            var me = this;
            me.SaveInstance();
            Log("UI", "Loading Instance to UI");
            if (IsNull(me.Instance)) {
                me.Instance = instance;
                if (IsNull(me.Instance.FactDictionary)) {
                    me.Instance.FactDictionary = new Model.InstanceFactDictionary();
                }
            }
            me.UITable.Manager.ClearDynamicItems(me.UITable);
            me.UITable.Manager.Clear(me.UITable);
            _Html(_SelectFirst(".tablepager"), "");
            me.SetExtensionByCode(me.Current_ExtensionCode);
            me.SetDynamicRows();
            Model.FactBase.LoadFromFactString(me.CurrentExtension);
            me.PopulateCells();
            var extensioncell = _SelectFirst(".report #Extension");
            var extensioneditorelement = _SelectFirst("#ExtensionEditor");
            var extensioneditorcontainerelement = _SelectFirst("#ExtensionEditor .editors");
            _AddEventHandler(extensioncell, "dblclick", function () {
                var html = me.GetExtensionEditor();
                _Html(extensioneditorcontainerelement, html);
                _Show(extensioneditorelement);
            });
        };
        Table.prototype.LoadExtension = function (li) {
            var me = this;
            //Log("UI","LoadExtension");
            this.CurrentExtension = li;
            var extensionscell = _SelectFirst("#Extension");
            var typeddimensionsofext = this.CurrentExtension.Dimensions.AsLinq().Where(function (i) { return i.IsTyped; }).ToArray();
            _Html(extensionscell, Format("{0} <br /> {1}", li.LabelCode, li.LabelContent));
            _Attribute(extensionscell, "title", li.FactString);
            _Attribute(extensionscell, "factkeys", li.FactKeys.join(","));
            var cells = this.UITable.Cells; //this.Cells;
            if (!IsNull(this.UITable.Manager.TemplateRow)) {
                cells = cells.concat(this.UITable.Manager.TemplateRow.Cells);
            }
            if (!IsNull(this.UITable.Manager.TemplateColumn)) {
                cells = cells.concat(this.UITable.Manager.TemplateColumn.Cells);
            }
            var extensionkeys = me.CurrentExtension.FactKeys;
            cells.forEach(function (cell, index) {
                if (!_HasClass(cell, "blocked")) {
                    var cellelement = cell.UIElement;
                    var cellkey = cell.GetAxisKeys(me.UITable);
                    //cellkey = me.TaxonomyService.MergeKeys(cellkey, extensionkeys, true);
                    cellkey = me.TaxonomyService.GetFixedKey(cellkey);
                    cell.CurrentFactKey = cellkey;
                }
            });
        };
        Table.prototype.PopulateCells = function () {
            var me = this;
            var c = 0;
            var cells = this.UITable.Cells; //this.Cells;
            cells.forEach(function (cell, index) {
                if (!_HasClass(cell, "blocked")) {
                    var fact = me.TaxonomyService.GetFactByKey(cell.CurrentFactKey);
                    if (!IsNull(fact)) {
                        _Html(cell.UIElement, fact.Value);
                        c++;
                    }
                }
            });
            Log("UI", Format("{0} cells were populated!", c));
        };
        Table.prototype.LoadRows = function (items, page, pagesize) {
            var me = this;
            var startix = pagesize * page;
            var endix = startix + pagesize;
            var datalength = GetLength(items);
            var itemspart = GetPart(items, startix, endix);
            me.UITable.Manager.ClearDynamicItems(me.UITable);
            itemspart.forEach(function (item) {
                var row = me.UITable.AddRow(-1, item.Value);
                me.SetDataCells2(row, item, me.templatefacts, me.templatedicts);
            });
            if (datalength > pagesize) {
                var $pager = $(".tablepager");
                if ($pager.length == 0 || 1 == 1) {
                    $pager.pagination(datalength, {
                        items_per_page: pagesize,
                        current_page: page ? page : 0,
                        link_to: "",
                        prev_text: "Prev",
                        next_text: "Next",
                        ellipse_text: "...",
                        prev_show_always: true,
                        next_show_always: true,
                        callback: function (pageix) {
                            //CallFunctionFrom(events, "onpaging");
                            me.LoadRows(items, pageix, pagesize);
                            me.PopulateCells();
                            //CallFunctionFrom(events, "onpaged");
                            return false;
                        },
                    });
                }
            }
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
            me.templatedicts = [];
            me.templatefacts = [];
            var templaterow = me.UITable.Manager.TemplateRow;
            var templatecol = me.UITable.Manager.TemplateColumn;
            if (!IsNull(templaterow)) {
                me.UITable.Manager.ClearDynamicItems(me.UITable);
                //s
                me.UITable.CanManageRows = false;
                templaterow.Cells.forEach(function (cell, ix) {
                    var fact = new Model.FactBase();
                    if (cell.IsKey()) {
                        var column = Controls.Cell.GetColumn(cell, me.UITable);
                        fact.FactKeys = Controls.Cell.GetKeysFromElement(column.HeaderCell.UIElement);
                    }
                    else {
                        fact.FactKeys = cell.GetAxisKeys(me.UITable);
                    }
                    fact.FactKeys = me.TaxonomyService.GetFixedKey(fact.FactKeys);
                    fact.FactKeys = fact.FactKeys.AsLinq().Distinct().ToArray();
                    var dict = {};
                    for (var i = 0; i < fact.FactKeys.length; i++) {
                        var key = fact.FactKeys[i];
                        if (me.TaxonomyService.IsTyped(key)) {
                            dict[key] = i;
                        }
                    }
                    me.templatedicts.push(dict);
                    me.templatefacts.push(fact);
                });
                //rows.forEach(function (rowitem,ix) {
                //    var row = me.UITable.AddRow(-1, rowitem.Value);
                //    me.SetDataCells2(row, rowitem, me.templatefacts, me.templatedicts);
                //});
                me.LoadRows(rows, 0, 100);
                //me.UITable.CanManageRows = true;
                me.UITable.Manager.ManageRows(me.UITable);
            }
            if (!IsNull(templatecol)) {
                //s
                me.UITable.CanManageColumns = false;
                templatecol.Cells.forEach(function (cell, ix) {
                    var fact = new Model.FactBase();
                    if (cell.IsKey()) {
                        var row = Controls.Cell.GetRow(cell, me.UITable);
                        fact.FactKeys = Controls.Cell.GetKeysFromElement(row.HeaderCell.UIElement);
                    }
                    else {
                        fact.FactKeys = cell.GetAxisKeys(me.UITable);
                    }
                    fact.FactKeys = me.TaxonomyService.GetFixedKey(fact.FactKeys);
                    fact.FactKeys = fact.FactKeys.AsLinq().Distinct().ToArray();
                    var dict = {};
                    for (var i = 0; i < fact.FactKeys.length; i++) {
                        var key = fact.FactKeys[i];
                        if (me.TaxonomyService.IsTyped(key)) {
                            dict[key] = i;
                        }
                    }
                    me.templatedicts.push(dict);
                    me.templatefacts.push(fact);
                });
                cols.forEach(function (colitem, ix) {
                    var col = me.UITable.AddColumn(-1, colitem.Value);
                    //me.SetCellIDs(row, null);
                    me.SetDataCells2(col, colitem, me.templatefacts, me.templatedicts);
                });
                //me.UITable.CanManageColumns = true;
                me.UITable.Manager.ManageColumns(me.UITable);
            }
        };
        Table.prototype.SetDataCells2 = function (cellcontainer, ditem, templatefacts, templatedicts) {
            var me = this;
            var containerfact = new Model.FactBase();
            _Attribute(cellcontainer.UIElement, "factstring", ditem.Key);
            var typedkeys = me.TaxonomyService.GetInstFactKey(ditem.Key);
            var typeddomains = [];
            cellcontainer.HeaderCell.SetFactKey(typedkeys);
            var typedomainkeys = {};
            typedkeys.forEach(function (key) {
                var domkey = me.TaxonomyService.GetInstanceDomain(key);
                var keystr = key.toString();
                var instdim = me.TaxonomyService.GetFactPartFromKey(keystr);
                var member = instdim.substr(instdim.lastIndexOf(":") + 1);
                typedomainkeys[domkey] = member;
                typeddomains.push(domkey);
            });
            //var cells = _Select("td", row.UIElement);
            cellcontainer.Cells.forEach(function (cell, ix) {
                var cellelement = cell.UIElement;
                var templatefact = templatefacts[ix];
                var dict = templatedicts[ix];
                var iskey = _HasClass(cellelement, "key");
                var cellfactkey = templatefact.FactKeys.slice();
                if (iskey) {
                    var celldimid = cellfactkey.join(",");
                    var text = typedomainkeys[celldimid];
                    _Html(cellelement, text);
                }
                else {
                    var factkey = [];
                    typedkeys.forEach(function (t, ix) {
                        var domkey = typeddomains[ix];
                        var tix = dict[domkey];
                        cellfactkey[tix] = t;
                    });
                    cell.CurrentFactKey = cellfactkey;
                }
            });
        };
        Table.prototype.SetCellFactKeys = function () {
            var me = this;
            me.UITable.Rows.forEach(function (row, rix) {
                var cell = row.HeaderCell;
                var factstring = cell.GetFactID();
                var key = me.TaxonomyService.GetTaxFactKey(factstring);
                cell.SetFactKey(key);
            });
            me.UITable.Columns.forEach(function (col, rix) {
                var cell = col.HeaderCell;
                var factstring = cell.GetFactID();
                var key = me.TaxonomyService.GetTaxFactKey(factstring);
                cell.SetFactKey(key);
            });
            if (!IsNull(me.UITable.Manager.TemplateRow)) {
                var cell = me.UITable.Manager.TemplateRow.HeaderCell;
                var factstring = cell.GetFactID();
                var key = me.TaxonomyService.GetTaxFactKey(factstring);
                cell.SetFactKey(key);
            }
            if (!IsNull(me.UITable.Manager.TemplateColumn)) {
                var cell = me.UITable.Manager.TemplateColumn.HeaderCell;
                var factstring = cell.GetFactID();
                var key = me.TaxonomyService.GetTaxFactKey(factstring);
                cell.SetFactKey(key);
            }
            me.Extensions.ForEach(function (ext, eix) {
                var item = ext;
                item.FactKeys = me.TaxonomyService.GetTaxFactKey(item.FactString);
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
            //Log("UI","SetExtensionByCode "+ code);
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
            //Log("UI","Navigation occured: " + window.location.hash);
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
        Table.prototype.GetExtensionEditor = function () {
            var me = this;
            var html = "";
            //return html;
            var extensiontemplate = me.CurrentExtension;
            extensiontemplate.Dimensions.forEach(function (dim) {
                var name = Model.Dimension.GetDomainFullName(dim);
                var label = Model.Dimension.DomainMemberFullName(dim);
                html += "<label for='" + name + "'>" + label + "</label>\n";
                if (IsNull(dim.DomainMember) || dim.IsTyped) {
                    var membervalue = IsNull(dim.DomainMember) ? "" : dim.DomainMember;
                    html += "<input type='text' fact='" + name + "' value='" + membervalue + "'/>\n";
                    if (dim.IsTyped) {
                    }
                    else {
                    }
                }
                html += "<br/>\n";
            });
            return html;
        };
        Table.prototype.SaveExtension = function () {
            var me = this;
            var extensiontemplate = me.CurrentExtension;
            extensiontemplate.Dimensions.forEach(function (dim) {
                var domainfullname = Model.Dimension.GetDomainFullName(dim);
                dim.DomainMember = _Value(_SelectFirst("input[fact='" + domainfullname + "']"));
            });
        };
        Table.prototype.SaveInstance = function () {
            return null;
            var me = this;
            if (me.UITable == null) {
                return null;
            }
            var instance = me.Instance;
            Log("UI", "Saving Instance to UI");
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
        return Table;
    })();
    UI.Table = Table;
})(UI || (UI = {}));
function SetExtension(extjson) {
}
//# sourceMappingURL=Table.js.map