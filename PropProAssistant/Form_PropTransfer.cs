using OfficeOpenXml;
using System;
using System.IO;
using System.Windows.Forms;

namespace PropProAssistant
{
    public partial class Form_PropTransfer : Form
    {
        private string _pathMainSheet;
        private string _pathModelSheet;

        public Form_PropTransfer()
        {
            InitializeComponent();

            Btn_MainSheetSelector.Text = "Selecionar Planilha Origem";
            Btn_MainSheetSelector.AutoSize = true;

            Btn_ModelSheetSelector.Text = "Selecionar Planilha Modelo";
            Btn_ModelSheetSelector.AutoSize = true;

            Btn_DataTransfer.Text = "Transferir Dados";
            Btn_DataTransfer.AutoSize = true;
        }

        private void Btn_FileSelector_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileSelector = new OpenFileDialog();
            fileSelector.Title = "Selecionar Planilha Origem";
            fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                _pathMainSheet = fileSelector.FileName;
            }
        }

        private void Btn_ModelSheetSelector_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileSelector = new OpenFileDialog();
            fileSelector.Title = "Selecionar Planilha Modelo";
            fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                _pathModelSheet = fileSelector.FileName;
            }
        }

        private void Btn_DataTransfer_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var mainPackage =  new ExcelPackage(new FileInfo(_pathMainSheet)))
            using (var modelPackage = new ExcelPackage(new FileInfo(_pathModelSheet)))
            {
                var mainSheet = mainPackage.Workbook.Worksheets[0];
                var modelSheet = modelPackage.Workbook.Worksheets[0];

                int mainRow = 2;

                for (int i = 2; i < modelSheet.Dimension.End.Row; i++)
                {
                    if (mainRow > mainSheet.Dimension.End.Row) break;

                    if (int.Parse(modelSheet.Cells[i, 1].Value.ToString())
                        == int.Parse(mainSheet.Cells[mainRow, 1].Value.ToString()))
                    {
                        modelSheet.Cells[i, 3].Value = mainSheet.Cells[mainRow, 7].Value;
                        modelSheet.Cells[i, 4].Value = mainSheet.Cells[mainRow, 5].Value;
                        modelSheet.Cells[i, 5].Value = mainSheet.Cells[mainRow, 5].Value;
                        mainRow++;
                    }
                }

                modelPackage.Save();

                MessageBox.Show("Transferência de dados concluida!");

                mainPackage.Dispose();
                modelPackage.Dispose();
            }
        }
    }
}
