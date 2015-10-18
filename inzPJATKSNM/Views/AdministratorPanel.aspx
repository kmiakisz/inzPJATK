<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministratorPanel.aspx.cs" Inherits="inzPJATKSNM.Views.AdministratorPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="ActiveSurveyButton" runat="server" Text="Trwające ankiety"/>
    <asp:Button ID="EditSurveyButton" runat="server" Text="Edytuj ankietę" />
    <asp:Button ID="AddAuthor" runat="server" Text="Nowy autor" OnClick="AddAuthor_Click" />
    <asp:Button ID="NewPhoto" runat="server" Text="Nowe Zdjecie" OnClick="NewPhoto_Click" />
    <asp:Button ID="AddNewSurveyButton" runat="server" Text="Nowa Ankieta" OnClick="AddNewSurveyButton_Click" />
 
</asp:Content>
