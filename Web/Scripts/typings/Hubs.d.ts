// Get signalr.d.ts.ts from https://github.com/borisyankov/DefinitelyTyped (or delete the reference)
/// <reference path="signalr/signalr.d.ts" />
/// <reference path="jquery/jquery.d.ts" />

////////////////////
// available hubs //
////////////////////
//#region available hubs

interface SignalR {

    /**
      * The hub implemented by Web.Hubs.EngineHub
      */
    engineHub : EngineHub;
}
//#endregion available hubs

///////////////////////
// Service Contracts //
///////////////////////
//#region service contracts

//#region EngineHub hub

interface EngineHub {
    
    /**
      * This property lets you send messages to the EngineHub hub.
      */
    server : EngineHubServer;

    /**
      * The functions on this property should be replaced if you want to receive messages from the EngineHub hub.
      */
    client : any;
}

interface EngineHubServer {
}

//#endregion EngineHub hub

//#endregion service contracts



////////////////////
// Data Contracts //
////////////////////
//#region data contracts

//#endregion data contracts

