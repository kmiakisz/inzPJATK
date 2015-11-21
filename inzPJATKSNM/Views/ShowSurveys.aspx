<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowSurveys.aspx.cs" Inherits="inzPJATKSNM.Views.ShowSurveys" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <br />
    <br />
    <asp:GridView ID="GridViewSurveys" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id_ankiety" DataSourceID="SqlDataSourceSurveys2" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnSelectedIndexChanged="GridViewSurveys_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button" />
            <asp:BoundField DataField="Nazwa" HeaderText="Nazwa" SortExpression="Nazwa" />
            <asp:BoundField DataField="Opis_ankiety" HeaderText="Opis_ankiety" SortExpression="Opis_ankiety" />
            <asp:BoundField DataField="Data_rozp" HeaderText="Data_rozp" SortExpression="Data_rozp" />
            <asp:BoundField DataField="Data_zak" HeaderText="Data_zak" SortExpression="Data_zak" />
            <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceSurveys2" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_ankiety], [Nazwa], [Opis_ankiety], [Data_rozp], [Data_zak], [Active] FROM [Ankieta] ORDER BY [Data_rozp] DESC"></asp:SqlDataSource>

</asp:Content>
