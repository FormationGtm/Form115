$(function () {
    $("#champDateDepart").datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        language: "fr",
        autoclose: true,
        todayHighlight: true,
        minDate: "0" // A reprendre
    });

    loadSearchParams();

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
        $(this).attr('hidden','true')
        return false;
    });
})

function chargerListeProduits() {
    console.log("Fonction appelée.");
    var tbodyListeProduits = $("#tbodyListeProduits");
    var obj = {
        IdHotel: $("[name=IdHotel]").val(),
        DateDepart: $("[name=DateDepart]").val(),
        DureeMinSejour: $("[name=DureeMinSejour]").val(),
        DureeMaxSejour: $("[name=DureeMaxSejour]").val(),
        DateDebut: $("[name=DateDebut]").val(),
        DateFin: $("[name=DateFin]").val(),
        NbPers: $("[name=NbPers]").find(":selected").val(),
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
                    str += "<td style='text-align:right'>" + obj.NbPers+ "</td>";
                    str += "<td class='btn btn-info'><a href='/Reservations/Reserver/"+item.sejour+"?quantite="+obj.NbPers +"'>Réserver</a></td>";
                    str += "</tr>";
                });
                console.log("each exécuté.")
                $("#tbodyListeProduits").html(str);
            }
        }
    );
}

function loadSearchParams() {
    var DateDepart = sessionStorage.getItem("DateDepart");
    var Duree = sessionStorage.getItem("Duree");
    //var NbPers = sessionStorage.getItem(NbPers);

    var DureeMin = (Duree != null) ? Math.max(1, Duree - 2) : 1;
    var DureeMax = (Duree != null) ? parseInt(Duree) + 2 : null;

    $("[name=DateDepart]").val(DateDepart);
    $("[name=DureeMinSejour]").val(DureeMin) ;
    $("[name=DureeMaxSejour]").val(DureeMax);
}

