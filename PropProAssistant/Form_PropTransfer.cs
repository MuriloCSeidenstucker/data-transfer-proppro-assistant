using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PropProAssistant
{
    public partial class Form_PropTransfer : Form
    {
        private Dictionary<int, Item> _items = new Dictionary<int, Item>();

        private string _pathMainWorksheet = string.Empty;
        private string _pathModelWorksheet = string.Empty;

        public Form_PropTransfer()
        {
            InitializeComponent();
            InitializeButtons();
            InitializeDebug();
        }

        private void InitializeButtons()
        {
            Btn_MainWorksheetSelector.Text = "Selecionar Planilha Origem";
            Btn_MainWorksheetSelector.AutoSize = true;

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

        private void Btn_MainWorksheetSelector_Click(object sender, EventArgs e)
        {
            using (var fileSelector = new OpenFileDialog())
            {
                fileSelector.Title = "Selecionar Planilha Origem";
                fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                if (fileSelector.ShowDialog() == DialogResult.OK)
                {
                    if (IsExcelFile(fileSelector.FileName))
                    {
                        _pathMainWorksheet = fileSelector.FileName;

                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (var package = new ExcelPackage(new FileInfo(_pathMainWorksheet)))
                        {
                            var worksheet = package.Workbook.Worksheets[0];
                            var itemCol = 0;
                            var brandCol = 0;
                            var unitPriceCol = 0;

                            if (!IsMainWorksheetValid(worksheet))
                            {
                                MessageBox.Show("A planilha selecionada não possui a estrutura esperada.", "Erro - Planilha inválida",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                _pathMainWorksheet = string.Empty;
                            }
                            else
                            {
                                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                                {
                                    if (worksheet.Cells[1, col].Value?.ToString() == "ITEM")
                                    {
                                        itemCol = col;
                                        continue;
                                    }
                                    if (worksheet.Cells[1, col].Value?.ToString() == "MARCA")
                                    {
                                        brandCol = col;
                                        continue;
                                    }
                                    if (Regex.IsMatch(worksheet.Cells[1, col].Value?.ToString(), @"CUSTO\s*\+\s*\d+%"))
                                    {
                                        unitPriceCol = col;
                                        break;
                                    }
                                }

                                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    int numberTemp = 0;
                                    decimal unitValueTemp = 0;
                                    if (int.TryParse(worksheet.Cells[row, itemCol].Value?.ToString(), out numberTemp)
                                        && decimal.TryParse(worksheet.Cells[row, unitPriceCol].Value?.ToString(), out unitValueTemp))
                                    {
                                        _items.Add(numberTemp,
                                            new Item
                                            {
                                                Number = numberTemp,
                                                Brand = worksheet.Cells[row, brandCol].Value?.ToString(),
                                                UnitValue = unitValueTemp
                                            });
                                    }
                                }
                            }
                            if (package != null) package.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("O arquivo selecionado não é um arquivo Excel válido.",
                            "Erro - Formato de arquivo inválido", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (fileSelector.ShowDialog() == DialogResult.OK)
                {
                    if (IsExcelFile(fileSelector.FileName))
                    {
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
                            if (package != null) package.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("O arquivo selecionado não é um arquivo Excel válido.",
                            "Erro - Formato de arquivo inválido",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Btn_DataTransfer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pathMainWorksheet) || string.IsNullOrEmpty(_pathModelWorksheet))
            {
                MessageBox.Show("Você deve selecionar uma planilha antes", "Erro - Planilha não selecionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var mainPackage = new ExcelPackage(new FileInfo(_pathMainWorksheet)))
            using (var modelPackage = new ExcelPackage(new FileInfo(_pathModelWorksheet)))
            {
                var mainWorksheet = mainPackage.Workbook.Worksheets[0];
                var modelWorksheet = modelPackage.Workbook.Worksheets[0];

                int mainRow = 2;

                for (int i = 2; i <= modelWorksheet.Dimension.End.Row; i++)
                {
                    if (mainRow > mainWorksheet.Dimension.End.Row) break;

                    if (int.TryParse(modelWorksheet.Cells[i, 1].Value.ToString(), out var modelItem)
                        && _items.ContainsKey(modelItem))
                    {
                        modelWorksheet.Cells[i, 3].Value = _items[modelItem].UnitValue;
                        modelWorksheet.Cells[i, 4].Value = _items[modelItem].Brand;
                        modelWorksheet.Cells[i, 5].Value = _items[modelItem].Brand;
                        mainRow++;
                    }
                }

                modelPackage.Save();

                MessageBox.Show("Transferência de dados concluida!");

                if (mainPackage != null) mainPackage.Dispose();
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
                    modelWorksheet.Cells[i, 3].Value = string.Empty;
                    modelWorksheet.Cells[i, 4].Value = string.Empty;
                    modelWorksheet.Cells[i, 5].Value = string.Empty;
                }

                modelPackage.Save();

                MessageBox.Show("Planilha Resetada!");

                if (modelPackage != null) modelPackage.Dispose();
            }
        }

        private bool IsExcelFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension.Equals(".xls", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".xlsm", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsColumnFilled(ExcelWorksheet worksheet, int column)
        {
            int lastRow = worksheet.Dimension?.End.Row ?? 0;

            for (int row = 2; row <= lastRow; row++)
            {
                if (worksheet.Cells[row, column].Value == null)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsMainWorksheetValid(ExcelWorksheet worksheet)
        {
            if (worksheet.Dimension?.Rows < 2) return false;

            if (worksheet.Cells[1, 7].Value?.ToString() == null) return false;

            if (worksheet.Cells[1, 1].Value?.ToString() != "ITEM"
                || worksheet.Cells[1, 2].Value?.ToString() != "DESCRIÇÃO"
                || worksheet.Cells[1, 3].Value?.ToString() != "UND"
                || worksheet.Cells[1, 4].Value?.ToString() != "QTD"
                || worksheet.Cells[1, 5].Value?.ToString() != "MARCA"
                || worksheet.Cells[1, 6].Value?.ToString() != "VALOR DE CUSTO"
                || !Regex.IsMatch(worksheet.Cells[1, 7].Value?.ToString(), @"CUSTO\s*\+\s*\d+%")
                || worksheet.Cells[1, 8].Value?.ToString() != "VALOR TOTAL"
                || worksheet.Cells[1, 9].Value?.ToString() != "PERCENTUAL MÍNIMO"
                || worksheet.Cells[1, 10].Value?.ToString() != "VALOR MÍNIMO"
                || worksheet.Cells[1, 11].Value?.ToString() != "LANCE ATUAL"
                || worksheet.Cells[1, 12].Value?.ToString() != "POSIÇÃO"
                || worksheet.Cells[1, 13].Value?.ToString() != "VALOR TOTAL DO LANCE")
            {
                return false;
            }

            if (!IsColumnFilled(worksheet, 1)
                || !IsColumnFilled(worksheet, 2)
                || !IsColumnFilled(worksheet, 3)
                || !IsColumnFilled(worksheet, 4)
                || !IsColumnFilled(worksheet, 5)
                || !IsColumnFilled(worksheet, 6))
            {
                return false;
            }

            return true;
        }

        private bool IsModelWorksheetValid(ExcelWorksheet worksheet)
        {
            if (worksheet.Dimension?.Rows < 2) return false;

            if (worksheet.Cells[1, 1].Value?.ToString() != "Lote"
                || worksheet.Cells[1, 2].Value?.ToString() != "Item"
                || worksheet.Cells[1, 3].Value?.ToString() != "Valor Prop."
                || worksheet.Cells[1, 4].Value?.ToString() != "Marca"
                || worksheet.Cells[1, 5].Value?.ToString() != "Modelo")
            {
                return false;
            }

            if (!IsColumnFilled(worksheet, 1)
                || !IsColumnFilled(worksheet, 2))
            {
                return false;
            }

            return true;
        }
    }
}
