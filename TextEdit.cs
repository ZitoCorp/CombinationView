using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace EZView
{
    public partial class TextEdit : Form
    {
        Viewer parentForm;
        string editingFile;

        public TextEdit(Viewer parent)
        {
            parentForm = parent;
            InitializeComponent();
        }

        public void Initialize(string text)
        {
            editingFile = text;
            richTextBox1.Text = System.IO.File.ReadAllText(text);
        }

        private void TextEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Enabled = true;
            this.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            parentForm.Enabled = true;
            System.IO.File.WriteAllText(editingFile, richTextBox1.Text);
            parentForm.UpdateCombos();
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            parentForm.Enabled = true;
            this.Close();
        }
    }
}
