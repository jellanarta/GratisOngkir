<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCDatePicker.ascx.cs" Inherits="Rule.Web.WebUserControl.UCDatePicker" %>
<script type="text/javascript">
    function copy(from, to, rvid) {
        var value = document.getElementById(from).value;

        if (value != '') {
            var split = value.split('/');
            $('#' + to).val(split[1] + '/' + split[0] + '/' + split[2]);
            $('#' + from).val(split[0] + '/' + split[1] + '/' + split[2]);
        }
        else {
            $('#' + to).val(value);
        }
        var rv = document.getElementById(rvid);
        ValidatorValidate(rv);
    }
</script>
<asp:TextBox runat="server" ID="txtDatePicker" CssClass="datePicker" ></asp:TextBox>
<asp:TextBox runat="server" ID="txtHidden" style="display:none;"></asp:TextBox>
<asp:RangeValidator ID="rvDate" runat="server" ErrorMessage="Invalid Date" ControlToValidate="txtHidden" Type="Date" Display="Dynamic" ForeColor="Red"></asp:RangeValidator>
<asp:RequiredFieldValidator id="rfvDatePicker" runat="server" ControlToValidate="txtDatePicker" Enabled="false" ErrorMessage="This field is required" Display="Dynamic" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
