<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaticticsPerSurveys.aspx.cs" Inherits="inzPJATKSNM.Views.StaticticsPerServey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function mailOpenModal() {
            $('#failModal').modal('show');
        }
    </script>
    <script type="text/javascript">
        function failOpenModal() {
            $('#mailModal').modal('show');
        }
    </script>
    <div id="page">
        <br />
        <% 
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
                Response.Write("<p><a href=\"Statistics.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-success\" role=\"button\">Pokaż statystykę.</a></p></div></div>");
            }
            Response.Write("</div>");
   
        %>
    </div>
    <div id="failModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Wystąpił błąd!</h4>
                </div>
                <div class="modal-body">
                    <%
                        Response.Write("<p> " + val + "</p>");
                    %>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                </div>
            </div>

        </div>
    </div>
    <div id="mailModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Wystąpił błąd!</h4>
                </div>
                <div class="modal-body">
                    <%
                        Response.Write("<p> " + Request.QueryString["err"] + "</p>");
                    %>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
