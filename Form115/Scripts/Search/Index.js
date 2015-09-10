$(function () {
    $("#listeContinents").change(loadRegions);
    $("#listeRegions").change(loadPays);
    $("#listePays").change(loadVilles);

    loadPreviousSearchParams();

    $("#SearchBtn").click(saveSearchOptions);

    // DateTimePicker
    $("#DateDepart").datepicker({
        format: "mm/dd/yyyy",
        todayBtn: "linked",
        language: "fr",
        autoclose: true,
        todayHighlight: true,
        minDate: "+1d"
    });

    $("input[name=DateIndifferente]:checkbox").change(setDateAbility)
});


function setDateAbility() {
    if ($("input[name=DateIndifferente]:checked").length == 0) {
        $("input[name=DateDepart]").prop("disabled", false);
        $("input[name=DateMarge]").prop("disabled", false);
    }
    else {
        $("input[name=DateDepart]").prop("disabled", true);
        $("input[name=DateMarge]").prop("disabled", true);
    }
}

function loadRegions() {
    IdContinent = $("#listeContinents").val();
    var str = '<option value="0">Sélectionner une région</option>';
    if (IdContinent != 0) {
        $.getJSON("/Browse/GetJSONRegions/" + IdContinent, function (data) {

            // $.each(data, function (idx, mar) {
            // str += '<optgroup  label="' + mar.Marque + '">'
            $.each(data, function (idx, region) {
                str += '<option value="' + region.Id + '">' + region.Nom + "</option>";

            });
            //    str += '</optgroup>';
            //});
            $("#listeRegions").prop('disabled', false);
            $("#listeRegions").html(str);
            $("#listeRegions").val('0');
        });
    }
    else {
        $("#listeRegions").html(str);
        $("#listeRegions").val('0');
        $("#listeRegions").prop('disabled', true);
    }
    $("#listePays").html('<option value="0">Sélectionner un pays</option>');
    $("#listeVilles").html('<option value="0">Sélectionner une ville</option>');
    $("#listePays").prop('disabled', true);
    $("#listeVilles").prop('disabled', true);
}

function loadPays() {
    IdRegion = $("#listeRegions").val();
    var str = '<option value="0">Sélectionner un pays</option>';
    if (IdRegion != 0) {
        $.getJSON("/Browse/GetJSONPays/" + IdRegion, function (data) {

            // $.each(data, function (idx, mar) {
            // str += '<optgroup  label="' + mar.Marque + '">'
            $.each(data, function (idx, pays) {
                str += '<option value="' + pays.Id + '">' + pays.Nom + "</option>";

            });
            //    str += '</optgroup>';
            //});
            $("#listePays").prop('disabled', false);
            $("#listePays").html(str);
            $("#listePays").val('0');
        });
    }
    else {
        $("#listePays").prop('disabled', true);
        $("#listePays").html(str);
        $("#listePays").val('0');
    }
    $("#listeVilles").html('<option value="0">Sélectionner une ville</option>');
    $("#listeVilles").prop('disabled', true);
}


function loadVilles() {
    IdPays = $("#listePays").val();
    var str = '<option value="0">Sélectionner une ville</option>';
    if (IdPays != 0) {
        $.getJSON("/Browse/GetJSONVilles/" + IdPays, function (data) {

            // $.each(data, function (idx, mar) {
            // str += '<optgroup  label="' + mar.Marque + '">'
            $.each(data, function (idx, ville) {
                str += '<option value="' + ville.Id + '">' + ville.Nom + "</option>";

            });
            $("#listeVilles").prop('disabled', false);
            $("#listeVilles").html(str);
            $("#listeVilles").val('0');
            //    str += '</optgroup>';
            //});
        });
    }
    else {
        $("#listeVilles").prop('disabled', true);
        $("#listeVilles").html(str);
        $("#listeVilles").val('0');
    }
}

function saveSearchOptions() {
    var IdContinent = $("#listeContinents").val();
    var IdRegion = $("#listeRegions").val();
    var IdPays = $("#listePays").val();
    var IdVille = $("#listeVilles").val();
    var DateIndifferente = $("#DateIndifferente:checked").val();
    var DateDepart = $("#DateDepart").val();
    var DateMarge = $("input[name=DateMarge]:checked").val();
    var DureeMini = $("#DureeMini").val();
    var DureeMaxi = $("#DureeMaxi").val();
    var PrixMini = $("#PrixMini").val();
    var PrixMaxi = $("#PrixMaxi").val();
    var NbPers = $("#NbPers").find(":selected").val();
    //var DisponibiliteMax = Math.max.apply(null,$("#NbPers:selected").each(function (i, selected) {
    //    Categories[i] = $(selected).val();
    //});
    var Categories = [];
    $("input[name=Categorie]:checked").each(function (i, selected) {
        Categories[i] = $(selected).val();
    });
    sessionStorage.setItem("Search_IdContinent", IdContinent);
    sessionStorage.setItem("Search_IdRegion", IdRegion);
    sessionStorage.setItem("Search_IdPays", IdPays);
    sessionStorage.setItem("Search_IdVille", IdVille);
    sessionStorage.setItem("Search_DateIndifferente", DateIndifferente);
    sessionStorage.setItem("Search_DateDepart", DateDepart);
    sessionStorage.setItem("Search_DateMarge", DateMarge);
    sessionStorage.setItem("Search_DureeMini", DureeMini);
    sessionStorage.setItem("Search_DureeMaxi", DureeMaxi);
    sessionStorage.setItem("Search_PrixMini", PrixMini);
    sessionStorage.setItem("Search_PrixMaxi", PrixMaxi);
    sessionStorage.setItem("Search_NbPers", NbPers);
    //sessionStorage.setItem("Search_DisponibiliteMax", DisponibiliteMax);
    sessionStorage.setItem("Search_Categories", Categories);
}

function loadPreviousSearchParams() {
    var IdContinent = sessionStorage.getItem("Search_IdContinent");
    var IdRegion = sessionStorage.getItem("Search_IdRegion");
    var IdPays = sessionStorage.getItem("Search_IdPays");
    var IdVille = sessionStorage.getItem("Search_IdVille");
    var DateIndifferente = sessionStorage.getItem("Search_DateIndifferente");
    var DateDepart = sessionStorage.getItem("Search_DateDepart");
    var DateMarge = sessionStorage.getItem("Search_DateMarge");
    var DureeMini = sessionStorage.getItem("Search_DureeMini");
    var DureeMaxi = sessionStorage.getItem("Search_DureeMaxi");
    var PrixMini = sessionStorage.getItem("Search_PrixMini");
    var PrixMaxi = sessionStorage.getItem("Search_PrixMaxi");
    var NbPers = sessionStorage.getItem("Search_NbPers");
    //var DisponibiliteMax = sessionStorage.getItem("Search_DisponibiliteMax");
    var Categories = sessionStorage.getItem("Search_Categories");


    if (IdContinent === null) {
        $("#listeContinents").val(0);
    }
    else {
        $("#listeContinents").val(IdContinent);
        if (typeof IdContinent != 0) {
            loadRegions();
        }
    }
    if (IdRegion === null) {
        $("#listeRegions").val(0);
    }
    else {
        $("#listeRegions").val(IdRegion);
        if (typeof IdRegion != 0) {
            loadPays();
        }
    }
    if (IdPays === null) {
        $("#listePays").val(0);
    }
    else {
        $("#listePays").val(IdPays);
        if (typeof IdPays != 0) {
            loadVilles();
        }
    }
    if (IdVille === null) {
        $("#listeVilles").val(0);
    }
    else {
        $("#listeVilles").val(IdVille);
    }
    if (typeof DateIndifferente !== null) {
        $("#DateIndifferente:checked").val(DateIndifferente);
    }
    if (typeof DateDepart !== "undefined") {
        $("#DateDepart").val(DateDepart);
    }
    if (typeof DateMarge !== "undefined") {
        $('input[name=DateMarge][value="' + DateMarge + '"').prop('checked', true);
    }
    if (typeof DureeMini !== "undefined") {
        $("#DureeMini").val(DureeMini);
    }
    if (typeof DureeMaxi !== "undefined") {
        $("#DureeMaxi").val(DureeMaxi);
    }
    if (typeof PrixMini !== "undefined") {
        $("#PrixMini").val(PrixMini);
    }
    if (typeof PrixMaxi !== "undefined") {
        $("#PrixMaxi").val(PrixMaxi);
    }
    if (typeof NbPers === "undefined") {
        $("#NbPers").val(1).change();
        //$("#NbPers").find(":selected").val(1);
    }
    else {
        $("#NbPers").val(NbPers).change();;
       //  $("#NbPers").find(":selected").val(NbPers);
    }
}
