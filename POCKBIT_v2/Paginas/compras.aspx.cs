using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POCKBIT_v2.Paginas
{
    public partial class compras : System.Web.UI.Page
    {
        public void BorrarTxt()
        {
            txtCantidadC.Text = "";
            ddlCodigoB.SelectedIndex = -1;
            ddlLote.SelectedIndex = -1;
            lblId.Text = "";
        }

        public string Get_ConnectionString()
        {
            string SQLServer_Connection_String = "Server=tcp:pockbitv3.database.windows.net,1433;Initial Catalog=PockbitBDv2;Persist Security Info=False;User ID=PockbitSuperAdmin77;Password=5#Xw1Rz!m8Q@eL9zD7kT&f3V;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return SQLServer_Connection_String;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarDropDownLists();
            }
        }

        protected void LlenarDropDownLists()
        {
            ddlCodigoB.DataBind();
            ddlLote.DataBind();
            GVCompras.DataBind();
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(Get_ConnectionString()))
                {
                    conexion.Open();
                    int id_medicamento = int.Parse(ddlCodigoB.SelectedValue);
                    int id_lote = int.Parse(ddlLote.SelectedValue);
                    int cantidad = int.Parse(txtCantidadC.Text);
                    int id_proveedor = 1; // Puedes ajustar este valor según tu lógica
                    string realizado_por = HttpContext.Current.User.Identity.Name;
                    DateTime fecha_de_entrada = DateTime.Now; // Asignar la fecha actual a una variable

                    // Sumar cantidad en la tabla lote donde id_lote = @id_lote
                    using (SqlCommand cmdUpdateLote = new SqlCommand("UPDATE lote SET cantidad = cantidad + @cantidad WHERE id_lote = @id_lote", conexion))
                    {
                        cmdUpdateLote.Parameters.AddWithValue("@id_lote", id_lote);
                        cmdUpdateLote.Parameters.AddWithValue("@cantidad", cantidad);
                        cmdUpdateLote.ExecuteNonQuery();
                    }

                    // Obtener el costo del medicamento relacionado con el lote
                    float costo = 0;
                    using (SqlCommand cmdGetCosto = new SqlCommand("SELECT m.costo FROM lote l INNER JOIN medicamento m ON l.id_medicamento = m.id_medicamento WHERE l.id_lote = @id_lote", conexion))
                    {
                        cmdGetCosto.Parameters.AddWithValue("@id_lote", id_lote);
                        using (SqlDataReader reader = cmdGetCosto.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                costo = Convert.ToSingle(reader["costo"]); // Convertir el valor a float
                            }
                        }
                    }

                    // Calcular el costo total
                    float costo_total = cantidad * costo;

                    // Insertar un nuevo registro en la tabla compra
                    using (SqlCommand cmdInsertCompra = new SqlCommand("INSERT INTO compra (id_lote, cantidad, costo_total, fecha_de_entrada, id_proveedor, realizado_por) VALUES (@id_lote, @cantidad, @costo_total, @fecha_de_entrada, @id_proveedor, @realizado_por)", conexion))
                    {
                        cmdInsertCompra.Parameters.AddWithValue("@id_lote", id_lote);
                        cmdInsertCompra.Parameters.AddWithValue("@cantidad", cantidad);
                        cmdInsertCompra.Parameters.AddWithValue("@costo_total", costo_total);
                        cmdInsertCompra.Parameters.AddWithValue("@realizado_por", realizado_por);
                        cmdInsertCompra.Parameters.AddWithValue("@id_proveedor", id_proveedor);
                        cmdInsertCompra.Parameters.AddWithValue("@fecha_de_entrada", fecha_de_entrada);
                        cmdInsertCompra.ExecuteNonQuery();
                    }

                    conexion.Close();
                    BorrarTxt();
                    LlenarDropDownLists(); // Actualizar dropdown lists
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message + "<br>" + ex.StackTrace);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(Get_ConnectionString()))
                {
                    conexion.Open();

                    int id_compra = int.Parse(lblId.Text);
                    int id_proveedor = 1; // Puedes ajustar este valor según tu lógica
                                          // Datos Nuevos
                    int id_medicamento = int.Parse(ddlCodigoB.SelectedValue);
                    int id_lote = int.Parse(ddlLote.SelectedValue);
                    int cantidad = int.Parse(txtCantidadC.Text);
                    string realizado_por = HttpContext.Current.User.Identity.Name;
                    DateTime fecha_de_entrada = DateTime.Now;

                    // Datos Antiguos
                    int id_medicamento_anterior = 0;
                    int id_lote_anterior = 0;
                    int cantidadAnterior = 0;

                    using (SqlCommand cmdSelect = new SqlCommand("SELECT c.cantidad, m.id_medicamento, l.id_lote FROM compra c INNER JOIN lote l ON c.id_lote = l.id_lote INNER JOIN medicamento m ON l.id_medicamento = m.id_medicamento WHERE c.id_compra = @id_compra", conexion))
                    {
                        cmdSelect.Parameters.AddWithValue("@id_compra", id_compra);
                        using (SqlDataReader reader = cmdSelect.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cantidadAnterior = reader.GetInt32(0);
                                id_medicamento_anterior = reader.GetInt32(1);
                                id_lote_anterior = reader.GetInt32(2);
                            }
                        }
                    }

                    // Calcular la diferencia
                    int diferencia = cantidad - cantidadAnterior;

                    if (id_medicamento == id_medicamento_anterior)
                    {
                        if (id_lote == id_lote_anterior)
                        {
                            // Actualizar la tabla lote
                            using (SqlCommand cmdUpdateLote = new SqlCommand("UPDATE lote SET cantidad = cantidad + @diferencia WHERE id_lote = @id_lote", conexion))
                            {
                                cmdUpdateLote.Parameters.AddWithValue("@id_lote", id_lote);
                                cmdUpdateLote.Parameters.AddWithValue("@diferencia", diferencia);
                                cmdUpdateLote.ExecuteNonQuery();
                            }

                            // Obtener el costo del medicamento relacionado con el lote
                            float costo = 0;
                            using (SqlCommand cmdGetCosto = new SqlCommand("SELECT m.costo FROM lote l INNER JOIN medicamento m ON l.id_medicamento = m.id_medicamento WHERE l.id_lote = @id_lote", conexion))
                            {
                                cmdGetCosto.Parameters.AddWithValue("@id_lote", id_lote);
                                using (SqlDataReader reader = cmdGetCosto.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        costo = Convert.ToSingle(reader["costo"]);
                                    }
                                }
                            }

                            // Calcular el costo total y actualizar la tabla compra
                            float costo_total = cantidad * costo;
                            using (SqlCommand cmdUpdateCompra = new SqlCommand("UPDATE compra SET cantidad = @cantidad, fecha_de_entrada = @fecha_de_entrada, realizado_por = @realizado_por, costo_total = @costo_total WHERE id_compra = @id_compra", conexion))
                            {
                                cmdUpdateCompra.Parameters.AddWithValue("@id_compra", id_compra);
                                cmdUpdateCompra.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdUpdateCompra.Parameters.AddWithValue("@fecha_de_entrada", fecha_de_entrada);
                                cmdUpdateCompra.Parameters.AddWithValue("@realizado_por", realizado_por);
                                cmdUpdateCompra.Parameters.AddWithValue("@costo_total", costo_total);
                                cmdUpdateCompra.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Restar cantidadAnterior en la tabla lote donde id_lote = id_lote_anterior
                            using (SqlCommand cmdRestarCantidad = new SqlCommand("UPDATE lote SET cantidad = cantidad - @cantidadAnterior WHERE id_lote = @id_lote_anterior", conexion))
                            {
                                cmdRestarCantidad.Parameters.AddWithValue("@cantidadAnterior", cantidadAnterior);
                                cmdRestarCantidad.Parameters.AddWithValue("@id_lote_anterior", id_lote_anterior);
                                cmdRestarCantidad.ExecuteNonQuery();
                            }

                            // Sumar cantidad en la tabla lote donde id_lote = @id_lote
                            using (SqlCommand cmdSumarCantidad = new SqlCommand("UPDATE lote SET cantidad = cantidad + @cantidad WHERE id_lote = @id_lote", conexion))
                            {
                                cmdSumarCantidad.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdSumarCantidad.Parameters.AddWithValue("@id_lote", id_lote);
                                cmdSumarCantidad.ExecuteNonQuery();
                            }

                            // Obtener el costo del medicamento relacionado con el lote
                            float costo = 0;
                            using (SqlCommand cmdGetCosto = new SqlCommand("SELECT m.costo FROM lote l INNER JOIN medicamento m ON l.id_medicamento = m.id_medicamento WHERE l.id_lote = @id_lote", conexion))
                            {
                                cmdGetCosto.Parameters.AddWithValue("@id_lote", id_lote);
                                using (SqlDataReader reader = cmdGetCosto.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        costo = Convert.ToSingle(reader["costo"]);
                                    }
                                }
                            }

                            // Calcular el costo total
                            float costo_total = cantidad * costo;

                            // Actualizar la tabla compra
                            using (SqlCommand cmdUpdateCompra = new SqlCommand("UPDATE compra SET cantidad = @cantidad, fecha_de_entrada = @fecha_de_entrada, realizado_por = @realizado_por, costo_total = @costo_total, id_lote = @id_lote WHERE id_compra = @id_compra", conexion))
                            {
                                cmdUpdateCompra.Parameters.AddWithValue("@id_compra", id_compra);
                                cmdUpdateCompra.Parameters.AddWithValue("@id_lote", id_lote);
                                cmdUpdateCompra.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdUpdateCompra.Parameters.AddWithValue("@fecha_de_entrada", fecha_de_entrada);
                                cmdUpdateCompra.Parameters.AddWithValue("@realizado_por", realizado_por);
                                cmdUpdateCompra.Parameters.AddWithValue("@costo_total", costo_total);
                                cmdUpdateCompra.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        // Restar cantidadAnterior en la tabla lote donde id_lote = id_lote_anterior y id_medicamento = id_medicamento_anterior
                        using (SqlCommand cmdRestarCantidad = new SqlCommand("UPDATE lote SET cantidad = cantidad - @cantidadAnterior WHERE id_lote = @id_lote_anterior AND id_medicamento = @id_medicamento_anterior", conexion))
                        {
                            cmdRestarCantidad.Parameters.AddWithValue("@cantidadAnterior", cantidadAnterior);
                            cmdRestarCantidad.Parameters.AddWithValue("@id_lote_anterior", id_lote_anterior);
                            cmdRestarCantidad.Parameters.AddWithValue("@id_medicamento_anterior", id_medicamento_anterior);
                            cmdRestarCantidad.ExecuteNonQuery();
                        }

                        // Sumar cantidad en la tabla lote donde id_lote = @id_lote y id_medicamento = @id_medicamento
                        using (SqlCommand cmdSumarCantidad = new SqlCommand("UPDATE lote SET cantidad = cantidad + @cantidad WHERE id_lote = @id_lote AND id_medicamento = @id_medicamento", conexion))
                        {
                            cmdSumarCantidad.Parameters.AddWithValue("@cantidad", cantidad);
                            cmdSumarCantidad.Parameters.AddWithValue("@id_lote", id_lote);
                            cmdSumarCantidad.Parameters.AddWithValue("@id_medicamento", id_medicamento);
                            cmdSumarCantidad.ExecuteNonQuery();
                        }

                        // Obtener el costo del medicamento relacionado con el lote
                        float costo = 0;
                        using (SqlCommand cmdGetCosto = new SqlCommand("SELECT m.costo FROM lote l INNER JOIN medicamento m ON l.id_medicamento = m.id_medicamento WHERE l.id_lote = @id_lote", conexion))
                        {
                            cmdGetCosto.Parameters.AddWithValue("@id_lote", id_lote);
                            using (SqlDataReader reader = cmdGetCosto.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    costo = Convert.ToSingle(reader["costo"]);
                                }
                            }
                        }

                        // Calcular el costo total y actualizar la tabla compra
                        float costo_total = cantidad * costo;
                        using (SqlCommand cmdUpdateCompra = new SqlCommand("UPDATE compra SET cantidad = @cantidad, fecha_de_entrada = @fecha_de_entrada, realizado_por = @realizado_por, costo_total = @costo_total, id_lote = @id_lote WHERE id_compra = @id_compra", conexion))
                        {
                            cmdUpdateCompra.Parameters.AddWithValue("@id_compra", id_compra);
                            cmdUpdateCompra.Parameters.AddWithValue("@id_lote", id_lote);
                            cmdUpdateCompra.Parameters.AddWithValue("@cantidad", cantidad);
                            cmdUpdateCompra.Parameters.AddWithValue("@fecha_de_entrada", fecha_de_entrada);
                            cmdUpdateCompra.Parameters.AddWithValue("@realizado_por", realizado_por);
                            cmdUpdateCompra.Parameters.AddWithValue("@costo_total", costo_total);
                            cmdUpdateCompra.ExecuteNonQuery();
                        }
                    }

                    conexion.Close();
                    BorrarTxt();
                    LlenarDropDownLists(); // Actualizar dropdown lists
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message + "<br>" + ex.StackTrace);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(Get_ConnectionString()))
                {
                    conexion.Open();
                    int id_compra = int.Parse(lblId.Text); // ID de la compra a eliminar
                    int id_lote = int.Parse(ddlLote.SelectedValue);
                    int cantidad = int.Parse(txtCantidadC.Text);

                    // Eliminar el registro de la tabla compra
                    using (SqlCommand cmdDeleteCompra = new SqlCommand("DELETE FROM compra WHERE id_compra = @id_compra", conexion))
                    {
                        cmdDeleteCompra.Parameters.AddWithValue("@id_compra", id_compra);
                        cmdDeleteCompra.ExecuteNonQuery();
                    }

                    // Restar cantidad en la tabla lote donde id_lote = @id_lote
                    using (SqlCommand cmdUpdateLote = new SqlCommand("UPDATE lote SET cantidad = cantidad - @cantidad WHERE id_lote = @id_lote", conexion))
                    {
                        cmdUpdateLote.Parameters.AddWithValue("@id_lote", id_lote);
                        cmdUpdateLote.Parameters.AddWithValue("@cantidad", cantidad);
                        cmdUpdateLote.ExecuteNonQuery();
                    }
                    conexion.Close();
                    BorrarTxt();
                    LlenarDropDownLists(); // Actualizar dropdown lists
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message + "<br>" + ex.StackTrace);
            }
        }
        protected void GVCompras_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblId.Text = GVCompras.SelectedRow.Cells[1].Text;
            txtCantidadC.Text = GVCompras.SelectedRow.Cells[7].Text;
        }
    }
}