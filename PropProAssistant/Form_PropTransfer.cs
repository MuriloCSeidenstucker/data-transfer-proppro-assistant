﻿using OfficeOpenXml;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PropProAssistant
{
    public partial class Form_PropTransfer : Form
    {
        private Button Btn_ResetButton;
        private Label Lbl_DebugLabel;
        private Label Lbl_DebugMenuLabel;

        private string _pathMainWorksheet = string.Empty;
        private string _pathModelWorksheet = string.Empty;

        public Form_PropTransfer()
        {
            InitializeComponent();

            Btn_MainWorksheetSelector.Text = "Selecionar Planilha Origem";
            Btn_MainWorksheetSelector.AutoSize = true;

            Btn_ModelWorksheetSelector.Text = "Selecionar Planilha Modelo";
            Btn_ModelWorksheetSelector.AutoSize = true;

            Btn_DataTransfer.Text = "Transferir Dados";
            Btn_DataTransfer.AutoSize = true;

#if DEBUG
            Lbl_DebugLabel = new Label();
            Lbl_DebugLabel.Text = "DEBUG MODE";
            Lbl_DebugLabel.ForeColor = Color.Red;
            Lbl_DebugLabel.Font = new Font(Font.FontFamily, 12, FontStyle.Bold);
            Lbl_DebugLabel.AutoSize = true;
            Lbl_DebugLabel.Location = new Point(ClientSize.Width - Lbl_DebugLabel.Width - 50, ClientSize.Height - Lbl_DebugLabel.Height - 10);
            Lbl_DebugLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(Lbl_DebugLabel);

            Lbl_DebugMenuLabel = new Label();
            Lbl_DebugMenuLabel.Text = "DEBUG MENU";
            Lbl_DebugMenuLabel.ForeColor = Color.Red;
            Lbl_DebugMenuLabel.Font = new Font(Font.FontFamily, 12, FontStyle.Bold);
            Lbl_DebugMenuLabel.AutoSize = true;
            Lbl_DebugMenuLabel.Location = new Point(ClientSize.Width - Lbl_DebugMenuLabel.Width - 50, ClientSize.Height - Lbl_DebugMenuLabel.Height - 100);
            Lbl_DebugMenuLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(Lbl_DebugMenuLabel);

            Btn_ResetButton = new Button();
            Btn_ResetButton.Text = "Reset";
            Btn_ResetButton.Location = new Point(ClientSize.Width - Btn_ResetButton.Width - 50, ClientSize.Height - Btn_ResetButton.Height - 80);
            Btn_ResetButton.Click += new System.EventHandler(Btn_ResetButton_Click);
            this.Controls.Add(Btn_ResetButton);
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

                            if (!IsMainWorksheetValid(worksheet))
                            {
                                MessageBox.Show("A planilha selecionada não possui a estrutura esperada.", "Erro - Planilha inválida",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                _pathMainWorksheet = string.Empty;
                            }
                            package.Dispose();
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

                for (int i = 2; i < modelWorksheet.Dimension.End.Row; i++)
                {
                    if (mainRow > mainWorksheet.Dimension.End.Row) break;

                    if (int.TryParse(modelWorksheet.Cells[i, 1].Value.ToString(), out var modelItem)
                        && int.TryParse(mainWorksheet.Cells[mainRow, 1].Value.ToString(), out var mainItem)
                        && modelItem == mainItem)
                    {
                        modelWorksheet.Cells[i, 3].Value = mainWorksheet.Cells[mainRow, 7].Value;
                        modelWorksheet.Cells[i, 4].Value = mainWorksheet.Cells[mainRow, 5].Value;
                        modelWorksheet.Cells[i, 5].Value = mainWorksheet.Cells[mainRow, 5].Value;
                        mainRow++;
                    }
                }

                modelPackage.Save();

                MessageBox.Show("Transferência de dados concluida!");

                if (mainPackage != null) mainPackage.Dispose();
                if (modelPackage != null) modelPackage.Dispose();
            }
        }

        private void Btn_ResetButton_Click(object sender, EventArgs e)
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

                for (int i = 2; i < modelWorksheet.Dimension.End.Row; i++)
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
    }
}
