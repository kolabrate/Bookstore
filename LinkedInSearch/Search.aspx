<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="LinkedInSearch.Search" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="TxtAuthCode" runat="server" TextMode="MultiLine" Height="164px" Width="891px" ReadOnly="true"></asp:TextBox>
        <br />
    <asp:Button ID="GetAccessToken" Text="Search People" runat="server" OnClick="GetAccessToken_Click" />
        <br />
    <asp:TextBox ID="TxtAccessToken" runat="server" TextMode="MultiLine" Height="164px" Width="891px" ReadOnly="true"></asp:TextBox>
        <br />
   
    </div>
    </form>
</body>
</html>
