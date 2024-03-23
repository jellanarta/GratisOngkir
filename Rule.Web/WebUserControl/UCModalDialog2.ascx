<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCModalDialog2.ascx.cs" Inherits="Rule.Web.WebUserControl.UCModalDialog2" %>
<div id="<%= ModalDialogName %>" class="overlay">
    <div id="dv" runat="server" class="modalDialogContent">     
        <a href="javascript:overlay('<%= ModalDialogName %>')" class="close">[x]</a>
        <br />
<%--        <asp:PlaceHolder ID="plov" runat="server">
        </asp:PlaceHolder>--%>
        <asp:Panel runat="server" ID="plov" ScrollBars="Auto" Height="500" Width="800" />
        <%--<asp:Panel runat="server" ID="plov" ScrollBars="Auto" />--%>
    </div>
</div>