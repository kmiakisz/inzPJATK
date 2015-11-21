<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowSurveys.aspx.cs" Inherits="inzPJATKSNM.Views.ShowSurveys" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <br />
    <br />

    <% 
     Response.Write("<div class = \"row\">");
            foreach(KeyValuePair<int, String> kvp in getURLDict()){
               String nazwa = getNazwyDict()[kvp.Key];
               String opis = getOpisyDict()[kvp.Key];
                Response.Write("<div class=\"col-sm-6 col-md-3\">");
                Response.Write("<div class=\"thumbnail\">");
                Response.Write("<img src=\""+kvp.Value+"\" alt=\" "+ nazwa +"\">");
                Response.Write("</div>");
                Response.Write("<div class=\"caption\">");   
                Response.Write("<h3>" +nazwa +"</h3>");        
                Response.Write("<p>"+opis+"</p>");
                Response.Write("<p> <a href=\"#\" class=\"btn btn-danger\" role=\"button\">Usuń </a> <a href=\"EditExistingSurvey.aspx\" class=\"btn btn-success\" role=\"button\">Edytuj</a></p></div></div>");   
            }
                Response.Write("</div>");
   
        %>
</asp:Content>
