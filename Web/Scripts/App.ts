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
        public MenuCommand(id: string)
        {
            var me = this;
            var lid = id.toLowerCase();
            var parameters: Dictionary = { "command": lid };

            var f = (p1) => {
                parameters["p1"] = p1;
                AjaxRequest("UI/Menu", "get", "json", parameters, function (data) {
                    Log(Format("command {0} successfull!", lid));
                }, function (error) { console.log(error); });
            }
            var handled = false;
            if (In(lid, "o_tax", "o_inst", "validate_folder", "process_folder")) {
                handled = true;
                BrowseFile(f);
            } 
            if (In(lid, "settings")) {
                handled = true;
                me.Settings_Show();
            } 
            if (!handled)
            {
                f(null);
            }
            
        }

        public Settings_Save()
        {
            var me = this;
            var settingscontainer = _SelectFirst("#EngineSettings");
            var settingsform = _SelectFirst("form", settingscontainer);
            var formdata = GetFromData(settingsform);
            var parameters = <Dictionary>ConvertFormDataToObj(formdata);
            AjaxRequest("Settings/Save", "post", "json", parameters, function (data) {
                Log("Settings were saved succeassfully!");
                me.Settings_Cancel();
            }, null);

        }
        public Settings_Cancel() {
            var settingscontainer = _SelectFirst("#EngineSettings");
            _Hide(settingscontainer);
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
                $(settingscontainer).center();
                _Show(settingscontainer);
            }, null);
           

        }

        public LoadContainers()
        {
            var me = this;
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
                        Log("SignalR Hub started")
                    });
                }
             
            };

            var waiter: General.Waiter = new General.Waiter((i: RequestHandler) => i.succeded, funcloader);
            //$(s_content_selector).hide();
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
                    Log("Error: "+error);
                });

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