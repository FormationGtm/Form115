$(function () {
    $('#DateDepart').datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        language: 'fr',
        autoclose: true,
        todayHighlight: true,
        mindate: "0",
        endDate: "-1d"
    });

    loadSejour();
});

function loadSejour() {
    var s = "";
    var mq = $("#champCacheSejour").val();

    $.getJSON("/Admin/Produits/GetJSONSejour/" + mq, function (data) {

        $.each(data, function (idx, item) {

            s += '<option value ="' + item.Sejour + '">' +item.Sejour+" - " +item.NomHotel + " - " + item.Ville + '(' + item.Pays +')' + "</option>";

        });
        $("#IdSejour").html(s);

        var valueOption = $("#champCacheSejour").val();
        selectionnerOption("IdSejour", valueOption);
    });
}