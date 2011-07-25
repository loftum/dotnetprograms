<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StuffLibrary.Models.GuiElements.GuiElementsViewModel>" %>
<%@ Import Namespace="StuffLibrary.HtmlTools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">StuffLibrary - Gui Elements</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        
        function postMessage(url) {
            var model = $("form").serialize();
            $.ajax({
                type: 'POST',
                url: url,
                data: model,
                success: function (data) {
                    StuffLibrary.Common.DisplayJsonResponse(data);
                }
            });
        }

        $(document).ready(function () {

            $("#successButton").click(function () {
                postMessage('/GuiElements/ShowSuccessMessage');
            });

            $("#infoButton").click(function () {
                postMessage('/GuiElements/ShowInfoMessage');
            });

            $("#warningButton").click(function () {
                postMessage('/GuiElements/ShowWarningMessage');
            });

            $("#errorButton").click(function () {
                postMessage('/GuiElements/ShowErrorMessage');
            });
            
        })
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadingContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm()) { %>
        <div>
            <div>
                <%=Html.DropDownListFor(model => model.SelectedValue, Model.AvailableValues) %>
            </div>
            <div>
                <%=Html.LabelFor(model => model.SuccessMessage) %>
                <%=Html.TextBoxFor(model => model.SuccessMessage) %>
                <%=Html.Button("successButton", "Show", "Show success message") %>
            </div>
            <div>
                <%=Html.LabelFor(model => model.InfoMessage) %>
                <%=Html.TextBoxFor(model => model.InfoMessage) %>
                <%=Html.Button("infoButton", "Show", "Show info message") %>
            </div>
            <div>
                <%=Html.LabelFor(model => model.WarningMessage) %>
                <%=Html.TextBoxFor(model => model.WarningMessage) %>
                <%=Html.Button("warningButton", "Show", "Show info message") %>
            </div>
            <div>
                <%=Html.LabelFor(model => model.ErrorMessage) %>
                <%=Html.TextBoxFor(model => model.ErrorMessage) %>
                <%=Html.Button("errorButton", "Show", "Show error message") %>
            </div>
        </div>
    <% } %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
