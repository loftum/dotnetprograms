StuffLibrary.Common = {

    DisplayJsonResponse: function (response) {
        StuffLibrary.Common.DisplayMessage(response.TypeDisplay, response.Message);
    },

    DisplayMessage: function (type, message) {
        var jsonMessage = $("#jsmessage");
        var flashMessage = $(".flashmessage-container");

        jsonMessage.html("<span>" + message + "</span>").attr("class", type).show();
        flashMessage.show();
        flashMessage.slideDown();

        if (type == "success") {
            flashMessage.delay(2500).slideUp(600);
        }
    }
}