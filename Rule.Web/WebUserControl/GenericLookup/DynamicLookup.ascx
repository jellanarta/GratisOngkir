<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicLookup.ascx.cs"
    Inherits="Rule.Web.WebUserControl.GenericLookup.DynamicLookup" %>
<asp:UpdatePanel runat="server" ID="upLookup" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>
        <asp:TextBox runat="server" ID="txt"></asp:TextBox><asp:ImageButton runat="server"
            ID="ib" ImageUrl="~/Images/Icon/IconLookup.gif" OnClick="ib_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
