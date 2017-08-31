function NavToggle() {
    var changeDisplay = $("#navBar");
    if (changeDisplay.hasClass("NoDisplay"))
    { changeDisplay.removeClass("NoDisplay"); }
    else
    { changeDisplay.addClass("NoDisplay"); }
}
function yearToggle() {
    var changeYear = document.getElementById("years");
    if (changeYear.style.display == "none")
    {
        changeYear.style.display = "block";
        document.getElementById("navIcon").classList.remove("fa-plus-square");
        document.getElementById("navIcon").classList.add("fa-minus-square");
    }
    else {
        changeYear.style.display = "none";
        document.getElementById("navIcon").classList.remove("fa-minus-square");
        document.getElementById("navIcon").classList.add("fa-plus-square");
    }
}
//shopping cart hover
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});