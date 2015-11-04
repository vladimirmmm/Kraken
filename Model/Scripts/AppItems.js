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
        this.CustomTrigger = null;
        this.$Target = null;
        this.$Me = null;
        this.HtmlFormat = HtmlFormat;
        this.ValueGetter = ValueGetter;
        this.ValueSetter = ValueSetter;
    }
    Editor.prototype.Save = function () {
        var new_value = this.ValueGetter(this.$Me);
        this.$Target.removeClass(Editor.editclass);
        this.$Me.remove();
        if (this.Original_Value != new_value) {
            this.TargetValueSetter(new_value);
        }
        else {
            _Html(this.$Target[0], this.Original_Value);
        }
    };
    Editor.prototype.Load = function (TargetElement, TargetValueGetter, TargetValueSetter) {
        var Target = $(TargetElement);
        var me = this;
        this.TargetValueGetter = TargetValueGetter;
        this.TargetValueSetter = TargetValueSetter;
        this.Original_Value = TargetValueGetter().trim();
        this.$Me = $(Format(this.HtmlFormat, this.Original_Value));
        //setting UI
        var t_width = Target.width();
        var t_height = Target.height();
        var t_l_padding = Target.padding("left");
        var t_r_padding = Target.padding("right");
        var t_t_padding = Target.padding("top");
        var t_b_padding = Target.padding("bottom");
        var t_tagname = Target.prop("tagName");
        var containerwidth = t_width; // - (t_l_padding + t_r_padding);
        var containerheight = t_height - (t_t_padding + t_b_padding);
        var containerfontfamily = Target.css('font-family');
        var containerfontsize = Target.css('font-size');
        var containerlineheight = Target.css('line-height');
        var containerbackgroundcolor = Target.parent().css('background-color');
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
        this.$Me.keypress(function (e) {
            if (e.which == 13) {
                me.Save();
            }
        });
        this.$Me.focus();
        if (IsNull(this.CustomTrigger)) {
            this.$Me.blur(function () {
                me.Save();
                return true;
            });
        }
    };
    Editor.editclass = "editing";
    return Editor;
})();
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
    _Hide(_Select(selector));
    var item = _SelectFirst(selector, _Parent(sender));
    _Show(item);
}
function SetPivots() {
    $("#maintable").resizableColumns();
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
//if (typeof console === "undefined") {
window.onerror = ErrorHandler;
//} 
//# sourceMappingURL=AppItems.js.map