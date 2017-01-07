<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTechniques.aspx.cs" Inherits="inzPJATKSNM.Views.ManageTechniques" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Demo.css" rel="stylesheet" />
    <script type="text/javascript">
        function failInsertOpenModal() {
            $('#failModal').modal('show');
        }
    </script>
    <div id="content" class="container-fluid">
        <div id="left" style="float: left; width: 55%">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="TechniqueLbl" runat="server" Text="Usuwanie Technik."></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <div id="TechniqueGridViewDiv">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id_Tech" DataSourceID="TechiqueDS" GridLines="None" CssClass="table table-hover table-striped">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:BoundField DataField="Id_Tech" HeaderText="Id_Tech" InsertVisible="False" ReadOnly="True" SortExpression="Id_Tech" />
                                <asp:BoundField DataField="Technika" HeaderText="Technika" SortExpression="Technika" />
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
                        <asp:Label ID="TechniqueLbl1" runat="server" Text="Dodaj nową technikę."></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <div id="TechniqueTextBoxDiv">
                        <asp:TextBox ID="TechniqueTxt" runat="server" CssClass="form-control" Text="" Visible="true"></asp:TextBox>
                    </div>
                    <div id="TechniqueAddBtn2DIV">
                        <asp:Button ID="Button1" runat="server" Text="Dodaj" OnClick="Button1_Click"  CssClass="btn btn-success"/>
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
</asp:Content>
