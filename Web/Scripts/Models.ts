module Model {

    export interface Dictionary<T> {
        [key: string]: T;
    }

    //export interface Dictionary<TKey,TValue> {
    //    [key: TKey]: TValue;
    //}
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

        public static GetAxis<TClass>(item: Hierarchy<TClass>, axis: string): TClass[]
        {
            var laxis = axis.toLowerCase();
            var result: TClass[] = [];
            var f_ancestors = ()=> {
                var litem = item.Parent;
                while (litem != null) {
                    result.push(litem.Item);
                    litem = litem.Parent;
                }
                return result;
            };
            if (laxis == "self") {
                result.push(item.Item);
            }
            if (laxis == "parent") {
                if (item.Parent != null) {
                    result.push(item.Parent.Item);
                }
            }
            if (laxis == "ancestor")
            {
                result = f_ancestors();
            }
            if (laxis == "ancestor-or-self") {
                result = f_ancestors();
                result.push(item.Item);
            }
            if (laxis == "child") {
                result = item.Children.AsLinq<Hierarchy<TClass>>().Select(i=>i.Item).ToArray();
            }
            if (laxis == "descendant") {
                result = Hierarchy.ToArray(item);
                removeFromArray(result, item.Item);
            }
            if (laxis == "descendant-or-self") {
                result = Hierarchy.ToArray(item);

            }
            if (laxis == "preceding") { }
            if (laxis == "preceding-sibling") { }
            if (laxis == "namespace") { }
            if (laxis == "following") { }
            if (laxis == "following-sibling") { }
            if (laxis == "attribute") { }
            
            return result;
        }

        public static FirstOrDefault<TClass>(item: Hierarchy<TClass>, func: Func<Hierarchy<TClass>,boolean>): Hierarchy<TClass> {
            var result: Hierarchy<TClass>= null;
            if (func(item)) {
                result = item;
            }
            else {
                for (var i = 0; i < item.Children.length;i++){
                    var child = item.Children[i];
                    result = Hierarchy.FirstOrDefault(child, func);
                    if (result != null) {
                        return result;
                    }
                }
            }
            return result;
        }

    }

    export class Dimension {
        public DimensionItem: string;
        public Domain: string;
        public DomainMember: string;
        public IsTyped: boolean;
        public static DefaultMember: string = "x0";

        public static DomainMemberFullName(dimension: Dimension): string {
            if ("DomainMemberFullName" in dimension) {
                return dimension["DomainMemberFullName"];
            } else {
                var dmfn = "";
                if (IsNull(dimension.DomainMember)) {
                    //dmfn = Format("[{0}]{1}", dimension.DimensionItem, dimension.Domain);
                    dmfn = "[" + dimension.DimensionItem + "]" + dimension.Domain;

                }
                else {
                    //dmfn = Format("[{0}]{1}:{2}", dimension.DimensionItem, dimension.Domain, dimension.DomainMember);
                    dmfn = "["+dimension.DimensionItem+"]"+dimension.Domain+":"+ dimension.DomainMember;
                }
                dimension["DomainMemberFullName"] = dmfn;
                return dmfn;
            }
        }
        public static ToStringForKey(dimension: Dimension): string {
            if (dimension.IsTyped || IsNull(dimension.DomainMember)) {
                //return Format("[{0}]{1}", dimension.DimensionItem, dimension.Domain);
                return "[" + dimension.DimensionItem + "]" + dimension.Domain;
            }

            //return Format("[{0}]{1}:{2}", dimension.DimensionItem, dimension.Domain, dimension.DomainMember);
            return "[" + dimension.DimensionItem + "]" + dimension.Domain + ":" + dimension.DomainMember;
        }

        public static IsTyped(domainmemberfullname: string): boolean
        {
            if (domainmemberfullname.indexOf("_typ") > -1) { return true; }
            return false;
        }

        public static GetDomainFullName(dimension:Dimension): string
        {
            return "[" + dimension.DimensionItem + "]" + dimension.Domain;
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
            //return Format("{0}:{1}", this.Namespace, this.Name);
            return this.Namespace+":"+ this.Name;
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

        public FactString: string = "";
        public FactKeys: number[] = [];
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
                //result += Format("{0},", dimstr);
                result += dimstr + ",";
            });
            me.FactString = result;
            return result;
        }
 
        public static GetFactKey(fact:FactBase): string {

            var result = "";
            if (!IsNull(fact.FactString))
            {
                var parts = fact.FactString.split(",");
                parts.forEach(function (part) {
                    if (!IsNull(part)) {
                        if (Dimension.IsTyped(part)) {
                            var dimparts = part.split(":");
                            if (dimparts.length > 3) {
                                result += part.substring(0, part.lastIndexOf(":"));

                            } else
                            {
                                result += part;

                            }
                        }
                        else {
                            result += part;
                        }
                        result += ",";
                    }
                });
            }
            return result;
        }

     
        public static Merge(target: FactBase, item: FactBase, overwrite:boolean=false)
        {
            if (IsNull(target.Concept)) { target.Concept = item.Concept; }
            var targetdimensions = target.Dimensions.AsLinq<Dimension>();
            item.Dimensions.forEach(function (dimension, ix) {
                var existing: Dimension = null;
                var dim: Dimension = null;
                var existing_ix = -1;
                for (var i = 0; i < target.Dimensions.length; i++)
                {
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
                } else
                {
                    if (overwrite)
                    {
                        target.Dimensions[existing_ix] = dimension;

                        //RemoveFrom(exisiting, target.Dimensions);
                        //target.Dimensions.push(dimension);

                    }
                }
            });

        }
        public static RemoveDimensionsWithDefaultMemebr(fact: FactBase)
        {
            var itemstoremove = [];
            fact.Dimensions.forEach((d,ix) => {
                if (d.DomainMember == Dimension.DefaultMember) { itemstoremove.push(d); }
            });
            itemstoremove.forEach((d, ix) => {
                RemoveFrom(d, fact.Dimensions);
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
                        //domain = Format("{0}:{1}", domainparts[0], domainparts[1]);
                        domain = domainparts[0]+":"+ domainparts[1];
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

            //var dimns = TextBetween(item, "[", ":");
            //if (lastdimns.Value != dimns) {
            //    lastdimns.Value = dimns;
            //}
            //else {
            //    item = Replace(item, dimns, "*");

            //}
            return item;

        }

        public static GetFullFactString(fact:FactBase): string {

            var result = "";
            if (!IsNull(fact.Concept)) {
                result = fact.Concept.FullName + ",";
            }
            var dimensions = fact.Dimensions.sort(function (a, b) { return Dimension.DomainMemberFullName(a) < Dimension.DomainMemberFullName(b) ? -1 : 1; });
            dimensions.forEach(function (dimension, index) {
                var dimstr = Dimension.DomainMemberFullName(dimension);

                result += Format("{0},", dimstr);
            });
            return result;
        }
    }

    export class FactGroup extends FactBase
    {
        public Facts: FactBase[] = [];
    }

    export class Unit extends Identifiable {
        public Measure: QualifiedName;
    }
    export class InstanceUnit extends Unit
    {
        public UnitRef: string = "";
    }
    export class FilingIndicator extends Identifiable
    {
        public Filed: boolean = true;
    }
    export class Entity extends Identifiable {
        public Scheme: string;
    }

    export class InstanceFact extends FactBase {
        public Value: string;
        public Entity: Entity;
        public Unit: Unit;
        public ID: string;
        public Decimals: string;
        public UnitID: string;
        public ContextID: string;
        public Content: string;
        public FactKey: string;
        //public FactString: string;
        public FactID: number;
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
            if (IsNull(this.FactString)) { return null; }
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

        public SetFromCellID(CellID: string) {
            var reportpart = CellID.substring(0, CellID.indexOf("<"));
            var cellpart = TextBetween(CellID, "<", ">");
            var cellparts = cellpart.split("|");
            if (cellparts.length == 3) {
                this.Report = reportpart;
                this.Extension = cellparts[0];
                this.Row = cellparts[1];
                this.Column = cellparts[2];
            }
        }
    }
    export class DynamicCellDictionary
    {
        public ExtDictionary: Dictionary<string> = {};
        public RowDictionary: Dictionary<string> = {};
        public ColDictionary: Dictionary<string> = {};
        public CellOfFact: Dictionary<string> = {};
        public Extensions: Hierarchy<LayoutItem> = null;
    }
    export class InstanceFactDictionary
    {
      
        public FactsByIndex: Dictionary<InstanceFact> = {};
        public FactsByTaxonomyKey: Dictionary<number[]> = {};
        public FactsByInstanceKey: Dictionary<number> = {};

        public GetIntKey = (s: string) => "";

        public ContainsStringKey(stringkey: string): boolean {
            var key = this.GetIntKey(stringkey);
            return this.ContainsKey(key);
        }

        public ContainsKey(key: string): boolean {
            return key in this.FactsByTaxonomyKey ? true : key in this.FactsByInstanceKey;
     
        }
        public GetFactsByStringKey(stringkey: string): InstanceFact[]
        {
            var key = this.GetIntKey(stringkey);
            return this.GetFacts(key);
        }
        public GetFacts(key: string):InstanceFact[]
        {
            var me = this;
            var result: InstanceFact[] = [];
            if (key in me.FactsByTaxonomyKey) {
                var ixs = me.FactsByTaxonomyKey[key];
                var factlist: InstanceFact[] = ixs.AsLinq<number>().Select(i=> me.FactsByIndex[i]).ToArray();
                return factlist;
            }
            else
            {
                if (key in me.FactsByInstanceKey)
                {
                    var ix = me.FactsByInstanceKey[key];
                    return [me.FactsByIndex[ix]];

                }
            }
            return result;
        }

        public AddFact(fact: InstanceFact)
        {
            var me = this;
            var ix = Object.keys(me.FactsByIndex).length;
            fact.FactID = ix;
            me.FactsByIndex[ix] = fact;
        }
    }
    export class Instance
    {
        public Facts: InstanceFact[] = [];
        public Units: Unit[] = [];
        public FilingIndicators: FilingIndicator[] = [];
        public ReportingDate: Date;
        public ReportingCurrency: string;
        public Entity: Entity;
        public TaxonomyModuleReference: string;
        public FullPath: string;
        public FactDictionary: InstanceFactDictionary = null;
  
        public FactParts: Model.Dictionary<number> = {};
        public CounterFactParts: Model.Dictionary<string> = {};

        public TypedFactMembers: Model.Dictionary<number[]> = {};
        public DimensionDomainOfTypedFactMembers: Model.Dictionary<number> = {};

        public FactIDDictionary: Dictionary<InstanceFact> = null;
        public DynamicCellDictionary: Dictionary<Dictionary<string>> = {};
        public DynamicReportCells: Dictionary<DynamicCellDictionary> = {};

       

        public static SaveFact(instance:Instance, fact: InstanceFact) {
            var me = this;

            var existingfact: InstanceFact = null;
            var factkey = FactBase.GetFactKey(fact);

            var existingfacts = instance.FactDictionary.GetFactsByStringKey(factkey);
            if (IsNull(existingfacts)) {
                existingfact = fact;
                instance.FactDictionary.AddFact(existingfact);
                existingfacts = instance.FactDictionary.GetFactsByStringKey[factkey];
            }
            else {

                if (existingfacts.length == 0) {
                    existingfacts.push(fact);
                    existingfact = fact;
                }
                else if (existingfacts.length == 1) {
                    existingfact = existingfacts[0];
                }
                else
                {
                    existingfact = existingfacts.AsLinq<InstanceFact>().FirstOrDefault(i=> i.FactString == fact.FactString);
                    if (IsNull(existingfact)) {
                        existingfacts.push(fact);
                        existingfact = fact;
                    } 
                }
            }
            if (IsNull(fact.Value)) {
                removeFromArray(existingfact, existingfacts);
            } else
            {
                existingfact.Value = fact.Value;
            }
        }
    }   

    export class Label {
        public LocalID: string;
        public LabelID: string;
        public Lang: string;
        public Code: string;
        public Content: string;
        public FileName: string;

    }
    export class SimplLabel {
        public Code: string;
        public Content: string;
    }
    export class FactItem {
        public FactString: string = "";
        public Value: string = "";
        public Cells: string[] = [];
    }

    export class SimlpeValidationParameter
    {
        public Name: string;
        public Facts: string[] = [];
        public FactIDs: string[] = [];
        public Value: string;
        public Cells: string[][] = [];
       // public CellsOfFacts: Dictionary<string[]> = {};
        public BindAsSequence: boolean;
        public FactItems: FactItem[] = [];
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

        public FactParts: Model.Dictionary<number> = {};
        public CounterFactParts: Model.Dictionary<string> = {};

        public Concepts: Model.Concept[] = [];
        public CellIndexDictionary: Model.Dictionary<string> = {};
        public Hierarchies: Model.Hierarchy<Model.QualifiedItem> = null;
        public TaxonomyDocuments: Model.TaxonomyDocument[] = [];
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
        public FactParts: Dictionary<number>;
        public TypedDimensions: Dictionary<number>;
        public Concepts: Dictionary<number>;
        public DimensionDomainsOfMembers: Dictionary<number>;
        public MembersofDimensionDomains: Dictionary<number[]>;

    }
    export class TaxonomyProperties {

    }

    export class TaxonomyDocument
    {
        public FileName: string = "";
        //public ReferencedFiles: string[] = [];
        public LocalRelPath: string = "";
        //public SourcePath: string = "";
    }

    export class TableInfo extends Identifiable
    {
        public Name: string = "";
        public Description: string = "";
        public Type: string = "";
        public CssClass: string = "";
        public HasData: number = 0;
        public ExtensionText: string = "";
    }
}