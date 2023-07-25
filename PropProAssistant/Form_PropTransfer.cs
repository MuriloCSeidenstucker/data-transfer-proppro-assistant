using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            Btn_PriceBidWorksheetSelector.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Btn_PriceBidWorksheetSelector.Location = new Point((int)((ClientSize.Width * 0.5) - (Btn_PriceBidWorksheetSelector.Width * 0.5)), 50);

            Btn_ModelWorksheetSelector.Text = "Selecionar Planilha Modelo";
            Btn_ModelWorksheetSelector.AutoSize = true;
            Btn_ModelWorksheetSelector.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Btn_ModelWorksheetSelector.Location = new Point((int)((ClientSize.Width * 0.5) - (Btn_ModelWorksheetSelector.Width * 0.5)), 100);

            Btn_DataTransfer.Text = "Transferir Dados";
            Btn_DataTransfer.AutoSize = true;
            Btn_DataTransfer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Btn_DataTransfer.Location = new Point((int)((ClientSize.Width * 0.5) - (Btn_DataTransfer.Width * 0.5)), 150);
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
                            decimal unitValueTemp = 0m;
                            decimal totalValueTemp = 0m;
                            if (int.TryParse(worksheet.Cells[row, PriceBidWorksheet.ItemCol].Value?.ToString(), out numberTemp) &&
                                decimal.TryParse(worksheet.Cells[row, PriceBidWorksheet.UnitPriceCol].Value?.ToString(), out unitValueTemp) &&
                                decimal.TryParse(worksheet.Cells[row, PriceBidWorksheet.TotalPriceCol].Value?.ToString(), out totalValueTemp))
                            {
                                _items.Add(numberTemp,
                                    new Item
                                    {
                                        Number = numberTemp,
                                        Brand = worksheet.Cells[row, PriceBidWorksheet.BrandCol].Value?.ToString(),
                                        UnitValue = unitValueTemp,
                                        Description = worksheet.Cells[row, PriceBidWorksheet.DescriptionCol].Value?.ToString(),
                                        TotalValue = totalValueTemp
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
                MessageBox.Show("Você deve selecionar uma planilha antes",
                    "Erro - Planilha não selecionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var modelPackage = new ExcelPackage(new FileInfo(_pathModelWorksheet)))
            {
                var modelWorksheet = modelPackage.Workbook.Worksheets[0];

                if (_items.Count > modelWorksheet.Dimension.End.Row - 1)
                {
                    var option = MessageBox.Show("A quantidade de itens da proposta é maior do que da planilha modelo. Deseja continuar?",
                    "Planilha Errada",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                    if (option == DialogResult.No) return;
                }

                if (IsSomeColumnCellFilled(modelWorksheet, _modelWorksheet.UnitValueCol))
                {
                    var option = MessageBox.Show("A planilha parece já estar preenchida. Deseja continuar?",
                    "Planilha Preenchida",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                    if (option == DialogResult.No) return;
                }

                int priceBidRow = 2;

                for (int row = 2; row <= modelWorksheet.Dimension.End.Row; row++)
                {
                    if (int.TryParse(modelWorksheet.Cells[row, _modelWorksheet.ItemCol].Value.ToString(), out var modelItem)
                        && _items.ContainsKey(modelItem))
                    {
                        modelWorksheet.Cells[row, _modelWorksheet.UnitValueCol].Value = _items[modelItem].UnitValue;
                        modelWorksheet.Cells[row, _modelWorksheet.BrandCol].Value = _items[modelItem].Brand;
                        modelWorksheet.Cells[row, _modelWorksheet.ModelCol].Value = _items[modelItem].Brand;
                        if (_modelWorksheet.DescriptionCol != 0)
                        {
                            modelWorksheet.Cells[row, _modelWorksheet.DescriptionCol].Value = _items[modelItem].Description;
                        }
                        if (_modelWorksheet.TotalValueCol != 0)
                        {
                            modelWorksheet.Cells[row, _modelWorksheet.TotalValueCol].Value = _items[modelItem].TotalValue;
                        }
                        //if (_modelWorksheet.AnvisaRegCol != 0)
                        //{
                        //    modelWorksheet.Cells[row, _modelWorksheet.AnvisaRegCol].Value = _items[modelItem].AnvisaReg;
                        //}
                        priceBidRow++;
                    }
                    else
                    {
                        modelWorksheet.Cells[row, _modelWorksheet.UnitValueCol].Value = 0;
                    }
                }

                modelPackage.Save();

                MessageBox.Show("Transferência de dados concluida!");
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

                for (int row = 2; row <= modelWorksheet.Dimension.End.Row; row++)
                {
                    modelWorksheet.Cells[row, _modelWorksheet.BrandCol].Value = string.Empty;
                    modelWorksheet.Cells[row, _modelWorksheet.ModelCol].Value = string.Empty;
                    modelWorksheet.Cells[row, _modelWorksheet.UnitValueCol].Value = string.Empty;

                    if (_modelWorksheet.DescriptionCol != 0)
                    {
                        modelWorksheet.Cells[row, _modelWorksheet.DescriptionCol].Value = string.Empty;
                    }
                    if (_modelWorksheet.TotalValueCol != 0)
                    {
                        modelWorksheet.Cells[row, _modelWorksheet.TotalValueCol].Value = string.Empty;
                    }
                    if (_modelWorksheet.AnvisaRegCol != 0)
                    {
                        modelWorksheet.Cells[row, _modelWorksheet.AnvisaRegCol].Value = string.Empty;
                    }
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
                    .ToUpperInvariant().Contains("MARCA") == true
                    || worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("FABRICANTE") == true))
                {
                    _modelWorksheet.BrandCol = col;
                    continue;
                }
                if (_modelWorksheet.ItemCol == 0
                    && (worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("ITEM") == true
                    || worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("LOTE") == true))
                {
                    _modelWorksheet.ItemCol = col;
                    continue;
                }
                if (_modelWorksheet.ModelCol == 0 && worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("MODELO") == true)
                {
                    _modelWorksheet.ModelCol = col;
                    continue;
                }
                if (_modelWorksheet.UnitValueCol == 0
                    && (worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("UNITÁRIO") == true
                    || worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("PROP") == true))
                {
                    _modelWorksheet.UnitValueCol = col;
                    continue;
                }
                if (_modelWorksheet.AmountCol == 0 && worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("QUANTIDADE") == true)
                {
                    _modelWorksheet.AmountCol = col;
                    continue;
                }
                if (_modelWorksheet.AnvisaRegCol == 0 && worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("ANVISA") == true)
                {
                    _modelWorksheet.AnvisaRegCol = col;
                    continue;
                }
                if (_modelWorksheet.DescriptionCol == 0 && worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("DESCRIÇÃO") == true)
                {
                    _modelWorksheet.DescriptionCol = col;
                    continue;
                }
                if (_modelWorksheet.TotalValueCol == 0 && worksheet.Cells[1, col].Value?.ToString()
                    .ToUpperInvariant().Contains("TOTAL") == true)
                {
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
