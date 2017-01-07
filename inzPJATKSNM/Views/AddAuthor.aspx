<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAuthor.aspx.cs" Inherits="inzPJATKSNM.Views.AddAuthor" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <script src="../Scripts/jquery-3.1.0.js"></script>
    <div id="autorDiv">
        <div id="content" class="container-fluid">
            <div id="left" style="float: left; width: 40%">
                <div id="NameControls">
                    <asp:Label ID="AuthorNameLabel" runat="server" Text="Imię" CssClass="label label-danger" meta:resourcekey="AuthorNameLabelResource1"></asp:Label>
                    <div id="NameControl">
                        <asp:TextBox ID="AuthorNameTextBox" runat="server" ClientIDMode="Static" CssClass="form-control" meta:resourcekey="AuthorNameTextBoxResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pole Imię jest wymagane!" Display="Dynamic" ControlToValidate="AuthorNameTextBox" ForeColor="Red" Font-Bold="True" ValidationGroup="A" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Pole Imię zawiera nieprawidłowe znaki!" Display="Dynamic" ControlToValidate="AuthorNameTextBox" ValidationExpression="^[a-zA-Z]+" ForeColor="Red" Font-Bold="True" ValidationGroup="A" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div id="SurnameControls">
                    <asp:Label ID="AuthorSurnameLabel" runat="server" Text="Nazwisko" CssClass="label label-danger" meta:resourcekey="AuthorSurnameLabelResource1"></asp:Label>
                    <div id="SurnameControl">
                        <asp:TextBox ID="AuthorSurnameTextBox" runat="server" ClientIDMode="Static" CssClass="form-control" meta:resourcekey="AuthorSurnameTextBoxResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Pole Nazwisko jest wymagane!" Display="Dynamic" ControlToValidate="AuthorSurnameTextBox" ForeColor="Red" Font-Bold="True" ValidationGroup="A" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Pole Nazwisko zawiera nieprawidłowe znaki!" Display="Dynamic" ControlToValidate="AuthorSurnameTextBox" ValidationExpression="^[a-zA-Z]+" ForeColor="Red" Font-Bold="True" ValidationGroup="A" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div id="NationalityControls">
                    <asp:Label ID="NationalityLabel" runat="server" Text="Narodowość" CssClass="label label-danger" meta:resourcekey="NationalityLabelResource1"></asp:Label>
                    <div id="NationalityDDL">
                        <asp:DropDownList ID="NationalityDropDownList" runat="server" DataSourceID="NarodowoscSqlDataSource1" DataTextField="Narodowosc" CssClass="form-control" DataValueField="Id_Narod" meta:resourcekey="NationalityDropDownListResource1"></asp:DropDownList><asp:SqlDataSource ID="NarodowoscSqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Narod], [Narodowosc] FROM [Narodowosc]"></asp:SqlDataSource>
                    </div>
                </div>
                <div id="SexControls">
                    <asp:Label ID="sexLabel" runat="server" Text="Płeć" CssClass="label label-danger" meta:resourcekey="sexLabelResource1"></asp:Label>
                    <div id="SexDDL">
                        <asp:DropDownList ID="PlecDropDownList" runat="server" DataSourceID="PlecSqlDataSource2" CssClass="form-control" DataTextField="Plec" DataValueField="Id_Plec" meta:resourcekey="PlecDropDownListResource1"></asp:DropDownList><asp:SqlDataSource ID="PlecSqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Plec], [Plec] FROM [Plec]"></asp:SqlDataSource>
                    </div>
                </div>
                <div id="EpokaContros">
                    <asp:Label ID="EpokaLabel" runat="server" Text="Epoka" CssClass="label label-danger" meta:resourcekey="EpokaLabelResource1"></asp:Label>
                    <div id="EpokaDDL">
                        <asp:DropDownList ID="EpokaDropDownList1" runat="server" DataSourceID="EpokaSqlDataSource1" CssClass="form-control" DataTextField="Epoka" DataValueField="Id_Epoki" meta:resourcekey="EpokaDropDownList1Resource1"></asp:DropDownList><asp:SqlDataSource ID="EpokaSqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Epoki], [Epoka] FROM [Epoka]"></asp:SqlDataSource>
                    </div>
                </div>
                <div id="buttonAdd">
                    <asp:Button ID="DodajButton" runat="server" Text="Zapisz" OnClick="DodajButton_Click" class="btn btn-danger" ValidationGroup="A" meta:resourcekey="DodajButtonResource1" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
