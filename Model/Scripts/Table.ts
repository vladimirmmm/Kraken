
module UI {

    export class Table {
        public Taxonomy: Model.Taxonomy = null;
        public Cells: Model.Cell[] = [];
        //public Extensions: Model.Hierarchy<Model.LayoutItem> = null;
        public ExtensionsRoot: Model.Hierarchy<Model.LayoutItem> = null;
        public Extensions: TSLinq.Linq<Model.LayoutItem> = null;
        public FactMap: Object = {};
        public CurrentExtension: Model.LayoutItem = null;
        public Instance: Model.Instance = null;
        public TemplateRow: Element = null;
        public TemplateCol: Element = null;

        public ConceptValues: Model.ConceptLookUp[] = [];

        public HtmlTemplate: string = "";
        public HtmlTemplatePath: string = "";
        
        private Current_CellID: string = "";
        private Current_ExtensionCode: string = "";
        private Current_ReportID: string = "";
        private IsInstanceLoaded: boolean = false;
        constructor()
        {
         
       
        }

        public LoadTable(reportid: string) {
            var me = this;
            var fload = function (data) {
                var jsonobj = JSON.parse(data);
                me.HtmlTemplatePath = jsonobj["HtmlTemplatePath"];

                me.ExtensionsRoot = jsonobj["ExtensionsRoot"];
                me.FactMap = jsonobj["FactMap"];


                AjaxRequest(me.HtmlTemplatePath, "get", "text/html", null, function (data)
                    {
                        _Html(_SelectFirst("#ReportContainer"), data);

                        me.Current_ReportID = reportid;

                        me.SetExternals();
                        me.Load();
                        me.GetData();
                        ShowNotification("Table instance loaded!");
                    },
                    function (error) {
                        console.log(error);
                    }
                );


                
            }
            AjaxRequest("Taxonomy/Table", "get", "text/html",
                { item: "factmap", reportid: reportid },
                function (data) {
                    fload(data);
                },
                function (error) { console.log(error); });


        }
 
        public SetExternals()
        {
            this.Extensions = Model.Hierarchy.ToArray(this.ExtensionsRoot)
                .AsLinq<Model.LayoutItem>()
                .Where(i=> In(i.Category,
                Model.LayoutItemCategory.Rule,
                Model.LayoutItemCategory.BreakDown))
                .Select(i=> i);
            this.CurrentExtension = this.ExtensionsRoot.Item;
     
        }

        public GetData()
        {
            var me = this;
            me.IsInstanceLoaded = false;

            me.LoadConceptValues();
            //me.LoadToUI();
            me.SetNavigation();
            

        }

        public SetNavigation()
        {
            var me = this;
            if (IsNull(me.Current_ExtensionCode)) {
                this.Current_ExtensionCode = this.Extensions.FirstOrDefault().LabelCode;
            }
            me.SetExtensionByCode(this.Current_ExtensionCode);
            me.LoadToUI();
            me.HighlightCell();

        }

        public LoadToUI()
        {
            var me = this;
            if (me.Instance != null && me.ConceptValues && !me.IsInstanceLoaded)
            {
                if (!IsNull(this.Instance)) {
                    this.LoadInstance(this.Instance);
                }
                me.IsInstanceLoaded = true;
            }
        }
  
        public Load()
        {
            var me = this;
            var uitablemanger = new Controls.TableManager();
            var uitable = new Controls.Table(uitablemanger);
            uitable.LoadfromHtml(_SelectFirst("#ReportContainer > table.report"));
            _EnsureEventHandler(_Select("table.report tr"), "click", function () {
                $('tr', 'table').removeClass("selected");
                $(this).addClass("selected");
            });
            /*
            $("table.report").on('click', 'tr', function () {
                $('tr', 'table').removeClass("selected");
                $(this).addClass("selected");
            });
            */
            this.LoadCellsFromHtml();
            this.SetCellEditors();

            var hash = window.location.hash;

         
        }

        public HighlightCell()
        {
            _RemoveClass(_Select(".highlight"), "highlight");
            //$(".highlight").removeClass("highlight");

            var cellselector = this.Current_CellID.replace(/_/g, "\\|").toUpperCase();
            var cells = _Select("#" + cellselector);
            _AddClass(cells, "highlight");
            //$("#" + cellselector).addClass("highlight");
            _Focus(cells);
            //$("#" + cellselector).focus();
        }

        public LoadConceptValues() {
            var me = this;
            var concepts: Model.Concept[] = <Model.Concept[]>GetPropertiesArray(me.Taxonomy.Concepts);

            if (me.Taxonomy.Hierarchies.length > 0 && concepts.length > 0) {
       
                me.ConceptValues = [];
                var htemp = new Model.Hierarchy<Model.QualifiedItem>();
                me.Taxonomy.Hierarchies.forEach(function (hierarchy) {
                    Model.QualifiedItem.Set(hierarchy.Item);

                });
                concepts.forEach(function (concept) {
                    Model.QualifiedName.Set(concept);
                    if (!IsNull(concept.Domain)) {
                        Model.QualifiedName.Set(concept.Domain);
                        concept.Domain.Name = IsNull(concept.Domain.Name) ? concept.Domain.ID : concept.Domain.Name;
                        var hiers = me.Taxonomy.Hierarchies.AsLinq<Model.Hierarchy<Model.QualifiedItem>>()
                            .Where(i=> i.Item.Name == concept.Domain.Name
                            && i.Item.Namespace == concept.Domain.Namespace
                            && i.Item.Role == concept.HierarchyRole
                            );
                        var hier = hiers.FirstOrDefault();
                        if (hier != null) {
                            var clkp = new Model.ConceptLookUp();
                            clkp.Concept = Format("{0}:{1}", concept.Namespace, concept.Name);
                            //hier["ToArray"] = Model.Hierarchy.ToArray(htemp); //() => htemp.ToArray.apply(hier);
                            var items = Model.Hierarchy.ToArray(hier);
                            items.forEach(function (item, index) {
                                if (index > 0) {
                                    var v = {};
                                    Model.QualifiedItem.Set(item);
                                    var id = Format("{0}:{1}", item.Namespace, item.Name);
                                    clkp.Values[id] = Format("({0}) {1}", id, item.Label == null ? "" : item.Label.Content);
                                }
                            });
                            clkp.OptionsHTML = ToOptionList(clkp.Values, true);
                            me.ConceptValues.push(clkp);
                        }
                    }
                });
            }
        }

        public GetConcepOptions(concept: string): string {
            var clkp = this.ConceptValues.AsLinq<Model.ConceptLookUp>().FirstOrDefault(i=> i.Concept == concept);
            if (clkp != null) {
                return clkp.OptionsHTML;
            }
            return "";
        }

        public SetCellEditors()
        {
            var me = this;
            var cellselector = ".data";
            var cells = _Select(cellselector);
            _RemoveEventHandlers(cells, "click");
            //$(cellselector).off("click");
            //$(cellselector).each(function (ix, item) {
            cells.forEach(function (item, ix) {
                var target = <Element>item;
                //var factitems = target.attr("factstring").split(",");
                var factitems = _Attribute(target,"factstring").split(",");
                var concept = "";
                if (factitems[0].indexOf("[") == -1)
                {
                    concept = factitems[0];
                }
    
                //if (!target.parent().hasClass("dynamic") && !target.hasClass("blocked")) {
                if (!_HasClass(_Parent(target), "dynamic") && !_HasClass(target,"blocked")) {
                
                    //target.click(function () {
                    _AddEventHandler(target,"click", function () {
                        //Notify("clicked")
                        //if (!target.hasClass(Editor.editclass)) {
                        if (!_HasClass(target, Editor.editclass)) {
                            var typeclass = "";
                            if (factitems[0].indexOf(":ei") > -1) {
                                typeclass = "ei";
                            }
                            if (factitems[0].indexOf(":bi") > -1) {
                                typeclass = "bi";
                            }
                            if (factitems[0].indexOf(":di") > -1) {
                                typeclass = "di";
                            }
                            var editor: Editor = null;
                            if (typeclass == "bi") {
                                editor = new Editor('<select class="celleditor"><option>true</option><option>false</option></select>',
                                    (i: JQuery) => i.val(),
                                    (i: JQuery, val: any) => { i.val(val); });

                            } 
                            if (typeclass=="ei") {
                                editor = new Editor(Format('<select class="celleditor">{0}</select>', me.GetConcepOptions(concept)),
                                    (i: JQuery) => i.val(),
                                    (i: JQuery, val: any) => { i.val(val); });

                            } 
                            if (typeclass == "di") {
                                editor = new Editor('<input type="text" class="celleditor datepicker" value="" />',
                                    (i: JQuery) => {
                                        return i.val();
                                    },
                                    (i: JQuery, val: any) => {
                                        i.datepicker({
                                            dateFormat: "yy-mm-dd",
                                            onSelect: function () {
                                                editor.Save();
                                            }
                                        });
                                        i.val(val);
                                    });
                                editor.CustomTrigger = () => { };

                            } 
                            if (typeclass == "") 
                            {
                                editor = new Editor('<input type="text" class="celleditor " value="" />',
                                    (i: JQuery) => i.val(),
                                    (i: JQuery, val: any) => i.val(val));
                            }

                            editor.Load(target,
                                () => _Html(target),
                                () => { _Html(target, editor.ValueGetter(editor.$Me)); me.ManageRows(); }
                                );
                        }

                    });
                }
            });
        }

        private ManageRows()
        {
            var me = this;
            if (!IsNull(me.TemplateRow)) {
                var parentrow = _Parent(me.TemplateRow);
                var lastrow = _FindFirst(parentrow,"tr:last");
                var selectedrow = _FindFirst(parentrow, "tr.selected");

                if (!IsNull(selectedrow) && !IsNull(lastrow)) {
            
                    if (me.EmptyCellsOnly(selectedrow) && lastrow != selectedrow) {
                        _Remove(selectedrow);
                    }
                    if (!me.EmptyCellsOnly(lastrow)){

                        var newrow = me.AddRow("newrow", false, 0);
                        me.SetCellEditors();
                    }

                }
            }
        }

        private EmptyCellsOnly(row: Element): boolean
        {
            var cells = _Select("td", row);
            var isallnull = true;
            cells.forEach(function (cellelement, ix) {
                var value = _Html(cellelement).trim();
                if (!IsNull(value)) {
                    isallnull = false;
                }
            });
            return isallnull;
        }

        public LoadCellsFromHtml()
        {
            var me = this;
            var s_ix = 1;
            me.Cells = [];
            var datacells = _Select(".report .data");
            datacells.forEach(function (cellelement, index) {
                var layoutid: string = _Attribute(cellelement,"id");
                var row = layoutid.split("|")[0];
                var col = layoutid.split("|")[1];
                row = row.substring(1);
                col = col.substring(1);

                var cell: Model.Cell = new Model.Cell();
                cell.IsBlocked = _HasClass(cellelement,"blocked");
                if (!cell.IsBlocked) {
                    cell.Row = row;
                    cell.Column = col;         
                    me.Cells.push(cell);
                }
                _Attribute(cellelement, "title", layoutid + "\r\n" + _Attribute(cellelement, "factstring"));
            });
            var templaterow = _SelectFirst("tr.dynamic");
            if (!IsNull(templaterow)) {
                me.TemplateRow = templaterow;
                me.AddRow("newrow", false, 0);
            }
      
        }

        public LoadCells(cells: Model.Cell[])
        {
            this.Cells = cells;
        }

        public LoadExtension(li: Model.LayoutItem) {
            this.CurrentExtension = li;
            var extensionscell = _SelectFirst("#Extension");
            _Html(extensionscell, Format("{0} <br /> {1}", li.LabelCode, li.LabelContent));
            _Attribute(extensionscell,"title", li.FactString);
        }

        public LoadInstance(instance: Model.Instance) {
            var me = this;

            if (IsNull(me.Instance)) {
                me.Instance = instance;
                if (IsNull(me.Instance.FactDictionary)) {

                    me.Instance.FactDictionary = {};
                    me.Instance.Facts.forEach(function (fact, index) {
                        if (!IsNull(me.Instance.FactDictionary[fact.FactString]))
                        {
                            var x = 5;
                        }
                        me.Instance.FactDictionary[fact.FactString] = fact;
                    });
                }
            }
            me.SetDynamicRows();
            if (!IsNull(me.FactMap)) {
                var c = 0;
                var extfacts = me.FactMap[me.CurrentExtension.LabelCode];
                if (extfacts != null) {
                    this.Cells.forEach(function (cell, index) {
                        if (!cell.IsBlocked) {
                            
                            if (cell.LayoutID in extfacts)
                            {
                                var factstring = extfacts[cell.LayoutID];
                                if (!IsNull(factstring))
                                {
                                    if (!(factstring in me.Instance.FactDictionary)) {
                                        // Notify(cell.FactString + " not found in the instance");
                                    } else {
                                        var facts: Model.InstanceFact[] = me.Instance.FactDictionary[factstring];
                                        if (facts.length == 1 && facts[0].FactKey==facts[0].FactString) {
                                            var fact = facts[0];
                                            if (!IsNull(fact)) {
                                                var selector = "#" + cell.LayoutID.replace("|", "\\|");
                                                var cellelement = _SelectFirst(selector);
                                                if (IsNull(cellelement)) {
                                                    ShowNotification(Format("No cell found with selector {0}", selector));
                                                }
                                                else
                                                {
                                                    _Html(cellelement, fact.Value);
                                                }
                                                c++;
                                                //set was here
                                            }
                                        }
                                        else
                                        {
                                            //dynamic
                                           

                                            facts.forEach(function (factobj, index) {
                                                var fact = Model.InstanceFact.Convert(factobj);
                                                //fact.Load();
                                                Model.FactBase.LoadFromFactString(fact);

                                                var typeddimensions = fact.Dimensions.AsLinq<Model.Dimension>().Where(i=> i.IsTyped).ToArray();
                                                var typedfacts = new Model.FactBase();
                                                typedfacts.Dimensions = typeddimensions;
                                                var rowid = me.GetDynamicRowID(cell.LayoutID, typedfacts);

                                                var cellid = cell.LayoutID;
                                                var r_ix = cellid.indexOf("R");
                                                if (r_ix > -1) {
                                                    cellid = cellid.replace("R", rowid);
                                                }
                                                var selector = "#" + cellid.replace("|", "\\|");
                                                var cellelement = _SelectFirst(selector);
                                                _Html(cellelement, fact.Value);
                                            });

                                        }
                                    }
                                }
                            }
                            
                        }
                    });
                    me.SetCellEditors();
                    ShowNotification(Format("{0} cells were populated!", c));
                }
            }
        }

        public SetDynamicRows()
        {
            var me = this;

            var datarows = _Select("tr.dynamicdata");
            _Remove(datarows);

            var cellobj = me.Cells[0];
            var url = window.location.pathname;
            var reportid = me.Current_ReportID;

            var extensioncode = IsNull(me.Current_ExtensionCode) ? this.ExtensionsRoot.Item.LabelCode : me.Current_ExtensionCode;


            var reportkey = Format("{0}|{1}", reportid, extensioncode);

            var rowidcontainer = me.Instance.DynamicCellDictionary[reportkey];
            var rows = GetProperties(rowidcontainer);
            rows.forEach(function (rowitem) { 
                var row = me.AddRow("", true, rowitem.Value);
                var fact = new Model.FactBase();
                fact.FactString = rowitem.Key;
                Model.FactBase.LoadFromFactString(fact);

                _Attribute(row, "factkey", rowitem.Key);

                var cells = _Select("td", row);
                cells.forEach(function (cellelement, index) {
                    var cellfactstring = _Attribute(cellelement,"factstring");
                    cellfactstring = Replace(cellfactstring.trim(), ",", "");
                    if (!IsNull(cellfactstring)) {
                        var dim = fact.Dimensions.AsLinq<Model.Dimension>().FirstOrDefault(i=> i.DomainMemberFullName.indexOf(cellfactstring) == 0);
                        if (dim != null) {
                            var text = dim.DomainMember;
                            _Text(cellelement, text);
                        }
                    }

                });
            });
            me.AddRow("newrow", false, 0);


        }

        public GetDynamicRowID(cellid:string, fact: Model.FactBase):string
        {
            var me = this;
            var newrowneeded = false;
            var rownum:number = 1;
            var row: Element = null;
            var factkey: string = "";
            if (fact != null) {
                factkey = fact.GetFactString();
                var rows = _Select("tr", _Parent(me.TemplateRow));// $("tr", me.$TemplateRow.parent());
                row = _SelectFirst("tr[factkey='" + factkey + "']");
            }

            if (fact != null)
            {
               _Attribute(row, "factkey", factkey);

               var cells = _Select("td", row);
               cells.forEach(function (cellelement, index) {
                    var cellfactstring = _Attribute(cellelement,"factstring");
                    cellfactstring = Replace(cellfactstring.trim(), ",", "");
                    if (!IsNull(cellfactstring)) {
                        var dim = fact.Dimensions.AsLinq<Model.Dimension>().FirstOrDefault(i=> i.DomainMemberFullName.indexOf(cellfactstring) == 0);
                        if (dim != null) {
                            var text = dim.DomainMember;
                            _Text(cellelement,text);
                        }
                    }

                });
            }

            return _Attribute(row,"id");
            
        }

        public AddRow(rowclass: string, beforelast: boolean, rownum: number): Element
        {
            var me = this;
            var id = Format("R{0}", rownum);
            var newrow: Element = null;
            if (!IsNull(me.TemplateRow)) {
                newrow = _Clone(me.TemplateRow);
                _Html(_Find(me.TemplateRow, "td"), "");
                var lastrow = _FindFirst(_Parent(me.TemplateRow), "tr:last");
                if (beforelast) {
                    _Before(lastrow, newrow);
                }
                else {
                    _After(lastrow, newrow);
                }

                _Attribute(newrow, "id", id);
                _AddClass(newrow, "dynamicdata");
                _RemoveClass(newrow, "dynamic");

                me.SetCellID(newrow);

                if (!beforelast) {
                    _Html(_Find(newrow, ".title"), "new row");
                }
            }
            return newrow;
        }

        private SetCellID(row: Element)
        {
            var cells = _Select("td", row);// $("td", $row);
            var rowid =_Attribute( row,"id");
            cells.forEach(function (cellelement, index) {
                var cellid = _Attribute(cellelement,"id");
                cellid = cellid.substring(cellid.indexOf("|"));
                cellid = rowid + cellid;
                _Attribute(cellelement, "id", cellid);
                _Attribute(cellelement, "title", cellid);
            });
        }
        
        public SetExtensionByCode(code: string)
        {
              if (this.Extensions.Count() > 0) {
                  var ext = this.Extensions.FirstOrDefault();
                if (!IsNull(code)) {
                    ext = this.Extensions.FirstOrDefault(i=> i.LabelCode == code);

                }
                if (!IsNull(ext)) {
                    this.LoadExtension(ext);
                }
            } 

        }


        public SetCells(item) {
            var cells = <Model.Cell[]>JSON.parse(item);
            this.LoadCells(cells);
        }

        public HashChanged() {
            ShowNotification("Navigation occured: " + window.location.hash);
            var me = this;
            var hash = window.location.hash;
            if (hash.length > 0) {
                if (hash[hash.length - 1] != ";") {
                    hash = hash + ";";
                }

                var reportid = TextBetween(hash, "report=", ";");
                var cellid = TextBetween(hash, "cell=", ";");
                var extcode = TextBetween(hash, "ext=", ";");

                if (extcode == "000") { extcode = "000"; }

                me.Current_CellID = cellid;
                if (me.Current_ExtensionCode != extcode) {
                    me.IsInstanceLoaded = false;
                }
                me.Current_ExtensionCode = extcode;
                if (me.Current_ReportID != reportid) {
                    me.Current_ReportID = reportid;
                    me.LoadTable(reportid);
                } else
                {
                    me.SetNavigation();

                }


            }

            var currentextensioncode = this.Current_ExtensionCode;
            //this.SetNavigation();
            //this.SetExtensionByCode(this.Current_ExtensionCode);  
            //this.HighlightCell();   
            //if (currentextensioncode != this.Current_ExtensionCode) {
            //    this.GetData();
            //}
     
        }
  
    }



} 
var Table: UI.Table = null;
function SetExtension(extjson:string) {
}

//function LoadInstance(instancejson:string) {
//    Table.LoadInstance(window[var_currentinstance]);

//}