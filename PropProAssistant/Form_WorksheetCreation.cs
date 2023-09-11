using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;
using System.Windows.Forms;

namespace PropProAssistant
{
    public partial class Form_WorksheetCreation : Form
    {
        private PriceBidWorksheet _model;

        public Form_WorksheetCreation()
        {
            InitializeComponent();

            Btn_CreateWorksheet.Text = "Gerar Planilha de Preços";
            Btn_CreateWorksheet.AutoSize = true;
        }

        private void Btn_CreateWorksheet_Click(object sender, EventArgs e)
        {
            GenerateWorksheet();
        }

        private void GenerateWorksheet()
        {
            string filePath = @"../../Test/WorksheetModels/Price_Bid_Worksheet.xlsx";
            _model = new PriceBidWorksheet(filePath);

            var fi = new FileInfo(filePath);
            if (fi.Exists) fi.Delete();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Main");

                GenerateHeader(worksheet);
                SetDefaultCells(worksheet);

                package.SaveAs(fi);
            }

            MessageBox.Show("Planilha criada com sucesso!");
        }

        private void GenerateHeader(ExcelWorksheet worksheet)
        {
            for (int i = 0; i <= _model.Structure.Length - 1; i++)
            {
                worksheet.Cells[1, i + 1].Value = _model.Structure[0, i];
            }

            worksheet.Columns.BestFit = true;
            worksheet.Columns.AutoFit(10, 30);
            worksheet.Columns.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Columns.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.View.ZoomScale = 80;
        }

        private void SetDefaultCells(ExcelWorksheet worksheet)
        {
            worksheet.Cells["F2"].Value = 10;
            double custo = double.Parse(worksheet.Cells["F2"].Value?.ToString());
            double percent = 0.3;
            double custoPlusPercent = custo + (custo * percent);
            worksheet.Cells["H2"].Value = custoPlusPercent;
        }
    }
}
