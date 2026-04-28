<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebClient.Register" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Register – Queue System</title>
    <style>
        body { font-family: Arial; background: #F0F4F8; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        .box { background: white; padding: 40px; border-radius: 10px; width: 380px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
        h2 { text-align: center; color: #2C3E50; margin-bottom: 24px; }
        label { display: block; color: #555; margin-bottom: 4px; }
        input[type=text], input[type=password] { width: 100%; padding: 10px; margin-bottom: 14px; border: 1px solid #ccc; border-radius: 6px; box-sizing: border-box; font-size: 14px; }
        .btn { width: 100%; padding: 12px; background: #27AE60; color: white; border: none; border-radius: 6px; font-size: 15px; cursor: pointer; }
        .btn:hover { background: #1e8449; }
        .link { text-align: center; margin-top: 14px; }
        .link a { color: #2980B9; text-decoration: none; }
        .error { color: red; text-align: center; margin-bottom: 12px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <h2>Register</h2>
            <asp:Label ID="LblError" runat="server" CssClass="error" Visible="false"/>
            <label>First Name</label>
            <asp:TextBox ID="TxtFirstName" runat="server"/>
            <label>Last Name</label>
            <asp:TextBox ID="TxtLastName" runat="server"/>
            <label>Username</label>
            <asp:TextBox ID="TxtUsername" runat="server"/>
            <label>Password</label>
            <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password"/>
            <label>Phone</label>
            <asp:TextBox ID="TxtPhone" runat="server"/>
            <asp:Button ID="BtnRegister" runat="server" Text="Register" CssClass="btn" OnClick="BtnRegister_Click"/>
            <div class="link">
                <a href="Login.aspx">Already have an account? Login</a>
            </div>
        </div>
    </form>
</body>
</html>