$(function () {
    $("a.Continents").click(function () {

        onSelectContinent($(this).prop("id"), $(this).html())
        
    });
    resizePartialViews();

    $('#partial_view_search_result1').clone().appendTo($("#hidden_partial_view")).attr('id', 'partial_view_search_result');
});

function resizePartialViews() {
    // donner à toutes les vues partielles la même hauteur
    var maxHeight = Math.max.apply(null, $("div.partial_view_search_result").map(function () {
        return $(this).height();
    }).get());
    $("div.partial_view_search_result").css('height', maxHeight);
}


function updateSearchResultpartialViews(data) {
    $("#partial_views").html('');
    $.each(data, function (idx, obj) {
        $('#partial_view_search_result').clone().appendTo($("#partial_views")).attr('id', 'partial_view_search_result' + idx)

        // Données de l'hotel
        $('#partial_view_search_result' + idx).find('h3.nom').html(obj.hotel.nom);
        $('#partial_view_search_result' + idx).find('p.ville').html(obj.hotel.ville);
        $('#partial_view_search_result' + idx).find('p.categorie').html(obj.hotel.categorie + '*');
        $('#partial_view_search_result' + idx).find('img.img-thumbnail').attr('src', "Areas/Admin/Uploads/" + obj.hotel.photo);
        $('#partial_view_search_result' + idx).find('a.id').prop('href', '/Hotel/Details/' + obj.hotel.id + '?nav=Browse');

        // Données des produits
        var nbLignes = parseInt($('#nbProduitsAffiches').html());
        $.each(obj.produits, function (idx_prod, produit) {
            if (idx_prod < nbLignes) {
                $('#partial_view_search_result' + idx).find('tr.ligne_produit_' + idx_prod).find('td.colp-date').html(produit.dateDepart.concat());
                $('#partial_view_search_result' + idx).find('tr.ligne_produit_' + idx_prod).find('td.colp-duree').html(produit.duree + 'j');
                $('#partial_view_search_result' + idx).find('tr.ligne_produit_' + idx_prod).find('td.colp-prix').html(produit.prix + '€');
            }
        });
        // on efface les lignes en trop si nécessaire
        for (var i = obj.produits.length ; i <= nbLignes; i++) {
            $('#partial_view_search_result' + idx).find('tr.ligne_produit_' + i).find('td.colp-date').html('');
            $('#partial_view_search_result' + idx).find('tr.ligne_produit_' + i).find('td.colp-duree').html('');
            $('#partial_view_search_result' + idx).find('tr.ligne_produit_' + i).find('td.colp-prix').html('');
        }
            
    });
}

function onSelectContinent(idContinent, nomContinent) {
    loadRegions(idContinent);
    $("#texteChoix").html("Choix de la région");

    // BreadCrumb
    $("#BreadCrumb").html('Parcourir le catalogue >> <a href="/Browse" id="Continents">Continents</a> >> <span href="#" id="Continent">' + nomContinent + '</span> ');
    $('#Continent').data('id', idContinent);

    // Vues partielles adaptées au continent
    $.getJSON('/Browse/GetJsonBestHotels/?continent=' + idContinent + '&region=0&ville=0', function (data) {
        updateSearchResultpartialViews(data);
        resizePartialViews();
    });

}

function onSelectRegion(idRegion, nomRegion) {
    loadPays(idRegion);
    $("#texteChoix").html("Choix du pays");

    // BreadCrumb
    IdContinent = $('#Continent').data('id')
    $("#BreadCrumb").html('Parcourir le catalogue >> <a href="/Browse" id="Continents">Continents</a> >> <a href="#" id="Continent">' + $("#Continent").html() + '</a> >> <span href="#" id="Region">' + nomRegion + '</span> ');
    $('#Continent').data('id', IdContinent);
    $("#Continent").click(function () {
        onSelectContinent(IdContinent, $("#Continent").html());
    });
    $('#Region').data('id', idRegion);

    // Vues partielles adaptées à la région
    $.getJSON('/Browse/GetJsonBestHotels/?continent=1&region=' + idRegion + '&ville=0', function (data) {
        updateSearchResultpartialViews(data);
        resizePartialViews();
    });
}

function onSelectPays(idPays, nomPays) {

    loadVilles(idPays);
    $("#texteChoix").html("Choix de la ville");

    IdContinent = $('#Continent').data('id');
    IdRegion = $('#Region').data('id');
    $("#BreadCrumb").html('Parcourir le catalogue >> <a href="Browse" id="Continents">Continents</a> >> <a href="#" id="Continent">' + $("#Continent").html() + '</a> >> <a href="#" id="Region">' + $("#Region").html() + '</a> >> <span href="#" id="Pays">' + nomPays + '</span> ');
    $('#Continent').data('id', IdContinent);
    $('#Region').data('id', IdRegion);
    $('#Pays').data('id', idPays);
    $("#Continent").click(function () {
        onSelectContinent(IdContinent, $("#Continent").html());
    });
    $("#Region").click(function () {
        onSelectRegion(IdRegion, $("#Region").html())
    });

    // Vues partielles adaptées au Pays
    $.getJSON('/Browse/GetJsonBestHotels/?continent=1&region=0&Pays=' + idPays + '&ville=0', function (data) {
        updateSearchResultpartialViews(data);
        resizePartialViews();
    });
}

function onSelectVille(idVille, nomVille) {

    // loadVilles(idVille);
    $("#listeChoix").html('');
    $("#texteChoix").html("Hôtels disponibles");

    IdContinent = $('#Continent').data('id');
    IdRegion = $('#Region').data('id');
    IdPays = $('#Pays').data('id');
    $("#BreadCrumb").html('Parcourir le catalogue >> <a href="Browse" id="Continents">Continents</a> >> <a href="#" id="Continent">' + $("#Continent").html() + '</a> >> <a href="#" id="Region">' + $("#Region").html() + '</a> >> <a href="#" id="Pays">' + $("#Pays").html() + '</a> >> <span href="#" id="Ville">' + nomVille + '</span> ');
    $('#Continent').data('id', IdContinent);
    $('#Region').data('id', IdRegion);
    $('#Pays').data('id', IdPays);
    $('#Ville').data('id', idVille);
    $("#Continent").click(function () {
        onSelectContinent(IdContinent, $("#Continent").html());
    });
    $("#Region").click(function () {
        onSelectRegion(IdRegion, $("#Region").html())
    });
    $("#Pays").click(function () {
        onSelectPays(IdPays, $("#Pays").html())
    });

    // Vues partielles adaptées au Pays
    $.getJSON('/Browse/GetJsonHotels/?continent=1&region=0&Ville=' + idVille, function (data) {
        updateSearchResultpartialViews(data);
        resizePartialViews();
    });
}


function loadRegions(IdContinent) {
    var str = '';
    if (IdContinent != 0) {
        $.getJSON("/Browse/GetJSONRegions/" + IdContinent, function (data) {

            $.each(data, function (idx, region) {
                str += '<li><a class="Regions btn-info li-browse" href="#" id="' + region.Id + '">' + region.Nom + '</a></li>';
            });

            $("#listeChoix").html(str);
                      
            $("a.Regions").click(function () {
                onSelectRegion($(this).prop("id"), $(this).html())
            });
        });
        
        // $('div.partial_view_search_result').html('');
    }
}


function loadPays(IdRegion) {
    var str = '';
    if (IdRegion != 0) {
        $.getJSON("/Browse/GetJSONPays/" + IdRegion, function (data) {

            $.each(data, function (idx, pays) {
                str += '<li><a class="Pays btn-info li-browse" href="#" id="' + pays.Id + '">' + pays.Nom + '</a></li>';
            });

            $("#listeChoix").html(str);
            $("a.Pays").click(function () {
                onSelectPays($(this).prop("id"), $(this).html());
            });
        });
    }
}


function loadVilles(IdPays) {
    var str = '';
    if (IdPays != 0) {
        $.getJSON("/Browse/GetJSONVilles/" + IdPays, function (data) {

            $.each(data, function (idx, ville) {
                str += '<li><a class="Villes btn-info li-browse" href="#" id="' + ville.Id + '">' + ville.Nom + '</a></li>'; //href="/Search/Result/' 
            });

            $("#listeChoix").html(str);
            $("a.Villes").click(function () {
                onSelectVille($(this).prop("id"), $(this).html());
            });
        });
    }
}
