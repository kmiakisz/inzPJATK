<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="inzPJATKSNM.Views.ManageCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content" class="container-fluid">
        <div id="left" style="float: left; width: 55%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="CategoryLbl" runat="server" Text="Usuwanie Kategorii."></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <div id="CategoryGridViewDiv">
                        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                    </div>
                    <div id="CategoryAddBt1nDIV">
                    </div>
                </div>
            </div>
        </div>
        <div class="demo" style="float: right; width: 35%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="CategoryLbl1" runat="server" Text="Dodaj nową kategorię."></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <div id="CategoryTxtBoxDiv">

                    </div>
                    <div id="CategoryAddBtn2DIV">

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
