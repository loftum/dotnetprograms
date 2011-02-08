<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.LibraryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Movie Library</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="/Scripts/MovieLibrary/ml.library.grid.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            ML.Library.Grid.Setup($("#list"), $("#pager"));


        });
    
    </script>

    
    

</asp:Content>
