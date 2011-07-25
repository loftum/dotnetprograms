<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StuffLibrary.Models.Movies.MovieIndexViewModel>" %>
<%@ Import Namespace="StuffLibrary.HtmlTools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">StuffLibrary - Movies</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var grid = $("#grid");

            var colNames = ['Id', 'Title'];
            var colModel = [
                { name: 'Id', index: 'Id', hidden: true },
                { name: 'Title', index: 'Title', width: 108, resizable: false },
            ];

            grid.jqGrid({
                url: '/Movie/JsonMovies',
                datatype: "json",
                colNames: colNames,
                colModel: colModel,
                width: 500,
                onSelectRow: function (id) {
                    var movieId = grid.jqGrid('getCell', id, 0);
                    document.location.href = "/Movie/Edit/" + movieId;
                }
            }).navGrid($("#pager"), { edit: false, add: false, del: false, search: false });
        });
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadingContent" runat="server">
    <h2><a href="/Movie">Movie</a></h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>StuffLibrary - Movies</h2>

    <%=Html.TextBox("searchField") %>
    <div>
        <table id="grid"></table>
        <div id="pager"></div>
    </div>
    <%=Html.ActionLink("Add", "RegisterNew", "Movie", null, new {@class="button"})%>
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
