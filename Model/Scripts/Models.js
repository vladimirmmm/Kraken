var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Model;
(function (Model) {
    var Hierarchy = (function () {
        function Hierarchy() {
            this.Children = [];
            this.Parent = null;
            this.Item = null;
        }
        Hierarchy.prototype.ToArray = function () {
            var me = this;
            var items = [];
            items.push(this.Item);
            this.Children.forEach(function (item) {
                if ("ToArray" in item == false) {
                    item["ToArray"] = me.ToArray;
                }
                items = items.concat(item.ToArray());
            });
            return items;
        };
        return Hierarchy;
    })();
    Model.Hierarchy = Hierarchy;
    var Dimension = (function () {
        function Dimension() {
        }
        Object.defineProperty(Dimension.prototype, "DomainMemberFullName", {
            get: function () {
                if (IsNull(this.DomainMember)) {
                    return Format("[{0}]{1}", this.DimensionItem, this.Domain);
                }
                return Format("[{0}]{1}:{2}", this.DimensionItem, this.Domain, this.DomainMember);
            },
            enumerable: true,
            configurable: true
        });
        return Dimension;
    })();
    Model.Dimension = Dimension;
    var Identifiable = (function () {
        function Identifiable() {
        }
        return Identifiable;
    })();
    Model.Identifiable = Identifiable;
    var QualifiedName = (function (_super) {
        __extends(QualifiedName, _super);
        function QualifiedName() {
            _super.apply(this, arguments);
        }
        QualifiedName.Create = function (content) {
            var result = new QualifiedName();
            var parts = content.split(":");
            if (parts.length == 2) {
                result.Name = parts[1];
                result.Namespace = parts[0];
            }
            else {
                result.Name = content;
            }
            return result;
        };
        Object.defineProperty(QualifiedName.prototype, "FullName", {
            get: function () {
                if (IsNull(this.Namespace)) {
                    return this.Name;
                }
                return Format("{0}:{1}", this.Namespace, this.Name);
            },
            enumerable: true,
            configurable: true
        });
        return QualifiedName;
    })(Identifiable);
    Model.QualifiedName = QualifiedName;
    var QualifiedItem = (function (_super) {
        __extends(QualifiedItem, _super);
        function QualifiedItem() {
            _super.apply(this, arguments);
        }
        Object.defineProperty(QualifiedItem.prototype, "LabelContent", {
            get: function () {
                return IsNull(this.Label) ? "" : this.Label.Content;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(QualifiedItem.prototype, "LabelCode", {
            get: function () {
                return IsNull(this.Label) ? "" : this.Label.Code;
            },
            enumerable: true,
            configurable: true
        });
        return QualifiedItem;
    })(QualifiedName);
    Model.QualifiedItem = QualifiedItem;
    var Concept = (function (_super) {
        __extends(Concept, _super);
        function Concept() {
            _super.apply(this, arguments);
            this.Domain = null;
            this.HierarchyRole = "";
        }
        return Concept;
    })(QualifiedName);
    Model.Concept = Concept;
    var FactBase = (function () {
        function FactBase() {
            this.Concept = null;
            this.Dimensions = [];
            this._FactString = "";
        }
        FactBase.prototype.GetFactString = function () {
            var me = this;
            var result = "";
            if (!IsNull(this.Concept)) {
                result = this.Concept.FullName + ",";
            }
            this.Dimensions.forEach(function (dimension, index) {
                result += Format("{0},", dimension.DomainMemberFullName);
            });
            return result;
        };
        Object.defineProperty(FactBase.prototype, "FactString", {
            get: function () {
                if (IsNull(this._FactString)) {
                    this._FactString = this.GetFactString();
                }
                return this._FactString;
            },
            set: function (value) {
                this._FactString = value;
            },
            enumerable: true,
            configurable: true
        });
        return FactBase;
    })();
    Model.FactBase = FactBase;
    var Fact = (function (_super) {
        __extends(Fact, _super);
        function Fact() {
            _super.apply(this, arguments);
        }
        Fact.Convert = function (obj) {
            var item = new Fact();
            item.FactKey = obj["FactKey"];
            item.FactString = obj["FactString"];
            item.ContextID = obj["ContextID"];
            item.UnitID = obj["UnitID"];
            item.Value = obj["Value"];
            return item;
        };
        Fact.prototype.Load = function () {
            var items = this.FactString.split(",");
            if (items.length > 0) {
                var item = items[0];
                if (item.indexOf("[") == -1) {
                    this.Concept = Model.QualifiedName.Create(item);
                }
                for (var i = 1; i < items.length; i++) {
                    var item = items[i];
                    if (!IsNull(item.trim())) {
                        var dimension = new Model.Dimension();
                        dimension.DimensionItem = TextBetween(item, "[", "]");
                        var domainmember = item.substring(item.lastIndexOf("]") + 1);
                        var domainmemberparts = domainmember.split(":");
                        if (domainmemberparts.length == 1) {
                            dimension.Domain = domainmemberparts[0];
                        }
                        if (domainmemberparts.length == 2) {
                            dimension.Domain = domainmemberparts[0];
                            dimension.DomainMember = domainmemberparts[1];
                        }
                        if (domainmemberparts.length == 3) {
                            dimension.Domain = Format("{0}:{1}", domainmemberparts[0], domainmemberparts[1]);
                            dimension.DomainMember = domainmemberparts[2];
                        }
                        this.Dimensions.push(dimension);
                    }
                }
            }
        };
        return Fact;
    })(FactBase);
    Model.Fact = Fact;
    var Unit = (function (_super) {
        __extends(Unit, _super);
        function Unit() {
            _super.apply(this, arguments);
        }
        return Unit;
    })(Identifiable);
    Model.Unit = Unit;
    var Entity = (function (_super) {
        __extends(Entity, _super);
        function Entity() {
            _super.apply(this, arguments);
        }
        return Entity;
    })(Identifiable);
    Model.Entity = Entity;
    var LayoutItem = (function (_super) {
        __extends(LayoutItem, _super);
        function LayoutItem() {
            _super.apply(this, arguments);
        }
        Object.defineProperty(LayoutItem.prototype, "LabelContent", {
            get: function () {
                return IsNull(this.Label) ? "" : this.Label.Content;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(LayoutItem.prototype, "LabelCode", {
            get: function () {
                return IsNull(this.Label) ? "" : this.Label.Code;
            },
            enumerable: true,
            configurable: true
        });
        return LayoutItem;
    })(FactBase);
    Model.LayoutItem = LayoutItem;
    var Cell = (function (_super) {
        __extends(Cell, _super);
        function Cell() {
            _super.apply(this, arguments);
        }
        Object.defineProperty(Cell.prototype, "CellID", {
            get: function () {
                return Format("{0}<{1}|{2}|{3}>", this.Report, this.Extension, this.Row, this.Column);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Cell.prototype, "LayoutID", {
            get: function () {
                return Format("R{0}|C{1}", this.Row, this.Column);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Cell.prototype, "FactKey", {
            get: function () {
                var me = this;
                var result = this.Concept.FullName + ",";
                this.Dimensions.forEach(function (dimension, index) {
                    result += Format("{0},", dimension.DomainMemberFullName);
                });
                return result;
            },
            enumerable: true,
            configurable: true
        });
        return Cell;
    })(Fact);
    Model.Cell = Cell;
    var Instance = (function () {
        function Instance() {
            this.Facts = [];
            this.FilingIndicators = [];
            this.FactDictionary = null;
        }
        return Instance;
    })();
    Model.Instance = Instance;
    var Label = (function () {
        function Label() {
        }
        return Label;
    })();
    Model.Label = Label;
})(Model || (Model = {}));
//# sourceMappingURL=Models.js.map