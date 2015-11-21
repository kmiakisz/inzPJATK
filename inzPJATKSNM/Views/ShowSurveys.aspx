<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowSurveys.aspx.cs" Inherits="inzPJATKSNM.Views.ShowSurveys" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <br />
    <br />

    <% 
            int ilAnkiet = liczbaAnkiet;
            foreach(KeyValuePair<int, String> kvp in getURLDict()){
               String nazwa = getNazwyDict()[kvp.Key];
               String opis = getOpisyDict()[kvp.Key];
            }
       
    <div class = "row">
   
        <div class="col-sm-6 col-md-3">
            <div class="thumbnail">
                <img src="../Images/SurveyPhotos/20150816_114132.jpg" alt="Generic placeholder thumbnail">
           </div>

            <div class="caption">
                <h3>Thumbnail label</h3>
                <p>Some sample text. Some sample text.</p>

                <p>
                    <a href="#" class="btn btn-danger" role="button">Usuń
                    </a>

                    <a href="#" class="btn btn-success" role="button">Edytuj
                    </a>
                </p>

            </div>
        </div>
        </div>
        %>
</asp:Content>
