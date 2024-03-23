function showErrorMessage(errorMessage) {
    var indexOfNewArray = errorMessage.indexOf('new Array(');
    if (indexOfNewArray == -1 || indexOfNewArray > 0)
        errorMessage = 'new Array("' + errorMessage + '")';

    if (parent.ShowErrorList)
        parent.ShowErrorList(eval(errorMessage));
}

$(
    function () {
        if (parent.CloseErrorList)
            parent.CloseErrorList();
    }

);

function deleteRecordConfirm() {
    return confirm('Are you sure want to delete this record?');
}

window.onclick = function () {
    if (parent.formClick)
        parent.formClick();
}
