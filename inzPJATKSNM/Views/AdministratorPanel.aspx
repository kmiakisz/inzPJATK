<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministratorPanel.aspx.cs" Inherits="inzPJATKSNM.Views.AdministratorPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="NewSurveyButton" runat="server" Text="Nowa ankieta" />
    <asp:Button ID="StatisticsButton" runat="server" Text="Statystyki" />
    <asp:Button ID="ActiveSurveyButton" runat="server" Text="Trwające ankiety" />
    <asp:Button ID="EditSurveyButton" runat="server" Text="Edytuj ankietę" />
    <ul class="thumbnails">
  <li class="span4">
    <div class="thumbnail">
      <img data-src="/Images/Icons/statistics-512.png" alt="">
      <h3>Thumbnail label</h3>
      <p>Thumbnail caption...</p>
    </div>
  </li>
</ul>
</asp:Content>
