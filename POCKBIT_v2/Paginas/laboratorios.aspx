<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="laboratorios.aspx.cs" Inherits="POCKBIT_v2.Paginas.laboratorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-control, .form-control-label {
            height: 38px;
            width: 100%;
        }

        .form-control-label {
            display: flex;
            align-items: center;
            justify-content: center;
            text-align: center;
        }

        .mb-4-spacing {
            margin-bottom: 1.5rem;
        }

        .btn-center {
            display: flex;
            justify-content: center;
        }

            .btn-center .col-md-3 {
                margin-left: 1rem;
                margin-right: 1rem;
            }

        .row-center {
            display: flex;
            justify-content: center;
            align-items: center;
        }
    </style>

    <div class="text-center mb-4">
        <h2>Añadir laboratorios</h2>
    </div>
    <div class="row mb-3 row-center">
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">ID:</label>
            <asp:Label ID="lblId" runat="server" CssClass="form-control"></asp:Label>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Laboratorio:</label>
            <asp:TextBox ID="txtNombreL" runat="server" Placeholder="Nombre Del Labolatorio" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Estado:</label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="row mb-3 btn-center">
        <div class="col-md-3 mb-3">
            <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary w-100" OnClick="btnInsertar_Click" />
        </div>
        <div class="col-md-3 mb-3">
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-secondary w-100" OnClick="btnModificar_Click" />
        </div>
        <div class="col-md-3 mb-3">
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger w-100" OnClick="btnEliminar_Click" />
        </div>
    </div>
    <asp:GridView ID="GRVLaboratorios" runat="server" AutoGenerateColumns="False" DataKeyNames="id_laboratorio" DataSourceID="SqlDataSourceLaboratorios" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GRVLaboratorios_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button"></asp:CommandField>
            <asp:BoundField DataField="id_laboratorio" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="id_laboratorio"></asp:BoundField>
            <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre"></asp:BoundField>
            <asp:CheckBoxField DataField="activo" HeaderText="Activo" SortExpression="activo"></asp:CheckBoxField>
        </Columns>
        <EditRowStyle BackColor="#2461BF"></EditRowStyle>

        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

        <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

        <RowStyle BackColor="#EFF3FB"></RowStyle>

        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

        <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

        <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

        <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

        <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
    </asp:GridView>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceLaboratorios" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT [id_laboratorio], [nombre], [activo] FROM [laboratorio]"></asp:SqlDataSource>
</asp:Content>
