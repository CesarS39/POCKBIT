<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="medicamentos.aspx.cs" Inherits="POCKBIT_v2.Paginas.medicamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-control, .form-control-label {
            height: 38px;
            width: 100%;
        }

        .form-control-label {
            display: flex;
            align-items: normal;
            justify-content: normal;
            text-align: justify;
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
    </style>
    <div class="text-center mb-4">
        <h2>Alta Medicamentos</h2>
    </div>
    <div class="row mb-4-spacing">
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">ID:</label>
            <asp:Label ID="lblId" runat="server" CssClass="form-control"></asp:Label>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Nombre comercial:</label>
            <asp:TextBox ID="txtNombreC" runat="server" Placeholder="Nombre Comercial" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Descripción:</label>
            <asp:TextBox ID="txtDescripcion" runat="server" Placeholder="Descripción" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Laboratorio:</label>
            <asp:DropDownList ID="ddlLaboratorio" runat="server" Placeholder="Laboratorio" CssClass="form-control" DataSourceID="SqlDataSourceDdlLaboratorios" DataTextField="nombre" DataValueField="id_laboratorio"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceDdlLaboratorios" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT id_laboratorio, nombre FROM laboratorio WHERE (activo = 1)"></asp:SqlDataSource>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Costo:</label>
            <asp:TextBox ID="txtCosto" runat="server" Placeholder="Costo" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Precio Máximo al Público:</label>
            <asp:TextBox ID="txtPrecioP" runat="server" Placeholder="Precio Máx Público" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Precio de venta:</label>
            <asp:TextBox ID="txtPrecioV" runat="server" Placeholder="Precio de Venta" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Código de Barras:</label>
            <asp:TextBox ID="txtCodigoB" runat="server" Placeholder="Código de Barras" CssClass="form-control"></asp:TextBox>
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
    <asp:GridView ID="GVMedicamentos" runat="server" AutoGenerateColumns="False" DataKeyNames="id_medicamento" DataSourceID="SqlDataSourceViewMedicamentos" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GVMedicamentos_SelectedIndexChanged1">
        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button"></asp:CommandField>
            <asp:BoundField DataField="id_medicamento" HeaderText="Id" ReadOnly="True" SortExpression="id_medicamento"></asp:BoundField>
            <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre"></asp:BoundField>
            <asp:BoundField DataField="descripcion" HeaderText="Descripcion" SortExpression="descripcion"></asp:BoundField>
            <asp:BoundField DataField="costo" HeaderText="Costo" SortExpression="costo"></asp:BoundField>
            <asp:BoundField DataField="precio_maximo_publico" HeaderText="Precio Maximo Al Publico" SortExpression="precio_maximo_publico"></asp:BoundField>
            <asp:BoundField DataField="precio_venta" HeaderText="Precio De Venta" SortExpression="precio_venta"></asp:BoundField>
            <asp:BoundField DataField="codigo_de_barras" HeaderText="Codigo De Barras" SortExpression="codigo_de_barras"></asp:BoundField>
            <asp:BoundField DataField="fecha_de_registro" HeaderText="Fecha De Registro" SortExpression="fecha_de_registro"></asp:BoundField>
            <asp:BoundField DataField="nombre_laboratorio" HeaderText="Laboratorios" SortExpression="nombre_laboratorio"></asp:BoundField>
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
    <asp:SqlDataSource runat="server" ID="SqlDataSourceViewMedicamentos" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT [id_medicamento], [nombre], [descripcion], [costo], [precio_maximo_publico], [precio_venta], [codigo_de_barras], [fecha_de_registro], [nombre_laboratorio], [activo] FROM [ViewMedicamento]"></asp:SqlDataSource>

</asp:Content>
