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
    public partial class GerenteForm : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=TESTING_DB;Integrated Security=True";


        public GerenteForm()
        {
            InitializeComponent();
        }

        private void GerenteForm_Load(object sender, EventArgs e)
        {
            // Cargar valores en el ComboBox de Estatus
            comboBoxEstatusGerente.Items.Add("Activo");
            comboBoxEstatusGerente.Items.Add("Inactivo");
            comboBoxEstatusGerente.SelectedIndex = 0;

            // Cargar Vehículos y Pruebas en sus respectivos controles
            LoadComboBoxVehiculos();
            CargarPruebasEnDataGridView();
            LoadComboBoxClientes(); 
        }

        private void LoadComboBoxClientes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT id_cliente, CONCAT(nombre, ' ', apellido) AS nombre_completo FROM Cliente WHERE estatus = 1";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxClienteGerente.DataSource = dt;
                    comboBoxClienteGerente.DisplayMember = "nombre_completo";
                    comboBoxClienteGerente.ValueMember = "id_cliente";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message);
            }
        }



        private void LoadComboBoxVehiculos()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT id_vehiculo, nombre FROM Vehiculo WHERE cantidad > 0";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxVehiculoGerente.DataSource = dt;
                    comboBoxVehiculoGerente.DisplayMember = "nombre";
                    comboBoxVehiculoGerente.ValueMember = "id_vehiculo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los vehículos: " + ex.Message);
            }
        }

        private void CargarPruebasEnDataGridView()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT p.id_prueba AS [ID],
                       v.nombre AS [Vehículo],
                       p.nombre_cliente AS [Cliente],
                       p.fecha_prueba AS [Fecha de Prueba],
                       p.duracion_minutos AS [Duración (minutos)],
                       p.observaciones AS [Observaciones],
                       CASE p.estatus WHEN 1 THEN 'Activo' ELSE 'Inactivo' END AS [Estatus]
                FROM PruebaVehiculo p
                INNER JOIN Vehiculo v ON p.id_vehiculo = v.id_vehiculo";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridViewPruebasGerente.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las pruebas: " + ex.Message);
            }
        }

        private void btnAgregarPruebaGerente_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxClienteGerente.SelectedValue == null ||
                    !int.TryParse(textBoxDuracionGerente.Text, out int duracion))
                {
                    MessageBox.Show("Por favor, complete todos los campos y asegúrese de que la duración sea un número válido.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO PruebaVehiculo (id_vehiculo, nombre_cliente, duracion_minutos, observaciones, estatus)
                             VALUES (@id_vehiculo, @id_cliente, @duracion, @observaciones, @estatus)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_vehiculo", comboBoxVehiculoGerente.SelectedValue);
                    cmd.Parameters.AddWithValue("@id_cliente", comboBoxClienteGerente.SelectedValue);
                    cmd.Parameters.AddWithValue("@duracion", duracion);
                    cmd.Parameters.AddWithValue("@observaciones", textBoxObservacionesGerente.Text);
                    cmd.Parameters.AddWithValue("@estatus", comboBoxEstatusGerente.SelectedItem.ToString() == "Activo" ? 1 : 0);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Prueba de manejo agregada correctamente.");
                    CargarPruebasEnDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la prueba: " + ex.Message);
            }
        }

        private void btnModificarPruebaGerente_Click(object sender, EventArgs e)
        {
            if (dataGridViewPruebasGerente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una prueba para modificar.");
                return;
            }

            try
            {
                // Validaciones
                if (comboBoxClienteGerente.SelectedValue == null ||
                    comboBoxVehiculoGerente.SelectedValue == null ||
                    !int.TryParse(textBoxDuracionGerente.Text, out int duracion))
                {
                    MessageBox.Show("Por favor, complete todos los campos correctamente.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE PruebaVehiculo 
                             SET id_vehiculo = @id_vehiculo, 
                                 nombre_cliente = @id_cliente, 
                                 duracion_minutos = @duracion, 
                                 observaciones = @observaciones,
                                 estatus = @estatus
                             WHERE id_prueba = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", dataGridViewPruebasGerente.SelectedRows[0].Cells["ID"].Value);
                    cmd.Parameters.AddWithValue("@id_vehiculo", comboBoxVehiculoGerente.SelectedValue);
                    cmd.Parameters.AddWithValue("@id_cliente", comboBoxClienteGerente.SelectedValue);
                    cmd.Parameters.AddWithValue("@duracion", duracion);
                    cmd.Parameters.AddWithValue("@observaciones", textBoxObservacionesGerente.Text);
                    cmd.Parameters.AddWithValue("@estatus", comboBoxEstatusGerente.SelectedItem.ToString() == "Activo" ? 1 : 0);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Prueba modificada correctamente.");
                    CargarPruebasEnDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la prueba: " + ex.Message);
            }
        }

        private void dataGridViewPruebasGerente_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewPruebasGerente.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewPruebasGerente.SelectedRows[0];

                comboBoxVehiculoGerente.SelectedValue = row.Cells["Vehículo"].Value.ToString();
                comboBoxClienteGerente.SelectedValue = row.Cells["Cliente"].Value.ToString();
                textBoxDuracionGerente.Text = row.Cells["Duración (minutos)"].Value.ToString();
                textBoxObservacionesGerente.Text = row.Cells["Observaciones"].Value.ToString();
                comboBoxEstatusGerente.SelectedItem = row.Cells["Estatus"].Value.ToString();
            }
        }
    }
}
