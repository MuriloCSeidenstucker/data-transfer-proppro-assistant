using OfficeOpenXml;
using System;
using System.IO;
using System.Windows.Forms;

namespace PropProAssistant
{
    public partial class Form_WorksheetCreation : Form
    {
        public Form_WorksheetCreation()
        {
            InitializeComponent();

            Btn_CreateWorksheet.Text = "Gerar Planilha de Preços";
            Btn_CreateWorksheet.AutoSize = true;
        }

        private void Btn_CreateWorksheet_Click(object sender, EventArgs e)
        {
            string filePath = @"../../Test/WorksheetModels/Price_Bid_Worksheet.xlsx";
            PriceBidWorksheet test = new PriceBidWorksheet(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (File.Exists(filePath))
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    for (int i = 0; i <= test.Structure.Length - 1; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = test.Structure[0, i];
                    }

                    worksheet.Columns.AutoFit();

                    package.Save();
                }
            }
            else
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Main");

                    for (int i = 0; i <= test.Structure.Length - 1; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = test.Structure[0, i];
                    }

                    worksheet.Columns.AutoFit();

                    package.SaveAs(new FileInfo(filePath));
                }
            }

            MessageBox.Show("Planilha criada com sucesso!");
        }
    }
}
