module Factories
{
    export function GetTablewithManager() : Controls.Table
    {
        var uitablemanger = new UITableManager();

        var uitable = new Controls.Table(uitablemanger);

        

        return uitable;
    }
} 