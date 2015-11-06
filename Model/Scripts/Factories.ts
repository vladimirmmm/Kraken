module Factories
{
    export function GetTablewithManager() : Controls.Table
    {
        var uitablemanger = new UITableManager();

        var uitable = new Controls.Table(uitablemanger);

        uitablemanger.CellEditorAssigner = function (element) {
            AssignEditor(
                //_Select("td:not(.blocked)", element),
                _Select("td.data:not(.blocked)", element),
                GetXbrlCellEditor,
                uitablemanger.OnCellChanged);

        };
        uitablemanger.OnLoaded = function (table) {
            uitablemanger.CellEditorAssigner(table.UIElement);
            var templaterow = _SelectFirst("tr.dynamic");
            if (!IsNull(templaterow)) {
                uitablemanger.TemplateRow = uitable.GetRowByElement(templaterow);
                _Hide(templaterow);
            }
        };
        uitablemanger.OnRowAdded = function (row) {
            var rowelement = row.UIElement;
            _AddClass(rowelement, "dynamicdata");
            _RemoveClass(rowelement, "dynamic");
            _Attribute(rowelement, "style", "");
            uitablemanger.CellEditorAssigner(row.UIElement);

        };
        uitablemanger.OnRowRemoved = function (row) {

        };
        uitablemanger.OnLayoutChanged = function (row) {
            if (uitable.CanManageRows) {
                uitablemanger.ManageRows(uitable);
            }
        };
        uitablemanger.OnCellChanged = function (cell, value) {
            var row = uitable.GetRowOfCell(cell);
            if (!IsNull(row) && !row.HasData()) {
                uitable.RemoveRow(row);
            }
            uitablemanger.ManageRows(uitable);
            //OnCellChanged(cell, value);
        };

        return uitable;
    }
} 