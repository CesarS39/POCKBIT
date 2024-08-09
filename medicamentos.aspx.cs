using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POCKBIT_v2.Paginas
{
    public partial class medicamentos : System.Web.UI.Page
    {
        public void BorrarTxt()
        {
            txtNombreC.Text = "";
            txtDescripcion.Text = "";
            txtCosto.Text = "";
            txtPrecioP.Text = "";
            txtPrecioV.Text = "";
            txtCodigoB.Text = "";
            lblId.Text = "";
            ddlEstado.SelectedIndex = 1;
        }

        public string Get_ConnectionString()
        {
            string SQLServer_Connection_String = "Server=tcp:pockbitv3.database.windows.net,1433;Initial Catalog=PockbitBDv2;Persist Security Info=False;User ID=PockbitSuperAdmin77;Password=5#Xw1Rz!m8Q@eL9zD7kT&f3V;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return SQLServer_Connection_String;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                using (SqlConnection conexion = new SqlConnection(Get_ConnectionString()))
                {
                    conexion.Open();

                    // Validar que no exista un medicamento con el mismo código de barras
                    string validarCodigo = "SELECT COUNT(*) FROM medicamento WHERE codigo_de_barras = @codigo_de_barras";
                    using (SqlCommand validarCmd = new SqlCommand(validarCodigo, conexion))
                    {
                        validarCmd.Parameters.AddWithValue("@codigo_de_barras", txtCodigoB.Text);
                        int count = (int)validarCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            Response.Write("Error: Ya existe un medicamento con el mismo código de barras.");
                            return;
                        }
                    }

                    sql = @"INSERT INTO medicamento 
                            (nombre, descripcion, costo, precio_maximo_publico, precio_venta, codigo_de_barras, id_laboratorio, fecha_de_registro, activo) 
                            VALUES 
                            (@nombre, @descripcion, @costo, @precio_maximo_publico, @precio_venta, @codigo_de_barras, @id_laboratorio, @fecha_de_registro, @activo)";

                    using (SqlCommand mycmd = new SqlCommand(sql, conexion))
                    {
                        mycmd.Parameters.AddWithValue("@nombre", txtNombreC.Text);
                        mycmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                        mycmd.Parameters.AddWithValue("@costo", txtCosto.Text);
                        mycmd.Parameters.AddWithValue("@precio_maximo_publico", txtPrecioP.Text);
                        mycmd.Parameters.AddWithValue("@precio_venta", txtPrecioV.Text);
                        mycmd.Parameters.AddWithValue("@codigo_de_barras", txtCodigoB.Text);
                        mycmd.Parameters.AddWithValue("@id_laboratorio", ddlLaboratorio.SelectedValue);
                        mycmd.Parameters.AddWithValue("@fecha_de_registro", DateTime.Now);
                        mycmd.Parameters.AddWithValue("@activo", ddlEstado.SelectedValue);

                        int rowsAffected = mycmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Medicamento insertado correctamente.");
                        }
                        else
                        {
                            Response.Write("No se insertó el Medicamento.");
                        }
                    }
                    conexion.Close();
                    BorrarTxt();
                    GVMedicamentos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                using (SqlConnection conexion = new SqlConnection(Get_ConnectionString()))
                {
                    conexion.Open();

                    // Validar que no exista otro medicamento con el mismo código de barras
                    string validarCodigo = "SELECT COUNT(*) FROM medicamento WHERE codigo_de_barras = @codigo_de_barras AND id_medicamento != @id_medicamento";
                    using (SqlCommand validarCmd = new SqlCommand(validarCodigo, conexion))
                    {
                        validarCmd.Parameters.AddWithValue("@codigo_de_barras", txtCodigoB.Text);
                        validarCmd.Parameters.AddWithValue("@id_medicamento", lblId.Text);
                        int count = (int)validarCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            Response.Write("Error: Ya existe otro medicamento con el mismo código de barras.");
                            return;
                        }
                    }

                    sql = @"UPDATE medicamento 
                            SET nombre = @nombre, descripcion = @descripcion, costo = @costo, precio_maximo_publico = @precio_maximo_publico, 
                                precio_venta = @precio_venta, codigo_de_barras = @codigo_de_barras, id_laboratorio = @id_laboratorio, activo = @activo
                            WHERE id_medicamento = @id_medicamento";

                    using (SqlCommand mycmd = new SqlCommand(sql, conexion))
                    {
                        mycmd.Parameters.AddWithValue("@nombre", txtNombreC.Text);
                        mycmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                        mycmd.Parameters.AddWithValue("@costo", txtCosto.Text);
                        mycmd.Parameters.AddWithValue("@precio_maximo_publico", txtPrecioP.Text);
                        mycmd.Parameters.AddWithValue("@precio_venta", txtPrecioV.Text);
                        mycmd.Parameters.AddWithValue("@codigo_de_barras", txtCodigoB.Text);
                        mycmd.Parameters.AddWithValue("@id_laboratorio", ddlLaboratorio.SelectedValue);
                        mycmd.Parameters.AddWithValue("@activo", ddlEstado.SelectedValue);
                        mycmd.Parameters.AddWithValue("@id_medicamento", lblId.Text);

                        int rowsAffected = mycmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Medicamento modificado correctamente.");
                        }
                        else
                        {
                            Response.Write("No se modificó el Medicamento.");
                        }
                    }
                    conexion.Close();
                    BorrarTxt();
                    GVMedicamentos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                using (SqlConnection conexion = new SqlConnection(Get_ConnectionString()))
                {
                    conexion.Open();
                    sql = "UPDATE medicamento SET activo = 0 WHERE id_medicamento = @id_medicamento";

                    using (SqlCommand mycmd = new SqlCommand(sql, conexion))
                    {
                        mycmd.Parameters.AddWithValue("@id_medicamento", lblId.Text);

                        int rowsAffected = mycmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Medicamento marcado como inactivo correctamente.");
                        }
                        else
                        {
                            Response.Write("No se pudo marcar el Medicamento como inactivo.");
                        }
                    }
                    conexion.Close();
                    BorrarTxt();
                    GVMedicamentos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void GVMedicamentos_SelectedIndexChanged1(object sender, EventArgs e)
        {
            lblId.Text = GVMedicamentos.SelectedRow.Cells[1].Text.ToString();
            txtNombreC.Text = GVMedicamentos.SelectedRow.Cells[2].Text.ToString();
            txtDescripcion.Text = GVMedicamentos.SelectedRow.Cells[3].Text.ToString();
            txtCosto.Text = GVMedicamentos.SelectedRow.Cells[4].Text.ToString();
            txtPrecioP.Text = GVMedicamentos.SelectedRow.Cells[5].Text.ToString();
            txtPrecioV.Text = GVMedicamentos.SelectedRow.Cells[6].Text.ToString();
            txtCodigoB.Text = GVMedicamentos.SelectedRow.Cells[7].Text.ToString();
            ddlEstado.SelectedValue = GVMedicamentos.SelectedRow.Cells[10].Text.ToString() == "Activo" ? "1" : "0";
        }
    }
}