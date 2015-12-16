<%@ Page Title="" Language="C#" MasterPageFile="~/VoteMaster.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="inzPJATKSNM.Views.StartPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Content/modal.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script> 
    <%
        Response.Write("<h1>" + inzPJATKSNM.Controllers.SurveyController.getSurveyName(int.Parse(Request.QueryString["Id"])) + "</h1>");
    %>
   

   <% 
     Response.Write("<div class = \"row\">");
     if (dziela != null)
     {
         foreach (inzPJATKSNM.Models.Dzieło dzielo in dziela)
         {
             Response.Write("<div class=\"col-sm-6 col-md-3\">");
             Response.Write("<div class=\"thumbnail\">");
             Response.Write("<img src=\"" + dzielo.URL + "\">");
             Response.Write("</div>");
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
      <div id="boxesPow">
        <div id="dialogPow" class="window">
    
        </div>
        <div id="maskPow"></div>
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
