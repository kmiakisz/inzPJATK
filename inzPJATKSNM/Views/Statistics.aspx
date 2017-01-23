<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="inzPJATKSNM.Views.Statistics" meta:resourcekey="PageResource1" %>

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
                        <asp:Label ID="HeaderLabel" runat="server" Text="Statystyki Ogólne." ForeColor="White" meta:resourcekey="HeaderLabelResource1"></asp:Label>
                        <!--<p id="header"; style="font-style: normal; color: white">Statystyki Ogólne.</p>-->
                    </div>
                    <div class="panel-body">
                        <div id="1">
                            <asp:Label ID="Lbl1" runat="server" CssClass="label label-default" meta:resourcekey="Lbl1Resource1"></asp:Label>
                        </div>
                        <div id="2">
                            <asp:Label ID="Lbl2" runat="server" CssClass="label label-default" meta:resourcekey="Lbl2Resource1"></asp:Label>
                        </div>
                        <div id="3">
                            <asp:Label ID="Lbl3" runat="server" CssClass="label label-default" meta:resourcekey="Lbl3Resource1"></asp:Label>
                        </div>
                        <div id="4">
                            <asp:Label ID="Lbl4" runat="server" CssClass="label label-default" meta:resourcekey="Lbl4Resource1"></asp:Label>
                        </div>
                        <div id="5">
                            <asp:Label ID="Lbl5" runat="server" CssClass="label label-default" meta:resourcekey="Lbl5Resource1"></asp:Label>
                        </div>
                        <div id="6">
                            <asp:Label ID="Lbl6" runat="server" CssClass="label label-default" meta:resourcekey="Lbl6Resource1"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div id="thumb" runat="server">
                    <% 
                        inzPJATKSNM.Controllers.Statistic stat = inzPJATKSNM.Views.Statistics.FillThumbnails();
                        String name, url;
                        if (stat.NumOfVisitors != 0)
                        {
                            Response.Write("<div class = \"row\">");

                            name = stat.ImgMaxVoteNumName;
                            url = stat.ImgMaxVoteNumUrl;

                            Response.Write("<p>Dzieło z nawyższą średnią oceną</p>");
                            Response.Write("<div class=\"col-sm-1 col-md-2\">");
                            Response.Write("<div class=\"thumbnail\">");
                            Response.Write("<img src=\"" + url + "\" alt=\" " + name + "\">");
                            Response.Write("</div>");
                            Response.Write("<div class=\"caption\">");
                            Response.Write("<h3>" + name + "</h3>");
                            Response.Write("</div></div>");


                            Response.Write("</div>");
                        } 
                    %>
                </div>

                <div>
                    <asp:Button ID="BackButton" runat="server" Text="Powrót do statystyk" CssClass="btn btn-success" OnClick="BackButton_Click" meta:resourcekey="BackButtonResource1" />
                </div>
            </div>
            <div id="right" style="float: right; width: 40%">
                <asp:Button ID="StatButton" runat="server" Text="Pokaż statystyki szczegółowe" CssClass="btn btn-success" OnClick="StatButton_Click" meta:resourcekey="StatButtonResource1" />
                <asp:Chart ID="Chart1" runat="server" DataSourceID="ObjectDataSource1" CssClass="form-control" meta:resourcekey="Chart1Resource1">
                    <Series>
                        <asp:Series Name="Series1" ChartType="StackedBar" XValueMember="photoName" YValueMembers="mark"></asp:Series>
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
