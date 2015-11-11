﻿module Model {

    export interface Dictionary<T> {
        [key: string]: T;
    }
    export interface KeyValuePair<K,V> {
        Key: K;
        Value: V;
    }
  
    export class Hierarchy<T>
    {
        public Children: Hierarchy<T>[] = [];
        public Parent: Hierarchy<T> = null;
        public Order: number;
        public Item: T = null;

        public static ToArray<TClass>(hierarchy: Hierarchy<TClass>): TClass[]
        {
            var me = this;
            var items: TClass[] = [];
            items.push(hierarchy.Item);
            hierarchy.Children.forEach(function (item) {
                if ("ToArray" in item == false)
                {
                    item["ToArray"] = me.ToArray;
                }
                items = items.concat(Hierarchy.ToArray(item));
            });
            return items; 
        }

        //public FirstOrDefault(func:Function): T
        //{
        //    var T = null;

        //    return T;
        //}
    }

    export class Dimension {
        public DimensionItem: string;
        public Domain: string;
        public DomainMember: string;
        public IsTyped: boolean;

        public static DomainMemberFullName(dimension: Dimension): string {

            if (IsNull(dimension.DomainMember)) {
                return Format("[{0}]{1}", dimension.DimensionItem, dimension.Domain);
            }

            return Format("[{0}]{1}:{2}", dimension.DimensionItem, dimension.Domain, dimension.DomainMember);
        }
        public static ToStringForKey(dimension: Dimension): string {
            if (dimension.IsTyped) {
                return Format("[{0}]{1}", dimension.DimensionItem, dimension.Domain);
            }

            if (IsNull(dimension.DomainMember)) {
                return Format("[{0}]{1}", dimension.DimensionItem, dimension.Domain);
            }

            return Format("[{0}]{1}:{2}", dimension.DimensionItem, dimension.Domain, dimension.DomainMember);
        }
    }

    export class Identifiable {
        public ID: string;

    }

    export class QualifiedName extends Identifiable {
        public Namespace: string;
        public Name: string;
        public Content: string;
        static Set(qn: QualifiedName)
        {
            var qname = QualifiedName.Create(qn.Content);
            qn.Name = qname.Name;
            qn.Namespace = qname.Namespace;
        }
        static Create(content: string): QualifiedName {
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
        }
        public get FullName(): string {
            if (IsNull(this.Namespace)) {
                return this.Name;
            }
            return Format("{0}:{1}", this.Namespace, this.Name);
        }

        public set FullName(value:string) {
            var parts = value.split(':');
            if (parts.length == 2)
            {
                this.Name = parts[1];
                this.Namespace = parts[0];
            }
        }
    }

    export class QualifiedItem extends QualifiedName
    {
        public Role: string;
        public LabelID: string;
        public Label: Label;

        public get LabelContent() {
            return IsNull(this.Label) ? "" : this.Label.Content;
        }

        public get LabelCode() {
            return IsNull(this.Label) ? "" : this.Label.Code;
        }
    }

    export class Concept extends QualifiedName
    {
        public Domain: QualifiedName = null;
        public HierarchyRole: string = "";
    }

    export class FactBase {
        public Concept: QualifiedName = null;
        public Dimensions: Dimension[] = [];

        private _FactString: string = "";
        public GetFactString(): string
        {
            var me = this;

            var result = "";
            if (!IsNull(this.Concept)) {
                result = this.Concept.FullName + ",";
            }
            var lastdimns = "";
            var ref = new Refrence(lastdimns);
            var dimensions = this.Dimensions.sort(function (a, b) { return Dimension.DomainMemberFullName(a) < Dimension.DomainMemberFullName(b) ? -1 : 1; });
            dimensions.forEach(function (dimension, index) {
                var dimstr = Dimension.DomainMemberFullName(dimension);
                dimstr = FactBase.Format(dimstr, ref);

                result += Format("{0},", dimstr);
            });
            return result;
        }

        public GetFactKey(): string {
            var me = this;

            var result = "";
            if (!IsNull(this.Concept)) {
                result = this.Concept.FullName + ",";
            }
            var lastdimns = "";
            var ref = new Refrence(lastdimns);
            this.Dimensions.forEach(function (dimension, index) {
                var dimstr = Dimension.ToStringForKey(dimension);
                dimstr = FactBase.Format(dimstr, ref);
                result += Format("{0},", dimstr);
            });
            return result;
        }

        public get FactString(): string {
            if (IsNull(this._FactString)) {
                this._FactString = this.GetFactString();
            }
            return this._FactString;
        }
        public set FactString(value:string) {
            this._FactString = value;
        }

        public static Merge(target: FactBase, item: FactBase, overwrite:boolean=false)
        {
            if (IsNull(target.Concept)) { target.Concept = item.Concept; }
            var targetdimensions = target.Dimensions.AsLinq<Dimension>();
            item.Dimensions.forEach(function (dimension, ix) {
                var exisiting = targetdimensions.FirstOrDefault(i=> i.Domain == dimension.Domain && i.DimensionItem == dimension.DimensionItem);
                if (IsNull(exisiting)) {
                    target.Dimensions.push(dimension);
                } else
                {
                    if (overwrite)
                    {
                        RemoveFrom(exisiting, target.Dimensions);
                        //removeFromArray(target.Dimensions, exisiting);
                        target.Dimensions.push(dimension);

                    }
                }
            });

        }

        public static LoadFromFactString(fact: FactBase)
        {
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
                    dimitem = Replace(dimitem,"*", lastdimns);
                }
                else {
                    lastdimns = dimitemns;
                }

                var domain = domainpart;
                var member = "";
                var dim = new Dimension();

                if (domainpart.indexOf(":")>-1) {
                    var domainparts = Split(domainpart, ":", true);
           
                    if (domainparts.length == 3 || domainparts[0].indexOf("_typ")>-1) {
                        domain = Format("{0}:{1}", domainparts[0], domainparts[1]);
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
        }

        public static GetFactFromString(factstring: string): FactBase
        {
            var fb = new FactBase();
            fb.FactString = factstring;
            FactBase.LoadFromFactString(fb);
            return fb;
        }

        public static Format(item: string, lastdimns: Refrence<string>) {

            var dimns = TextBetween(item, "[", ":");
            if (lastdimns.Value != dimns) {
                lastdimns.Value = dimns;
            }
            else {
                item = Replace(item, dimns, "*");

            }
            return item;

        }
    }

    export class FactGroup extends FactBase
    {
        public Facts: FactBase[] = [];
    }

    export class Unit extends Identifiable {
        public Measure: QualifiedName;
    }

    export class Entity extends Identifiable {
        public Scheme: string;
    }

    export class InstanceFact extends FactBase {
        public Value: string;
        public Entity: Entity;
        public Unit: Unit;
        public UnitID: string;
        public ContextID: string;
        public FactKey: string;
        public FactString: string;
        public Cells: string[] = [];

        public static Convert(obj:Object): InstanceFact
        {
            var item = new InstanceFact();
            item.FactKey = obj["FactKey"];
            item.FactString = obj["FactString"];
            item.ContextID = obj["ContextID"];
            item.UnitID = obj["UnitID"];
            item.Value = obj["Value"];
            return item;
        }

        public Load()
        {
            var items = this.FactString.split(",");
            if (items.length > 0)
            {
                var item = items[0];
                if (item.indexOf("[") == -1)
                {
                    this.Concept = Model.QualifiedName.Create(item);
                }
                for (var i = 1; i < items.length; i++)
                {
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
                        if (domainmemberparts.length == 3)
                        {
                            dimension.Domain = Format("{0}:{1}", domainmemberparts[0], domainmemberparts[1]);
                            dimension.DomainMember = domainmemberparts[2];
                            dimension.IsTyped = true;
                        }
                        this.Dimensions.push(dimension);
                    }
                }

            }
        }

    }
    export enum LayoutItemCategory {
        Unknown = 0,
        Aspect = 1,
        Rule = 2,
        BreakDown = 3,
        Dynamic = 4,
        Filter = 5,
    }

    export class LayoutItem extends FactBase {
        public ID: string;
        public Axis: string;
        public Category: LayoutItemCategory = LayoutItemCategory.Unknown;
        public Label: Label;

        public get LabelContent() {
            return IsNull(this.Label) ? "": this.Label.Content;
        }

        public get LabelCode() {
            return IsNull(this.Label) ? "" : this.Label.Code;
        }

    }

    export class ConceptLookUp {
        public Concept: string = "";
        public Values: Object = {};
        public OptionsHTML: string = "";
    }

    export class Cell extends InstanceFact {
        public Report: string;
        public Extension: string;
        public Row: string;
        public Column: string;
        public IsBlocked: boolean;

        public get CellID() {
            return Format("{0}<{1}|{2}|{3}>", this.Report, this.Extension, this.Row, this.Column);
        }

        public get LayoutID() {
            return Format("R{0}|C{1}", this.Row, this.Column);
        }

        public get FactKey():string {
            var me = this;
            var result = this.Concept.FullName+",";
            this.Dimensions.forEach(function (dimension, index) {
                result += Format("{0},", Dimension.DomainMemberFullName(dimension));
            });
            return result;
        }
    }

    export class Instance
    {
        public Facts: InstanceFact[] = [];
        public FilingIndicators: string[] = [];
        public ReportingDate: Date;
        public ReportingCurrency: string;
        public Entity: Entity;
        public TaxonomyModuleReference: string;
        public FullPath: string;
        public FactDictionary: Object = null;
        public DynamicCellDictionary: Dictionary<Dictionary<string>> = {};
    }   

    export class Label {
        public LabelID: string;
        public Lang: string;
        public Code: string;
        public Content: string;
        public FileName: string;

    }

    export class SimlpeValidationParameter
    {
        public Name: string;
        public Facts: string[] = [];
        public Value: string;
        public CellsOfFacts: Dictionary<string[]> = {};
        public BindAsSequence: boolean;

    }

    export class ValidationParameter {
        public Name: string;
        public FactGroups: FactGroup[] = [];
        public FallBackValue: string;
        public Type: string;
        public BindAsSequence: string;

    }

    export class ValidationRuleResult
    {
        public ID: string;
        public Parameters: SimlpeValidationParameter[] = [];
        public HasAllFind: string;
    }

    export class ValidationRule
    {
        public ID: string;
        public FunctionName: string;
        public OriginalExpression: string;
        public Title: string;
        public DisplayText: string;
        public Parameters: ValidationParameter[] = [];
        public Results: ValidationRuleResult[] = [];
        public HasAllFind: string= "";
    }

    export class Taxonomy
    {
        public Name: string;
        public EntryDocumentName: string;
        public ValidationRules: ValidationRule[] = [];
        public Labels: Model.Label[] = [];
        public Facts: Dictionary<string[]> = {};
        public FactList: General.KeyValue[] = [];
        public Module: TaxonomyModule = null;

        public Concepts: Model.Concept[] = [];
        public Hierarchies: Model.Hierarchy<Model.QualifiedItem>[] = [];
        public ConceptValues: Model.ConceptLookUp[] = [];

    }
    export class TaxonomyModule
    {
        public Name: string;
        public TaxonomyName: string;
        public FromDate: Date;
        public ToDate: Date;
        public SchemaRef: string;
        public LocalPath: string;

    }
    export class TaxonomyProperties {

    }

    export class TableInfo extends Identifiable
    {
        public Name: string = "";
        public Description: string = "";
        public Type: string = "";
        public CssClass: string = "";
        public ExtensionText: string = "";
    }
}