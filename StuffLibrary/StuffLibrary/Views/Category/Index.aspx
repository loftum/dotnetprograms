<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StuffLibrary.Models.Categories.CategoryIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    StuffLibrary - Categories
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
                var grid = $("#grid");

                var colNames = ['Id', 'Name'];
                var colModel = [
                    { name: 'Id', index: 'Id', hidden: true },
                    { name: 'Name', index: 'Name', width: 100, resizable: false, sortable: true },
                ];

                grid.jqGrid({
                    url: '/Category/JsonCategories',
                    datatype: "json",
                    colNames: colNames,
                    colModel: colModel,
                    sortname: 'Name',
                    sortorder: 'asc',
                    width: 1000,
                    onSelectRow: function (id) {
                        var categoryId = grid.jqGrid('getCell', id, 0);
                        document.location.href = "/Category/Edit/" + categoryId;
                    }
                }).navGrid($("#pager"), { edit: false, add: false, del: false, search: false });
            });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadingContent" runat="server">
    <h2><a href="/Category">Categories</a></h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>StuffLibrary - Categories</h2>
    <%=Html.TextBox("searchField") %>
    <div>
        <table id="grid"></table>
        <div id="pager"></div>
    </div>
    <%=Html.ActionLink("Add", "RegisterNew", "Category", null, new {@class="button"})%>
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
