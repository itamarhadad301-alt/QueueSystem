<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TakeTicket.aspx.cs" Inherits="WebClient.TakeTicket" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Take a Ticket – Queue System</title>
    <style>
        body { font-family: Arial; background: #F0F4F8; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        .box { background: white; padding: 40px; border-radius: 10px; width: 380px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
        h2 { text-align: center; color: #2C3E50; margin-bottom: 24px; }
        label { display: block; color: #555; margin-bottom: 4px; }
        select { width: 100%; padding: 10px; margin-bottom: 16px; border: 1px solid #ccc; border-radius: 6px; font-size: 14px; }
        .btn { width: 100%; padding: 12px; background: #2980B9; color: white; border: none; border-radius: 6px; font-size: 15px; cursor: pointer; margin-bottom: 10px; }
        .btn-back { background: #95A5A6; }
        .btn:hover { opacity: 0.9; }
        .error { color: red; text-align: center; margin-bottom: 12px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <h2>🎫 Take a Ticket</h2>
            <asp:Label ID="LblError" runat="server" CssClass="error" Visible="false"/>

            <label>Select Branch</label>
            <asp:DropDownList ID="DdlBranch" runat="server"
                              AutoPostBack="true"
                              OnSelectedIndexChanged="DdlBranch_SelectedIndexChanged"/>

            <label>Select Service</label>
            <asp:DropDownList ID="DdlService" runat="server"/>

            <asp:Button ID="BtnTake" runat="server" Text="🎫 Take Ticket"
                        CssClass="btn" OnClick="BtnTake_Click"/>
            <a href="Home.aspx" class="btn btn-back" style="display:block; text-align:center; text-decoration:none; padding:12px; border-radius:6px; color:white;">Back</a>
        </div>
    </form>
</body>
</html>