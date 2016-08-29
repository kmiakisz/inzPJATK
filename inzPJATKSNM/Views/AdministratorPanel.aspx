<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministratorPanel.aspx.cs" Inherits="inzPJATKSNM.Views.AdministratorPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <script type="text/javascript">
        function failOpenModal() {
            $('#failModal').modal('show');
        }
    </script>
    <div id="content" class="container-fluid">
        <div class="row offset 3">
            <div class="col-sm-1 col-md-2">
                <div class="thumbnail tile tile-medium tile-lime">
                    <a href="AddNewPhoto.aspx" style="color: transparent">AddNewPhoto.aspx<h3>Nowe Zdjecie</h3>
                    </a>
                </div>
            </div>
            <div class="col-sm-1 col-md-4">
                <div class="thumbnail tile tile-wide tile-teal">
                    <!-- <img src="images/twittertile.png"> -->
                    <a href="NewSurvey.aspx" style="color: transparent">NewSurvey.aspx<h3>Nowa Ankieta</h3>
                    </a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2">
                <div class="thumbnail tile tile-medium tile-orange">
                    <a href="AddMusic.aspx" style="color: transparent">AddMusic.aspx<h3>Muzyka</h3>
                    </a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2" runat="server" id="mgmtDiv">
                <div class="thumbnail tile tile-wide tile-red">
                    <a href="ManageUsers.aspx" style="color: transparent">ManageUsers.aspx<h3>Zarządzanie<br/>użytkownikami</h3>
                    </a>
                </div>
            </div>
        </div>
        <div class="row offset 3">
            <div class="col-sm-1 col-md-4">
                <div class="thumbnail tile tile-wide tile-red">
                    <a href="ShowSurveys.aspx" style="color: transparent">ShowSurveys.aspx<h3>Pokaz Ankiety</h3>
                    </a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2">
                <div class="thumbnail tile tile-medium">
                    <a href="AddAuthor.aspx" style="color: transparent">AddAuthor.aspx<h3>Autorzy</h3>
                    </a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2">
                <div class="thumbnail tile tile-medium tile-green">
                    <a href="Statistics.aspx" style="color: transparent">Statistics.aspx<h4>Statystyki</h4>
                    </a>
                </div>
            </div>

        </div>
    </div>
    <div id="failModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Wystąpił błąd</h4>
                </div>
                <div class="modal-body">
                    <%
                        Response.Write("<p>" + Request.QueryString["err"] + "</p>");
                    %>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
