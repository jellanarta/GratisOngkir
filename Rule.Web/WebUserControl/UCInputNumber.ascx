<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCInputNumber.ascx.cs"
    Inherits="Rule.Web.WebUserControl.UCInputNumber" %>
<input id="hdnInput" type="hidden" name="hdnInput" runat="server" />
<asp:TextBox ID="txtInput" runat="server" Text="0"></asp:TextBox>
<asp:TextBox ID="txtInputDummy" runat="server" BorderColor="#ffffff" ForeColor="#ffffff"
    Width="0px" Visible="true" Style="visibility: hidden"></asp:TextBox><asp:Label ID="ltlPercent"
        Visible="False" runat="server" Text="%"></asp:Label><font color="#ff0000"><asp:Literal
            ID="ltlMandatory" runat="server" Visible="False" Text="*)"></asp:Literal></font>
<asp:Literal runat="server" ID="ltlInteger" Visible="false" Text="" />
<asp:Literal runat="server" ID="ltlMedPercent" Visible="false" Text="" />
<asp:Literal runat="server" ID="ltlShortPercent" Visible="false" Text="" />
<asp:RequiredFieldValidator ID="rfvInput" runat="server" Display="Dynamic" ControlToValidate="txtInputDummy"
    ErrorMessage="This field is required" Enabled="False" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
<asp:RangeValidator ID="rgvInput" runat="server" Type="Double" Display="Dynamic"
    MaximumValue="999999999999999" MinimumValue="-999999999999999" ControlToValidate="txtInputDummy"
    ErrorMessage="*" ForeColor="Red"></asp:RangeValidator><asp:CompareValidator ID="cpvInput"
        runat="server" Display="Dynamic" ControlToValidate="txtInputDummy" ErrorMessage="*"
        Enabled="False" ForeColor="Red"></asp:CompareValidator>
<asp:CustomValidator runat="server" ID="cvInput" Enabled="false" ControlToValidate="txtInputDummy"
    Display="Dynamic"></asp:CustomValidator>
<script type="text/javascript">
    function validateKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode != 45 && charCode != 44 && charCode != 46))
            return false;

        return true;
    }
</script>
