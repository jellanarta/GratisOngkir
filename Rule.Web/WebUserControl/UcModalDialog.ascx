<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcModalDialog.ascx.cs" Inherits="Rule.Web.WebUserControl.UcModalDialog" %>
<div id="<%= ModalDialogName %>" class="overlay">
    <div id="dv" runat="server" class="modalDialogContent">     
        <a href="javascript:overlay('<%= ModalDialogName %>')" class="close">[x]</a>
        <br />
        <asp:PlaceHolder ID="plov" runat="server">
        </asp:PlaceHolder>
    </div>
</div>