/// <reference path="typings/jquery/jquery.d.ts" />
var logitems: number = 0;
function Log(item:string)
{
    logitems++;
    var element = _SelectFirst("#contentlog");

    if (logitems > 1000)
    {
        _Html(element, "");
        logitems = 1;
    }
    item = Replace(item, "\r\n", "<br/>");
    $(element).append(Format("{0}<br/>", item));
    element.scrollTop = element.scrollHeight;
}


function BrowseFile(callback: Function)
{
    var uploader = _SelectFirst("#fileuploader");
    _EnsureEventHandler(uploader, "change", function () {
        var file = _Value(uploader);
        Log("FB: " + file);
        CallFunction(callback, [file]);
    });
    $(uploader).click();

}
jQuery.fn.selectText = function () {
    this.find('input').each(function () {
        if ($(this).prev().length == 0 || !$(this).prev().hasClass('p_copy')) {
            $('<p class="p_copy" style="position: absolute; z-index: -1;"></p>').insertBefore($(this));
        }
        $(this).prev().html($(this).val());
    });
    var doc = document;
    var element = this[0];
    console.log(this, element);
    if (window.getSelection) {
        var selection = window.getSelection();
        var range:Range = document.createRange();
        range.selectNodeContents(element);
        selection.removeAllRanges();
        selection.addRange(range);
    }
};

function BrowseFolder(callback: Function)
{
    var uploader = _SelectFirst("#fileuploader");
    _EnsureEventHandler(uploader, "change", function () {
        var file = _Value(uploader);
        Log("FB: " + file);
        CallFunction(callback, [file]);

    });
    $(uploader).click();

}
function Select(sender): any {
    var $command = $(sender);
    var sel = "selected";
    var $commands = $command.parent().children();//a
    $commands.removeClass(sel);
    $command.addClass(sel);

    var selectfromlist = function (item: JQuery, items: JQuery) {
        if (item.length > 0) {
            items.removeClass(sel);
            item.addClass(sel);
        }
    };

    var $list = $command.parents(".list").first();
    if ($list.length == 1) {
        var tag: string = $list.prop("tagName");
        tag = tag.toLowerCase();
        var $listitem: JQuery = null;
        var $listitems: JQuery = null;
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

function LoadPage($bindtarget: JQuery, $pager: JQuery, data: any, page: number, pagesize: number, events?: Object) {
    var me = this;
    var startix = pagesize * page;
    var endix = startix + pagesize;
    var itemspart = GetPart(data, startix, endix);
    var datalength = GetLength(data);
    CallFunctionFrom(events, "onloading", itemspart);
    BindX($bindtarget, itemspart);
    CallFunctionFrom(events, "onloaded", itemspart);

    if ($pager.length == 0 || 1 == 1) {
        $pager.pagination(datalength,
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
                    CallFunctionFrom(events, "onpaging");
                    LoadPage($bindtarget, $pager, data, pageix, pagesize, events);
                    CallFunctionFrom(events, "onpaged");
                    return false;
                },
            });
    } else {
        //console.log("")
    }

}

function LoadPageAsync($bindtarget: JQuery, $pager: JQuery,
    functionwithcallback: General.FunctionWithCallback,
    page: number, pagesize: number, parameters:Object, events?: Object) {
    var me = this;
    var startix = pagesize * page;
    var endix = startix + pagesize;
    functionwithcallback.Callback = (result: DataResult) => {

        CallFunctionFrom(events, "onloading", result.Items);
        BindX($bindtarget, result.Items);
        CallFunctionFrom(events, "onloaded", result.Items);

        if ($pager.length == 0 || 1 == 1) {
            $pager.pagination(result.Total,
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
                        CallFunctionFrom(events, "onpaging");
                        LoadPageAsync($bindtarget, $pager, functionwithcallback,
                            pageix, pagesize, parameters, events);
                        CallFunctionFrom(events, "onpaged");
                        return false;
                    },
                });
        } else {
            //console.log("")
        }
    };
    if (IsNull(parameters)) { parameters = {}; }
    SetProperty(parameters, "page", page);
    SetProperty(parameters, "pagesize", pagesize);
    //if (!("page" in parameters)) { parameters["page"] = page; }
    //if (!("pagesize" in parameters)) { parameters["pagesize"] = pagesize;}
    functionwithcallback.Call(parameters);




}

function Ajax(url: string, method: string, parameters: Dictionary, generichandler: Function, contentType?: string) {
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
    var params: Object = parameters;
    if (method.toLowerCase() == "get") {
        params = ToObjectX(parameters); //Clone(parameters);
    }
    if (method.toLowerCase() == "post") {
        params = JSON.stringify(parameters);
    }
    //StartProgress("ajax");
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
            Log("Error at " + Id + ": " + errormsg);
            generichandler(result);

        }
    });
}

function ToElements(item: JQuery): Element[] {
    var items: Element[] = [];
    item.each(function (ix, element) {
        items.push(element);
    });
    return items;
}

function HtmlEncode(value: string): string {
    return $('<div/>').text(value).html();
}

function HtmlDecode(value: string): string {
    return $('<div/>').html(value).text();
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

function FormatDate(d: Date, format?: string): string {
    if (IsNull(format)) { format = "yy/mm/dd hh:ii:ss"; }
    if (IsNull(d)) { return ""; }
    //return $.formatDateTime(format, d);
    return d.format(format);
}
interface Date
{
    format(format: string): string;
}
interface String
{
    zf: Function;
    string:Function
}
interface Number {
    zf: Function;

}

// a global month names array
var gsMonthNames = new Array(
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December'
    );
// a global day names array
var gsDayNames = new Array(
    'Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday'
    );
String.prototype.zf = function (l) { return '0'.string(l - this.length) + this; }
String.prototype.string = function (l) { var s = '', i = 0; while (i++ < l) { s += this; } return s; }
Number.prototype.zf = function (l) { return this.toString().zf(l); }
// the date format prototype
Date.prototype.format = function (f) {
    if (!this.valueOf())
        return 'n.a.';//&nbsp;

    var d = this;
    return f.replace(/(yyyy|yy|y|MMMM|MMM|MM|M|dddd|ddd|dd|d|HH|H|hh|h|mm|m|ss|s|t)/gi,
        function ($1) {
            var h = null;
            switch ($1) {
                case 'yyyy': return d.getFullYear();
                case 'yy': return (d.getFullYear() % 100).zf(2);
                case 'y': return (d.getFullYear() % 100);
                case 'MMMM': return gsMonthNames[d.getMonth()];
                case 'MMM': return gsMonthNames[d.getMonth()].substr(0, 3);
                case 'MM': return (d.getMonth() + 1).zf(2);
                case 'M': return (d.getMonth() + 1);
                case 'dddd': return gsDayNames[d.getDay()];
                case 'ddd': return gsDayNames[d.getDay()].substr(0, 3);
                case 'dd': return d.getDate().zf(2);
                case 'd': return d.getDate();
                case 'HH': return d.getHours().zf(2);
                case 'H': return d.getHours();
                case 'hh': return ((h = d.getHours() % 12) ? h : 12).zf(2);
                case 'h': return ((h = d.getHours() % 12) ? h : 12);
                case 'mm': return d.getMinutes().zf(2);
                case 'm': return d.getMinutes();
                case 'ss': return d.getSeconds().zf(2);
                case 's': return d.getSeconds();
                case 't': return d.getHours() < 12 ? 'A.M.' : 'P.M.';
            }
        }
        );
}
function JqueryAttribute(obj, name: string, value?: string): string {
    if (value === undefined) {
        return $(obj).attr(name);
    }
    else {

        $(obj).attr(name, value);
        return value;
    }
}
function Attribute(obj, name: string, value?: string): string {
    if (obj instanceof Element) {
        if (value === undefined) {
            var result = "";
            if (name in obj) {
                result = obj[name];
            } else
            {
                result = obj.getAttribute(name);
            }
            return result === undefined ? "" : result;
        }
        else {

            if (name in obj) {
                obj[name] = value;
            } else {
                obj.setAttribute(name, value);
            }
            return value;
        }
    }
    else {

        return JqueryAttribute(obj, name, value);
    }

}
function JqueryContent(obj, value?: string): string {
    if (value === undefined) {
        return $(obj).html();
    }
    else {

        $(obj).html(value);
        return value;
    }
}
function Content(obj, value?: string): string {
    if (obj instanceof Element) {
        if (value === undefined) {
            var result = obj.innerHTML;
            
            return result === undefined ? "" : result;
        }
        else {
            obj.innerHTML = value;
            return value;
        }
    }
    else {

        return JqueryContent(obj, value);
    }

}

function Css(obj, name: string, value?: string): string {
    return CallJQueryFunction(obj, "css", name, value);
}

function Value(obj, value?: string): string {
    return CallJQueryFunction(obj, "val", "", value);
}

//function Content(obj, value?: string): string {
//    return CallJQueryFunction(obj, "html", "", value);

//}
function XText(obj, value?: string): string {
    return CallJQueryFunction(obj, "text", "", value);

}
//function CallJQueryFunction(obj, functionname: string, property: string, value?: string): string {

//}
function CallJQueryFunction(obj, functionname: string, property: string, value?: string): string {
    //var jqueryfunction = $(obj)[functionname]; 
    var jqueryfunction = (...args: any[]): string => { return CallFunctionWithContext($(obj), $(obj)[functionname], args); }; 

    var result = "";
    if (IsNull(property)) {
        if (value == undefined) {
            result = jqueryfunction();
        } else
        {
            jqueryfunction(value);
            result =  value;
        }
    }
    else
    {
        if (value == undefined) {
            result = jqueryfunction(property);
        } else {
            jqueryfunction(property,value);
            result = value;
        }
    }
    return IsNull(result) ? "" : result;
    
}

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();

    var paramObj = {};
    $.each(a, function (_, kv) {
        paramObj[kv.name] = kv.value;
    });
    return paramObj
};

function SerializeForm(selector): Dictionary {
    return $(selector).serializeObject();
}

$.fn.extend({
    padding: function (direction: string): number {
        // calculate the values you need, using a switch statement
        // or some other clever solution you figure out

        // this now contains a wrapped set with the element you apply the 
        // function on, and direction should be one of the four strings 'top', 
        // 'right', 'left' or 'bottom'

        // That means you could probably do something like (pseudo code):
        var paddingvalue: string = this.css('padding-' + direction).trim();
        var intPart = "";
        var unit = paddingvalue.substring(paddingvalue.length - 2);
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
jQuery.fn.center = function () {
    this.css("position", "fixed");
    this.css("top",($(window).height() / 2) - (this.outerHeight() / 2));
    this.css("left",($(window).width() / 2) - (this.outerWidth() / 2));
    return this;
}

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

function BindVash(target: any, data: any, parent?: any) {
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
function GetFromData(form: Element):any[]
{
    return $(form).serializeArray();
}
function ConvertFormDataToObj(formdata: any[]): Object
{
    var result = {};
    formdata.forEach(function (value) {
        var key = value["name"];
        var value = value["value"];
        if (!IsNull(key))
        {
            result[key] = value;
        }
    });

    return result;
}
function _SetFunctions() {
    _Select = function (CssSelector: string, from?: HTMLElement) {
        if (IsNull(from)) {
            return ToElements($(CssSelector));

        } else
        {
            return ToElements($(CssSelector, from));

        }
    };

    _SelectFirst = function (CssSelector: string, from?: HTMLElement) {
        var elements = _Select(CssSelector, from);
        if (elements.length > 0)
        {
            return elements[0]
        }
        return null;
    };

    _Find = function (element:Element, CssSelector: string):Element[] {
        return ToElements($(element).find(CssSelector));
    };

    _FindFirst = function (element: Element, CssSelector: string): Element {
        var elements = _Find(element, CssSelector);
        if (elements.length > 0) {
            return elements[0]
        }
        return null;
    };

    _Children = function (element: Element, CssSelector?: string): Element[] {
        return ToElements($(element).children(CssSelector));
    };

    _FirstChildren = function (element: Element, CssSelector?: string): Element {
        var elements = _Children(element, CssSelector);
        if (elements.length > 0) {
            return elements[0]
        }
        return null;
    };

    _Width = function (target: Element, value?: any): number {
        if (!IsNull(value)) {
            $(target).width(value);
        }
        return $(target).width();
    };

    _Height = function (target: Element, value?: any): number {
        if (!IsNull(value)) {
            $(target).width(value);
        }
        return $(target).width();
    };

    _Parent = function (target: Element, selector?: string):Element {
        var parent = $(target).parent(selector)
        return parent.length == 0 ? null : parent[0];
    };

    _Parents = function (target: Element, selector?: string): Element[] {
        var parents = $(target).parents(selector)
        return ToElements(parents);
    };

    _AddEventHandler = function (element: any, eventname: string, handler: any) {
        $(element).on(eventname, handler);
    };

    _RemoveEventHandler = function (element: any, eventname: string, handler: any) {
        $(element).unbind(eventname, handler);
    };

    _EnsureEventHandler = function (element: any, eventname: string, handler: any) {
        _RemoveEventHandler(element, eventname, handler);
        _AddEventHandler(element, eventname, handler);
    };

    _RemoveEventHandlers = function (elements: any, eventname: string) {
        $(elements).off(eventname);
    };



    _Attribute = Attribute;
    _RemoveAttribute = (target: any, attributename: string) => { $(target).removeAttr(attributename);};

    _Property = function (element: Element, propertyname: string) {
        return $(element).prop(propertyname);
    };
    _TagName = function (element: Element) {
        return IsNull(element) ? "" : element.tagName;
    };
    _Value = Value;

    _Html = Content;
    _Text = XText;

    _Remove = (element: any) => { $(element).remove(); };
    _Append = (target: Element, element: Element) => { $(target).append(element); };
    _After = (target: Element, element: Element) => { $(target).after(element); };
    _Before = (target: Element, element: Element) => { $(target).before(element); };

    _HasClass = (element: any, classname: string): boolean => $(element).hasClass(classname);
    _AddClass = (element: any, classname: string) => { $(element).addClass(classname) };
    _RemoveClass = (element: any, classname: string) => { $(element).removeClass(classname) };

    _Css = Css;


    _Focus = (element: Element) => { $(element).focus(); };
    _Show = (element: Element) => {
        //$(element).show();
        _RemoveClass(element, "hidden");
    };
    _Hide = (element: Element) => {
        //$(element).hide();
        _AddClass(element, "hidden");
    };
    _IsVisible = (element: Element): boolean => { return $(element).is(':visible'); };
    _Clone = (element: Element) : Element => $(element).clone()[0];
}
_SetFunctions();