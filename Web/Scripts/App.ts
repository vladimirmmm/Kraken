/// <reference path="typings/signalr/signalr.d.ts" />

module Applications
{
    export class App
    {
        public instancecontainer: Control.InstanceContainer = new Control.InstanceContainer(); 
        public taxonomycontainer: Control.TaxonomyContainer = new Control.TaxonomyContainer(); 

        public Tabs_Main: any = null;
        public Tabs_Taxonomy: any = null;
        public Tabs_instance: any = null;

        public TaskManager = new ProgressManager("#progressbar");

        private MenuAccessorFunction: Function = null;

        public MenuCommand(id: string)
        {
            var me = this;
            var lid = id.toLowerCase();
            var parameters: Dictionary = {};

            me.MenuAccessorFunction = (lid, p1) => {
                parameters["p1"] = p1;
                parameters["command"] = lid;
                AjaxRequest("UI/Menu", "get", "json", parameters, function (data) {
                    //Log(Format("command {0} successfull!", lid));
                }, function (error) { console.log(error); });
            }
            var handled = false;
            if (In(lid, "o_tax_l", "o_inst")) {
                handled = true;
                BrowseFile(lid, me.MenuAccessorFunction);
            } 
            if (In(lid, "o_tax_w")) {
                handled = true;
                me.OpenWebPath(lid);
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
            if (!handled)
            {
                me.MenuAccessorFunction(lid, null);
            }
            
        }

        public Settings_Save(element:Element)
        {
            var me = this;
            var settingscontainer = _SelectFirst("#EngineSettings");
            var settingsform = _SelectFirst("form", settingscontainer);
            var formdata = GetFromData(settingsform);
            var parameters = <Dictionary>ConvertFormDataToObj(formdata);
            AjaxRequest("Settings/Save", "post", "json", parameters, function (data) {
                Log("Settings were saved succeassfully!");
                me.CloseWindow(element);
            }, null);

        }

        public Settings_Show() {
            var settingscontainer = _SelectFirst("#EngineSettings");
            AjaxRequest("Settings/Get", "get", "json", null, function (data) {
                var control: HTMLInputElement = null;
                var items = GetProperties(data);
                items.forEach(function (item) {
                    control = <HTMLInputElement>_SelectFirst("[name="+item.Key+"]", settingscontainer);
                    control.checked = ToBool(item.Value);
                });
                //_Center(settingscontainer);
                _Show(settingscontainer);
            }, null);
           

        }

        public OpenWebPath(lid:string)
        {
            var me = this;
            var pathcontainer = _SelectFirst("#AppPath");
            _Show(pathcontainer);
            //_Center(pathcontainer);
            var input = _SelectFirst("textarea", pathcontainer);
            var okbutton = _SelectFirst(".ok", pathcontainer);
            var okfunction = () => {
                var val = _Value(input);
                var items = val.split("\r\n");
                var path = items[0];
                CallFunction(me.MenuAccessorFunction, [lid, path]);
                me.CloseWindow(okbutton);
            };
            _EnsureEventHandler(okbutton, "click", okfunction);


        }
        public ShowAbout(lid: string) {
            var me = this;
            var aboutcontainer = _SelectFirst("#AppAbout");
            var infocontainer = _SelectFirst("div.infocontainer", aboutcontainer);
            _Show(aboutcontainer);
            //_Center(aboutcontainer);

            AjaxRequest("App/Info", "get", "text", null, function (data) {
                var html = Replace(data, "\r\n", "<br/>");
                _Html(infocontainer, html);

            },null);
        }
        public CloseWindow(element: Element)
        {
            var control = _Parent(element, ".window");
            _Hide(control);
        }

        public LoadContainers()
        {
            var me = this;
            Log(window["BuildID"]);
            var $containers = $('[container-for]');
            me.SetMenu();
            SetPivots();

            var funcloader = function () {
                //me.Load.call(me)
                var engineHub = $.connection.engineHub;
                if (!IsNull(engineHub)) {
                    if (!IsNull(engineHub.client)) {
                        engineHub.client.sendText = function (message:string) {
                            Log(message);
                        };
                        engineHub.client.sendMessage = function (message:General.Message) {
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
                if (IsDesktop())
                {
                    ShowNotification("UI ready.");

                }
                StopProgress("layout");
            };

            var waiter: General.Waiter = new General.Waiter((i: RequestHandler) => i.succeded, funcloader);
            //$(s_content_selector).hide();
            StartProgress("layout");

            $containers.each(function (ix, item) {
                var $item = $(item);
                var contanerid = $item.attr("container-for");
                var ajaxrequest = AjaxRequestComplex(Format("Layout/{0}.html", contanerid), "get", "text/html", {}, [function (data) {
                    $item.html(data);
                },
                () => waiter.Check()
                ], [function (error) {
                        waiter.Check()
                    }]);
                waiter.Items.push(ajaxrequest)
            });

            waiter.Start();
        }

        public SetMenu()
        {

            var f = function (item)
            {
                var dsname: string = item.displayname;
                var s_ix = dsname.lastIndexOf("\\");
                if (s_ix > -1)
                {
                    dsname = dsname.substring(s_ix + 1);
                }
                return Format("<a title='{1}' href='javascript:void(0); app.MenuCommand(\"{0}\");'>{2}</a>", Replace(item.id,"\\","\\\\"), item.displayname, dsname);
            }
            AjaxRequest("UI/Menu", "get", "json", null, function (data) {
                var html = RenderHierarchy(data, f);
                _Html(_SelectFirst("#menucontainer"), html);
                var menu = $("#menucontainer>ul").menu({
                    position: { my: "left top", at: "left+5 top+20" }
                });
                $(menu).mouseleave(function () {
                    menu.menu('collapseAll');
                });
                $('#menucontainer > li').hover(
                    function () {
                        $(this).find('ul').stop(true, true).fadeIn("fast");
                    },
                    function () {
                        $(this).find('ul').stop(true, true).fadeOut("fast");
                    });

            }, function (error) {
                    Log(errortag + error);
                });

        }

        public ShowOnBottomTab(element:Element, tabselector:string)
        {
            var tmpcontainer = _SelectFirst("#tempcontainer");
            var tab = _SelectFirst(tabselector);
            var children = _Children(tab);
            children.forEach(function (child) {
                _Append(tmpcontainer, child);
            });
            _Append(tab, element);
            LoadTab("#LogWindow", tabselector);
           
        }

        public Load()
        {
            var me = this;
         
            me.taxonomycontainer.SetExternals();
            me.instancecontainer.SetExternals();
        }
    }
} 

var app = new Applications.App();

var actioncenter = new Engine.ActionCenter();
var uimanager = new Engine.UIManager();
var resourcemanager: IResourceManager = { Get: function (key: string, culture?: string) { return key; } };

var activeItem = null;