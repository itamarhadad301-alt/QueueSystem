<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebClient.Home" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Home – Queue System</title>
    <style>
        body { font-family: Arial; background: #F0F4F8; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        .box { background: white; padding: 40px; border-radius: 10px; width: 360px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); text-align: center; }
        h2 { color: #2C3E50; margin-bottom: 30px; }
        .btn { display: block; width: 100%; padding: 14px; margin-bottom: 14px; border: none; border-radius: 6px; font-size: 15px; cursor: pointer; color: white; text-decoration: none; }
        .btn-blue  { background: #2980B9; }
        .btn-green { background: #27AE60; }
        .btn-red   { background: #E74C3C; }
        .btn:hover { opacity: 0.9; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <h2>Welcome, <asp:Label ID="LblName" runat="server"/>!</h2>
            <a href="TakeTicket.aspx" class="btn btn-blue">🎫 Take a Ticket</a>
            <a href="MyTicket.aspx" class="btn btn-green">📋 My Ticket</a>
            <asp:Button ID="BtnLogout" runat="server" Text="Logout"
                        CssClass="btn btn-red" OnClick="BtnLogout_Click"/>
        </div>
    </form>
</body>
</html>