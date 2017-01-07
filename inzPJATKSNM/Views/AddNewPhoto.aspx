<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewPhoto.aspx.cs" Inherits="inzPJATKSNM.Views.AddNewPhoto" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Scripts/dropzone/dropzone.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/dropzone/dropzone.js" type="text/javascript"></script>
    <h3><span class="label label-danger">
        <asp:Label ID="Label1" runat="server" Text="Dodaj zdjęcie." meta:resourcekey="Label1Resource1"></asp:Label></span></h3>


    &nbsp;<script type="text/javascript">
              function failOpenModal() {
                  $('#failModal').modal('show');
              }
</script><script>
              $(document).ready(function () {
                  Dropzone.autoDiscover = false;
                  //Simple Dropzonejs 
                  $("#dZUpload").dropzone({
                      url: "hn_FileUpload.ashx",
                      addRemoveLinks: true,
                      success: function (file, response) {
                          var imgName = response;
                          file.previewElement.classList.add("dz-success");
                      },
                      error: function (file, response) {
                          file.previewElement.classList.add("dz-error");
                      }
                  });
              });
    </script>
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=Avatar.ClientID %>');
            var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
    <br />
    <div id="content" class="container-fluid">
        <div id="left" style="float: left; width: 45%">
            <div class="container">
                <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" Height="25px" Width="350px" CssClass="form-control" meta:resourcekey="FileUpload1Resource1" onchange="previewFile()" />
                <asp:Label ID="StatusLabel" runat="server" CssClass="label label-danger" meta:resourcekey="StatusLabelResource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Proszę wybrać zdjęcie!" ControlToValidate="FileUpload1" Font-Bold="True" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Wybrano plik o złym rozszerzeniu!" ControlToValidate="FileUpload1" OnServerValidate="CustomValidator1_ServerValidate" Font-Bold="True" ForeColor="Red" Display="Dynamic" meta:resourcekey="CustomValidator1Resource1"></asp:CustomValidator>
                <br />
                <asp:Label ID="NazwaLabel" runat="server" Text="Nazwa" class="label label-danger" meta:resourcekey="NazwaLabelResource1"></asp:Label>
                <asp:TextBox ID="NazwaTextBox" runat="server" class="form-control" max-height="100px" max-width="350px" meta:resourcekey="NazwaTextBoxResource1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pole Nazwa jest wymagane!" ControlToValidate="NazwaTextBox" ForeColor="Red" Font-Bold="True" Display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="TechnikaLabel" runat="server" Text="Technika" class="label label-danger" meta:resourcekey="TechnikaLabelResource1"></asp:Label>
                <asp:DropDownList ID="TechnikaDropDownList" class="form-control" Width="250px" runat="server" DataSourceID="SqlDataSource1" DataTextField="Technika" DataValueField="Id_Tech" meta:resourcekey="TechnikaDropDownListResource1"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Tech], [Technika] FROM [Technika]" DeleteCommand="DELETE FROM [Technika] WHERE [Id_Tech] = @Id_Tech" InsertCommand="INSERT INTO [Technika] ([Technika]) VALUES (@Technika)" UpdateCommand="UPDATE [Technika] SET [Technika] = @Technika WHERE [Id_Tech] = @Id_Tech">
                    <DeleteParameters>
                        <asp:Parameter Name="Id_Tech" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Technika" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Technika" Type="String" />
                        <asp:Parameter Name="Id_Tech" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <br />
                <asp:Label ID="KategoriaLabel" runat="server" Text="Kategoria" class="label label-danger" meta:resourcekey="KategoriaLabelResource1"></asp:Label>
                <asp:DropDownList ID="KategoriaDropDownList" runat="server" class="form-control" Width="250px" DataSourceID="SqlDataSource2" DataTextField="Kategoria" DataValueField="Id_Kat" meta:resourcekey="KategoriaDropDownListResource1"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Kat], [Kategoria] FROM [Kategoria]"></asp:SqlDataSource>
                <br />
                <asp:Label ID="AutorLabel" runat="server" Text="Autor" class="label label-danger" meta:resourcekey="AutorLabelResource1"></asp:Label>
                <asp:DropDownList ID="AutorDropDownList" runat="server" class="form-control" Width="250px" DataSourceID="AuthorDS" DataTextField="Column1" DataValueField="Column1" meta:resourcekey="AutorDropDownListResource1"></asp:DropDownList>
                <asp:SqlDataSource ID="AuthorDS" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT IMIE+' '+NAZWISKO FROM AUTOR;"></asp:SqlDataSource>
                <br />
                <asp:Button ID="UploadButton" runat="server" Text="Zapisz" class="btn btn-danger" OnClick="UploadButton_Click" meta:resourcekey="UploadButtonResource1" />
            </div>
        </div>
        <div id="right" style="float: right; width: 45%">
            <% Response.Write("<h3><b>Załadowane zdjęcie</b></h3>"); %>
            <asp:Image ID="Avatar" runat="server" Height="225px" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
        </div>
    </div>
    <div id="failModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Wystąpił błąd</h4>
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
