<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCGridHeader.ascx.cs" Inherits="Rule.Web.WebUserControl.UCGridHeader" %>

<div class="pager" align="left">
    <asp:LinkButton runat="server" ID="lbRefreshGrid" Text="Refresh Grid" OnCommand="pagerHeader_Click" CommandName="Refresh" CausesValidation="false"></asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbAdd" Text="Add" OnCommand="pagerHeader_Click" CommandName="Add" Visible="false" CssClass="rightLink"></asp:LinkButton>
</div>
