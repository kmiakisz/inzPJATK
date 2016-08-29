<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="inzPJATKSNM.Views.Statistics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/bootstrap.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/jquery-2.1.4.js"></script>
    <div id="horizon">
        <br />
        <div id="content" class="container-fluid">
            <div id="left" style="float: left; width: 30%">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <p style="font-style: normal; color: white">Statystyki Ogólne.</p>
                    </div>
                    <div class="panel-body">
                        <div id="1">
                            <asp:Label ID="Lbl1" runat="server" Text="" CssClass="label label-default"></asp:Label>
                        </div>
                        <div id="2">
                            <asp:Label ID="Lbl2" runat="server" Text="" CssClass="label label-default"></asp:Label>
                        </div>
                        <div id="3">
                            <asp:Label ID="Lbl3" runat="server" Text="" CssClass="label label-default"></asp:Label>
                        </div>
                        <div id="4">
                            <asp:Label ID="Lbl4" runat="server" Text="" CssClass="label label-default"></asp:Label>
                        </div>
                        <div id="5">
                            <asp:Label ID="Lbl5" runat="server" Text="" CssClass="label label-default"></asp:Label>
                        </div>
                        <div id="6">
                            <asp:Label ID="Lbl6" runat="server" Text="" CssClass="label label-default"></asp:Label>
                        </div>
                    </div>
                </div>
                <div>
                    <asp:Button ID="BackButton" runat="server" Text="Powrót do statystyk" CssClass="btn btn-success" OnClick="BackButton_Click" />
                </div>
            </div>
            <div id="right" style="float: right; width: 40%">
                <asp:Button ID="StatButton" runat="server" Text="Pokaż statystyki szczegółowe" CssClass="btn btn-success" OnClick="StatButton_Click" />
                <asp:Chart ID="Chart1" runat="server" DataSourceID="ObjectDataSource1" CssClass="form-control">
                    <Series>
                        <asp:Series Name="Series1" ChartType="StackedBar" XValueMember="photoId" YValueMembers="mark"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="FillChart" TypeName="inzPJATKSNM.Views.Statistics"></asp:ObjectDataSource>
            </div>
        </div>
    </div>
</asp:Content>
