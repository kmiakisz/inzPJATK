<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="inzPJATKSNM.Views.ManageCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <script type="text/javascript">
        function existedObjModal() {
            $('#existsObjectModal').modal('show');
        }
    </script>
    <div id="content" class="container-fluid">
        <div id="left" style="float: left; width: 55%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="CategoryLbl" runat="server" Text="Usuwanie Kategorii." ForeColor="White"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <div id="CategoryGridViewDiv">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Id_Kat" DataSourceID="CategoriesDS" GridLines="None" CssClass="table table-hover table-striped">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:TemplateField HeaderText="Id_Kat" InsertVisible="False" SortExpression="Id_Kat">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Id_Kat") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Id_Kat") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kategoria" SortExpression="Kategoria">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Kategoria") %>' ValidationGroup="B"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="B" ErrorMessage="Pole nie może być puste!" Display="Dynamic" ForeColor="Red" Font-Bold="True" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Kategoria") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:SqlDataSource ID="CategoriesDS" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" DeleteCommand="DELETE FROM [Kategoria] WHERE [Id_Kat] = @Id_Kat" InsertCommand="INSERT INTO [Kategoria] ([Kategoria]) VALUES (@Kategoria)" SelectCommand="SELECT [Id_Kat], [Kategoria] FROM [Kategoria]" UpdateCommand="UPDATE [Kategoria] SET [Kategoria] = @Kategoria WHERE [Id_Kat] = @Id_Kat">
                            <DeleteParameters>
                                <asp:Parameter Name="Id_Kat" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="Kategoria" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Kategoria" Type="String" />
                                <asp:Parameter Name="Id_Kat" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>

                    </div>
                    <div id="CategoryAddBt1nDIV">
                    </div>
                </div>
            </div>
        </div>
        <div class="demo" style="float: right; width: 35%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="CategoryLbl1" runat="server" Text="Dodaj nową kategorię." ForeColor="White"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div id="CategoryTxtBoxDiv">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text="" Visible="true" ValidationGroup="A"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pole nie może być puste!" Display="Dynamic" ControlToValidate="TextBox1" ForeColor="Red" Font-Bold="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                    <div id="CategoryAddBtn2DIV">
                        <asp:Button ID="Button1" runat="server" Text="Dodaj" CssClass="btn btn-success" OnClick="Button1_Click" ValidationGroup="A" />
                    </div>
                </div>
            </div>
        </div>
        <div id="existsObjectModal" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Wystąpił błąd!</h4>
                    </div>
                    <div class="modal-body">
                        Aktualnie dodawana kategoria istnieje już w bazie!
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
