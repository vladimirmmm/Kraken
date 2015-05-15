module Model {
    export class Dimension {
        public DimensionItem: string;
        public Domain: string;
        public DomainMember: string;

        public get DomainMemberFullName(): string {
            if (IsNull(this.DomainMember)) {
                return Format("[{0}]{1}", this.DimensionItem, this.Domain);
            }

            return Format("[{0}]{1}:{2}", this.DimensionItem, this.Domain, this.DomainMember);
        }
    }

    export class QualifiedName {
        public Namespace: string;
        public Name: string;

        constructor() {

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
    }

    export class FactBase {
        public Concept: QualifiedName = null;
        public Dimensions: Dimension[] = [];

        public get FactString(): string {
            var me = this;
            var result = this.Concept.FullName + ",";
            this.Dimensions.forEach(function (dimension, index) {
                result += Format("{0},", dimension.DomainMemberFullName);
            });
            return result;
        }
    }

    export class Fact extends FactBase {
        public Value: string;
        public Entity: Entity;
        public Unit: Unit;
        public ContextID: string;

    }

    export class Identifiable {
        public ID: string;

    }

    export class Unit extends Identifiable {
        public Measure: QualifiedName;
    }
    export class Entity extends Identifiable {
        public Scheme: string;
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

    export class Cell extends Fact {
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
        public Facts: Fact[] = [];
        public FilingIndicators: string[] = [];
        public Reporting: Date;
        public ReportingCurrency: string;
        public Entity: Entity;

        public FactDictionary:Object = {};
    }   

    export class Label {
        public labelID: string;
        public Lang: string;
        public Code: string;
        public Content: string;
        public FileName: string;

    }


}