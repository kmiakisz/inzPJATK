<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="inzPJATKSNM.Views.ManageUsers" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div id="left" style="float: left; width: 80%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="Label1" runat="server" Text="Zarządzanie Użytkownikami" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" DataKeyNames="ID_USER" DataSourceID="UsersDataSource" 
                        CssClass="table table-hover table-striped" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" meta:resourcekey="GridView1Resource1" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" meta:resourcekey="CommandFieldResource1" />
                            <asp:BoundField DataField="ID_USER" HeaderText="ID_USER" InsertVisible="False" ReadOnly="True" SortExpression="ID_USER" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="EMAIL_LOGIN" HeaderText="EMAIL_LOGIN" SortExpression="EMAIL_LOGIN" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="ID_ROLE" HeaderText="ID_ROLE" SortExpression="ID_ROLE" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME" meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField DataField="SURNAME" HeaderText="SURNAME" SortExpression="SURNAME" meta:resourcekey="BoundFieldResource5" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:inzSNMConnectionString %>' DeleteCommand="DELETE FROM [SNM_USER] WHERE [ID_USER] = @ID_USER" InsertCommand="INSERT INTO [SNM_USER] ([EMAIL_LOGIN], [NAME], [SURNAME]) VALUES (@EMAIL_LOGIN, @NAME, @SURNAME)" SelectCommand="SELECT [ID_USER], [EMAIL_LOGIN], [NAME], [SURNAME] FROM [SNM_USER] WHERE ([ID_USER] <> @ID_USER)" UpdateCommand="UPDATE [SNM_USER] SET [EMAIL_LOGIN] = @EMAIL_LOGIN, [NAME] = @NAME, [SURNAME] = @SURNAME WHERE [ID_USER] = @ID_USER">
                        <DeleteParameters>
                            <asp:Parameter Name="ID_USER" Type="Int32"></asp:Parameter>
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="EMAIL_LOGIN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="NAME" Type="String"></asp:Parameter>
                            <asp:Parameter Name="SURNAME" Type="String"></asp:Parameter>
                        </InsertParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="HiddenField1" PropertyName="Value" Name="ID_USER" Type="Int32"></asp:ControlParameter>
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="EMAIL_LOGIN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="NAME" Type="String"></asp:Parameter>
                            <asp:Parameter Name="SURNAME" Type="String"></asp:Parameter>
                            <asp:Parameter Name="ID_USER" Type="Int32"></asp:Parameter>
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="UsersDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" DeleteCommand="DELETE FROM [SNM_USER] WHERE [ID_USER] = @ID_USER" InsertCommand="INSERT INTO [SNM_USER] ([EMAIL_LOGIN], [ID_ROLE], [NAME], [SURNAME]) VALUES (@EMAIL_LOGIN, @ID_ROLE, @NAME, @SURNAME)" SelectCommand="SELECT [ID_USER], [EMAIL_LOGIN], [ID_ROLE], [NAME], [SURNAME] FROM [SNM_USER]" UpdateCommand="UPDATE [SNM_USER] SET [EMAIL_LOGIN] = @EMAIL_LOGIN, [ID_ROLE] = @ID_ROLE, [NAME] = @NAME, [SURNAME] = @SURNAME WHERE [ID_USER] = @ID_USER">
                        <DeleteParameters>
                            <asp:Parameter Name="ID_USER" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="EMAIL_LOGIN" Type="String" />
                            <asp:Parameter Name="ID_ROLE" Type="Int32" />
                            <asp:Parameter Name="NAME" Type="String" />
                            <asp:Parameter Name="SURNAME" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="EMAIL_LOGIN" Type="String" />
                            <asp:Parameter Name="ID_ROLE" Type="Int32" />
                            <asp:Parameter Name="NAME" Type="String" />
                            <asp:Parameter Name="SURNAME" Type="String" />
                            <asp:Parameter Name="ID_USER" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </div>
            </div>
            <div id="buttons">
                <asp:Button ID="BckBtn" runat="server" Text="Powrót" CssClass="btn btn-success" OnClick="BckBtn_Click" meta:resourcekey="BckBtnResource1" />
            </div>
        </div>
        <div class="demo" style="float: right; width: 10%">
            <asp:Button ID="NewUserBtn" runat="server" Text="Dodaj nowego użytkownika" CssClass="btn btn-danger" Width="200px" OnClick="NewUserBtn_Click" meta:resourcekey="NewUserBtnResource1"/>
            <br />
            <br />
            <!--<asp:Button ID="Button2" runat="server" Text="Zarządzanie uprawnieniami" CssClass="btn btn-danger" Width="200px"/>-->
        </div>
    </div>
</asp:Content>
