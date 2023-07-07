using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PropProAssistant
{
    public partial class Form_PropTransfer : Form
    {
        public Form_PropTransfer()
        {
            InitializeComponent();

            Btn_FileSelector.Text = "Selecionar Planilha";
            Btn_FileSelector.AutoSize = true;
        }

        private void Btn_FileSelector_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileSelector = new OpenFileDialog();
            fileSelector.Title = "Selecionar Planilha";
            fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileSelector.FileName;
                MessageBox.Show(filePath);
            }
        }
    }
}
