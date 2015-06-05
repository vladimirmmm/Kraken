var Control;
(function (Control) {
    var InstanceContainer = (function () {
        function InstanceContainer() {
            this.Instance = null;
            this.ValidationResults = [];
            this.SetExternals();
        }
        InstanceContainer.prototype.SetExternals = function () {
            this.Instance = window["currentinstance"] == null ? {} : window["currentinstance"];
            this.ValidationResults = window["currentvalidationresults"] == null ? {} : window["currentvalidationresults"];
        };
        InstanceContainer.prototype.LoadInstance = function (instancejson) {
            var item = JSON.parse(instancejson);
            this.Instance = item;
        };
        return InstanceContainer;
    })();
    Control.InstanceContainer = InstanceContainer;
})(Control || (Control = {}));
var instancecontainer = new Control.InstanceContainer();
//# sourceMappingURL=Instance.js.map