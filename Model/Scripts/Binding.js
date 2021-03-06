var TemplateDictionary = [];
var TemplateDictionaryItem = (function () {
    function TemplateDictionaryItem() {
        this.Item = null;
        this.Template = null;
    }
    return TemplateDictionaryItem;
})();
var BindingTemplate = (function () {
    function BindingTemplate() {
        this.ID = "";
        this.ChildID = "";
        this.Children = [];
        //public Child: BindingTemplate = null;
        this.Parent = null;
        this.ChildPlaceholder = "@children@";
        this.AccessorExpression = "";
        this.IsEnumerationBinding = false;
        this.IsRecursive = false;
        this.PageSize = 0;
    }
    BindingTemplate.prototype.Bind = function (data) {
        var result_html = "";
        var me = this;
        console.log("binding " + data);
        result_html = BindLevel(this.Content, data);
        var children = me.Children;
        if (!IsNull(me.Parent) && me.Parent.IsRecursive) {
            children = [me.Parent];
        }
        children.forEach(function (child) {
            var accessorexpr = child.AccessorExpression;
            var shouldenumerate = child.Parent.IsEnumerationBinding;
            if (accessorexpr != "nobind") {
                var items = Access(data, accessorexpr);
                items = IsNull(items) ? [] : items;
                var childitems = "";
                var hasdata = false;
                var childfunc = function (item) {
                    childitems += child.Bind(item);
                    hasdata = true;
                };
                if (shouldenumerate) {
                    EnumerateObject(items, me, childfunc);
                    if (!hasdata) {
                        childitems = Replace(childitems, "@X@", "");
                    }
                    else {
                    }
                }
                else {
                    childitems += child.Bind(items);
                }
                if (result_html.indexOf(child.ID) > -1) {
                    result_html = Replace(result_html, child.ID, childitems);
                }
                if (result_html.indexOf("@X@") > -1) {
                    {
                        result_html = Replace(result_html, "@X@", childitems);
                    }
                }
            }
        });
        return result_html;
    };
    BindingTemplate.prototype.ToHierarchyString = function (tab) {
        var me = this;
        var result = "";
        tab = IsNull(tab) ? "    " : tab;
        result += Format("{0} {1} {2}\n", tab, this.ID, this.AccessorExpression);
        me.Children.forEach(function (child) {
            result += child.ToHierarchyString(tab + tab);
        });
        return result;
    };
    BindingTemplate.prototype.GetExpression = function (item) {
        var expr = item.attr("binding");
        return expr;
    };
    return BindingTemplate;
})();
function BindLevel(html, data) {
    var result_html = html;
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
            s_html = Replace(s_html, subbinding, dataitem);
        });
        result_html = Replace(result_html, binding, s_html);
    });
    return result_html;
}
function GetBindingTemplate(target) {
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
    var templatecollection = [];
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
        var isrecursive = jitem.attr("binding-mode") == "recursive";
        jitem.removeAttr("binding-type");
        var html = OuterHtml(jitem);
        var t = new BindingTemplate();
        templatecollection.push(t);
        t.ID = childID;
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
    var roottemplate = null;
    var templatelist = templatecollection.AsLinq();
    templatecollection.forEach(function (item) {
        var parenttemplate = templatelist.FirstOrDefault(function (i) { return i.Content.indexOf(item.ID) > -1; });
        if (parenttemplate != null) {
            item.Parent = parenttemplate;
            if (IsNull(item.AccessorExpression)) {
                //item.AccessorExpression = parenttemplate.AccessorExpression;
                item.AccessorExpression = 'this';
            }
            parenttemplate.Children.push(item);
        }
        else {
            roottemplate = item;
        }
    });
    if (IsNull(roottemplate)) {
        roottemplate = templatelist.FirstOrDefault(function (i) { return IsNull(i.Parent); });
    }
    return roottemplate;
}
function BindX(item, data) {
    if (item.length == 0) {
        ShowNotification("BindX: " + item.selector + " has no items!");
    }
    else {
        var bt = null;
        var templatedictionaryitem = TemplateDictionary.AsLinq().FirstOrDefault(function (i) { return i.Item[0] == item[0]; });
        if (templatedictionaryitem == null) {
            if (item.length > 0) {
                bt = GetBindingTemplate(item);
                templatedictionaryitem = new TemplateDictionaryItem();
                templatedictionaryitem.Item = item;
                templatedictionaryitem.Template = bt;
                TemplateDictionary.push(templatedictionaryitem);
            }
            else {
                console.log(item.selector + " was not found! (BindX)");
            }
        }
        else {
            bt = templatedictionaryitem.Template;
        }
        item[0].innerHTML = bt.Bind(data);
    }
}
//# sourceMappingURL=Binding.js.map