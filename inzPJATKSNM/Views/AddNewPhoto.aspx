<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewPhoto.aspx.cs" Inherits="inzPJATKSNM.Views.AddNewPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3><span class="label label-danger">Dodaj zdjęcie</span></h3>

    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="UploadButton" runat="server" Text="Zapisz" class="btn btn-danger" OnClick="UploadButton_Click" />
    <asp:Label ID="StatusLabel" runat="server" Text="" CssClass="label label-danger"></asp:Label>
</asp:Content>
