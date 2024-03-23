<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLoadingPanel.ascx.cs"
    Inherits="Rule.Web.WebUserControl.UCLoadingPanel" %>
<link href='<%=ResolveUrl("~/Styles/NC_LoadingPanel.css") %>' rel="stylesheet" type="text/css" />
<asp:UpdateProgress ID="upProgress" runat="server">
    <ProgressTemplate>
        <div id="loadingPanel" runat="server" class="loadingPanel">
            <div class="loading">
                <img src='<%=ResolveUrl("~/Images/loading-circle.gif") %>' />
                <br />
                <asp:Literal ID="ltlLoading" runat="server" Text="Loading"></asp:Literal>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
