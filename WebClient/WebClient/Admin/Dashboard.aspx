<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WebClient.Admin.Dashboard" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Admin Dashboard – Queue System</title>
    <style>
        body { font-family: Arial; background: #F0F4F8; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        .box { background: white; padding: 40px; border-radius: 10px; width: 380px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); text-align: center; }
        h2 { color: #2C3E50; margin-bottom: 6px; }
        p  { color: #888; margin-bottom: 28px; }
        .btn { display: block; width: 100%; padding: 14px; margin-bottom: 12px; border: none; border-radius: 6px; font-size: 15px; cursor: pointer; color: white; text-decoration: none; box-sizing: border-box; }
        .btn-dark   { background: #2C3E50; }
        .btn-blue   { background: #2980B9; }
        .btn-green  { background: #27AE60; }
        .btn-orange { background: #E67E22; }
        .btn-red    { background: #E74C3C; }
        .btn:hover  { opacity: 0.9; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <h2>Admin Dashboard</h2>
            <p>Welcome, <asp:Label ID="LblName" runat="server"/>!</p>
            <a href="Branches.aspx"  class="btn btn-dark">🏢 Manage Branches</a>
            <a href="Services.aspx"  class="btn btn-blue">⚙️ Manage Services</a>
            <a href="Users.aspx"     class="btn btn-green">👥 Manage Users</a>
            <a href="Queue.aspx"     class="btn btn-orange">🎫 Call Queue</a>
            <asp:Button ID="BtnLogout" runat="server" Text="Logout"
                        CssClass="btn btn-red" OnClick="BtnLogout_Click"/>
        </div>
    </form>
</body>
</html>