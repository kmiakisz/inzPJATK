<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AboutUser.aspx.cs" Inherits="inzPJATKSNM.Views.AboutUser" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div id="left" style="float: left; width: 50%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title"><asp:Label ID="AboutUserLbl" runat="server" Text="Dane użytkownika." meta:resourcekey="AboutUserLblResource1"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-hover table-striped" DataSourceID="ObjectDataSource1" meta:resourcekey="GridView1Resource1">
                        <Columns>
                            <asp:BoundField DataField="login" HeaderText="login" SortExpression="login" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="imie" HeaderText="imie" SortExpression="imie" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="nazwisko" HeaderText="nazwisko" SortExpression="nazwisko" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="nazwaRoli" HeaderText="nazwaRoli" SortExpression="nazwaRoli" meta:resourcekey="BoundFieldResource4" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetUser" TypeName="inzPJATKSNM.Controllers.UserController">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
            <div id="backButtonDiv">
                <asp:Button ID="BckBtn" runat="server" Text="Powrót." CssClass="btn btn-success" meta:resourcekey="BckBtnResource1" OnClick="BckBtn_Click" />
            </div>
        </div>
        <div id="right" style="float: right; width: 35%">
            <asp:Button ID="ResPwdBtn" runat="server" Text="Reset Hasła." CssClass="btn btn-danger" OnClick="ResPwdBtn_Click" meta:resourcekey="ResPwdBtnResource1" />
        </div>
    </div>
</asp:Content>
