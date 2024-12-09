using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static PROYECTO_5TO_SEMESTRE.Login;

namespace PROYECTO_5TO_SEMESTRE
{
    public partial class Contratar : Form
    {

        // Debes declarar esta variable a nivel de clase
        private int currentEmployeeId = 0;
        private int currentVehiculoId = 0;


        public Contratar()
        {
            InitializeComponent();
        }



        // Método para encriptar la contraseña utilizando SHA256
        private string EncriptarContraseña(string contraseña)
        {
            // Crear una instancia del algoritmo SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir la contraseña en un array de bytes
                byte[] bytes = Encoding.UTF8.GetBytes(contraseña);

                // Aplicar el algoritmo SHA256
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Convertir el hash a una cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));  // "x2" es para representar cada byte como un valor hexadecimal
                }

                // Devolver la contraseña encriptada
                return sb.ToString();
            }
        }



        // Conexión a la base de datos
        string connectionString = "Data Source=eduardomv\\SQLEXPRESS;Initial Catalog=TESTING_DB;Integrated Security=True";

        private void Contratar_Load(object sender, EventArgs e)
        {
            // Cargar roles en el ComboBox
            LoadRoles();

            // Cargar turnos en el ComboBox
            LoadTurnos();

            CargarEmpleados();

            CargarComboBox();

            ConfigurarNumericUpDown();

            ConfigurarDateTimePicker();

            CargarVehiculos();

            ConfigurarComboBoxEstatus();
        }

        // Método que carga los datos de los empleados al DataGridView
        private void CargarEmpleados()
        {
            try
            {
                // Crear la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Consulta para obtener los datos de la tabla EMPLEADO
                    string query = "SELECT id_empleado, nombre, id_rol, id_turno, correo, telefono, fecha_contratacion, salario, estatus FROM EMPLEADO";

                    // Crear el adaptador de datos
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Crear un DataTable para almacenar los datos
                    DataTable dataTable = new DataTable();

                    // Llenar el DataTable con los datos de la base de datos
                    dataAdapter.Fill(dataTable);

                    // Asignar el DataTable al DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message);
            }
        }



        private void LoadRoles()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter daRoles = new SqlDataAdapter("SELECT id_rol, nombre FROM ROL", conn);
                DataTable dtRoles = new DataTable();
                daRoles.Fill(dtRoles);

                cmbRol.DisplayMember = "nombre";  // Lo que se verá en el ComboBox
                cmbRol.ValueMember = "id_rol";   // Lo que se usa como valor interno
                cmbRol.DataSource = dtRoles;
            }
        }

        private void LoadTurnos()
        {
            // Conexión a la base de datos
            string query = "SELECT id_turno, nombre FROM TURNO";  // Asegúrate de que la tabla es 'TURNO' en mayúsculas

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dtTurnos = new DataTable();
                dataAdapter.Fill(dtTurnos);

                // Asignar el DataSource y configurar ValueMember y DisplayMember
                cmbTurno.DataSource = dtTurnos;
                cmbTurno.ValueMember = "id_turno"; // Este es el valor que se devolverá
                cmbTurno.DisplayMember = "nombre"; // Este es el texto que se mostrará en el ComboBox
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }






        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRol.SelectedValue != null)
            {
                // Obtener el id_rol seleccionado
                int rolId = Convert.ToInt32(cmbRol.SelectedValue);

                // Conexión a la base de datos
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Obtener el salario del rol seleccionado
                    string query = "SELECT salario FROM ROL WHERE id_rol = @idRol";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idRol", rolId);

                        // Ejecutar la consulta y obtener el salario
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            decimal salario = Convert.ToDecimal(result);
                            lblSalario.Text = "Salario: " + salario.ToString("C");
                        }
                    }
                }
            }
        }

        private void cmbTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTurno.SelectedValue != null)
            {
                try
                {
                    // Verificar el valor seleccionado
                    Console.WriteLine($"SelectedValue: {cmbTurno.SelectedValue}");
                    int turnoId = Convert.ToInt32(cmbTurno.SelectedValue);  // Convertir a int

                    // Consulta para obtener la descripción del turno seleccionado
                    string query = "SELECT descripcion FROM TURNO WHERE id_turno = @turnoId";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@turnoId", turnoId);

                        connection.Open();
                        var result = command.ExecuteScalar();

                        if (result != null)
                        {
                            lblTurno.Text = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Mostrar error en la consola
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Obtener los datos del formulario
                string nombre = txtNombre.Text;
                string correo = txtEmail.Text;
                string telefono = txtTelefono.Text;
                int idRol = Convert.ToInt32(cmbRol.SelectedValue);
                int idTurno = Convert.ToInt32(cmbTurno.SelectedValue);
                decimal salario = Convert.ToDecimal(lblSalario.Text.Replace("Salario: ", "").Replace("$", "").Replace(",", ""));
                string contraseñaEncriptada = EncriptarContraseña(txtContraseña.Text);

                // Aquí ya no es necesario guardar la imagen
                // El valor de "foto" puede ser nulo o vacío si ya no se maneja la imagen
                object foto = DBNull.Value;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta para insertar el nuevo empleado sin imagen
                    string query = "INSERT INTO EMPLEADO (nombre, correo, contraseña, id_rol, id_turno, telefono, salario, fecha_contratacion, estatus, id_creacion, id_modificacion) " +
                                   "VALUES (@nombre, @correo, @contraseña, @idRol, @idTurno, @telefono, @salario, @fechaContratacion, 1, @idCreacion, @idModificacion)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contraseña", contraseñaEncriptada);
                        cmd.Parameters.AddWithValue("@idRol", idRol);
                        cmd.Parameters.AddWithValue("@idTurno", idTurno);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@salario", salario);
                        cmd.Parameters.AddWithValue("@fechaContratacion", DateTime.Now);
                        cmd.Parameters.AddWithValue("@idCreacion", SessionData.currentUserId); // Asignar el id del jefe autenticado
                        cmd.Parameters.AddWithValue("@idModificacion", SessionData.currentUserId); // Inicialmente, el id de modificación es el mismo

                        cmd.ExecuteNonQuery(); // Ejecutar la consulta
                    }

                    MessageBox.Show("Empleado registrado exitosamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el empleado: " + ex.Message);
            }
        }



        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtEmail.Clear();
            txtContraseña.Clear();
            txtTelefono.Clear();
            cmbRol.SelectedIndex = -1;
            cmbTurno.SelectedIndex = -1;
            lblSalario.Text = "Salario: $0.00"; // Resetea el salario mostrado
        }




        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat); // Guarda la imagen en el MemoryStream en su formato original
                return ms.ToArray();  // Retorna el array de bytes
            }
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CargarEmpleados();
        }







        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegúrate de que no se esté seleccionando la fila de encabezado
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Rellenar los demás campos
                txtNombre.Text = row.Cells["nombre"].Value.ToString();
                txtEmail.Text = row.Cells["correo"].Value.ToString();
                txtTelefono.Text = row.Cells["telefono"].Value.ToString();
                cmbRol.SelectedValue = row.Cells["id_rol"].Value;
                cmbTurno.SelectedValue = row.Cells["id_turno"].Value;
                lblSalario.Text = "Salario: " + row.Cells["salario"].Value.ToString();

                // Configurar el ComboBox de Estatus
                cmbEstatus.SelectedValue = Convert.ToInt32(row.Cells["estatus"].Value);

                // Guardar el ID del empleado para usarlo al actualizar
                currentEmployeeId = Convert.ToInt32(row.Cells["id_empleado"].Value);
            }
        }

        private void ConfigurarComboBoxEstatus()
        {
            // Crear una tabla para el ComboBox de estatus
            DataTable estatusTable = new DataTable();
            estatusTable.Columns.Add("Value", typeof(int));
            estatusTable.Columns.Add("Text", typeof(string));

            // Agregar las opciones de estatus
            estatusTable.Rows.Add(1, "Activo");
            estatusTable.Rows.Add(0, "Inactivo");

            // Configurar el ComboBox
            cmbEstatus.DataSource = estatusTable;
            cmbEstatus.DisplayMember = "Text";  // Mostrar "Activo" o "Inactivo"
            cmbEstatus.ValueMember = "Value";  // Usar 1 o 0 como valor
            cmbEstatus.SelectedIndex = -1; // No seleccionar ninguna opción al inicio
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que los campos requeridos no estén vacíos
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtTelefono.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.");
                    return;
                }

                // Obtener los datos desde los controles
                string nombre = txtNombre.Text;
                string correo = txtEmail.Text;
                string telefono = txtTelefono.Text;
                int idRol = Convert.ToInt32(cmbRol.SelectedValue);
                int idTurno = Convert.ToInt32(cmbTurno.SelectedValue);
                decimal salario = Convert.ToDecimal(lblSalario.Text.Replace("Salario: ", "").Replace("$", "").Replace(",", ""));
                int estatus = Convert.ToInt32(cmbEstatus.SelectedValue); // Obtener el estatus seleccionado

                // Crear la conexión a la base de datos
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta para actualizar el empleado
                    string query = @"
            UPDATE EMPLEADO 
            SET nombre = @nombre, 
                correo = @correo, 
                id_rol = @idRol, 
                id_turno = @idTurno, 
                telefono = @telefono, 
                salario = @salario, 
                estatus = @estatus, 
                id_modificacion = @idModificacion, 
                fecha_modificacion = @fechaModificacion 
            WHERE id_empleado = @idEmpleado";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@idRol", idRol);
                        cmd.Parameters.AddWithValue("@idTurno", idTurno);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@salario", salario);
                        cmd.Parameters.AddWithValue("@estatus", estatus); // Actualizar el estatus
                        cmd.Parameters.AddWithValue("@idModificacion", SessionData.currentUserId); // Asignar el ID del usuario que modifica
                        cmd.Parameters.AddWithValue("@fechaModificacion", DateTime.Now); // Fecha de modificación
                        cmd.Parameters.AddWithValue("@idEmpleado", currentEmployeeId); // El ID del empleado a modificar

                        // Ejecutar la consulta
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Empleado modificado exitosamente.");
                CargarEmpleados(); // Refrescar la lista
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el empleado: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que los campos requeridos no estén vacíos
                if (string.IsNullOrEmpty(txtNombreAuto.Text) || cmbMarca.SelectedIndex == -1 || cmbModelo.SelectedIndex == -1 ||
                    cmbColor.SelectedIndex == -1 || cmbTipoVehiculo.SelectedIndex == -1 || numericUpDownCantidad.Value <= 0 || numericUpDownPrecio.Value <= 0)
                {
                    MessageBox.Show("Por favor, complete todos los campos.");
                    return;
                }

                // Obtener los datos desde los controles
                string nombre = txtNombreAuto.Text;
                int marcaId = Convert.ToInt32(cmbMarca.SelectedValue);
                int modeloId = Convert.ToInt32(cmbModelo.SelectedValue);
                int colorId = Convert.ToInt32(cmbColor.SelectedValue);
                int tipoId = Convert.ToInt32(cmbTipoVehiculo.SelectedValue);
                int cantidad = (int)numericUpDownCantidad.Value;
                decimal precio = numericUpDownPrecio.Value;
                string descripcion = txtDescripcion.Text;
                DateTime año = dateTimePickerAño.Value;

                // Crear la conexión a la base de datos
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta para insertar el nuevo vehículo
                    string query = "INSERT INTO Vehiculo (nombre, modelo_id, marca_id, color_id, tipo_id, cantidad, precio, descripcion, año, fecha_registro) " +
                                   "VALUES (@nombre, @modeloId, @marcaId, @colorId, @tipoId, @cantidad, @precio, @descripcion, @año, @fechaRegistro)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@modeloId", modeloId);
                        cmd.Parameters.AddWithValue("@marcaId", marcaId);
                        cmd.Parameters.AddWithValue("@colorId", colorId);
                        cmd.Parameters.AddWithValue("@tipoId", tipoId);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@año", año);
                        cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now);

                        // Ejecutar la consulta
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Vehículo agregado exitosamente.");
                LimpiarFormulario(); // Limpiar el formulario después de agregar
                CargarVehiculos(); // Refrescar la vista del DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el vehículo: " + ex.Message);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombreAuto.Clear();
            cmbMarca.SelectedIndex = -1;
            cmbModelo.SelectedIndex = -1;
            cmbColor.SelectedIndex = -1;
            cmbTipoVehiculo.SelectedIndex = -1;
            numericUpDownCantidad.Value = 0;
            numericUpDownPrecio.Value = 0;
            txtDescripcion.Clear();
            dateTimePickerAño.Value = DateTime.Now;
        }


        private void CargarVehiculos()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Consulta para cargar vehículos con los IDs y nombres descriptivos
                    string query = @"
                SELECT 
                    v.id_vehiculo, 
                    v.nombre, 
                    v.modelo_id, 
                    mo.nombre AS modelo, 
                    v.marca_id, 
                    ma.nombre AS marca, 
                    v.color_id, 
                    c.nombre AS color, 
                    v.tipo_id, 
                    t.nombre AS tipo, 
                    v.cantidad, 
                    v.precio, 
                    v.descripcion, 
                    v.año, 
                    v.fecha_registro
                FROM Vehiculo v
                JOIN Modelos mo ON v.modelo_id = mo.id_modelo
                JOIN Marcas ma ON v.marca_id = ma.id_marca
                JOIN Colores c ON v.color_id = c.id_color
                JOIN TipoVehiculo t ON v.tipo_id = t.id_tipo_vehiculo";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asignar el DataTable al DataGridView
                    dataGridViewVehiculos.DataSource = dt;
                }

                LimpiarFormulario(); // Limpiar los campos después de cargar los vehículos
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los vehículos: " + ex.Message);
            }
        }









        private void CargarComboBox()
        {
            try
            {
                // Llenar el ComboBox de Marcas
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_marca, nombre FROM MARCAS";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable marcas = new DataTable();
                    adapter.Fill(marcas);
                    cmbMarca.DisplayMember = "nombre";
                    cmbMarca.ValueMember = "id_marca";
                    cmbMarca.DataSource = marcas;
                }

                // Llenar el ComboBox de Modelos
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_modelo, nombre FROM MODELOS";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable modelos = new DataTable();
                    adapter.Fill(modelos);
                    cmbModelo.DisplayMember = "nombre";
                    cmbModelo.ValueMember = "id_modelo";
                    cmbModelo.DataSource = modelos;
                }

                // Llenar el ComboBox de Tipos de Vehículo
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_tipo_vehiculo, nombre FROM TipoVehiculo";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable tiposVehiculo = new DataTable();
                    adapter.Fill(tiposVehiculo);
                    cmbTipoVehiculo.DisplayMember = "nombre";
                    cmbTipoVehiculo.ValueMember = "id_tipo_vehiculo";
                    cmbTipoVehiculo.DataSource = tiposVehiculo;
                }

                // Llenar el ComboBox de Colores
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_color, nombre FROM COLORES    ";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable colores = new DataTable();
                    adapter.Fill(colores);
                    cmbColor.DisplayMember = "nombre";
                    cmbColor.ValueMember = "id_color";
                    cmbColor.DataSource = colores;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los ComboBox: " + ex.Message);
            }
        }


        private void ConfigurarDateTimePicker()
        {
            // Configurar el DateTimePicker para solo seleccionar el año
            dateTimePickerAño.Format = DateTimePickerFormat.Custom;
            dateTimePickerAño.CustomFormat = "yyyy"; // Solo mostrar el año
            dateTimePickerAño.ShowUpDown = true; // Usar botones para cambiar el año
            dateTimePickerAño.Value = DateTime.Now; // Por defecto, el año actual
        }

        private void ConfigurarNumericUpDown()
        {
            // Configurar el NumericUpDown para el precio
            numericUpDownPrecio.Minimum = 0; // Precio mínimo
            numericUpDownPrecio.Maximum = 1000000; // Precio máximo
            numericUpDownPrecio.DecimalPlaces = 2; // Permitirá dos decimales
            numericUpDownPrecio.Increment = 1000; // Incremento de 1000 en el precio
        }



        private void tabPage2_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay filas seleccionadas en el DataGridView
                if (dataGridViewVehiculos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Por favor, seleccione un vehículo para modificar.");
                    return;
                }

                // Obtener los valores modificados del formulario
                string nombre = txtNombreAuto.Text;
                int modeloId = Convert.ToInt32(cmbModelo.SelectedValue); // Suponiendo que usas ComboBox para modelo
                int marcaId = Convert.ToInt32(cmbMarca.SelectedValue); // Suponiendo que usas ComboBox para marca
                int colorId = Convert.ToInt32(cmbColor.SelectedValue); // Suponiendo que usas ComboBox para color
                int tipoId = Convert.ToInt32(cmbTipoVehiculo.SelectedValue); // Suponiendo que usas ComboBox para tipo
                int cantidad = (int)numericUpDownCantidad.Value; // Usando NumericUpDown para cantidad
                decimal precio = numericUpDownPrecio.Value; // Usando NumericUpDown para precio 
                string descripcion = txtDescripcion.Text;

                // Obtener el año de la fecha seleccionada en el DateTimePicker
                int año = dateTimePickerAño.Value.Year;

                // Crear una fecha válida con solo el año (ejemplo: 01-01-2022)
                DateTime fecha = new DateTime(año, 1, 1);

                // Obtener el id_vehiculo de la fila seleccionada
                int vehiculoId = Convert.ToInt32(dataGridViewVehiculos.SelectedRows[0].Cells["id_vehiculo"].Value);

                // Conexión a la base de datos
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta SQL para actualizar el vehículo
                    string query = "UPDATE Vehiculo SET " +
                                   "nombre = @nombre, " +
                                   "modelo_id = @modeloId, " +
                                   "marca_id = @marcaId, " +
                                   "color_id = @colorId, " +
                                   "tipo_id = @tipoId, " +
                                   "cantidad = @cantidad, " +
                                   "precio = @precio, " +
                                   "descripcion = @descripcion, " +
                                   "año = @año " +
                                   "WHERE id_vehiculo = @idVehiculo";

                    // Comando SQL
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Añadir los parámetros a la consulta
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@modeloId", modeloId);
                        cmd.Parameters.AddWithValue("@marcaId", marcaId);
                        cmd.Parameters.AddWithValue("@colorId", colorId);
                        cmd.Parameters.AddWithValue("@tipoId", tipoId);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@año", fecha); // Pasamos la fecha completa (01-01-año)
                        cmd.Parameters.AddWithValue("@idVehiculo", vehiculoId);

                        // Ejecutar la consulta de actualización
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Vehículo modificado exitosamente.");
                CargarVehiculos(); // Recargar los vehículos para reflejar los cambios
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el vehículo: " + ex.Message);
            }
        }

        private void dataGridViewVehiculos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegúrate de que no se esté seleccionando la fila de encabezado
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridViewVehiculos.Rows[e.RowIndex];

                // Verifica si las columnas existen antes de asignar
                if (row.Cells["nombre"].Value != null)
                    txtNombreAuto.Text = row.Cells["nombre"].Value.ToString();

                if (row.Cells["marca_id"].Value != null)
                    cmbMarca.SelectedValue = Convert.ToInt32(row.Cells["marca_id"].Value);

                if (row.Cells["modelo_id"].Value != null)
                    cmbModelo.SelectedValue = Convert.ToInt32(row.Cells["modelo_id"].Value);

                if (row.Cells["color_id"].Value != null)
                    cmbColor.SelectedValue = Convert.ToInt32(row.Cells["color_id"].Value);

                if (row.Cells["tipo_id"].Value != null)
                    cmbTipoVehiculo.SelectedValue = Convert.ToInt32(row.Cells["tipo_id"].Value);

                if (row.Cells["cantidad"].Value != null)
                    numericUpDownCantidad.Value = Convert.ToInt32(row.Cells["cantidad"].Value);

                if (row.Cells["precio"].Value != null)
                    numericUpDownPrecio.Value = Convert.ToDecimal(row.Cells["precio"].Value);

                if (row.Cells["descripcion"].Value != null)
                    txtDescripcion.Text = row.Cells["descripcion"].Value.ToString();

                if (row.Cells["año"].Value != null)
                {
                    DateTime año;
                    if (DateTime.TryParse(row.Cells["año"].Value.ToString(), out año))
                    {
                        dateTimePickerAño.Value = año;
                    }
                }

                // Guardar el ID del vehículo para usarlo al actualizar
                if (row.Cells["id_vehiculo"].Value != null)
                    currentVehiculoId = Convert.ToInt32(row.Cells["id_vehiculo"].Value);
            }
        }
    }
}
