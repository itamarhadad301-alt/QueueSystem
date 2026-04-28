<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyTicket.aspx.cs" Inherits="WebClient.MyTicket" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>My Ticket – Queue System</title>
    <style>
        body { font-family: Arial; background: #F0F4F8; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        .box { background: white; padding: 40px; border-radius: 10px; width: 380px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); text-align: center; }
        h2 { color: #2C3E50; margin-bottom: 24px; }
        .ticket-number { font-size: 72px; font-weight: bold; color: #2980B9; }
        .status { font-size: 18px; color: #555; margin: 8px 0; }
        .position { font-size: 18px; color: #27AE60; margin-bottom: 24px; }
        .btn { display: block; width: 100%; padding: 12px; margin-bottom: 10px; border: none; border-radius: 6px; font-size: 15px; cursor: pointer; color: white; text-decoration: none; box-sizing: border-box; }
        .btn-blue   { background: #2980B9; }
        .btn-red    { background: #E74C3C; }
        .btn-grey   { background: #95A5A6; }
        .btn:hover  { opacity: 0.9; }
        .no-ticket  { font-size: 18px; color: #999; margin: 24px 0; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <h2>📋 My Ticket</h2>

            <asp:Panel ID="PnlTicket" runat="server" Visible="false">
                <div class="ticket-number">
                    <asp:Label ID="LblTicketNumber" runat="server"/>
                </div>
                <div class="status">
                    <asp:Label ID="LblStatus" runat="server"/>
                </div>
                <div class="position">
                    <asp:Label ID="LblPosition" runat="server"/>
                </div>
                <asp:Button ID="BtnRefresh" runat="server" Text="🔄 Refresh"
                            CssClass="btn btn-blue" OnClick="BtnRefresh_Click"/>
                <asp:Button ID="BtnCancel" runat="server" Text="❌ Cancel Ticket"
                            CssClass="btn btn-red" OnClick="BtnCancel_Click"
                            OnClientClick="return confirm('Are you sure you want to cancel your ticket?');"/>
            </asp:Panel>

            <asp:Panel ID="PnlNoTicket" runat="server" Visible="false">
                <div class="no-ticket">You have no active ticket</div>
            </asp:Panel>

            <a href="Home.aspx" class="btn btn-grey">Back to Home</a>
        </div>
    </form>
</body>
</html>