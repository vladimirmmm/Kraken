var Applications;
(function (Applications) {
    var App = (function () {
        function App() {
            this.instancecontainer = new Control.InstanceContainer();
            this.taxonomycontainer = new Control.TaxonomyContainer();
        }
        App.prototype.LoadContainers = function () {
            var me = this;
            var $containers = $('[container-for]');
            var funcloader = function () {
                me.Load.call(me);
            };
            var waiter = new Waiter(function (i) { return i.succeded; }, funcloader);
            $(s_content_selector).hide();
            $containers.each(function (ix, item) {
                var $item = $(item);
                var contanerid = $item.attr("container-for");
                var ajaxrequest = AjaxRequestComplex(Format("Layout/{0}.html", contanerid), "get", "text/html", {}, [function (data) {
                    $item.html(data);
                }, function () { return waiter.Check(); }], [function (error) {
                    console.log(error);
                }]);
                waiter.Items.push(ajaxrequest);
            });
            waiter.Start();
        };
        App.prototype.Load = function () {
            var me = this;
            SetPivots();
            me.taxonomycontainer.SetExternals();
            me.instancecontainer.SetExternals();
        };
        return App;
    })();
    Applications.App = App;
})(Applications || (Applications = {}));
var app = new Applications.App();
//# sourceMappingURL=App.js.map