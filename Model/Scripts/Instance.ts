module Control
{
    export class InstanceContainer {
        public Instance: Model.Instance = null;
        public ValidationResults: Model.ValidationRuleResult[] = [];
        constructor() {
            this.SetExternals();
        }
        public SetExternals() {
            this.Instance = window["currentinstance"] == null ? {} : window["currentinstance"];
            this.ValidationResults = window["currentvalidationresults"] == null ? {} : window["currentvalidationresults"];

        }
        public LoadInstance(instancejson: string) {
            var item: Model.Instance = <Model.Instance>JSON.parse(instancejson);
            this.Instance = item;
        }

        public LoadToUI()
        {
            Bind("#factlist", this.Instance.Facts);
        }
    }
}


var instancecontainer = new Control.InstanceContainer();