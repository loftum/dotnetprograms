<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StuffLibrary.Models.Movies.MovieViewModel>" %>
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("Save", "Movie")) { %>
        <%=Html.HiddenFor(model => model.Movie.Id) %>
        <%=Html.LabelFor(model => model.Movie.Title)%>
        <%=Html.TextBoxFor(model => model.Movie.Title)%>
        <div class="clearer"></div>
        <%=Html.SubmitButton("Save") %>
    <% } %>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
