function ExpandUnexpandMenu(affected) {
    if ($(affected).is(':hidden')) {
        $(affected).animate({ width: 'show' });
    }
    else {
        $(affected).animate({ width: 'hide' });
    }
}
function ShowCloseMenu(affected, beside) {
    if ($(affected).is(':hidden')) {
        $(affected).animate({ width: 'show' });
    }
    else {
        $(affected).animate({ width: 'hide' });
    }
    if (!$(beside).is(':hidden')) {
        $(beside).animate({ width: 'hide' });
    }
}

function ExpandUnexpandErrorList() {
    if ($('#minErrorList').text() == '[-]') {
        $('#errorList').css("height", "13px");
        $('#lbErrorContent').css("display", "none");
        $('#minErrorList').text('[+]');
    }
    else {
        $('#errorList').css("height", "100px");
        $('#lbErrorContent').css("display", "block");
        $('#minErrorList').text('[-]');
    }
}

function ShowErrorList(msg) {
    $('#errorList').css("display", "block");
    if ($('#minErrorList').text() != '[-]') ExpandUnexpandErrorList();

    $('#lbErrorContent option').each(function (i, option) { $(option).remove(); });
    for (i = 0; i < msg.length; i++)
        $('#lbErrorContent').append(new Option("- " + msg[i]));
}
function CloseErrorList() {
    $('#lbErrorContent option').each(function (i, option) { $(option).remove(); });
    $('#errorList').css("display", "none");
}
//Main page script END

function showErrorMessage(errorMessage) {
    var indexOfNewArray = errorMessage.indexOf('new Array(');
    if (indexOfNewArray == -1 || indexOfNewArray > 0)
        errorMessage = 'new Array("' + errorMessage + '")';

    ShowErrorList(eval(errorMessage));
}

$(
    function () {
        $('div#errorTitle').dblclick(
        function () {
            ExpandUnexpandErrorList();
        });
    }
);

function openScreen(url, ifid) {
    $('iframe#' + ifid).attr('src', url);
    //    document.getElementById(ifid).src = url;
}

function formClick() {
    var treeContainer = $('iframe#treeContainer');
    var favouriteContainer = $('iframe#favouriteContainer');

    if (!treeContainer.is(':hidden')) {
        treeContainer.animate({ width: 'hide' });
    }

    if (!favouriteContainer.is(':hidden')) {
        favouriteContainer.animate({ width: 'hide' });
    }
}

$('body').click(function () {
    formClick();
});


//function ReceiveServerData(arg, context) {
//    //alert(arg);
//}
//function setSubsystemValues(subsystem) {
//    document.getElementById("hdnSubsystem").value = subsystem;
//    CallTheServer(subsystem, '');
//}

var notifTime = 0;
function ShowSaveNotification(messageType) {
    var saveNotification = $('div#saveNotification');
    var messageContent = $('p#messageContent');

    window.clearTimeout(notifTime);
    if (messageType == "save")
        messageContent.text("Save Success");
    else if (messageType == "edit")
        messageContent.text('Edit Success');
    else if (messageType == "delete")
        messageContent.text('Delete Success');
    else
        messageContent.text(messageType);

    saveNotification.fadeIn('slow');
    notifTime = window.setTimeout(function () {
        saveNotification.fadeOut('slow');
    }, 5000);
}

function showNotes(notesText) {
    var notesContainer = $('div#notesContainer');
    var notesMessage = $('p#notesMessage');

    if (notesContainer.is(':hidden')) {
        notesContainer.animate({ height: 'show' });
        notesMessage.html(notesText);
    }
    else {
        notesContainer.animate({ height: 'hide' });
    }
}

function hideElement(elementID) {
    if (!$(elementID).is(':hidden')) {
        $(elementID).animate({ height: 'hide' });
    }
}

function showPopupWindows(url) {
    var x = window.open(url, 'manualContainer', 'width=800,height=600,scrollbars=yes');
    x.focus();
}

function fillHiddenField(target, value) {
    document.getElementById(target).value = value;
}
