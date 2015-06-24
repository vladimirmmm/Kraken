module Model {

    interface Dictionary<T> {
        [key: string]: T;
    }

    export class Hierarchy<T>
    {
        public Children: Hierarchy<T>[] = [];
        public Parent: Hierarchy<T> = null;
        public Order: number;
        public Item: T = null;

        public ToArray(): T[]
        {
            var me = this;
            var items: T[] = [];
            items.push(this.Item);
            this.Children.forEach(function (item) {
                if ("ToArray" in item == false)
                {
                    item["ToArray"] = me.ToArray;
                }
                items = items.concat(item.ToArray());
            });
            return items; 
        }
    }

    export class Dimension {
        public DimensionItem: string;
        public Domain: string;
        public DomainMember: string;
        public IsTyped: boolean;

        public get DomainMemberFullName(): string {
            if (IsNull(this.DomainMember)) {
                return Format("[{0}]{1}", this.DimensionItem, this.Domain);
            }

            return Format("[{0}]{1}:{2}", this.DimensionItem, this.Domain, this.DomainMember);
        }
    }

    export class Identifiable {
        public ID: string;

    }

    export class QualifiedName extends Identifiable {
        public Namespace: string;
        public Name: string;

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
            this.Dimensions.forEach(function (dimension, index) {
                result += Format("{0},", dimension.DomainMemberFullName);
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

    export class LayoutItem extends FactBase {
        public ID: string;
        public Axis: string;
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
                result +=Format("{0},",dimension.DomainMemberFullName);
            });
            return result;
        }
    }

    export class Instance
    {
        public Facts: InstanceFact[] = [];
        public FilingIndicators: string[] = [];
        public Reporting: Date;
        public ReportingCurrency: string;
        public Entity: Entity;

        public FactDictionary:Object = null;
    }   

    export class Label {
        public labelID: string;
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
        public Cells: string[] = [];

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

    }

    export class ValidationRule
    {
        public ID: string;
        public FunctionName: string;
        public OriginalExpression: string;
        public DisplayText: string;
        public Parameters: ValidationParameter[] = [];
        public Results: ValidationRuleResult[] = [];
    }


    export class Taxonomy
    {
        public Name: string;
        public EntryDocumentName: string;
        public ValidationRules: ValidationRule[] = [];
        //public FactMap: Dictionary<string[]> = {};
        public Labels: Dictionary<string[]> = {};

        public Concepts: Model.Concept[] = [];
        public Hierarchies: Model.Hierarchy<Model.QualifiedItem>[] = [];
        public ConceptValues: Model.ConceptLookUp[] = [];

    }

}