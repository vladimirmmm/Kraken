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

var StopProgress: F_Progress = function (id: string) { return null; };
var StartProgress: F_Progress = function (id: string) { return null; };
var ResultFormatter: F_ResultFormatter = function (rawdata) { return rawdata };


var requests: General.KeyValue[] = []; 

module Engine {

    export class ActionCenter {
        private Selector: any = null;
        private CurrentSelector: any = null;
        private ListSelector: any = null;
        private ActionBarSelector: any = null;
        private class_Error: string = "n-error";
        private class_Warning: string = "n-warning";
        private class_Info: string = "n-info";
        private class_Success: string = "n-success";
        private format_Notification: string = "<div class=\"notification {1}\">{0}</div>";

        public SetSelectors(selector: any, currentselector: any, listselector: any, actionbarselector: any) {
            this.Selector = selector;
            this.CurrentSelector = currentselector;
            this.ListSelector = listselector;
            this.ActionBarSelector = actionbarselector;
        }

        public AddSuccess(content: string) {
            this.AddNotification(content, this.class_Success);
        }
        public AddInfo(content: string) {
            this.AddNotification(content, this.class_Info);
        }
        public AddWarning(content: string) {
            this.AddNotification(content, this.class_Warning);
        }
        public AddError(content: string) {
            this.AddNotification(content, this.class_Error);
        }

        public AddNotification(content: string, cssclass?: string) {
            content = Format(this.format_Notification, content, cssclass);
            var lastmessage = $(this.CurrentSelector).html();
            $(this.CurrentSelector).html(content);
            $(this.ListSelector).prepend(lastmessage);
            $(this.Selector).show();
        }

        public ClearAll() {
            this.ClearCurrent();
            this.ClearList();
            $(this.Selector).hide();

        }

        public ClearCurrent() {
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

    export class UIManager {
        private duration: number = 200;
        private min_width: number = 150;

        private GetMaxWidth(): number {
            var maxwidth = $("#main-content").width();
            return maxwidth;
        }

        public ActivateList() {

            $("#ListController").parent().animate({ "max-width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });
            $("#SaveController").parent().animate({ "width": this.min_width + "px" }, { duration: this.duration, queue: false });
        }
        public ActivateSave() {
            $("#ListController").parent().animate({ "max-width": this.min_width + "px" }, { duration: this.duration, queue: false });
            $("#SaveController").parent().animate({ "width": (this.GetMaxWidth() - this.min_width) + "px" }, { duration: this.duration, queue: false });

        }
    }
}

class Editor {
    public HtmlFormat: string = "";
    public ValueGetter: Function = null;
    public ValueSetter: Function = null;
    public TargetValueGetter: Function = null;
    public TargetValueSetter: Function = null;
    public CustomTrigger: Function = null;
    public $Target: JQuery = null;
    public $Me: JQuery = null;
    public Current_Value: string;
    public Original_Value: string;

    static editclass: string = "editing";

    constructor(HtmlFormat: string, ValueGetter: Function, ValueSetter: Function) {
        this.HtmlFormat = HtmlFormat;
        this.ValueGetter = ValueGetter;
        this.ValueSetter = ValueSetter;
    }

    public Save() {
        var new_value = this.ValueGetter(this.$Me);
        this.$Target.removeClass(Editor.editclass);
        this.$Me.remove();
        if (this.Original_Value != new_value) {
            this.TargetValueSetter(new_value)
        } else
        {
            _Html(this.$Target[0], this.Original_Value);
        }

    }

    public Load(TargetElement: Element, TargetValueGetter: Function, TargetValueSetter: Function) {
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
        var containerwidth = t_width;// - (t_l_padding + t_r_padding);

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
    }
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
    Error(errortext);
    return true;
}
function ShowHideChild(selector: any, sender: any) {
    _Hide(_Select(selector));
    var item = _SelectFirst(selector, _Parent(sender));
    _Show(item);
}
function SetPivots() {

    $("#maintable").resizableColumns();

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
    if ('Notify' in window.external) {
        window.external.Notify(strdata);
    } else {
        console.log(strdata);
    }
}

function Communication_Listener(data: string) {
    //Notify("Communication_Listener_Start");
    var message: General.Message = <General.Message>JSON.parse(data);
    data = "";

    //Notify("Communication_Listener Parsed");

    data = null;
    if (message.Category == "ajax") {
        asyncFunc(() => {
            //Notify("Calling AjaxResponse");
            AjaxResponse(message);
        });
        //clearobject(message);
        //AjaxResponse(message);
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