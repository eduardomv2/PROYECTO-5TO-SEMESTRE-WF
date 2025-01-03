﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PROYECTO_5TO_SEMESTRE.Login;

namespace PROYECTO_5TO_SEMESTRE
{
    public partial class MecanicoForm : Form
    {
        //string connectionString = "Server=localhost;Database=TESTING_DB;Integrated Security=True;";
        private string connectionString = "Data Source=eduardomv\\SQLEXPRESS;Initial Catalog=TESTING_DB;Integrated Security=True";

        public MecanicoForm()
        {
            InitializeComponent();
        }

        private void CargarMantenimiento()
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        // Consulta para obtener los datos de mantenimiento
            //        string query = "SELECT M.id_mantenimiento, V.nombre AS vehiculo, E.nombre AS empleado, MT.nombre AS tipo_mantenimiento, " +
            //                       "M.fecha_inicio, M.fecha_proximo_mantenimiento, M.fecha_creacion, M.fecha_modificacion, " +
            //                       "CASE M.estatus WHEN 0 THEN 'Terminado' ELSE 'En progreso' END AS estatus " +
            //                       "FROM Mantenimiento M " +
            //                       "INNER JOIN Vehiculo V ON M.id_vehiculo = V.id_vehiculo " +
            //                       "INNER JOIN Empleado E ON M.id_empleado = E.id_empleado " +
            //                       "INNER JOIN MantenimientoTipo MT ON M.id_mantenimiento_tipo = MT.id_mantenimiento_tipo";

            //        SqlCommand cmd = new SqlCommand(query, conn);
            //        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            //        DataTable dt = new DataTable();
            //        dataAdapter.Fill(dt);

            //        // Asignar los datos al DataGridView
            //        dataGridViewMantenimiento.DataSource = dt;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al cargar los datos de mantenimiento: " + ex.Message);
            //}

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_mantenimiento, id_vehiculo, id_mantenimiento_tipo, fecha_inicio, fecha_proximo_mantenimiento, estatus FROM Mantenimiento";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    dataGridViewMantenimiento.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los mantenimientos: " + ex.Message);
            }

        }

        


        private void MecanicoForm_Load(object sender, EventArgs e)
        {

            CargarVehiculos();
            CargarTiposMantenimiento();
            CargarEstatus();
            CargarMantenimiento();
            CargarEstatusVendido();
            CargarMantenimientoVendido(); 
            CargarVehiculosVendido();
            CargarMantenimientosVehiculosVendidos();
        }

        private void CargarVehiculosVendido()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Modificamos la consulta para buscar vehículos vendidos con estatus '1' (vendidos)
                    string query = @"
                SELECT v.id_vehiculo, v.nombre
                FROM Vehiculo v
                WHERE EXISTS (
                    SELECT 1 FROM Venta ve WHERE ve.id_vehiculo = v.id_vehiculo AND ve.estatus = 1
                )";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Llenar el ComboBox de vehículos vendidos
                    comboBoxVehiculosVendidoOs.DataSource = dt;
                    comboBoxVehiculosVendidoOs.DisplayMember = "nombre";
                    comboBoxVehiculosVendidoOs.ValueMember = "id_vehiculo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar vehículos vendidos: " + ex.Message);
            }
        }

        private void CargarEstatus()
        {
            // Asegúrate de que el ComboBox se llena correctamente
            comboBoxEstatus.Items.Clear();
            comboBoxEstatus.Items.Add(new { Text = "En progreso", Value = 1 });
            comboBoxEstatus.Items.Add(new { Text = "Terminado", Value = 0 });

            // Establecer el valor predeterminado (por ejemplo, "En progreso")
            comboBoxEstatus.SelectedIndex = 0;
        }


        private void CargarMantenimientosVehiculosVendidos()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta para obtener los mantenimientos de los vehículos vendidos
                    string query = @"
                SELECT m.id_mantenimiento, m.id_vehiculo, v.nombre AS vehiculo, 
                       m.id_mantenimiento_tipo, mt.nombre AS mantenimiento_tipo, 
                       m.fecha_inicio, m.fecha_proximo_mantenimiento, 
                       m.estatus
                FROM Mantenimiento m
                JOIN Vehiculo v ON m.id_vehiculo = v.id_vehiculo
                JOIN MantenimientoTipo mt ON m.id_mantenimiento_tipo = mt.id_mantenimiento_tipo
                JOIN Venta ve ON v.id_vehiculo = ve.id_vehiculo
                WHERE ve.estatus = 1";  // Solo los vehículos vendidos (estatus = 1)

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Llenar el DataGridView con los resultados
                    dataGridViewMantenimientosVendidos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los mantenimientos de vehículos vendidos: " + ex.Message);
            }
        }





        private void CargarVehiculos()
        {
            try
            {
                // Conectar a la base de datos
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Consultar los vehículos disponibles
                    string query = "SELECT id_vehiculo, nombre FROM Vehiculo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Llenar el ComboBox de vehículos
                    comboBoxVehiculosVendidos.DataSource = dt;
                    comboBoxVehiculosVendidos.DisplayMember = "nombre";
                    comboBoxVehiculosVendidos.ValueMember = "id_vehiculo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar vehículos: " + ex.Message);
            }
        }

        private void CargarTiposMantenimiento()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Usar el nombre correcto de la tabla
                    string query = "SELECT id_mantenimiento_tipo, nombre FROM MantenimientoTipo"; // Nombre correcto de la tabla
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asignar los datos al ComboBox
                    comboBoxMantenimientoTipo.DataSource = dt;
                    comboBoxMantenimientoTipo.DisplayMember = "nombre";
                    comboBoxMantenimientoTipo.ValueMember = "id_mantenimiento_tipo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los tipos de mantenimiento: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores seleccionados del formulario
                int idVehiculo = (int)comboBoxVehiculosVendidos.SelectedValue;
                int idMantenimientoTipo = (int)comboBoxMantenimientoTipo.SelectedValue;
                int idEmpleado = SessionData.currentUserId; // ID del empleado que está registrado
                DateTime fechaInicio = dateTimePickerInicio.Value;
                DateTime fechaProximoMantenimiento = dateTimePickerProximoMantenimiento.Value;

                // Declarar la variable estatus
                int estatus = -1;

                // Verificar que el ComboBox de estatus tiene un valor seleccionado
                if (comboBoxEstatus.SelectedItem != null)
                {
                    estatus = (int)((dynamic)comboBoxEstatus.SelectedItem).Value; // Obtener el valor seleccionado
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un estatus.");
                    return; // Salir del método si no se seleccionó un estatus
                }

                // Verificar si los datos son correctos
                if (idVehiculo > 0 && idMantenimientoTipo > 0)
                {
                    // Insertar el mantenimiento en la base de datos
                    string query = "INSERT INTO Mantenimiento (id_vehiculo, id_empleado, id_mantenimiento_tipo, fecha_inicio, fecha_proximo_mantenimiento, fecha_creacion, id_modificacion, fecha_modificacion, estatus) " +
                                   "VALUES (@idVehiculo, @idEmpleado, @idMantenimientoTipo, @fechaInicio, @fechaProximoMantenimiento, GETDATE(), @idEmpleado, GETDATE(), @estatus)";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        cmd.Parameters.AddWithValue("@idMantenimientoTipo", idMantenimientoTipo);
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaProximoMantenimiento", fechaProximoMantenimiento);
                        cmd.Parameters.AddWithValue("@estatus", estatus);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Mantenimiento agregado exitosamente.");

                        // Actualizar el DataGridView si es necesario
                        CargarMantenimiento();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, complete todos los campos correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el mantenimiento: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si se ha seleccionado un mantenimiento
                int idMantenimiento = int.Parse(labelIdMantenimiento.Text); // Obtener el id desde el Label

                if (idMantenimiento == 0)
                {
                    MessageBox.Show("Por favor, seleccione un mantenimiento para modificar.");
                    return;
                }

                // Obtener los valores modificados del formulario
                int idVehiculo = (int)comboBoxVehiculosVendidos.SelectedValue;
                int idMantenimientoTipo = (int)comboBoxMantenimientoTipo.SelectedValue;
                int idEmpleado = SessionData.currentUserId; // El ID del empleado que está registrado
                DateTime fechaInicio = dateTimePickerInicio.Value;
                DateTime fechaProximoMantenimiento = dateTimePickerProximoMantenimiento.Value;

                // Obtener el estatus seleccionado
                int estatus = (int)((dynamic)comboBoxEstatus.SelectedItem).Value;

                // Verificar si los datos son correctos
                if (idVehiculo > 0 && idMantenimientoTipo > 0 && idMantenimiento > 0)
                {
                    // Actualizar el mantenimiento en la base de datos
                    string query = "UPDATE Mantenimiento SET " +
                                   "id_vehiculo = @idVehiculo, " +
                                   "id_mantenimiento_tipo = @idMantenimientoTipo, " +
                                   "fecha_inicio = @fechaInicio, " +
                                   "fecha_proximo_mantenimiento = @fechaProximoMantenimiento, " +
                                   "id_empleado = @idEmpleado, " +
                                   "estatus = @estatus, " +
                                   "id_modificacion = @idEmpleado, " +
                                   "fecha_modificacion = GETDATE() " +
                                   "WHERE id_mantenimiento = @idMantenimiento";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idMantenimiento", idMantenimiento); // Usar el id del Label
                        cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                        cmd.Parameters.AddWithValue("@idMantenimientoTipo", idMantenimientoTipo);
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaProximoMantenimiento", fechaProximoMantenimiento);
                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        cmd.Parameters.AddWithValue("@estatus", estatus);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Mantenimiento modificado exitosamente.");

                        // Actualizar el DataGridView con los datos modificados
                        CargarMantenimiento();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, complete todos los campos correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el mantenimiento: " + ex.Message);
            }
        }

        private void dataGridViewMantenimiento_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewMantenimiento.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridViewMantenimiento.SelectedRows[0];

                // Cargar los valores de la fila seleccionada en los controles
                comboBoxVehiculosVendidos.SelectedValue = row.Cells["id_vehiculo"].Value;
                comboBoxMantenimientoTipo.SelectedValue = row.Cells["id_mantenimiento_tipo"].Value;
                dateTimePickerInicio.Value = (DateTime)row.Cells["fecha_inicio"].Value;
                dateTimePickerProximoMantenimiento.Value = (DateTime)row.Cells["fecha_proximo_mantenimiento"].Value;

                // Establecer el estatus en el ComboBox
                string estatus = row.Cells["estatus"].Value.ToString();
                comboBoxEstatus.SelectedIndex = estatus == "En progreso" ? 0 : 1;

                // Guardar el ID del mantenimiento seleccionado en el Label invisible
                labelIdMantenimiento.Text = row.Cells["id_mantenimiento"].Value.ToString(); // Usar un Label para almacenar el id_mantenimiento
            }
        }

        // MECANICO CARRO VENDIDO

        private void CargarEstatusVendido()
        {
            // Asegúrate de que el ComboBox se llena correctamente
            comboBoxEstatusVendido.Items.Clear();
            comboBoxEstatusVendido.Items.Add(new { Text = "En progreso", Value = 1 });
            comboBoxEstatusVendido.Items.Add(new { Text = "Terminado", Value = 0 });

            // Establecer el valor predeterminado (por ejemplo, "En progreso")
            comboBoxEstatusVendido.SelectedIndex = 0;
        }

        private void CargarMantenimientoVendido()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_mantenimiento_tipo, nombre FROM MantenimientoTipo"; // Aquí ajustamos el nombre de la tabla
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    comboBoxMantenimientoTipoVendido.DataSource = dt;
                    comboBoxMantenimientoTipoVendido.DisplayMember = "nombre";
                    comboBoxMantenimientoTipoVendido.ValueMember = "id_mantenimiento_tipo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los tipos de mantenimiento: " + ex.Message);
            }
        }

       


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores seleccionados del formulario
                int idVehiculo = (int)comboBoxVehiculosVendidoOs.SelectedValue;
                int idMantenimientoTipo = (int)comboBoxMantenimientoTipoVendido.SelectedValue;
                int idEmpleado = SessionData.currentUserId; // ID del empleado que está registrado
                DateTime fechaInicio = dateTimePickerProximoMantenimientoVendido.Value;
                DateTime fechaProximoMantenimiento = dateTimePickerProximoMantenimientoVendido.Value;

                // Definir la variable estatus fuera del bloque if
                int estatus;

                // Verificar que el ComboBox de estatus tiene un valor seleccionado
                if (comboBoxEstatusVendido.SelectedItem != null)
                {
                    estatus = (int)((dynamic)comboBoxEstatusVendido.SelectedItem).Value; // Obtener el valor seleccionado
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un estatus.");
                    return; // Salir del método si no se seleccionó un estatus
                }

                // Verificar si los datos son correctos
                if (idVehiculo > 0 && idMantenimientoTipo > 0)
                {
                    // Insertar el mantenimiento en la nueva tabla de mantenimiento para vehículos vendidos
                    string query = "INSERT INTO Mantenimiento_Vendido (id_vehiculo, id_empleado, id_mantenimiento_tipo, fecha_inicio, fecha_proximo_mantenimiento, fecha_creacion, id_modificacion, fecha_modificacion, estatus) " +
                                   "VALUES (@idVehiculo, @idEmpleado, @idMantenimientoTipo, @fechaInicio, @fechaProximoMantenimiento, GETDATE(), @idEmpleado, GETDATE(), @estatus)";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        cmd.Parameters.AddWithValue("@idMantenimientoTipo", idMantenimientoTipo);
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaProximoMantenimiento", fechaProximoMantenimiento);
                        cmd.Parameters.AddWithValue("@estatus", estatus);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Mantenimiento agregado exitosamente.");

                        // Actualizar el DataGridView si es necesario
                        CargarMantenimientoVendido();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, complete todos los campos correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el mantenimiento: " + ex.Message);
            }
        }

        private void dataGridViewMantenimientosVendidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewMantenimientosVendidos.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridViewMantenimientosVendidos.SelectedRows[0];

                // Cargar los valores de la fila seleccionada en los controles
                comboBoxVehiculosVendidos.SelectedValue = row.Cells["id_vehiculo"].Value;
                comboBoxMantenimientoTipo.SelectedValue = row.Cells["id_mantenimiento_tipo"].Value;
                dateTimePickerProximoMantenimientoVendido.Value = (DateTime)row.Cells["fecha_inicio"].Value;
                dateTimePicker1.Value = (DateTime)row.Cells["fecha_proximo_mantenimiento"].Value;

                // Establecer el estatus en el ComboBox (En progreso o Terminado)
                var estatusValue = row.Cells["estatus"].Value;
                if (estatusValue != DBNull.Value)
                {
                    int estatus = Convert.ToInt32(estatusValue);  // Usamos Convert.ToInt32 para manejar correctamente el tipo de datos
                    comboBoxEstatusVendido.SelectedIndex = estatus == 1 ? 0 : 1; // Asume 1 -> "En progreso", 0 -> "Terminado"
                }
                else
                {
                    // Si estatus es DBNull o no está disponible, se puede manejar de otra forma
                    comboBoxEstatusVendido.SelectedIndex = -1; // O cualquier valor predeterminado
                }

                // Guardar el ID del mantenimiento seleccionado (esto nos ayudará para la actualización)
                label3.Text = row.Cells["id_mantenimiento"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores seleccionados de los controles
                int idMantenimiento = int.Parse(label3.Text); // ID del mantenimiento
                int idVehiculo = (int)comboBoxVehiculosVendidos.SelectedValue;
                int idMantenimientoTipo = (int)comboBoxMantenimientoTipo.SelectedValue;
                DateTime fechaInicio = dateTimePickerProximoMantenimientoVendido.Value;
                DateTime fechaProximoMantenimiento = dateTimePicker1.Value;

                // Obtener el estatus
                int estatus = (int)((dynamic)comboBoxEstatusVendido.SelectedItem).Value;

                // Verificar que todos los datos estén completos
                if (idVehiculo > 0 && idMantenimientoTipo > 0)
                {
                    // Actualizar el mantenimiento en la base de datos
                    string query = "UPDATE Mantenimiento " +
                                   "SET id_vehiculo = @idVehiculo, id_mantenimiento_tipo = @idMantenimientoTipo, " +
                                   "fecha_inicio = @fechaInicio, fecha_proximo_mantenimiento = @fechaProximoMantenimiento, " +
                                   "estatus = @estatus, id_modificacion = @idEmpleado, fecha_modificacion = GETDATE() " +
                                   "WHERE id_mantenimiento = @idMantenimiento";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                        cmd.Parameters.AddWithValue("@idMantenimientoTipo", idMantenimientoTipo);
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaProximoMantenimiento", fechaProximoMantenimiento);
                        cmd.Parameters.AddWithValue("@estatus", estatus);
                        cmd.Parameters.AddWithValue("@idEmpleado", SessionData.currentUserId); // Usamos el ID del empleado actual
                        cmd.Parameters.AddWithValue("@idMantenimiento", idMantenimiento);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Mantenimiento actualizado exitosamente.");

                        // Actualizar el DataGridView
                        CargarMantenimientosVehiculosVendidos();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, complete todos los campos correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el mantenimiento: " + ex.Message);
            }
        }
    }
}
