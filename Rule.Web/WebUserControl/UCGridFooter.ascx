<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCGridFooter.ascx.cs" Inherits="Rule.Web.WebUserControl.UCGridFooter" %>

<div class="pager" align="left">
    <asp:LinkButton runat="server" ID="lbFirstRecord" Text="|<" OnCommand="Navigation_Click" Enabled="false"
        AutoPostBack="true" CommandName="First" CausesValidation="false"></asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbPrevRecord" Text="<" OnCommand="Navigation_Click" Enabled="false"
         AutoPostBack="true" CommandName="Prev" CausesValidation="false"></asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbNextRecord" Text=">" OnCommand="Navigation_Click"
         AutoPostBack="true" CommandName="Next" CausesValidation="false"></asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbLastRecord" Text=">|" OnCommand="Navigation_Click" Enabled="false"
         AutoPostBack="true" CommandName="Last" CausesValidation="false"></asp:LinkButton>
    <asp:DropDownList runat="server" ID="ddlPageSize" CssClass="ddlInput" Width="50px"
        AutoPostBack="true" onselectedindexchanged="ddlPageSize_SelectedIndexChanged" CausesValidation="false">
        <asp:ListItem Text="10" Value="10"></asp:ListItem>
        <asp:ListItem Text="20" Value="20"></asp:ListItem>
        <asp:ListItem Text="50" Value="50"></asp:ListItem>
    </asp:DropDownList>
    <asp:Literal runat="server" ID="ltlRecord" Text="Records each page"></asp:Literal>
    <span style="float: right; height: 19px;">
        <asp:LinkButton runat="server" ID="lbCountRecords" 
         AutoPostBack="true" Text="Count Total Records" onclick="lbCountRecords_Click" CausesValidation="false"></asp:LinkButton>
    </span>
</div>
