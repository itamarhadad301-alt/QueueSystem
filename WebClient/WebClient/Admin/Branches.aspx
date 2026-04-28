<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Branches.aspx.cs" Inherits="WebClient.Admin.Branches" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Manage Branches</title>
    <style>
        body { font-family: Arial; background: #F0F4F8; margin: 0; padding: 30px; }
        .container { max-width: 800px; margin: auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
        h2 { color: #2C3E50; margin-bottom: 20px; }
        input[type=text] { padding: 8px; border: 1px solid #ccc; border-radius: 6px; font-size: 14px; margin-right: 8px; width: 200px; }
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
        .form-row { display: flex; gap: 10px; align-items: center; flex-wrap: wrap; margin-bottom: 20px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>🏢 Manage Branches</h2>
            <asp:Label ID="LblError" runat="server" CssClass="error" Visible="false"/>

            <div class="form-row">
                <asp:TextBox ID="TxtBranchName" runat="server" placeholder="Branch Name"/>
<asp:TextBox ID="TxtLocation" runat="server" placeholder="Address"/>
                <asp:Button  ID="BtnAdd"        runat="server" Text="Add" CssClass="btn btn-green" OnClick="BtnAdd_Click"/>
                <asp:Button  ID="BtnUpdate"     runat="server" Text="Update" CssClass="btn btn-blue" OnClick="BtnUpdate_Click" Visible="false"/>
                <asp:Button  ID="BtnClear"      runat="server" Text="Clear" CssClass="btn btn-grey" OnClick="BtnClear_Click"/>
            </div>

            <asp:GridView ID="GvBranches" runat="server"
                          AutoGenerateColumns="false"
                          OnRowCommand="GvBranches_RowCommand">
                <Columns>
                    <asp:BoundField DataField="BranchId"   HeaderText="ID"/>
<asp:BoundField DataField="BranchName" HeaderText="Branch Name"/>
<asp:BoundField DataField="Address"    HeaderText="Address"/>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="Edit"   CommandName="EditRow"   CommandArgument='<%# Eval("BranchId") %>' CssClass="btn btn-blue"/>
                            <asp:Button runat="server" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("BranchId") %>' CssClass="btn btn-red"
                                        OnClientClick="return confirm('Delete this branch?');"/>
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