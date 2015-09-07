module Applications
{
    export class App
    {
        public instancecontainer: Control.InstanceContainer = new Control.InstanceContainer(); 
        public taxonomycontainer: Control.TaxonomyContainer = new Control.TaxonomyContainer(); 

        public LoadContainers()
        {
            var me = this;
            var $containers = $('[container-for]');

            var funcloader = function () { me.Load.call(me) };

            var waiter: Waiter = new Waiter((i: RequestHandler) => i.succeded, funcloader);
            $(s_content_selector).hide();
            $containers.each(function (ix, item) {
                var $item = $(item);
                var contanerid = $item.attr("container-for");
                var ajaxrequest = AjaxRequestComplex(Format("Layout/{0}.html", contanerid), "get", "text/html", {}, [function (data) {
                    $item.html(data);
                },
                () => waiter.Check()
                ], [function (error) { console.log(error); }]);
                waiter.Items.push(ajaxrequest)
            });

            waiter.Start();
        }

        public Load()
        {
            var me = this;
            SetPivots();
            me.taxonomycontainer.SetExternals();
            me.instancecontainer.SetExternals();
        }
    }
} 

var app = new Applications.App();