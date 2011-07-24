<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StuffLibrary.Models.Movies.MovieIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">StuffLibrary - Movies</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            
            var colNames = ['Id', 'Title'];
            var colModel = [
                { name: 'Id', index: 'Id', hidden: true },
                { name: 'Title', index: 'Title', width: 108, resizable: false },
            ];
            
            $("#grid").jqGrid({
                url: '/Movie/JsonMovies',
                datatype: "json",
                colNames: colNames,
                colModel: colModel,
                width: 500
            }).navGrid($("#pager"), { edit: false, add: false, del: false, search: false });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>StuffLibrary - Movies</h2>

    <div>
        <table id="grid"></table>
        <div id="pager"></div>
    </div>
    <%=Html.ActionLink("Add", "RegisterNew", "Movie") %>
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
