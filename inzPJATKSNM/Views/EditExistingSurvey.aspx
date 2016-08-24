<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditExistingSurvey.aspx.cs" Inherits="inzPJATKSNM.Views.EditExistingSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Demo.css" rel="stylesheet" />
    <link href="../Content/lightslider.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.1.4.js"></script>
    <script src="../Scripts/lightslider.js"></script>
    <script src="../Scripts/datapicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#lightSlider').lightSlider({
                gallery: true,
                item: 1,
                loop: true,
                slideMargin: 0,
                thumbItem: 9
            });
            $(".delete").click(function (event) {

                debugger;
                $("#" + event.target.id).css("background-color", "transparent");
                //if($("#"+event.target.id).data('clicked')){
                //  $(".show-image").click(function(event) {
                //       $("#"+event.target.id).css("border-color","green");
                // });

                //  }
                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
                $.ajax({
                    url: '<%= ResolveUrl("NewSurvey.aspx/RemovePhotoFromSurvey") %>',
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
           // $(".update").dblclick(function (event) {
           //     $("#" + event.target.id).css("background-color", "transparent");
           // });

        });

    </script>
        <script type="text/javascript">
            $(document).ready(function () {

                $('#lightSlider2').lightSlider({
                    gallery: true,
                    item: 1,
                    loop: true,
                    slideMargin: 0,
                    thumbItem: 9
                });
                $(".update").click(function (event) {

                    debugger;
                    $("#" + event.target.id).css("background-color", "transparent");
                    //if($("#"+event.target.id).data('clicked')){
                    //  $(".show-image").click(function(event) {
                    //       $("#"+event.target.id).css("border-color","green");
                    // });

                    //  }
                    var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
                    $.ajax({
                        url: '<%= ResolveUrl("NewSurvey.aspx/AddPhotoToSurvey") %>',
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
           // $(".update").dblclick(function (event) {
            //    $("#" + event.target.id).css("background-color", "transparent");
           // });

        });

    </script>
    <script type="text/javascript">
        // When the document is ready
        $(document).ready(function () {

            $('#example1').datepicker({
                format: "dd/mm/yyyy"
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
      <script runat="server">
        protected void removeFromSurvey(int id,string url)
        {
            usedPhotos.Remove(id);
            freePhotos.Add(id, url);
        }
    </script>
      <script runat="server">
        protected void addToSurvey(int id,string url)
        {
            freePhotos.Remove(id);
            usedPhotos.Add(id, url);   
        }
    </script>
    <script runat="server">
        protected Dictionary<Int32,String> GetFreePhotos()
        {
            Dictionary<Int32, String> freePhotosFromDB;
            freePhotosFromDB = inzPJATKSNM.Controllers.EditExistingSurveyController.getFreePhotos();
            return freePhotosFromDB;
        }
    </script>
        <script runat="server">
        protected Dictionary<Int32,String> GetUsedPhotos()
        {
            int surveyId = int.Parse(Request.QueryString["Id"]);
            Dictionary<Int32, String> usedPhotosFromDB;
            usedPhotosFromDB = inzPJATKSNM.Controllers.EditExistingSurveyController.getUsedPhotos(surveyId);
            return usedPhotosFromDB;
        }
    </script>

    <div id="content" class="container-fluid">
        <h3><span class="label label-danger">Edycja Ankiety</span></h3>
        <div id="left" style="float: left; width: 30%">
            <asp:Label ID="SurveyNameLabel" runat="server" Text="Nazwa Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="SurveyNameTextBox1" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
            <asp:Label ID="ServeyDescribtionLabel" runat="server" Text="Opis Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="ServeyDescribtionTextBox1" runat="server" class="form-control"></asp:TextBox>
            <asp:Label ID="MusicLabel" runat="server" Text="Wybierz muzykę: " class="label label-danger"></asp:Label>
            <asp:DropDownList ID="MusicDropDownList" runat="server" class="form-control" Style="width: 80%" DataSourceID="MuzykaSDS" DataTextField="Tytul" DataValueField="Id_Muzyka">
                <asp:ListItem Text="--Wybierz--" Value="" Enabled="true">dfg</asp:ListItem> 
            </asp:DropDownList>
               <asp:DropDownList ID="TypeDropDownList" runat="server" class="form-control" Style="width: 80%" DataSourceID="SqlDataSource1" DataTextField="Typ" DataValueField="Typ"></asp:DropDownList>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Typ] FROM [Ankieta]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="MuzykaSDS" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Muzyka], [Tytul] FROM [Muzyka]"></asp:SqlDataSource>
            <asp:Label ID="Label1" runat="server" Text="Wybierz date zakonczenia: " CssClass="label label-danger"></asp:Label>
            <input type="text" placeholder="click to show datepicker" id="example1" class="form-control" runat="server">
            <br />
            <asp:Label ID="Data_zakLAb" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Status: " CssClass="label label-danger"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                <asp:ListItem>--Wybierz Stan --</asp:ListItem>
                <asp:ListItem Value="1">Aktywna</asp:ListItem>
                <asp:ListItem Value="0">Nieaktywna</asp:ListItem>
            </asp:DropDownList>
           
            <br />
            <br />
            <div id="Buttons" style="float: left; width: 56%">
                <asp:Button ID="AcceptButton" runat="server" Text="Zapisz" class="btn btn-success" Style="float: left" OnClick="AcceptButton_Click" />
                <asp:Button ID="CancelButton" runat="server" Text="Anuluj" class="btn btn-danger" Style="float: right" OnClick="CancelButton_Click" />
            </div>
            
        </div>
        <div id="right" style="float: right; width: 40%">
            <div class="demo">
                <ul id="lightSlider">
                    <% 
                     
                        int surveyId = int.Parse(Request.QueryString["Id"]);
                        foreach (Int32 key in usedPhotos.Keys)
                        {
                            currentKey = key;
                            currentValue = usedPhotos[key];
                            Response.Write("<li data-thumb=" + usedPhotos[key] + ">"
                                + " <div class=\"show-image\" id=" + key + ">"
                                + " <img src=" + usedPhotos[key] + " />"
                                //+ " <input class=\"update\" type=\"button\" value=\" \" onserverclick=\"AddToSurvey\" id=" + key + " name =" + usedPhotos[key] + " />"
                                + " <input class=\"delete\" type=\"button\" value=\" \" onserverclick=\"AddToSurvey\" name =" + key + ">"
                                + " </div>"
                                + "</li>  ");

                        }
                    %>
                </ul>
            </div>
            <div id="buttons" align="center">
                <asp:Button ID="Add_Button" runat="server" Text="Dodaj ↓ " CssClass="btn btn-success" OnClick="Add_Button_Click"/>
                <asp:Button ID="Del_Button" runat="server" Text="Usuń ↑ " CssClass="btn btn-danger" OnClick="Del_Button_Click"/>
            </div>
            <div class="demo">
                <ul id="lightSlider2">
                    <% 
                        int x_1 = 0;
                        foreach (String s in freePhotos.Values)
                        {

                            Response.Write("<li data-thumb=" + s + ">"
                                + " <div class=\"show-image\" id=" + x_1 + ">"
                                + " <img src=" + s + " />"
                                + " <input class=\"update\" type=\"button\" value=\" \" onserverclick=\"AddToSurvey\" id=" + x_1 + " name =" + s + " />"
                                + " </div>"
                                + "</li>  ");

                                x_1++;

                        }
                    %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
