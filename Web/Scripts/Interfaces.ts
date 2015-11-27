
interface cbdelegate { (result: any): any; }
interface Dictionary { [s: string]: Object; }
interface F_Progress { (id: string): any; }
interface F_ResultFormatter { (rawdata: any): any; }

interface JQueryStatic {
    formatDateTime(format: string, d: Date);
    w8n(...any);

}
interface JQuery {
    w8n(...any);
    serializeObject(...any);
    menu(...any);
    tabs(...any);
    padding(direction: string): number;
    pagination(total: any, options: any);
    resizable(options?: any);
    colResizable(options?: any);
    resizableColumns(options?: any);
    datepicker(options?: any);
    selectText();
    center();
}

interface External {
    Notify(obj: any)
}

interface RequestHandler {
    success: Function[];
    error: Function[];
    Id: string;
    succeded: boolean;

}

interface DataResult {
    Total: number;
    Items: any[];
    Item: any;
}

interface IResourceManager {
    Get(key: string, culture?: string): string;
}

class Refrence<T> {
    public Value: T;

    constructor(reference: T) {
        this.Value = reference;
    }
}

module General {
    export class KeyValue {
        public Key: string = "";
        public Value: any = null;
    }

    export class Message {
        public Id: string;
        public Url: string;
        public Category: string;
        public Parameters: Object = {};
        public ContentType: string;
        public Error: string
        public Data: string
    }
    export class FunctionWithCallback {
        public Func: Function = null;
        public Callback: Function = (data) => { console.log("No CallbackDefined") };

        constructor(f: Function) {
            this.Func = f;
        }

        public Call(...args: any[]) {
            if (IsFunction(this.Func)) {
                this.Func(this, args);
            }
        }

    }
    export class Waiter {
        public Items: Object[] = [];
        public Condition: Function = null;
        public AllCompleted: Function = null;
        private IsStarted: boolean = false;

        constructor(Condition: Function, AllCompleted: Function) {
            this.AllCompleted = AllCompleted;
            this.Condition = Condition;
        }
        public Check() {
            var me = this;
            var result = true;
            this.Items.forEach(function (Item) {
                if (!me.Condition(Item)) {
                    result = false;
                }
            });
            if (result && me.IsStarted) {
                me.Stop();
                me.AllCompleted();
                me.Items = [];

            }
        }
        public WaitFor(Item: Object) {
            var me = this;
            me.Items.push(Item);
        }
        public Start() {
            this.IsStarted = true;
            this.Check();
        }
        public Stop() {
            this.IsStarted = false;
        }
    }

}