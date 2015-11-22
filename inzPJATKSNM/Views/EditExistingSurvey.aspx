﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditExistingSurvey.aspx.cs" Inherits="inzPJATKSNM.Views.EditExistingSurvey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <link href="../Content/Demo.css" rel="stylesheet" />
    <link href="../Content/lightslider.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.1.4.js"></script>
    <script src="../Scripts/lightslider.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
           
            $('#lightSlider').lightSlider({
                gallery: true,
                item: 1,
                loop: true,
                slideMargin: 0,
                thumbItem: 9
            });
           $(".update").click(function(event) {
                
                debugger;
                $("#"+event.target.id).css("background-color", "lightgreen");
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
                        data: '{"url":'+ link + ' }',
                        dataType: 'json',
                        success: function(){

                        },
                        error: function(er){
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
       <h3><span class="label label-danger">Edycja Ankiety</span></h3>
       <div id="left" style="float:left ; width:30%">
            <asp:Label ID="SurveyNameLabel" runat="server" Text="Nazwa Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="SurveyNameTextBox1" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
            <asp:Label ID="ServeyDescribtionLabel" runat="server" Text="Opis Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="ServeyDescribtionTextBox1" runat="server" class="form-control"></asp:TextBox>
            <asp:Label ID="MusicLabel" runat="server" Text="Wybierz muzykę: " class="label label-danger"></asp:Label>
            <asp:DropDownList ID="MusicDropDownList" runat="server" class="form-control" style="width:80%" DataSourceID="MusicDataSource" DataTextField="Tytul" DataValueField="Id_Muzyka">
                <asp:ListItem Text="--Wybierz--" Value="0" Enabled="true">dfg</asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="MusicDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Muzyka], [Tytul] FROM [Muzyka]"></asp:SqlDataSource>
            <br /><br />
            <div id="Buttons" style="float:left; width:56%">
                <asp:Button ID="AcceptButton" runat="server" Text="Zapisz" class="btn btn-success" style="float:left" OnClick="AcceptButton_Click"/>
                <asp:Button ID="CancelButton" runat="server" Text="Anuluj" class="btn btn-danger" style="float:right" OnClick="CancelButton_Click"/>
           </div>
         
       </div>
       
       <div class="demo" style="float: right; width: 40%">
           <!--
           <li data-thumb="http://www.adrenalinemotorsport.pl/photos/aktualnosci/37163.jpg">
            <div class="show-image">
                <img src="http://www.adrenalinemotorsport.pl/photos/aktualnosci/37163.jpg" />
                <input class="update" type="button" value=" " id="updateBtn" onclick="AddToSurvey" /> 
            </div>
        </li>
           -->
           <ul id="lightSlider">
               <% 
                   int x = 0;
                   foreach(String s in GetList())
                  {
                      
                      Response.Write("<li data-thumb=" + s + ">" 
                          +" <div class=\"show-image\" id=" + x +">"
                          +" <img src=" + s + " />"
                          + " <input class=\"update\" type=\"button\" value=\" \" onserverclick=\"AddToSurvey\" id=" + x + " name ="  + s  +  " />"
                          +" </div>"
                          + "</li>  ");

                      x++;
                      
                  }
               %>
           </ul>



       </div>
       
</div>
</asp:Content>
