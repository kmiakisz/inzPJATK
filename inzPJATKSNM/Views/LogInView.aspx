<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogInView.aspx.cs" Inherits="inzPJATKSNM.Views.LogInView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">Logowanie</h3>
            </div>
            <div class="panel-body">
                <div id="login">
                    <asp:Label ID="LoginLbl" runat="server" Text="Email" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="LoginTxt" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div id="pwd">
                    <asp:Label ID="PwdLbl" runat="server" Text="Hasło" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>
            </div>
        </div>
        <div id="loginbutton">
            <asp:Button ID="LogInButton" runat="server" Text="Zaloguj."  CssClass="btn btn-success"/>
            <asp:Button ID="ResetPwdButton" runat="server" Text="Nie pamiętam hasła." CssClass="btn btn-danger" />
        </div>
    </div>
</asp:Content>
