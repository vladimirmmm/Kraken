
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

function ShowContent(selector: string, sender: any) {
    var id = selector.replace("#", "");
    var $activator = $(sender);// $("[activator-for=" + id + "]");
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

function ShowContentByID(selector: string) {
    var id = selector.replace("#", "");
    var $activator = $("[activator-for=" + id + "]").first();
    ShowContent(selector, $activator);
    return $activator;
}

function ShowContentBySender(sender: any) {
    var $activator = $(sender);
    var targetselector = "#" + $activator.attr("activator-for");
    ShowContent(targetselector, sender);

    return $activator;

}

function LoadPage($bindtarget: JQuery, $pager: JQuery, data: any, page: number, pagesize: number, events?: Object) {
    var me = this;
    var startix = pagesize * page;
    var endix = startix + pagesize;
    var itemspart = GetPart(data, startix, endix);
    var datalength = GetLength(data);
    CallFunction(events, "onloading", itemspart);
    BindX($bindtarget, itemspart);
    CallFunction(events, "onloaded", itemspart);

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
                    CallFunction(events, "onpaging");
                    LoadPage($bindtarget, $pager, data, pageix, pagesize, events);
                    CallFunction(events, "onpaged");
                    return false;
                },
            });
    } else {
        //console.log("")
    }

}

function LoadPageAsync($bindtarget: JQuery, $pager: JQuery,
    functionwithcallback: FunctionWithCallback,
    page: number, pagesize: number, parameters:Object, events?: Object) {
    var me = this;
    var startix = pagesize * page;
    var endix = startix + pagesize;
    functionwithcallback.Callback = (result: DataResult) => {

        CallFunction(events, "onloading", result.Items);
        BindX($bindtarget, result.Items);
        CallFunction(events, "onloaded", result.Items);

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
                        CallFunction(events, "onpaging");
                        LoadPageAsync($bindtarget, $pager, functionwithcallback,
                            pageix, pagesize, parameters, events);
                        CallFunction(events, "onpaged");
                        return false;
                    },
                });
        } else {
            //console.log("")
        }
    };
    if (IsNull(parameters)) { parameters = {};}
    if (!("page" in parameters)) { parameters["page"] = page; }
    if (!("pagesize" in parameters)) { parameters["pagesize"] = pagesize;}
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

function Attribute(obj, name: string, value?: string): string {
    if (arguments.length == 3) {
        $(obj).attr(name, value);
    }
    if (arguments.length == 2) {
        return $(obj).attr(name);
    }
    return "";
}

function Css(obj, name: string, value?: string): string {
    if (arguments.length == 3) {
        $(obj).css(name, value);
    }
    if (arguments.length == 2) {
        return $(obj).css(name);
    }
    return "";
}

function Value(obj, value?: string): string {
    if (arguments.length == 2) {
        $(obj).val(value);
    }
    if (arguments.length == 1) {
        return $(obj).val();
    }
    return "";
}

function Content(obj, value?: string): string {
    if (arguments.length == 2) {
        $(obj).html(value);
    }
    if (arguments.length == 1) {
        return $(obj).html();
    }
    return "";
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


    _AddEventHandler = function (element: HTMLElement, eventname: string, handler: any) {
        $(element).on(eventname, handler);
    };

    _RemoveEventHandler = function (element: HTMLElement, eventname: string, handler: any) {
        $(element).unbind(eventname, handler);
    };

    _EnsureEventHandler = function (element: HTMLElement, eventname: string, handler: any) {
        _RemoveEventHandler(element, eventname, handler);
        _AddEventHandler(element, eventname, handler);
    };

    _Attribute = Attribute;

    _Property = function (element: Element, propertyname: string) {
        return $(element).prop(propertyname);
    };

    _Value = Value;

    _Html = Content;

    _Remove = (element: Element) => { $(element).remove(); };
    _Append = (target: Element, element: Element) => { $(target).append(element); };
    _After = (target: Element, element: Element) => { $(target).after(element); };
    _Before = (target: Element, element: Element) => { $(target).before(element); };

    _HasClass = (element: Element, classname: string): boolean => $(element).hasClass(classname);
    _AddClass = (element: Element, classname: string) => { $(element).addClass(classname) };
    _RemoveClass = (element: Element, classname: string) => { $(element).removeClass(classname) };

    _Css = Css;


    _Focus = (element: Element) => { $(element).focus(); };

    _Clone = (element: Element) : Element => $(element).clone()[0];
}
_SetFunctions();