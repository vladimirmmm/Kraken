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
        public Current_ExtensionCode: string = "";
        public Current_ReportID: string = "";
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
                    me.SaveInstance();
                    _Html(_SelectFirst("#ReportContainer"), data);
                    me.Current_ReportID = reportid;

                    me.SetExternals();
                    me.Load();
                    me.GetData();
                    var table = Model.Hierarchy.FirstOrDefault(app.taxonomycontainer.TableStructure,
                        i=> {
                            var id = i.Item.ID;
                            id = id.indexOf("<") > -1 ? id.substr(0, id.indexOf("<")) : id;
                            //console.log(Format("{0}=={1}", id, reportid));

                            return id == reportid
                        });
                    if (!IsNull(table) && !IsNull(table.Item)) {
                        _Html(_SelectFirst("#DetailTitle"), table.Item.Name);
                    }
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
            var me = this;
            var dynamiccells = this.Instance.DynamicReportCells[this.Current_ReportID];
            if (!IsNull(dynamiccells)) {
                if (!IsNull(dynamiccells.Extensions)) {
                    me.ExtensionsRoot.Children = dynamiccells.Extensions.Children;
                }
            }
            var extensions = me.ExtensionsRoot.Children;

            this.Extensions = extensions.AsLinq<Model.Hierarchy<Model.LayoutItem>>()
                .Select(i=> i.Item);

            var current_extension = me.ExtensionsRoot.Item;
            if (this.ExtensionsRoot.Children.length > 0)
            {
                current_extension = me.ExtensionsRoot.Children[0].Item;
            }
            me.LoadExtension(current_extension);
            //this.CurrentExtension = current_extension;

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
        private changes: Object = {};
        public Load()
        {
            var me = this;
            me.UITable = Factories.GetTablewithManager()
            me.UITable.OnCellChanged =
            (cell: Model.Cell, value: any) => {
                me.UITable.Manager.OnCellChanged();
            };
            me.UITable.LoadfromHtml(_SelectFirst("#ReportContainer > table.report"));
            
            this.LoadCellsFromHtml();

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
            Log("LoadExtension");
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
        public GetFactFor = (a, b) => null;

        public LoadInstance(instance: Model.Instance) {
            var me = this;
            me.SaveInstance();
            ShowNotification("Loading Instance to UI");
            if (IsNull(me.Instance)) {
                me.Instance = instance;
                if (IsNull(me.Instance.FactDictionary)) {

                    me.Instance.FactDictionary = new Model.InstanceFactDictionary();
                }
            }
            me.UITable.Manager.Clear(me.UITable);
            me.SetDynamicRows();
            me.SetExtensionByCode(me.Current_ExtensionCode);

            var c = 0;
            var cells = this.UITable.Cells; //this.Cells;
            Model.FactBase.LoadFromFactString(me.CurrentExtension);

            cells.forEach(function (cell, index) {
                if (!_HasClass(cell, "blocked")) {
                    var cellelement = cell.UIElement;
                    var cell_layoutid = _Attribute(cellelement, "id");
                    var cell_factstring = _Attribute(cellelement, "factstring");
                    var cellfb = new Model.FactBase();
                    cellfb.FactString = cell_factstring;//  cell.FactString;

                    var factstring = cell_factstring;

                    Model.FactBase.LoadFromFactString(cellfb);
                    Model.FactBase.Merge(cellfb, me.CurrentExtension, true);
                    Model.FactBase.RemoveDimensionsWithDefaultMemebr(cellfb);
                    factstring = cellfb.GetFactString();
          
                    if (!IsNull(factstring)) {
                      
                        var fact = me.GetFactFor(cellfb, cell_layoutid);
                        if (!IsNull(fact))
                        {
                            _Html(cellelement, fact.Value);
        
                            c++;   
                        } 
                        
                    }
                            
                }
            });

            var extensioncell = _SelectFirst(".report #Extension");
            var extensioneditorelement = _SelectFirst("#ExtensionEditor");
            var extensioneditorcontainerelement = _SelectFirst("#ExtensionEditor .editors");
            
            _AddEventHandler(extensioncell, "dblclick",() => {
                var html = me.GetExtensionEditor();
                _Html(extensioneditorcontainerelement, html);
                _Show(extensioneditorelement);
            });

            ShowNotification(Format("{0} cells were populated!", c));
            
        }
  
        public SaveInstance()
        {
            return null;
            var me = this;
            if (me.UITable == null) { return null;}
            var instance = me.Instance;

            ShowNotification("Saving Instance to UI");

            var c = 0;
            var cells = me.UITable.Cells; //this.Cells;
            var facts: Model.InstanceFact[] = [];
          
            cells.forEach(function (cell, index) {
                if (!_HasClass(cell, "blocked") && !IsNull(cell.ColID) && !IsNull(cell.RowID) ) {
                    var row = me.UITable.GetRowOfCell(cell);
                    var col = me.UITable.GetColOfCell(cell);
                    var celluielement = cell.UIElement;
                    var value = _Text(celluielement);
                    var dynamicfact: Model.InstanceFact = new Model.InstanceFact();
                    if (_HasClass(row.UIElement, "dynamicdata"))
                    {
                        var keys = "";
                        row.Cells.forEach(c=>
                        {
                            if (_HasClass(c.UIElement, "key"))
                            {
                                dynamicfact.FactString += _Attribute(c.UIElement, "factstring");
                                dynamicfact.FactString += _Value(c.UIElement);
                            }
                        });
                    }
                    if (_HasClass(col.UIElement, "dynamicdata")) {
                        var keys = "";
                        col.Cells.forEach(c=> {
                            if (_HasClass(c.UIElement, "key")) {
                                dynamicfact.FactString += _Attribute(c.UIElement, "factstring");
                                dynamicfact.FactString += _Value(c.UIElement);
                            }
                        });
                    }
                    var cellfact: Model.InstanceFact = new Model.InstanceFact();
                    cellfact.FactString = _Attribute(cell.UIElement, "factstring");
                    Model.FactBase.Merge(cellfact, dynamicfact, true);
                    Model.FactBase.Merge(cellfact, me.CurrentExtension, true);
                    cellfact.Value = value;

                    Model.Instance.SaveFact(me.Instance, cellfact);

                    

                }
            });

 
        }

        public SetDynamicRows()
        {
            var me = this;
            var cellobj = me.Cells[0];
            var url = window.location.pathname;
            var reportid = me.Current_ReportID;
            var extensioncode = IsNull(me.Current_ExtensionCode) ? this.ExtensionsRoot.Item.LabelCode : me.Current_ExtensionCode;

            var tbody = _SelectFirst("tbody", me.UITable.UIElement);

            var dynamicdatacontainer = me.Instance.DynamicReportCells[reportid];

            var rows = IsNull(dynamicdatacontainer) ? [] : GetProperties(dynamicdatacontainer.RowDictionary);
            var cols = IsNull(dynamicdatacontainer) ? [] : GetProperties(dynamicdatacontainer.ColDictionary);
            var exts = IsNull(dynamicdatacontainer) ? [] : GetProperties(dynamicdatacontainer.ExtDictionary);


            var templaterow = me.UITable.Manager.TemplateRow;
            var templatecol = me.UITable.Manager.TemplateColumn;

            if (!IsNull(templaterow)) {
                me.UITable.Manager.ClearDynamicItems(me.UITable);

                var templatefacts: Model.FactBase[] = [];
                //s
                me.UITable.CanManageRows = false;
                templaterow.Cells.forEach(function (cell, ix) {
                    var factstring = _Attribute(cell.UIElement, "factstring");
                    var fact = Model.FactBase.GetFactFromString(factstring);
                    templatefacts.push(fact);
                });
                rows.forEach(function (rowitem) {
                    var row = me.UITable.AddRow(-1, rowitem.Value);
                    me.SetDataCells(row, rowitem, templatefacts);
                   
                });
                //me.UITable.CanManageRows = true;
                me.UITable.Manager.ManageRows(me.UITable);


                //me.AddRow("newrow", false, 0);
            }
            if (!IsNull(templatecol)) {

                var templatefacts: Model.FactBase[] = [];
                //s
                me.UITable.CanManageColumns = false;
                templatecol.Cells.forEach(function (cell, ix) {
                    var factstring = _Attribute(cell.UIElement, "factstring");
                    var fact = Model.FactBase.GetFactFromString(factstring);
                    templatefacts.push(fact);
                });
                cols.forEach(function (colitem) {
                    var col = me.UITable.AddColumn(-1, colitem.Value);
                    //me.SetCellIDs(row, null);
                    me.SetDataCells(col, colitem, templatefacts);
                    
                });
                //me.UITable.CanManageColumns = true;
                me.UITable.Manager.ManageColumns(me.UITable);
            }
            if (exts.length > 0)
            {
                //var extitem = Model.Hierarchy.FirstOrDefault(me.ExtensionsRoot,
                //    i=> i.Item.LabelContent == me.CurrentExtension.LabelContent);
                //var extix = me.ExtensionsRoot.Children.indexOf(extitem);

                //var extdictitem = exts[extix];
                Log("SetExtension");

                exts.forEach((item,ix) => {
                   
                    var extitem = me.ExtensionsRoot.Children[ix];
                    var id = Format("Ext_{0}", item.Value);
                    //var labelcontent = Format("Extension {0}", item.Value);
                    extitem.Item.ID = id;
                    extitem.Item.Label = new Model.Label();
                    extitem.Item.LabelCode = item.Value;
                    //extitem.Item.LabelContent = labelcontent;
                    extitem.Item.Label.Code = item.Value;
                    //extitem.Item.Label.Content = labelcontent;
                    extitem.Item.FactString = item.Key;

                });
                me.LoadExtension(me.CurrentExtension);
                //me.CurrentExtension.FactString = extdictitem.Key;
            }

        }

        private SetDataCells(cellcontainer: Controls.CellContainer, ditem:General.KeyValue, templatefacts: Model.FactBase[])
        {
            var me = this;

            var containerfact = new Model.FactBase();
            containerfact.FactString = ditem.Key;
            Model.FactBase.LoadFromFactString(containerfact);
            //Dynamic Attempt 6
            Model.FactBase.Merge(containerfact, me.CurrentExtension);

            var containerfactdimensionsquery = containerfact.Dimensions.AsLinq<Model.Dimension>();
            var containerfactdict: Model.Dictionary<string> = {};
            _Attribute(cellcontainer.UIElement, "factstring", ditem.Key);

            //var cells = _Select("td", row.UIElement);
            cellcontainer.Cells.forEach(function (cell, ix) {
                var cellelement = cell.UIElement;
                var templatefact = templatefacts[ix];
                var iskey = _HasClass(cellelement, "key");
                if (iskey) {
                 
                        var celldimension = _Attribute(cell.UIElement, "factstring");
                        celldimension = celldimension.replace(",", "");
                    var dim = containerfactdimensionsquery.FirstOrDefault(
                        i=> Model.Dimension.GetDomainFullName(i) == celldimension
                        );
                    if (dim != null) {
                        var text = dim.DomainMember;
                        _Html(cellelement, text);
                    }
                }
                else {
                    var fact = new Model.FactBase();
                    Model.FactBase.Merge(fact, templatefact, true);
                    Model.FactBase.Merge(fact, containerfact, true);
                    var fs = fact.GetFactString();
                    _Attribute(cellelement, "factstring", fs);
                }

            });
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
        
        public SetExtensionByCode(code: string) {
            Log("SetExtensionByCode "+ code);
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
                if (!IsNull(reportid)) {
                    if (extcode == "000") { extcode = "000"; }

                    me.Current_CellID = cellid;
                    if (me.Current_ExtensionCode != extcode) {
                        me.IsInstanceLoaded = false;

                    }
                    me.Current_ExtensionCode = extcode;
                    if (me.Current_ReportID != reportid) {
                        me.Current_ReportID = reportid;
                        me.LoadTable(reportid);
                    } else {
                        me.SetNavigation();

                    }
                }
        

            }

            var currentextensioncode = this.Current_ExtensionCode;
   
     
        }
  
        public GetExtensionEditor(): string
        {
            var me = this;
            var html = "";
            //return html;
            var extensiontemplate = me.CurrentExtension;
            extensiontemplate.Dimensions.forEach((dim) => {
                var name = Model.Dimension.GetDomainFullName(dim);
                var label = Model.Dimension.DomainMemberFullName(dim);
                html += "<label for='" + name + "'>" + label + "</label>\n";
                if (IsNull(dim.DomainMember) || dim.IsTyped)
                {               
                    var membervalue = IsNull(dim.DomainMember) ? "" : dim.DomainMember;
                    html += "<input type='text' fact='" + name + "' value='" + membervalue+"'/>\n";
                    if (dim.IsTyped) {

                    } else {

                    }
                }
                html += "<br/>\n";

            });
            return html;
        }

        public SaveExtension() {
            var me = this;
            var extensiontemplate = me.CurrentExtension;
            extensiontemplate.Dimensions.forEach((dim) => {
                var domainfullname = Model.Dimension.GetDomainFullName(dim);
                dim.DomainMember = _Value(_SelectFirst("input[fact='" + domainfullname+"']"));
            });
        }

    }



} 

function SetExtension(extjson:string) {
}

