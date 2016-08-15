<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministratorPanel.aspx.cs" Inherits="inzPJATKSNM.Views.AdministratorPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div id="content" class="container-fluid">
        <div class="row offset 3">
            <div class="col-sm-1 col-md-2">
                <div class="thumbnail tile tile-medium tile-lime">
                    <a href="AddNewPhoto.aspx" style="color:transparent">AddNewPhoto.aspx<h3>Nowe Zdjecie</h3></a>
                </div>
            </div>
            <div class="col-sm-1 col-md-4">
                <div class="thumbnail tile tile-wide tile-teal">  
                      <!-- <img src="images/twittertile.png"> -->
                      <a href="NewSurvey.aspx" style="color:transparent">NewSurvey.aspx<h3>Nowa Ankieta</h3></a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2">
                <div class="thumbnail tile tile-medium tile-orange">
                    <a href="AddMusic.aspx" style="color:transparent">AddMusic.aspx<h3>Muzyka</h3></a>
                </div>
            </div>

        </div>
        <div class="row offset 3">
            <div class="col-sm-1 col-md-4">
                <div class="thumbnail tile tile-wide tile-red">
                    <a href="ShowSurveys.aspx" style="color:transparent">ShowSurveys.aspx<h3>Pokaz Ankiety</h3></a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2">
                <div class="thumbnail tile tile-medium">
                    <a href="AddAuthor.aspx" style="color:transparent">AddAuthor.aspx<h3>Autorzy</h3></a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2">
                <div class="thumbnail tile tile-medium tile-green">
                    <a href="#">
                        <h4 class="tile-text">Statystyki</h4>
                    </a>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
