<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Queue.aspx.cs" Inherits="WebClient.Admin.Queue" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Call Queue</title>
    <style>
        body { font-family: Arial; background: #F0F4F8; margin: 0; padding: 30px; }
        .container { max-width: 900px; margin: auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
        h2 { color: #2C3E50; margin-bottom: 20px; }
        select { padding: 8px; border: 1px solid #ccc; border-radius: 6px; font-size: 14px; margin-right: 8px; width: 200px; }
        .btn { padding: 9px 18px; border: none; border-radius: 6px; font-size: 14px; cursor: pointer; color: white; margin-right: 6px; }
        .btn-blue   { background: #2980B9; }
        .btn-green  { background: #27AE60; }
        .btn-orange { background: #E67E22; }
        .btn-red    { background: #E74C3C; }
        .btn-grey   { background: #95A5A6; }
        .btn:hover  { opacity: 0.9; }
        table { width: 100%; border-collapse: collapse; margin-top: 20px; }
        th { background: #2C3E50; color: white; padding: 10px; text-align: left; }
        td { padding: 10px; border-bottom: 1px solid #eee; }
        tr:hover { background: #f9f9f9; }
        .filter-row { display: flex; gap: 10px; align-items: center; flex-wrap: wrap; margin-bottom: 20px; }
        .actions { margin-bottom: 20px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>🎫 Call Queue</h2>

            <div class="filter-row">
                <asp:DropDownList ID="DdlBranch" runat="server" AutoPostBack="true"
                                  OnSelectedIndexChanged="DdlBranch_SelectedIndexChanged"/>
                <asp:DropDownList ID="DdlService" runat="server" AutoPostBack="true"
                                  OnSelectedIndexChanged="DdlService_SelectedIndexChanged"/>
                <asp:Button ID="BtnRefresh" runat="server" Text="🔄 Refresh"
                            CssClass="btn btn-blue" OnClick="BtnRefresh_Click"/>
            </div>

            <div class="actions">
                <asp:Button ID="BtnCallNext" runat="server" Text="📢 Call Next"
                            CssClass="btn btn-green" OnClick="BtnCallNext_Click"/>
                <asp:Button ID="BtnSkip"     runat="server" Text="⏭ Skip"
                            CssClass="btn btn-orange" OnClick="BtnSkip_Click"/>
                <asp:Button ID="BtnClose"    runat="server" Text="✅ Close"
                            CssClass="btn btn-red" OnClick="BtnClose_Click"/>
            </div>

            <asp:Label ID="LblMsg" runat="server" ForeColor="Green" Visible="false"/>

            <asp:GridView ID="GvQueue" runat="server"
                          AutoGenerateColumns="false"
                          OnSelectedIndexChanged="GvQueue_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="Select" HeaderText=""/>
                    <asp:BoundField DataField="TicketId"     HeaderText="ID"/>
                    <asp:BoundField DataField="TicketNumber" HeaderText="Ticket #"/>
                    <asp:BoundField DataField="Status"       HeaderText="Status"/>
                    <asp:BoundField DataField="UserId"       HeaderText="User ID"/>
                </Columns>
            </asp:GridView>

            <br/>
            <a href="Dashboard.aspx" class="btn btn-grey" style="text-decoration:none; display:inline-block;">← Back</a>
        </div>
    </form>
</body>
</html>