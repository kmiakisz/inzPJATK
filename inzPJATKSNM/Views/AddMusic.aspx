﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMusic.aspx.cs" Inherits="inzPJATKSNM.Views.AddMusic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
     <script type="text/javascript">
         function play() {
             var audioValue = document.getElementById('<%=audioHidden.ClientID%>').value;
            alert(audioValue);
            embed = document.createElement("embed");
            embed.setAttribute("src", audioValue);
            embed.setAttribute("hidden", true);
            embed.setAttribute("autostart", true);
            embed.setAttribute("volume", 100);
            embed.setAttribute("type", audio/mp3);
            document.body.appendChild(embed);
        }
        window.onload = function () {
            play();
        }
        </script>
    <asp:FileUpload ID="MusicFileUpload" runat="server" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nie wybrano pliku z muzyką!" Display="Dynamic" Font-Bold="true" ForeColor="Red" ControlToValidate="MusicFileUpload"></asp:RequiredFieldValidator>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Wybrano plik o złym rozszerzeniu. Akceptowane formaty to .mp3 i .wav" ControlToValidate="MusicFileUpload" Font-Bold="true" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator1_ServerValidate" ></asp:CustomValidator>
    <br />
    <asp:Button ID="ZapiszButton" runat="server" Text="Zapisz" class="btn btn-danger" OnClick="ZapiszButton_Click"/>
    <asp:Button ID="AnulujButton" runat="server" Text="Anuluj" class="btn btn-danger" OnClick="AnulujButton_Click"/>
    
   
    
    <asp:HiddenField ID="audioHidden" runat="server" Value="C:\Users\Kubus\Source\Repos\inzPJATK\inzPJATKSNM\Music\01. Dwa Sławy - Przyjaciele dwa.mp3" />
</asp:Content>
