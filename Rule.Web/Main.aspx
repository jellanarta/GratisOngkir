<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Rule.Web.Main" %>

<%@ Register Src="WebUserControl/UcModalDialog.ascx" TagName="UcModalDialog" TagPrefix="uc1" %>
<%--<%@ Register Src="WebUserControl/UCNotification.ascx" TagName="UCNotification" TagPrefix="uc2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Confins - Main</title>
</head>
<body onresize="resize()">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="smMainPage">
    </asp:ScriptManager>
    <div id="header">
        <span><a href="http://www.ad-ins.com" target="_blank">
            <img src="Images/CONFINS.png" />
        </a></span><span id="navMenu">
            <asp:LinkButton runat="server" ID="navLogout" OnClick="navLogout_Click" OnClientClick="return confirm('Are you sure you want to quit?');"
                Text="LOGOUT" ></asp:LinkButton>
            <br />
            <asp:Label runat="server" ID="ltlBusinessDate" CssClass="businessDate"></asp:Label>
        </span><span id="userDetails">
            <asp:Literal ID="ltlWelcomeMsg" runat="server"></asp:Literal></span>
    </div>
    <div id="main">
        <div id="toggle">
            <%--<a id="toggleLink" href="javascript:ShowCloseMenu('iframe#treeContainer', 'iframe#favouriteContainer')"
                style="color: Black;">--%>
                <div id="dMenuPadding">
                    <img id="imgMenu" src="Images/menu.png" /></div>
            </a>
        </div>
        <iframe runat="server" id="mainPage" name="mainPage"></iframe>
    </div>
    <div id="footer">
        <label>
            Copyright &copy; AdIns 2011. All Right Reserved</label>
    </div>
    <div id="errorList" runat="server">
        <div id="errorTitle">
            <asp:Literal runat="server" ID="ltrErrorTitle" Text="Warning Messages"></asp:Literal>
            <a href="javascript:CloseErrorList()" id="closeErrorList">[x]</a> <a href="javascript:ExpandUnexpandErrorList()"
                id="minErrorList">[-]</a></div>
        <asp:ListBox runat="server" ID="lbErrorContent"></asp:ListBox>
    </div>
    <asp:HiddenField ID="hdnSubsystem" runat="server" />
    <asp:HiddenField ID="hdnFormId" runat="server" Value="49" />
    </form>
</body>
</html>
