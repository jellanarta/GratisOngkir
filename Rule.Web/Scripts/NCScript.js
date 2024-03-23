//digunakan untuk membuat subsection agar dapat di-hide/unhide
//ketika di-hide, link akan berubah menjadi [+], kalau di-unhide akan berubah menjadi [-]
function ExpandUnexpandMenu(expander, affected, title) {
    var affected = $('#' + affected);
    var expander = $('#' + expander);
    var title = $('#' + title);

    if (affected.is(':hidden')) {
        affected.slideDown();
        expander.text('[-]');
        expander.css("color", "black");
        title.css("color", "black");
    }
    else {
        affected.slideUp();
        expander.text('[+]');
        expander.css("color", "gray");
        title.css("color", "gray");
    }
}

function Status(status) {
    window.status = status;
}

function padZero(number, length) {
    var str = '' + number;
    while (str.length < length) {
        str = '0' + str;
    }
    return str;
}

function overlay(id) {
    overlay(id, false);
}

function overlay(id, isValidate) {
    if (typeof Page_IsValid != 'undefined' && isValidate)
        if (!Page_IsValid) return;
    el = $("#" + id);
    if (el.css("visibility") == "visible") {
        el.css("visibility", "hidden");
    }
    else {
        $("#" + id + " > div").center();
        el.css("visibility", "visible");
    }
}

function EndRequestHandler(sender, args) {
    var data, response;
    if (response = args.get_response())
        if (response._responseAvailable && response._xmlHttpRequest)
            data = response._xmlHttpRequest.responseText;
    var sign = 'eval=';
    if (data) {
        if (data.indexOf(sign) == 0) {
            eval(data.substr(sign.length));
            return;
        }
    }

    errorHandler(args);
}

function errorHandler(args) {
    if (args.get_error() != undefined) {
        if (args.get_response().get_statusCode() == '200') {
            var errorMessage = args.get_error().message;

            var indexOfColon = errorMessage.indexOf(':');
            if (indexOfColon != -1 && (indexOfColon + 2) <= errorMessage.length)
                errorMessage = errorMessage.substr(indexOfColon + 2);

            showErrorMessage(errorMessage);
        }
        else
            showErrorMessage('An unspecified error occurred.');
        args.set_errorHandled(true);
    }
}

function openScreen(url, ifid) {
    $('iframe#' + ifid).attr('src', url);
    //    document.getElementById(ifid).src = url;
}

//function centering element
//untuk pemakaian dengan $(element).center()
jQuery.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", ($(window).height() - this.height()) / 2 + "px");       /*$(window).scrollTop() +*/
    this.css("left", ($(window).width() - this.width()) / 2 + "px");        /*$(window).scrollLeft() +*/
    return this;
}

$(
    function () {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    }
);

//untuk update progress
window_onload = pageLoad;
function pageLoad() {
    resize();
    $('.loading').center();
}

//Main page script BEGIN
function resize() {
    var htmlheight = document.body.parentNode.clientHeight - 49;
    var htmlwidth = document.body.parentNode.clientWidth - 25;

    $('iframe#mainPage').width(htmlwidth);
    $('div#main').height(htmlheight);

    var toggleMenuPadding = (htmlheight * 0.6 - 121) / 2;
    var toggleFavouritePadding = (htmlheight * 0.4 - 121) / 2;

    $('div#dMenuPadding').css('padding', toggleMenuPadding + 'px 0px');
    $('div#dFavouritePadding').css('padding', toggleFavouritePadding + 'px 0px');
}

function formatValue(pInput, pHidden, pValidasi) {
    var oInput = document.getElementById(pInput);
    var oHidden = document.getElementById(pHidden);
    var oValidasi = document.getElementById(pValidasi);

    if (oInput != undefined) {
        if (oInput.value != undefined) {
            var ftotal = oInput.value;
            while (ftotal.indexOf(",") > 0) {
                ftotal = ftotal.replace(",", "");
            }
            oInput.value = FormatPercentNumber(ftotal);
            ftotal = oInput.value;

            while (ftotal.indexOf(",") > 0) {
                ftotal = ftotal.replace(",", "");
            }
            oHidden.value = ftotal;
            oValidasi.value = ftotal;
        }
    }
}

function clearFormatValue(pInput) {
    var oInput = document.getElementById(pInput);
    var ftotal = oInput.value;
    while (ftotal.indexOf(",") > 0) {
        ftotal = ftotal.replace(",", "");
    }
    oInput.value = ftotal;
}

function getFormattedValue(value) {
    var ftotal = value;
    while (ftotal.indexOf(",") > 0) {
        ftotal = ftotal.replace(",", "");
    }
    //alert(ftotal);
    return parseFloat(ftotal);
}

function FormatPercentNumber(num) {
    if (isNaN(num)) { num = "0"; }
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10) { cents = "0" + cents; }
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    }
    return (((sign) ? '' : '-') + num + '.' + cents);
}

function showPopupWindows(url) {
    var x = window.open(url, 'manualContainer', 'width=800,height=600,scrollbars=yes');
    x.focus();
}


//Begin of UCInputPercent
function formatValuePercent(pInput, pHidden, pValidasi) {
    var oInput = document.getElementById(pInput);
    var oHidden = document.getElementById(pHidden);
    var oValidasi = document.getElementById(pValidasi);

    if (oInput != undefined) {
        if (oInput.value != undefined) {
            var ftotal = oInput.value;
            while (ftotal.indexOf(",") > 0) {
                ftotal = ftotal.replace(",", "");
            }
            oInput.value = FormatPercentNumberPercent(ftotal);
            ftotal = oInput.value;

            while (ftotal.indexOf(",") > 0) {
                ftotal = ftotal.replace(",", "");
            }
            oHidden.value = ftotal;
            oValidasi.value = ftotal;
        }
    }
}

function FormatPercentNumberPercent(num) {
    if (isNaN(num)) { num = "0"; }
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 1000000 + 0.50000000001);
    cents = num % 1000000;
    num = Math.floor(num / 1000000).toString();
    if (cents < 10) { cents = "0" + cents; }
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    }
    return (((sign) ? '' : '-') + num + '.' + cents);
}

//End of UCInputPercent

//Begin of Integer
function formatValueInteger(pInput, pHidden, pValidasi) {
    var oInput = document.getElementById(pInput);
    var oHidden = document.getElementById(pHidden);
    var oValidasi = document.getElementById(pValidasi);

    if (oInput != undefined) {
        if (oInput.value != undefined) {
            var ftotal = oInput.value;
            while (ftotal.indexOf(",") > 0) {
                ftotal = ftotal.replace(",", "");
            }
            oInput.value = FormatPercentNumberInteger(ftotal);
            ftotal = oInput.value;

            while (ftotal.indexOf(",") > 0) {
                ftotal = ftotal.replace(",", "");
            }
            oHidden.value = ftotal;
            oValidasi.value = ftotal;
        }
    }
}

function FormatPercentNumberInteger(num) {
    if (isNaN(num)) { num = "0"; }
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10) { cents = "0" + cents; }
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    }
    return (((sign) ? '' : '-') + num);
}

//End of Integer