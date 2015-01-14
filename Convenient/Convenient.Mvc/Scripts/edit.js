$(document).ready(function () {
    $("body").on("click", ".addButton", addItem);
    $("body").on("click", ".removeButton", removeItem);
    $("body").on("keyup", "input:text", makeTextBoxesLargeEnough);
    $("body").on("focus", "input:text", makeTextBoxesLargeEnough);
});

function makeTextBoxesLargeEnough() {
    var self = $(this);
    self.css("max-width", "none");
    var temp = $("<span>")
        .css("font", self.css("font"))
        .text(this.value)
        .appendTo("body");
    var width = temp.width() + 10;
    temp.remove();
    if (self.width() < width) {
        self.width(width);
    };
}

function removeItem() {
    var self = $(this);
    var li = self.parents("li").first();
    var ul = li.parents("ul").first();
    li.remove();
    reIndex(ul);
};

function reIndex(ul) {
    $(ul).children("li").each(function (index) {
        var li = $(this);
        var oldIndex = li.attr('data-index') || -1;
        var pattern = oldIndex + "(?![\s\S]*" + oldIndex + ")";
        var regex = RegExp(pattern);
        li.attr('data-index', index);
        li.find("[id]").each(function () {
            this.id = this.id.replace(regex, index);
        });
        li.find("[name]").each(function () {
            var name = $(this).attr("name");
            $(this).attr("name", name.replace(regex, index));
        });
        li.find("[for]").each(function () {
            var name = $(this).attr("for");
            $(this).attr("for", name.replace(regex, index));
        });
        li.find("label").each(function () {
            var value = $(this).html();
            $(this).html(value.replace(regex, index));
        });
    });
}

function addItem() {
    var self = $(this);
    var template = self.siblings(".template").clone();
    template.find("input, checkbox, select").each(function () { $(this).removeAttr('disabled'); });

    var list = self.siblings(".addList");
    list.append($("<li class='form-group'></li>").append(template.html()));
    reIndex(list);
};