<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogInView.aspx.cs" Inherits="inzPJATKSNM.Views.LogInView" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <script type="text/javascript">
             function failOpenModal() {
                 $('#failModal').modal('show');
             }
        </script>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <div id="content" class="container-fluid">
        <br />
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title"><asp:Label ID="Label1" runat="server" Text="Logowanie" meta:resourcekey="Label1Resource1"></asp:Label></h3>
            </div>
            <div class="panel-body">
                <div id="login">
                    <asp:Label ID="LoginLbl" runat="server" Text="Email" CssClass="label label-danger" meta:resourcekey="LoginLblResource1"></asp:Label>
                    <asp:TextBox ID="LoginTxt" runat="server" CssClass="form-control" meta:resourcekey="LoginTxtResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="LoginTxt" runat="server" ErrorMessage="Email/Login jest polem wymaganym!" ForeColor="Red" Font-Bold="True" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
                <div id="pwd">
                    <asp:Label ID="PwdLbl" runat="server" Text="Hasło" CssClass="label label-danger" meta:resourcekey="PwdLblResource1"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Password" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBox2" runat="server" ErrorMessage="Hasło jest polem wymaganym!" ForeColor="Red" Font-Bold="True" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div id="loginbutton">
            <asp:Button ID="LogInButton" runat="server" Text="Zaloguj."  CssClass="btn btn-success" OnClick="LogInButton_Click" meta:resourcekey="LogInButtonResource1" ValidationGroup="A"/>
            <asp:Button ID="ResetPwdButton" runat="server" Text="Nie pamiętam hasła." CssClass="btn btn-danger" OnClick="ResetPwdButton_Click" meta:resourcekey="ResetPwdButtonResource1" />
        </div>
    </div>
           <div id="failModal"  class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button   type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Wystąpił błąd</h4>
                    </div>
                    <div class="modal-body">
                        <%
                            Response.Write("<p>"+Request.QueryString["err"]+"</p>");
                             %>                      
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
