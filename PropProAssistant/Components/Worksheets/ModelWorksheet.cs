using OfficeOpenXml;
using System.IO;
using System;
using System.Windows.Forms;

namespace PropProAssistant
{
    public abstract class ModelWorksheet
    {
        public string[,] Structure { get; protected set; }
        public string Path { get; protected set; }
        public int ItemCol { get; protected set; }
        public int BrandCol { get; protected set; }
        public int ModelCol { get; protected set; }
        public int UnitValueCol { get; protected set; }

        public virtual bool Validate()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(Path)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                if (worksheet.Dimension?.Rows < 2) return false;

                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    if (worksheet.Cells[1, col].Value == null) return false;

                    if (worksheet.Cells[1, col].Value?.ToString().ToUpperInvariant()
                        .Equals(Structure[0, col - 1],
                        StringComparison.OrdinalIgnoreCase) == false)
                    {
                        return false;
                    }
                }

                if (!WorksheetService.IsSomeColumnCellFilled(worksheet, ItemCol)) return false;

                if (WorksheetService.IsSomeColumnCellFilled(worksheet, UnitValueCol))
                {
                    var option = MessageBox.Show("A planilha parece já estar preenchida. Deseja continuar?",
                        "Planilha Preenchida",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (option == DialogResult.No)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
