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
    var errortext = 'UI Error: ' + errorMsg + ' Script: ' + url + ' Line: ' + lineNumber;
    Error(errortext);
    return true;
}
function ShowHideChild(selector, sender) {
    $(selector).hide();
    var $item = $(selector, $(sender).parent()).first();
    $item.show();
}
function SetPivots() {
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
function Activate(jitem) {
    var $item = jitem;
    var $parent = $item.parent();
    var previouswidth = 0;
    var minsize = 200;
    var margin = 0;
    var previouswidth = 0;
    $parent.children().each(function (ix, item) {
        var current = $(item);
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
function LoadJS(path) {
    var fileref = document.createElement('script');
    fileref.setAttribute("type", "text/javascript");
    fileref.setAttribute("src", path);
}
function GetFunctionBody(f) {
    var result = "";
    var entire = f.toString();
    var body = entire.slice(entire.indexOf("{") + 1, entire.lastIndexOf("}"));
    return result;
}
function Select(sender) {
    var $command = $(sender);
    var sel = "selected";
    var $commands = $command.parent().children(); //a
    $commands.removeClass(sel);
    $command.addClass(sel);
    var selectfromlist = function (item, items) {
        if (item.length > 0) {
            items.removeClass(sel);
            item.addClass(sel);
        }
    };
    var $list = $command.parents(".list").first();
    if ($list.length == 1) {
        var tag = $list.prop("tagName");
        tag = tag.toLowerCase();
        var $listitem = null;
        var $listitems = null;
        if (tag == "ul") {
            $listitem = $command.parents("li").first();
            $listitems = $list.children();
        }
        if (tag == "table") {
            $listitem = $command.parents("tr").first();
            $listitems = $("tr", $list);
        }
        selectfromlist($listitem, $listitems);
    }
    return sender;
}
function GetReturnStatement(f) {
    var body = GetFunctionBody(f);
    var body = body.substring(body.lastIndexOf("return "));
    return body.substring(body.indexOf(" ") + 1);
}
function GetMemberExpression(f) {
    return "";
}
function StringEquals(s1, s2) {
    if (typeof s1 == "string" && typeof s2 == "string") {
        return s1.toString().toLowerCase() == s2.toString().toLowerCase();
    }
    return false;
}
function ToObjectX(items) {
    var obj = {};
    for (var key in items) {
        if (items.hasOwnProperty(key)) {
            var kvobj = items[key]; //
            if (typeof (kvobj) == "object" && "Key" in kvobj) {
                if (!IsNull(kvobj.Key)) {
                    obj[kvobj.Key] = kvobj.Value;
                }
            }
            else {
                obj[key] = kvobj;
            }
        }
    }
    ;
    return obj;
}
function ToObject(items) {
    var obj = {};
    items.forEach(function (item) {
        obj[item.Key] = item.Value;
    });
    return obj;
}
var General;
(function (General) {
    var KeyValue = (function () {
        function KeyValue() {
            this.Key = "";
            this.Value = null;
        }
        return KeyValue;
    })();
    General.KeyValue = KeyValue;
    var Message = (function () {
        function Message() {
            this.Parameters = {};
        }
        return Message;
    })();
    General.Message = Message;
})(General || (General = {}));
var Waiter = (function () {
    function Waiter(Condition, AllCompleted) {
        this.Items = [];
        this.Condition = null;
        this.AllCompleted = null;
        this.IsStarted = false;
        this.AllCompleted = AllCompleted;
        this.Condition = Condition;
    }
    Waiter.prototype.Check = function () {
        var me = this;
        var result = true;
        this.Items.forEach(function (Item) {
            if (!me.Condition(Item)) {
                result = false;
            }
        });
        if (result && me.IsStarted) {
            me.Stop();
            me.AllCompleted();
            me.Items = [];
        }
    };
    Waiter.prototype.WaitFor = function (Item) {
        var me = this;
        me.Items.push(Item);
    };
    Waiter.prototype.Start = function () {
        this.IsStarted = true;
        this.Check();
    };
    Waiter.prototype.Stop = function () {
        this.IsStarted = false;
    };
    return Waiter;
})();
var StopProgress = function (id) {
    return null;
};
var StartProgress = function (id) {
    return null;
};
var ResultFormatter = function (rawdata) {
    return rawdata;
};
var requests = [];
try {
    if (!IsNull(parent["requests"])) {
        requests = parent["requests"];
    }
}
catch (err) {
}
function ShowContent(selector, sender) {
    var id = selector.replace("#", "");
    var $activator = $(sender); // $("[activator-for=" + id + "]");
    Select($activator);
    var $content = $(selector);
    var $parents = ($activator.length == 0 ? $content : $activator).parents(s_contentcontainer_selector);
    var $parent = $parents.first();
    $parent.children(s_content_selector).hide();
    if ($parent.length > 0) {
        var id = $parent.attr("id");
        ShowContentByID("#" + id);
    }
    if ($content.length == 0) {
        ShowError("ShowContent: " + selector + " has not items!");
    }
    $content.show();
    return $activator;
}
function ShowContentByID(selector) {
    var id = selector.replace("#", "");
    var $activator = $("[activator-for=" + id + "]").first();
    ShowContent(selector, $activator);
    return $activator;
}
function ShowContentBySender(sender) {
    var $activator = $(sender);
    var targetselector = "#" + $activator.attr("activator-for");
    ShowContent(targetselector, sender);
    return $activator;
}
function GetPart(data, startix, endix) {
    var part = [];
    if (IsArray(data)) {
        part = data.slice(startix, endix);
    }
    else {
        var ix = 0;
        for (var propertyName in data) {
            if (data.hasOwnProperty(propertyName)) {
                if (ix >= startix && ix < endix) {
                    var item = data[propertyName];
                    item["PropertyName"] = propertyName;
                    part.push(item);
                }
                ix++;
            }
        }
    }
    return part;
}
function EnumerateObject(target, context, func) {
    if (IsArray(target)) {
        var ix = 0;
        target.forEach(function (item) {
            func.call(context, item, ix);
            ix++;
        });
    }
    else {
        for (var propertyName in target) {
            if (target.hasOwnProperty(propertyName)) {
                func(target[propertyName], propertyName);
            }
        }
    }
}
function GetLength(data) {
    if (IsArray(data)) {
        return data.length;
    }
    else {
        var ix = 0;
        for (var propertyName in data) {
            if (data.hasOwnProperty(propertyName)) {
                ix++;
            }
        }
        return ix;
    }
    return 0;
}
function LoadPage($bindtarget, $pager, data, page, pagesize, events) {
    var me = this;
    var startix = pagesize * page;
    var endix = startix + pagesize;
    var itemspart = GetPart(data, startix, endix);
    var datalength = GetLength(data);
    CallFunction(events, "onloading", itemspart);
    BindX($bindtarget, itemspart);
    CallFunction(events, "onloaded", itemspart);
    if ($pager.length == 0 || 1 == 1) {
        $pager.pagination(datalength, {
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
                LoadPage($bindtarget, $pager, data, pageix, pagesize, events);
                CallFunction(events, "onpaged");
                return false;
            },
        });
    }
    else {
    }
}
function CallFunction(eventcontainer, eventname, args) {
    if (!IsNull(eventcontainer)) {
        if (eventname in eventcontainer && IsFunction(eventcontainer[eventname])) {
            eventcontainer[eventname](args);
        }
    }
}
function CallFunctionVariable(func, args) {
    if (!IsNull(func) && IsFunction(func)) {
        func(args);
    }
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
    if ('Notify' in window.external) {
        window.external.Notify(strdata);
    }
    else {
        console.log(strdata);
    }
}
function asyncFunc(func) {
    setTimeout(function () {
        func();
    }, 10);
}
function Communication_Listener(data) {
    //Notify("Communication_Listener_Start");
    var message = JSON.parse(data);
    data = "";
    //Notify("Communication_Listener Parsed");
    data = null;
    if (message.Category == "ajax") {
        asyncFunc(function () {
            //Notify("Calling AjaxResponse");
            AjaxResponse(message);
        });
    }
    if (message.Category == "notfication") {
    }
    if (message.Category == "error") {
    }
    if (message.Category == "action") {
        if (message.Url.toLowerCase() == "instance") {
            app.instancecontainer.HandleAction(message);
        }
    }
    if (message.Category == "debug") {
        debugger;
    }
    //Notify("Communication_Listener_End");
}
function AjaxRequest(url, method, contenttype, parameters, success, error) {
    return AjaxRequestComplex(url, method, contenttype, parameters, [success], [error]);
}
function AjaxRequestComplexX(url, method, contenttype, parameters, success, error) {
    var requestid = Guid();
    var requesthandler = { error: error, success: success, Id: requestid, succeded: false };
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
function AjaxRequestComplex(url, method, contenttype, parameters, success, error) {
    var requestid = Guid();
    var requesthandler = { error: error, success: success, Id: requestid, succeded: false };
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
    if ('Notify' in window.external) {
        Communication_ToApp(msg);
    }
    else {
        Ajax("Instance/Index", "get", { msg: msg }, AjaxResponse, contenttype);
    }
    return requesthandler;
}
function AjaxResponse(message) {
    var request = requests.AsLinq().FirstOrDefault(function (i) { return i.Key == message.Id; });
    if (request != null) {
        var requesthandler = request.Value;
        var stringdata = message.Data;
        message.Data = "";
        var response = stringdata;
        if (message.ContentType.indexOf("json") > -1) {
            if (!IsNull(stringdata)) {
                response = JSON.parse(stringdata);
                stringdata = "";
            }
        }
        var ix = requests.indexOf(request);
        if (ix > -1) {
            requests.splice(ix, 1);
        }
        if (IsNull(message.Error)) {
            requesthandler.succeded = true;
            requesthandler.success.forEach(function (func) {
                func(response, requesthandler);
            });
        }
        else {
            requesthandler.error.forEach(function (func) {
                func(response);
            });
            ShowError("Response Error: " + response);
        }
    }
    else {
        ShowError("Request not found! " + message.Id);
    }
}
function clearobject(item) {
    if (typeof item == "string") {
        item = "";
    }
    else {
        for (var propertyName in item) {
            delete item[propertyName];
        }
    }
}
function Ajax(url, method, parameters, generichandler, contentType) {
    var result = {}; //new Engine.InfoContainer();
    var _contentType = "text/html";
    var _dataType = "";
    var callback = function (result) {
        return false;
    };
    var S_Callback = "callback";
    if (!IsNull(parameters) && parameters[S_Callback] instanceof Function) {
        callback = parameters[S_Callback];
    }
    if (contentType == "json") {
        _contentType = "application/json; charset=UTF-8";
        _dataType = "json";
    }
    var params = parameters;
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
            var errorobj = GetErrorObj(exception, this.contentType);
            var errormsg = Format("Request failed: {0}", errorobj.message);
            //errormsg += Format("\nurl: {0}", Id) + "\n" + errorobj.stacktrace;
            actioncenter.AddError(errormsg);
            SetProperty(result, "Error", exception);
            generichandler(result);
        }
    });
}
function GetErrorObj(exception, contenttype) {
    var exceptiontext = "responseJSON" in exception ? exception["responseJSON"] : "";
    var stacktrace = "";
    var errorobj = { message: "", stacktrace: "" };
    if (contenttype == "text/html") {
        errorobj.message = $(exception["responseText"]).text();
        return errorobj;
    }
    if ("responseJSON" in exception) {
        if (typeof (exception["responseJSON"]) == "object") {
            exceptiontext = exception["responseJSON"].Message;
            stacktrace = exception["responseJSON"].StackTrace;
        }
        else {
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
function SetProperty(target, name, value) {
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
}
;
/*Strings*/
function TextBetween(text, begintag, endtag) {
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
}
;
function TextsBetween(text, begintag, endtag, withtags) {
    var result = [];
    while (text.indexOf(begintag) > -1 && text.indexOf(begintag) > -1) {
        var item = TextBetween(text, begintag, endtag);
        var fullitem = begintag + item + endtag;
        if (withtags) {
            result.push(fullitem);
        }
        else {
            result.push(item);
        }
        text = text.substring(text.indexOf(endtag) + endtag.length);
    }
    return result;
}
;
function Format() {
    var any = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        any[_i - 0] = arguments[_i];
    }
    var args = Array.prototype.slice.call(arguments, 1);
    if (args.length == 1) {
        if (IsArray(args[0])) {
            args = args[0];
        }
    }
    var format = arguments[0];
    return format.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
}
;
function Property(item, property, value) {
    if (typeof value === "undefined" && !IsNull(item)) {
        if (property in item) {
            return item[property];
        }
        return null;
    }
    else {
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
            value = eval(key);
            root = true;
        }
        else {
            value = value && value[key];
        }
    });
    return (typeof value != 'undefined' && value !== null);
}
;
function In(item) {
    var any = [];
    for (var _i = 1; _i < arguments.length; _i++) {
        any[_i - 1] = arguments[_i];
    }
    if (arguments.length < 2)
        return false;
    else {
        var array = Array.prototype.slice.call(arguments, 0);
        array.splice(0, 1);
        //return array.where(function (x) {
        //    return x === item;
        //}).length > 0;
        var found = false;
        array.forEach(function (item_i) {
            if (item_i === item) {
                found = true;
            }
        });
        return found;
    }
}
function IsAllNull(item) {
    var any = [];
    for (var _i = 1; _i < arguments.length; _i++) {
        any[_i - 1] = arguments[_i];
    }
    if (arguments.length < 1)
        return false;
    else {
        var array = Array.prototype.slice.call(arguments, 0);
        array.splice(0, 0);
        var nullcount = 0;
        array.forEach(function (item_i) {
            if (IsNull(item_i)) {
                nullcount++;
            }
        });
        return nullcount == array.length;
    }
}
function IsAllNotNull(item) {
    var any = [];
    for (var _i = 1; _i < arguments.length; _i++) {
        any[_i - 1] = arguments[_i];
    }
    if (arguments.length < 1)
        return false;
    else {
        var array = Array.prototype.slice.call(arguments, 0);
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
function removeFromArray(arr) {
    var any = [];
    for (var _i = 1; _i < arguments.length; _i++) {
        any[_i - 1] = arguments[_i];
    }
    var what, a = arguments, L = a.length, ax;
    while (L > 1 && arr.length) {
        what = a[--L];
        while ((ax = arr.indexOf(what)) !== -1) {
            arr.splice(ax, 1);
        }
    }
    return arr;
}
function Equals(arg1, arg2) {
    if (typeof (arg1) == "string" && typeof (arg2) == "string") {
        return (arg1.toLowerCase() == arg2.toLowerCase());
    }
    return arg1 == arg2;
}
function GetClientQueryString(hash) {
    if (hash == null) {
        hash = window.location.hash;
    }
    var parameters = [];
    if (hash.indexOf("?") > -1) {
        hash = hash.substring(hash.indexOf("?") + 1);
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
    if (!rootid) {
        rootid == null;
    }
    var Children = items.where(function (i) {
        return i[parentproperty] == rootid;
    });
    Children.forEach(function (item) {
        var parentid = item[idproperty];
        item.Children = ToHierarchy(items, idproperty, parentproperty, parentid);
    });
    return Children;
}
;
function ForAll(hierarchy, childrenproperty, func) {
    func(hierarchy);
    if (childrenproperty in hierarchy) {
        var children = hierarchy[childrenproperty];
        if (!IsNull(children) && IsArray(children)) {
            children.forEach(function (item) {
                ForAll(item, childrenproperty, func);
            });
        }
    }
}
function Clone(obj) {
    if (null == obj || "object" != typeof obj)
        return obj;
    var copy = {};
    for (var attr in obj) {
        if (obj.hasOwnProperty(attr))
            copy[attr] = obj[attr];
    }
    return copy;
}
function IsNull(item) {
    return item == 'undefined' || item == null || (typeof (item) == "string" && item == "");
}
;
function s4() {
    return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
}
function Guid() {
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}
/*End Objects*/
/*HTML*/
function ToHtmlAttributeListString(obj) {
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
}
;
function RenderHierarchy(obj, itemformatter) {
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
}
;
function HtmlToText(html) {
    var tmp = document.createElement("DIV");
    tmp.innerHTML = html;
    return tmp.textContent || tmp.innerText || "";
}
function ToString(item) {
    return IsNull(item) ? "" : item.toString();
}
function HtmlEncode(value) {
    return $('<div/>').text(value).html();
}
function HtmlDecode(value) {
    return $('<div/>').html(value).text();
}
function Truncate(item, limit) {
    var result = "";
    if (IsNull(limit)) {
        limit = 40;
    }
    if (typeof item == "string") {
        if (item.length > limit) {
            result = item.substring(0, limit) + "...";
        }
        else {
            result = item;
        }
    }
    return result;
}
function BindEvent(selector, events, handler) {
    $(selector).unbind(events, handler).bind(events, handler);
}
;
function UnBindEvent(selector, events) {
    $(selector).unbind(events);
}
;
function ShowHide(target) {
    $(target).toggleClass("hidden");
}
/*End HTML*/
/*DateTime*/
function JsonToDate(item) {
    if (IsNull(item)) {
        return null;
    }
    var x = item.match(/\d+/)[0];
    var nr = +x;
    return new Date(nr);
}
function ToDate(item) {
    return FormatDate(JsonToDate(item));
}
function FormatDate(d, format) {
    if (IsNull(format)) {
        format = "yy/mm/dd hh:ii:ss";
    }
    if (IsNull(d)) {
        return "";
    }
    return $.formatDateTime(format, d);
}
function ToNormalDate(item) {
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
function Res(key, culture) {
    var res = key;
    res = resourcemanager.Get(key, culture);
    return res;
}
function Attribute(obj, name, value) {
    if (arguments.length == 3) {
        $(obj).attr(name, value);
    }
    if (arguments.length == 2) {
        return $(obj).attr(name);
    }
    return "";
}
function Content(obj, value) {
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
            if (result.obj) {
                result.typename = result.obj['TypeName'];
            }
        }
        if (c == parts.length - 1) {
            result.propertyname = key;
        }
        c++;
    });
    result.resourceid = Format("Models.{0}.{1}", result.typename, result.propertyname);
    var val = eval(exp);
    ;
    result.value = val ? val : "";
    return result;
}
;
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
    return paramObj;
};
function SerializeForm(selector) {
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
        }
        else {
            html += Format('<li class="file ext_{2}"><a href="#" rel="{1}{0}">{0}</a></li>', item.ID, item.Directory, item.Extension);
        }
    });
    html += "</ul>";
    return html;
}
$.fn.extend({
    padding: function (direction) {
        // calculate the values you need, using a switch statement
        // or some other clever solution you figure out
        // this now contains a wrapped set with the element you apply the 
        // function on, and direction should be one of the four strings 'top', 
        // 'right', 'left' or 'bottom'
        // That means you could probably do something like (pseudo code):
        var paddingvalue = this.css('padding-' + direction).trim();
        var intPart = "";
        var unit = paddingvalue.substring(paddingvalue.length - 2);
        intPart = paddingvalue.replace(unit, "");
        switch (unit) {
            case 'px':
                return Number(intPart);
            case 'em':
                return 0;
            default:
        }
    }
});
$.fn.extend({
    editable: function () {
        var that = this, $edittextbox = $('<input type="text"></input>').css('min-width', that.width()), submitChanges = function () {
            that.html($edittextbox.val());
            that.show();
            that.trigger('editsubmit', [that.html()]);
            $(document).unbind('click', submitChanges);
            $edittextbox.detach();
        }, tempVal;
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
function browserSupportsWebWorkers() {
    //return typeof window.Worker === "function";
    return false;
}
var Engine;
(function (Engine) {
    var ActionCenter = (function () {
        function ActionCenter() {
            this.Selector = null;
            this.CurrentSelector = null;
            this.ListSelector = null;
            this.ActionBarSelector = null;
            this.class_Error = "n-error";
            this.class_Warning = "n-warning";
            this.class_Info = "n-info";
            this.class_Success = "n-success";
            this.format_Notification = "<div class=\"notification {1}\">{0}</div>";
        }
        ActionCenter.prototype.SetSelectors = function (selector, currentselector, listselector, actionbarselector) {
            this.Selector = selector;
            this.CurrentSelector = currentselector;
            this.ListSelector = listselector;
            this.ActionBarSelector = actionbarselector;
        };
        ActionCenter.prototype.AddSuccess = function (content) {
            this.AddNotification(content, this.class_Success);
        };
        ActionCenter.prototype.AddInfo = function (content) {
            this.AddNotification(content, this.class_Info);
        };
        ActionCenter.prototype.AddWarning = function (content) {
            this.AddNotification(content, this.class_Warning);
        };
        ActionCenter.prototype.AddError = function (content) {
            this.AddNotification(content, this.class_Error);
        };
        ActionCenter.prototype.AddNotification = function (content, cssclass) {
            content = Format(this.format_Notification, content, cssclass);
            var lastmessage = $(this.CurrentSelector).html();
            $(this.CurrentSelector).html(content);
            $(this.ListSelector).prepend(lastmessage);
            $(this.Selector).show();
        };
        ActionCenter.prototype.ClearAll = function () {
            this.ClearCurrent();
            this.ClearList();
            $(this.Selector).hide();
        };
        ActionCenter.prototype.ClearCurrent = function () {
            $(this.CurrentSelector).html("");
        };
        ActionCenter.prototype.ClearList = function () {
            $(this.ListSelector).html("");
        };
        ActionCenter.prototype.ToggleListVisibility = function () {
            if ($(this.ListSelector).is(":visible")) {
                $(this.ListSelector).hide();
            }
            else {
                $(this.ListSelector).show();
            }
        };
        return ActionCenter;
    })();
    Engine.ActionCenter = ActionCenter;
    var UIManager = (function () {
        function UIManager() {
            this.duration = 200;
            this.min_width = 150;
        }
        UIManager.prototype.GetMaxWidth = function () {
            var maxwidth = $("#main-content").width();
            return maxwidth;
        };
        UIManager.prototype.ActivateList = function () {
            $("#ListController").parent().animate({ "max-width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });
            $("#SaveController").parent().animate({ "width": this.min_width + "px" }, { duration: this.duration, queue: false });
        };
        UIManager.prototype.ActivateSave = function () {
            $("#ListController").parent().animate({ "max-width": this.min_width + "px" }, { duration: this.duration, queue: false });
            $("#SaveController").parent().animate({ "width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });
        };
        return UIManager;
    })();
    Engine.UIManager = UIManager;
})(Engine || (Engine = {}));
var Editor = (function () {
    function Editor(HtmlFormat, ValueGetter, ValueSetter) {
        this.HtmlFormat = "";
        this.ValueGetter = null;
        this.ValueSetter = null;
        this.TargetValueGetter = null;
        this.TargetValueSetter = null;
        this.$Target = null;
        this.$Me = null;
        this.HtmlFormat = HtmlFormat;
        this.ValueGetter = ValueGetter;
        this.ValueSetter = ValueSetter;
    }
    Editor.prototype.Save = function () {
        this.TargetValueSetter(this.ValueGetter(this.$Me));
        this.$Target.removeClass(Editor.editclass);
        this.$Me.remove();
    };
    Editor.prototype.Load = function (Target, TargetValueGetter, TargetValueSetter) {
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
        this.$Me.blur(function () {
            me.Save();
        });
        this.$Me.keypress(function (e) {
            if (e.which == 13) {
                me.Save();
            }
        });
        this.$Me.focus();
    };
    Editor.editclass = "editing";
    return Editor;
})();
function MakeEditable2(cellselector) {
    $(cellselector).off("click");
    $(cellselector).click(function () {
        var $target = $(this);
        if (!$target.hasClass(Editor.editclass)) {
            var editor = new Editor('<input type="text" class="celleditor" value="" />', function (i) { return i.val(); }, function (i, val) { return i.val(val); });
            editor.Load($target, function () { return $target.html(); }, function () { return $target.html(editor.ValueGetter(editor.$Me)); });
        }
    });
}
function MakeEditable3(cellselector, optionObject) {
    $(cellselector).off("click");
    $(cellselector).click(function () {
        var $target = $(this);
        if (!$target.hasClass(Editor.editclass)) {
            var editor = new Editor(Format('<select class="celleditor">{0}</select>', ToOptionList(optionObject, false)), function (i) { return i.val(); }, function (i, val) {
                i.val(val);
            });
            editor.Load($target, function () { return $target.html(); }, function () { return $target.html(editor.ValueGetter(editor.$Me)); });
        }
    });
}
var testoptions = { "eba_GA:x1": "Africa", "eba_GA:x2": "EU", "eba_GA:x3": "USA sfsdg fsdfsfs" };
function MakeEditable(cellselector) {
    function SaveCell(target) {
        var parent = target.parent();
        var iskey = parent.hasClass("data-key");
        var newvalue = iskey ? target.val() : target.text();
        if (iskey && IsNull(newvalue)) {
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
                SaveCell($(this));
            });
        }
    });
}
function ToOptionList(obj, addemptyoption) {
    var result = "";
    if (addemptyoption) {
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
function NormalizeFolderPath(folder) {
    if (folder == null) {
        folder = "";
    }
    if (folder.indexOf("~") == 0) {
        folder = folder.substring(1);
    }
    if (folder[folder.lentgh - 1] != "/") {
        folder = folder + "/";
    }
    return folder;
}
function SetCustomFields(fieldcontainerselector, targetinputselector) {
    var result = [];
    $(".custom-field", $(fieldcontainerselector)).each(function (ix, item) {
        var value = $(item).val();
        var name = $(item).attr("name");
        var kv = { Key: name, Value: value };
        result.push(kv);
    });
    $(targetinputselector).val(JSON.stringify(result));
    return result;
}
function Eval(obj) {
    if (IsNull(obj)) {
        return null;
    }
    if (IsFunction(obj)) {
        return obj();
    }
    return obj;
}
function IsFunction(functionToCheck) {
    var getType = {};
    return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
}
function IsArray(value) {
    if (Array.isArray) {
        return Array.isArray(value);
    }
    return false;
}
function Split(text, delimeters, removeempty) {
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
    if (removeempty) {
        var resultwithoutempty = [];
        result.forEach(function (item) {
            if (!IsNull(item.trim())) {
                resultwithoutempty.push(item);
            }
        });
        return resultwithoutempty;
    }
    return result;
}
function Access(obj, key) {
    if (key == "this") {
        return obj;
    }
    return key.split(".").reduce(function (o, x) {
        return (typeof o == "undefined" || o === null) ? o : o[x];
    }, obj);
}
function OuterHtml(item) {
    return item[0].outerHTML;
    //return item.wrapAll('<div>').parent().html(); 
}
function Bind(target, data, parent) {
    var _this = this;
    var fBind = function (target, data, parent) { return _this.Bind.call(_this, target, data, parent); };
    var NoCheck = [];
    var targetitem = $(target);
    var bindattribute = "binding";
    var attributespecifier = "=>";
    var jquerytargets = targetitem.find("*[" + bindattribute + "]");
    var targets = [];
    if (!IsNull($(target).attr(bindattribute))) {
        targets.push(target);
    }
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
                formatString = expr; //originalexpr.replace("{" + expr + "}", "{0}");
            }
            else {
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
                values.push(val);
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
            }
            else {
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
function Replace(text, texttoreplace, textwithreplace) {
    /*
    var reg = new RegExp(texttoreplace, "g");
    return text.replace(reg, textwithreplace);
    */
    var index = 0;
    do {
        text = text.replace(texttoreplace, textwithreplace);
    } while ((index = text.indexOf(texttoreplace, index + 1)) > -1);
    return text;
}
function GetProperties(item) {
    var properties = [];
    for (var propertyName in item) {
        if (item.hasOwnProperty(propertyName)) {
            var propertyValue = item[propertyName];
            var kv = new General.KeyValue();
            kv.Key = propertyName;
            kv.Value = propertyValue;
            properties.push(kv);
        }
    }
    return properties;
}
var S_Bind_Start = "bind[";
var S_Bind_End = "]";
var s_list_selector = ".list";
var s_listpager_selector = ".listpager";
var s_listfilter_selector = ".listfilter";
var s_sublist_selector = ".sublist";
var s_sublistpager_selector = ".sublistpager";
var s_detail_selector = ".detail";
var s_parent_selector = ".parent";
var s_contentcontainer_selector = ".contentcontainer";
var s_content_selector = ".subcontent";
var actioncenter = new Engine.ActionCenter();
var uimanager = new Engine.UIManager();
var resourcemanager = { Get: function (key, culture) {
    return key;
} };
var activeItem = null;
//# sourceMappingURL=Utils.js.map