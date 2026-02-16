using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_final__Jp
{
    public partial class Iniciosesion : Form
    {
        public Iniciosesion()
        {
            InitializeComponent();
            txtContrasena.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginform registro = new loginform();
            registro.Show();  
            // Cerrar el formulario de registro
            this.Hide();
        }

        private void Iniciosesion_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            string rutaUsuarios = Path.Combine(Application.StartupPath, "usuarios.txt");

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor completa todos los campos.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool encontrado = false;

            if (File.Exists(rutaUsuarios))
            {
                try
                {
                    // Leer todas las líneas y comprobar coincidencia usuario;contraseña
                    var lineas = File.ReadAllLines(rutaUsuarios);

                    foreach (var linea in lineas)
                    {
                        if (string.IsNullOrWhiteSpace(linea)) continue;

                        // Separar en máximo 2 partes por ';' (por si la contraseña tiene ';')
                        var partes = linea.Split(new[] { ';' }, 2);
                        if (partes.Length < 2) continue;

                        string usuArchivo = partes[0].Trim();
                        string passArchivo = partes[1].Trim();

                        if (usuArchivo == usuario && passArchivo == contrasena)
                        {
                            encontrado = true;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error leyendo archivo de usuarios: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (encontrado)
            {
                // Credenciales correctas -> abrir Form1 (o lo que quieras)
                Form1 main = new Form1();
                main.Show();
                this.Hide();
            }
            else
            {
                // Mensaje exacto que pediste
                MessageBox.Show("Usuario o contraseña incorrectos, intentalo de nuevo porfavor",
                    "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Opcional: limpiar la contraseña para que vuelva a intentarlo
                txtContrasena.Clear();
                txtContrasena.Focus();
            }
        }
    }
}
