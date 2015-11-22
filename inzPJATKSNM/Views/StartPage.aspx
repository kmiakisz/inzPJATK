<%@ Page Title="" Language="C#" MasterPageFile="~/VoteMaster.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="inzPJATKSNM.Views.StartPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Content/modal.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script> 
    <script src="../Scripts/modal.js"></script>
    <div id="boxes">
        <div id="dialog" class="window">
            <asp:Label ID="nationalityLabel" runat="server" Text="Narodowosc" CssClass="label label-danger"></asp:Label>
            <asp:DropDownList ID="nationalityDDL" runat="server" CssClass="form-control" DataSourceID="NarodowoscSQL" DataValueField="Id_Narod" DataTextField="Narodowosc"></asp:DropDownList>
            <asp:SqlDataSource ID="NarodowoscSQL" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Narodowosc] , [Id_Narod] FROM [Narodowosc]"></asp:SqlDataSource>
            
            <asp:Label ID="sexLabel" runat="server" Text="Plec" CssClass="label label-danger"></asp:Label>   
            <asp:DropDownList ID="sexDDL" runat="server" CssClass="form-control" DataSourceID="sexSQL" DataTextField="Plec" DataValueField="Id_Plec"></asp:DropDownList>
            <asp:SqlDataSource ID="sexSQL" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Plec] , [Id_Plec] FROM [Plec]"></asp:SqlDataSource>
            
            <asp:Label ID="ageLabel" runat="server" Text="Przedzial Wiekowy" CssClass="label label-danger"></asp:Label>
            <asp:DropDownList ID="ageDDL" runat="server" CssClass="form-control" DataSourceID="ageSQL" DataTextField="Wiek" DataValueField="Id_Wiek"></asp:DropDownList>
            <asp:SqlDataSource ID="ageSQL" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Wiek] , [Id_Wiek] FROM [Wiek]"></asp:SqlDataSource>
            <div id="popupfoot"><a href="#" class="close agree">Anuluj</a>  <a class="agree" style="color: red;" href="#">Dodaj</a> </div>
        </div>
        <div id="mask"></div>
    </div>
</asp:Content>
