<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCSubSectionContainer.ascx.cs"
    Inherits="Rule.Web.WebUserControl.UCSectionContainer" %>
<div class="subSection">
    <a href="javascript:ExpandUnexpandMenu('<%= toggleID %>','<%= affectedID %>', '<%= subSectionID.ClientID %>')" id="<%= toggleID %>">
        [-]</a>
    <asp:Label runat="server" ID="subSectionID"></asp:Label>
</div>
