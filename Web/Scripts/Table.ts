﻿/// <reference path="Interfaces.ts" />
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
 
        public SetExternals() {
            var extensions = this.ExtensionsRoot.Children;
            var dynamiccells = this.Instance.DynamicReportCells[this.Current_ReportID];
            if (!IsNull(dynamiccells)) {
                if (!IsNull(dynamiccells.Extensions)) {
                    extensions = dynamiccells.Extensions.Children;
                }
            }

            this.Extensions = extensions.AsLinq<Model.Hierarchy<Model.LayoutItem>>()
                .Select(i=> i.Item);


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
                var firstextension = this.Extensions.FirstOrDefault();
                if (!IsNull(firstextension)) {
                    this.Current_ExtensionCode = this.Extensions.FirstOrDefault().LabelCode;
                }
            }
            me.SetExtensionByCode(this.Current_ExtensionCode);
            me.LoadToUI();
            me.HighlightCell();

        }

        public LoadToUI()
        {
            var me = this;
            if (!this.IsInstanceLoaded) {
                this.LoadInstance(this.Instance);
                this.IsInstanceLoaded = true;
            }
  
        }
  
        public Load()
        {
            var me = this;
            me.UITable = Factories.GetTablewithManager()
            me.UITable.LoadfromHtml(_SelectFirst("#ReportContainer > table.report"));
            
            _EnsureEventHandler(_Select("table.report tr"), "click", function () {
                _RemoveClass(_Select("table.report tr"), selectedclass);
                _AddClass(this, selectedclass);
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
            var typeddimensionsofext = this.CurrentExtension.Dimensions.AsLinq<Model.Dimension>().Where(i=> i.IsTyped).ToArray();
            this.Cells.forEach(function (cell, ix) {
                //var 
                //cell.FactString
            });
            _Html(extensionscell, Format("{0} <br /> {1}", li.LabelCode, li.LabelContent));
            
            _Attribute(extensionscell,"title", li.FactString);
        }

        public LoadInstance(instance: Model.Instance) {
            var me = this;
            ShowNotification("Loading Instance to UI");
            if (IsNull(me.Instance)) {
                me.Instance = instance;
                if (IsNull(me.Instance.FactDictionary)) {

                    me.Instance.FactDictionary = {};     
                }
            }
            me.UITable.Manager.Clear(me.UITable);
            me.SetDynamicRows();

            var c = 0;
            var cells = this.UITable.Cells; //this.Cells;
            cells.forEach(function (cell, index) {
                if (!_HasClass(cell, "blocked")) {
                    var cellelement = cell.UIElement;
                    var cell_layoutid = _Attribute(cellelement, "id");
                    var cell_factstring = _Attribute(cellelement, "factstring");
                    var cellfb = new Model.FactBase();
                    cellfb.FactString = cell_factstring;//  cell.FactString;
                    //Dynamic Attempt 6
                    /*
                    Model.FactBase.LoadFromFactString(cellfb);
                    Model.FactBase.Merge(cellfb, me.CurrentExtension);
                    var factstring = cellfb.GetFactString();
                    */
                    var factstring = cell_factstring;
                    if (me.UITable.Manager.TemplateRow == null)
                    {
                        Model.FactBase.LoadFromFactString(cellfb);
                        Model.FactBase.Merge(cellfb, me.CurrentExtension);
                        factstring = cellfb.GetFactString();
                    }
          
                    if (!IsNull(factstring)) {
                        var fact = Model.Instance.GetFactFor(me.Instance, cellfb, cell_layoutid);
                        if (!IsNull(fact))
                        {
                            _Html(cellelement, fact.Value);
        
                            c++;   
                        } 
                        
                    }
                            
                }
            });

            ShowNotification(Format("{0} cells were populated!", c));
  
        }

        public SetDynamicRows()
        {
            var me = this;
            var templaterow = me.UITable.Manager.TemplateRow;
            if (!IsNull(templaterow)) {
                var uirows = me.UITable.Rows.AsLinq<Controls.Row>().Where(i=> _HasClass(i.UIElement, "dynamicdata")).Select(i=> i.RowID).ToArray();
                me.UITable.CanManageRows = false;
                uirows.forEach(function (rowid, ix) {
                    var count = uirows.length;

                    me.UITable.RemoveRowByID(rowid);
                });
                me.UITable.CanManageRows = true;

                var cellobj = me.Cells[0];
                var url = window.location.pathname;
                var reportid = me.Current_ReportID;

                var extensioncode = IsNull(me.Current_ExtensionCode) ? this.ExtensionsRoot.Item.LabelCode : me.Current_ExtensionCode;

                var rowidcontainer = me.Instance.DynamicReportCells[reportid];
                var rows = IsNull(rowidcontainer) ? [] : GetProperties(rowidcontainer.RowDictionary);

                me.UITable.CanManageRows = false;

                var templatefactstrings: string[] = [];
                templaterow.Cells.forEach(function (cell, ix) {
                    var factstring = _Attribute(cell.UIElement, "factstring");
                    templatefactstrings.push(factstring);
                });
                rows.forEach(function (rowitem) {
                    var row = me.UITable.AddRow(-1, rowitem.Value);
                    //row.RowID = rowitem.Value;
                    me.SetCellIDs(row);

                    var rowfact = new Model.FactBase();
                    rowfact.FactString = rowitem.Key;
                    Model.FactBase.LoadFromFactString(rowfact);
                    //Dynamic Attempt 6
                    Model.FactBase.Merge(rowfact, me.CurrentExtension);

                    var rowfactdimensionsquery = rowfact.Dimensions.AsLinq<Model.Dimension>();

                    _Attribute(row.UIElement, "factstring", rowitem.Key);

                    var cells = _Select("td", row.UIElement);
                    cells.forEach(function (cellelement, ix) {
                        var cellfactstring = templatefactstrings[ix];// _Attribute(cellelement, "factstring");
                  
                        var cfs = Replace(cellfactstring.trim(), ",", "");
                        if (!IsNull(cfs)) {
                            var dim = rowfactdimensionsquery.FirstOrDefault(i=> Model.Dimension.DomainMemberFullName(i).indexOf(cfs) == 0);
                            if (dim != null) {
                                var text = dim.DomainMember;
                                _Text(cellelement, text);
                            }
                            else {
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

            return "";
            
        }

        private SetCellIDs(row: Controls.Row) {
 
            var rowid = _Attribute(row.UIElement, "id");
            row.Cells.forEach(function (cell, index) {
                var cellid = _Attribute(cell.UIElement, "id");
                cellid = cellid.substring(cellid.indexOf("|"));
                cellid = rowid + cellid;
                _Attribute(cell.UIElement, "id", cellid);
                //_Attribute(cell.UIElement, "title", cellid);
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
   
     
        }
  
    }



} 
//var UITable: UI.Table = null;
function SetExtension(extjson:string) {
}

//function LoadInstance(instancejson:string) {
//    Table.LoadInstance(window[var_currentinstance]);

//}