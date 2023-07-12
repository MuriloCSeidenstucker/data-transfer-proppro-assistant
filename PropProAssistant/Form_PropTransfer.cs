using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PropProAssistant
{
    public partial class Form_PropTransfer : Form
    {
        public Form_PropTransfer()
        {
            InitializeComponent();

            Btn_FileSelector.Text = "Selecionar Planilha Origem";
            Btn_FileSelector.AutoSize = true;
        }

        private void Btn_FileSelector_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileSelector = new OpenFileDialog();
            fileSelector.Title = "Selecionar Planilha Origem";
            fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileSelector.FileName;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var sheet = package.Workbook.Worksheets[0];

                    for (int i = 2; i < sheet.Dimension.End.Row; i++)
                    {
                        if (int.Parse(sheet.Cells[i, 1].Value.ToString()) == 34)
                        {
                            MessageBox.Show(sheet.Cells[i, 2].Value.ToString());
                        }
                    }

                    package.Dispose();
                }
            }
        }
    }
}
