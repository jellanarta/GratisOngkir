<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCust.aspx.cs" Inherits="Training.AddCust" %>
<%@ Register Src ="~/WebUserControl/UcDatePicker.ascx" TagName="UcDatePicker" TagPrefix="ucDate"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div>
        <asp:ScriptManager runat ="server" ID ="smApplication"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="upPath" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="linkPath">
                    <asp:Label runat="server" ID="lblPath" Text="AddCust.aspx"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="upToolbar" UpdateMode="Conditional">
        <ContentTemplate>
        <div runat="server" id="toolbar">
            <span><label id="pageTitle">Add Customer</label></span><span id="toolMenuContainer">
                <asp:LinkButton runat="server" CssClass="lbMenu" ID="lbSave" Text="Save" OnClick="lbSave_Click"></asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="lbMenu" ID="lbReset" Text="Reset" OnClick="lbReset_Click" ></asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="lbMenu" ID="lbView" Text="View" OnClick="lbView_Click" ></asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="lbMenu" ID="lbDel" Text="Delete" OnClick="lbDel_Click" ></asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="lbMenu" ID="lbRedirect" Text="Redirect" OnClick="lbRedirect_Click" ></asp:LinkButton>
            </span>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <br /><br /><br /><br /><br />
        <div id="dContent">
            <asp:UpdatePanel runat="server" ID="upForm" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div id ="dForm" class ="Form" runat="server">
                        <div id="dinput">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Literal runat="server" ID ="ltl_CustName" Text ="Customer Name"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID ="txt_CustName"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID ="rbl_Gender" runat ="server" RepeatDirection ="Horizontal">
                                            <asp:ListItem Value ="M" Text ="Male"></asp:ListItem>
                                            <asp:ListItem Value ="F" Text ="Female"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal runat ="server" ID="ltl_BirthDate" Text="Customer Birth Date"></asp:Literal>
                                    </td>
                                    <td>
                                        <ucDate:UcDatePicker runat="server" id ="UCBirthDate"></ucDate:UcDatePicker>
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox runat="server" ID ="txt_id"></asp:TextBox>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
