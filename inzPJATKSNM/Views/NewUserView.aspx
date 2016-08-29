<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewUserView.aspx.cs" Inherits="inzPJATKSNM.Views.NewUserView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">Dane nowego użytkownika</h3>
            </div>
            <div class="panel-body">
                <div id="login">
                    <asp:Label ID="LoginLbl" runat="server" Text="Login" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="LoginTxt" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div id="pwd">
                    <asp:Label ID="HasłoLbl" runat="server" Text="Hasło" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="PwdTxt" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div id="name">
                    <asp:Label ID="ImięLbl" runat="server" Text="Imię" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="NameTxt" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div id="surname">
                    <asp:Label ID="NazwiskoLbl" runat="server" Text="Nazwisko" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="SurnameTxt" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div id="role">
                    <asp:Label ID="RolaLbl" runat="server" Text="Rola" CssClass="label label-danger"></asp:Label>
                    <asp:DropDownList ID="RoleDDL" runat="server" CssClass="form-control" DataSourceID="RoleDataSource" DataTextField="ROLE_NAME" DataValueField="ID_ROLE"></asp:DropDownList>
                    <asp:SqlDataSource ID="RoleDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [ID_ROLE], [ROLE_NAME] FROM [SNM_ROLE]"></asp:SqlDataSource>
                </div>
            </div>
        </div>
        <div id="acceptbutton">
            <asp:Button ID="AcceptButton" runat="server" Text="Dodaj" CssClass="btn btn-success" />
        </div>
    </div>

</asp:Content>
