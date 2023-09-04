using OfficeOpenXml;
using System.IO;
using System.Windows.Forms;
using System;

namespace PropProAssistant
{
    public class LicitanetModel : ModelWorksheet
    {
        public int ItemCodeCol { get; }
        public int DescriptionCol { get; }
        public int AmountCol { get; }

        public LicitanetModel(string path)
        {
            Path = path;
            ItemCol = 1;
            ItemCodeCol = 2;
            DescriptionCol = 3;
            AmountCol = 4;
            BrandCol = 5;
            ModelCol = 6;
            UnitValueCol = 7;

            Structure = new string[1, 7]
            {
                { "LOTE", "CÓDIGO DO ITEM", "DESCRIÇÃO DO ITEM", "QUANTIDADE DO ITEM", "MARCA", "MODELO", "VALOR UNITÁRIO" }
            };
        }

        public override bool Validate()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(Path)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                if (worksheet.Dimension?.Rows < 2) return false;

                //TEMP: Worksheet provided by the portal comes with null cells in the header.
                // I still don't know how to deal with this bug.
                for (int col = 1; col <= worksheet.Dimension.End.Column - 2; col++)
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
