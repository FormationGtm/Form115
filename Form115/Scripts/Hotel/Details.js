$(function () {
    $("#DateDepart").datepicker({
        format: "mm/dd/yyyy",
        todayBtn: "linked",
        language: "fr",
        autoclose: true,
        todayHighlight: true,
        minDate: "0" // A reprendre
    });

    if ($("input[name=Nav]").val() == "Search") {
        loadSearchParams();
    }

    $("input").change(chargerListeProduits);
    $("select").change(chargerListeProduits);
    chargerListeProduits();

    // Réponses à des commentaires : afficher un formulaire idoine
    $("a.LienRepondre").click(function () {
        console.log('hello');
        var commentaireId = $(this).attr('id').split('-').pop();
        var formulaireId = "FormulaireReponse-" + commentaireId;
        $("#FormulaireCommentaire").clone().appendTo($(this).parent()).attr("id", formulaireId);
        $('#' + formulaireId).find('input[name="IdCommentaire"]').attr('value', commentaireId)
        $(this).attr('hidden', 'true')
        return false;
    });
})

function chargerListeProduits() {
    console.log("Fonction appelée.");
    var tbodyListeProduits = $("#tbodyListeProduits");
    var obj = {
        IdHotel: $("[name=IdHotel]").val(),
        // DateDepart: $("[name=DateDepart]").val(),
        // DureeMinSejour: $("[name=DureeMinSejour]").val(),
        // DureeMaxSejour: $("[name=DureeMaxSejour]").val(),
        // DateDebut: $("[name=DateDebut]").val(),
        // DateFin: $("[name=DateFin]").val(),
        // NbPers: $("[name=NbPers]").find(":selected").val(),
        DateIndifferente: $("#DateIndifferente:checked").val(),
        DateDepart: $("#DateDepart").val(),
        DateMarge: $("input[name=DateMarge]:checked").val(),
        DureeMini: $("#DureeMini").val(),
        DureeMaxi: $("#DureeMaxi").val(),
        PrixMini: $("#PrixMini").val(),
        PrixMaxi: $("#PrixMaxi").val(),
        NbPers: $("#NbPers").find(":selected").val()
    };
    console.log(obj);
    $.post(
        '/Hotel/listeProduits',
        obj,
        function (data) {
            console.log("Requête effectuée et réponse reçue.");
            console.log(data);
            var str = "";
            if (data.length == 0) {
                $("#tbodyListeProduits").html('<tr><td colspan="0" class="colspan0">Aucun résultat</td></tr>');
            }
            else {
                $.each(data, function (index, item) {
                    str += "<tr>";
                    str += "<td>" + item.date + "</td>";
                    str += "<td>" + item.duree + " jours</td>";
                    // TODO CSS
                    if (item.promotions == 0) {
                        str += "<td style='text-align:right'>" + item.prix + "€</td>";
                    }
                    else {
                        str += "<td style='text-align:right'><s>" + item.prix + '€</s> <span class="text-info">' + item.prixSolde + "€</span> </td>";
                    }
                    str += "<td style='text-align:right'>" + item.nb_restants + "</td>";
                    str += "<td style='text-align:right'>" + obj.NbPers + "</td>";
                    str += "<td><a class='btn-info btn sansBordure' href='/Reservations/Reserver/" + item.sejour + "?quantite=" + obj.NbPers + "'>Réserver</a></td>";
                    str += "</tr>";
                });
                console.log("each exécuté.")
                $("#tbodyListeProduits").html(str);
            }
        }
    );
    $("input[name=DateIndifferente]:checkbox").change(setDateAbility)
}

function loadSearchParams() {
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
    if (typeof NbPers !== "undefined") {
        $("#NbPers").val(NbPers);
    }
}

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