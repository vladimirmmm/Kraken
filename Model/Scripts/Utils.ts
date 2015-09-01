
interface cbdelegate { (result: any): any; }
interface Dictionary { [s: string]: Object; }
interface F_Progress { (id: string): any; }
interface F_ResultFormatter { (rawdata: any): any; }

interface JQueryStatic {
    formatDateTime(format: string, d: Date);
    w8n(...any);

}
interface JQuery
{
    w8n(...any);
    serializeObject(...any);
    padding(direction: string): number;
    pagination(total: any, options: any);
    resizable(options?: any);
    colResizable(options?: any);
    resizableColumns(options?: any);
}

interface External {
    Notify(obj: any)
}
function CreateMsg(category: string): General.Message {
    var msg = new General.Message();
    msg.Category = category;
    return msg;
}
function CreateNotificationMsg(message: string): General.Message
{
    var msg = CreateMsg("notification");
    msg.Data = message;
    return msg
}
function CreateAjaxMsg(): General.Message {
    var msg = CreateMsg("ajax");
    return msg
}
function CreateErrorMsg(errormessage: string):General.Message
{
    var msg = CreateMsg("error");
    msg.Error = errormessage;
    return msg;
}
function ErrorHandler(errorMsg, url, lineNumber) {
    var errortext = 'Error: ' + errorMsg + ' Script: ' + url + ' Line: ' + lineNumber;
    Error(errortext);
    return true;
}
function ShowHideChild(selector: any,sender:any)
{
    $(selector).hide();
    var $item = $(selector, $(sender).parent()).first();
    $item.show();
}
function SetPivots()
{
    
    $("#maintable").resizableColumns();
    //$("#maintable").colResizable({
    //    liveDrag: false,
    //});
    //$("#pivot").splitPane({
    //    type: "v",
    //    outline: true,
    //    minLeft: 100, sizeLeft: 150, minRight: 100,
    //    resizeToWidth: true,
    //    cookie: "vsplitter",
    //    accessKey: 'I'
    //});
    /*
    $(".pivotitem").click(function () {
        Activate($(this));
    });
    var iframe = $($("#tableframe")[0]["contentWindow"].document);
    $(iframe).click(function () {
        Activate($("#tableframe").parent());
    });
    */
}

var waitForFinalEvent = (function () {
    var timers = {};
    return function (callback, ms, uniqueId) {
        if (!uniqueId) {
            uniqueId = "Don't call this twice without a uniqueId";
        }
        if (timers[uniqueId]) {
            clearTimeout(timers[uniqueId]);
        }
        timers[uniqueId] = setTimeout(callback, ms);
    };
})();

function Activate(jitem: JQuery)
{
    var $item = jitem;
    var $parent = $item.parent();
    var previouswidth = 0;
    var minsize = 200;
    var margin = 0;
    var previouswidth = 0;
    $parent.children().each(function (ix, item) {
        var current: JQuery = $(item);
        var currentwidth = current.width();
        margin = previouswidth == 0 ? 0 : minsize - previouswidth;
        if (current[0] == $item[0]) {
            //$item.animate({ "margin-left": (-200)+"px" }, { duration: this.duration, queue: false });
            current.css("z-index", "1");
        }
        else {
            current.css("z-index", "0");
        }
        current.css("margin-left", margin + "px");

        previouswidth = currentwidth; 
    });


       // $("#ListController").parent().animate({ "max-width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });
        //$("#SaveController").parent().animate({ "width": this.min_width + "px" }, { duration: this.duration, queue: false });

 
}
//Notify("typeof console " + typeof console);
//if (typeof console === "undefined") {

    window.onerror = ErrorHandler;
//}




function LoadJS(path:string)
{
    var fileref = document.createElement('script')
    fileref.setAttribute("type", "text/javascript")
    fileref.setAttribute("src", path)

}
function GetFunctionBody(f: Function):string
{
    var result = "";
    var entire = f.toString();
    var body = entire.slice(entire.indexOf("{") + 1, entire.lastIndexOf("}"));
    return result;
}

function GetReturnStatement(f: Function): string
{
    var body = GetFunctionBody(f);
    var body = body.substring(body.lastIndexOf("return "));
    return body.substring(body.indexOf(" ") + 1);

}

function GetMemberExpression(f: Function): string
{
    return "";
}
function StringEquals(s1: any, s2: any): boolean
{

    if (typeof s1 == "string" && typeof s2 == "string")
    {
        return s1.toString().toLowerCase() == s2.toString().toLowerCase();
    }
    return false;
}
function ToObjectX(items: Dictionary): Object {
    var obj = {};
    for (var key in items) {
        if (items.hasOwnProperty(key)) {
            var kvobj = <General.KeyValue>items[key]; //
            if (typeof (kvobj)=="object" && "Key" in kvobj) {
                if (!IsNull(kvobj.Key)) {
                    obj[kvobj.Key] = kvobj.Value;
                }
            } else
            {
                obj[key] = kvobj;
            }
        }
    };
    return obj;
}

function ToObject(items: General.KeyValue[]): Object
{
    var obj = {};
    items.forEach(function (item)
    {
        obj[item.Key] = item.Value;
    });
    return obj;
}

module General {
    export class KeyValue {
        public Key: string = "";
        public Value: any = null;
    }

    export class Message
    {
        public Id: string;
        public Url: string;
        public Category: string;
        public Parameters: Object = {};
        public ContentType: string;
        public Error: string
        public Data:string
    }

}

interface RequestHandler
{
    success: Function[];
    error: Function[];
    Id: string;
    succeded: boolean;
    /*
    url: string;
    contenttype: string;
    */
}

class Waiter
{
    public Items: Object[] = [];
    public Condition: Function = null;
    public AllCompleted: Function = null;
    private IsStarted: boolean = false;

    constructor(Condition: Function, AllCompleted: Function)
    {
        this.AllCompleted = AllCompleted;
        this.Condition = Condition;
    }
    public Check()
    {
        var me = this;
        var result = true;
        this.Items.forEach(function (Item) {
            if (!me.Condition(Item))
            {
                result = false;
            }
        });
        if (result && me.IsStarted)
        {
            me.Stop();
            me.AllCompleted();
            me.Items = [];

        }
    }
    public WaitFor(Item:Object)
    {
        var me = this;
        me.Items.push(Item);
    }
    public Start()
    {
        this.IsStarted = true;
        this.Check();
    }
    public Stop() {
        this.IsStarted = false;
    }
}

var StopProgress: F_Progress = function (id: string) { return null; };
var StartProgress: F_Progress = function (id: string) { return null; };
var ResultFormatter: F_ResultFormatter = function (rawdata) { return rawdata };


var requests: General.KeyValue[] = []; 
try {
    if (!IsNull(parent["requests"])){
        requests = parent["requests"];
    }
}
catch (err) {
    
}
function ShowContent(selector: string, sender:any)
{
    var me = this;
    var $parent = $(sender).parents(s_contentcontainer_selector).first();
    $parent.children(s_content_selector).hide();
    var $contenttoshow = $parent.children(selector)
    if ($contenttoshow.length == 0)
    {
        ShowError("ShowContent: " + $contenttoshow.selector + " has not items!");
    }
    $contenttoshow.show();


}

function LoadPage($bindtarget: JQuery, $pager: JQuery, items: any[], page: number, pagesize: number, events?: Object)
{
    var me = this;
    var startix = pagesize * page;
    var endix = startix + pagesize;
    var itemspart = items.slice(startix, endix);
    BindX($bindtarget, itemspart);
    if ($pager.length == 0 || 1 == 1) {
        $pager.pagination(items.length,
            {
                items_per_page: pagesize,
                current_page: page ? page : 0,
                link_to: "",
                prev_text: "Prev",
                next_text: "Next",
                ellipse_text: "...",
                prev_show_always: true,
                next_show_always: true,
                callback: function (pageix) {
                    CallFunction(events, "onpaging");
                    LoadPage($bindtarget, $pager, items, pageix, pagesize, events);
                    CallFunction(events, "onpaged");

                },
            });
    } else
    {
        //console.log("")
    }

}

function CallFunction(eventcontainer: Object, eventname: string, args?: any[])
{
    if (!IsNull(eventcontainer))
    {
        if (eventname in eventcontainer && IsFunction(eventcontainer[eventname]))
        {
            eventcontainer[eventname](args);
        }
    }
}

function CallFunctionVariable(func: Function, args?: any[])
{
    if (!IsNull(func) && IsFunction(func)) {
        func(args);
    }
    
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
function Communication_ToApp(message: General.Message)
{
    var strdata = JSON.stringify(message);
    if ('Notify' in window.external) {
        window.external.Notify(strdata);
    } else {
        console.log(strdata);
    }
}

function Communication_Listener(data: string) {
    var message: General.Message = <General.Message>JSON.parse(data);
    if (message.Category == "ajax") {
        AjaxResponse(message);
    }
    if (message.Category == "notfication")
    {

    }
    if (message.Category == "error") {

    }
    if (message.Category == "action") {
        if (message.Url.toLowerCase() == "instance")
        {
            app.instancecontainer.HandleAction(message);
        }
    }
    if (message.Category == "debug") {
        debugger;
    }
}

function AjaxRequest(url: string, method: string, contenttype: string, parameters: Dictionary, success: Function, error: Function): RequestHandler {

    return AjaxRequestComplex(url, method, contenttype, parameters, [success], [error]);
}

function AjaxRequestComplex(url: string, method: string, contenttype: string, parameters: Dictionary, success: [Function], error: [Function]): RequestHandler {
 

    var requestid = Guid();
    var requesthandler = <RequestHandler>{ error: error, success: success, Id: requestid, succeded: false};
    var kv = new General.KeyValue();
    kv.Key = requestid;
    kv.Value = requesthandler;
    requests.push(kv);
    //var notification = { url: url, parameters: parameters, requestid: requestid, contenttype: contenttype };
    var msg = CreateAjaxMsg();
    msg.Url = url;
    msg.Parameters = parameters;
    msg.Id = requestid;
    msg.ContentType = contenttype;
    Communication_ToApp(msg);
    return requesthandler;

} 

function AjaxResponse(message: General.Message)
{
    

    var request = requests.AsLinq<General.KeyValue>().FirstOrDefault(i=> i.Key == message.Id);
    if (request != null) {
        var requesthandler = <RequestHandler>request.Value;
        var stringdata = message.Data;
        var response = stringdata;
        if (message.ContentType.indexOf("json") > -1) {
            if (!IsNull(stringdata)) {
                response = JSON.parse(stringdata);
            }
        }

        var ix = requests.indexOf(request);
        if (ix > -1) {
            requests.splice(ix, 1);
        }

        if (IsNull(message.Error)) {
            requesthandler.succeded = true;
            requesthandler.success.forEach(function (func: Function) {
                func(response, requesthandler);
            });
        }
        else {
            requesthandler.error.forEach(function (func: Function) {
                func(response);
            });
            ShowError("Response Error: " + response);
        }
    } else
    {
        ShowError("Request not found! " + message.Id);
    }
}

function Ajax(url:string, method:string, parameters:Dictionary, generichandler:Function, contentType?:string) {
    var result = {}; //new Engine.InfoContainer();
    var _contentType = "text/html";
    var _dataType = "";
    var callback: cbdelegate = function (result: any) { return false; };
    var S_Callback = "callback";
    if (!IsNull(parameters) && parameters[S_Callback] instanceof Function) {
        callback = <cbdelegate>parameters[S_Callback];
    }
    if (contentType == "json") {
        _contentType = "application/json; charset=UTF-8";
        _dataType = "json";
    }
    var params:Object = parameters;
    if (method.toLowerCase() == "get") {
        params = ToObjectX(parameters); //Clone(parameters);
    }
    if (method.toLowerCase() == "post") {
        params = JSON.stringify(parameters);
    }
    StartProgress("ajax");
    //_App.ProgressManager.StartProgress("ajax");
    $.ajax({
        url: GetBaseURL() + url,
        contentType: _contentType,
        dataType: _dataType,
        type: method,
        data: params,
        cache: false,
        success: function (data) {
            StopProgress("ajax");
            var Id = this.url.toString();
            result = ResultFormatter(data);
            console.log(Format("Request succeeded - {0}", Id));
            generichandler(result);
            callback(result);
        },
        error: function (exception) {
            StopProgress("ajax");
            var Id = this.url.toString();
            var errorobj = GetErrorObj(exception, this.contentType)
            var errormsg = Format("Request failed: {0}", errorobj.message);
            //errormsg += Format("\nurl: {0}", Id) + "\n" + errorobj.stacktrace;
            actioncenter.AddError(errormsg);
            SetProperty(result, "Error", exception);
            generichandler(result);

        }
    });
}

function GetErrorObj(exception, contenttype?:string) {
    var exceptiontext = "responseJSON" in exception ? exception["responseJSON"] : "";
    var stacktrace = "";
    var errorobj = { message: "", stacktrace: "" };
    if (contenttype == "text/html")
    {
        errorobj.message = $(exception["responseText"]).text();
        return errorobj;
    }
    if ("responseJSON" in exception) {
        if (typeof (exception["responseJSON"]) == "object") {
            exceptiontext = exception["responseJSON"].Message;
            stacktrace = exception["responseJSON"].StackTrace;
        } else
        {
            exceptiontext = exception["responseJSON"];
        }
    }
    else {
        if ("responseText" in exception) {
            exceptiontext = Truncate(HtmlEncode(exception["responseText"]), 400);
        }
    }
    errorobj = { message: exceptiontext, stacktrace: stacktrace };
    return errorobj;
}

function SetProperty(target:Object, name:string, value:any)
{
    if (!IsNull(target)) {
        target[name] = value;
    }

}
function GetBaseURL() {
    var url = window.location.href.split('/');
    var baseUrl = url[0] + '//' + url[2] + '/';
    if (url[3].length == 5) {
        if (url[3].match(/[a-z]{2}-[a-z]{2}/gi)) {
            baseUrl = baseUrl + url[3] + '/';
        }
    }
    return baseUrl;
};

/*Strings*/

function TextBetween(text: string, begintag: string, endtag: string):string {
    var result = "";
    if (typeof text == "string") {
        var ixs = text.indexOf(begintag);
        if (ixs > -1) {
            ixs = ixs + begintag.length;
            var ixe = text.indexOf(endtag, ixs);
            if (ixe > -1) {
                result = text.substring(ixs, ixe);
            }
        }
    }
    return result;
};

function TextsBetween(text:string, begintag:string, endtag:string,withtags:boolean):string[] {
    var result:string[]=[];
    while (text.indexOf(begintag) > -1 && text.indexOf(begintag)>-1)
    {
        var item = TextBetween(text, begintag, endtag);
 
        var fullitem = begintag + item + endtag;
        if (withtags) {
            result.push(fullitem);
        } else
        {
            result.push(item);
        }

        text = text.substring(text.indexOf(endtag) + endtag.length);
    }
    return result;
};
function Format(...any):string {
    var args:any[] = Array.prototype.slice.call(arguments, 1);
    if (args.length == 1)
    {
        if (IsArray(args[0]))
        {
            args = args[0];
        }
        //if 
    }
    var format = arguments[0];
    return format.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined'
            ? args[number]
            : match
            ;
    });
};

function Property(item: any, property: string, value?: any):any
{
    if (typeof value === "undefined" && !IsNull(item)) {
        if (property in item) { return item[property]; }
        return null;
    }
    else
    {
        item[property] = value;
    }
    return null;
}

/*End Strings*/



/*Objects*/

function IsDefined(value, path) {
    var root = false;
    path.split('.').forEach(function (key) {
        if (!root) {
            value = eval(key); root = true;
        } else {
            value = value && value[key];
        }
    });
    return (typeof value != 'undefined' && value !== null);
};

function In(item,...any) {
    if (arguments.length < 2)
        return false;
    else {
        var array: any[]  = Array.prototype.slice.call(arguments, 0);
        array.splice(0, 1);
        //return array.where(function (x) {
        //    return x === item;
        //}).length > 0;
        var found = false;
        array.forEach(function (item_i) {
            if (item_i===item) {
                found = true;
            }
        });
        return found;
    }
}

function IsAllNull(item, ...any) {
    if (arguments.length < 1)
        return false;
    else {
        var array:any[] = Array.prototype.slice.call(arguments, 0);
        array.splice(0, 0);
        var nullcount = 0;
        array.forEach(function (item_i) {
            if (IsNull(item_i))
            {
                nullcount++;
            }
        });
        return nullcount == array.length;
    }
}

function IsAllNotNull(item, ...any) {
    if (arguments.length < 1)
        return false;
    else {
        var array: any[] = Array.prototype.slice.call(arguments, 0);
        array.splice(0, 0);
        var nullcount = 0;
        array.forEach(function (item_i) {
            if (IsNull(item_i)) {
                nullcount++;
            }
        });
        return nullcount == 0;
    }
}

function cleanArray(actual) {
    var newArray = new Array();
    for (var i = 0; i < actual.length; i++) {
        if (actual[i]) {
            newArray.push(actual[i]);
        }
    }
    return newArray;
}

function removeFromArray(arr,...any) {
    var what, a = arguments, L = a.length, ax;
    while (L > 1 && arr.length) {
        what = a[--L];
        while ((ax = arr.indexOf(what)) !== -1) {
            arr.splice(ax, 1);
        }
    }
    return arr;
}

function Equals(arg1: any, arg2: any):boolean
{
    if (typeof (arg1) == "string" && typeof (arg2) == "string")
    {
        return (arg1.toLowerCase() == arg2.toLowerCase());
    }
    return arg1 == arg2;
}

function GetClientQueryString(hash?: string): General.KeyValue[] {
    if (hash==null) { hash = window.location.hash; }
    var parameters: General.KeyValue[] = [];
    if (hash.indexOf("?") > -1) {
        hash = hash.substring(hash.indexOf("?") + 1)
    }
    if (hash.length > 2) {

        var items = hash.split("&");
        items.forEach(function (item) {
            var kv = new General.KeyValue();
            var psplit = item.split("=");
            kv.Key = psplit[0].trim().toLowerCase();
            kv.Value = psplit[1].trim();
            parameters.push(kv);

        });
    }
    return parameters;
}

function ToHierarchy(items, idproperty, parentproperty, rootid) {
    if (!rootid) { rootid == null; }
    var Children = items.where(function (i) { return i[parentproperty] == rootid; });

    Children.forEach(function (item) {
        var parentid = item[idproperty];
        item.Children = ToHierarchy(items, idproperty, parentproperty, parentid);
    });
    return Children;
};

function ForAll(hierarchy: Object,childrenproperty:string, func:Function)
{
    func(hierarchy);
    if (childrenproperty in hierarchy)
    {
        var children = hierarchy[childrenproperty];
        if (!IsNull(children) && IsArray(children))
        {
            (<any[]>children).forEach(function (item) {
                ForAll(item, childrenproperty, func);
            });
        }
    }
}

function Clone(obj:Object):Object {

    if (null == obj || "object" != typeof obj) return obj;
    var copy = {};
    for (var attr in obj) {
        if (obj.hasOwnProperty(attr)) copy[attr] = obj[attr];
    }
    return copy;
}

function IsNull(item: any): boolean {
    return item == 'undefined' || item == null || (typeof(item)=="string" && item == "");
};

function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
}

function Guid():string {
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}
/*End Objects*/

/*HTML*/
function ToHtmlAttributeListString(obj:Object) {
    var str = "";
    for (var prop in obj) {
        if (obj.hasOwnProperty(prop)) {
            var value = obj[prop];
            if (!IsNull(value)) {
                str += prop + '="' + value + '" ';
            }
        }
    }
    return str;
};

function RenderHierarchy(obj: Object[], itemformatter?: Function) {
    var html = "";
    if (obj instanceof Array) {
        if (obj.length > 0) {
            html = "<ul>";
            obj.forEach(function (item) {
                html += "<li>";
                html += itemformatter(item);
                html += RenderHierarchy(item["Children"], itemformatter);
                html += "</li>\n";
            });
            html += "</ul>";
        }
    }
    return html;
};

function HtmlToText(html: string): string {
    var tmp = document.createElement("DIV");
    tmp.innerHTML = html;
    return tmp.textContent || tmp.innerText || "";
}

function ToString(item: Object)
{
    return IsNull(item) ? "" : item.toString();
}

function HtmlEncode(value: string): string {
    return $('<div/>').text(value).html();
}

function HtmlDecode(value: string): string {
    return $('<div/>').html(value).text();
}

function Truncate(item: string, limit?: number) {
    var result = "";
    if (IsNull(limit)) { limit = 40; }
    if (typeof item == "string") {

        if (item.length > limit) {
            result = item.substring(0, limit) + "...";
        } else {
            result = item;
        }
    }
    return result;
}

function BindEvent(selector, events, handler) {
    $(selector).unbind(events, handler).bind(events, handler);
};

function UnBindEvent(selector, events) {
    $(selector).unbind(events);
};

function ShowHide(target) {
    $(target).toggleClass("hidden");

}
/*End HTML*/

/*DateTime*/
function JsonToDate(item:string) {
    if (IsNull(item)) { return null; }
    var x = item.match(/\d+/)[0];
    var nr:any = +x;
    return new Date(nr);
}

function ToDate(item: string)
{
    return FormatDate(JsonToDate(item));
}

function FormatDate(d:Date, format?:string):string {
    if (IsNull(format)) { format = "yy/mm/dd hh:ii:ss"; }
    if (IsNull(d)) { return ""; }
    return $.formatDateTime(format, d);
}

function ToNormalDate(item:string):string {
    return FormatDate(JsonToDate(item));
}
/*End DateTime*/

/*Proto*/
if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (needle) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] === needle) {
                return i;
            }
        }
        return -1;
    };
}

function Res(key: string, culture?: string): string {
    var res = key;
    res = resourcemanager.Get(key, culture);
    return res;
}

function Attribute(obj, name:string, value?:string):string {
    if (arguments.length == 3) {
        $(obj).attr(name, value);  
    }
    if (arguments.length == 2) {
        return $(obj).attr(name);
    }
    return "";
}

function Content(obj, value?:string):string {
    if (arguments.length == 2) {
        $(obj).html(value);
    }
    if (arguments.length == 1) {
        return $(obj).html();
    }
    return "";
}
/**End Proto/

/*Expressions*/
 function parseExp(expression, model) {
    var body = expression.toString();
    var exp = TextBetween(body, "return ", "}");
    exp = exp.trim();
    var c = 0;
    var result = { propertyname: "", fullpropertyname: "", obj: null, value: null, typename: "", resourceid: "" };
    var parts = exp.split('.');
    var item = model;
    var value = model;
    var sc = ".";
    parts.forEach(function (key) {
        if (c == 0) {
            value = value ? value : eval(key);
        }
        if (c > 0) {
            //value = value && value[key];
            value = value[key];
            result.fullpropertyname += key;
            if (c < parts.length - 1) {
                result.fullpropertyname + sc;
            }
        }
        if (c == parts.length - 2) {
            result.obj = value;
            if (result.obj) { result.typename = result.obj['TypeName']; }

        }
        if (c == parts.length - 1) {
            result.propertyname = key;
        }
        c++;
    });
    result.resourceid = Format("Models.{0}.{1}", result.typename, result.propertyname);
    var val = eval(exp);;
    result.value = val ? val : "";
    return result;
};
/*End Expressions*/

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    //$.each(a, function () {
    //    if (o[this.name] !== undefined) {
    //        if (!o[this.name].push) {
    //            o[this.name] = [o[this.name]];
    //        }
    //        o[this.name].push(this.value || '');
    //    } else {
    //        o[this.name] = this.value || '';
    //    }
    //});
    //return o;

    var paramObj = {};
    $.each(a, function (_, kv) {
        paramObj[kv.name] = kv.value;
    });
    return paramObj
};

function SerializeForm(selector): Dictionary {
    return $(selector).serializeObject();
}

function FilesIntoUL(viewmodel) {
    var model = viewmodel.Items;
    var html = "";
    html += '<ul class="jqueryFileTree" style="display: none;">';
    //<li class="directory collapsed"><a href="#" rel="/this/folder/">Folder Name</a></li>
    //<li class="file ext_txt"><a href="#" rel="/this/folder/filename.txt">filename.txt</a></li>
    model.forEach(function (item) {
        if (item.IsFolder) {
            html += Format('<li class="directory collapsed"><a href="#" rel="{1}{0}/\">{0}</a></li>', item.ID, item.Directory);
        } else {
            html += Format('<li class="file ext_{2}"><a href="#" rel="{1}{0}">{0}</a></li>', item.ID, item.Directory, item.Extension);
        }
    });
    html += "</ul>";
    return html;

}
$.fn.extend({
    padding: function (direction: string):number {
        // calculate the values you need, using a switch statement
        // or some other clever solution you figure out

        // this now contains a wrapped set with the element you apply the 
        // function on, and direction should be one of the four strings 'top', 
        // 'right', 'left' or 'bottom'

        // That means you could probably do something like (pseudo code):
        var paddingvalue:string = this.css('padding-' + direction).trim();
        var intPart = "";
        var unit = paddingvalue.substring(paddingvalue.length-2);
        intPart = paddingvalue.replace(unit, "");
        //stest.substring(0, stest.lastIndexOf("px"))
        //var intPart = this.css('padding-' + direction).rem();
        //var unit = this.css('padding-' + direction).getUnit();

        switch (unit) {
            case 'px':
                return Number(intPart);
            case 'em':
                return 0; //ConvertEmToPx(intPart)
            default:
            // Do whatever you feel good about as default action
            // Just make sure you return a value on each code path
        }
    }
});
$.fn.extend({
    editable: function () {
        var that = this,
            $edittextbox = $('<input type="text"></input>').css('min-width', that.width()),
            submitChanges = function () {
                that.html($edittextbox.val());
                that.show();
                that.trigger('editsubmit', [that.html()]);
                $(document).unbind('click', submitChanges);
                $edittextbox.detach();
            },
            tempVal;
        $edittextbox.click(function (event) {
            event.stopPropagation();
        });

        that.dblclick(function (e) {
            tempVal = that.html();
            $edittextbox.val(tempVal).insertBefore(that).bind('keypress', function (e) {
                if ($(this).val() !== '') {
                    var code = (e.keyCode ? e.keyCode : e.which);
                    if (code == 13) {
                        submitChanges();
                    }
                }
            });
            that.hide();
            $(document).click(submitChanges);
        });
        return that;
    }
});


function browserSupportsWebWorkers():boolean {
    //return typeof window.Worker === "function";
    return false;
}

module Engine
{  

    export class ActionCenter
    {
        private Selector: any = null;
        private CurrentSelector: any = null;
        private ListSelector: any = null;
        private ActionBarSelector: any = null;
        private class_Error: string = "n-error";
        private class_Warning: string = "n-warning";
        private class_Info: string = "n-info";
        private class_Success: string = "n-success";
        private format_Notification: string = "<div class=\"notification {1}\">{0}</div>";

        public SetSelectors(selector:any, currentselector: any, listselector: any,actionbarselector:any)
        {
            this.Selector = selector;
            this.CurrentSelector = currentselector;
            this.ListSelector = listselector;
            this.ActionBarSelector = actionbarselector;
        }

        public AddSuccess(content: string)
        {
            this.AddNotification(content, this.class_Success);
        }
        public AddInfo(content: string)
        {
            this.AddNotification(content, this.class_Info);
        }
        public AddWarning(content: string)
        {
            this.AddNotification(content, this.class_Warning);
        }
        public AddError(content: string)
        {
            this.AddNotification(content, this.class_Error);
        }

        public AddNotification(content: string, cssclass?:string)
        {
            content = Format(this.format_Notification, content, cssclass);
            var lastmessage = $(this.CurrentSelector).html();
            $(this.CurrentSelector).html(content);
            $(this.ListSelector).prepend(lastmessage);
            $(this.Selector).show();
        }

        public ClearAll()
        {
            this.ClearCurrent();
            this.ClearList();
            $(this.Selector).hide();
            
        }

        public ClearCurrent()
        {
            $(this.CurrentSelector).html("");

        }
        public ClearList() {
            $(this.ListSelector).html("");

        }
        public ToggleListVisibility() {
            if ($(this.ListSelector).is(":visible")) {
                $(this.ListSelector).hide();
            }
            else {
                $(this.ListSelector).show();

            }
        }
    }

    export class UIManager
    {
        private duration: number = 200;
        private min_width: number = 150;

        private GetMaxWidth():number
        {
             var maxwidth = $("#main-content").width();
             return maxwidth;
        }

        public ActivateList()
        {

                $("#ListController").parent().animate({ "max-width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });
                $("#SaveController").parent().animate({ "width": this.min_width + "px" }, { duration: this.duration, queue: false });
        }
        public ActivateSave()
        {
            $("#ListController").parent().animate({ "max-width": this.min_width + "px" }, { duration: this.duration, queue: false });
            $("#SaveController").parent().animate({ "width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });

        }
    }
}

class Editor
{
    public HtmlFormat: string = "";
    public ValueGetter: Function = null;
    public ValueSetter: Function = null;
    public TargetValueGetter: Function = null;
    public TargetValueSetter: Function = null;
    public $Target: JQuery = null; 
    public $Me: JQuery = null; 
    public Current_Value: string;
    public Original_Value: string;

    static editclass: string = "editing";

    constructor(HtmlFormat: string, ValueGetter: Function, ValueSetter: Function)
    {
        this.HtmlFormat = HtmlFormat;
        this.ValueGetter = ValueGetter;
        this.ValueSetter = ValueSetter;
    }

    public Save()
    {
        this.TargetValueSetter(this.ValueGetter(this.$Me));
        this.$Target.removeClass(Editor.editclass);
        this.$Me.remove();
    }

    public Load(Target: JQuery, TargetValueGetter: Function, TargetValueSetter: Function)
    {
        var me = this;
        this.TargetValueGetter = TargetValueGetter;
        this.TargetValueSetter = TargetValueSetter;
        this.Original_Value = TargetValueGetter().trim();
        this.$Me = $(Format(this.HtmlFormat, this.Original_Value));

        //setting UI
        var containerwidth = Target.width() - (Target.padding("left") + Target.padding("right"));
        var containerheight = Target.height() - (Target.padding("top") + Target.padding("bottom"));
        var containerfontfamily = Target.css('font-family');
        var containerfontsize = Target.css('font-size');
        var containerlineheight = Target.css('line-height');

        this.$Me.width(containerwidth);
        this.$Me.height(containerheight);
        this.$Me.css('font-family', containerfontfamily);
        this.$Me.css('font-size', containerfontsize);
        this.$Me.css('line-height', containerlineheight);
        //end setting UI
        this.ValueSetter(this.$Me, this.Original_Value);

        this.$Target = Target;
        this.$Target.html('');
        this.$Me.appendTo(this.$Target);
        this.$Target.addClass(Editor.editclass);


        this.$Me.blur(function () { me.Save(); });
        this.$Me.keypress(function (e) {
            if (e.which == 13) {
                me.Save();
            }
        });

        this.$Me.focus();
    }
}
function MakeEditable2(cellselector)
{
    $(cellselector).off("click");
    $(cellselector).click(function () {
        var $target = $(this);
        if (!$target.hasClass(Editor.editclass)) {
            var editor = new Editor('<input type="text" class="celleditor" value="" />',(i: JQuery) => i.val(), (i: JQuery, val: any) => i.val(val));
            editor.Load($target,() => $target.html(),() => $target.html(editor.ValueGetter(editor.$Me)));
        }
    });
}

function MakeEditable3(cellselector, optionObject) {
    $(cellselector).off("click");
    $(cellselector).click(function () {
        var $target = $(this);
        if (!$target.hasClass(Editor.editclass)) {
  
            var editor = new Editor(Format('<select class="celleditor">{0}</select>', ToOptionList(optionObject, false)),(i: JQuery) => i.val(),(i: JQuery, val: any) => { i.val(val); });
            editor.Load($target,() => $target.html(), () => $target.html(editor.ValueGetter(editor.$Me)));
        }
    });
}
var testoptions = { "eba_GA:x1": "Africa", "eba_GA:x2": "EU", "eba_GA:x3": "USA sfsdg fsdfsfs"};

function MakeEditable(cellselector)
{
    function SaveCell(target)
    {
        var parent = target.parent();
        var iskey = parent.hasClass("data-key");
        var newvalue = iskey ? target.val() : target.text();
        if (iskey && IsNull(newvalue))
        {
            var jrow = parent.closest('tr');
            jrow.remove();
        }
      
        parent.text(newvalue);
        parent.removeClass("cellEditing");
    }
    $(cellselector).off("click");
    $(cellselector).click(function () {
        var jitem = $(this);
        if (!jitem.hasClass("cellEditing")) {
            var htmlformat = "";
            var html = "";
            var OriginalContent = "";

            if (jitem.hasClass("data-key")) {
                htmlformat = '<input type="text" value="{0}" />';
            }
            else {
                htmlformat = '<textarea class="cell- editor">{0}</textarea>';
            }
            OriginalContent = jitem.text().trim();

            html = Format(htmlformat, OriginalContent);
            jitem.addClass("cellEditing");
            jitem.html(html);
            jitem.children().first().focus();

            jitem.children().first().keypress(function (e) {
                if (e.which == 13) {
                    SaveCell($(this));
                }

            });
            jitem.children().first().blur(function () {
                SaveCell($(this))
            });
        }
    });


}

function ToOptionList(obj: Object, addemptyoption:boolean): string
{
    var result = "";
    if (addemptyoption)
    {
        result += Format('<option value="">-select-</option>\n');
    }
    for (var prop in obj) {
        var val = obj[prop];
        if (obj.hasOwnProperty(prop) && typeof val !== "function") {
            result += Format('<option value="{0}">{1}</option>\n', prop, val);
        }
    }
    return result;
}
function NormalizeFolderPath(folder:any):string
{
    if (folder == null) { folder = ""; }
    if (folder.indexOf("~") == 0) { folder = folder.substring(1); }
    if (folder[folder.lentgh - 1] != "/")
    {
        folder = folder + "/";
    }

    return folder;
}


function SetCustomFields(fieldcontainerselector: any, targetinputselector:any): General.KeyValue[]
{
    var result: General.KeyValue[] = [];
    $(".custom-field", $(fieldcontainerselector)).each(function (ix, item) {
        var value = $(item).val();
        var name = $(item).attr("name");
        var kv: General.KeyValue = { Key: name, Value: value };
        result.push(kv);
    });

    $(targetinputselector).val(JSON.stringify(result));

    return result;
}

function Eval(obj)
{
    if (IsNull(obj)) { return null; }
    if (IsFunction(obj)) { return obj(); }
    return obj;
}

function IsFunction(functionToCheck) {
    var getType = {};
    return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
}

function IsArray(value: any) {
    if (Array.isArray) {
        return Array.isArray(value);
    }
    return false;
}

function Split(text: string, delimeters: any, removeempty:boolean)
{
    var result = [text];
    if (typeof (delimeters) == 'string') {
        delimeters = [delimeters];
    }
    while (delimeters.length > 0) {
        for (var i = 0; i < result.length; i++) {
            var tempSplit = result[i].split(delimeters[0]);
            result = result.slice(0, i).concat(tempSplit).concat(result.slice(i + 1));
        }
        delimeters.shift();
    }
    if (removeempty)
    {
        var resultwithoutempty = [];
        result.forEach(function (item) {
            if (!IsNull(item.trim()))
            {
                resultwithoutempty.push(item);
            }
        });
        return resultwithoutempty;
    }
    return result;

}

function Access(obj, key) {
    if (key == "this") { return obj; }
    return key.split(".").reduce(function (o, x) {
        return (typeof o == "undefined" || o === null) ? o : o[x];
    }, obj);
}

function OuterHtml(item: JQuery): string
{
    return item[0].outerHTML;
    //return item.wrapAll('<div>').parent().html(); 
}

function Bind(target: any, data: any, parent?: any) {
    var fBind = (target, data, parent?) => this.Bind.call(this, target, data, parent);
    var NoCheck = [];
    var targetitem = $(target);
    var bindattribute = "binding";
    var attributespecifier = "=>";
    var jquerytargets = targetitem.find("*[" + bindattribute + "]");
    var targets = [];
    if (!IsNull($(target).attr(bindattribute))) { targets.push(target); }
    jquerytargets.each(function (ix, item) {
        targets.push(item);
    });
    //console.log(Format("Binding target: {0}", targets.length));
    targets.forEach(function (item, ix) {
        if (NoCheck.indexOf(item) == -1) {
            var id = $(item).attr(bindattribute);
            var targetattribute = "";
            var formatString = "{0}";
            var expr = id;
            if (expr.indexOf(attributespecifier) > -1) {
                var isplit = expr.split(attributespecifier);
                if (isplit.length == 2) {
                    targetattribute = isplit[0];
                    expr = isplit[1];

                }
            }
            var originalexpr = expr;
            var expressions = [];
            var i = 0;
            for (var expression = TextBetween(expr, "{", "}"); !IsNull(expression); expression = TextBetween(expr, "{", "}")) {
                expressions.push(expression);
                expr = expr.replace("{" + expression + "}", "<<<" + i + ">>>");
                i++;
            }
            expr = expr.replace(/<<</g, "{").replace(/>>>/g, "}");

            if (expressions.length > 0) {
                formatString = expr;//originalexpr.replace("{" + expr + "}", "{0}");
            } else {
                expressions.push(originalexpr);
            }
            var values = [];
            expressions.forEach(function (expression) {
                var val = null;
                val = Access(data, expression);
                if (typeof val == "string") {
                    if (val.indexOf("/Date(") == 0) {
                        val = ToDate(val);
                    }
                    else {
                        val = val.replace(/(?:\r\n|\r|\n)/g, '<br />');
                    }
                }
                values.push(val)
            });

            var elementtemplate = $('[binding-type=template]', $(item)).first();
            var newelementX = $(elementtemplate).clone(true, true);
            newelementX.removeAttr("binding-type");

            var firstvalue = values[0];
            if (IsArray(firstvalue)) {

                if (elementtemplate.length == 0) {
                    console.log('no template found!');
                }
                $(item).empty();
                elementtemplate.appendTo($(item));
                var itemstoadd = [];
                var bindattributeselector = "[" + bindattribute + "]";
                firstvalue.forEach(function (childitem) {
                 

                    fBind(newelementX, childitem, firstvalue);
                    itemstoadd.push(OuterHtml(newelementX));


                });
                //newelement.appendTo($(item));
                $(item).append(itemstoadd.join('\n'));
            } else {

                if (IsNull(targetattribute)) {
                    if (IsNull(elementtemplate) || elementtemplate.length == 0) {
                        $(item).html(Format(formatString, values));
                    }
                }
                else {
                    $(item).attr(targetattribute, Format(formatString, values));

                }

            }
        }
    });
}
function Replace(text:string, texttoreplace:string, textwithreplace:string):string
{
    /*
    var reg = new RegExp(texttoreplace, "g");
    return text.replace(reg, textwithreplace);
    */
    var index = 0;
    do {
        text = text.replace(texttoreplace, textwithreplace);
    }
    while ((index = text.indexOf(texttoreplace, index + 1)) > -1);
    return text;
}


function GetProperties(item: Object): General.KeyValue[]
{
    var properties: any[] = [];
    for (var propertyName in item) {
        if (item.hasOwnProperty(propertyName)) {
            var propertyValue = item[propertyName];
            var kv = new General.KeyValue();
            kv.Key = propertyName;
            kv.Value = propertyValue;
            properties.push(kv)
        }
    }
    return properties;
}

class BindingTemplate
{
    public ID: string = "";
    public ChildID: string = "";
    public Children: BindingTemplate[] = [];
    //public Child: BindingTemplate = null;
    public Parent: BindingTemplate = null;
    public Content: string;
    public ChildPlaceholder: string = "@children@";
    public AccessorExpression: string = "";
    public PageSize: number = 0;
    
    public Bind(data:Object):string
    {
     
        var result_html: string = "";
        var me = this;
        result_html = BindLevel(this.Content, data);
        me.Children.forEach(function (child) {
            if (child.AccessorExpression != "nobind") {
                var items = Access(data, child.AccessorExpression);
                items = IsNull(items) ? [] : items;
                var childitems = "";
                
                if (IsArray(items)) {
                     items.forEach(function (item) {
                    childitems += child.Bind(item);
                });
                }
                else {
                    for (var propertyName in items) {
                        if (items.hasOwnProperty(propertyName)) {
                            var item = items[propertyName];
                            childitems += child.Bind(item);
                        }
                    }
                }
             
                result_html = Replace(result_html, child.ID, childitems);
            }
        });
        return result_html;

    }


    public ToHierarchyString(tab:string):string
    {
        var me = this;
        var result = "";
        tab = IsNull(tab) ? "    " : tab;
        result += Format("{0} {1} {2}\n", tab, this.ID, this.AccessorExpression);
        me.Children.forEach(function (child) {
            result += child.ToHierarchyString(tab + tab);

        });
   
        return result;
    }
    
    public GetExpression(item: JQuery)
    {
        var expr = item.attr("binding");
        return expr;
    } 
}

function GetBindingTemplate(target:JQuery)
{
    var templates = $('[binding-type=template]', target);
    var me = this;

    templates.each(function (index, item) {
        var jitem = $(item);
        var ix = jitem.index();
        var parent = jitem.parents("[binding-items]")[0];
        var parentbinding = $(parent).attr("binding-items");
        var parentpagesize = $(parent).attr("binding-pagesize");
        var placeholder = "@" + index + "@";
        var placeholdernode = document.createTextNode(placeholder);
        //jitem.insertBefore(placeholdernode);
        $(placeholdernode).insertBefore(jitem);
        jitem.attr("ChildID", placeholder);
        jitem.attr("Expression", parentbinding);
        jitem.attr("PageSize", parentpagesize);
        jitem.remove();


    });

    var templatecollection: BindingTemplate[] = [];

    templates.each(function (index, item) {
        var jitem = $(item);
        var placeholder = "@" + index + "@";
        var childID = jitem.attr("ChildID");
        jitem.removeAttr("ChildID");
        var parentBinding = jitem.attr("Expression");
        jitem.removeAttr("Expression");
        var parentPageSize = jitem.attr("PageSize");
        jitem.removeAttr("PageSize");

        jitem.removeAttr("binding-type");
        var html = OuterHtml(jitem);
        var t = new BindingTemplate();
        templatecollection.push(t);
        t.ID = childID
        t.Content = html;
        t.AccessorExpression = parentBinding;
        //t.ParentPageSize = parentPageSize;

    });

    var t = new BindingTemplate();
    templatecollection.push(t);
    t.ID = "@root@";
    t.Content = target.html();
    t.AccessorExpression = "this";
    var roottemplate: BindingTemplate = null;
    var templatelist = templatecollection.AsLinq<BindingTemplate>();
    templatecollection.forEach(function (item) {
        var parenttemplate = templatelist.FirstOrDefault(i=> i.Content.indexOf(item.ID) > -1);
        if (parenttemplate != null) {
            item.Parent = parenttemplate;
            if (IsNull(item.AccessorExpression)) {
                item.AccessorExpression = parenttemplate.AccessorExpression;
                //todo
            }
            parenttemplate.Children.push(item);
        }
        else
        {
            roottemplate = item;
        }
    });
    if (IsNull(roottemplate))
    {
        roottemplate = templatelist.FirstOrDefault(i=> IsNull(i.Parent));
    }
    return roottemplate;
}

var TemplateDictionary: TemplateDictionaryItem[] = [];
class TemplateDictionaryItem
{
    public Item: JQuery = null;
    public Template: BindingTemplate = null;
}

function BindX(item: JQuery, data: Object)
{
    if (item.length == 0) {
        ShowNotification("BindX: " + item.selector + " has no items!");
    } else {
        var bt: BindingTemplate = null;
        var templatedictionaryitem = TemplateDictionary.AsLinq<TemplateDictionaryItem>().FirstOrDefault(i=> i.Item[0] == item[0]);
        if (templatedictionaryitem == null) {
            if (item.length > 0) {
                bt = GetBindingTemplate(item);
                templatedictionaryitem = new TemplateDictionaryItem();
                templatedictionaryitem.Item = item;
                templatedictionaryitem.Template = bt;
                TemplateDictionary.push(templatedictionaryitem);
            } else {
                console.log(item.selector + " was not found! (BindX)");
            }
        } else {
            bt = templatedictionaryitem.Template;
        }
        item[0].innerHTML = bt.Bind(data);
    }
}
var S_Bind_Start = "bind[";
var S_Bind_End = "]";
var s_list_selector: string = ".list";
var s_listpager_selector: string = ".listpager";
var s_listfilter_selector: string = ".listfilter";
var s_sublist_selector: string = ".sublist";
var s_sublistpager_selector: string = ".sublistpager";
var s_detail_selector: string = ".detail";
var s_parent_selector: string = ".parent";
var s_contentcontainer_selector: string = ".contentcontainer";
var s_content_selector: string = ".subcontent";

function BindLevel(html: string, data: Object):string
{
    var result_html: string = html;
    var bindings = TextsBetween(html, S_Bind_Start, S_Bind_End, true);
    bindings.forEach(function (binding) {
        var subbindings = TextsBetween(binding, "{", "}", true);
        var bindingexpression = TextBetween(binding, S_Bind_Start, S_Bind_End);
        if (subbindings.length == 0)
        {
            subbindings.push(bindingexpression);
        } 
        var s_html = bindingexpression;
        subbindings.forEach(function (subbinding) {
            var subbindingexpression = subbinding.indexOf("{")>-1 ? TextBetween(subbinding, "{", "}") : subbinding;
            var dataitem = Access(data, subbindingexpression);
            s_html = Replace(s_html, subbinding , dataitem);
            //s_html = Replace(s_html, S_Bind_Start + subbinding + S_Bind_End, dataitem);
        });
        result_html = Replace(result_html, binding, s_html);

    });
    //result_html = Replace(result_html, S_Bind_Start, "");
    //result_html = Replace(result_html, S_Bind_End, "");
    return result_html;
}

var actioncenter = new Engine.ActionCenter();
var uimanager = new Engine.UIManager();
var resourcemanager: IResourceManager = { Get: function (key: string, culture?: string) { return key;}};
interface IResourceManager
{
    Get(key: string, culture?: string): string;
}
var activeItem = null;



