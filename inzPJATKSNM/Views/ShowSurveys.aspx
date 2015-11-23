<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="ShowSurveys.aspx.cs" Inherits="inzPJATKSNM.Views.ShowSurveys" %>
<asp:Content ID="Content1"  ContentPlaceHolderID="MainContent" runat="server">


    <br />
    <br />

    <% 
     Response.Write("<div class = \"row\">");
            foreach(KeyValuePair<int, String> kvp in getURLDict()){
               String nazwa = getNazwyDict()[kvp.Key];
               String opis = getOpisyDict()[kvp.Key];
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\">");
                Response.Write("<img src=\""+kvp.Value+"\" alt=\" "+ nazwa +"\">");
                Response.Write("</div>");
                Response.Write("<div class=\"caption\">");   
                Response.Write("<h3>" +nazwa +"</h3>");        
                Response.Write("<p>"+opis+"</p>");
                Response.Write("<p> <button onclick=\"otworzModal(\'" + kvp.Key + "\');return false;\" class=\"btn btn-danger\">Usuń </button> <a href=\"EditExistingSurvey.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-success\" role=\"button\">Edytuj</a><a href=\"EditExistingSurvey.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-primary\" role=\"button\">Udostępnij</a></p></div></div>");   
            }
                Response.Write("</div>");
   
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
        function editItem(i) {
            var ida = Sys.Serialization.JavaScriptSerializer.serialize(i);
            debugger;
            
            $.ajax({
                url: '<%= ResolveUrl("EditExistingSurvey.aspx/returnId") %>',
                method: 'post',
                contentType: 'application/json',
                data: '{"id":' + ida + ' }',
                dataType: 'json',
                success: function (response) {
                    
                },
                error: function (er) {
                    Alert("Zdarzył się potworny błąd!!!")
                }
            });
            
            
        }
</script>

        <!-- Modal -->
        <div id="myModal"  class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button   type="button" class="close" data-dismiss="modal">&times;</button>
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

    
</asp:Content>
