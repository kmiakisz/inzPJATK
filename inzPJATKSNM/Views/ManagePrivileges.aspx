<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagePrivileges.aspx.cs" Inherits="inzPJATKSNM.Views.ManagePrivileges" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    s<link href="../Content/bootstrap.css" rel="stylesheet" /><link href="../Content/bootstrap.min.css" rel="stylesheet" /><div id="content" class="container-fluid">
        <br />
        <div id="left" style="float: left; width: 70%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="Label1" runat="server" Text="Posiadane uprawnienia." meta:resourcekey="Label1Resource1"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-hover table-striped" DataKeyNames="id_user_priviledges" meta:resourcekey="GridView1Resource1" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" meta:resourcekey="CommandFieldResource1" />
                            <asp:BoundField DataField="id_user_priviledges" HeaderText="id_user_priviledges" SortExpression="id_user_priviledges" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="usedId" HeaderText="usedId" SortExpression="usedId" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="userName" HeaderText="userName" SortExpression="userName" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="userSurname" HeaderText="userSurname" SortExpression="userSurname" meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField DataField="privilegeId" HeaderText="privilegeId" SortExpression="privilegeId" meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField DataField="privilegeName" HeaderText="privilegeName" SortExpression="privilegeName" meta:resourcekey="BoundFieldResource6" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="DeletePriv" SelectMethod="GetUserPrivilegePerId" TypeName="inzPJATKSNM.Controllers.UserController">
                        <DeleteParameters>
                            <asp:Parameter Name="id_user_priviledges" Type="Int32" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:QueryStringParameter Name="userId" QueryStringField="id" Type="Int32"/>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
            <div id="buttons">
                <asp:Button ID="BckBtn" runat="server" Text="Powrót" CssClass="btn btn-success" meta:resourcekey="BckBtnResource1" OnClick="BckBtn_Click" />
            </div>
        </div>
        <div id="right" style="float: right; width: 20%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="Label2" runat="server" Text="Dodaj uprawnienia." meta:resourcekey="Label2Resource1"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <div id="PrivilegeDDLDiv">
                        <asp:DropDownList ID="PrivilegeDDL" runat="server" CssClass="form-control" DataSourceID="ODS_AvailablePrivs" DataTextField="nazwa" DataValueField="uprawnienieId" meta:resourcekey="PrivilegeDDLResource1" AutoPostBack="True"></asp:DropDownList>
                        <asp:ObjectDataSource ID="ODS_AvailablePrivs" runat="server" SelectMethod="GetAvailablePrivById" TypeName="inzPJATKSNM.Controllers.UserController">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="userId" QueryStringField="id" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                    <div id="PrivilegeAddBtnDIV">
                        <asp:Button ID="PrivilegeAddBtn" runat="server" Text="Dodaj" CssClass="btn btn-success" OnClick="PrivilegeAddBtn_Click" meta:resourcekey="PrivilegeAddBtnResource1" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
