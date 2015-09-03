$(function () {
    
    $("#IdSejour").change(loadHotel);
    loadHotel();

    $('#DateDepart').datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        language: 'fr',
        autoclose: true,
        todayHighlight: true,
        mindate:"0",
        endDate: "-1d"
    });

   
});



function loadHotel() {
    var s = "";
    var mq = $("#IdSejour").val();

    $.getJSON("/Admin/Produits/GetJSONHotel/" + mq, function (data) {

        $.each(data, function (idx, item) {

            s += '<option value ="' + item.Hotel + '">' + item.NomHotel+" - "+ item.Ville +'('+ item.Pays +')' + "</option>";

        });
        $("#IdHotel").html(s);

    });
}