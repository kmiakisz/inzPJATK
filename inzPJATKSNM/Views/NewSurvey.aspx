<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSurvey.aspx.cs" Inherits="inzPJATKSNM.Views.NewSurvey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3><span class="label label-danger">Nowa ankieta</span></h3>

    <asp:Label ID="SurveyNameLabel" runat="server" Text="Nazwa Ankiety" class="label label-danger"></asp:Label>
    <asp:TextBox ID="SurveyNameTextBox" runat="server" class="form-control"></asp:TextBox>

    <asp:Label ID="ServeyDescribtionLabel" runat="server" Text="Opis Ankiety" class="label label-danger"></asp:Label>
    <asp:TextBox ID="ServeyDescribtionTextBox" runat="server" class="form-control"></asp:TextBox>


</asp:Content>
