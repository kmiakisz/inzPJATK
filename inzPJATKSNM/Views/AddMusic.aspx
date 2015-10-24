<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMusic.aspx.cs" Inherits="inzPJATKSNM.Views.AddMusic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FileUpload ID="MusicFileUpload" runat="server" />
    <br />
    <asp:Button ID="ZapiszButton" runat="server" Text="Zapisz" class="btn btn-danger" OnClick="ZapiszButton_Click"/>
    <asp:Button ID="AnulujButton" runat="server" Text="Anuluj" class="btn btn-danger" OnClick="AnulujButton_Click"/>
</asp:Content>
