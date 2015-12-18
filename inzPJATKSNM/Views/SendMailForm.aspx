<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendMailForm.aspx.cs" Inherits="inzPJATKSNM.Views.SendMailForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
   
    <div class="row" runat="server" id="row">
        <div class="col-lg-6">
            <table ID="tab2"  class="table table-striped"> 
                <%
                    int ID = 0;
                    foreach (String mail in inzPJATKSNM.Controllers.MailController.getMailList())
                    {
                    Response.Write(
                         "<asp:TableRow>"
                        + "<asp:TableCell>"
                        + "<asp:CheckBox ID=\"mailCheckBox" + ID + "\"" + " runat=\"server\" CssClass=\"form-control\" />"
                        + "</asp:TableCell"
                        + "<asp:TableCell>"
                        + "<asp:TextBox ID=\"mailTextBox" + ID + "\"" + " runat=\"server\" CssClass=\"form-control\" Text=\"" + mail + "\"" + " />"
                        + "</asp:TableCell>"
                        + "</asp:TableRow>"
                        );
                    ID++;
                    }
                %>
            </table>
            
        </div>
    </div>

    <link href="../Content/modal.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

    <div id="boxes">
        <div id="dialog" class="window">
            <div class="form-group">
                <label for="comment"><span class="glyphicon glyphicon-envelope"></span>TEMAT:</label>
                <textarea class="form-control" rows="1" id="subject" runat="server"></textarea>
            </div>
            <div class="form-group">
                <label for="comment">TRESC:</label>
                <textarea class="form-control" rows="5" id="body" runat="server"></textarea>
            </div>
            <div id="popupfoot">
                <a href="#" class="close agree">Anuluj</a>
                <asp:Button ID="Accept" class="btn btn-success" runat="server" Text="Akceptuj" OnClick="Accept_Click" OnClientClick="closeModal();" />
            </div>
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

        });

    </script>
</asp:Content>
