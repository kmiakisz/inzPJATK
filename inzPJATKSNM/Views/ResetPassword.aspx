<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="inzPJATKSNM.Views.ResetPassword" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <asp:Label ID="Label1" runat="server" Text="Resetowanie Hasła" meta:resourcekey="Label1Resource1"></asp:Label></h3>
            </div>
            <div class="panel-body">
                <div id="login">
                    <asp:Label ID="Login1Lbl" runat="server" Text="Email" CssClass="label label-danger" meta:resourcekey="Login1LblResource1"></asp:Label>
                    <asp:TextBox ID="Login1Txt" runat="server" CssClass="form-control" meta:resourcekey="Login1TxtResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Login1Txt" runat="server" ErrorMessage="Pole email jest wymagane!" ForeColor="Red" Font-Bold="True" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
                <div id="token">
                    <asp:Label ID="TokenLbl" runat="server" Text="Token" CssClass="label label-danger" meta:resourcekey="TokenLblResource1"></asp:Label>
                    <asp:TextBox ID="TokenTxt" runat="server" CssClass="form-control" TextMode="Password" meta:resourcekey="TokenTxtResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TokenTxt" runat="server" ErrorMessage="Pole token jest wymagane!" ForeColor="Red" Font-Bold="True" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div id="changesbutton">
            <asp:Button ID="ChangesButton" runat="server" Text="Wyślij." CssClass="btn btn-success" OnClick="ChangesButton_Click" ValidationGroup="A" meta:resourcekey="ChangesButtonResource1" />
        </div>
    </div>
</asp:Content>
