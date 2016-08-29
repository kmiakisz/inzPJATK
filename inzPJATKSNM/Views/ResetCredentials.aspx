<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetCredentials.aspx.cs" Inherits="inzPJATKSNM.Views.ResetCredentials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">Reset Hasła</h3>
            </div>
            <div class="panel-body">
                <div id="pwd">
                    <asp:Label ID="PwdLabel" runat="server" Text="Nowe hasło." CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="PwdTxt" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div id="retypePwd">
                    <asp:Label ID="RePwdLabel" runat="server" Text="Powtórz nowe hasło." CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="RePwdTxt" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div id="loginbutton">
            <asp:Button ID="Button1" runat="server" Text="Zapisz nowe hasło." CssClass="btn btn-success" />
        </div>
    </div>
</asp:Content>
