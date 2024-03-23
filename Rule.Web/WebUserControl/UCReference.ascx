<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCReference.ascx.cs"
    Inherits="Rule.Web.WebUserControl.UCReference" %>
<asp:DropDownList runat="server" ID="ddlReference">
</asp:DropDownList>
<asp:RequiredFieldValidator runat="server" ID="rfvDdlReference" Display="Dynamic"
    ControlToValidate="ddlReference" Text="This field is required" InitialValue=""></asp:RequiredFieldValidator>
<asp:Literal runat="server" ID="ltlReference" Visible="false"></asp:Literal>
<asp:HiddenField runat="server" ID="hdnReference" />
