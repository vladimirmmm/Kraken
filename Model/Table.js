function Notify(notification) {
    if ('Notify' in window.external) {
        window.external.Notify(notification);
    } else {
        console.log(notification);
    }
}
function ErrorHandler(errorMsg, url, lineNumber) {
    var errortext = 'Error: ' + errorMsg + ' Script: ' + url + ' Line: ' + lineNumber;
    Notify(errortext);
    return true;
}
window.onerror = ErrorHandler;

function SetExtension(extension) {
    var li = JSON.parse(extension);
    Notify(extension);
    if (li != null && 'LabelContent' in li) {
        Notify("ok");
        $("#Extension").html(li.LabelContent);
    }
}
function Load(facts) {
 
}