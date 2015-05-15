var Control;
(function (Control) {
    var InstanceContainer = (function () {
        function InstanceContainer() {
            this.Instance = null;
        }
        InstanceContainer.prototype.LoadInstance = function (instancejson) {
            var item = JSON.parse(instancejson);
            this.Instance = item;
        };
        return InstanceContainer;
    })();
    Control.InstanceContainer = InstanceContainer;
})(Control || (Control = {}));
var instance = new Control.InstanceContainer();
//# sourceMappingURL=Instance.js.map