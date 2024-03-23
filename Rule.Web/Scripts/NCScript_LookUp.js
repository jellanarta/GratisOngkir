//Generic look up script BEGIN
function keyIdUp(e, obj, lisId, ltsId, hdnId, minTypedLetters) {
    $("#" + hdnId).val(e.keyCode);
    if (e.keyCode == 40 && obj.value.length >= minTypedLetters) {
        __doPostBack(hdnId, '');
        $("#" + lisId).css("display", "inline-block");
        var ltSearch = $("#" + ltsId);
        ltSearch.focus();
    }
    else
        $("#" + lisId).css("display", "none");
}

function searchDown(e, obj, tkiId, lisId) {
    var ltSearch = $("#" + obj.id);
    switch (e.keyCode) {
        case 38:
            if (ltSearch.attr('selectedIndex') == 0) {
                $("#" + lisId).css("display", "none");
                $("#" + tkiId).focus();
            }
            break;
    }
    return false;
}

function searchUp(e, obj, tkiId, lisId, overlayId) {
    switch (e.keyCode) {
        case 13:
            searchSelect(obj, tkiId, lisId, overlayId);
            break;
        case 27:
            $("#" + lisId).css("display", "none");
            $("#" + tkiId).focus();
            break;
    }
    return false;
}

function searchSelect(obj, tkiId, lisId, overlayId) {
    var ltSearch = $("#" + obj.id);
    if (ltSearch.val() != 'See More') {
        eval(ltSearch.val());
        $("#" + tkiId).focus();
    }
    else {
        __doPostBack(obj.id, '');
        overlay(overlayId);
        ltSearch.attr('selectedIndex', 0);
    }
    $("#" + lisId).css("display", "none");
}

function CheckSelected(sender, args) {
    var obj = $("#" + sender["title"]);
    if (obj.val() == "") args.IsValid = false;
    else args.IsValid = true;
    alert('a');
}

function vgc(objId, targetSpanId, msg) {
    if ($("#" + objId).val() == "") {
        $("#" + targetSpanId).attr("innerHTML", msg);
        return false;
    }
    else {        
        $("#" + targetSpanId).attr("innerHTML", "");
        return true;
    }
}

function doLookupSort(hdnId) {
    __doPostBack(hdnId, '');
}
//Generic look up script END