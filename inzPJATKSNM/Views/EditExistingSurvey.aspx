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
            $(".addPhoto").click(function (event) {

                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
                
                $.ajax({
                    url: '<%= ResolveUrl("EditExistingSurvey.aspx/AddPhoto") %>',
                    method: 'post',
                    contentType: 'application/json',
                    data: '{"url":' + link + ' }',
                    dataType: 'json',
                    success: function () {

                    },
                    error: function (er) {
                      
                    }
                });
            });
            $(".delete").click(function (event) {

                debugger;
                $("#" + event.target.id).css("background-color", "transparent");
              
                var link = Sys.Serialization.JavaScriptSerializer.serialize(event.target.name);
               
                $.ajax({
                    url: '<%= ResolveUrl("EditExistingSurvey.aspx/removePhotoFromSurvey") %>',
                    method: 'post',
                    contentType: 'application/json',
                    data: '{"url":' + link + ' }',
                    dataType: 'json',
                    success: function () {
                        location.reload();
                    },
                    error: function (er) {
                       
                    }
                });
            });
        
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
                        Alert(er)
                    }
                });
            });
          
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
           // photoFromDB = inzPJATKSNM.Controllers.NewSurveyController.getPhotoList();
            return new List<String>();
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
            <asp:Label ID="MusicLabel" runat="server" Text="Typ ankiety:" class="label label-danger"></asp:Label>
       <asp:DropDownList ID="TypeDropDownList" runat="server" class="form-control" Style="width: 80%">
                <asp:ListItem Text="PUBLICZNA" Value="PUBLIC" Enabled="true">PUBLICZNA</asp:ListItem>
                <asp:ListItem Text="PRYWATNA" Value="PRIVATE" Enabled="true">PRYWATNA</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label1" runat="server" Text="Wybierz date zakonczenia: " CssClass="label label-danger"></asp:Label>
            <input type="text" placeholder="click to show datepicker" id="example1" class="form-control" runat="server">
            <br />
            <asp:Label ID="Data_zakLAb" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Status: " CssClass="label label-danger"></asp:Label>
           
           
            <br />
            <br />
            <div id="Buttons" style="float: left; width: 56%">
                <asp:Button ID="AcceptButton" runat="server" Text="Zapisz" class="btn btn-success" Style="float: left" OnClick="AcceptButton_Click" />
                <asp:Button ID="CancelButton" runat="server" Text="Anuluj" class="btn btn-danger" Style="float: right" OnClick="CancelButton_Click" />
            </div>
            
        </div>
       <div class="demo" style="float: right; width: 40%">
      <%
          Response.Write("<h3>Zdjęcia w ankiecie</h3>");
           %>
            <ul id="lightSlider">
                <% 
                    foreach (inzPJATKSNM.Models.Dzieło dzielo in  getSurveyPhotos())
                    {
                        Response.Write("<li data-thumb=" + dzielo.URL + ">"
                            + " <div class=\"show-image\" id=" + dzielo.Id_dzieło + ">"
                            + " <img src=" + dzielo.URL + " />"
                            + " <input class=\"delete\" type=\"button\" value=\" \" onserverclick=\"AddToSurvey\" id=" + dzielo.Id_dzieło + " name =" + dzielo.URL + ">"
                            + " </div>"
                            + "</li>  " );
                      
                    }
                %>
            </ul>
            <br />
            
               <div >
               
               <h3>Dostępne zdjęcia</h3>
                       <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="KategorieDataSource" DataTextField="Kategoria" DataValueField="Id_Kat" OnTextChanged="kategoriaChanged" AutoPostBack="True" 
                    onselectedindexchanged="kategoriaChanged" AppendDataBoundItems="true">
                       <asp:ListItem Selected="True" Value="0">Wszystkie</asp:ListItem>
                   </asp:DropDownList>
                   <asp:SqlDataSource ID="KategorieDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Kat], [Kategoria] FROM [Kategoria]"></asp:SqlDataSource>
                   &nbsp<asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="TechnikaDataSource" DataTextField="Technika" DataValueField="Id_Tech" AutoPostBack="True" 
                    onselectedindexchanged="technikaChanged" AppendDataBoundItems="true">
                       <asp:ListItem Selected="True" Value="0">Wszystkie</asp:ListItem>
                   </asp:DropDownList>
                   <asp:SqlDataSource ID="TechnikaDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Tech], [Technika] FROM [Technika]"></asp:SqlDataSource>
                   &nbsp<asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="AutorDataSource" DataTextField="Nazwisko" DataValueField="Id_Autora" AutoPostBack="True" 
                    onselectedindexchanged="autorChanged" AppendDataBoundItems="true">
                       <asp:ListItem Selected="True" Value="0">Wszystkie</asp:ListItem>
                   </asp:DropDownList>
                   <asp:SqlDataSource ID="AutorDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Autora], [Nazwisko] FROM [Autor]"></asp:SqlDataSource>
          <br />
                    <% 
               Response.Write("<div class = \"row\">");
              
               foreach (inzPJATKSNM.Models.Dzieło dzielo in getFilteredPhoto())
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
    </div>
     <script type="text/javascript">
         function failOpenModal() {
             $('#failModal').modal('show');
         }
    </script>
       <div id="failModal"  class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button   type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Wystąpił błąd</h4>
                    </div>
                    <div class="modal-body">
                        <%
                            Response.Write("<p>"+Request.QueryString["err"]+"</p>");
                             %>                      
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
