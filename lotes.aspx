<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="lotes.aspx.cs" Inherits="POCKBIT_v2.Paginas.lotes" %>

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
        <h2>Registro de Lotes</h2>
    </div>
    <div class="row mb-4 row-center">
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
            <label class="form-control-label">Numero de Lote:</label>
            <asp:TextBox ID="txtNumeroLote" runat="server" Placeholder="Número de lote" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3 mb-4-spacing">
            <label class="form-control-label">Fecha de caducidad:</label>
            <input type="date" id="txtFechaCaducidad" runat="server" class="form-control" />
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
    <asp:GridView ID="GVLotes" runat="server" AutoGenerateColumns="False" DataKeyNames="id_lote" DataSourceID="SqlDataSourceLotes" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GVLotes_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button"></asp:CommandField>
            <asp:BoundField DataField="id_lote" HeaderText="id_lote" ReadOnly="True" InsertVisible="False" SortExpression="id_lote"></asp:BoundField>
            <asp:BoundField DataField="numero_de_lote" HeaderText="numero_de_lote" SortExpression="numero_de_lote"></asp:BoundField>
            <asp:BoundField DataField="fecha_caducidad" HeaderText="fecha_caducidad" SortExpression="fecha_caducidad"></asp:BoundField>
            <asp:BoundField DataField="cantidad" HeaderText="cantidad" SortExpression="cantidad"></asp:BoundField>
            <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="Laboratorio"></asp:BoundField>
            <asp:BoundField DataField="Expr1" HeaderText="Expr1" SortExpression="Medicamento"></asp:BoundField>
            <asp:BoundField DataField="descripcion" HeaderText="descripcion" SortExpression="descripcion"></asp:BoundField>
            <asp:BoundField DataField="codigo_de_barras" HeaderText="codigo_de_barras" SortExpression="codigo_de_barras"></asp:BoundField>
            <asp:CheckBoxField DataField="activo" HeaderText="activo" SortExpression="activo"></asp:CheckBoxField>
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
    <asp:SqlDataSource runat="server" ID="SqlDataSourceLotes" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT lote.id_lote, lote.numero_de_lote, lote.fecha_caducidad, lote.cantidad, laboratorio.nombre, medicamento.nombre AS Expr1, medicamento.descripcion, medicamento.codigo_de_barras, lote.activo FROM laboratorio INNER JOIN medicamento ON laboratorio.id_laboratorio = medicamento.id_laboratorio INNER JOIN lote ON medicamento.id_medicamento = lote.id_medicamento WHERE (laboratorio.activo = 1)"></asp:SqlDataSource>
</asp:Content>
