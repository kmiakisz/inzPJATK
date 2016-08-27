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
        <script type="text/javascript">
            function toMuchPhotosModal() {
                $('#toMuchPhotosModal').modal('show');
            }
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
            <div id="SurveyName">
                <asp:Label ID="SurveyNameLabel" runat="server" Text="Nazwa Ankiety" class="label label-danger"></asp:Label>
                <asp:TextBox ID="SurveyNameTextBox" runat="server" class="form-control" Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nazwa Ankiety nie może być pusta!" ControlToValidate="SurveyNameTextBox" ForeColor="Red" Font-Bold="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div id="SurveyDescribtion">
                <asp:Label ID="ServeyDescribtionLabel" runat="server" Text="Opis Ankiety" class="label label-danger"></asp:Label>
                <asp:TextBox ID="ServeyDescribtionTextBox" runat="server" class="form-control" Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Opis ankiety nie może być pusty!" ControlToValidate="ServeyDescribtionTextBox" ForeColor="Red" Font-Bold="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div id="SurveyMusic">
                <asp:Label ID="MusicLabel" runat="server" Text="Wybierz muzykę: " class="label label-danger"></asp:Label>
                <asp:DropDownList ID="MusicDropDownList" runat="server" class="form-control" Style="width: 80%" DataSourceID="MusicDataSource" DataTextField="Tytul" DataValueField="Id_Muzyka">
                    <asp:ListItem Text="--Wybierz--" Value="0" Enabled="true">dfg</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div id="SurveyType">
                <asp:Label ID="TypeLabel" runat="server" Text="Typ Ankiety" class="label label-danger"></asp:Label>
                <asp:DropDownList ID="TypeDropDownList" runat="server" class="form-control" Style="width: 80%">
                    <asp:ListItem Text="PUBLICZNA" Value="PUBLIC" Enabled="true">PUBLICZNA</asp:ListItem>
                    <asp:ListItem Text="PRYWATNA" Value="PRIVATE" Enabled="true">PRYWATNA</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:SqlDataSource ID="MusicDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Muzyka], [Tytul] FROM [Muzyka]"></asp:SqlDataSource>
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
                    foreach (String s in GetList())
                    {

                        Response.Write("<li data-thumb=" + s + ">"
                            + " <div class=\"show-image\" id=" + x + ">"
                            + " <img src=" + s + " />"
                            + " <input class=\"update\" type=\"button\" value=\" \" onserverclick=\"AddToSurvey\" id=" + x + " name =" + s + " />"
                            + " </div>"
                            + "</li>  ");

                        x++;
                        if (x > 10) //walidacja na ilosc zdjec w ankiecie.
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "toMuchPhotosModal();", true);  
                        }

                    }
                %>
            </ul>



        </div>
               <div id="toMuchPhotosModal"  class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button   type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 class="modal-title">Wystąpił błąd!</h3>
                    </div>
                    <div class="modal-body">
                        <h5 class="modal-title">Limit zdjęć w ankiecie - 10, został przekroczony!</h5>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>
