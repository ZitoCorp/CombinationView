using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace EZView
{
    public partial class Viewer : Form
    {
        // Flag for saving
        bool changesMade = false;

        DataTable parsedData = new DataTable();

        // Read data from csv file
        private void ReadData()
        {
            TextFieldParser tfp = new TextFieldParser(@"GroupData.csv");
            tfp.SetDelimiters(",");
            string[] fields = tfp.ReadFields();

            // Column headers
            foreach(string field in fields)
            {
                parsedData.Columns.Add(field);
            }

            // Rest of the data
            while (!tfp.EndOfData)
                parsedData.Rows.Add(tfp.ReadFields());
        }

        // Save data to file
        private void SaveData()
        {
            StringBuilder stringBuilder = new StringBuilder();

            // Construct first line with column headers
            var columnHeaders = dataGridView1.Columns.Cast<DataGridViewColumn>();
            stringBuilder.AppendLine(string.Join(",", columnHeaders.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            // Construct remaining lines from normal DataGridViewRows
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                stringBuilder.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));

                // Exclude extra blank line
                if (row.Index == dataGridView1.Rows.Count - 2)
                    break;
            }

            // Write stringbuilder data to stream
            System.IO.StreamWriter writer = new StreamWriter(@"GroupData.csv");

            writer.Write(stringBuilder.ToString());
            writer.Close();
            writer.Dispose();

            // Save complete
            changesMade = false;
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            // No changes were made, exit normally
            if (!changesMade) return;

            // Check if user would like to save or cancel exiting
            DialogResult exitResult = MessageBox.Show("Would you like to save?", "Exiting...", MessageBoxButtons.YesNoCancel);
            if (exitResult == DialogResult.Yes)
            {
                SaveData();
            }
            else if(exitResult == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        void PopulateCombo(string file, ComboBox combo)
        {
            combo.Items.Clear();
            string[] lines = file.Split('\n');
            foreach(string line in lines)
            {
                combo.Items.Add(line);
            }
        }

        public Viewer()
        {
            InitializeComponent();

            UpdateCombos();

            // Check if CSV exists, otherwise create it
            if(File.Exists(@"GroupData.csv"))
            {
                ReadData();
            }
            
            dataGridView1.DataSource = parsedData;

            ClearView();
        }

        // Set datagridview to default view
        void ClearView()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.HeaderText != "Default")
                    column.Visible = false;
            }
        }

        // Return if string is valid or not
        private static bool StringIsValid(string str)
        {
            char[] cArray = str.ToCharArray();

            foreach(char c in cArray)
            {
                if(!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') ||
                (c >= '0' && c <= '9') || (c == '-')))
                {
                    return false;
                }
            }
            return true;
        }

        #region Datatable interactions

        public void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                dataGridView1.SelectedCells.Cast<DataGridViewCell>().ToList().ForEach(cell => cell.Value = "");

                parsedData.Rows.Cast<DataRow>().ToList().FindAll(
                    row => string.IsNullOrEmpty(string.Join("", row.ItemArray))
                    ).ForEach(row => { parsedData.Rows.Remove(row); });

                changesMade = true;
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            dataGridView1.ClearSelection();
            DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
            if(hit.RowIndex != -1 && hit.ColumnIndex != -1)
            {
                dataGridView1.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                contextMenuStrip2.Show(dataGridView1, e.X, e.Y);
            }

        }

        private void Paste(object sender, EventArgs e)
        {
            string clipData = Clipboard.GetText();
            string[] clipdatas = clipData.Split('\n');
            if (clipdatas[clipdatas.Length - 1] == "")
            {
                clipdatas = clipdatas.Take(clipdatas.Count() - 1).ToArray();
            }

            int cellRow = dataGridView1.SelectedCells[0].RowIndex - 1;
            int cellColumn = dataGridView1.SelectedCells[0].ColumnIndex;

            foreach (string clip in clipdatas)
            {
                if (cellRow + 1 >= parsedData.Rows.Count)
                {
                    DataRow row = parsedData.NewRow();
                    parsedData.Rows.Add(row);
                }
                cellRow = cellRow + 1;

                parsedData.Rows[cellRow][cellColumn] = clip;
            }

            parsedData.AcceptChanges();

            changesMade = true;
        }

        #endregion

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            changesMade = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Organise grid of data in a hierarchy.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCombination();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCombination();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCombination();
        }

        // Show combinations of office, department and title.
        void ShowCombination()
        {
            // Check that combos aren't their default values
            if(OfficeCombo.Text != "Offices" && DepartmentCombo.Text != "Departments" && TitleCombo.Text != "Titles")
            {
                ClearView();
                string officeText = OfficeCombo.Text.ToString().Trim();
                string departmentText = DepartmentCombo.Text.ToString().Trim();
                string titleText = TitleCombo.Text.ToString().Trim();


                List<String> combinations = new List<string>{ officeText, departmentText, titleText,
                    officeText + "-" + departmentText,
                    officeText + "-" + titleText,
                    departmentText + "-" + titleText,
                    officeText + "-" + departmentText + "-" + titleText
                };

                List<DataGridViewColumn> newColumns = new List<DataGridViewColumn>();
               
                foreach(string comb in combinations.ToList()) {
                    foreach(DataGridViewColumn col in dataGridView1.Columns) {
                        if(col.HeaderText == comb) {
                            col.Visible = true;
                            combinations.Remove(comb);
                            break;
                        }
                    }
                }

                combinations.ForEach(comb => dataGridView1.Columns.Add(comb, comb));
                changesMade = true;
            }
        }

        private void OfficeBtn_Click(object sender, EventArgs e)
        {
            TextEdit textEdit = new TextEdit(this);
            textEdit.Show();

            textEdit.Initialize("Offices.txt");
            this.Enabled = false;
        }

        private void DepartmentBtn_Click(object sender, EventArgs e)
        {
            TextEdit textEdit = new TextEdit(this);
            textEdit.Show();

            textEdit.Initialize("Departments.txt");
            this.Enabled = false;
        }

        public void UpdateCombos()
        {
            PopulateCombo(File.ReadAllText("Offices.txt"), OfficeCombo);
            PopulateCombo(File.ReadAllText("Departments.txt"), DepartmentCombo);
            PopulateCombo(File.ReadAllText("Titles.txt"), TitleCombo);
        }

        private void TitleBtn_Click(object sender, EventArgs e)
        {
            TextEdit textEdit = new TextEdit(this);
            textEdit.Show();

            textEdit.Initialize("Titles.txt");
            this.Enabled = false;
        }
    }
}
