<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCSearchSimple.ascx.cs" Inherits="Rule.Web.WebUserControl.Search.UCSearchSimple" %>
<%@ Register Src="../UCDatePicker.ascx" TagName="UCDatePicker" TagPrefix="uc1" %>
<%@ Register Src="../UCReference.ascx" TagName="UCReference" TagPrefix="uc2" %>
<div id="dSearchForm">
    <asp:UpdatePanel runat="server" ID="upFixedSearch" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Repeater runat="server" ID="rptFixedSearch" OnItemDataBound="rptFixedSearch_ItemDataBound">
                <HeaderTemplate>
                    <table class="formTable">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="width: 20%" class="tdDesc">
                            <asp:Label runat="server" ID="lblText" Text='<%# Eval("Text") %>'></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlSearchCond" CssClass="ddlInput" Width="120px"
                                Visible="false">
                            </asp:DropDownList>
                        </td>
                        <td class="tdValue">
                            <asp:TextBox runat="server" ID="txtSearchValue" CssClass="txtInput"></asp:TextBox>
                            <uc2:UCReference ID="ucReference" runat="server" Visible="false" />
                            <asp:DropDownList runat="server" ID="ddlBool" CssClass="ddlInput" Width="50px" Visible="false">
                                <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <uc1:UCDatePicker ID="ucDatePicker" runat="server" Visible="false" />
                            <asp:Label runat="server" ID="lblDescription" Visible="false"></asp:Label>
                            <asp:RangeValidator runat="server" ID="rvNumber" Type="Integer" MinimumValue="0"
                                MaximumValue="99999999" ControlToValidate="txtSearchValue" ErrorMessage="Wrong input"
                                Enabled="false" SetFocusOnError="true" Display="Dynamic">
                            </asp:RangeValidator>
                            <asp:RequiredFieldValidator runat="server" ID="rfvInput" ErrorMessage="This field is required"
                                ControlToValidate="txtSearchValue" Enabled="false" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="formTable">
        <tr>
            <td align="right">
                <asp:UpdatePanel runat="server" ID="upButtonSearch" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:LinkButton runat="server" ID="lbSearch" Text="SEARCH" CssClass="button" OnClick="lbSearch_Click"
                            CausesValidation="true"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lbReset" Text="RESET" CssClass="buttonComp" OnClick="lbReset_Click"
                            CausesValidation="false"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</div>
