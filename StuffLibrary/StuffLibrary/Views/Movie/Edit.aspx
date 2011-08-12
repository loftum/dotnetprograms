<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StuffLibrary.Models.Movies.MovieViewModel>" %>
<%@ Import Namespace="StuffLibrary.Common.ExtensionMethods" %>
<%@ Import Namespace="StuffLibrary.HtmlTools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.Title %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="HeadingContent" runat="server">
    <h2>
        <a href="/Movie">Movie</a> // <%= Model.MovieTitle %>
    </h2>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#category").autocomplete({
                source: <%= Model.AvailableCategories.ToJavaScript() %>
            });
        })
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("Save", "Movie")) { %>
        <%= Html.HiddenFor(model => model.Movie.Id) %>
        <%= Html.HiddenFor(model => model.Movie.Version) %>
        <%= Html.LabelFor(model => model.Movie.Title)%>
        <%= Html.TextBoxFor(model => model.Movie.Title)%>
        <div class="clearer"></div>
        
        <div class="ui-widget">
            <label for="category">Category</label><input id="category" type="text"/>
        </div>
        <div id="categories"></div>
        <div class="clearer"></div>

        <%=Html.SubmitButton("Save") %>
    <% } %>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
