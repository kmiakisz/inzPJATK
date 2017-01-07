<%@ Page Title="" Language="C#" MasterPageFile="~/VoteMaster.Master" AutoEventWireup="true" CodeBehind="Surveys.aspx.cs" Inherits="inzPJATKSNM.Views.Surveys" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function mailOpenModal() {
            $('#failModal').modal('show');
        }
    </script>
    <script type="text/javascript">
        function validateEmail() {
            var email = document.getElementById("emailField").value;
            var re = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
            if (!re.test(email)) {
                alert("Email is not valid! Please correct your email address or leave it as blank.")
            }
        }
    </script>
      <script type="text/javascript">
          function failOpenModal() {
              $('#mailModal').modal('show');
          }
    </script>
        <script type="text/javascript">
            function subscriptionOpenModal() {
                $('#subscriptionModal').modal('show');
            }
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             $(".addEmail").click(function (event) {
                 var emailVal = $("#subscriptionModal").find('input[name="email"]').val();
                 var link = Sys.Serialization.JavaScriptSerializer.serialize(emailVal);
                 $.ajax({
                    url: '<%= ResolveUrl("Surveys.aspx/addEmail") %>',
                           method: 'post',
                           contentType: 'application/json',
                           data: '{"email":' + link + ' }',
                           dataType: 'json',
                           success: function () {
                               alert(e);
                           },
                           error: function (er) {
                               alert(JSON.stringify(er))
                           }
                });
            });
    });
         </script>
    <br /><br />
     <% 
         Response.Write("<br/><div class = \"row\">");
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
                Response.Write("<p><a href=\"StartPage.aspx?" + "Id=" + kvp.Key + "\" class=\"btn btn-primary\" role=\"button\">Zagłosuj</a></p></div></div>");   
            }
                Response.Write("</div>");
   
        %>
       <div id="failModal"  class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button   type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Wystąpił błąd!</h4>
                    </div>
                    <div class="modal-body">
                        <%
                            Response.Write("<p> " + val +"</p>");
                             %>
                       
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                    </div>
                </div>

            </div>
        </div>
     <div id="mailModal"  class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button   type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Wystąpił błąd!</h4>
                    </div>
                    <div class="modal-body">
                        <%
                            Response.Write("<p> " + Request.QueryString["err"] +"</p>");
                             %>
                       
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                    </div>
                </div>

            </div>
        </div>
            <!-- subscriptionModal -->
        <div id="subscriptionModal"  class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button   type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Dziękujemy!</h4>
                    </div>
                    <div class="modal-body">
                        <p>Dziękujemy za uzupełnienie ankiety.</p>
                        <br/>
                        <p>Jeżeli Ci się podobało, bądź na bieżąco i subskrybuj nas!</p>
                        <br />
                        <label for="comment"><span class="glyphicon glyphicon-envelope"></span>Email: </label>
                        <br />
                        <input class="email" name="email" id="emailField" onchange="validateEmail();"/>
                        <h5 id='result'></h5>
                    </div>
                    <div class="modal-footer">
                        <button type="button" data-dismiss="modal" class="addEmail" onclick="subscription_Click">OK</button>
                    </div>
                </div>

            </div>
        </div>
</asp:Content>
