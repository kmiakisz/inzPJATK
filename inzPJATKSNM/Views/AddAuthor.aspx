<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAuthor.aspx.cs" Inherits="inzPJATKSNM.Views.AddAuthor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="AuthorNameLabel" runat="server" Text="Imię" CssClass="label label-danger"></asp:Label>
    <br />
    <asp:TextBox ID="AuthorNameTextBox" runat="server"></asp:TextBox>
    <br />

    <asp:Label ID="AuthorSurnameLabel" runat="server" Text="Nazwisko" CssClass="label label-danger"></asp:Label>
    <br />

    <asp:TextBox ID="AuthorSurnameTextBox" runat="server"></asp:TextBox>
    <br />

    <asp:Label ID="NationalityLabel" runat="server" Text="Narodowość" CssClass="label label-danger"></asp:Label>
    <br />

    <asp:DropDownList ID="NationalityDropDownList" runat="server" DataSourceID="NarodowoscSqlDataSource1" DataTextField="Narodowosc" DataValueField="Id_Narod"></asp:DropDownList><asp:SqlDataSource ID="NarodowoscSqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Narod], [Narodowosc] FROM [Narodowosc]"></asp:SqlDataSource>
    <br />
    <asp:Label ID="sexLabel" runat="server" Text="Płeć" CssClass="label label-danger"></asp:Label>
    <br />
    <asp:DropDownList ID="PlecDropDownList" runat="server" DataSourceID="PlecSqlDataSource2" DataTextField="Plec" DataValueField="Id_Plec"></asp:DropDownList><asp:SqlDataSource ID="PlecSqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Plec], [Plec] FROM [Plec]"></asp:SqlDataSource>
    <br />
    <asp:Label ID="EpokaDropDownList" runat="server" Text="Epoka" CssClass="label label-danger"></asp:Label>
    <br />
    <asp:DropDownList ID="EpokaDropDownList1" runat="server" DataSourceID="EpokaSqlDataSource1" DataTextField="Epoka" DataValueField="Id_Epoki"></asp:DropDownList><asp:SqlDataSource ID="EpokaSqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Epoki], [Epoka] FROM [Epoka]"></asp:SqlDataSource>
    <br />
    <asp:Button ID="DodajButton" runat="server" Text="Zapisz" OnClick="DodajButton_Click" class="btn btn-danger"/>
</asp:Content>
