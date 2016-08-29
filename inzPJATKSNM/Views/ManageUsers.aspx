﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="inzPJATKSNM.Views.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div id="left" style="float: left; width: 60%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">Zarządzanie Użytkownikami</h3>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-striped" GridLines="None"
                        AutoGenerateColumns="False" DataSourceID="ManageUsersDataSource" AllowPaging="True" AllowSorting="True">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="Login-Email" HeaderText="Login-Email" SortExpression="Login-Email" />
                            <asp:BoundField DataField="Imię" HeaderText="Imię" SortExpression="Imię" />
                            <asp:BoundField DataField="Nazwisko" HeaderText="Nazwisko" SortExpression="Nazwisko" />
                            <asp:BoundField DataField="Rola" HeaderText="Rola" SortExpression="Rola" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="ManageUsersDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT U.EMAIL_LOGIN AS &quot;Login-Email&quot; , U.NAME AS Imię, U.SURNAME AS Nazwisko, R.ROLE_NAME AS Rola
FROM SNM_USER U
JOIN SNM_ROLE R
ON U.ID_ROLE = R.ID_ROLE;"></asp:SqlDataSource>
                </div>
            </div>
            <div id="buttons">
                <asp:Button ID="BckBtn" runat="server" Text="Powrót" CssClass="btn btn-success" />
            </div>
        </div>
        <div class="demo" style="float: right; width: 30%">
            <asp:Button ID="NewUserBtn" runat="server" Text="Dodaj nowego użytkownika" CssClass="btn btn-danger" Width="200px" OnClick="NewUserBtn_Click"/>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Free" CssClass="btn btn-danger" Width="200px"/>
        </div>
    </div>
</asp:Content>
