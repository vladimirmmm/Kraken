var TemplateDictionary: TemplateDictionaryItem[] = [];
var S_Bind_Start = "$bind:";
var S_Bind_End = "$";

class TemplateDictionaryItem {
    //public Item: Element = null;
    public Item: JQuery = null;
    public Template: BindingTemplate = null;
}

class BindingTemplate {
    public ID: string = "";
    public ChildID: string = "";
    public Children: BindingTemplate[] = [];
    //public Child: BindingTemplate = null;
    public Parent: BindingTemplate = null;
    public Content: string;
    public ChildPlaceholder: string = "@children@";
    public AccessorExpression: string = "";
    public IsEnumerationBinding: boolean = false;
    public IsRecursive: boolean = false;
    public PageSize: number = 0;

    public Bind(data: Object, level:number=0, maxlevel:number=-1): string {
     
        var result_html: string = "";
        var me = this;
        //console.log("binding " + data);
        result_html = BindLevel(this.Content, data);
        var children = me.Children;
        if (!IsNull(me.Parent) && me.Parent.IsRecursive)
        {
            children = [me.Parent];
            //result_html += me.Parent.Bind(data);
        }
        if (level > maxlevel && maxlevel > -1) {
            return "";
        }
        children.forEach(function (child) {
            var accessorexpr =  child.AccessorExpression;
            var shouldenumerate = child.Parent.IsEnumerationBinding;

            if (accessorexpr != "nobind") {
                var items = Access(data, accessorexpr);

                items = IsNull(items) ? [] : items;
                var childitems = "";
                var hasdata = false;

                var childfunc = function (item) {
                    childitems += child.Bind(item, level + 1, maxlevel);
                    hasdata = true;
                };
                if (shouldenumerate) {

                    EnumerateObject(items, me, childfunc);
                    if (!hasdata) {
                        childitems = Bind_Replace(childitems, "@X@", "");
                    } else
                    {
                       // result_html = Replace(result_html, "@X@", childitems);
                    }
                }
                else
                {
                    childitems += child.Bind(items, level + 1, maxlevel);
                }

            

                if (result_html.indexOf(child.ID) > -1) {
                    result_html = Bind_Replace(result_html, child.ID, childitems);
                }
                if (result_html.indexOf("@X@") > -1) {
                    {
                        result_html = Bind_Replace(result_html, "@X@", childitems);
                    }
                }
            }
        });
        return result_html;

    }


    public ToHierarchyString(tab: string): string {
        var me = this;
        var result = "";
        tab = IsNull(tab) ? "    " : tab;
        result += Format("{0} {1} {2}\n", tab, this.ID, this.AccessorExpression);
        me.Children.forEach(function (child) {
            result += child.ToHierarchyString(tab + tab);

        });

        return result;
    }

    public GetExpression(item: JQuery) {
        var expr = item.attr("binding");
        return expr;
    }
}

function BindLevel(html: string, data: Object): string {
    var result_html: string = html;
    var bindings = TextsBetween(html, S_Bind_Start, S_Bind_End, true);
    bindings.forEach(function (binding) {
        var subbindings = TextsBetween(binding, "{", "}", true);
        var bindingexpression = TextBetween(binding, S_Bind_Start, S_Bind_End);
        if (subbindings.length == 0) {
            subbindings.push(bindingexpression);
        }
        var s_html = bindingexpression;
        subbindings.forEach(function (subbinding) {
            var subbindingexpression = subbinding.indexOf("{") > -1 ? TextBetween(subbinding, "{", "}") : subbinding;
            var dataitem = Access(data, subbindingexpression);
            s_html = Bind_Replace(s_html, subbinding, dataitem);
        });
        result_html = Bind_Replace(result_html, binding, s_html);

    });
    return result_html;
}

function GetBindingTemplate(target: JQuery) {
    //var templates = $('[binding-type=template]', target);
    var templates = $('[binding-items], [binding-type=template]', target);
    var me = this;

    templates.each(function (index, item) {
        var jitem = $(item);
        var ix = jitem.index();
        var parent = jitem.parents("[binding-items]")[0];
        //var parentbinding = $(parent).attr("binding-items");
        var parentbinding = jitem.attr("binding-items");
        var isenumeration = Format("{0}", !IsNull(parentbinding));
        //var parentpagesize = $(parent).attr("binding-pagesize");
        var placeholder = "@" + index + "@";
        var placeholdernode = document.createTextNode(placeholder);
        //jitem.insertBefore(placeholdernode);
        $(placeholdernode).insertBefore(jitem);
        jitem.attr("ChildID", placeholder);
        jitem.attr("Expression", parentbinding);
        jitem.attr("IsEnumeration", isenumeration);
        //jitem.attr("PageSize", parentpagesize);
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
        var isenumeration = jitem.attr("IsEnumeration");
        jitem.removeAttr("IsEnumeration");
        //var parentPageSize = jitem.attr("PageSize");
        //jitem.removeAttr("PageSize");
        var isrecursive = jitem.attr("binding-mode") == "recursive"
        jitem.removeAttr("binding-type");
        var html = OuterHtml(jitem);
        var t = new BindingTemplate();
        templatecollection.push(t);
        t.ID = childID
        t.Content = html;
        t.AccessorExpression = parentBinding;
        t.IsRecursive = isrecursive;
        t.IsEnumerationBinding = isenumeration == "true" ? true : false;

        //t.ParentPageSize = parentPageSize;

    });

    var t = new BindingTemplate();
    templatecollection.push(t);
    t.ID = "@root@";
    t.Content = target.html();
    t.AccessorExpression = "this";
    t.IsRecursive = target.children().first().attr("binding-mode") == "recursive";
    var isenumeration = !IsNull(target.attr("binding-items"));
    t.IsEnumerationBinding = isenumeration;
    var roottemplate: BindingTemplate = null;
    var templatelist = templatecollection.AsLinq<BindingTemplate>();
    templatecollection.forEach(function (item) {
        var parenttemplate = templatelist.FirstOrDefault(i=> i.Content.indexOf(item.ID) > -1);
        if (parenttemplate != null) {
            item.Parent = parenttemplate;
            if (IsNull(item.AccessorExpression)) {
                //item.AccessorExpression = parenttemplate.AccessorExpression;
                item.AccessorExpression = 'this';
                //todo
            }
            parenttemplate.Children.push(item);
        }
        else {
            roottemplate = item;
        }
    });
    if (IsNull(roottemplate)) {
        roottemplate = templatelist.FirstOrDefault(i=> IsNull(i.Parent));
    }
    return roottemplate;
}



function BindX(item: any, data: Object, maxlevel:number=-1) {
    var jitem = $(item);
    if (jitem.length == 0) {
        Log("UI","BindX: " + jitem.selector + " has no items!");
    } else {
        var bt: BindingTemplate = null;
        var templatedictionaryitem = TemplateDictionary.AsLinq<TemplateDictionaryItem>()
            .FirstOrDefault(i=> i.Item[0] == jitem[0]);
        if (templatedictionaryitem == null) {
            if (jitem.length > 0) {
                bt = GetBindingTemplate(jitem);
                templatedictionaryitem = new TemplateDictionaryItem();
                templatedictionaryitem.Item = jitem;//.clone()[0];
                templatedictionaryitem.Template = bt;
                TemplateDictionary.push(templatedictionaryitem);
            } else {
                console.log(jitem.selector + " was not found! (BindX)");
            }
        } else {
            bt = templatedictionaryitem.Template;
        }
        _Html(jitem[0], bt.Bind(data, 0, maxlevel));
        jitem.removeAttr("binding-type");
        //item[0].innerHTML = bt.Bind(data);
    }
}

function GetBindingTemplateX(item: any): BindingTemplate
{
    var jitem = $(item);
    if (jitem.length == 0) {
        console.log("BindX: " + jitem.selector + " has no items!");
        return null;
    } else {
        var bt: BindingTemplate = null;
        var templatedictionaryitem = TemplateDictionary.AsLinq<TemplateDictionaryItem>()
            .FirstOrDefault(i=> i.Item[0] == jitem[0]);
        if (templatedictionaryitem != null) {
            bt = templatedictionaryitem.Template;
        }
        return bt;
    }
}
