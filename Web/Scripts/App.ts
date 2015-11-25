/// <reference path="typings/signalr/signalr.d.ts" />

module Applications
{
    export class App
    {
        public instancecontainer: Control.InstanceContainer = new Control.InstanceContainer(); 
        public taxonomycontainer: Control.TaxonomyContainer = new Control.TaxonomyContainer(); 

        public MenuCommand(id: string)
        {
            var lid = id.toLowerCase();
            var parameters: Dictionary = { "command": lid };

            var f = (p1) => {
                parameters["p1"] = p1;
                AjaxRequest("UI/Menu", "get", "json", parameters, function (data) {
                    Log(Format("command {0} successfull!", lid));
                }, function (error) { console.log(error); });
            }

            if (In(lid, "o_tax", "o_tax", "validate_folder", "process_folder")) {
                BrowseFile(f);
            } else
            {
                f(null);
            }
            
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
            $(s_content_selector).hide();
            $containers.each(function (ix, item) {
                var $item = $(item);
                var contanerid = $item.attr("container-for");
                var ajaxrequest = AjaxRequestComplex(Format("Layout/{0}.html", contanerid), "get", "text/html", {}, [function (data) {
                    $item.html(data);
                },
                () => waiter.Check()
                ], [function (error) {
                        console.log(error);
                    }]);
                waiter.Items.push(ajaxrequest)
            });

            waiter.Start();
        }

        public SetMenu()
        {

            var f = function (item)
            {
                return Format("<a href='javascript:void(0); app.MenuCommand(\"{0}\");'>{1}</a>", Replace(item.id,"\\","\\\\"), item.displayname);
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