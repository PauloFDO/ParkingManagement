$(document).ready(function () {
    ShowHideCustomElements();
});

function ShowHideCustomElements() {
    if ($("#custom-assignment").val().length > 0) {
        $("#custom-assignment-fields").show();
    } else {
        $("#custom-assignment-fields").hide();
    }
}