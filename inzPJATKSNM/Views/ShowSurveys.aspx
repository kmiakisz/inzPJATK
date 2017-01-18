<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowSurveys.aspx.cs" Inherits="inzPJATKSNM.Views.ShowSurveys" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <script type="text/javascript">
        function mailOpenModal() {
            $('#mailModal').modal('show');
        }
    </script>
    <script type="text/javascript">
        function failOpenModal() {
            $('#failModal').modal('show');
        }
    </script>
    <%
        string username = inzPJATKSNM.Controllers.AuthenticationController.getLogin((string)HttpContext.Current.Session["token"]);
        inzPJATKSNM.AuthModels.User user = inzPJATKSNM.Controllers.AuthenticationController.getUser(username);
        List<Int32> list = inzPJATKSNM.Controllers.UserController.GetUserPrivilegeListIdPerUserId(user.userId);
        if (list.Contains(8) && !list.Contains(14) && !list.Contains(6))//system mailingowy
        {
            Response.Write("<div class = \"row\">");
            foreach (KeyValuePair<int, String> kvp in getURLDict())
            {
                String nazwa = getNazwyDict()[kvp.Key];
                String opis = getOpisyDict()[kvp.Key];
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\">");
                Response.Write("<img src=\"" + kvp.Value + "\" alt=\" " + nazwa + "\">");
                Response.Write("</div>");
                Response.Write("<div class=\"caption\">");
                Response.Write("<h3>" + nazwa + "</h3>");
                Response.Write("<p>" + opis + "</p>");
                Response.Write("<p><a href=\"SendMailForm.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-primary\" role=\"button\">Udostępnij</a></p></div></div>");
            }
            Response.Write("</div>");
        }
        if (list.Contains(8) && !list.Contains(14) && list.Contains(6))//system mailingowy+edycja ankiet
        {
            Response.Write("<div class = \"row\">");
            foreach (KeyValuePair<int, String> kvp in getURLDict())
            {
                String nazwa = getNazwyDict()[kvp.Key];
                String opis = getOpisyDict()[kvp.Key];
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\">");
                Response.Write("<img src=\"" + kvp.Value + "\" alt=\" " + nazwa + "\">");
                Response.Write("</div>");
                Response.Write("<div class=\"caption\">");
                Response.Write("<h3>" + nazwa + "</h3>");
                Response.Write("<p>" + opis + "</p>");
                Response.Write("<p><a href=\"EditExistingSurvey.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-success\" role=\"button\">Edytuj</a>&nbsp<a href=\"SendMailForm.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-primary\" role=\"button\">Udostępnij</a></p></div></div>");
            }
            Response.Write("</div>");
        }
        if (list.Contains(8) && list.Contains(14) && !list.Contains(6))//system mailingowy+usuwanie
        {
            Response.Write("<div class = \"row\">");
            foreach (KeyValuePair<int, String> kvp in getURLDict())
            {
                String nazwa = getNazwyDict()[kvp.Key];
                String opis = getOpisyDict()[kvp.Key];
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\">");
                Response.Write("<img src=\"" + kvp.Value + "\" alt=\" " + nazwa + "\">");
                Response.Write("</div>");
                Response.Write("<div class=\"caption\">");
                Response.Write("<h3>" + nazwa + "</h3>");
                Response.Write("<p>" + opis + "</p>");
                Response.Write("<p><button onclick=\"otworzModal(\'" + kvp.Key + "\');return false;\" class=\"btn btn-danger\">Usuń </button>&nbsp<a href=\"SendMailForm.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-primary\" role=\"button\">Udostępnij</a></p></div></div>");
            }
            Response.Write("</div>");
        }
        if (list.Contains(14) && !list.Contains(6) && !list.Contains(8))//usuwanie
        {
            Response.Write("<div class = \"row\">");
            foreach (KeyValuePair<int, String> kvp in getURLDict())
            {
                String nazwa = getNazwyDict()[kvp.Key];
                String opis = getOpisyDict()[kvp.Key];
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\">");
                Response.Write("<img src=\"" + kvp.Value + "\" alt=\" " + nazwa + "\">");
                Response.Write("</div>");
                Response.Write("<div class=\"caption\">");
                Response.Write("<h3>" + nazwa + "</h3>");
                Response.Write("<p>" + opis + "</p>");
                Response.Write("<p><button onclick=\"otworzModal(\'" + kvp.Key + "\');return false;\" class=\"btn btn-danger\">Usuń </button></p></div></div>");
            }
            Response.Write("</div>");
        }
        if (list.Contains(14) && list.Contains(6) && !list.Contains(8))//usuwanie+edycja ankiet
        {
            Response.Write("<div class = \"row\">");
            foreach (KeyValuePair<int, String> kvp in getURLDict())
            {
                String nazwa = getNazwyDict()[kvp.Key];
                String opis = getOpisyDict()[kvp.Key];
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\">");
                Response.Write("<img src=\"" + kvp.Value + "\" alt=\" " + nazwa + "\">");
                Response.Write("</div>");
                Response.Write("<div class=\"caption\">");
                Response.Write("<h3>" + nazwa + "</h3>");
                Response.Write("<p>" + opis + "</p>");
                Response.Write("<p><button onclick=\"otworzModal(\'" + kvp.Key + "\');return false;\" class=\"btn btn-danger\">Usuń </button> <a href=\"EditExistingSurvey.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-success\" role=\"button\">Edytuj</a></p></div></div>");
            }
            Response.Write("</div>");
        }
        if (list.Contains(6) && !list.Contains(14) && !list.Contains(8))//edycja
        {
            Response.Write("<div class = \"row\">");
            foreach (KeyValuePair<int, String> kvp in getURLDict())
            {
                String nazwa = getNazwyDict()[kvp.Key];
                String opis = getOpisyDict()[kvp.Key];
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\">");
                Response.Write("<img src=\"" + kvp.Value + "\" alt=\" " + nazwa + "\">");
                Response.Write("</div>");
                Response.Write("<div class=\"caption\">");
                Response.Write("<h3>" + nazwa + "</h3>");
                Response.Write("<p>" + opis + "</p>");
                Response.Write("<p><a href=\"EditExistingSurvey.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-success\" role=\"button\">Edytuj</a></p></div></div>");
            }
            Response.Write("</div>");
        }
        if (list.Contains(8) && list.Contains(14) && list.Contains(6))//all
        {
            Response.Write("<div class = \"row\">");
            foreach (KeyValuePair<int, String> kvp in getURLDict())
            {
                String nazwa = getNazwyDict()[kvp.Key];
                String opis = getOpisyDict()[kvp.Key];
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\">");
                Response.Write("<img src=\"" + kvp.Value + "\" alt=\" " + nazwa + "\">");
                Response.Write("</div>");
                Response.Write("<div class=\"caption\">");
                Response.Write("<h3>" + nazwa + "</h3>");
                Response.Write("<p>" + opis + "</p>");
                Response.Write("<p><button onclick=\"otworzModal(\'" + kvp.Key + "\');return false;\" class=\"btn btn-danger\">Usuń </button> <a href=\"EditExistingSurvey.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-success\" role=\"button\">Edytuj</a>&nbsp<a href=\"SendMailForm.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-primary\" role=\"button\">Udostępnij</a></p></div></div>");
            }
            Response.Write("</div>");
        }
    %>
    <script type="text/javascript">
        var id;
        function otworzModal(i) {
            id = i;

            $('#myModal').modal('show');


        }
        function test() {
            var ida = Sys.Serialization.JavaScriptSerializer.serialize(id);

            $.ajax({
                url: '<%= ResolveUrl("ShowSurveys.aspx/usunAnkiete") %>',
                method: 'post',
                contentType: 'application/json',
                data: '{"id":' + ida + ' }',
                dataType: 'json',
                success: function () {

                },
                error: function (er) {
                    Alert("Zdarzył się potworny błąd!!!")
                }
            });
            $('#myModal').modal('hide');
            reloadPage();

        }
        function reloadPage() {
            window.location.reload()
        }
    </script>

    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Usuń</h4>
                </div>
                <div class="modal-body">
                    <p>Czy jesteś pewny, że chcesz usunąć ankietę? </p>
                </div>
                <div class="modal-footer">
                    <button onclick="test()" type="button" class="btn btn-danger">Usuń</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Anuluj</button>
                </div>
            </div>
        </div>
    </div>
    <!--Modal z Maili-->
    <div id="mailModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Wysłano maile</h4>
                </div>
                <div class="modal-body">
                    <%
                        Response.Write("<p>Wysłano " + Request.QueryString["val"] + " e-mail(e)!</p>");
                    %>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
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
