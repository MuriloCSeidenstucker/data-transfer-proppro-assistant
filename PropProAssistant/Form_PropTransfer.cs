using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PropProAssistant
{
    public partial class Form_PropTransfer : Form
    {
        private Dictionary<int, Item> _items = new Dictionary<int, Item>();
        private ModelWorksheet _modelWorksheet = new ModelWorksheet();

        private string _pathPriceBidWorksheet = string.Empty;
        private string _pathModelWorksheet = string.Empty;

        public Form_PropTransfer()
        {
            InitializeComponent();
            InitializeButtons();
            InitializeDebug();
        }

        private void InitializeButtons()
        {
            Btn_PriceBidWorksheetSelector.Text = "Selecionar Planilha Proposta";
            Btn_PriceBidWorksheetSelector.AutoSize = true;

            Btn_ModelWorksheetSelector.Text = "Selecionar Planilha Modelo";
            Btn_ModelWorksheetSelector.AutoSize = true;

            Btn_DataTransfer.Text = "Transferir Dados";
            Btn_DataTransfer.AutoSize = true;
        }

        private void InitializeDebug()
        {
#if DEBUG
            DebugComponent.InitializeDebugControls(this);
#endif
        }

        private void Btn_PriceBidWorksheetSelector_Click(object sender, EventArgs e)
        {
            using (var fileSelector = new OpenFileDialog())
            {
                fileSelector.Title = "Selecionar Planilha Proposta";
                fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                fileSelector.Multiselect = false;

                if (fileSelector.ShowDialog() == DialogResult.OK)
                {
                    if (!IsExcelFile(fileSelector.FileName))
                    {
                        MessageBox.Show("O arquivo selecionado não é um arquivo Excel válido.",
                            "Erro - Formato de arquivo inválido", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _pathPriceBidWorksheet = fileSelector.FileName;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(new FileInfo(_pathPriceBidWorksheet)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        if (!IsPriceBidWorksheetValid(worksheet))
                        {
                            MessageBox.Show("A planilha selecionada não possui a estrutura esperada.",
                                "Erro - Planilha inválida",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _pathPriceBidWorksheet = string.Empty;
                            return;
                        }

                        if (_items.Count > 0) _items.Clear();

                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            int numberTemp = 0;
                            decimal unitValueTemp = 0;
                            if (int.TryParse(worksheet.Cells[row, PriceBidWorksheet.ItemCol].Value?.ToString(), out numberTemp) &&
                                decimal.TryParse(worksheet.Cells[row, PriceBidWorksheet.UnitPriceCol].Value?.ToString(), out unitValueTemp))
                            {
                                _items.Add(numberTemp,
                                    new Item
                                    {
                                        Number = numberTemp,
                                        Brand = worksheet.Cells[row, PriceBidWorksheet.BrandCol].Value?.ToString(),
                                        UnitValue = unitValueTemp
                                    });
                            }
                        }
                    }
                }
            }
        }

        private void Btn_ModelWorksheetSelector_Click(object sender, EventArgs e)
        {
            using (var fileSelector = new OpenFileDialog())
            {
                fileSelector.Title = "Selecionar Planilha Modelo";
                fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                fileSelector.Multiselect = false;

                if (fileSelector.ShowDialog() == DialogResult.OK)
                {
                    if (!IsExcelFile(fileSelector.FileName))
                    {
                        MessageBox.Show("O arquivo selecionado não é um arquivo Excel válido.",
                            "Erro - Formato de arquivo inválido",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _pathModelWorksheet = fileSelector.FileName;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(new FileInfo(_pathModelWorksheet)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        if (!IsModelWorksheetValid(worksheet))
                        {
                            MessageBox.Show("A planilha selecionada não possui a estrutura esperada.", "Erro - Planilha inválida",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _pathModelWorksheet = string.Empty;
                        }
                    }
                }
            }
        }

        private void Btn_DataTransfer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pathPriceBidWorksheet) || string.IsNullOrEmpty(_pathModelWorksheet))
            {
                MessageBox.Show("Você deve selecionar uma planilha antes", "Erro - Planilha não selecionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var priceBidPackage = new ExcelPackage(new FileInfo(_pathPriceBidWorksheet)))
            using (var modelPackage = new ExcelPackage(new FileInfo(_pathModelWorksheet)))
            {
                var priceBidWorksheet = priceBidPackage.Workbook.Worksheets[0];
                var modelWorksheet = modelPackage.Workbook.Worksheets[0];

                if (IsSomeColumnCellFilled(modelWorksheet, _modelWorksheet.UnitValueCol))
                {
                    var option = MessageBox.Show("A planilha parece já estar preenchida. Deseja continuar?",
                    "Planilha Preenchida",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                    if (option == DialogResult.No) return;
                }

                int priceBidRow = 2;

                for (int i = 2; i <= modelWorksheet.Dimension.End.Row; i++)
                {
                    if (priceBidRow > priceBidWorksheet.Dimension.End.Row) break;

                    if (int.TryParse(modelWorksheet.Cells[i, 1].Value.ToString(), out var modelItem)
                        && _items.ContainsKey(modelItem))
                    {
                        modelWorksheet.Cells[i, _modelWorksheet.UnitValueCol].Value = _items[modelItem].UnitValue;
                        modelWorksheet.Cells[i, _modelWorksheet.BrandCol].Value = _items[modelItem].Brand;
                        modelWorksheet.Cells[i, _modelWorksheet.ModelCol].Value = _items[modelItem].Brand;
                        priceBidRow++;
                    }
                }

                modelPackage.Save();

                MessageBox.Show("Transferência de dados concluida!");

                if (priceBidPackage != null) priceBidPackage.Dispose();
                if (modelPackage != null) modelPackage.Dispose();
            }
        }

        public void Btn_ResetButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pathModelWorksheet))
            {
                MessageBox.Show("Você deve selecionar uma planilha antes", "Erro - Planilha não selecionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var modelPackage = new ExcelPackage(new FileInfo(_pathModelWorksheet)))
            {
                var modelWorksheet = modelPackage.Workbook.Worksheets[0];

                for (int i = 2; i <= modelWorksheet.Dimension.End.Row; i++)
                {
                    modelWorksheet.Cells[i, _modelWorksheet.BrandCol].Value = string.Empty;
                    modelWorksheet.Cells[i, _modelWorksheet.ModelCol].Value = string.Empty;
                    modelWorksheet.Cells[i, _modelWorksheet.UnitValueCol].Value = string.Empty;
                }

                modelPackage.Save();

                MessageBox.Show("Planilha Resetada!");
            }
        }

        private bool IsExcelFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension.Equals(".xls", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".xlsm", StringComparison.OrdinalIgnoreCase);
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

        private bool IsPriceBidWorksheetValid(ExcelWorksheet worksheet)
        {
            if (worksheet.Dimension?.Rows < 2) return false;

            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                if (worksheet.Cells[1, col].Value?.ToString().ToUpperInvariant()
                    .Equals(PriceBidWorksheet.Structure[0, col - 1],
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return false;
                }
            }

            if (!IsSomeColumnCellFilled(worksheet, PriceBidWorksheet.ItemCol)
                || !IsSomeColumnCellFilled(worksheet, PriceBidWorksheet.BrandCol)
                || !IsSomeColumnCellFilled(worksheet, PriceBidWorksheet.UnitPriceCol))
            {
                return false;
            }

            return true;
        }

        private bool IsModelWorksheetValid(ExcelWorksheet worksheet)
        {
            if (worksheet.Dimension?.Rows < 2) return false;

            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                if (_modelWorksheet.BrandCol == 0
                    && (worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("MARCA") == true
                    || worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("FABRICANTE") == true))
                {
                    Console.WriteLine($"Col: {col}, {worksheet.Cells[1, col].Value?.ToString()}");
                    _modelWorksheet.BrandCol = col;
                    continue;
                }
                if (_modelWorksheet.ItemCol == 0
                    && (worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("ITEM") == true
                    || worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("LOTE") == true))
                {
                    Console.WriteLine($"Col: {col}, {worksheet.Cells[1, col].Value?.ToString()}");
                    _modelWorksheet.ItemCol = col;
                    continue;
                }
                if (_modelWorksheet.ModelCol == 0
                    && worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("MODELO") == true)
                {
                    Console.WriteLine($"Col: {col}, {worksheet.Cells[1, col].Value?.ToString()}");
                    _modelWorksheet.ModelCol = col;
                    continue;
                }
                if (_modelWorksheet.UnitValueCol == 0
                    && (worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("UNITÁRIO") == true
                    || worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("PROP") == true))
                {
                    Console.WriteLine($"Col: {col}, {worksheet.Cells[1, col].Value?.ToString()}");
                    _modelWorksheet.UnitValueCol = col;
                    continue;
                }
                if (_modelWorksheet.AmountCol == 0
                    && worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("QUANTIDADE") == true)
                {
                    Console.WriteLine($"Col: {col}, {worksheet.Cells[1, col].Value?.ToString()}");
                    _modelWorksheet.AmountCol = col;
                    continue;
                }
                if (_modelWorksheet.AnvisaRegCol == 0
                    && worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("ANVISA") == true)
                {
                    Console.WriteLine($"Col: {col}, {worksheet.Cells[1, col].Value?.ToString()}");
                    _modelWorksheet.AnvisaRegCol = col;
                    continue;
                }
                if (_modelWorksheet.DescriptionCol == 0
                    && (worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("DESCRIÇÃO") == true
                    || worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("PRODUTO") == true))
                {
                    Console.WriteLine($"Col: {col}, {worksheet.Cells[1, col].Value?.ToString()}");
                    _modelWorksheet.DescriptionCol = col;
                    continue;
                }
                if (_modelWorksheet.TotalValueCol == 0
                    && worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant()
                    .Contains("TOTAL") == true)
                {
                    Console.WriteLine($"Col: {col}, {worksheet.Cells[1, col].Value?.ToString()}");
                    _modelWorksheet.TotalValueCol = col;
                    continue;
                }
            }

            if (_modelWorksheet.ItemCol == 0
                || _modelWorksheet.BrandCol == 0
                || _modelWorksheet.ModelCol == 0
                || _modelWorksheet.UnitValueCol == 0)
            {
                return false;
            }

            if (IsSomeColumnCellFilled(worksheet, _modelWorksheet.UnitValueCol))
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

            if (!IsSomeColumnCellFilled(worksheet, _modelWorksheet.ItemCol))
            {
                return false;
            }

            return true;
        }
    }
}
