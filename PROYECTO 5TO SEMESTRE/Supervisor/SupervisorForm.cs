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

namespace PROYECTO_5TO_SEMESTRE
{
    public partial class SupervisorForm : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=TESTING_DB;Integrated Security=True";



        public SupervisorForm()
        {
            InitializeComponent();
        }

        private void SupervisorForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxCapacidadSurtido();
            LoadComboBoxDescuentos();
            LoadComboBoxDescripcionAdmin();

            CargarMarcasEnDataGridView();
            CargarGarantiasEnDataGridView();

            comboBoxEstatusAdmin.Items.Add("Activo");
            comboBoxEstatusAdmin.Items.Add("Inactivo");
            comboBoxEstatusAdmin.SelectedIndex = 0;
        }

        private void LoadComboBoxDescripcionAdmin()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT id_descripcion, descripcion FROM DescripcionGarantia";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxDescripcionAdmin.DataSource = dt;
                    comboBoxDescripcionAdmin.DisplayMember = "descripcion";
                    comboBoxDescripcionAdmin.ValueMember = "id_descripcion";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las descripciones: " + ex.Message);
            }
        }


        private void CargarGarantiasEnDataGridView()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT g.id_tipo_garantia AS [ID],
                       g.nombre AS [Nombre],
                       d.descripcion AS [Descripción],
                       g.duracion_meses AS [Duración (meses)],
                       g.precio AS [Precio],
                       CASE g.estatus WHEN 1 THEN 'Activo' ELSE 'Inactivo' END AS [Estatus]
                FROM Garantia g
                LEFT JOIN DescripcionGarantia d ON g.id_descripcion = d.id_descripcion";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridViewGarantiasAdmin.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las garantías: " + ex.Message);
            }
        }




        private void CargarMarcasEnDataGridView()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    m.id_marca AS [ID],
                    m.nombre AS [Nombre],
                    m.responsable AS [Responsable],
                    cs.nombre AS [Capacidad de Surtido],
                    d.nombre AS [Descuento],
                    CASE m.estatus 
                        WHEN 1 THEN 'Activo'
                        WHEN 0 THEN 'Inactivo'
                        ELSE 'Desconocido'
                    END AS [Estatus]
                FROM MARCAS m
                LEFT JOIN CapacidadSurtido cs ON m.id_capacidad_surtido = cs.id
                LEFT JOIN Descuentos d ON m.id_descuento = d.id";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asignar el DataTable al DataGridView
                    dataGridViewMarcas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las marcas: " + ex.Message);
            }
        }


        private void comboBoxMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        private void button1_Click(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text) || string.IsNullOrWhiteSpace(textBoxResponsable.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            try
            {
                // Obtener los valores ingresados
                string nombreMarca = textBoxNombre.Text;
                string responsable = textBoxResponsable.Text;
                int idCapacidadSurtido = (int)comboBoxCapacidadSurtido.SelectedValue;
                int idDescuento = (int)comboBoxDescuentos.SelectedValue;
                int estatus = comboBoxEstatus.SelectedItem.ToString() == "Activo" ? 1 : 0; // Establecer estatus 1=Activo, 0=Inactivo

                // Insertar los valores en la base de datos
                string query = "INSERT INTO MARCAS (nombre, responsable, id_capacidad_surtido, id_descuento, estatus) " +
                               "VALUES (@nombre, @responsable, @idCapacidadSurtido, @idDescuento, @estatus)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", nombreMarca);
                    cmd.Parameters.AddWithValue("@responsable", responsable);
                    cmd.Parameters.AddWithValue("@idCapacidadSurtido", idCapacidadSurtido);
                    cmd.Parameters.AddWithValue("@idDescuento", idDescuento);
                    cmd.Parameters.AddWithValue("@estatus", estatus);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Marca agregada exitosamente.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la marca: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNombre.Text) || string.IsNullOrEmpty(textBoxResponsable.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Obtener los valores desde los controles
            int marcaId = int.Parse(labelMarcaId.Text); // ID de la marca
            string nombre = textBoxNombre.Text;
            string responsable = textBoxResponsable.Text;
            int capacidadSurtidoId = (int)comboBoxCapacidadSurtido.SelectedValue;
            int descuentoId = (int)comboBoxDescuentos.SelectedValue;
            string estatus = comboBoxEstatus.SelectedIndex == 0 ? "Activo" : "Inactivo"; // Estatus como texto

            // Conexión a la base de datos
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=TESTING_DB;Integrated Security=True"; // Asegúrate de que esta cadena es correcta
            string query = @"UPDATE MARCAS
             SET nombre = @nombre, responsable = @responsable, 
                 capacidad_surtido = @capacidadSurtidoId, 
                 descuentos = @descuentoId, estatus = @estatus
             WHERE id_marca = @marcaId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@responsable", responsable);
                command.Parameters.AddWithValue("@capacidadSurtidoId", capacidadSurtidoId);
                command.Parameters.AddWithValue("@descuentoId", descuentoId);
                command.Parameters.AddWithValue("@estatus", estatus);
                command.Parameters.AddWithValue("@marcaId", marcaId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Marca actualizada correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la marca: " + ex.Message);
                }
            }
        }
        

        private void LoadComboBoxCapacidadSurtido()
        {
            string query = "SELECT id, nombre FROM CapacidadSurtido"; // Usando las columnas correctas

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Asignar los datos al ComboBox
                comboBoxCapacidadSurtido.DataSource = dt;
                comboBoxCapacidadSurtido.DisplayMember = "nombre"; // Muestra el nombre de la capacidad de surtido
                comboBoxCapacidadSurtido.ValueMember = "id"; // Usa el ID como valor subyacente
            }
        }


        private void LoadComboBoxDescuentos()
        {
            string query = "SELECT id, nombre FROM Descuentos"; // Usando las columnas correctas

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Asignar los datos al ComboBox
                comboBoxDescuentos.DataSource = dt;
                comboBoxDescuentos.DisplayMember = "nombre"; // Muestra el nombre del descuento
                comboBoxDescuentos.ValueMember = "id"; // Usa el ID como valor subyacente
            }
        }



        private void dataGridViewMarcas_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewMarcas.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridViewMarcas.SelectedRows[0];

                // Cargar los valores de la fila seleccionada en los controles
                textBoxNombre.Text = row.Cells["Nombre"].Value.ToString();
                textBoxResponsable.Text = row.Cells["Responsable"].Value.ToString();

                // Asignar el valor de Capacidad de Surtido al ComboBox
                var capacidadSurtidoValue = row.Cells["Capacidad de Surtido"].Value;
                if (capacidadSurtidoValue != DBNull.Value && capacidadSurtidoValue != null)
                {
                    // Asegurarse de que el valor es un entero (id de capacidad surtido)
                    if (int.TryParse(capacidadSurtidoValue.ToString(), out int capacidadId))
                    {
                        comboBoxCapacidadSurtido.SelectedValue = capacidadId;
                    }
                }

                // Asignar el valor de Descuento al ComboBox
                var descuentoValue = row.Cells["Descuento"].Value;
                if (descuentoValue != DBNull.Value && descuentoValue != null)
                {
                    // Asegurarse de que el valor es un entero (id de descuento)
                    if (int.TryParse(descuentoValue.ToString(), out int descuentoId))
                    {
                        comboBoxDescuentos.SelectedValue = descuentoId;
                    }
                }

                // Establecer el estatus en el ComboBox
                string estatus = row.Cells["Estatus"].Value.ToString();
                comboBoxEstatus.SelectedIndex = estatus == "Activo" ? 0 : 1;

                // Guardar el ID de la marca seleccionada (para futuras modificaciones)
                labelMarcaId.Text = row.Cells["ID"].Value.ToString();
            }
        }

        private void btnAgregarAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Garantia (nombre, id_descripcion, duracion_meses, precio, estatus, id_creacion, fecha_creacion)
                             VALUES (@nombre, @id_descripcion, @duracion, @precio, @estatus, 1, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", textBoxNombreAdmin.Text);
                    cmd.Parameters.AddWithValue("@id_descripcion", comboBoxDescripcionAdmin.SelectedValue);
                    cmd.Parameters.AddWithValue("@duracion", int.Parse(textBoxDuracionAdmin.Text));
                    cmd.Parameters.AddWithValue("@precio", decimal.Parse(textBoxPrecioAdmin.Text));
                    cmd.Parameters.AddWithValue("@estatus", comboBoxEstatusAdmin.SelectedItem.ToString() == "Activo" ? 1 : 0);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Garantía agregada correctamente.");
                    CargarGarantiasEnDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar garantía: " + ex.Message);
            }
        }

        private void btnModificarAdmin_Click(object sender, EventArgs e)
        {
            if (dataGridViewGarantiasAdmin.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una garantía para modificar.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Garantia 
                             SET nombre = @nombre, 
                                 id_descripcion = @id_descripcion,
                                 duracion_meses = @duracion, 
                                 precio = @precio, 
                                 estatus = @estatus,
                                 id_modificacion = 1, 
                                 fecha_modificacion = GETDATE()
                             WHERE id_tipo_garantia = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", dataGridViewGarantiasAdmin.SelectedRows[0].Cells["ID"].Value);
                    cmd.Parameters.AddWithValue("@nombre", textBoxNombreAdmin.Text);
                    cmd.Parameters.AddWithValue("@id_descripcion", comboBoxDescripcionAdmin.SelectedValue);
                    cmd.Parameters.AddWithValue("@duracion", int.Parse(textBoxDuracionAdmin.Text));
                    cmd.Parameters.AddWithValue("@precio", decimal.Parse(textBoxPrecioAdmin.Text));
                    cmd.Parameters.AddWithValue("@estatus", comboBoxEstatusAdmin.SelectedItem.ToString() == "Activo" ? 1 : 0);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Garantía modificada correctamente.");
                    CargarGarantiasEnDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar garantía: " + ex.Message);
            }
        }

        private void dataGridViewGarantiasAdmin_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewGarantiasAdmin.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewGarantiasAdmin.SelectedRows[0];

                textBoxNombreAdmin.Text = row.Cells["Nombre"].Value.ToString();        
                textBoxDuracionAdmin.Text = row.Cells["Duración (meses)"].Value.ToString();
                textBoxPrecioAdmin.Text = row.Cells["Precio"].Value.ToString();
                comboBoxEstatusAdmin.SelectedItem = row.Cells["Estatus"].Value.ToString();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
