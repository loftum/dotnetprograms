<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StuffLibrary.Models.Categories.CategoryViewModel>" %>
<%@ Import Namespace="StuffLibrary.HtmlTools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    StuffLibrary - Edit Category
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadingContent" runat="server">
    <h2>
        <a href="/Category">Categories</a> // <%= Model.CategoryName %>
    </h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <% using (Html.BeginForm("Save", "Category")) { %>
        <%= Html.HiddenFor(model => model.Category.Id) %>
        <%= Html.HiddenFor(model => model.Category.Version) %>
        <%= Html.LabelFor(model => model.Category.Name) %>
        <%= Html.TextBoxFor(model => model.Category.Name) %>
        <div class="clearer"></div>
        <%=Html.SubmitButton("Save") %>
    <% } %>
</asp:Content>





<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
