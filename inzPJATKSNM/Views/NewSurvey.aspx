<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSurvey.aspx.cs" Inherits="inzPJATKSNM.Views.NewSurvey" %>
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
         });
</script>

   <div id="content" class="container-fluid">
       <h3><span class="label label-danger">Nowa ankieta</span></h3>
       <div id="left" style="float:left ; width:30%">
            <asp:Label ID="SurveyNameLabel" runat="server" Text="Nazwa Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="SurveyNameTextBox" runat="server" class="form-control"></asp:TextBox>
            <asp:Label ID="ServeyDescribtionLabel" runat="server" Text="Opis Ankiety" class="label label-danger"></asp:Label>
            <asp:TextBox ID="ServeyDescribtionTextBox" runat="server" class="form-control"></asp:TextBox>
            <asp:Label ID="MusicLabel" runat="server" Text="Wybierz muzykę: " class="label label-danger"></asp:Label>
            <asp:DropDownList ID="MusicDropDownList" runat="server" class="form-control" style="width:80%" DataSourceID="MusicDataSource" DataTextField="Tytul" DataValueField="Id_Muzyka">
                <asp:ListItem Text="--Wybierz--" Value="0" Enabled="true">dfg</asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="MusicDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Muzyka], [Tytul] FROM [Muzyka]"></asp:SqlDataSource>
            <br /><br />
            <div id="Buttons" style="float:left ; width:50%">
                <asp:Button ID="AcceptButton" runat="server" Text="Dodaj" class="btn btn-danger" style="float:left"/>
                <asp:Button ID="CancelButton" runat="server" Text="Anuluj" class="btn btn-danger" style="float:right"/>
           </div>
         
       </div>
       
           <div class="demo" style="float:right; width:40%">
               <ul id="lightSlider">
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-1.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-1.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-2.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-2.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-3.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-3.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-4.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-4.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-5.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-5.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-6.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-6.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-7.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-7.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-8.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-8.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-9.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-9.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-10.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-10.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-11.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-12.jpg" />
                   </li>
                   <li data-thumb="http://sachinchoolur.github.io/lightslider/img/thumb/cS-13.jpg">
                       <img src="http://sachinchoolur.github.io/lightslider/img/cS-13.jpg" />
                   </li>
               </ul>
</div>
       </div>

</asp:Content>
