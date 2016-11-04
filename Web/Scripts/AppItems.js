var var_currentinstance = "currentinstance";
var var_currentinstancevalidationresults = "currentvalidationresults";
var var_tax_concepts = "tax_concepts";
var var_tax_facts = "tax_facts";
var var_tax_labels = "tax_labels";
var var_tax_validations = "tax_validations";
var var_tax_hierarchies = "tax_hierarchies";
var s_list_selector = ".list";
var s_listpager_selector = ".listpager";
var s_listfilter_selector = ".listfilter";
var s_sublist_selector = ".sublist";
var s_sublistpager_selector = ".sublistpager";
var s_detail_selector = ".detail";
var s_parent_selector = ".parent";
var s_contentcontainer_selector = ".contentcontainer";
var s_content_selector = ".subcontent";
var selectedclass = "selected";
var s_selectedclass = ".selected";
var StopProgress = function (id) {
    app.TaskManager.StopProgress(id);
};
var StartProgress = function (id) {
    app.TaskManager.StartProgress(id);
};
var ResultFormatter = function (rawdata) {
    return rawdata;
};
var requests = [];
function GetXbrlCellEditor(target) {
    var typeclass = "";
    var cell = app.taxonomycontainer.Table.UITable.GetCellByElement(target);
    var factstring = app.taxonomycontainer.Table.TaxonomyService.GetFactStringKey(cell.CurrentFactKey);
    //var factstring = _Attribute(target, "factstring");
    var factitems = factstring.split(",");
    var concept = "";
    if (factitems[0].indexOf("[") == -1) {
        concept = factitems[0];
    }
    if (concept.indexOf(":ei") > -1) {
        typeclass = "ei";
    }
    if (concept.indexOf(":bi") > -1) {
        typeclass = "bi";
    }
    if (concept.indexOf(":di") > -1) {
        typeclass = "di";
    }
    var editor = GetDefaultEditor(target);
    if (typeclass == "bi") {
        editor = new Editor('<select class="celleditor"><option>true</option><option>false</option></select>', function (i) { return i.val(); }, function (i, val) {
            i.val(val);
        });
    }
    if (typeclass == "ei") {
        editor = new Editor(Format('<select class="celleditor">{0}</select>', app.taxonomycontainer.GetConcepOptions(concept)), function (i) { return i.val(); }, function (i, val) {
            i.val(val);
        });
    }
    if (IsNull(concept)) {
        var fact = Model.FactBase.GetFactFromString(factstring);
        Log("NullConcept");
        if (!IsNull(fact.Dimensions) && fact.Dimensions.length == 1 && !fact.Dimensions[0].IsTyped) {
            Log("NullConcept >>");
            var role = _Attribute(target, "role");
            editor = new Editor(Format('<select class="celleditor">{0}</select>', app.taxonomycontainer.GetMembersOfHierarchy(role)), function (i) { return i.val(); }, function (i, val) {
                i.val(val);
            });
        }
    }
    if (typeclass == "di") {
        editor = new Editor('<input type="text" class="celleditor datepicker" value="" />', function (i) {
            return i.val();
        }, function (i, val) {
            i.datepicker({
                dateFormat: "yy-mm-dd",
                onSelect: function () {
                    editor.Save();
                }
            });
            i.val(val);
        });
        editor.CustomTrigger = function () {
        };
    }
    if (typeclass == "" && !IsNull(concept)) {
        editor = new Editor('<input type="text" class="celleditor " value="" />', function (i) { return i.val(); }, function (i, val) { return i.val(val); });
    }
    return editor;
}
function CreateMsg(category) {
    var msg = new General.Message();
    msg.Category = category;
    return msg;
}
function CreateNotificationMsg(message) {
    var msg = CreateMsg("notification");
    msg.Data = message;
    return msg;
}
function CreateAjaxMsg() {
    var msg = CreateMsg("ajax");
    return msg;
}
function CreateErrorMsg(errormessage) {
    var msg = CreateMsg("error");
    msg.Error = errormessage;
    return msg;
}
function ErrorHandler(errorMsg, url, lineNumber) {
    var errortext = errortag + 'UI Error: ' + errorMsg + ' Script: ' + url + ' Line: ' + lineNumber;
    Log(errortext);
    return true;
}
function ShowHideChild(selector, sender) {
    _Hide(_Select(selector));
    var item = _SelectFirst(selector, _Parent(sender));
    _Show(item);
}
function setStyle(cssText) {
    var $style = $("<style type='text/css'>").appendTo('head');
    $style.html(cssText);
}
;
function SetPivots() {
    var accentcolor = $(".accentColor").css("color");
    setStyle(".selected {color: #a2c139} .ui-state-active>a {color: #a2c139}");
    $("#resizablecol1").resizable({
        handles: 'e',
        alsoResize: "#colgroup1",
        minWidth: 18
    });
    //$("#colgroup2h").resizable({
    //    handles: 'e',
    //    alsoResize: "#colgroup2",
    //    minWidth: 18
    //});
    $("#LogWindow").resizable({
        handles: 'n',
        helper: "#resizable-helper",
        minHeight: 50,
        stop: function (event, ui) {
            $(this).css("width", '');
        }
    });
    $("#ConsoleWindow").tabs({
        /* show: { effect: "slide", direction: "right", duration: 200 }*/
        activate: function (event, ui) {
            if (ui.newPanel[0] == $("#tab_logs")[0]) {
                ActivateLogUI();
            }
        }
    });
    $("#DetailWindow").tabs({
        /* show: { effect: "slide", direction: "right", duration: 200 }*/
        activate: function (event, ui) {
            //if (ui.newPanel[0] == $("#tab_cell")[0]) {
            //    ActivateLogUI();
            //}
        }
    });
    app.Tabs_Main = $("#MainContainer").tabs({
        show: { effect: "slide", direction: "right", duration: 200 },
        beforeActivate: function (event, ui) {
        }
    });
    app.Tabs_Taxonomy = $("#TaxonomyContainer").tabs({
        show: { effect: "slide", direction: "right", duration: 200 },
        beforeActivate: function (event, ui) {
        }
    });
    app.Tabs_instance = $("#InstanceContainer").tabs({
        show: { effect: "slide", direction: "right", duration: 200 },
        beforeActivate: function (event, ui) {
        }
    });
    $("#contentlog").keydown(function (event) {
        if (event.ctrlKey && event.keyCode == 65) {
            console.log("Ctrl+A event captured!");
            event.preventDefault();
            $("#contentlog").selectText();
            $("#contentlog").focus();
        }
    });
    _AddEventHandler(window, "keyup", function (event) {
        if (event.keyCode == 46) {
            var focusedlement = $(':focus').length == 1 ? $(':focus')[0] : null;
            var contentlog = _SelectFirst("#contentlog");
            if (contentlog == focusedlement) {
                _Html(contentlog, "");
            }
        }
    });
}
function Notify(message) {
    ShowNotification(message);
}
function ShowNotification(message) {
    var msg = CreateNotificationMsg(message);
    Communication_ToApp(msg);
}
function ShowError(message) {
    var msg = CreateErrorMsg(message);
    Communication_ToApp(msg);
}
function Communication_ToApp(message) {
    var strdata = JSON.stringify(message);
    if (IsDesktop()) {
        window.external.Notify(strdata);
    }
    else {
        Ajax("Instance/Index", "get", { msg: message }, null, "json");
    }
}
function Communication_Listener(data) {
    var message = JSON.parse(data);
    MessageReceived(message);
}
function MessageReceived(message) {
    //Notify("Communication_Listener_Start");
    //Notify("Communication_Listener Parsed");
    if (message.Category == "ajax") {
        asyncFunc(function () {
            //Notify("Calling AjaxResponse");
            AjaxResponse(message);
        });
    }
    if (message.Category == "notfication") {
        Log(message.Data);
    }
    if (message.Category == "error") {
    }
    if (message.Category == "action") {
        message.Url = IsNull(message.Url) ? "" : message.Url;
        if (message.Url.toLowerCase() == "instance") {
            app.instancecontainer.HandleAction(message);
        }
        if (message.Data.toLowerCase() == "instanceloaded") {
            Log("Instance Loaded");
            app.LoadInstance();
        }
        if (message.Data.toLowerCase() == "taxonomyloaded") {
            Log("Taxonomy Loaded");
            app.LoadTaxonomy();
        }
    }
    if (message.Category == "debug") {
        debugger;
    }
    //Notify("Communication_Listener_End");
}
//if (typeof console === "undefined") {
function IsDesktop() {
    return 'Notify' in window.external;
}
if (IsDesktop()) {
    window.onerror = ErrorHandler;
}
//} 
//# sourceMappingURL=AppItems.js.map