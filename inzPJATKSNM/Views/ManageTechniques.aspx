<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTechniques.aspx.cs" Inherits="inzPJATKSNM.Views.ManageTechniques" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Demo.css" rel="stylesheet" />
    <script type="text/javascript">
        function failInsertOpenModal() {
            $('#failModal').modal('show');
        }
    </script>
    <script type="text/javascript">
        function existedObjModal() {
            $('#existsObjectModal').modal('show');
        }
    </script>
    <br />
    <div id="content" class="container-fluid">
        <div id="left" style="float: left; width: 55%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="TechniqueLbl" runat="server" Text="Usuwanie Technik." ForeColor="White"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <div id="TechniqueGridViewDiv">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id_Tech" DataSourceID="TechiqueDS" GridLines="None" CssClass="table table-hover table-striped">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:TemplateField HeaderText="Id_Tech" InsertVisible="False" SortExpression="Id_Tech">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Id_Tech") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Id_Tech") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Technika" SortExpression="Technika">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Technika") %>'></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Pole nie może być puste!" ValidationGroup="B" Display="Dynamic" ForeColor="Red" Font-Bold="True" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Technika") %>' ValidationGroup="B"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="TechiqueDS" runat="server" ConnectionString="<%$ ConnectionStrings:inzSNMConnectionString %>" DeleteCommand="DELETE FROM [Technika] WHERE [Id_Tech] = @Id_Tech" InsertCommand="INSERT INTO [Technika] ([Technika]) VALUES (@Technika)" SelectCommand="SELECT [Id_Tech], [Technika] FROM [Technika]" UpdateCommand="UPDATE [Technika] SET [Technika] = @Technika WHERE [Id_Tech] = @Id_Tech">
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
                    </div>
                    <div id="TechniqueAddBt1nDIV">
                    </div>
                </div>
            </div>
        </div>
        <div class="demo" style="float: right; width: 35%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="TechniqueLbl1" runat="server" Text="Dodaj nową technikę." ForeColor="White"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <div id="TechniqueTextBoxDiv">
                        <asp:TextBox ID="TechniqueTxt" runat="server" CssClass="form-control" Text="" Visible="true" ValidationGroup="A"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pole nie może być puste!" ValidationGroup="A" ControlToValidate="TechniqueTxt" Display="Dynamic" ForeColor="Red" Font-Bold="True"></asp:RequiredFieldValidator>
                    </div>
                    <div id="TechniqueAddBtn2DIV">
                        <asp:Button ID="Button1" runat="server" Text="Dodaj" OnClick="Button1_Click" CssClass="btn btn-success" ValidationGroup="A" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="failModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Wystąpił błąd!</h4>
                </div>
                <div class="modal-body">
                    <%
                        Response.Write("<p> " + val + "</p>");
                    %>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
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
                    Aktualnie dodawana technika istnieje już w bazie!
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Zamknij</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
