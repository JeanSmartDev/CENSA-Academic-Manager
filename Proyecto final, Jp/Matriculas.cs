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
    public partial class Matriculas : Form
    {
        public Matriculas()
        {
            InitializeComponent();
        }

        private readonly string rutaMatriculas = Path.Combine(Application.StartupPath, "matriculas.txt");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(Data);
            row.Cells[0].Value = T1.Text;
            row.Cells[1].Value = T2.Text;
            row.Cells[2].Value = T3.Text;
            row.Cells[3].Value = T4.Text;
            row.Cells[4].Value = T5.Text;
            row.Cells[5].Value = T6.Text;
            row.Cells[6].Value = C1.SelectedItem.ToString();
            row.Cells[7].Value = C2.SelectedItem.ToString();
            row.Cells[8].Value = C3.SelectedItem.ToString();
            row.Cells[9].Value = T10.Text;
            row.Cells[10].Value = T11.Text;

            Data.Rows.Add(row);

            string linea = string.Join(";", new string[] {
                T1.Text, T2.Text, T3.Text, T4.Text, T5.Text, T6.Text,
                C1.SelectedItem.ToString(), C2.SelectedItem.ToString(), C3.SelectedItem.ToString(),
                T10.Text, T11.Text
            });

            try
            {
                File.AppendAllText(rutaMatriculas, linea + Environment.NewLine);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error al guardar el archivo: " + ex.Message);
            }

            ClearInputs();
            MessageBox.Show("Datos agregados correctamente al DataGridView y guardados en el archivo.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Data.SelectedRows.Count > 0)
            {
                DataGridViewRow row = Data.SelectedRows[0];

                if (!ValidateInputs()) return;

                row.Cells[0].Value = T1.Text;
                row.Cells[1].Value = T2.Text;
                row.Cells[2].Value = T3.Text;
                row.Cells[3].Value = T4.Text;
                row.Cells[4].Value = T5.Text;
                row.Cells[5].Value = T6.Text;
                row.Cells[6].Value = C1.SelectedItem.ToString();
                row.Cells[7].Value = C2.SelectedItem.ToString();
                row.Cells[8].Value = C3.SelectedItem.ToString();
                row.Cells[9].Value = T10.Text;
                row.Cells[10].Value = T11.Text;

                GuardarDataGridViewEnArchivo();

                MessageBox.Show("Datos modificados correctamente.");
            }
            else
            {
                MessageBox.Show("Selecciona primero una fila para modificar.");
            }
        }

        private void Data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = Data.Rows[e.RowIndex];

                T1.Text = row.Cells[0].Value?.ToString() ?? "";
                T2.Text = row.Cells[1].Value?.ToString() ?? "";
                T3.Text = row.Cells[2].Value?.ToString() ?? "";
                T4.Text = row.Cells[3].Value?.ToString() ?? "";
                T5.Text = row.Cells[4].Value?.ToString() ?? "";
                T6.Text = row.Cells[5].Value?.ToString() ?? "";
                T10.Text = row.Cells[9].Value?.ToString() ?? "";
                T11.Text = row.Cells[10].Value?.ToString() ?? "";

                C1.SelectedItem = row.Cells[6].Value?.ToString();
                C2.SelectedItem = row.Cells[7].Value?.ToString();
                C3.SelectedItem = row.Cells[8].Value?.ToString();
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

        private void button5_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void Matriculas_Load(object sender, EventArgs e)
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

        private void DeleteRowFromFile(string[] rowData)
        {
            try
            {
                // Read all lines from the file
                List<string> lines = File.ReadAllLines(rutaMatriculas).ToList();

                // Construct the line to match the row's data
                string lineToDelete = string.Join(";", rowData);

                // Remove the line from the list of lines (if it exists)
                lines.Remove(lineToDelete);

                // Write the updated lines back to the file
                File.WriteAllLines(rutaMatriculas, lines);

            }
            catch (IOException ex)
            {
                MessageBox.Show("Error al eliminar la fila del archivo: " + ex.Message);
            }
        }

        private bool ValidateInputs()
        {
            TextBox[] textBoxes = { T1, T2, T3, T4, T5, T6, T10, T11 };
            ComboBox[] comboBoxes = { C1, C2, C3 };

            foreach (TextBox tb in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    MessageBox.Show("Por favor completa todos los campos de texto.");
                    return false;
                }
            }

            foreach (ComboBox cb in comboBoxes)
            {
                if (cb.SelectedIndex < 0)
                {
                    MessageBox.Show("Por favor selecciona una opción en todos los combobox.");
                    return false;
                }
            }

            return true;
        }

        private void ClearInputs()
        {
            T1.Clear();
            T2.Clear();
            T3.Clear();
            T4.Clear();
            T5.Clear();
            T6.Clear();
            T10.Clear();
            T11.Clear();

            C1.SelectedIndex = -1;
            C2.SelectedIndex = -1;
            C3.SelectedIndex = -1;

            T1.Focus();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}



