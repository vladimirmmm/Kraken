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
function LoadPageAsync($bindtarget, $pager, functionwithcallback, page, pagesize, parameters, events) {
    var me = this;
    var startix = pagesize * page;
    var endix = startix + pagesize;
    functionwithcallback.Callback = function (result) {
        CallFunction(events, "onloading", result.Items);
        BindX($bindtarget, result.Items);
        CallFunction(events, "onloaded", result.Items);
        if ($pager.length == 0 || 1 == 1) {
            $pager.pagination(result.Total, {
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
                    LoadPageAsync($bindtarget, $pager, functionwithcallback, pageix, pagesize, parameters, events);
                    CallFunction(events, "onpaged");
                    return false;
                },
            });
        }
        else {
        }
    };
    if (IsNull(parameters)) {
        parameters = {};
    }
    if (!("page" in parameters)) {
        parameters["page"] = page;
    }
    if (!("pagesize" in parameters)) {
        parameters["pagesize"] = pagesize;
    }
    functionwithcallback.Call(parameters);
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
function HtmlEncode(value) {
    return $('<div/>').text(value).html();
}
function HtmlDecode(value) {
    return $('<div/>').html(value).text();
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
function FormatDate(d, format) {
    if (IsNull(format)) {
        format = "yy/mm/dd hh:ii:ss";
    }
    if (IsNull(d)) {
        return "";
    }
    //return $.formatDateTime(format, d);
    return d.format(format);
}
// a global month names array
var gsMonthNames = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');
// a global day names array
var gsDayNames = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');
String.prototype.zf = function (l) {
    return '0'.string(l - this.length) + this;
};
String.prototype.string = function (l) {
    var s = '', i = 0;
    while (i++ < l) {
        s += this;
    }
    return s;
};
Number.prototype.zf = function (l) {
    return this.toString().zf(l);
};
// the date format prototype
Date.prototype.format = function (f) {
    if (!this.valueOf())
        return 'n.a.'; //&nbsp;
    var d = this;
    return f.replace(/(yyyy|yy|y|MMMM|MMM|MM|M|dddd|ddd|dd|d|HH|H|hh|h|mm|m|ss|s|t)/gi, function ($1) {
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
    });
};
function Attribute(obj, name, value) {
    if (arguments.length == 3) {
        $(obj).attr(name, value);
    }
    if (arguments.length == 2) {
        return $(obj).attr(name);
    }
    return "";
}
function Css(obj, name, value) {
    if (arguments.length == 3) {
        $(obj).css(name, value);
    }
    if (arguments.length == 2) {
        return $(obj).css(name);
    }
    return "";
}
function Value(obj, value) {
    if (arguments.length == 2) {
        $(obj).val(value);
    }
    if (arguments.length == 1) {
        return $(obj).val();
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
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    var paramObj = {};
    $.each(a, function (_, kv) {
        paramObj[kv.name] = kv.value;
    });
    return paramObj;
};
function SerializeForm(selector) {
    return $(selector).serializeObject();
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
function _SetFunctions() {
    _Select = function (CssSelector, from) {
        if (IsNull(from)) {
            return ToElements($(CssSelector));
        }
        else {
            return ToElements($(CssSelector, from));
        }
    };
    _SelectFirst = function (CssSelector, from) {
        var elements = _Select(CssSelector, from);
        if (elements.length > 0) {
            return elements[0];
        }
        return null;
    };
    _AddEventHandler = function (element, eventname, handler) {
        $(element).on(eventname, handler);
    };
    _RemoveEventHandler = function (element, eventname, handler) {
        $(element).unbind(eventname, handler);
    };
    _EnsureEventHandler = function (element, eventname, handler) {
        _RemoveEventHandler(element, eventname, handler);
        _AddEventHandler(element, eventname, handler);
    };
    _Attribute = Attribute;
    _Property = function (element, propertyname) {
        return $(element).prop(propertyname);
    };
    _Value = Value;
    _Html = Content;
    _Remove = function (element) {
        $(element).remove();
    };
    _Append = function (target, element) {
        $(target).append(element);
    };
    _After = function (target, element) {
        $(target).after(element);
    };
    _Before = function (target, element) {
        $(target).before(element);
    };
    _HasClass = function (element, classname) { return $(element).hasClass(classname); };
    _AddClass = function (element, classname) {
        $(element).addClass(classname);
    };
    _RemoveClass = function (element, classname) {
        $(element).removeClass(classname);
    };
    _Css = Css;
    _Focus = function (element) {
        $(element).focus();
    };
    _Clone = function (element) { return $(element).clone()[0]; };
}
_SetFunctions();
//# sourceMappingURL=JqueryUtils.js.map