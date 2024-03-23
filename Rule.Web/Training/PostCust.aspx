<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostCust.aspx.cs" Inherits="Training.PostCust" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="postcust" Text="Send A Cust" OnClick="Send_Cust" runat="server" />
        </div>
    </form>
</body>
</html>
