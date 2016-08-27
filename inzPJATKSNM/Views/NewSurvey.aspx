<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSurvey.aspx.cs" Inherits="inzPJATKSNM.Views.NewSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Demo.css" rel="stylesheet" />
    <link href="../Content/Thumbnails.css" rel="stylesheet" />
    <link href="../Content/lightSlider.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.1.4.js"></script>
    <script src="../Scripts/dropzone.js"></script>
    <script src="../Scripts/lightSlider.js"></script>
    <script type="text/javascript">
        
        $(document).ready(function () {
            $(".addPhoto").click(function (event) {

                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
                alert(link);
                $.ajax({
                    url: '<%= ResolveUrl("NewSurvey.aspx/addPhoto") %>',
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
            $('#lightSlider').lightSlider({
                gallery: true,
                item: 1,
                loop: true,
                slideMargin: 0,
                thumbItem: 9
            });
            $(".update").click(function (event) {

                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
                $.ajax({
                    url: '<%= ResolveUrl("NewSurvey.aspx/removePhotoFromSurvey") %>',
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

    <div id="content" class="container-fluid">
        <h3><span class="label label-danger">Nowa ankieta</span></h3>
        <div id="left" style="float: left; width: 30%">
            <asp:Label ID="SurveyNameLabel" runat="server" Text="Nazwa Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="SurveyNameTextBox" runat="server" class="form-control" Text=""></asp:TextBox>
            <asp:Label ID="ServeyDescribtionLabel" runat="server" Text="Opis Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="ServeyDescribtionTextBox" runat="server" class="form-control" Text=""></asp:TextBox>
             <asp:Label ID="TypeLabel" runat="server" Text="Typ ankiety" class="label label-danger"></asp:Label>
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
      <%
          Response.Write("<h3>Zdjęcia w ankiecie</h3>");
           %>
            <ul id="lightSlider">
                <% 
                    foreach (inzPJATKSNM.Models.Dzieło dzielo in getSurveyPhotos().Values)
                    {
                        Response.Write("<li data-thumb=" + dzielo.URL + ">"
                            + " <div class=\"show-image\" id=" + dzielo.Id_dzieło + ">"
                            + " <img src=" + dzielo.URL + " />"
                            + " <input class=\"update\" type=\"button\" value=\" \" onserverclick=\"AddToSurvey\" id=" + dzielo.Id_dzieło + " name =" + dzielo.URL + " />"
                            + " </div>"
                            + "</li>  " );
                      
                    }
                %>
            </ul>
            <br />
               <div >
           <% 
               Response.Write("<h3>Dostępne zdjęcia</h3>");
               Response.Write("<div class = \"row\">");
               foreach (inzPJATKSNM.Models.Dzieło dzielo in getPhotoFromDB().Values)
               {
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\" width=100 height=100>");
                Response.Write("<input id=\"" + dzielo.URL + "\" name=\"" + dzielo.URL + "\" type=\"submit\" class=\"addPhoto\" runat=\"server\" style=\"background-color:transparent; border-color:transparent;\" onserverclick=\"addPhoto\">"); 
                Response.Write("<img src=\""+dzielo.URL+"\"/>");
                Response.Write("</input>");
                Response.Write("</div>");             
                Response.Write("</div>");
            }
            Response.Write("</div>");
        %>
        </div>
        </div>
               <script>
                  
    </script>
    </div>
</asp:Content>
        