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
    public partial class loginform : Form
    {
        public loginform()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (usuario == "" || contrasena == "")
            {
                MessageBox.Show("Por favor completa todos los campos.");
                return;
            }

            string ruta = "usuarios.txt";

            // Evitar que se repita el usuario
            if (File.Exists(ruta))
            {
                string[] lineas = File.ReadAllLines(ruta);
                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(';');
                    if (datos[0] == usuario)
                    {
                        MessageBox.Show("Ese usuario ya existe.");
                        return;
                    }
                }
            }

            // Guardar en archivo
            using (StreamWriter sw = new StreamWriter(ruta, true))
            {
                sw.WriteLine(usuario + ";" + contrasena);
            }

            MessageBox.Show("Usuario registrado correctamente.");

            // Abrir el formulario de login después del registro
            Iniciosesion login = new Iniciosesion();
            login.Show();

            // Cerrar el formulario de registro
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Iniciosesion login = new Iniciosesion();
            login.Show();

            // Cerrar el formulario de registro
            this.Hide();
        }
    }
}
