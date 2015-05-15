var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Model;
(function (Model) {
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
    var QualifiedName = (function () {
        function QualifiedName() {
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
    })();
    Model.QualifiedName = QualifiedName;
    var FactBase = (function () {
        function FactBase() {
            this.Concept = null;
            this.Dimensions = [];
        }
        Object.defineProperty(FactBase.prototype, "FactString", {
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
        return FactBase;
    })();
    Model.FactBase = FactBase;
    var Fact = (function (_super) {
        __extends(Fact, _super);
        function Fact() {
            _super.apply(this, arguments);
        }
        return Fact;
    })(FactBase);
    Model.Fact = Fact;
    var Identifiable = (function () {
        function Identifiable() {
        }
        return Identifiable;
    })();
    Model.Identifiable = Identifiable;
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
            this.FactDictionary = {};
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