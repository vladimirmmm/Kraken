/// <reference path="typings/signalr/signalr.d.ts" />
var Applications;
(function (Applications) {
    var TaskAction = (function () {
        function TaskAction() {
            this.ActiveTasksNr = 0;
            this.OnCompleted = function () {
            };
        }
        return TaskAction;
    })();
    Applications.TaskAction = TaskAction;
    var Waiter = (function () {
        function Waiter() {
            this.Waiters = {};
        }
        Waiter.prototype.SetWaiter = function (waiterid, oncompleted) {
            var me = this;
            var a = new TaskAction();
            a.OnCompleted = oncompleted;
            a.ActiveTasksNr = 0;
            me.Waiters[waiterid] = a;
        };
        Waiter.prototype.StartTask = function (waiterid, task) {
            var me = this;
            //Log("UI","StartTask: " + task);
            me.Waiters[waiterid].ActiveTasksNr++;
        };
        Waiter.prototype.EndTask = function (waiterid, task) {
            var me = this;
            var waiter = me.Waiters[waiterid];
            waiter.ActiveTasksNr--;
            //Log("UI","EndTask: " + task);
            if (waiter.ActiveTasksNr == 0) {
                waiter.OnCompleted();
            }
        };
        return Waiter;
    })();
    Applications.Waiter = Waiter;
    var App = (function () {
        function App() {
            this.instancecontainer = new Control.InstanceContainer();
            this.taxonomycontainer = new Control.TaxonomyContainer();
            this.Tabs_Main = null;
            this.Tabs_Taxonomy = null;
            this.Tabs_instance = null;
            this.TaskManager = new ProgressManager("#progressbar");
            this.MenuAccessorFunction = null;
            this.IsTaxonomyLoading = false;
            this.ShouldLoadInstance = false;
        }
        App.prototype.MenuCommand = function (id) {
            var me = this;
            var lid = id.toLowerCase();
            var parameters = {};
            me.MenuAccessorFunction = function (lid, p1) {
                parameters["p1"] = p1;
                parameters["command"] = lid;
                AjaxRequest("UI/Menu", "get", "json", parameters, function (data) {
                    //Log(Format("command {0} successfull!", lid));
                }, function (error) {
                    console.log(error);
                }, null);
            };
            var handled = false;
            if (In(lid, "o_tax_l", "o_inst")) {
                handled = true;
                BrowseFile(lid, me.MenuAccessorFunction);
            }
            if (In(lid, "o_tax_w")) {
                handled = true;
                me.OpenWebPath(lid);
            }
            if (In(lid, "save_xbrl_instance")) {
                var parameter = { facts: JSON.stringify(me.instancecontainer.Instance.Facts) };
                AjaxRequest("Instance/Save", "post", "json", parameter, function (result) {
                    ShowNotification(Format("{0} facts were saved!", parameter.facts.length));
                    app.instancecontainer.LoadInstance(null);
                    //() => {
                    //    me.LoadInstance(me.Instance);
                    //}
                    // );
                }, function (error) {
                    ShowError("Facts were not saved: " + GetErrorObj(error).message);
                }, null);
                handled = true;
            }
            if (In(lid, "validate_folder", "process_folder")) {
                handled = true;
                BrowseFolder(lid, me.MenuAccessorFunction);
            }
            if (In(lid, "settings")) {
                handled = true;
                me.Settings_Show();
            }
            if (In(lid, "about")) {
                handled = true;
                me.ShowAbout(lid);
            }
            if (!handled) {
                me.MenuAccessorFunction(lid, null);
            }
        };
        App.prototype.Settings_Save = function (element) {
            var me = this;
            var settingscontainer = _SelectFirst("#EngineSettings");
            var settingsform = _SelectFirst("form", settingscontainer);
            var formdata = GetFromData(settingsform);
            var parameters = ConvertFormDataToObj(formdata);
            AjaxRequest("Settings/Save", "post", "json", parameters, function (data) {
                Log("UI", "Settings were saved succeassfully!");
                me.CloseWindow(element);
            }, null, null);
        };
        App.prototype.Settings_Show = function () {
            var settingscontainer = _SelectFirst("#EngineSettings");
            var settingslist = _SelectFirst("#SettingsList");
            AjaxRequest("Settings/Get", "get", "json", null, function (data) {
                var items = GetProperties(data);
                _Html(settingslist, " ");
                var c_html = "";
                items.forEach(function (item) {
                    var isbool = In(item.Value.toLowerCase(), "true", "false");
                    var html = Format('<input type="text" name="{0}" value="{1}"/>', item.Key, item.Value);
                    if (isbool) {
                        html = Format('<input type="checkbox" name="{0}" {1}/> \n', item.Key, ToBool(item.Value) ? ' checked="checked"' : '');
                    }
                    html = Format("<li>\n{0}\n<label>{1}</label>\n</li>", html, item.Key);
                    c_html = c_html + html;
                });
                _Html(settingslist, c_html);
                //_Center(settingscontainer);
                _Show(settingscontainer);
            }, null, null);
        };
        App.prototype.OpenWebPath = function (lid) {
            var me = this;
            var pathcontainer = _SelectFirst("#AppPath");
            _Show(pathcontainer);
            //_Center(pathcontainer);
            var input = _SelectFirst("textarea", pathcontainer);
            var okbutton = _SelectFirst(".ok", pathcontainer);
            var okfunction = function () {
                var val = _Value(input);
                var items = val.split("\r\n");
                var path = items[0];
                CallFunction(me.MenuAccessorFunction, [lid, path]);
                me.CloseWindow(okbutton);
            };
            _EnsureEventHandler(okbutton, "click", okfunction);
        };
        App.prototype.ShowAbout = function (lid) {
            var me = this;
            var aboutcontainer = _SelectFirst("#AppAbout");
            var infocontainer = _SelectFirst("div.infocontainer", aboutcontainer);
            _Show(aboutcontainer);
            //_Center(aboutcontainer);
            AjaxRequest("App/Info", "get", "text", null, function (data) {
                var html = Replace(data, "\r\n", "<br/>");
                _Html(infocontainer, html);
            }, null, null);
        };
        App.prototype.CloseWindow = function (element) {
            var control = _Parent(element, ".window");
            _Hide(control);
        };
        App.prototype.LoadContainers = function () {
            var me = this;
            Log("UI", window["BuildID"]);
            var $containers = $('[container-for]');
            me.SetMenu();
            SetPivots();
            var funcloader = function () {
                //me.Load.call(me)
                var engineHub = $.connection.engineHub;
                if (!IsNull(engineHub)) {
                    if (!IsNull(engineHub.client)) {
                        engineHub.client.sendText = function (message) {
                            Log("UI", "SR: " + message);
                        };
                        engineHub.client.sendMessage = function (message) {
                            MessageReceived(message);
                        };
                    }
                }
                if (!IsDesktop() && !IsNull($.connection) && !IsNull($.connection.hub)) {
                    $.connection.hub.start().done(function () {
                        //Log("SignalR Hub started");
                        ShowNotification("SignalR Hub started. UI ready.");
                    });
                }
                if (IsDesktop()) {
                    Log("UI", "UI ready.");
                }
                StopProgress("layout");
            };
            var waiter = new General.Waiter(function (i) { return i.succeded; }, funcloader);
            //$(s_content_selector).hide();
            StartProgress("layout");
            $containers.each(function (ix, item) {
                var $item = $(item);
                var contanerid = $item.attr("container-for");
                var ajaxrequest = AjaxRequestComplex(Format("Layout/{0}.html", contanerid), "get", "text/html", {}, [function (data) {
                    $item.html(data);
                }, function () { return waiter.Check(); }], [function (error) {
                    waiter.Check();
                }], [function (Id) {
                }]);
                waiter.Items.push(ajaxrequest);
            });
            waiter.Start();
        };
        App.prototype.SetMenu = function () {
            var f = function (item) {
                var dsname = item.displayname;
                var s_ix = dsname.lastIndexOf("\\");
                if (s_ix > -1) {
                    dsname = dsname.substring(s_ix + 1);
                }
                return Format("<a title='{1}' href='javascript:void(0); app.MenuCommand(\"{0}\");'>{2}</a>", Replace(item.id, "\\", "\\\\"), item.displayname, dsname);
            };
            AjaxRequest("UI/Menu", "get", "json", null, function (data) {
                var html = RenderHierarchy(data, f);
                _Html(_SelectFirst("#menucontainer"), html);
                var menu = $("#menucontainer>ul").menu({
                    position: { my: "left top", at: "left+5 top+20" }
                });
                $(menu).mouseleave(function () {
                    menu.menu('collapseAll');
                });
                $('#menucontainer > li').hover(function () {
                    $(this).find('ul').stop(true, true).fadeIn("fast");
                }, function () {
                    $(this).find('ul').stop(true, true).fadeOut("fast");
                });
            }, function (error) {
                Log("UI", errortag + error);
            }, null);
        };
        App.prototype.ShowOnBottomTab = function (element, tabselector) {
            var tmpcontainer = _SelectFirst("#tempcontainer");
            var tab = _SelectFirst(tabselector);
            var children = _Children(tab);
            children.forEach(function (child) {
                _Append(tmpcontainer, child);
            });
            _Append(tab, element);
            //LoadTab("#ConsoleWindow", tabselector);
            LoadTab("#DetailWindow", tabselector);
        };
        App.prototype.LoadInstance = function () {
            Log("UI", "Attempt to Load the Instance");
            var me = this;
            if (!me.IsTaxonomyLoading) {
                me.instancecontainer.SetExternals();
            }
            else {
                me.ShouldLoadInstance = true;
            }
        };
        App.prototype.LoadTaxonomy = function () {
            var me = this;
            me.instancecontainer.Clear();
            me.taxonomycontainer.Clear();
            GC();
            me.IsTaxonomyLoading = true;
            me.taxonomycontainer.Table.TaxonomyService = new Service.TaxonomyService(me.taxonomycontainer, me.instancecontainer);
            me.taxonomycontainer.OnLoaded = function () {
                Log("UI", "Taxonomy OnLoaded");
                me.IsTaxonomyLoading = false;
                if (me.ShouldLoadInstance) {
                    me.ShouldLoadInstance = false;
                    me.LoadInstance();
                }
            };
            me.taxonomycontainer.SetExternals();
        };
        App.Waiters = new Waiter();
        return App;
    })();
    Applications.App = App;
})(Applications || (Applications = {}));
function GC() {
    if (typeof (window["CollectGarbage"]) == "function") {
        //Log("UI","CollectGarbage");
        window["CollectGarbage"]();
    }
}
var app = new Applications.App();
var actioncenter = new Engine.ActionCenter();
var uimanager = new Engine.UIManager();
var resourcemanager = { Get: function (key, culture) {
    return key;
} };
var activeItem = null;
//# sourceMappingURL=App.js.map