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
        Hierarchy.ToArray = function (hierarchy) {
            var me = this;
            var items = [];
            items.push(hierarchy.Item);
            hierarchy.Children.forEach(function (item) {
                if ("ToArray" in item == false) {
                    item["ToArray"] = me.ToArray;
                }
                items = items.concat(Hierarchy.ToArray(item));
            });
            return items;
        };
        Hierarchy.FirstOrDefault = function (item, func) {
            var result = null;
            if (func(item)) {
                result = item;
            }
            else {
                for (var i = 0; i < item.Children.length; i++) {
                    var child = item.Children[i];
                    result = Hierarchy.FirstOrDefault(child, func);
                    if (result != null) {
                        return result;
                    }
                }
            }
            return result;
        };
        return Hierarchy;
    })();
    Model.Hierarchy = Hierarchy;
    var Dimension = (function () {
        function Dimension() {
        }
        Dimension.DomainMemberFullName = function (dimension) {
            if ("DomainMemberFullName" in dimension) {
                return dimension["DomainMemberFullName"];
            }
            else {
                var dmfn = "";
                if (IsNull(dimension.DomainMember)) {
                    //dmfn = Format("[{0}]{1}", dimension.DimensionItem, dimension.Domain);
                    dmfn = "[" + dimension.DimensionItem + "]" + dimension.Domain;
                }
                else {
                    //dmfn = Format("[{0}]{1}:{2}", dimension.DimensionItem, dimension.Domain, dimension.DomainMember);
                    dmfn = "[" + dimension.DimensionItem + "]" + dimension.Domain + ":" + dimension.DomainMember;
                }
                dimension["DomainMemberFullName"] = dmfn;
                return dmfn;
            }
        };
        Dimension.ToStringForKey = function (dimension) {
            if (dimension.IsTyped || IsNull(dimension.DomainMember)) {
                //return Format("[{0}]{1}", dimension.DimensionItem, dimension.Domain);
                return "[" + dimension.DimensionItem + "]" + dimension.Domain;
            }
            //return Format("[{0}]{1}:{2}", dimension.DimensionItem, dimension.Domain, dimension.DomainMember);
            return "[" + dimension.DimensionItem + "]" + dimension.Domain + ":" + dimension.DomainMember;
        };
        Dimension.IsTyped = function (domainmemberfullname) {
            if (domainmemberfullname.indexOf("_typ") > -1) {
                return true;
            }
            return false;
        };
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
                //return Format("{0}:{1}", this.Namespace, this.Name);
                return this.Namespace + ":" + this.Name;
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
            var lastdimns = "";
            var ref = new Refrence(lastdimns);
            var dimensions = this.Dimensions.sort(function (a, b) {
                return Dimension.DomainMemberFullName(a) < Dimension.DomainMemberFullName(b) ? -1 : 1;
            });
            dimensions.forEach(function (dimension, index) {
                var dimstr = Dimension.DomainMemberFullName(dimension);
                dimstr = FactBase.Format(dimstr, ref);
                //result += Format("{0},", dimstr);
                result += dimstr + ",";
            });
            me._FactString = result;
            return result;
        };
        FactBase.prototype.GetFactKey = function () {
            var me = this;
            var result = "";
            if (!IsNull(this.FactString)) {
                var parts = this.FactString.split(",");
                parts.forEach(function (part) {
                    if (!IsNull(part)) {
                        if (Dimension.IsTyped(part)) {
                            result += part.substring(0, part.lastIndexOf(":"));
                        }
                        else {
                            result += part;
                        }
                        result += ",";
                    }
                });
            }
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
        FactBase.Merge = function (target, item, overwrite) {
            if (overwrite === void 0) { overwrite = false; }
            if (IsNull(target.Concept)) {
                target.Concept = item.Concept;
            }
            var targetdimensions = target.Dimensions.AsLinq();
            item.Dimensions.forEach(function (dimension, ix) {
                var existing = null;
                var dim = null;
                var existing_ix = -1;
                for (var i = 0; i < target.Dimensions.length; i++) {
                    dim = target.Dimensions[i];
                    if (dim.Domain == dimension.Domain && dim.DimensionItem == dimension.DimensionItem) {
                        existing = dim;
                        existing_ix = i;
                        break;
                    }
                }
                //var exisiting = targetdimensions.FirstOrDefault(i=> i.Domain == dimension.Domain && i.DimensionItem == dimension.DimensionItem);
                if (IsNull(existing)) {
                    target.Dimensions.push(dimension);
                }
                else {
                    if (overwrite) {
                        target.Dimensions[existing_ix] = dimension;
                    }
                }
            });
        };
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
            var lastdimns = "";
            dimparts.forEach(function (dimpart) {
                var dimitem = TextBetween(dimpart, "[", "]");
                var domainpart = dimpart.substring(dimitem.length + 2);
                var dimitemns = dimitem.substr(0, dimitem.indexOf(":"));
                if (dimitemns == "*") {
                    dimitem = Replace(dimitem, "*", lastdimns);
                }
                else {
                    lastdimns = dimitemns;
                }
                var domain = domainpart;
                var member = "";
                var dim = new Dimension();
                if (domainpart.indexOf(":") > -1) {
                    var domainparts = Split(domainpart, ":", true);
                    if (domainparts.length == 3 || domainparts[0].indexOf("_typ") > -1) {
                        //domain = Format("{0}:{1}", domainparts[0], domainparts[1]);
                        domain = domainparts[0] + ":" + domainparts[1];
                        member = domainparts[2];
                        dim.IsTyped = true;
                    }
                    if (domainparts.length == 2 && !dim.IsTyped) {
                        domain = domainparts[0];
                        member = domainparts[1];
                    }
                }
                dim.DimensionItem = dimitem;
                dim.Domain = domain;
                dim.DomainMember = member;
                me.Dimensions.push(dim);
            });
        };
        FactBase.GetFactFromString = function (factstring) {
            var fb = new FactBase();
            fb.FactString = factstring;
            FactBase.LoadFromFactString(fb);
            return fb;
        };
        FactBase.Format = function (item, lastdimns) {
            var dimns = TextBetween(item, "[", ":");
            if (lastdimns.Value != dimns) {
                lastdimns.Value = dimns;
            }
            else {
                item = Replace(item, dimns, "*");
            }
            return item;
        };
        FactBase.GetFullFactString = function (fact) {
            var result = "";
            if (!IsNull(fact.Concept)) {
                result = fact.Concept.FullName + ",";
            }
            var dimensions = fact.Dimensions.sort(function (a, b) {
                return Dimension.DomainMemberFullName(a) < Dimension.DomainMemberFullName(b) ? -1 : 1;
            });
            dimensions.forEach(function (dimension, index) {
                var dimstr = Dimension.DomainMemberFullName(dimension);
                result += Format("{0},", dimstr);
            });
            return result;
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
    (function (LayoutItemCategory) {
        LayoutItemCategory[LayoutItemCategory["Unknown"] = 0] = "Unknown";
        LayoutItemCategory[LayoutItemCategory["Aspect"] = 1] = "Aspect";
        LayoutItemCategory[LayoutItemCategory["Rule"] = 2] = "Rule";
        LayoutItemCategory[LayoutItemCategory["BreakDown"] = 3] = "BreakDown";
        LayoutItemCategory[LayoutItemCategory["Dynamic"] = 4] = "Dynamic";
        LayoutItemCategory[LayoutItemCategory["Filter"] = 5] = "Filter";
    })(Model.LayoutItemCategory || (Model.LayoutItemCategory = {}));
    var LayoutItemCategory = Model.LayoutItemCategory;
    var LayoutItem = (function (_super) {
        __extends(LayoutItem, _super);
        function LayoutItem() {
            _super.apply(this, arguments);
            this.Category = 0 /* Unknown */;
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
                    result += Format("{0},", Dimension.DomainMemberFullName(dimension));
                });
                return result;
            },
            enumerable: true,
            configurable: true
        });
        Cell.prototype.SetFromCellID = function (CellID) {
            var reportpart = CellID.substring(0, CellID.indexOf("<"));
            var cellpart = TextBetween(CellID, "<", ">");
            var cellparts = cellpart.split("|");
            if (cellparts.length == 3) {
                this.Report = reportpart;
                this.Extension = cellparts[0];
                this.Row = cellparts[1];
                this.Column = cellparts[2];
            }
        };
        return Cell;
    })(InstanceFact);
    Model.Cell = Cell;
    var DynamicCellDictionary = (function () {
        function DynamicCellDictionary() {
            this.ExtDictionary = {};
            this.RowDictionary = {};
            this.ColDictionary = {};
            this.CellOfFact = {};
            this.Extensions = null;
        }
        return DynamicCellDictionary;
    })();
    Model.DynamicCellDictionary = DynamicCellDictionary;
    var Instance = (function () {
        function Instance() {
            this.Facts = [];
            this.FilingIndicators = [];
            this.FactDictionary = null;
            this.DynamicCellDictionary = {};
            this.DynamicReportCells = {};
        }
        Instance.GetFactFor = function (me, cellfact, cellid) {
            var facts = [];
            var fact = null;
            var factkey = cellfact.GetFactKey();
            var factstring = cellfact.FactString;
            if (factkey in me.FactDictionary) {
                facts = me.FactDictionary[factkey];
                if (facts.length > 0) {
                    fact = facts.AsLinq().FirstOrDefault(function (i) { return i.FactString == factstring; });
                }
            }
            return fact;
        };
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