$(function () {
    // donner à toutes les vues partielles la même hauteur
    var maxHeight = Math.max.apply(null, $("div.partial_view_search_result").map(function ()
    {
        return $(this).height();
    }).get());
    $("div.partial_view_search_result").css('height', maxHeight);
});