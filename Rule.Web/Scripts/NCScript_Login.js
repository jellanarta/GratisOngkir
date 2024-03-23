function show() {
    $('#loginFrame').fadeIn(500, function () {
        document.getElementById('txt_RefUser_Username').focus();
        $('#imgLogin').fadeIn(500, function () {
            resize();
            $('#compLogo').fadeIn(500);
            $('#copyright').fadeIn(500);
            $('#overlay').fadeIn(1);
        });
    });
}

function resize() {
    $('#imgLogin').height(document.body.parentNode.clientHeight);
}

function textbox_keypressed(event) {
    if (event.keyCode == '13') {
        if (!basicLoginValidation()) {
            return;
        }
        window.focus();
        __doPostBack('lbLogin', '');
    }
}

function basicLoginValidation() {
    showErrorMessage('');
    if (document.getElementById('txt_RefUser_Username').value == '' || document.getElementById('txt_RefUser_Password').value == '') {
        showErrorMessage('Please fill username and password.');
        return false;
    }
    else {
        return true;
    }
}

function showErrorMessage(errorMessage) {
    var indexOfNewArray = errorMessage.indexOf('new Array(');
    if (indexOfNewArray == -1 || indexOfNewArray > 0)
        errorMessage = 'new Array("' + errorMessage + '")';

    document.getElementById('lblError').innerHTML = eval(errorMessage);
}

$(function () {
    $('#txt_RefUser_Username').keypress(textbox_keypressed);
    $('#txt_RefUser_Password').keypress(textbox_keypressed);
});