
module UI {

    export class Table {
        public Cells: Model.Cell[] = [];
        //public Extensions: Model.Hierarchy<Model.LayoutItem> = null;
        public ExtensionsRoot: Model.Hierarchy<Model.LayoutItem> = null;
        public Extensions: TSLinq.Linq<Model.LayoutItem> = null;
        public FactMap: Object = {};
        public CurrentExtension: Model.LayoutItem = null;
        private Instance: Model.Instance = null;
        public $TemplateRow: JQuery = null;

        public Concepts: Model.Concept[] = [];
        public Hierarchies: Model.Hierarchy<Model.QualifiedItem>[] = null;
        public ConceptValues: Model.ConceptLookUp[] = [];
        
        private Current_CellID: string = "";
        private Current_ExtensionCode: string = "";
        private IsInstanceLoaded: boolean = false;
        constructor()
        {
         
            this.SetExternals();
            this.SetNavigation();
            this.SetExtensionByCode(this.Current_ExtensionCode);
            this.Load();
            this.HighlightCell();
            this.GetData();
            ShowNotification("Table instance loaded!");
        }
 
        public SetExternals()
        {

            this.FactMap = window["FactMap"];
            this.ExtensionsRoot = window["Extensions"];
            this.Extensions = Model.Hierarchy.ToArray(this.ExtensionsRoot)
                .AsLinq<Model.LayoutItem>()
                .Where(i=> In(i.Category,
                Model.LayoutItemCategory.Rule,
                Model.LayoutItemCategory.BreakDown))
                .Select(i=> i);
     
        }

        public GetData()
        {
            var me = this;
            me.IsInstanceLoaded = false;
            var waiter: Waiter = new Waiter((i: RequestHandler) => i.succeded,() => { me.LoadConceptValues(); me.LoadToUI(); });

            //debugger;
            waiter.WaitFor(AjaxRequest("Instance/Get", "get", "json", null, function (data) {
                me.Instance = data;
                waiter.Check();
            }, function (error) { console.log(error); }));

            waiter.WaitFor(AjaxRequest("Taxonomy/Concepts", "get", "json", null, function (data) {
                me.Concepts = data
                //me.Concepts = me.Concepts.AsLinq<Model.Concept>().Where(i=> i.Domain != null).ToArray();

                waiter.Check();

            }, function (error) { console.log(error); }));
            waiter.WaitFor(AjaxRequest("Taxonomy/Hierarchies", "get", "json", null, function (data) {
                me.Hierarchies = data;
                waiter.Check();
            }, function (error) { console.log(error); }));

            waiter.Start();

        }

        public SetNavigation()
        {
            var hash = window.location.hash;
            if (hash.length > 0) {
                if (hash[hash.length - 1] != ";") {
                    hash = hash + ";";
                }

                var cellid = TextBetween(hash, "cell=", ";");
                var extcode = TextBetween(hash, "ext=", ";");
                if (extcode == "000") { extcode = "000"; }
                this.Current_CellID = cellid;
                this.Current_ExtensionCode = extcode;
            }
            else
            {
                this.Current_ExtensionCode = this.Extensions.FirstOrDefault().LabelCode;
            }
            var code = this.Current_ExtensionCode;
            this.CurrentExtension = this.Extensions.FirstOrDefault(i=> i.LabelCode == code);
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
            $(window).on('hashchange', function () {
                me.HashChanged();
            });
            $("table").on('click', 'tr', function () {
                $('tr', 'table').removeClass("selected");
                $(this).addClass("selected");
            });
            this.LoadCellsFromHtml();
            this.SetCellEditors();

            var hash = window.location.hash;

         
        }

        public HighlightCell()
        {
            $(".highlight").removeClass("highlight");

            var cellselector = this.Current_CellID.replace(/_/g, "\\|").toUpperCase();
            $("#" + cellselector).addClass("highlight");
            $("#" + cellselector).focus();
        }

        public LoadConceptValues() {
            var me = this;
            if (me.Hierarchies.length > 0 && me.Concepts.length > 0) {
       
                me.ConceptValues = [];
                var htemp = new Model.Hierarchy<Model.QualifiedItem>();
                me.Hierarchies.forEach(function (hierarchy) {
                    Model.QualifiedItem.Set(hierarchy.Item);

                });
                me.Concepts.forEach(function (concept) {
                    Model.QualifiedName.Set(concept);
                    Model.QualifiedName.Set(concept.Domain);
                    concept.Domain.Name = IsNull(concept.Domain.Name) ? concept.Domain.ID : concept.Domain.Name;
                    var hiers = me.Hierarchies.AsLinq<Model.Hierarchy<Model.QualifiedItem>>()
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
            $(cellselector).off("click");
            $(cellselector).each(function (ix, item) {
                var $target = $(item);
                var factitems = $target.attr("factstring").split(",");
                var concept = "";
                if (factitems[0].indexOf("[") == -1)
                {
                    concept = factitems[0];
                }
    
                if (!$target.parent().hasClass("dynamic") && !$target.hasClass("blocked")) {
             
                    $target.click(function () {
                        if (!$target.hasClass(Editor.editclass)) {
                            var typeclass = "";
                            if (factitems[0].indexOf(":ei") > -1) {
                                typeclass = "ei";
                            }
                            if (factitems[0].indexOf(":di") > -1) {
                                typeclass = "di";
                            }
                            var editor: Editor = null;
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

                            editor.Load($target,
                                () => $target.html(),
                                () => { $target.html(editor.ValueGetter(editor.$Me)); me.ManageRows(); }
                                );
                        }

                    });
                }
            });
        }

        private ManageRows()
        {
            var me = this;
            if (!IsNull(me.$TemplateRow)) {
                var $parentrow = me.$TemplateRow.parent();
                var $lastrow = $parentrow.find("tr:last");
                var $selectedrow = $parentrow.find("tr.selected");

                if ($selectedrow.length > 0 && $lastrow.length) {
            
                    if (me.IsAllNull($selectedrow) && $lastrow[0] != $selectedrow[0]) {
                        $selectedrow.remove();
                    }
                    if (!me.IsAllNull($lastrow)){

                        var $newrow = me.AddRow("newrow", false, 0);
                        me.SetCellEditors();
                    }

                }
            }
        }

        private IsAllNull($row: JQuery): boolean
        {
            var $cells = $("td", $row);
            var isallnull = true;
            $cells.each(function (ix, cell) {
                if (!IsNull($(cell).html().trim())) {
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
            $(".data").each(function (index, item) {
                var $cell = $(item);
                var layoutid: string = $cell.attr("id");
                var row = layoutid.split("|")[0];
                var col = layoutid.split("|")[1];
                row = row.substring(1);
                col = col.substring(1);

                var cell: Model.Cell = new Model.Cell();
                cell.IsBlocked = $cell.hasClass("blocked");
                if (!cell.IsBlocked) {
                    cell.Row = row;
                    cell.Column = col;         
                    me.Cells.push(cell);
                }
                $cell.attr("title", layoutid + "\r\n" + $cell.attr("factstring"));
            });
            var templaterow = $(".dynamic");
            if (templaterow.length > 0) {
                me.$TemplateRow = $(templaterow[0]);
                me.AddRow("newrow", false, 0);
            }
      
        }

        public LoadCells(cells: Model.Cell[])
        {
            this.Cells = cells;
        }

        public LoadExtension(li: Model.LayoutItem) {
            this.CurrentExtension = li;
            $("#Extension").html(Format("{0} <br /> {1}", li.LabelCode, li.LabelContent));
            $("#Extension").attr("title", li.FactString);
        }

        public LoadInstance(instance: Model.Instance) {
            var me = this;
            $("dynamicdata").remove();
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
                                                var selector = "#" + cell.LayoutID.replace("|","\\|");
                                                if ($(selector).length == 0) {
                                                    ShowNotification(Format("No cell found with selector {0}", selector));

                                                }
                                                c++;
                                                $(selector).html(fact.Value);
                                            }
                                        }
                                        else
                                        {
                                            //dynamic
                                           

                                            facts.forEach(function (factobj, index) {
                                                var fact = Model.InstanceFact.Convert(factobj);
                                                fact.Load();
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
                                                $(selector).html(fact.Value);
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
            var cellobj = me.Cells[0];
            var url = window.location.pathname;
            var reportname = url.substring(url.lastIndexOf('/') + 1);
            reportname = reportname.replace(".html", "");
            var extensioncode = IsNull(me.Current_ExtensionCode) ? this.ExtensionsRoot.Item.LabelCode : me.Current_ExtensionCode;


            var reportkey = Format("{0}|{1}", reportname, extensioncode);

            var rowidcontainer = me.Instance.DynamicCellDictionary[reportkey];
            var rows = GetProperties(rowidcontainer);
            rows.forEach(function (rowitem) { 
                var $row = me.AddRow("", true, rowitem.Value);
                var fact = new Model.FactBase();
                fact.FactString = rowitem.Key;
                Model.FactBase.LoadFromFactString(fact);

                $row.attr("factkey", rowitem.Key);

                var cells = $("td", $row);
                cells.each(function (index, cell) {
                    var $cell = $(cell);
                    var cellfactstring = $cell.attr("factstring");
                    cellfactstring = Replace(cellfactstring.trim(), ",", "");
                    if (!IsNull(cellfactstring)) {
                        var dim = fact.Dimensions.AsLinq<Model.Dimension>().FirstOrDefault(i=> i.DomainMemberFullName.indexOf(cellfactstring) == 0);
                        if (dim != null) {
                            var text = dim.DomainMember;
                            $cell.text(text);
                        }
                    }

                });
            });

        }

        public GetDynamicRowID(cellid:string, fact: Model.FactBase):string
        {
            var me = this;
            var newrowneeded = false;
            var rownum:number = 1;
            var $row: JQuery = null;
            var factkey: string = "";
            if (fact != null) {
                factkey = fact.GetFactString();
                var $rows = $("tr", me.$TemplateRow.parent());
                $row = $("tr[factkey='" + factkey + "']");
            }

            if (fact != null)
            {
                $row.attr("factkey", factkey);

                var cells = $("td", $row);
                cells.each(function (index, cell) {
                    var $cell = $(cell);
                    var cellfactstring = $cell.attr("factstring");
                    cellfactstring = Replace(cellfactstring.trim(), ",", "");
                    if (!IsNull(cellfactstring)) {
                        var dim = fact.Dimensions.AsLinq<Model.Dimension>().FirstOrDefault(i=> i.DomainMemberFullName.indexOf(cellfactstring) == 0);
                        if (dim != null) {
                            var text = dim.DomainMember;
                            $cell.text(text);
                        }
                    }

                });
            }

            return $row.attr("id");
            
        }

        public AddRow(rowclass: string, beforelast: boolean, rownum: number): JQuery
        {
            var me = this;
            var id = Format("R{0}", rownum);

            var $newrow = me.$TemplateRow.clone();
            me.$TemplateRow.find("td").html("");
            var $lastrow = me.$TemplateRow.parent().find("tr:last");
            if (beforelast) {
                $lastrow.before($newrow);
            }
            else {
                $lastrow.after($newrow);
            }

            $newrow.attr("id", id);
            $newrow.addClass("dynamicdata");
            $newrow.removeClass("dynamic");
         
            me.SetCellID($newrow);

            if (!beforelast) {
                $newrow.find(".title").html("new row");
            }
            var $rows = $("tr", me.$TemplateRow.parent());
   
            return $newrow;
        }

        private SetCellID($row: JQuery)
        {
            var cells = $("td", $row);
            var rowid = $row.attr("id");
            cells.each(function (index, cell) {
                var $cell = $(cell);
                var cellid = $cell.attr("id");
                cellid = cellid.substring(cellid.indexOf("|"));
                cellid = rowid + cellid;
                $cell.attr("id", cellid);
                $cell.attr("title", cellid);
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
            ShowNotification("Navigation occured: "+window.location.hash);
            var currentextensioncode = this.Current_ExtensionCode;
            this.SetNavigation();
            this.SetExtensionByCode(this.Current_ExtensionCode);  
            this.HighlightCell();   
            if (currentextensioncode != this.Current_ExtensionCode) {
                this.GetData();
            }
     
        }
  
    }



} 
var Table: UI.Table = null;
function SetExtension(extjson:string) {
}

//function LoadInstance(instancejson:string) {
//    Table.LoadInstance(window[var_currentinstance]);

//}