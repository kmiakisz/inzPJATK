<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministratorPanel.aspx.cs" Inherits="inzPJATKSNM.Views.AdministratorPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <br />
    <div id="content" class="container-fluid">
        <div class="row-md-2">
            <div class="col-sm-6 col-md-3">
                <asp:Button ID="Button1" runat="server" Text="Button"  class="btn btn-default" Height="100px" Width="100px"/>
                <br />
                </div>
            <div class="col-sm-6 col-md-3">
                <asp:Button ID="Button2" runat="server" Text="Button" class="btn btn-primary" Height="100px" Width="100px"/>
                <br />
                </div>
            <div class="col-sm-6 col-md-3">
                <asp:Button ID="Button3" runat="server" Text="Button" class="btn btn-success" Height="100px" Width="100px"/>
                <br />
                </div>
        </div>
        <div class="row-md-2">
            <div class="col-sm-6 col-md-3">
                <asp:Button ID="Button4" runat="server" Text="Button" class="btn btn-info" Height="100px" Width="100px"/>
                <br />
                </div>
            <div class="col-sm-6 col-md-3">
                <asp:Button ID="Button5" runat="server" Text="Button" class="btn btn-warning" Height="100px" Width="100px"/>
                <br />
                </div>
            <div class="col-sm-6 col-md-3">
                <asp:Button ID="Button6" runat="server" Text="Button" class="btn btn-danger" Height="100px" Width="100px"/>
                <br />
                </div>
        </div>
    </div>
 
</asp:Content>
