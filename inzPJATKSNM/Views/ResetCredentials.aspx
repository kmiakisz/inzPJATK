<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetCredentials.aspx.cs" Inherits="inzPJATKSNM.Views.ResetCredentials" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <asp:Label ID="Label1" runat="server" Text="Reset Hasła" meta:resourcekey="Label1Resource1"></asp:Label></h3>
            </div>
            <div class="panel-body">
                <div id="pwd">
                    <asp:Label ID="PwdLabel" runat="server" Text="Nowe hasło." CssClass="label label-danger" meta:resourcekey="PwdLabelResource1"></asp:Label>
                    <asp:TextBox ID="PwdTxt" runat="server" CssClass="form-control" TextMode="Password" meta:resourcekey="PwdTxtResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="PwdTxt" runat="server" ErrorMessage="Pole : Nowe hasło, jest wymagane!" ForeColor="Red" Font-Bold="True" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
                <div id="retypePwd">
                    <asp:Label ID="RePwdLabel" runat="server" Text="Powtórz nowe hasło." CssClass="label label-danger" meta:resourcekey="RePwdLabelResource1"></asp:Label>
                    <asp:TextBox ID="RePwdTxt" runat="server" CssClass="form-control" TextMode="Password" meta:resourcekey="RePwdTxtResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="RePwdTxt" runat="server" ErrorMessage="Pole : Powtórz nowe hasło, jest wymagane!" ForeColor="Red" Font-Bold="True" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Wprowadzone hasła są niezgodne." ControlToCompare="PwdTxt" ControlToValidate="RePwdTxt" ValidationGroup="A" ForeColor="Red" Font-Bold="True" Display="Dynamic" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                </div>
            </div>
        </div>
        <div id="loginbutton">
            <asp:Button ID="Button1" runat="server" Text="Zapisz nowe hasło." CssClass="btn btn-success" OnClick="Button1_Click" ValidationGroup="A" meta:resourcekey="Button1Resource1" />
        </div>
    </div>
</asp:Content>
