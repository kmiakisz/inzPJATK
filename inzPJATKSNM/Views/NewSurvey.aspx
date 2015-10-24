<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSurvey.aspx.cs" Inherits="inzPJATKSNM.Views.NewSurvey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div id="content" class="container-fluid">
       <h3><span class="label label-danger">Nowa ankieta</span></h3>
       <div id="left" style="float:left ; width:30%">
            <asp:Label ID="SurveyNameLabel" runat="server" Text="Nazwa Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="SurveyNameTextBox" runat="server" class="form-control"></asp:TextBox>
            <asp:Label ID="ServeyDescribtionLabel" runat="server" Text="Opis Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="ServeyDescribtionTextBox" runat="server" class="form-control"></asp:TextBox>
            <asp:Label ID="MusicLabel" runat="server" Text="Wybierz muzykę: " class="label label-danger"></asp:Label>
            <asp:DropDownList ID="MusicDropDownList" runat="server" class="form-control" style="width:80%" DataSourceID="MusicDataSource" DataTextField="Tytul" DataValueField="Id_Muzyka">
                <asp:ListItem Selected="True" Value="0" Enabled="true" Text="--Wybierz utwór --"></asp:ListItem>
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="MusicDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Muzyka], [Tytul] FROM [Muzyka]"></asp:SqlDataSource>
            <br /><br />
            <div id="Buttons" style="float:left ; width:50%">
                <asp:Button ID="AcceptButton" runat="server" Text="Dodaj" class="btn btn-danger" style="float:left"/>
                <asp:Button ID="CancelButton" runat="server" Text="Anuluj" class="btn btn-danger" style="float:right"/>
           </div>
         
       </div>
       <div id="right" style="float:right ; width:70% ; border: 1px solid red">
           <asp:Button ID="Button1" runat="server" Text="Button" />
       </div>
   </div>
</asp:Content>
