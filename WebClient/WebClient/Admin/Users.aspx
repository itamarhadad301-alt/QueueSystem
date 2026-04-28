<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="WebClient.Admin.Users" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Manage Users</title>
    <style>
        body { font-family: Arial; background: #F0F4F8; margin: 0; padding: 30px; }
        .container { max-width: 900px; margin: auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
        h2 { color: #2C3E50; margin-bottom: 20px; }
        input[type=text], input[type=password], select { padding: 8px; border: 1px solid #ccc; border-radius: 6px; font-size: 14px; margin-right: 6px; width: 140px; }
        .btn { padding: 9px 18px; border: none; border-radius: 6px; font-size: 14px; cursor: pointer; color: white; margin-right: 6px; }
        .btn-blue  { background: #2980B9; }
        .btn-green { background: #27AE60; }
        .btn-red   { background: #E74C3C; }
        .btn-grey  { background: #95A5A6; }
        .btn:hover { opacity: 0.9; }
        table { width: 100%; border-collapse: collapse; margin-top: 20px; }
        th { background: #2C3E50; color: white; padding: 10px; text-align: left; }
        td { padding: 10px; border-bottom: 1px solid #eee; }
        tr:hover { background: #f9f9f9; }
        .error { color: red; margin-bottom: 10px; }
        .form-row { display: flex; gap: 8px; align-items: center; flex-wrap: wrap; margin-bottom: 20px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>👥 Manage Users</h2>
            <asp:Label ID="LblError" runat="server" CssClass="error" Visible="false"/>

            <div class="form-row">
                <asp:TextBox ID="TxtFirstName" runat="server" placeholder="First Name"/>
                <asp:TextBox ID="TxtLastName"  runat="server" placeholder="Last Name"/>
                <asp:TextBox ID="TxtUsername"  runat="server" placeholder="Username"/>
                <asp:TextBox ID="TxtPassword"  runat="server" placeholder="Password"/>
                <asp:TextBox ID="TxtPhone"     runat="server" placeholder="Phone"/>
                <asp:DropDownList ID="DdlRole" runat="server">
                    <asp:ListItem Text="User"  Value="User"/>
                    <asp:ListItem Text="Admin" Value="Admin"/>
                </asp:DropDownList>
                <asp:Button ID="BtnAdd"    runat="server" Text="Add"    CssClass="btn btn-green" OnClick="BtnAdd_Click"/>
                <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="btn btn-blue"  OnClick="BtnUpdate_Click" Visible="false"/>
                <asp:Button ID="BtnClear"  runat="server" Text="Clear"  CssClass="btn btn-grey"  OnClick="BtnClear_Click"/>
            </div>

            <asp:GridView ID="GvUsers" runat="server"
                          AutoGenerateColumns="false"
                          OnRowCommand="GvUsers_RowCommand">
                <Columns>
                    <asp:BoundField DataField="UserId"    HeaderText="ID"/>
                    <asp:BoundField DataField="FirstName" HeaderText="First Name"/>
                    <asp:BoundField DataField="LastName"  HeaderText="Last Name"/>
                    <asp:BoundField DataField="Username"  HeaderText="Username"/>
                    <asp:BoundField DataField="Phone"     HeaderText="Phone"/>
                    <asp:BoundField DataField="Role"      HeaderText="Role"/>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="Edit"   CommandName="EditRow"   CommandArgument='<%# Eval("UserId") %>' CssClass="btn btn-blue"/>
                            <asp:Button runat="server" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("UserId") %>' CssClass="btn btn-red"
                                        OnClientClick="return confirm('Delete this user?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <br/>
            <a href="Dashboard.aspx" class="btn btn-grey" style="text-decoration:none; display:inline-block;">← Back</a>
        </div>
    </form>
</body>
</html>