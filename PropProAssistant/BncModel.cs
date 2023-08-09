using OfficeOpenXml;
using System;
using System.IO;
using System.Windows.Forms;

namespace PropProAssistant
{
    public class BncModel : IWorksheet
    {
        public string[,] Structure { get; }
        public string Path { get; }
        public int ItemCol { get; }
        public int BrandCol { get; }
        public int ModelCol { get; }
        public int UnitValueCol { get; }
        public int BatchCol { get; }

        public BncModel(string path)
        {
            Path = path;
            BatchCol = 1;
            ItemCol = 2;
            UnitValueCol = 3;
            BrandCol = 4;
            ModelCol = 5;

            Structure = new string[1, 5]
            {
                { "Lote", "Item", "Valor Prop.", "Marca", "Modelo", }
            };
        }

        public void Validate()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(Path)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    if (worksheet.Cells[1, col].Value?.ToString().ToUpperInvariant()
                        .Equals(Structure[0, col - 1],
                        StringComparison.OrdinalIgnoreCase) == false)
                    {
                        MessageBox.Show("A planilha selecionada não possui a estrutura esperada.",
                            "Erro - Planilha inválida",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
    }
}
