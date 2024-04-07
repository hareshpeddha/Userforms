<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registeruser.aspx.cs" Inherits="userforms.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Name:</label>
            <asp:TextBox ID="name" runat="server"></asp:TextBox><br />

            <label>Username:</label>
            <asp:TextBox ID="usrname" runat="server"></asp:TextBox><br />

            <label>Password:</label>
            <asp:TextBox ID="pwd" runat="server" TextMode="Password"></asp:TextBox><br />

            <label>Confirm Password:</label>
            <asp:TextBox ID="confirmpwd" runat="server" TextMode="Password"></asp:TextBox><br />

            <asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click" />
            <asp:Button ID="clear" runat="server" Text="Clear" OnClick="clear_Click" />
            <asp:Button ID="CONVERT" runat="server" Text="CONVERT" OnClick="CONVERT_Click" />
        </div>
    </form>
</body>
</html>
