$(function () {
    $("#champDateFin").datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        language: "fr",
        autoclose: true,
        todayHighlight: true,
    });
    $("#champDateDebut").datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        language: "fr",
        autoclose: true,
        todayHighlight: true,
    });

    var valueOption = $("#champCacheIdHotel").val();
    selectionnerOption("listeDeroulanteHotels", valueOption);
   
});