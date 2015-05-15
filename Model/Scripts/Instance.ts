module Control
{
    export class InstanceContainer {
        public Instance: Model.Instance = null;

        public LoadInstance(instancejson: string) {
            var item: Model.Instance = <Model.Instance>JSON.parse(instancejson);
            this.Instance = item;
        }
    }
}


var instance = new Control.InstanceContainer();