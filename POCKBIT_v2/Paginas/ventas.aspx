<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ventas.aspx.cs" Inherits="POCKBIT_v2.Paginas.ventas" %>

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
        <h2>Registro de Venta de Medicamentos</h2>
    </div>
    <div class="row mb-4-spacing">
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">ID:</label>
            <asp:Label ID="lblId" runat="server" CssClass="form-control"></asp:Label>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Código de Barras:</label>
            <asp:DropDownList ID="ddlCodigoB" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="SqlDataSourceCodigosBarras" DataTextField="codigo_de_barras" DataValueField="id_medicamento"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceCodigosBarras" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT id_medicamento, codigo_de_barras FROM medicamento WHERE (activo = 1)"></asp:SqlDataSource>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Seleccionar Lote:</label>
            <asp:DropDownList ID="ddlLote" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceLotes" DataTextField="numero_de_lote" DataValueField="id_lote"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceLotes" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT id_lote, numero_de_lote FROM lote WHERE (id_medicamento = @id_medicamento) AND (activo = 1) ORDER BY id_lote">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlCodigoB" PropertyName="SelectedValue" Name="id_medicamento"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Cantidad Vendida:</label>
            <asp:TextBox ID="txtCantidadC" runat="server" Placeholder="Numeros enteros" CssClass="form-control"></asp:TextBox>
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
</asp:Content>
