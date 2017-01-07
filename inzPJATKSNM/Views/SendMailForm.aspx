<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendMailForm.aspx.cs" Inherits="inzPJATKSNM.Views.SendMailForm" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" runat="server" id="row">
        <div class="col-lg-6">
            <table id="tab2" class="table table-striped">
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
                <a href="HomePage.aspx" class="close agree">Anuluj</a>
                <asp:Button ID="Accept" class="btn btn-success" runat="server" Text="Akceptuj" OnClientClick="closeModal();" meta:resourcekey="AcceptResource1" />
                <%-- OnClick="Accept_Click" --%>
            </div>

        </div>
        <div id="mask"></div>
    </div>
    <div id="horizon">
        <br />
        <div id="content" class="container-fluid">
            <div>
                <asp:CheckBox ID="AllMailList" runat="server" OnCheckedChanged="AllMailList_CheckedChanged" meta:resourcekey="AllMailListResource1" />
                <asp:Label ID="Label1" runat="server" Text="Listy Grup Studentów" CssClass="label label-danger" meta:resourcekey="Label1Resource1"></asp:Label>
                <div id="mailgroup">
                    <asp:ListBox ID="AllMailListLst" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="Grupa" DataValueField="Grupa" OnDataBound="AllMailListLst_DataBound" OnSelectedIndexChanged="AllMailListLst_SelectedIndexChanged" SelectionMode="Multiple" meta:resourcekey="AllMailListLstResource1"></asp:ListBox>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Grupa] FROM [Grupa_Mail]"></asp:SqlDataSource>
                </div>
            </div>
            <div>
                <asp:CheckBox ID="SubsMailList" runat="server" meta:resourcekey="SubsMailListResource1" />
                <asp:Label ID="Label2" runat="server" Text="Lista Subskrybentów" CssClass="label label-danger" meta:resourcekey="Label2Resource1"></asp:Label>
                <div id="subsMail">
                    <asp:ListBox ID="SubsMailListLst" runat="server" CssClass="form-control" DataSourceID="SqlDataSource2" DataTextField="Email" DataValueField="Email" OnSelectedIndexChanged="SubsMailListLst_SelectedIndexChanged" SelectionMode="Multiple" meta:resourcekey="SubsMailListLstResource1"></asp:ListBox>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT Email FROM Glosujący WHERE Email IS NOT NULL;"></asp:SqlDataSource>
                </div>
            </div>
            <div>
                <asp:CheckBox ID="CustomMail" runat="server" meta:resourcekey="CustomMailResource1" />
                <asp:Label ID="CustomLbl" runat="server" Text="Własny Mail -> (maile wpisujemy odzielając je znakiem średnika - ';')" CssClass="label label-danger" meta:resourcekey="CustomLblResource1"></asp:Label>
                <div id="customMail">
                    <asp:TextBox ID="CustomMailTxt" runat="server" CssClass="form-control" OnTextChanged="CustomMailTxt_TextChanged" Style="width: 400px" meta:resourcekey="CustomMailTxtResource1"></asp:TextBox>
                </div>
            </div>
        </div> 
        <div id="button">
            <asp:Button ID="SendMailButton" runat="server" Text="Wyślij" CssClass="btn btn-success" OnClick="SendMailButton_Click" meta:resourcekey="SendMailButtonResource1" />
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
