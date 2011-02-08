ML.Library.Grid = {

    Setup: function (grid, pager) {

        var colModel = ML.Library.Grid.GenerateColModel();
        var colNames = ML.Library.Grid.GenerateColNames();

        grid.jqGrid({
            url: '/Library/JsonLibrary',
            colNames: colNames,
            colModel: colModel,
            width: 500,
            pager: pager,
            fotterrow: true,
            userDataOnFooter: true
        });
    },

    GenerateColModel: function () {
        return [
                { name: 'Id', index: 'Id', hidden: true },
                { name: 'Title', index: 'Title', width: 80, resizable: false },
                { name: 'SubTitle', index: 'SubTitle', width: 80, resizable: false }
            ];
    },

    GenerateColNames: function () {
        return ['Id', 'Title', 'SubTitle'];
    }
}