using System;
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
    public partial class Vendedor : Form
    {
        string connectionString = "Data Source=eduardomv\\SQLEXPRESS;Initial Catalog=TESTING_DB;Integrated Security=True";
        private int currentClientId = 0;


        public Vendedor()
        {
            InitializeComponent();
            CargarEstatus();
            CargarClientes();
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void CargarEstatus()
        {
            cmbEstatus.Items.Add("Activo");
            cmbEstatus.Items.Add("Inactivo");
            cmbEstatus.SelectedIndex = 0; // Por defecto, seleccionar "Activo"
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que los campos obligatorios no estén vacíos
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtTelefono.Text))
                {
                    MessageBox.Show("Por favor, complete los campos obligatorios.");
                    return;
                }

                // Validar el formato del correo
                if (!txtCorreo.Text.Contains("@"))
                {
                    MessageBox.Show("Por favor, ingrese un correo válido.");
                    return;
                }

                // Datos del cliente
                string nombre = txtNombre.Text;
                string correo = txtCorreo.Text;
                string telefono = txtTelefono.Text;
                string direccion = txtDireccion.Text;
                string rfc = txtRFC.Text;
                string estatus = cmbEstatus.SelectedItem.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Insertar el cliente en la base de datos
                    string query = "INSERT INTO Cliente (nombre, email, telefono, direccion, rfc, estatus, id_creacion, fecha_creacion) " +
                                   "VALUES (@nombre, @email, @telefono, @direccion, @rfc, @estatus, @idCreacion, @fechaCreacion)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@email", correo);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.Parameters.AddWithValue("@rfc", rfc);
                        cmd.Parameters.AddWithValue("@estatus", estatus);
                        cmd.Parameters.AddWithValue("@idCreacion", SessionData.currentUserId); // ID del usuario actual
                        cmd.Parameters.AddWithValue("@fechaCreacion", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cliente agregado exitosamente.");
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el cliente: " + ex.Message);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtRFC.Clear();
            cmbEstatus.SelectedIndex = 0; // Restablecer a "Activo"
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }


        private void CargarClientes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    id_cliente, 
                    nombre, 
                    apellido, 
                    email, 
                    telefono, 
                    direccion, 
                    rfc, 
                    fecha_creacion, 
                    id_creacion, 
                    fecha_modificacion, 
                    id_modificacion, 
                    estatus 
                FROM Cliente";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dtClientes = new DataTable();
                    adapter.Fill(dtClientes);

                    dataGridView1.DataSource = dtClientes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentClientId == 0)
                {
                    MessageBox.Show("Seleccione un cliente para modificar.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                UPDATE Cliente 
                SET 
                    nombre = @nombre, 
                    apellido = @apellido, 
                    email = @email, 
                    telefono = @telefono, 
                    direccion = @direccion, 
                    rfc = @rfc, 
                    estatus = @estatus, 
                    fecha_modificacion = @fechaModificacion, 
                    id_modificacion = @idModificacion 
                WHERE id_cliente = @idCliente";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@apellido", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@email", txtCorreo.Text);
                        cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                        cmd.Parameters.AddWithValue("@rfc", txtRFC.Text);
                        cmd.Parameters.AddWithValue("@estatus", cmbEstatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@fechaModificacion", DateTime.Now);
                        cmd.Parameters.AddWithValue("@idModificacion", Login.SessionData.currentUserId);
                        cmd.Parameters.AddWithValue("@idCliente", currentClientId);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cliente modificado exitosamente.");
                CargarClientes(); // Actualizar DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el cliente: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtNombre.Text = row.Cells["nombre"].Value.ToString();
                txtApellido.Text = row.Cells["apellido"].Value.ToString();
                txtCorreo.Text = row.Cells["email"].Value.ToString();
                txtTelefono.Text = row.Cells["telefono"].Value.ToString();
                txtDireccion.Text = row.Cells["direccion"].Value.ToString();
                txtRFC.Text = row.Cells["rfc"].Value.ToString();
                cmbEstatus.SelectedItem = row.Cells["estatus"].Value.ToString();

                // Guardar el ID del cliente actual para modificar
                currentClientId = Convert.ToInt32(row.Cells["id_cliente"].Value);
            }
        }

        




        // Cargar clientes en el ComboBox
        private void CargarClientesVenta()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_cliente, CONCAT(nombre, ' ', apellido) AS Cliente FROM Cliente WHERE estatus = 'Activo'";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    comboBoxClientes.DataSource = dt;
                    comboBoxClientes.DisplayMember = "Cliente";
                    comboBoxClientes.ValueMember = "id_cliente";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message);
            }
        }

        private void RegistrarVenta()
        {
            try
            {
                // Verificar que los ComboBox no tengan valores nulos
                if (comboBoxVehiculo.SelectedValue == null || comboBoxClientes.SelectedValue == null ||
                    comboBoxMetodoPago.SelectedItem == null || comboBoxGarantia.SelectedValue == null)
                {
                    MessageBox.Show("Por favor, complete todos los campos.");
                    return;
                }

                // Obtener los valores seleccionados de los ComboBox
                int idVehiculo = (int)comboBoxVehiculo.SelectedValue;
                int idCliente = (int)comboBoxClientes.SelectedValue;
                string metodoPago = comboBoxMetodoPago.SelectedItem.ToString();
                int idGarantia = (int)comboBoxGarantia.SelectedValue;

                // Obtener el precio del vehículo
                decimal precio = ObtenerPrecioVehiculo(idVehiculo); // Asegúrate de que esta función devuelve un valor decimal válido

                // Validar que el precio no sea nulo o inválido
                if (precio <= 0)
                {
                    MessageBox.Show("El precio del vehículo es inválido.");
                    return;
                }

                // Asignar valores estáticos para empleado y creación (pueden venir de tu sistema de autenticación)
                int idEmpleado = 1;  // Asigna el ID del empleado actual (según el sistema de autenticación)
                int idCreacion = 1;  // Asigna el ID de la persona que está creando el registro
                string estatus = "Activo";  // Define el estado de la venta (puede ser "Activo", "Pendiente", etc.)

                // Imprimir los valores obtenidos para depuración (puedes remover esta línea después de probar)
                Console.WriteLine($"ID Vehículo: {idVehiculo}, ID Cliente: {idCliente}, Método de Pago: {metodoPago}, ID Garantía: {idGarantia}");
                Console.WriteLine($"Precio: {precio}");

                // Insertar los datos en la tabla Venta
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                INSERT INTO Venta (
                    id_cliente, 
                    id_empleado, 
                    id_vehiculo, 
                    metodo_pago, 
                    precio, 
                    garantia, 
                    id_creacion, 
                    fecha_creacion, 
                    id_modificacion, 
                    fecha_modificacion, 
                    estatus
                ) 
                VALUES (
                    @id_cliente, 
                    @id_empleado, 
                    @id_vehiculo, 
                    @metodo_pago, 
                    @precio, 
                    @garantia, 
                    @id_creacion, 
                    GETDATE(), 
                    @id_modificacion, 
                    GETDATE(), 
                    @estatus
                )";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@id_empleado", idEmpleado);
                    cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo);
                    cmd.Parameters.AddWithValue("@metodo_pago", metodoPago);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@garantia", idGarantia != 0 ? idGarantia.ToString() : DBNull.Value.ToString());  // Garantía como texto (si es nulo, pasa DBNull)
                    cmd.Parameters.AddWithValue("@id_creacion", idCreacion);
                    cmd.Parameters.AddWithValue("@id_modificacion", DBNull.Value);  // Inicialmente, no hay modificaciones
                    cmd.Parameters.AddWithValue("@estatus", estatus);

                    // Ejecutar el comando
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Venta registrada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la venta: " + ex.Message);
            }
        }

        private decimal ObtenerPrecioVehiculo(int idVehiculo)
        {
            // Este método debería devolver el precio del vehículo basado en el ID.
            // Suponiendo que hay una tabla Vehiculos con el campo 'precio'.
            decimal precio = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT precio FROM Vehiculo WHERE id_vehiculo = @idVehiculo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                var result = cmd.ExecuteScalar();

                // Verificar que el resultado no sea nulo y sea un valor decimal
                if (result != DBNull.Value)
                {
                    precio = Convert.ToDecimal(result);
                }
                else
                {
                    throw new Exception("No se pudo obtener el precio del vehículo.");
                }
            }
            return precio;
        }

        private void CargarVentas()
        {
            try
            {
                // Crear la conexión y la consulta SQL
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_venta, id_cliente, id_empleado, id_vehiculo, metodo_pago, precio, garantia, " +
                                   "id_creacion, fecha_creacion, id_modificacion, fecha_modificacion, estatus " +
                                   "FROM Venta";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asignar la DataTable como origen de datos para el DataGridView
                    dataGridViewVentas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las ventas: " + ex.Message);
            }
        }


        private void ActualizarCantidadVehiculo(int idVehiculo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Vehiculo SET cantidad = cantidad - 1 WHERE id_vehiculo = @id_vehiculo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la cantidad del vehículo: " + ex.Message);
            }
        }


        private void CargarGarantias()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_tipo_garantia, nombre FROM Garantia";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    comboBoxGarantia.DataSource = dt;
                    comboBoxGarantia.DisplayMember = "nombre";
                    comboBoxGarantia.ValueMember = "id_tipo_garantia";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las garantías: " + ex.Message);
            }
        }




      
        private void CargarMetodosDePago()
        {
            List<string> metodosPago = new List<string> { "Efectivo", "Tarjeta", "Transferencia" };
            comboBoxMetodoPago.DataSource = metodosPago;
        }

        private void CargarVehiculosDisponibles()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Obtén solo nombres únicos de los vehículos
                    string query = "SELECT DISTINCT nombre FROM Vehiculo WHERE cantidad > 0";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asigna los datos al ComboBox de vehículos
                    comboBoxVehiculo.DataSource = dt;
                    comboBoxVehiculo.DisplayMember = "nombre";  // Mostrar solo el nombre
                    comboBoxVehiculo.ValueMember = "nombre";   // El valor será el nombre del vehículo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los vehículos: " + ex.Message);
            }
        }










        private void button3_Click(object sender, EventArgs e)
        {
            RegistrarVenta();
        }

        private void Vendedor_Load(object sender, EventArgs e)
        {
            CargarGarantias();
            CargarVehiculosDisponibles();
            CargarClientesVenta();
            CargarMetodoDePago();
            CargarVentas(); 
        }

        private void comboBoxVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxVehiculo.SelectedValue != null)
            {
                string nombreVehiculo = comboBoxVehiculo.SelectedValue.ToString();

                // Llamar a los métodos para cargar los detalles del vehículo seleccionado
                CargarModelos(nombreVehiculo);
                CargarMarcas(nombreVehiculo);
                CargarTipos(nombreVehiculo);
                CargarColores(nombreVehiculo);
            }
        }




        private void CargarModelos(string nombreVehiculo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT m.id_modelo, m.nombre FROM Modelos m " +
                                   "JOIN Vehiculo v ON v.modelo_id = m.id_modelo " +
                                   "WHERE v.nombre = @nombreVehiculo";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombreVehiculo", nombreVehiculo);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    comboBoxModelo.DataSource = dt;
                    comboBoxModelo.DisplayMember = "nombre";
                    comboBoxModelo.ValueMember = "id_modelo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los modelos: " + ex.Message);
            }
        }


        private void CargarMarcas(string nombreVehiculo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT ma.id_marca, ma.nombre FROM Marcas ma " +
                                   "JOIN Vehiculo v ON v.marca_id = ma.id_marca " +
                                   "WHERE v.nombre = @nombreVehiculo";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombreVehiculo", nombreVehiculo);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    comboBoxMarca.DataSource = dt;
                    comboBoxMarca.DisplayMember = "nombre";
                    comboBoxMarca.ValueMember = "id_marca";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las marcas: " + ex.Message);
            }
        }


        private void CargarTipos(string nombreVehiculo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT t.id_tipo_vehiculo, t.nombre FROM TipoVehiculo t " +
                                   "JOIN Vehiculo v ON v.tipo_id = t.id_tipo_vehiculo " +
                                   "WHERE v.nombre = @nombreVehiculo";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombreVehiculo", nombreVehiculo);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    comboBoxTipo.DataSource = dt;
                    comboBoxTipo.DisplayMember = "nombre";
                    comboBoxTipo.ValueMember = "id_tipo_vehiculo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los tipos: " + ex.Message);
            }
        }


        private void CargarColores(string nombreVehiculo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT c.id_color, c.nombre FROM Colores c " +
                                   "JOIN Vehiculo v ON v.color_id = c.id_color " +
                                   "WHERE v.nombre = @nombreVehiculo";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombreVehiculo", nombreVehiculo);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    comboBoxColor.DataSource = dt;
                    comboBoxColor.DisplayMember = "nombre";
                    comboBoxColor.ValueMember = "id_color";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los colores: " + ex.Message);
            }
        }


        private void CargarMetodoDePago()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_metodo_pago, nombre FROM MetodoPago";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    comboBoxMetodoPago.DataSource = dt;
                    comboBoxMetodoPago.DisplayMember = "nombre";
                    comboBoxMetodoPago.ValueMember = "id_metodo_pago";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los métodos de pago: " + ex.Message);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }


}
