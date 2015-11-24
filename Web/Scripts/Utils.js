var _Select = function (CssSelector, from) { return null; };
var _SelectFirst = function (CssSelector, from) { return null; };
var _Find = function (element, CssSelector) { return null; };
var _FindFirst = function (element, CssSelector) { return null; };
var _Parent = function (element, selector) { return null; };
var _Parents = function (element, selector) { return []; };
var _Children = function (element, CssSelector) { return null; };
var _FirstChildren = function (element, CssSelector) { return null; };
var _AddEventHandler = function (element, eventname, handler) {
};
var _RemoveEventHandler = function (element, eventname, handler) {
};
var _RemoveEventHandlers = function (element, eventname) {
};
var _EnsureEventHandler = function (element, eventname, handler) {
};
var _Attribute = function (element, attributename, attributevalue) { return ""; };
var _RemoveAttribute = function (target, attributename) {
};
var _Property = function (element, propertyname) { return ""; };
var _Value = function (element, value) { return ""; };
var _Html = function (element, html) { return ""; };
var _Text = function (element, text) { return ""; };
var _Remove = function (element) {
};
var _Append = function (target, element) {
};
var _After = function (target, element) {
};
var _Before = function (target, element) {
};
var _HasClass = function (element, classname) { return false; };
var _AddClass = function (element, classname) {
};
var _RemoveClass = function (element, classname) {
};
var _Css = function (element, value) {
};
var _Width = function (element, value) { return -1; };
var _Height = function (element, value) { return -1; };
var _Focus = function (element) {
};
var _Show = function (element) {
};
var _Hide = function (element) {
};
var _IsVisible = function (element) { return false; };
var _Clone = function (element) { return null; };
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
function RemoveFrom(item, items) {
    var ix = items.indexOf(item);
    items.splice(ix, 1);
}
function CallFunctionFrom(eventcontainer, eventname, args) {
    if (!IsNull(eventcontainer)) {
        if (eventname in eventcontainer && IsFunction(eventcontainer[eventname])) {
            eventcontainer[eventname](args);
        }
    }
}
function CallFunction(func, args) {
    if (!IsNull(func) && IsFunction(func)) {
        return func.apply(this, args);
    }
}
function CallFunctionWithContext(context, func, args) {
    if (!IsNull(func) && IsFunction(func)) {
        return func.apply(context, args);
    }
    return null;
}
function asyncFunc(func) {
    setTimeout(function () {
        func();
    }, 10);
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
    if (IsDesktop()) {
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
function TextBetween(text, begintag, endtag, withtags) {
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
    if (withtags) {
        result = begintag + result + endtag;
    }
    return result;
}
;
function TextsBetween(text, begintag, endtag, withtags) {
    var result = [];
    while (text.indexOf(begintag) > -1 && text.indexOf(begintag) > -1) {
        var item = TextBetween(text, begintag, endtag);
        if (withtags) {
            var fullitem = begintag + item + endtag;
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
function FormatSimpleTest() {
    var any = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        any[_i - 0] = arguments[_i];
    }
    var args = Array.prototype.slice.call(arguments, 1);
    var format = arguments[0];
    return format.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
}
;
function FormatSimple(format, args) {
    return format.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
}
;
function Format() {
    var any = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        any[_i - 0] = arguments[_i];
    }
    var args = Array.prototype.slice.call(arguments, 1);
    //if (args.length == 1)
    //{
    //    if (IsArray(args[0]))
    //    {
    //        args = args[0];
    //    }
    //    //if 
    //}
    var format = arguments[0];
    if (format.indexOf("d}") == -1 || format.indexOf("{{") == -1) {
        return FormatSimple(format, args);
    }
    format = Replace(format, "{{", "xF<w&");
    format = Replace(format, "}}", "xF>w&");
    var result = format;
    var parts = TextsBetween(format, "{", "}", true);
    parts.forEach(function (item, ix) {
        var ix = -1;
        var inner = item.substring(1, item.length - 1);
        var partformat = "";
        if (inner.indexOf(":") > -1) {
            ix = Number(inner.substring(0, inner.indexOf(":")));
            partformat = inner.substring(inner.indexOf(":") + 1);
        }
        else {
            ix = Number(inner);
        }
        var arg = args[ix];
        if (!IsNull(format)) {
            if (arg instanceof Date) {
                arg = FormatDate(arg, partformat);
            }
            if (IsNumeric(arg) && (!(arg instanceof Date))) {
                if (partformat.toLowerCase().indexOf("d") == 0) {
                    var padnr = Number(partformat.substring(1));
                    arg = pad(Number(arg), padnr, "0", 0);
                }
            }
        }
        result = Replace(result, item, arg);
    });
    return result;
}
;
function IsNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
//string/number,length=2,char=0,0/false=Left-1/true=Right
function pad(a, b, c, d) {
    return a = (a || c || 0) + '', b = new Array((++b || 3) - a.length).join(c || 0), d ? a + b : b + a;
}
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
    //return item == 'undefined' || item == null || (typeof (item) == "string" && item == "");
    return item === undefined || item == null || item === "";
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
/*
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
*/
function RenderHierarchy(obj, itemformatter, level) {
    if (level === void 0) { level = 0; }
    var html = "";
    var children = obj["Children"];
    children = IsNull(children) ? [] : children;
    if (level > 0) {
        html += "<li>";
        html += itemformatter(obj);
    }
    if (children.length > 0) {
        html += "<ul>";
        children.forEach(function (item) {
            html += RenderHierarchy(item, itemformatter, level + 1);
        });
        html += "</ul>";
    }
    if (level > 0) {
        html += "</li>\n";
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
/*
HTMLElement.prototype.toString = function () {
    var html = (<HTMLElement>this).outerHTML;
    var result = "<" + TextBetween(html, "<", ">") + ">";
    return result;
}
*/
function Res(key, culture) {
    var res = key;
    res = resourcemanager.Get(key, culture);
    return res;
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
function browserSupportsWebWorkers() {
    //return typeof window.Worker === "function";
    return false;
}
/*
function MakeEditable2(cellselector)
{
    $(cellselector).off("click");
    $(cellselector).click(function () {
        var target = <Element>this;
        if (!_HasClass(target, Editor.editclass)) {
            var editor = new Editor('<input type="text" class="celleditor" value="" />',(i: JQuery) => i.val(), (i: JQuery, val: any) => i.val(val));
            editor.Load(target,() => _Html(target), () => _Html(target, editor.ValueGetter(editor.$Me)));
        }

    });
}

function MakeEditable3(cellselector, optionObject) {
    $(cellselector).off("click");
    $(cellselector).click(function () {
        var target = <Element>this;
        if (!_HasClass(target, Editor.editclass)) {
            var editor = new Editor(Format('<select class="celleditor">{0}</select>', ToOptionList(optionObject, false)),(i: JQuery) => i.val(),(i: JQuery, val: any) => { i.val(val); });
            editor.Load(target,() => _Html(target),() => _Html(target, editor.ValueGetter(editor.$Me)));
        }
    });
}
*/
function Editable(cellselector, editedcallback) {
    var targets = _Select(cellselector);
    _AddEventHandler(targets, "click", function (event) {
        var target = event.currentTarget;
        if (!_HasClass(target, Editor.editclass)) {
            var editor = new Editor('<input type="text" class="celleditor " value="" />', function (i) { return i.val(); }, function (i, val) { return i.val(val); });
            editor.Load(target, function () { return _Html(target); }, function () {
                var value = editor.ValueGetter(editor.$Me);
                _Html(target, value);
                editedcallback(target, value);
            });
        }
    });
}
function GetDefaultEditor(target) {
    var editor = new Editor('<input type="text" class="celleditor " value="" />', function (i) { return i.val(); }, function (i, val) { return i.val(val); });
    return editor;
}
function EditCell(cell, editorAccessor, editedcallback) {
    if (!_HasClass(cell, Editor.editclass)) {
        var editor = editorAccessor(cell);
        editor.Load(cell, function () { return _Html(cell); }, function () {
            var value = editor.ValueGetter(editor.$Me);
            _Html(cell, value);
            editedcallback(cell, value);
        });
    }
}
function AssignEditor(targetsaccessor, editorAccessor, editedcallback) {
    var targets = targetsaccessor();
    _AddEventHandler(targets, "click", function (event) {
        var target = event.currentTarget;
        if (!_HasClass(target, Editor.editclass)) {
            var editor = editorAccessor(target);
            editor.Load(target, function () { return _Html(target); }, function () {
                var value = editor.ValueGetter(editor.$Me);
                _Html(target, value);
                editedcallback(target, value);
            });
        }
    });
}
/*
function AssignEditor(cellselector: any, editorAccessor: Function, editedcallback: Function) {
    var targets = _Select(cellselector);
    _AddEventHandler(targets, "click", function (event: any) {
        var target = event.currentTarget;

        if (!_HasClass(target, Editor.editclass)) {
            var editor = editorAccessor(target);

            editor.Load(target,
                () => _Html(target),
                () => {
                    var value = editor.ValueGetter(editor.$Me);
                    _Html(target, value);
                    editedcallback(target, value);
                }
                );
        }
    });
}
*/
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
    var result = key.split(".").reduce(function (o, x) {
        return (typeof o == "undefined" || o === null) ? o : o[x];
    }, obj);
    return IsNull(result) ? "" : result;
}
function OuterHtml(item) {
    return item[0].outerHTML;
    //return item.wrapAll('<div>').parent().html(); 
}
function Replace(text, texttoreplace, textwithreplace) {
    if (IsNull(texttoreplace)) {
        return text;
    }
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
function GetPropertiesArray(item) {
    var properties = [];
    for (var propertyName in item) {
        if (item.hasOwnProperty(propertyName)) {
            var propertyValue = item[propertyName];
            properties.push(propertyValue);
        }
    }
    return properties;
}
//# sourceMappingURL=Utils.js.map