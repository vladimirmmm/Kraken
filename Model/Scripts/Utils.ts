
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
}

interface External {
    Notify(obj: any)
}
function ErrorHandler(errorMsg, url, lineNumber) {
    var errortext = 'Error: ' + errorMsg + ' Script: ' + url + ' Line: ' + lineNumber;
    Notify(errortext);
    return true;
}
if (typeof console === "undefined") {

    window.onerror = ErrorHandler;
}
function Notify(notification) {
    if ('Notify' in window.external) {
        window.external.Notify(notification);
    } else {
        console.log(notification);
    }
}

//function SetExtension(extension) {
//    var li = <Model.LayoutItem>JSON.parse(extension);

//    if (li != null && 'LabelContent' in li) {

//        $("#Extension").html(li.LabelContent);
//        $("#Extension").attr("title", li.FactString);

//    }
//}
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
function ToObject(items:General.KeyValue[]):Object
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
}
var StopProgress: F_Progress = function (id: string) { return null; };
var StartProgress: F_Progress = function (id: string) { return null; };
var ResultFormatter: F_ResultFormatter = function (rawdata) { return rawdata};

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
};
function Format(...any):string {
    var args = Array.prototype.slice.call(arguments, 1);
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
        var array = Array.prototype.slice.call(arguments, 0);
        array.splice(0, 1);
        return array.where(function (x) {
            return x === item;
        }).length > 0;
    }
}

function IsAllNull(item, ...any) {
    if (arguments.length < 1)
        return false;
    else {
        var array = Array.prototype.slice.call(arguments, 0);
        array.splice(0, 0);
        return array.where(function (x) {
            return !IsNull(x);
        }).length > 0;
    }
}

function IsAllNotNull(item, ...any) {
    if (arguments.length < 1)
        return false;
    else {
        var array = Array.prototype.slice.call(arguments, 0);
        array.splice(0, 0);
        var nulls = array.where(function (x) { return IsNull(x); });
        return nulls.length == 0;
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
  
            var editor = new Editor(Format('<select class="celleditor">{0}</select>', ToOptionList(optionObject)),(i: JQuery) => i.val(),(i: JQuery, val: any) => { i.val(val); });
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

function ToOptionList(obj: Object): string
{
    var result = "";
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

var actioncenter = new Engine.ActionCenter();
var uimanager = new Engine.UIManager();
var resourcemanager: IResourceManager = { Get: function (key: string, culture?: string) { return key;}};
interface IResourceManager
{
    Get(key: string, culture?: string): string;
}
var activeItem = null;


