<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="inzPJATKSNM.Views.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">Resetowanie Hasła</h3>
            </div>
            <div class="panel-body">
                <div id="login">
                    <asp:Label ID="Login1Lbl" runat="server" Text="Email" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="Login1Txt" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div id="token">
                    <asp:Label ID="TokenLbl" runat="server" Text="Token" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="TokenTxt" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>
            </div>
        </div>
        <div id="changesbutton">
            <asp:Button ID="ChangesButton" runat="server" Text="Wyślij." CssClass="btn btn-success" />
        </div>
    </div>
</asp:Content>
