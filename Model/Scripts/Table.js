var UI;
(function (UI) {
    var Table = (function () {
        function Table() {
            this.Cells = [];
            this.Extensions = [];
            this.FactMap = {};
            this.CurrentExtension = null;
            this.JSONPath = "";
            this.Instance = null;
            this.CurrentCells = [];
            this.GetFactMap();
        }
        Table.prototype.LoadCellsFromHtml = function () {
            var me = this;
            var s_ix = 1;
            me.Cells = [];
            $(".data").each(function (index, item) {
                var layoutid = $(item).attr("layoutid");
                var factstring = $(item).attr("factstring");
                var factarray = factstring.split(",");
                var row = layoutid.split("|")[0];
                var col = layoutid.split("|")[1];
                row = row.substring(1);
                col = col.substring(1);
                var cell = new Model.Cell();
                cell.Row = row;
                cell.IsBlocked = $(item).hasClass("blocked");
                if (!cell.IsBlocked) {
                    cell.Column = col;
                    cell.Concept = Model.QualifiedName.Create(factarray[0]);
                    for (var i = 1; i < factarray.length; i++) {
                        if (!IsNull(factarray[i])) {
                            var dimparts = factarray[i].split(/[\[\]:]+/);
                            var dimension = new Model.Dimension();
                            dimension.DimensionItem = dimparts.length > s_ix ? dimparts[s_ix] + ":" + dimparts[s_ix + 1] : "";
                            dimension.Domain = dimparts.length > s_ix + 2 ? dimparts[s_ix + 2] : "";
                            dimension.DomainMember = dimparts.length > s_ix + 3 ? dimparts[s_ix + 3] : "";
                            cell.Dimensions.push(dimension);
                        }
                    }
                    me.Cells.push(cell);
                }
            });
        };
        Table.prototype.LoadCells = function (cells) {
            this.Cells = cells;
        };
        Table.prototype.GetFactMap = function () {
            this.FactMap = window["FactMap"];
        };
        Table.prototype.LoadExtension = function (li) {
            this.CurrentExtension = li;
            //this.SetCurrentCells();
        };
        Table.prototype.LoadInstance = function (instance) {
            var me = this;
            //this.SetCurrentCells();
            if (IsNull(me.Instance)) {
                instance.FactDictionary = {};
                instance.Facts.forEach(function (fact, index) {
                    instance.FactDictionary[fact.FactString] = fact;
                });
                me.Instance = instance;
            }
            if (!IsNull(me.FactMap)) {
                var c = 0;
                var extfacts = me.FactMap[me.CurrentExtension.LabelCode];
                if (extfacts != null) {
                    this.Cells.forEach(function (cell, index) {
                        if (!cell.IsBlocked) {
                            if (!(cell.FactString in me.Instance.FactDictionary)) {
                            }
                            else {
                                var value = me.Instance.FactDictionary[extfacts[cell.LayoutID]];
                                if (!IsNull(value)) {
                                    var selector = "[layoutid=\"" + cell.LayoutID + "\"]";
                                    if ($(selector).length == 0) {
                                        Notify(Format("No cell found with selector {0}", selector));
                                    }
                                    c++;
                                    $(selector).html(value.Value);
                                }
                            }
                        }
                    });
                    Notify(Format("{0} cells were populated!", c));
                }
            }
        };
        Table.prototype.SetCurrentCells = function () {
            this.CurrentCells = [];
            var me = this;
            var ext_dimensions = IsNull(me.CurrentExtension) ? [] : me.CurrentExtension.Dimensions;
            var ext_concept = IsNull(me.CurrentExtension) ? null : me.CurrentExtension.Concept;
            this.Cells.forEach(function (uicell, index) {
                var fact = new Model.Cell();
                var dimensions = uicell.Dimensions.concat(ext_dimensions);
                fact.Dimensions = dimensions.AsLinq().OrderBy(function (i) { return i.DomainMemberFullName; }).ToArray();
                fact.Concept = IsNull(uicell.Concept) ? ext_concept : uicell.Concept;
                fact.Row = uicell.Row;
                fact.Column = uicell.Column;
                fact.IsBlocked = uicell.IsBlocked;
                me.CurrentCells.push(fact);
            });
        };
        Table.prototype.SetExtension = function (item) {
            var li = JSON.parse(item);
            if (li != null && 'LabelContent' in li) {
                this.LoadExtension(li);
                $("#Extension").html(li.LabelContent);
                $("#Extension").attr("title", li.FactString);
            }
            else {
                li = new Model.LayoutItem();
                var label = new Model.Label();
                label.Code = "000";
                li.Label = label;
                this.LoadExtension(li);
            }
        };
        Table.prototype.SetCells = function (item) {
            var cells = JSON.parse(item);
            this.LoadCells(cells);
        };
        Table.prototype.SetInstance = function (item) {
            Notify("Instance recieved");
            var instance = JSON.parse(item);
            //f
            this.LoadInstance(instance);
        };
        Table.prototype.TestInstance = function () {
            this.LoadInstance(window["lastinstance"]);
        };
        return Table;
    })();
    UI.Table = Table;
})(UI || (UI = {}));
var Table = null;
function SetExtension(extjson) {
    Table.SetExtension(extjson);
}
function LoadInstance(instancejson) {
    Table.SetInstance(instancejson);
}
//# sourceMappingURL=Table.js.map