<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministratorPanel.aspx.cs" Inherits="inzPJATKSNM.Views.AdministratorPanel" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    w<br />
    <script type="text/javascript">
        function failOpenModal() {
            $('#failModal').modal('show');
        }
    </script>
    <div id="content" class="container-fluid">
        <div class="row offset 3">
            <div class="col-sm-1 col-md-2" runat="server" id="newPhotoDiv">
                <div class="thumbnail tile tile-medium tile-lime">
                    <a href="AddNewPhoto.aspx" style="color: transparent">AddNewPhoto.aspx<h3>
                        <asp:Label ID="Label1" runat="server" Text="Nowe Zdjecie" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                    </a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2" runat="server" id="newSurveyDiv"> 
                <div class="thumbnail tile tile-medium tile-orange">
                    <a href="NewSurvey.aspx" style="color: transparent">NewSurvey.aspx<h3>
                        <asp:Label ID="Label2" runat="server" Text="Nowa </br> Ankieta" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                    </a>
                </div>
            </div>
            <div class="col-sm-1 col-md-4" runat="server" id="userPanelDiv">
                <div class="thumbnail tile tile-wide tile-teal">
                    <div id="button" class="container-fluid">
                        <br />
                        <asp:Button ID="Button1" Text="Panel użytkownika" runat="server" Style="background-color: Transparent;border:none;width:100%;height:100%;font-size:xx-large;outline:none" OnClick="AboutUserClick" meta:resourcekey="Button1Resource1"></asp:Button>
                    </div>

                </div>
            </div>
            <div class="col-sm-1 col-md-2" runat="server" id="mgmtDiv">
                <div class="thumbnail tile tile-wide tile-red">
                    <a href="ManageUsers.aspx" style="color: transparent">ManageUsers.aspx<h3>
                        <asp:Label ID="Label3" runat="server" Text="Zarządzanie<br />
                        użytkownikami" meta:resourcekey="Label3Resource1"></asp:Label></h3>
                    </a>
                </div>
            </div>
        </div>
        <div class="row offset 3">
            <div class="col-sm-1 col-md-4" runat="server" id="showSurveysDiv">
                <div class="thumbnail tile tile-wide tile-red">
                    <a href="ShowSurveys.aspx" style="color: transparent">ShowSurveys.aspx<h3>
                        <asp:Label ID="Label4" runat="server" Text="Pokaz Ankiety" meta:resourcekey="Label4Resource1"></asp:Label></h3>
                    </a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2" runat="server" id="authorsDiv">
                <div class="thumbnail tile tile-medium">
                    <a href="AddAuthor.aspx" style="color: transparent">AddAuthor.aspx<h3>
                        <asp:Label ID="Label5" runat="server" Text="Autorzy" meta:resourcekey="Label5Resource1"></asp:Label></h3>
                    </a>
                </div>
            </div>
            <div class="col-sm-1 col-md-2" runat="server" id="statisticsDiv">
                <div class="thumbnail tile tile-medium tile-green">
                    <a href="Statistics.aspx" style="color: transparent">Statistics.aspx<h4>
                        <asp:Label ID="Label6" runat="server" Text="Statystyki" meta:resourcekey="Label6Resource1"></asp:Label></h4>
                    </a>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("a#AboutUser").click(function (event) {
                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
                $.ajax({
                    url: "AdministratorPanel.aspx/GetUrl",
                    method: 'post',
                    contentType: 'application/json charset=utf-8',
                    data: '{dupa:' + link + '}',
                    dataType: 'json',
                    success: function (data) {
                        console.log(data.d);
                        window.location.href = data.d;
                    },
                    error: function (er) {
                        console.log(data.d + er.d);
                        alert(er.Message);
                    }
                });
            });

        });

    </script>
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
    <script>

    </script>

</asp:Content>
