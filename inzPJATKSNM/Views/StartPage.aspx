<%@ Page Title="" Language="C#" MasterPageFile="~/VoteMaster.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="inzPJATKSNM.Views.StartPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Content/modal.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script> 
    <script src="../Scripts/jquery.fullscreenslides.js"></script>
    <link href="../Content/fullscreenstyle.css" rel="stylesheet" />
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
             Response.Write("<p><asp:DropDownList cssClass=\"form-control\" runat=\"server\"></asp:DropDownList>  </p></div></div>");
         }
         Response.Write("</div>");
     }
     else
     {
         //modal o pustych zdjeciach
     }
     
   
        %>
   

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
</asp:Content>
