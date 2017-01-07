<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditExistingSurvey.aspx.cs" Inherits="inzPJATKSNM.Views.EditExistingSurvey" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Demo.css" rel="stylesheet" />
    <link href="../Content/lightslider.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.1.4.js"></script>
    <script src="../Scripts/lightslider.js"></script>
    <script src="../Scripts/datapicker.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/jquery-3.1.0.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            /* $('#lightSlider').lightSlider({
                 gallery: true,
                 item: 1,
                 loop: true,
                 slideMargin: 0,
                 thumbItem: 9
             });*/
            $(".addPhoto").click(function (event) {

                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
                alert("add");
                $.ajax({
                    url: '<%= ResolveUrl("EditExistingSurvey.aspx/AddPhoto") %>',
                    method: 'post',
                    contentType: 'application/json',
                    data: '{"url":' + link + ' }',
                    dataType: 'json',
                    success: function () {
                        location.reload();
                    },
                    error: function (er) {
                    }
                });
            });
            $(".delete").click(function (event) {
                //$("#" + event.target.id).css("background-color", "transparent"); // commented due to not worked delete function : unexpected err in console
                alert("delete");
                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);

                $.ajax({
                    url: '<%= ResolveUrl("EditExistingSurvey.aspx/removePhotoFromSurvey") %>',
                    method: 'post',
                    contentType: 'application/json',
                    data: '{"url":' + link + ' }',
                    dataType: 'json',
                    success: function () {
                        location.reload();
                    },
                    error: function (er) {

                    }
                });
            });

        });

    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            /* $('#lightSlider2').lightSlider({
                 gallery: true,
                 item: 1,
                 loop: true,
                 slideMargin: 0,
                 thumbItem: 9
             });*/
            $(".update").click(function (event) {

                //debugger;
                $("#" + event.target.id).css("background-color", "transparent");

                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
                $.ajax({
                    url: '<%= ResolveUrl("NewSurvey.aspx/AddPhotoToSurvey") %>',
                    method: 'post',
                    contentType: 'application/json',
                    data: '{"url":' + link + ' }',
                    dataType: 'json',
                    success: function () {

                    },
                    error: function (er) {

                    }
                });
            });

        });

    </script>
    <!--<script type="text/javascript">
        // When the document is ready
        $(document).ready(function () {

            $('#example1').datepicker({
                format: "dd/mm/yyyy"
            });

        });
    </script>-->

    <script runat="server">
        protected List<String> GetList()
        {
            List<String> photoFromDB;
            // photoFromDB = inzPJATKSNM.Controllers.NewSurveyController.getPhotoList();
            return new List<String>();
        }
    </script>
    <script runat="server">
        protected void removeFromSurvey(int id, string url)
        {
            usedPhotos.Remove(id);
            freePhotos.Add(id, url);
        }
    </script>
    <script runat="server">
        protected void addToSurvey(int id, string url)
        {
            freePhotos.Remove(id);
            usedPhotos.Add(id, url);
        }
    </script>
    <script runat="server">
        protected Dictionary<Int32, String> GetFreePhotos()
        {
            Dictionary<Int32, String> freePhotosFromDB;
            freePhotosFromDB = inzPJATKSNM.Controllers.EditExistingSurveyController.getFreePhotos();
            return freePhotosFromDB;
        }
    </script>
    <script runat="server">
        protected Dictionary<Int32, String> GetUsedPhotos()
        {
            int surveyId = int.Parse(Request.QueryString["Id"]);
            Dictionary<Int32, String> usedPhotosFromDB;
            usedPhotosFromDB = inzPJATKSNM.Controllers.EditExistingSurveyController.getUsedPhotos(surveyId);
            return usedPhotosFromDB;
        }
    </script>

    <div id="content" class="container-fluid">
        <h3><span class="label label-danger">Edycja Ankiety</span></h3>
        <div id="left" style="float: left; width: 30%">
            <asp:Label ID="SurveyNameLabel" runat="server" Text="Nazwa Ankiety" class="label label-danger" meta:resourcekey="SurveyNameLabelResource1"></asp:Label>
            <asp:TextBox ID="SurveyNameTextBox1" runat="server" CssClass="form-control" meta:resourcekey="SurveyNameTextBox1Resource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nazwa Ankiety nie może być pusta!" ControlToValidate="SurveyNameTextBox1" Display="Dynamic" Font-Bold="True" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
            <asp:Label ID="ServeyDescribtionLabel" runat="server" Text="Opis Ankiety" class="label label-danger" meta:resourcekey="ServeyDescribtionLabelResource1"></asp:Label>
            <asp:TextBox ID="ServeyDescribtionTextBox1" runat="server" class="form-control" meta:resourcekey="ServeyDescribtionTextBox1Resource1"></asp:TextBox>
            <asp:Label ID="MusicLabel" runat="server" Text="Typ ankiety:" class="label label-danger" meta:resourcekey="MusicLabelResource1"></asp:Label>
            <asp:DropDownList ID="TypeDropDownList" runat="server" class="form-control" Style="width: 80%" meta:resourcekey="TypeDropDownListResource1">
                <asp:ListItem Text="PUBLICZNA" Value="PUBLIC" Enabled="true" meta:resourcekey="ListItemResource1">PUBLICZNA</asp:ListItem>
                <asp:ListItem Text="PRYWATNA" Value="PRIVATE" Enabled="true" meta:resourcekey="ListItemResource2">PRYWATNA</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="ZakDatLbl" runat="server" Text="Data rozpoczęcia : " CssClass="label label-danger" meta:resourcekey="ZakDatLblResource1"></asp:Label>
            <br /><br />
            <asp:Label ID="Label1" runat="server" Text="Wybierz date zakonczenia(dd-mm-yyyy): " CssClass="label label-danger" meta:resourcekey="Label1Resource1"></asp:Label>
            <br />
            <asp:TextBox ID="EndDateTxt" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Status: " CssClass="label label-danger" meta:resourcekey="Label2Resource1"></asp:Label>
            <br />
            <br />
            <div id="Buttons" style="float: left; width: 56%">
                <asp:Button ID="AcceptButton" runat="server" Text="Zapisz" class="btn btn-success" Style="float: left" OnClick="AcceptButton_Click" meta:resourcekey="AcceptButtonResource1" />
                <asp:Button ID="CancelButton" runat="server" Text="Anuluj" class="btn btn-danger" Style="float: right" OnClick="CancelButton_Click" meta:resourcekey="CancelButtonResource1" />
            </div>

        </div>
        <div class="demo" style="float: right; width: 45%">
            <%
                Response.Write("<h3><b>Zdjęcia w ankiecie</b></h3>");
            %>
            <!--   <ul id="lightSlider">
                <% 
                foreach (inzPJATKSNM.Models.Dzieło dzielo in getSurveyPhotos())
                {
                    Response.Write("<li data-thumb=" + dzielo.URL + ">"
                        + " <div class=\"show-image\" id=" + dzielo.Id_dzieło + ">"
                        + " <img src=" + dzielo.URL + " />"
                        + " <input class=\"delete\" type=\"button\" value=\" \" onserverclick=\"AddToSurvey\" id=" + dzielo.Id_dzieło + " name =" + dzielo.URL + ">"
                            + " </div>"
                        + "</li>  ");

                }
                %>
            </ul>-->
            <% 
                Response.Write("<div class = \"row\">");

                foreach (inzPJATKSNM.Models.Dzieło dzielo in getSurveyPhotos())
                {
                    Response.Write("<div class=\"col-sm-6 col-md-3\">");
                    Response.Write("<div class=\"thumbnail\" width=100 height=100>");
                    Response.Write("<input id=\"" + dzielo.URL + "\" name=\"" + dzielo.URL + "\" type=\"submit\" class=\"delete\" runat=\"server\" value=\"Usuń\" style=\"background-color:transparent; border-color:transparent;\" onserverclick=\"removeFromSurvey\">");
                    Response.Write("<img src=\"" + dzielo.URL + "\"/>");
                    Response.Write("</input>");
                    Response.Write("</div>");
                    Response.Write("</div>");
                }
                Response.Write("</div>");
            %>
            <br />

            <div>

                <h3><b>Dostępne zdjęcia</b></h3>
                <div style="float: left; width: 33%;">
                    <asp:Label ID="CategoryLbl" runat="server" Text="Kategoria" Font-Bold="True" ForeColor="Red" meta:resourcekey="CategoryLblResource1"></asp:Label>
                    <br />
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="KategorieDataSource" DataTextField="Kategoria" DataValueField="Id_Kat" OnTextChanged="kategoriaChanged" AutoPostBack="True"
                        OnSelectedIndexChanged="kategoriaChanged" AppendDataBoundItems="True" meta:resourcekey="DropDownList2Resource1">
                        <asp:ListItem Selected="True" Value="0" meta:resourcekey="ListItemResource3">Wszystkie</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="float: left; width: 33%;">
                    <asp:Label ID="TecLbl" runat="server" Text="Technika" Font-Bold="True" ForeColor="Red" meta:resourcekey="TecLblResource1"></asp:Label>
                    <br />
                    <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="TechnikaDataSource" DataTextField="Technika" DataValueField="Id_Tech" AutoPostBack="True"
                        OnSelectedIndexChanged="technikaChanged" AppendDataBoundItems="True" meta:resourcekey="DropDownList3Resource1">
                        <asp:ListItem Selected="True" Value="0" meta:resourcekey="ListItemResource4">Wszystkie</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="float: left; width: 33%;">
                    <asp:Label ID="AuthLbl" runat="server" Text="Autor" Font-Bold="True" ForeColor="Red" meta:resourcekey="AuthLblResource1"></asp:Label>
                    <br />
                    <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="AutorDataSource" DataTextField="Nazwisko" DataValueField="Id_Autora" AutoPostBack="True"
                        OnSelectedIndexChanged="autorChanged" AppendDataBoundItems="True" meta:resourcekey="DropDownList4Resource1">
                        <asp:ListItem Selected="True" Value="0" meta:resourcekey="ListItemResource5">Wszystkie</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <asp:SqlDataSource ID="KategorieDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Kat], [Kategoria] FROM [Kategoria]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="TechnikaDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Tech], [Technika] FROM [Technika]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="AutorDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Autora], [Nazwisko] FROM [Autor]"></asp:SqlDataSource>
                <br />
                <% 
                    Response.Write("<div class = \"row\">");

                    foreach (inzPJATKSNM.Models.Dzieło dzielo in getFilteredPhoto())
                    {
                        Response.Write("<div class=\"col-sm-6 col-md-3\">");
                        Response.Write("<div class=\"thumbnail\" width=100px height=100px>");
                        Response.Write("<input id=\"" + dzielo.URL + "\" name=\"" + dzielo.URL + "\" type=\"submit\" class=\"addPhoto\" runat=\"server\" value=\"Dodaj\" style=\"background-color:transparent; border-color:transparent;\" onserverclick=\"addPhoto\">");
                        Response.Write("<img src=\"" + dzielo.URL + "\"/>");
                        Response.Write("</input>");
                        Response.Write("</div>");
                        Response.Write("</div>");
                    }
                    Response.Write("</div>");
                %>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function failOpenModal() {
            $('#failModal').modal('show');
        }
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
</asp:Content>
