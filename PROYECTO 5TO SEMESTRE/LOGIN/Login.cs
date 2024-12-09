using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_5TO_SEMESTRE
{
    public partial class Login : Form
    {
       

        public Login()
        {
            InitializeComponent();

        }

        public static class SessionData
        {
            public static int currentUserId { get; set; }
        }



        // Definir la cadena de conexión aquí
        private string connectionString = "Data Source=eduardomv\\SQLEXPRESS;Initial Catalog=TESTING_DB;Integrated Security=True";

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

        // Método para comparar contraseñas encriptadas
        private bool VerificarContraseña(string contraseñaIngresada, string contraseñaGuardada)
        {
            // Encriptar la contraseña ingresada por el usuario
            string contraseñaEncriptada = EncriptarContraseña(contraseñaIngresada);

            // Comparar las dos contraseñas encriptadas
            return contraseñaEncriptada == contraseñaGuardada;
        }




        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string correo = txtCorreo.Text;
                string contraseña = txtContraseña.Text;
                string contraseñaEncriptada = EncriptarContraseña(contraseña);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta para obtener los datos del empleado
                    string query = "SELECT id_empleado, id_rol, nombre, contraseña FROM EMPLEADO WHERE correo = @correo";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@correo", correo);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Verificar la contraseña encriptada
                                string storedPassword = reader["contraseña"].ToString();
                                if (storedPassword == contraseñaEncriptada)
                                {
                                    // Asignar el ID del empleado actual a la sesión
                                    SessionData.currentUserId = Convert.ToInt32(reader["id_empleado"]);

                                    // Obtener el rol del empleado
                                    int idRol = Convert.ToInt32(reader["id_rol"]);

                                    // Redirigir a los formularios respectivos según el id_rol
                                    switch (idRol)
                                    {
                                        case 1: // Jefe
                                            Contratar jefeForm = new Contratar();
                                            jefeForm.Show();
                                            break;
                                        case 2: // Vendedor
                                            Vendedor vendedorForm = new Vendedor();
                                            vendedorForm.Show();
                                            break;
                                        case 3: // Mecánico
                                            MecanicoForm mecanicoForm = new MecanicoForm();
                                            mecanicoForm.Show();
                                            break;
                                        case 4: // Recepcionista
                                            RecepcionistaForm recepcionistaForm = new RecepcionistaForm();
                                            recepcionistaForm.Show();
                                            break;
                                        case 5: // Contador
                                            ContadorForm contadorForm = new ContadorForm();
                                            contadorForm.Show();
                                            break;
                                        case 6: // Supervisor
                                            SupervisorForm supervisorForm = new SupervisorForm();
                                            supervisorForm.Show();
                                            break;
                                        case 7: // Gerente
                                            GerenteForm gerenteForm = new GerenteForm();
                                            gerenteForm.Show();
                                            break;
                                        case 8: // Asistente de ventas
                                            AsistenteVentasForm asistenteVentasForm = new AsistenteVentasForm();
                                            asistenteVentasForm.Show();
                                            break;
                                        case 9: // Administrador
                                            AdministradorForm administradorForm = new AdministradorForm();
                                            administradorForm.Show();
                                            break;
                                        case 10: // Auxiliar de taller
                                            AuxiliarTallerForm auxiliarTallerForm = new AuxiliarTallerForm();
                                            auxiliarTallerForm.Show();
                                            break;
                                        default:
                                            MessageBox.Show("Rol no reconocido.");
                                            return;
                                    }

                                    this.Hide(); // Ocultar el formulario de login
                                }
                                else
                                {
                                    MessageBox.Show("Contraseña incorrecta.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Correo no encontrado.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Asegúrate de que UseSystemPasswordChar esté activado para ocultar los caracteres de la contraseña
            txtContraseña.UseSystemPasswordChar = true;
        }
    }
}
