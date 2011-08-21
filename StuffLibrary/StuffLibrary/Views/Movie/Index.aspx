<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StuffLibrary.Models.Movies.MovieIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">StuffLibrary - Movies</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            var grid = $("#grid");
            var pager = $("#pager");

            var colNames = ['Id', 'Title', 'Category'];
            var colModel = [
                { name: 'Id', index: 'Id', hidden: true },
                { name: 'Title', index: 'Title', width: 100, resizable: false, sortable: true },
                { name: 'Category', index: 'Category', width: 100, resizable: false, sortable: true }
            ];

            grid.jqGrid({
                url: '/Movie/JsonMovies',
                datatype: "json",
                postData: { Query: '' },
                colNames: colNames,
                colModel: colModel,
                sortname: 'Title',
                sortorder: 'asc',
                width: 1000,
                viewrecords: true,
                hidegrid: false,
                pager: pager,
                onSelectRow: function (id) {
                    var movieId = grid.jqGrid('getCell', id, 0);
                    document.location.href = "/Movie/Edit/" + movieId;
                }
            }).navGrid($("#pager"), { edit: false, add: false, del: false, search: false });

            var searchField = $("#searchField");
            searchField.bind('keyup', function (e) {
                if (e.which == 13) {
                    search();
                }
            });

            $("#searchButton").click(function () {
                search();
            });

            function search() {
                var query = searchField.val();
                grid.setPostDataItem('Query', query);
                grid.trigger('reloadGrid');
            }
        });
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadingContent" runat="server">
    <div class="borderunder">
        <h2><a href="/Movie">Movies</a></h2>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>StuffLibrary - Movies</h2>

    <%= Html.TextBox("searchField") %>
    <input id="searchButton" type="button" value="Search"/>
    <div>
        <table id="grid"></table>
        <div id="pager"></div>
    </div>
    <%=Html.ActionLink("Add", "RegisterNew", "Movie", null, new {@class="button"})%>
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
