<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAuthor.aspx.cs" Inherits="inzPJATKSNM.Views.AddAuthor" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/jquery-3.1.0.js"></script>
        <div id="autorDiv">
            <div id="NameControls">
                <asp:Label ID="AuthorNameLabel" runat="server" Text="Imię" CssClass="label label-danger"></asp:Label>
                <div id="NameControl">
                    <asp:TextBox ID="AuthorNameTextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pole Imię jest wymagane!" Display="Dynamic" ControlToValidate="AuthorNameTextBox" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Pole Imię zawiera nieprawidłowe znaki!" Display="Dynamic" ControlToValidate="AuthorNameTextBox" ValidationExpression="^[a-zA-Z]+" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div id="SurnameControls">
                <asp:Label ID="AuthorSurnameLabel" runat="server" Text="Nazwisko" CssClass="label label-danger"></asp:Label>
                <div id="SurnameControl">
                    <asp:TextBox ID="AuthorSurnameTextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Pole Nazwisko jest wymagane!" Display="Dynamic" ControlToValidate="AuthorSurnameTextBox" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Pole Nazwisko zawiera nieprawidłowe znaki!" Display="Dynamic" ControlToValidate="AuthorSurnameTextBox" ValidationExpression="^[a-zA-Z]+" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div id="NationalityControls">
                <asp:Label ID="NationalityLabel" runat="server" Text="Narodowość" CssClass="label label-danger"></asp:Label>
                <div id="NationalityDDL">
                    <asp:DropDownList ID="NationalityDropDownList" runat="server" DataSourceID="NarodowoscSqlDataSource1" DataTextField="Narodowosc" DataValueField="Id_Narod"></asp:DropDownList><asp:SqlDataSource ID="NarodowoscSqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Narod], [Narodowosc] FROM [Narodowosc]"></asp:SqlDataSource>
                </div>
            </div>
            <div id="SexControls">
                <asp:Label ID="sexLabel" runat="server" Text="Płeć" CssClass="label label-danger"></asp:Label>
                <div id="SexDDL">
                    <asp:DropDownList ID="PlecDropDownList" runat="server" DataSourceID="PlecSqlDataSource2" DataTextField="Plec" DataValueField="Id_Plec"></asp:DropDownList><asp:SqlDataSource ID="PlecSqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Plec], [Plec] FROM [Plec]"></asp:SqlDataSource>
                </div>
            </div>
            <div id="EpokaContros">
                <asp:Label ID="EpokaLabel" runat="server" Text="Epoka" CssClass="label label-danger"></asp:Label>
                <div id="EpokaDDL">
                    <asp:DropDownList ID="EpokaDropDownList1" runat="server" DataSourceID="EpokaSqlDataSource1" DataTextField="Epoka" DataValueField="Id_Epoki"></asp:DropDownList><asp:SqlDataSource ID="EpokaSqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Epoki], [Epoka] FROM [Epoka]"></asp:SqlDataSource>
                </div>
            </div>
            <div id="buttonAdd">
                <asp:Button ID="DodajButton" runat="server" Text="Zapisz" OnClick="DodajButton_Click" class="btn btn-danger" />
            </div>
        </div>
</asp:Content>
