<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCSearch.ascx.cs" Inherits="Rule.Web.WebUserControl.Search.UCSearch" %>
<div class="subSection">
    <asp:Label runat="server" ID="subSectionID" Text="Search"></asp:Label>
</div>
<div id="dSearchForm">
    <asp:UpdatePanel runat="server" ID="upFixedSearch" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:PlaceHolder runat="server" ID="phFixedSearch"></asp:PlaceHolder>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="formTable">
        <tr>
            <td align="right">
                <asp:UpdatePanel runat="server" ID="upButton" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="btnSearch" Text="SEARCH" CssClass="button" OnClick="btnSearch_Click"
                            CausesValidation="true"></asp:Button>
                        <asp:LinkButton runat="server" ID="lbReset" Text="RESET" CssClass="buttonComp" OnClick="lbReset_Click"
                            CausesValidation="false"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</div>
