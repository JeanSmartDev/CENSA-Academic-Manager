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
    public partial class Informe : Form
    {
        public Informe()
        {
            InitializeComponent();
        }
        private readonly string rutaMatriculas = Path.Combine(Application.StartupPath, "matriculas.txt");

        private void Informe_Load(object sender, EventArgs e)
        {
            if (File.Exists(rutaMatriculas))
            {
                string[] lineas = File.ReadAllLines(rutaMatriculas);
                foreach (string linea in lineas)
                {
                    string[] campos = linea.Split(';');

                    if (campos.Length == 11)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(Data);

                        for (int i = 0; i < campos.Length; i++)
                        {
                            row.Cells[i].Value = campos[i];
                        }

                        Data.Rows.Add(row);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void GuardarDataGridViewEnArchivo()
        {
            List<string> lineas = new List<string>();

            foreach (DataGridViewRow row in Data.Rows)
            {
                if (!row.IsNewRow) // Ignorar fila de nueva entrada
                {
                    string[] campos = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        campos[i] = row.Cells[i].Value?.ToString() ?? "";
                    }
                    lineas.Add(string.Join(";", campos));
                }
            }

            try
            {
                File.WriteAllLines(rutaMatriculas, lineas);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error al guardar el archivo: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Data.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "¿Deseas eliminar la fila seleccionada?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Get the row to be deleted
                    DataGridViewRow rowToDelete = Data.SelectedRows[0];

                    // Get the values of the row in order to find the corresponding line in the file
                    string[] rowData = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        rowData[i] = rowToDelete.Cells[i].Value?.ToString() ?? "";
                    }

                    // Remove the row from the DataGridView
                    Data.Rows.Remove(rowToDelete);

                    // Now, remove the corresponding line from the file
                    DeleteRowFromFile(rowData);

                    MessageBox.Show("Fila eliminada correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona primero una fila para eliminar.");
            }
        }
        private void DeleteRowFromFile(string[] rowData)
        {
            try
            {

                List<string> lines = File.ReadAllLines(rutaMatriculas).ToList();

                string lineToDelete = string.Join(";", rowData);


                lines.Remove(lineToDelete);


                File.WriteAllLines(rutaMatriculas, lines);

            }
            catch (IOException ex)
            {
                MessageBox.Show("Error al eliminar la fila del archivo: " + ex.Message);
            }
        }
    }
}
