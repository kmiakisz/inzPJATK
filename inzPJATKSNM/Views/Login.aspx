<%@ Page Title="" Language="C#" MasterPageFile="~/VoteMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="inzPJATKSNM.Views.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate">
    </asp:Login>
</asp:Content>
