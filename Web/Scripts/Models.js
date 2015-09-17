var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Model;
(function (Model) {
    //interface Dictionary<T,K> {
    //    [key: K]: T;
    //}
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
        Object.defineProperty(Dimension.prototype, "ToStringForKey", {
            get: function () {
                if (this.IsTyped) {
                    return Format("[{0}]{1}", this.DimensionItem, this.Domain);
                }
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
        QualifiedName.Set = function (qn) {
            var qname = QualifiedName.Create(qn.Content);
            qn.Name = qname.Name;
            qn.Namespace = qname.Namespace;
        };
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
            set: function (value) {
                var parts = value.split(':');
                if (parts.length == 2) {
                    this.Name = parts[1];
                    this.Namespace = parts[0];
                }
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
        FactBase.prototype.GetFactKey = function () {
            var me = this;
            var result = "";
            if (!IsNull(this.Concept)) {
                result = this.Concept.FullName + ",";
            }
            this.Dimensions.forEach(function (dimension, index) {
                result += Format("{0},", dimension.ToStringForKey);
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
        FactBase.LoadFromFactString = function (fact) {
            var me = fact;
            me.Dimensions = [];
            me.Concept = null;
            var item = me.FactString;
            var parts = Split(item, ",", true);
            var toskip = 0;
            if (parts.length > 0) {
                if (parts[0].indexOf("[") == -1) {
                    toskip = 1;
                    var concept = QualifiedName.Create(parts[0]);
                    me.Concept = concept;
                }
            }
            var dimparts = parts;
            dimparts.splice(0, toskip);
            dimparts.forEach(function (dimpart) {
                var dimitem = TextBetween(dimpart, "[", "]");
                var domainpart = dimpart.substring(dimitem.length + 2);
                var domain = domainpart;
                var member = "";
                var dim = new Dimension();
                if (domainpart.indexOf(":") > -1) {
                    var domainparts = Split(domainpart, ":", true);
                    if (domainparts.length == 2) {
                        domain = domainparts[0];
                        member = domainparts[1];
                    }
                    if (domainparts.length == 3) {
                        domain = Format("{0}:{1}", domainparts[0], domainparts[1]);
                        member = domainparts[2];
                        dim.IsTyped = true;
                    }
                }
                dim.DimensionItem = dimitem;
                dim.Domain = domain;
                dim.DomainMember = member;
                me.Dimensions.push(dim);
            });
        };
        return FactBase;
    })();
    Model.FactBase = FactBase;
    var FactGroup = (function (_super) {
        __extends(FactGroup, _super);
        function FactGroup() {
            _super.apply(this, arguments);
            this.Facts = [];
        }
        return FactGroup;
    })(FactBase);
    Model.FactGroup = FactGroup;
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
    var InstanceFact = (function (_super) {
        __extends(InstanceFact, _super);
        function InstanceFact() {
            _super.apply(this, arguments);
            this.Cells = [];
        }
        InstanceFact.Convert = function (obj) {
            var item = new InstanceFact();
            item.FactKey = obj["FactKey"];
            item.FactString = obj["FactString"];
            item.ContextID = obj["ContextID"];
            item.UnitID = obj["UnitID"];
            item.Value = obj["Value"];
            return item;
        };
        InstanceFact.prototype.Load = function () {
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
                            dimension.IsTyped = true;
                        }
                        this.Dimensions.push(dimension);
                    }
                }
            }
        };
        return InstanceFact;
    })(FactBase);
    Model.InstanceFact = InstanceFact;
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
    var ConceptLookUp = (function () {
        function ConceptLookUp() {
            this.Concept = "";
            this.Values = {};
            this.OptionsHTML = "";
        }
        return ConceptLookUp;
    })();
    Model.ConceptLookUp = ConceptLookUp;
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
    })(InstanceFact);
    Model.Cell = Cell;
    var Instance = (function () {
        function Instance() {
            this.Facts = [];
            this.FilingIndicators = [];
            this.FactDictionary = null;
            this.DynamicCellDictionary = {};
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
    var SimlpeValidationParameter = (function () {
        function SimlpeValidationParameter() {
            this.Facts = [];
            this.CellsOfFacts = {};
        }
        return SimlpeValidationParameter;
    })();
    Model.SimlpeValidationParameter = SimlpeValidationParameter;
    var ValidationParameter = (function () {
        function ValidationParameter() {
            this.FactGroups = [];
        }
        return ValidationParameter;
    })();
    Model.ValidationParameter = ValidationParameter;
    var ValidationRuleResult = (function () {
        function ValidationRuleResult() {
            this.Parameters = [];
        }
        return ValidationRuleResult;
    })();
    Model.ValidationRuleResult = ValidationRuleResult;
    var ValidationRule = (function () {
        function ValidationRule() {
            this.Parameters = [];
            this.Results = [];
            this.HasAllFind = "";
        }
        return ValidationRule;
    })();
    Model.ValidationRule = ValidationRule;
    var Taxonomy = (function () {
        function Taxonomy() {
            this.ValidationRules = [];
            this.Labels = [];
            this.Facts = {};
            this.FactList = [];
            this.Module = null;
            this.Concepts = [];
            this.Hierarchies = [];
            this.ConceptValues = [];
        }
        return Taxonomy;
    })();
    Model.Taxonomy = Taxonomy;
    var TaxonomyModule = (function () {
        function TaxonomyModule() {
        }
        return TaxonomyModule;
    })();
    Model.TaxonomyModule = TaxonomyModule;
    var TaxonomyProperties = (function () {
        function TaxonomyProperties() {
        }
        return TaxonomyProperties;
    })();
    Model.TaxonomyProperties = TaxonomyProperties;
    var TableInfo = (function (_super) {
        __extends(TableInfo, _super);
        function TableInfo() {
            _super.apply(this, arguments);
            this.Name = "";
            this.Description = "";
            this.Type = "";
            this.CssClass = "";
            this.ExtensionText = "";
        }
        return TableInfo;
    })(Identifiable);
    Model.TableInfo = TableInfo;
})(Model || (Model = {}));
//# sourceMappingURL=Models.js.map