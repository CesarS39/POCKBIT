using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POCKBIT_v2.Paginas
{
    public partial class lotes : System.Web.UI.Page
    {
        public void BorrarTxt()
        {
            txtNumeroLote.Text = "";
            txtFechaCaducidad.Value = "";
            lblId.Text = "";
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

                    // Validar que no exista un lote con el mismo número para el mismo medicamento
                    string validarLote = "SELECT COUNT(*) FROM lote WHERE numero_de_lote = @numero_de_lote AND id_medicamento = @id_medicamento";
                    using (SqlCommand validarCmd = new SqlCommand(validarLote, conexion))
                    {
                        validarCmd.Parameters.AddWithValue("@numero_de_lote", txtNumeroLote.Text);
                        validarCmd.Parameters.AddWithValue("@id_medicamento", int.Parse(ddlCodigoB.SelectedValue));
                        int count = (int)validarCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            Response.Write("Error: Ya existe un lote con el mismo número para este medicamento.");
                            return;
                        }
                    }

                    sql = "INSERT INTO lote (numero_de_lote, fecha_caducidad, id_medicamento) VALUES (@numero_de_lote, @fecha_caducidad, @id_medicamento)";
                    using (SqlCommand mycmd = new SqlCommand(sql, conexion))
                    {
                        mycmd.Parameters.AddWithValue("@fecha_caducidad", txtFechaCaducidad.Value);
                        mycmd.Parameters.AddWithValue("@id_medicamento", int.Parse(ddlCodigoB.SelectedValue));
                        mycmd.Parameters.AddWithValue("@numero_de_lote", txtNumeroLote.Text);

                        int rowsAffected = mycmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Lote insertado correctamente.");
                        }
                        else
                        {
                            Response.Write("No se insertó el lote.");
                        }
                    }
                    conexion.Close();
                    BorrarTxt();
                    GVLotes.DataBind();
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

                    // Validar que no exista otro lote con el mismo número para el mismo medicamento
                    string validarLote = "SELECT COUNT(*) FROM lote WHERE numero_de_lote = @numero_de_lote AND id_medicamento = @id_medicamento AND id_lote != @id_lote";
                    using (SqlCommand validarCmd = new SqlCommand(validarLote, conexion))
                    {
                        validarCmd.Parameters.AddWithValue("@numero_de_lote", txtNumeroLote.Text);
                        validarCmd.Parameters.AddWithValue("@id_medicamento", int.Parse(ddlCodigoB.SelectedValue));
                        validarCmd.Parameters.AddWithValue("@id_lote", int.Parse(lblId.Text));
                        int count = (int)validarCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            Response.Write("Error: Ya existe otro lote con el mismo número para este medicamento.");
                            return;
                        }
                    }

                    sql = "UPDATE lote SET numero_de_lote = @numero_de_lote, fecha_caducidad = @fecha_caducidad, id_medicamento = @id_medicamento WHERE id_lote = @id_lote";
                    using (SqlCommand mycmd = new SqlCommand(sql, conexion))
                    {
                        mycmd.Parameters.AddWithValue("@fecha_caducidad", txtFechaCaducidad.Value);
                        mycmd.Parameters.AddWithValue("@id_medicamento", int.Parse(ddlCodigoB.SelectedValue));
                        mycmd.Parameters.AddWithValue("@numero_de_lote", txtNumeroLote.Text);
                        mycmd.Parameters.AddWithValue("@id_lote", int.Parse(lblId.Text)); // Suponiendo que el ID del lote a modificar se almacena en un campo oculto

                        int rowsAffected = mycmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Lote modificado correctamente.");
                        }
                        else
                        {
                            Response.Write("No se modificó el lote.");
                        }
                    }
                    conexion.Close();
                    BorrarTxt();
                    GVLotes.DataBind();
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
                    sql = "UPDATE lote SET activo = 0 WHERE id_lote = @id_lote";
                    using (SqlCommand mycmd = new SqlCommand(sql, conexion))
                    {
                        mycmd.Parameters.AddWithValue("@id_lote", int.Parse(lblId.Text));

                        int rowsAffected = mycmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Lote deshabilitado correctamente.");
                        }
                        else
                        {
                            Response.Write("No se deshabilitó el lote.");
                        }
                    }
                    conexion.Close();
                    BorrarTxt();
                    GVLotes.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void GVLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblId.Text = GVLotes.SelectedRow.Cells[1].Text;
            txtNumeroLote.Text = GVLotes.SelectedRow.Cells[2].Text;
            txtFechaCaducidad.Value = GVLotes.SelectedRow.Cells[3].Text;
        }
    }
}
