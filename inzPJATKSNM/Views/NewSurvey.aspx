<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSurvey.aspx.cs" Inherits="inzPJATKSNM.Views.NewSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Demo.css" rel="stylesheet" />
    <link href="../Content/lightSlider.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.1.4.js"></script>
    <script src="../Scripts/dropzone.js"></script>
    <script src="../Scripts/lightSlider.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#lightSlider').lightSlider({
                gallery: true,
                item: 1,
                loop: true,
                slideMargin: 0,
                thumbItem: 9
            });
            $(".update").click(function (event) {

                debugger;
                $("#" + event.target.id).css("background-color", "lightgreen");
                //if($("#"+event.target.id).data('clicked')){
                //  $(".show-image").click(function(event) {
                //       $("#"+event.target.id).css("border-color","green");
                // });

                //  }
                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
                $.ajax({
                    url: '<%= ResolveUrl("NewSurvey.aspx/addToPhotoToSurvey") %>',
                        method: 'post',
                        contentType: 'application/json',
                        data: '{"url":' + link + ' }',
                        dataType: 'json',
                        success: function () {
                        },
                        error: function (er) {
                            Alert("Zdarzył się potworny błąd!!!")
                        }
                    });
           });
            $(".update").dblclick(function (event) {
                $("#" + event.target.id).css("background-color", "transparent");
            });

        });

    </script>

    <script runat="server">
        protected List<String> GetList()
        {
            List<String> photoFromDB;
            photoFromDB = inzPJATKSNM.Controllers.NewSurveyController.getPhotoList();
            return photoFromDB;
        }
    </script>

    <div id="content" class="container-fluid">
        <h3><span class="label label-danger">Nowa ankieta</span></h3>
        <div id="left" style="float: left; width: 30%">
            <asp:Label ID="SurveyNameLabel" runat="server" Text="Nazwa Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="SurveyNameTextBox" runat="server" class="form-control" Text=""></asp:TextBox>
            <asp:Label ID="ServeyDescribtionLabel" runat="server" Text="Opis Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="ServeyDescribtionTextBox" runat="server" class="form-control" Text=""></asp:TextBox>
            <asp:DropDownList ID="TypeDropDownList" runat="server" class="form-control" Style="width: 80%">
                <asp:ListItem Text="PUBLICZNA" Value="PUBLIC" Enabled="true">PUBLICZNA</asp:ListItem>
                <asp:ListItem Text="PRYWATNA" Value="PRIVATE" Enabled="true">PRYWATNA</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <div id="Buttons" style="float: left; width: 56%">
                <asp:Button ID="AcceptButton" runat="server" Text="Dodaj" class="btn btn-danger" Style="float: left" OnClick="AcceptButton_Click" />
                <asp:Button ID="CancelButton" runat="server" Text="Anuluj" class="btn btn-danger" Style="float: right" OnClick="CancelButton_Click" />
            </div>

        </div>

        <div class="demo" style="float: right; width: 40%">

            <ul id="lightSlider">
                <% 
                    int x = 0;
                    foreach (String s in getSurveyPhotos())
                    {
                        Response.Write("<li data-thumb=" + s + ">"
                            + " <div class=\"show-image\" id=" + x + ">"
                            + " <img src=" + s + " />"
                            + " <input class=\"update\" type=\"button\" value=\" \" onserverclick=\"AddToSurvey\" id=" + x + " name =" + s + " />"
                            + " </div>"
                            + "</li>  ");

                        x++;
                    }
                %>
            </ul>
        </div>
        <br />
        <%
            foreach(String s in GetList()){
                Response.Write("<a href=\"" + s + ">"
                    + "<IMG HEIGHT=50 WIDTH=50 SRC=\"" + s + "\"></A>");
            }
             %>
    </div>
</asp:Content>
