using OfficeOpenXml;
using System;
using System.IO;
using System.Windows.Forms;

namespace PropProAssistant
{
    public class PriceBidWorksheet
    {
        public string[,] Structure { get; }
        public string Path { get; }
        public int ItemCol { get; }
        public int DescriptionCol { get; }
        public int UnitCol { get; }
        public int AmountCol { get; }
        public int BrandCol { get; }
        public int CostPriceCol { get; }
        public int EntryPercentCol { get; }
        public int UnitPriceCol { get; }
        public int TotalPriceCol { get; }
        public int MinPercentCol { get; }
        public int MinPriceCol { get; }
        public int CurrentBidCol { get; }
        public int PositionCol { get; }
        public int TotalBidCol { get; }

        public PriceBidWorksheet(string path)
        {
            Path = path;
            ItemCol = 1;
            DescriptionCol = 2;
            UnitCol = 3;
            AmountCol = 4;
            BrandCol = 5;
            CostPriceCol = 6;
            EntryPercentCol = 7;
            UnitPriceCol = 8;
            TotalPriceCol = 9;
            MinPercentCol = 10;
            MinPriceCol = 11;
            CurrentBidCol = 12;
            PositionCol = 13;
            TotalBidCol = 14;

            Structure = new string[1, 14]
{
                { "ITEM", "DESCRIÇÃO", "UND", "QTD", "MARCA", "VALOR DE CUSTO", "PERCENTUAL DE ENTRADA",
                    "CUSTO + PERCENTUAL DE ENTRADA", "VALOR TOTAL", "PERCENTUAL MÍNIMO", "VALOR MÍNIMO",
                    "LANCE ATUAL", "POSIÇÃO", "VALOR TOTAL DO LANCE" }
};
        }

        public bool Validate()
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

                if (!IsSomeColumnCellFilled(worksheet, ItemCol)) return false;
                if (!IsSomeColumnCellFilled(worksheet, BrandCol)) return false;
                if (!IsSomeColumnCellFilled(worksheet, UnitPriceCol)) return false;
            }
            return true;
        }

        private bool IsSomeColumnCellFilled(ExcelWorksheet worksheet, int column)
        {
            int lastRow = worksheet.Dimension?.End.Row ?? 0;

            for (int row = 2; row <= lastRow; row++)
            {
                if (!string.IsNullOrEmpty(worksheet.Cells[row, column].Value?.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
