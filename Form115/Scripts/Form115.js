function selectionnerOption(idListeDeroulante, valueOption) {
    var selecteur = "#"+idListeDeroulante+" > option[value=" +valueOption + "]";
    $(selecteur).attr("selected", "selected");
}