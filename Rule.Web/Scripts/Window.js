var windowIndex = 0;

function openNewWindow(id, title, url, enableMin,enableMax,enableClose)
{
  var cw, ch, fw, fh, n, fx, fy, xf = xFenster.instances;
  //x = 200+windowIndex*20;
  //y = 50+windowIndex*10;
  x = 300;
  y = 1;
  windowIndex++;
  w = document.documentElement.clientWidth - 300;
  h = document.documentElement.clientHeight - 22;
  if(xf[id]) { // if it already exists
    xf[id].show();
    if (url) xf[id].href(url);
    if (title) xf[id].title(title);
    xf[id].restore();
  }
  else { // it doesn't yet exist so create it
    cw = xClientWidth();
    ch = xClientHeight();
    fw = w || (cw / 3);
    fh = h || (ch / 3);
    n = Number(id.substr(2));
    fx = x || (40 * n);
    fy = y || (100 + (n * 40));
    new xFenster(id, title, url, fx, fy, fw, fh, 200, null, 0, 1, 0,
                 true, true, enableMin, enableMax, enableClose, false, true,
                 null, null, null, null, null, null, null, null,
                 'xfCon', 'xfClient', 'xfTBar', 'xfTBarF', 'xfSBar', 'xfSBarF',
                 'xfRIco', 'xfNIco', 'xfMIco', 'xfOIco', 'xfCIco',
                 'Resize', 'Minimize', 'Maximize', 'Restore', 'Close');
  }
}

function closeAllWindows() {
    if(confirm('Are you sure you want to close all windows?'))
    {
        var o = xFenster.instances;
        for(i in o)
        {
            if(o[i]) {
                if (o[i].client.id != 'menuWin')o[i].hide();
            }
        }
    }
}

function minAllWindows() {
    var o = xFenster.instances;
    for (i in o) {
        if (o[i] && !o[i].hidden && o.hasOwnProperty(i) && !o[i].minimized) {
            if (o[i].client.id != 'menuWin') o[i].minimize();
        }
    }
}

function openScreen(url,ifid) {
    document.getElementById(ifid).src = url;
}

var menuFocusFlag;
function showMenu(ifid) {
    var ifObj = document.getElementById(ifid);
    if (ifObj.style.visibility == 'hidden') {
        ifObj.style.visibility = 'visible';
        ifObj.style.zIndex = 2;
        menuFocusFlag = true;
    }
    else {
        closeMenu(ifid);
    }
}

function closeMenu(ifid) {
    if(menuFocusFlag)
    {          
        var ifObj = document.getElementById(ifid);
        ifObj.style.visibility = 'hidden';
        ifObj.style.zIndex = 0;
        menuFocusFlag = false; 
    }
}