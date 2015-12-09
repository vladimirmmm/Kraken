var var_currentinstance = "currentinstance";
var var_currentinstancevalidationresults = "currentvalidationresults";
var var_tax_concepts = "tax_concepts";
var var_tax_facts = "tax_facts";
var var_tax_labels = "tax_labels";
var var_tax_validations = "tax_validations";
var var_tax_hierarchies = "tax_hierarchies";

var s_list_selector: string = ".list";
var s_listpager_selector: string = ".listpager";
var s_listfilter_selector: string = ".listfilter";
var s_sublist_selector: string = ".sublist";
var s_sublistpager_selector: string = ".sublistpager";
var s_detail_selector: string = ".detail";
var s_parent_selector: string = ".parent";
var s_contentcontainer_selector: string = ".contentcontainer";
var s_content_selector: string = ".subcontent";

var selectedclass: string = "selected";
var s_selectedclass: string = ".selected";



var StopProgress: F_Progress = function (id: string) {
    app.TaskManager.StopProgress(id);
};
var StartProgress: F_Progress = function (id: string) {
    app.TaskManager.StartProgress(id);
};
var ResultFormatter: F_ResultFormatter = function (rawdata) { return rawdata };



var requests: General.KeyValue[] = [];


function GetXbrlCellEditor(target: Element) {
    var typeclass = "";

    var factitems = _Attribute(target, "factstring").split(",");
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
    var editor: Editor = null;
    if (typeclass == "bi") {
        editor = new Editor('<select class="celleditor"><option>true</option><option>false</option></select>',
            (i: JQuery) => i.val(),
            (i: JQuery, val: any) => { i.val(val); });

    }
    if (typeclass == "ei") {
        editor = new Editor(Format('<select class="celleditor">{0}</select>', app.taxonomycontainer.GetConcepOptions(concept)),
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
    if (typeclass == "") {
        editor = new Editor('<input type="text" class="celleditor " value="" />',
            (i: JQuery) => i.val(),
            (i: JQuery, val: any) => i.val(val));
    }
    return editor;
}


function CreateMsg(category: string): General.Message {
    var msg = new General.Message();
    msg.Category = category;
    return msg;
}

function CreateNotificationMsg(message: string): General.Message {
    var msg = CreateMsg("notification");
    msg.Data = message;
    return msg
}

function CreateAjaxMsg(): General.Message {
    var msg = CreateMsg("ajax");
    return msg
}

function CreateErrorMsg(errormessage: string): General.Message {
    var msg = CreateMsg("error");
    msg.Error = errormessage;
    return msg;
}

function ErrorHandler(errorMsg, url, lineNumber) {
    var errortext = 'UI Error: ' + errorMsg + ' Script: ' + url + ' Line: ' + lineNumber;
    Log(errortext);
    return true;
}

function ShowHideChild(selector: any, sender: any) {
    _Hide(_Select(selector));
    var item = _SelectFirst(selector, _Parent(sender));
    _Show(item);
}
function setStyle(cssText): any {
    var $style = $("<style type='text/css'>").appendTo('head'); 
    $style.html(cssText);
};

function SetPivots() {
    var accentcolor = $(".accentColor").css("color");
    setStyle(".selected {color: #a2c139} .ui-state-active>a {color: #a2c139}");

    $("#colgroup1h").resizable({
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
        minHeight: 50
    });

    $("#LogWindow").tabs({
       /* show: { effect: "slide", direction: "right", duration: 200 }*/

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
    app.Tabs_instance =  $("#InstanceContainer").tabs({
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
    _AddEventHandler(window,"keyup", function (event) {

        if (event.keyCode == 46) {
            var focusedlement = $(':focus').length == 1 ? $(':focus')[0] : null;
            var contentlog = _SelectFirst("#contentlog");
            if (contentlog == focusedlement) {
                _Html(contentlog, "");
            }

        }
    });



}

function Notify(message: string) {
    ShowNotification(message);
}

function ShowNotification(message: string) {
    var msg = CreateNotificationMsg(message);
    Communication_ToApp(msg);
}

function ShowError(message: string) {
    var msg = CreateErrorMsg(message);
    Communication_ToApp(msg);

}

function Communication_ToApp(message: General.Message) {
    var strdata = JSON.stringify(message);
    if (IsDesktop()) {
        window.external.Notify(strdata);
    } else {
        console.log(strdata);
    }
}

function Communication_Listener(data: string) {
    var message: General.Message = <General.Message>JSON.parse(data);
    MessageReceived(message);
}
function MessageReceived(message: General.Message) {
    //Notify("Communication_Listener_Start");

    //Notify("Communication_Listener Parsed");

    if (message.Category == "ajax") {
        asyncFunc(() => {
            //Notify("Calling AjaxResponse");
            AjaxResponse(message);
        });
        //clearobject(message);
        //AjaxResponse(message);
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
            app.Load();
        }
        if (message.Data.toLowerCase() == "taxonomyloaded") {
            app.Load();

        }
    }
    if (message.Category == "debug") {
        debugger;
    }
    //Notify("Communication_Listener_End");
}

//if (typeof console === "undefined") {



function IsDesktop()
{
    return 'Notify' in window.external;
}

if (IsDesktop())
{
    window.onerror = ErrorHandler;
}

//}