/// <reference path="Interfaces.ts" />
/// <reference path="Utils.ts" />
/// <reference path="JqueryUtils.ts" />
/// <reference path="Models.ts" />
/// <reference path="Linq.ts" />
/// <reference path="TableManager.ts" />
/// <reference path="Binding.ts" />


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
        public UITable: Controls.Table = null;


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

                ShowNotification("Getting Html");

                AjaxRequest(me.HtmlTemplatePath, "get", "text/html", null, function (data) {

                    _Html(_SelectFirst("#ReportContainer"), data);

                    me.Current_ReportID = reportid;

                    me.SetExternals();
                    me.Load();
                    me.GetData();
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
            if (me.Instance != null && !me.IsInstanceLoaded)
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
            me.UITable = Factories.GetTablewithManager()
            me.UITable.LoadfromHtml(_SelectFirst("#ReportContainer > table.report"));
            
            _EnsureEventHandler(_Select("table.report tr"), "click", function () {
                _RemoveClass(_Select("table.report tr"), "selected");
                _AddClass(this, "selected");
            });
         
            this.LoadCellsFromHtml();
            //this.SetCellEditors();

            var hash = window.location.hash;

         
        }

        public HighlightCell()
        {
            _RemoveClass(_Select(".highlight"), "highlight");

            var cellselector = this.Current_CellID.replace(/_/g, "\\|").toUpperCase();
            var cells = _Select("#" + cellselector);
            _AddClass(cells, "highlight");
            _Focus(cells);
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
                var factstring = _Attribute(cellelement, "factstring");
                row = row.substring(1);
                col = col.substring(1);

                var cell: Model.Cell = new Model.Cell();
                cell.IsBlocked = _HasClass(cellelement,"blocked");
                if (!cell.IsBlocked) {
                    cell.Row = row;
                    cell.Column = col;         
                    cell.FactString = factstring;         
                    me.Cells.push(cell);
                }
                _Attribute(cellelement, "title", layoutid + "\r\n" + _Attribute(cellelement, "factstring"));
            });
   
      
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
                        if (!IsNull(me.Instance.FactDictionary[fact.FactString])) {
                            var x = 5;
                        }
                        me.Instance.FactDictionary[fact.FactString] = fact;
                    });
                }
            }

            me.SetDynamicRows();


            //if (!IsNull(me.FactMap)) {
                var c = 0;
                //var extfacts = me.FactMap[me.CurrentExtension.FactString];
                //if (extfacts != null) {
                this.Cells.forEach(function (cell, index) {
                    if (!cell.IsBlocked) {
                            
                        //if (cell.LayoutID in extfacts)
                        //{

                        var cellfb = new Model.FactBase();
                        cellfb.FactString = cell.FactString;
                        Model.FactBase.LoadFromFactString(cellfb);

                        Model.FactBase.Merge(cellfb, me.CurrentExtension);
                        var factstring = cellfb.GetFactString();

                        //var factstring = extfacts[cell.LayoutID];
                        if (!IsNull(factstring)) {
                            if (!(factstring in me.Instance.FactDictionary)) {
                                //Notify(cell.FactString + " not found in the instance");
                            } else {
                                var facts: Model.InstanceFact[] = me.Instance.FactDictionary[factstring];
                                if (facts.length == 1 && facts[0].FactKey == facts[0].FactString) {
                                    var fact = facts[0];
                                    if (!IsNull(fact)) {
                                        var selector = "#" + cell.LayoutID.replace("|", "\\|");
                                        var cellelement = _SelectFirst(selector);
                                        if (IsNull(cellelement)) {
                                            ShowNotification(Format("No cell found with selector {0}", selector));
                                        }
                                        else {
                                            _Html(cellelement, fact.Value);
                                        }
                                        c++;
                                        //set was here
                                    }
                                }
                                else {
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
                                        //var cellelement = _SelectFirst(selector);
                                        var cellelement = _SelectFirst(selector, _SelectFirst("#"+rowid));
                                        _Html(cellelement, fact.Value);
                                    });

                                }
                            }
                        }
                        //}
                            
                    }
                });
                //me.SetCellEditors();
                ShowNotification(Format("{0} cells were populated!", c));
                //}
            //}
        }

        public SetDynamicRows()
        {
            var me = this;
        
            //var datarows = _Select("tr.dynamicdata");
            //_Remove(datarows);

            var cellobj = me.Cells[0];
            var url = window.location.pathname;
            var reportid = me.Current_ReportID;

            var extensioncode = IsNull(me.Current_ExtensionCode) ? this.ExtensionsRoot.Item.LabelCode : me.Current_ExtensionCode;


            var reportkey = Format("{0}|{1}", reportid, extensioncode);

            var rowidcontainer = me.Instance.DynamicCellDictionary[reportkey];
            var rows = GetProperties(rowidcontainer);
            me.UITable.CanManageRows = false;
            rows.forEach(function (rowitem) { 
                var row = me.UITable.AddRow(-1);
       
                me.SetCellID(row.UIElement);

                var rowfact = new Model.FactBase();
                rowfact.FactString = rowitem.Key;
                Model.FactBase.LoadFromFactString(rowfact);

                _Attribute(row.UIElement, "factstring", rowitem.Key);

                var cells = _Select("td", row.UIElement);
                cells.forEach(function (cellelement, index) {
                    var cellfactstring = _Attribute(cellelement, "factstring");
                    var cfs = Replace(cellfactstring.trim(), ",", "");
                    if (!IsNull(cfs)) {
                        var dim = rowfact.Dimensions.AsLinq<Model.Dimension>().FirstOrDefault(i=> i.DomainMemberFullName.indexOf(cfs) == 0);
                        if (dim != null) {
                            var text = dim.DomainMember;
                            _Text(cellelement, text);
                        }
                        else
                        {
                            var fact = Model.FactBase.GetFactFromString(cellfactstring);
                            Model.FactBase.Merge(fact, rowfact, true);
                            var fs = fact.GetFactString();
                            _Attribute(cellelement, "factstring", fs);
                        }
                    }

                });
            });
            me.UITable.CanManageRows = true;
            me.UITable.Manager.ManageRows(me.UITable);
            //me.AddRow("newrow", false, 0);


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
                var rows = _Select("tr", _Parent(me.TemplateRow));
                row = _SelectFirst("tr[factstring='" + factkey + "']");
            }
            if (!IsNull(row))
            {
                return _Attribute(row, "id");
            }

            //if (fact != null)
            //{
            //    _Attribute(row, "factstring", factkey);

            //   var cells = _Select("td", row);
            //   cells.forEach(function (cellelement, index) {
            //        var cellfactstring = _Attribute(cellelement,"factstring");
            //        cellfactstring = Replace(cellfactstring.trim(), ",", "");
            //        if (!IsNull(cellfactstring)) {
            //            var dim = fact.Dimensions.AsLinq<Model.Dimension>().FirstOrDefault(i=> i.DomainMemberFullName.indexOf(cellfactstring) == 0);
            //            if (dim != null) {
            //                var text = dim.DomainMember;
            //                _Text(cellelement,text);
            //            }
            //        }

            //    });
            //}

            return "";
            
        }


        private SetCellID(row: Element)
        {
            var cells = _Select("td", row);
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