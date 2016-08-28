<%@ Page Title="" Language="C#" MasterPageFile="~/VoteMaster.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="inzPJATKSNM.Views.StartPage" runat="server"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Content/modal.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script> 
    <script src="../Scripts/jquery.fullscreenslides.js"></script>
    <link href="../Content/fullscreenstyle.css" rel="stylesheet" />
    
  <script type="text/javascript">
      function subscriptionOpenModal() {
          $('#mailModal').modal('show');
      }
    </script>
     <script type="text/javascript">
         function mailOpenModal() {
             alert("duzy chuj");
             $('#subscriptionModal').modal('show');
         }
    </script>
    <%
        Response.Write("<h1>" + inzPJATKSNM.Controllers.SurveyController.getSurveyName(int.Parse(Request.QueryString["Id"])) + "</h1>");
    %>
    
       <script id="sample">
           $(function () {
               // initialize the slideshow
               $('.image img').fullscreenslides();

               // All events are bound to this container element
               var $container = $('#fullscreenSlideshowContainer');

               $container
                 //This is triggered once:
                 .bind("init", function () {

                     // The slideshow does not provide its own UI, so add your own
                     // check the fullscreenstyle.css for corresponding styles
                     $container
                       .append('<div class="ui" id="fs-close">&times;</div>')
                       .append('<div class="ui" id="fs-loader">Loading...</div>')
                       .append('<div class="ui" id="fs-prev">&lt;</div>')
                       .append('<div class="ui" id="fs-next">&gt;</div>')
                       .append('<div class="ui" id="fs-caption"><span></span></div>');

                     // Bind to the ui elements and trigger slideshow events
                     $('#fs-prev').click(function () {
                         // You can trigger the transition to the previous slide
                         $container.trigger("prevSlide");
                     });
                     $('#fs-next').click(function () {
                         // You can trigger the transition to the next slide
                         $container.trigger("nextSlide");
                     });
                     $('#fs-close').click(function () {
                         // You can close the slide show like this:
                         $container.trigger("close");
                     });

                 })
                 // When a slide starts to load this is called
                 .bind("startLoading", function () {
                     // show spinner
                     $('#fs-loader').show();
                 })
                 // When a slide stops to load this is called:
                 .bind("stopLoading", function () {
                     // hide spinner
                     $('#fs-loader').hide();
                 })
                 // When a slide is shown this is called.
                 // The "loading" events are triggered only once per slide.
                 // The "start" and "end" events are called every time.
                 // Notice the "slide" argument:
                 .bind("startOfSlide", function (event, slide) {
                     // set and show caption
                     $('#fs-caption span').text(slide.title);
                     $('#fs-caption').show();
                 })
                 // before a slide is hidden this is called:
                 .bind("endOfSlide", function (event, slide) {
                     $('#fs-caption').hide();
                 });
           });
    </script>
   <% 
     Response.Write("<div class = \"row\">");
     if (dziela != null)
     {
         
         foreach (inzPJATKSNM.Models.Dzieło dzielo in dziela)
         {
             Response.Write("<div class=\"col-sm-6 col-md-3\">");
             Response.Write("<div class=\"image\">");
             Response.Write("<div class=\"thumbnail\">");
             Response.Write("<a href=\""+dzielo.URL+"\" >");
             Response.Write("<img src=\"" + dzielo.URL + "\">");
             Response.Write("</a>");
             Response.Write("</div></div>");
             Response.Write("<div class=\"caption\">");
             Response.Write("<h3>" + inzPJATKSNM.Controllers.SurveyController.getKategoria(dzielo.Id_Kat) + "</h3>");
             Response.Write("<p>" + inzPJATKSNM.Controllers.SurveyController.getTechnika(dzielo.Id_Tech) + "</p>");
             Response.Write("<p>" + inzPJATKSNM.Controllers.SurveyController.getAutor(dzielo.Id_Autora) + "</p>");
             Response.Write("<p>Ocena: </p><select onChange=\"removeSameValue("+dzielo.Id_dzieło+")\" id=\"" + dzielo.Id_dzieło + "\" name =\""+ dzielo.Id_dzieło + "\" class=\"form-control\">");
             Response.Write("<option value=\"--wybierz--\">--wybierz--</option>");
             for (int i = 1; i <= dziela.Count; i++)
             {
                 int j = i+1;
               Response.Write("<option value=\"" + j + "\">" + i + "</option>");
             }
                 Response.Write("</select>");
             Response.Write("</div></div>");
 
         }
         Response.Write("</div>");
     }
     else
     {
         //modal o pustych zdjeciach
     }
   
        %>
    <script type="text/javascript">
        function getOceny() {
            var nameValue;
            $selects.each(function () {
                $selects.not(this)
                if (this.value != "--wybierz--") {
                    $selects.find('option[value="' + this.value + '"]')
                }
                nameValue += this.name + ',' + this.value+';';
               
            });
            document.getElementById('<%=TextBox1.ClientID %>').value = nameValue;
        }
    </script>
    <br/>
    <br />
    <div id="buttons">
        <asp:Button ID="vote" runat="server" Text="Zagłosuj" class="btn btn-success" OnClick="vote_Click" OnClientClick="getOceny()"/>
        <asp:TextBox ID="TextBox1" name="TextBox1" runat="server" ClientIDMode="Static" style="display:none;"></asp:TextBox>
        &nbsp
        <asp:Button ID="cancel" runat="server" Text="Anuluj" class="btn btn-danger" OnClick="cancel_Click"/>
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </div>
    

    <div id="boxes">
        <div id="dialog" class="window">
            <asp:Label ID="nationalityLabel" runat="server" Text="Narodowosc" CssClass="label label-danger"></asp:Label>
            <asp:DropDownList ID="nationalityDDL" runat="server" CssClass="form-control" DataSourceID="NarodowoscSQL" DataValueField="Id_Narod" DataTextField="Narodowosc"></asp:DropDownList>
            <asp:SqlDataSource ID="NarodowoscSQL" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Narodowosc] , [Id_Narod] FROM [Narodowosc]"></asp:SqlDataSource>
            
            <asp:Label ID="sexLabel" runat="server" Text="Plec" CssClass="label label-danger"></asp:Label>   
            <asp:DropDownList ID="sexDDL" runat="server" CssClass="form-control" DataSourceID="sexSQL" DataTextField="Plec" DataValueField="Id_Plec"></asp:DropDownList>
            <asp:SqlDataSource ID="sexSQL" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Plec] , [Id_Plec] FROM [Plec]"></asp:SqlDataSource>
            
            <asp:Label ID="ageLabel" runat="server" Text="Przedzial Wiekowy" CssClass="label label-danger"></asp:Label>
            <asp:DropDownList ID="ageDDL" runat="server" CssClass="form-control" DataSourceID="ageSQL" DataTextField="Wiek" DataValueField="Id_Wiek"></asp:DropDownList>
            <asp:SqlDataSource ID="ageSQL" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Wiek] , [Id_Wiek] FROM [Wiek]"></asp:SqlDataSource>
            <div id="popupfoot"><a href="#" class="close agree">Anuluj</a> <asp:Button ID="Accept" class="btn btn-success" runat="server" Text="Akceptuj" OnClientClick="closeModal();" OnClick="Accept_Click"/> </div>
        </div>
        <div id="mask"></div>
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
                        <textarea class="form-control" rows="1" id="email" runat="server"></textarea>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" onclick="subscription_Click">Ok</button>
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
                        <h4 class="modal-title">Wysłano maile</h4>
                    </div>
                    <div class="modal-body">
                        <%
                            Response.Write("<p>Udało się wysłać  maili!!!</p>");
                             %>
                       
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                    </div>
                </div>
            </div>
        </div>
   
    <script>
        $('.window .btn btn-success').click(function (e) {
            //Cancel the link behavior
            e.preventDefault();

            $('#mask').hide();
            $('.window').hide();
        });
        var isPostBack = <%=Convert.ToString(Page.IsPostBack).ToLower()%>
        $(document).ready(function () {

            var id = '#dialog';

            //Get the screen height and width
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();
            if(isPostBack){
               
            } else {
                //Set heigth and width to mask to fill up the whole screen
                $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

                //transition effect
                $('#mask').fadeIn(500);
                $('#mask').fadeTo("slow", 0.9);

                //Get the window height and width
                var winH = $(window).height();
                var winW = $(window).width();

                //Set the popup window to center
                $(id).css('top', winH / 2 - $(id).height() / 2);
                $(id).css('left', winW / 2 - $(id).width() / 2);

                //transition effect
                $(id).fadeIn(2000);
            }
            

            //if close button is clicked
            $('.window .close').click(function (e) {
                //Cancel the link behavior
                e.preventDefault();

                $('#mask').hide();
                $('.window').hide();
            });

            //if mask is clicked
            $('#mask').click(function () {
                $(this).hide();
                $('.window').hide();
            });
            
        });
        
    </script>
    <script>
        var $selects = $('select');

        $selects.on('change', function () {

            // enable all options
            $selects.find('option').prop('disabled', false);

            // loop over each select, use its value to 
            // disable the options in the other selects
            $selects.each(function () {
                $selects.not(this)
                if(this.value!="--wybierz--"){
                    $selects.find('option[value="' + this.value + '"]')
                   .prop('disabled', true);  
                }else{
                    $selects.find('option[value="' + this.value + '"]')
                                       .prop('disabled', false);

                };
            });

        });
    </script>
    <style>
    #buttons {
    width: 50%;
    margin: 0 auto;
    }
    </style>
</asp:Content>
