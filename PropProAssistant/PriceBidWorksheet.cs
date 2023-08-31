using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace PropProAssistant
{
    public class PriceBidWorksheet
    {
        public Dictionary<int, Item> Items { get; private set; }
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

            Items = new Dictionary<int, Item>();

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

                if (!WorksheetService.IsSomeColumnCellFilled(worksheet, UnitCol)) return false;
                if (!WorksheetService.IsSomeColumnCellFilled(worksheet, ItemCol)) return false;
                if (!WorksheetService.IsSomeColumnCellFilled(worksheet, BrandCol)) return false;
                if (!WorksheetService.IsSomeColumnCellFilled(worksheet, AmountCol)) return false;
                if (!WorksheetService.IsSomeColumnCellFilled(worksheet, UnitPriceCol)) return false;
                if (!WorksheetService.IsSomeColumnCellFilled(worksheet, TotalPriceCol)) return false;
                if (!WorksheetService.IsSomeColumnCellFilled(worksheet, DescriptionCol)) return false;
            }
            return true;
        }

        public void FillDictionary()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(Path)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                if (Items.Count > 0) Items.Clear();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (int.TryParse(worksheet.Cells[row, ItemCol].Value?.ToString(), out int itemNum) &&
                        int.TryParse(worksheet.Cells[row, AmountCol].Value?.ToString(), out int amount) &&
                        decimal.TryParse(worksheet.Cells[row, UnitPriceCol].Value?.ToString(), out decimal unitPrice) &&
                        decimal.TryParse(worksheet.Cells[row, TotalPriceCol].Value?.ToString(), out decimal totalPrice))
                    {
                        Items.Add(itemNum,
                            new Item
                            {
                                Unit = worksheet.Cells[row, UnitCol].Value?.ToString(),
                                Number = itemNum,
                                Brand = worksheet.Cells[row, BrandCol].Value?.ToString(),
                                Amount = amount,
                                UnitPrice = unitPrice,
                                TotalPrice = totalPrice,
                                Description = worksheet.Cells[row, DescriptionCol].Value?.ToString()
                            });
                    }
                }
            }
        }
    }
}
