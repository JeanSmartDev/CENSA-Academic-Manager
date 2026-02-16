using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_final__Jp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AplicarEstiloBotones(this);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.BorderSize = 0;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();   // Crear instancia
            f2.StartPosition = FormStartPosition.CenterScreen; // Que aparezca centrado
            f2.Show();
        }
        private void AplicarEstiloBotones(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
                    btn.FlatAppearance.MouseDownBackColor = btn.BackColor;
                    btn.FlatAppearance.BorderSize = 0;   // quitar bordes
                    btn.TabStop = false;                 // evitar recuadro de foco
                }
            }
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Matriculas M = new Matriculas();
            M.StartPosition = FormStartPosition.CenterScreen;
            M.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Salir S = new Salir();
            S.StartPosition = FormStartPosition.CenterScreen;
            S.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Informe I = new Informe();
            I.StartPosition = FormStartPosition.CenterScreen;
            I.Show();
        }
    }
}
