<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewPhoto.aspx.cs" Inherits="inzPJATKSNM.Views.AddNewPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <link href="../Scripts/dropzone/dropzone.css" rel="stylesheet" type="text/css" />
 <script src="../Scripts/dropzone/dropzone.js" type="text/javascript"></script>
  
    <h3><span class="label label-danger">Dodaj zdjęcie</span></h3>

    <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static"  Height="24px" Width="258px" />
    <div id="dZUpload" class="dropzone">
        <div class="dz-default dz-message"></div>
    </div>
    <script type="text/javascript">
        function failOpenModal() {
            $('#failModal').modal('show');
        }
    </script>
    <script>
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
     <form id="frmMain" runat="server" class="dropzone">
            <div>
                <div class="fallback">
                    <input name="file" type="file" multiple />
                </div>
            </div>
        </form>
    <br />
    <asp:Label ID="StatusLabel" runat="server" Text="" CssClass="label label-danger"></asp:Label>

        <div class="container">
            <br />
            <asp:Label ID="TechnikaLabel" runat="server" Text="Technika" class="label label-danger"></asp:Label>
            <asp:DropDownList ID="TechnikaDropDownList" class="form-control" Width="250px" runat="server" DataSourceID="SqlDataSource1" DataTextField="Technika" DataValueField="Id_Tech"></asp:DropDownList>
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
            <asp:Label ID="KategoriaLabel" runat="server" Text="Kategoria" class="label label-danger"></asp:Label>
            <asp:DropDownList ID="KategoriaDropDownList" runat="server" class="form-control" Width="250px" DataSourceID="SqlDataSource2" DataTextField="Kategoria" DataValueField="Id_Kat"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Kat], [Kategoria] FROM [Kategoria]"></asp:SqlDataSource>
            <br />
            <asp:Label ID="AutorLabel" runat="server" Text="Autor" class="label label-danger"></asp:Label>
            <asp:DropDownList ID="AutorDropDownList" runat="server" class="form-control" Width="250px" DataSourceID="SqlDataSource3" DataTextField="Nazwisko" DataValueField="Id_Autora"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" SelectCommand="SELECT [Id_Autora], [Nazwisko] FROM [Autor]"></asp:SqlDataSource>
            <br />
            <asp:Button ID="UploadButton" runat="server" Text="Zapisz" class="btn btn-danger" OnClick="UploadButton_Click" />
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
