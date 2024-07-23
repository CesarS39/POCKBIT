using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POCKBIT_v2.Paginas
{
    public partial class laboratorios : System.Web.UI.Page
    {
        public void BorrarTxt()
        {
            txtNombreL.Text = "";
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
                    sql = "INSERT INTO laboratorio (nombre, activo) VALUES (@nombre, @activo)";
                    using (SqlCommand mycmd = new SqlCommand(sql, conexion))
                    {
                        mycmd.Parameters.AddWithValue("@nombre", txtNombreL.Text);
                        mycmd.Parameters.AddWithValue("@activo", ddlEstado.SelectedValue);

                        int rowsAffected = mycmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Laboratorio insertado correctamente.");
                        }
                        else
                        {
                            Response.Write("No se insertó el laboratorio.");
                        }
                    }
                    conexion.Close();
                    BorrarTxt();
                    GRVLaboratorios.DataBind();
                    SqlDataSourceLaboratorios.DataBind();
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
                    sql = "UPDATE laboratorio SET nombre = @nombre, activo = @activo WHERE id_laboratorio = @id";
                    using (SqlCommand mycmd = new SqlCommand(sql, conexion))
                    {
                        mycmd.Parameters.AddWithValue("@nombre", txtNombreL.Text);
                        mycmd.Parameters.AddWithValue("@activo", ddlEstado.SelectedValue);
                        mycmd.Parameters.AddWithValue("@id", lblId.Text);

                        int rowsAffected = mycmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Laboratorio modificado correctamente.");
                        }
                        else
                        {
                            Response.Write("No se modificó el laboratorio.");
                        }
                    }
                    conexion.Close();
                    BorrarTxt();
                    GRVLaboratorios.DataBind();
                    SqlDataSourceLaboratorios.DataBind();
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
                    sql = "UPDATE laboratorio SET activo = 0 WHERE id_laboratorio = @id";
                    using (SqlCommand mycmd = new SqlCommand(sql, conexion))
                    {
                        mycmd.Parameters.AddWithValue("@id", lblId.Text);

                        int rowsAffected = mycmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Laboratorio marcado como inactivo correctamente.");
                        }
                        else
                        {
                            Response.Write("No se pudo marcar el laboratorio como inactivo.");
                        }
                    }
                    conexion.Close();
                    BorrarTxt();
                    GRVLaboratorios.DataBind();
                    SqlDataSourceLaboratorios.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void GRVLaboratorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblId.Text = GRVLaboratorios.SelectedRow.Cells[1].Text.ToString();
            txtNombreL.Text = GRVLaboratorios.SelectedRow.Cells[2].Text.ToString();
            ddlEstado.SelectedValue = GRVLaboratorios.SelectedRow.Cells[3].Text.ToString() == "Activo" ? "1" : "0";
        }
    }
}