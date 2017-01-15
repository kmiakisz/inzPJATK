<%@ Page Title="" Language="C#" MasterPageFile="~/VoteMaster.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="inzPJATKSNM.Views.StartPage" runat="server" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Content/modal.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script>
    <script src="../Scripts/jquery.fullscreenslides.js"></script>
    <link href="../Content/fullscreenstyle.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function subscriptionOpenModal() {
            $('#mailModal').modal('show');
        }
    </script>
    <script type="text/javascript">
        function mailOpenModal() {
            $('#subscriptionModal').modal('show');
        }
    </script>
    <%
        Response.Write("<br/><br/>");
        Response.Write("<h2>" + inzPJATKSNM.Controllers.SurveyController.getSurveyName(int.Parse(Request.QueryString["Id"])) + "</h2>");
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
                Response.Write("<a href=\"" + dzielo.URL + "\" >");
                Response.Write("<img src=\"" + dzielo.URL + "\">");
                Response.Write("</a>");
                Response.Write("</div></div>");
                Response.Write("<div class=\"caption\">");
                Response.Write("<h3>" + inzPJATKSNM.Controllers.SurveyController.getKategoria(dzielo.Id_Kat) + "</h3>");
                Response.Write("<p>" + inzPJATKSNM.Controllers.SurveyController.getTechnika(dzielo.Id_Tech) + "</p>");
                Response.Write("<p>" + inzPJATKSNM.Controllers.SurveyController.getAutor(dzielo.Id_Autora) + "</p>");
                Response.Write("<p>Ocena: </p><select onChange=\"removeSameValue(" + dzielo.Id_dzieło + ")\" id=\"" + dzielo.Id_dzieło + "\" name =\"" + dzielo.Id_dzieło + "\" class=\"form-control\">");
                Response.Write("<option value=\"--wybierz--\">--wybierz--</option>");
                for (int i = 1; i <= dziela.Count; i++)
                {
                    int j = i + 1;
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
                nameValue += this.name + ',' + this.value + ';';

            });
            document.getElementById('<%=TextBox1.ClientID %>').value = nameValue;
        }
    </script>
    <br />
    <br />
    <div id="buttons">
        <asp:Button ID="vote" runat="server" Text="Zagłosuj" class="btn btn-success" OnClick="vote_Click" OnClientClick="getOceny()" meta:resourcekey="voteResource1" />
        <asp:TextBox ID="TextBox1" name="TextBox1" runat="server" ClientIDMode="Static" Style="display: none;" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
        &nbsp
        <asp:Button ID="cancel" runat="server" Text="Anuluj" class="btn btn-danger" OnClick="cancel_Click" meta:resourcekey="cancelResource1" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </div>


    <div id="boxes">
        <div id="dialog" class="window">
            <asp:Label ID="nationalityLabel" runat="server" Text="Narodowosc" CssClass="label label-danger" meta:resourcekey="nationalityLabelResource1"></asp:Label>
            <asp:DropDownList ID="DropDownList_Narod" runat="server" DataSourceID="SqlDataSource1" DataTextField="narodowosc" DataValueField="id_narod" CssClass="form-control"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="select narodowosc,id_narod from Narodowosc"></asp:SqlDataSource>

            <asp:Label ID="sexLabel" runat="server" Text="Plec" CssClass="label label-danger" meta:resourcekey="sexLabelResource1"></asp:Label>
            <asp:DropDownList ID="DropDownList_Sex" runat="server" DataSourceID="SqlDataSource2" DataTextField="plec" DataValueField="id_plec" CssClass="form-control"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="select plec,id_plec from Plec"></asp:SqlDataSource>

            <asp:Label ID="ageLabel" runat="server" Text="Przedzial Wiekowy" CssClass="label label-danger" meta:resourcekey="ageLabelResource1"></asp:Label>
            <asp:DropDownList ID="DropDownList_Age" DataSourceID="SqlDataSource3" DataTextField="wiek" DataValueField="id_wiek" runat="server" CssClass="form-control"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="select wiek,id_wiek from Wiek"></asp:SqlDataSource>

            <div class="languages">
                <br />
                <asp:ImageButton ID="EngButton" runat="server" ImageUrl="~/Images/eng.png" OnClick="EngButton_Click" />
                <asp:ImageButton ID="PolButton" runat="server" ImageUrl="~/Images/pol.jpg" OnClick="PolButton_Click" />
            </div>

            <div id="popupfoot">
                <a href="Surveys.aspx" class="close agree">Anuluj</a>
                <asp:Button ID="Accept" class="btn btn-success" runat="server" Text="Akceptuj" OnClientClick="closeModal();" OnClick="Accept_Click" meta:resourcekey="AcceptResource1" />
            </div>
        </div>
        <div id="mask"></div>
    </div>
    <!-- subscriptionModal -->
    <div id="subscriptionModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Dziękujemy!</h4>
                </div>
                <div class="modal-body">
                    <p>Dziękujemy za uzupełnienie ankiety.</p>
                    <br />
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
                        Response.Write("<p>Udało się wysłać  maili!!!</p>");
                    %>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <script type="text/javascript">
        function starVote() {
            console.log('jestem tu ludzi tlum');
            $(document).ready(function () {
                console.log('a wszystko takie dziwne');
                var id = '#dialog';
                //Get the screen height and width
                var maskHeight = $(document).height();
                var maskWidth = $(window).width();
                //if (isPostBack) {

                //} else {
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
                // }


                //if close button is clicked redirect to site with 
                $('.window .close').click(function (e) {
                    //Cancel the link behavior
                    //e.preventDefault();
                    $('#mask').hide();
                    $('.window').hide();
                });

                //if mask is clicked
                /*$('#mask').click(function () {
                    $(this).hide();
                    $('.window').hide();
                });*/
            });
        };
    </script>
    <script>//information about voter
        $('.window .btn btn-success').click(function (e) {
            //Cancel the link behavior
            e.preventDefault();
            console.log('halo, czy ja tu wgl wchodze?');
            $('#mask').hide();
            $('.window').hide();
        });

        /* $(document).ready(function () {
 
             var id = '#dialog';
                 //Get the screen height and width
                 var maskHeight = $(document).height();
                 var maskWidth = $(window).width();
                 if (isPostBack) {
 
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
         });*/
        console.log('wchodze i wychodze');
    </script>
    <script>
        var $selects = $('.row select');
        console.log('teraz jestem tutaj');
        $selects.on('change', function () {

            // enable all options
            $selects.find('option').prop('disabled', false);

            // loop over each select, use its value to 
            // disable the options in the other selects
            $selects.each(function () {
                console.log('niby disabluje 1');
                $selects.not(this)
                if (this.value != "--wybierz--") {
                    $selects.find('option[value="' + this.value + '"]')
                   .prop('disabled', true);
                    console.log('jestem sobie w ifie' + this.value);
                } else {
                    $selects.find('option[value="' + this.value + '"]')
                                       .prop('disabled', false);
                    console.log('a teraz jestem w elsie');
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
