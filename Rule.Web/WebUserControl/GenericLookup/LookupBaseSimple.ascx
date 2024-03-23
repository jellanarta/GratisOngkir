<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LookupBaseSimple.ascx.cs" Inherits="Rule.Web.WebUserControl.GenericLookup.LookupBaseSimple" %>
<%@ Register src="~/WebUserControl/UcModalDialog2.ascx" tagname="UcModalDialog2" tagprefix="uc1" %>
<%@ Register Src="~/WebUserControl/UCGridFooter.ascx" TagName="UCGridFooter" TagPrefix="uc2" %>
<%@ Register src="~/WebUserControl/UCGridHeader.ascx" tagname="UCGridHeader" tagprefix="uc3" %>
<%@ Register Src="~/WebUserControl/Search/UCSearchSimple.ascx" TagName="UCSearchSimple" TagPrefix="uc4" %>
<%@ Register Src="~/WebUserControl/UCSubSectionContainer.ascx" TagName="UCSubSectionContainer" TagPrefix="uc5" %>

<script type="text/javascript">
    function SelectClick() {
        PageMethods.SelectClick();
    }
</script>

<input id="hdnControlId" type="hidden" name="hdnControlId" runat="server"/>
<input id="hdnIndex" type="hidden" name="hdnIndex" runat="server"/>

<ul style="list-style-type: none; display: inline-block; margin: 0;padding:0;">
    <li>
        <asp:TextBox ID="txt" runat="server" CssClass="txtInput" ReadOnly="true" />
        <asp:ImageButton ID="imb" runat="server" ImageUrl="~/Images/Edit.gif" OnClick="imbLookup_Click" CausesValidation="false" />
        <asp:RequiredFieldValidator runat="server" ID="rfv" ControlToValidate="txt" Text="This field is required" ForeColor="Red" InitialValue="" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:PlaceHolder ID="plc" runat="server"></asp:PlaceHolder>
    </li> 
</ul>

<uc1:UcModalDialog2 ID="umd" runat="server">
    <ControlsContainer>
        <h1>LOOK UP - <%= LookupTitle %></h1> 
        <asp:UpdatePanel runat="server" ID="upL" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <uc4:UCSearchSimple ID="ucS" runat="server"/>
                <br />
                <asp:UpdatePanel runat="server" ID="upG" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:Panel runat="server" id="divGrid" Visible="false">
                            <asp:GridView runat="server" ID="gvL" AutoGenerateColumns="true" GridLines="None"
                                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" OnRowDataBound="gvList_RowDataBound"
                                HeaderStyle-HorizontalAlign="Center">
                            </asp:GridView>
                            <asp:HiddenField ID="hdnSort" runat="server" OnValueChanged="hdnSort_ValueChanged" />
                            <uc2:UCGridFooter ID="ucGF" runat="server" />
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ucS" /> 
                        <asp:AsyncPostBackTrigger ControlID="imb" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="hdnSort" EventName="ValueChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ControlsContainer>
</uc1:UcModalDialog2>
