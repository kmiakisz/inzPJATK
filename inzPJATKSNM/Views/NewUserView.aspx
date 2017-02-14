<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewUserView.aspx.cs" Inherits="inzPJATKSNM.Views.NewUserView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function errObjModal() {
            $('#errModal').modal('show');
        }
    </script>
    <div id="content" class="container-fluid">
        <br />
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">Dane nowego użytkownika</h3>
            </div>
            <div class="panel-body">
                <div id="email">
                    <asp:Label ID="EmailLbl" runat="server" Text="Email" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="EmailTxt" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="EmailTxt" runat="server" ErrorMessage="Pole email jest wymagane!" ForeColor="Red" Font-Bold="True" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
                <div id="name">
                    <asp:Label ID="ImięLbl" runat="server" Text="Imię" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="NameTxt" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="NameTxt" runat="server" ErrorMessage="Pole imię jest wymagame!" ForeColor="Red" Font-Bold="True" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
                <div id="surname">
                    <asp:Label ID="NazwiskoLbl" runat="server" Text="Nazwisko" CssClass="label label-danger"></asp:Label>
                    <asp:TextBox ID="SurnameTxt" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="SurnameTxt" runat="server" ErrorMessage="Pole nazwisko jest wymagane!" ForeColor="Red" Font-Bold="True" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
                <div id="role">
                    <asp:Label ID="RolaLbl" runat="server" Text="Rola" CssClass="label label-danger"></asp:Label>
                    <asp:DropDownList ID="RoleDDL" runat="server" CssClass="form-control" DataSourceID="RoleDataSource" DataTextField="ROLE_NAME" DataValueField="ID_ROLE"></asp:DropDownList>
                    <asp:SqlDataSource ID="RoleDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [ID_ROLE], [ROLE_NAME] FROM [SNM_ROLE]"></asp:SqlDataSource>
                </div>
            </div>
        </div>
        <div id="acceptbutton">
            <asp:Button ID="AcceptButton" runat="server" Text="Dodaj" CssClass="btn btn-success" OnClick="AcceptButton_Click" ValidationGroup="A" />
        </div>
    </div>
    <div id="errModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Wystąpił błąd!</h4>
                </div>
                <div class="modal-body">
                    <%
                        Response.Write("<p>" + Request.QueryString["err"] + "</p>");
                    %>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
