using OfficeOpenXml;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PropProAssistant
{
    public partial class Form_PropTransfer : Form
    {
        private PriceBidWorksheet _priceBidWorksheet;
        private ModelWorksheet _modelWorksheet;

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

            string[] models = new string[] { "Selecione o Portal", "BNC", "Licitanet", "Portal de Compras Públicas" };
            Cbx_WorksheetModels.Items.AddRange(models);
            Cbx_WorksheetModels.SelectedIndex = 0;
            Cbx_WorksheetModels.Size = new Size(160, 21);
            Cbx_WorksheetModels.Location = new Point((int)((ClientSize.Width * 0.5) - (Cbx_WorksheetModels.Width * 0.5)), 130);
            Cbx_WorksheetModels.DropDownStyle = ComboBoxStyle.DropDownList;

            Btn_DataTransfer.Text = "Transferir Dados";
            Btn_DataTransfer.AutoSize = true;
            Btn_DataTransfer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Btn_DataTransfer.Location = new Point((int)((ClientSize.Width * 0.5) - (Btn_DataTransfer.Width * 0.5)), 180);
        }

        private void InitializeDebug()
        {
#if DEBUG
            DebugComponent.InitializeDebugControls(this);
#endif
        }

        private void Btn_PriceBidWorksheetSelector_Click(object sender, EventArgs e)
        {
            string path = GetWorksheetPath();
            if (string.IsNullOrEmpty(path)) return;

            _priceBidWorksheet = new PriceBidWorksheet(path);
            if (_priceBidWorksheet == null) return;

            bool validWorksheet = _priceBidWorksheet.Validate();
            if (!validWorksheet)
            {
                _priceBidWorksheet = null;
                MessageBox.Show("A planilha selecionada não possui a estrutura esperada.",
                    "Erro - Planilha inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _priceBidWorksheet.FillDictionary();
            }
        }

        private void Btn_ModelWorksheetSelector_Click(object sender, EventArgs e)
        {
            string path = GetWorksheetPath();
            if (string.IsNullOrEmpty(path)) return;

            int selectedPortal = Cbx_WorksheetModels.SelectedIndex;
            _modelWorksheet = GetModelWorksheet(path, selectedPortal);
            if (_modelWorksheet == null) return;

            bool validWorksheet = _modelWorksheet.Validate();
            if (!validWorksheet)
            {
                _modelWorksheet = null;
                MessageBox.Show("A planilha selecionada não possui a estrutura esperada.",
                    "Erro - Planilha inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_DataTransfer_Click(object sender, EventArgs e)
        {
            if (_priceBidWorksheet == null || _modelWorksheet == null)
            {
                MessageBox.Show("Você deve selecionar uma planilha antes",
                    "Erro - Planilha não selecionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var modelPackage = new ExcelPackage(new FileInfo(_modelWorksheet.Path)))
            {
                var modelWorksheet = modelPackage.Workbook.Worksheets[0];

                if (_priceBidWorksheet.Items.Keys.LastOrDefault() > modelWorksheet.Dimension.End.Row - 1)
                {
                    MessageBox.Show("A quantidade de itens da proposta é maior do que da planilha modelo.",
                        "Planilha Incorreta",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int row = 2; row <= modelWorksheet.Dimension.End.Row; row++)
                {
                    modelWorksheet.Cells[row, _modelWorksheet.UnitValueCol].Value = 0;
                }

                foreach (var item in _priceBidWorksheet.Items)
                {
                    modelWorksheet.Cells[item.Key + 1, _modelWorksheet.UnitValueCol].Value = item.Value.UnitPrice;
                    modelWorksheet.Cells[item.Key + 1, _modelWorksheet.BrandCol].Value = item.Value.Brand;
                    modelWorksheet.Cells[item.Key + 1, _modelWorksheet.ModelCol].Value = item.Value.Brand;
                }

                modelPackage.Save();

                MessageBox.Show("Transferência de dados concluida!");
            }
        }

        public void Btn_ResetButton_Click(object sender, EventArgs e)
        {
            if (_modelWorksheet == null)
            {
                MessageBox.Show("Você deve selecionar uma planilha antes",
                    "Erro - Planilha não selecionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var modelPackage = new ExcelPackage(new FileInfo(_modelWorksheet.Path)))
            {
                var modelWorksheet = modelPackage.Workbook.Worksheets[0];

                for (int row = 2; row <= modelWorksheet.Dimension.End.Row; row++)
                {
                    modelWorksheet.Cells[row, _modelWorksheet.BrandCol].Value = string.Empty;
                    modelWorksheet.Cells[row, _modelWorksheet.ModelCol].Value = string.Empty;
                    modelWorksheet.Cells[row, _modelWorksheet.UnitValueCol].Value = string.Empty;

                    //if (_modelWorksheet.DescriptionCol != 0)
                    //{
                    //    modelWorksheet.Cells[row, _modelWorksheet.DescriptionCol].Value = string.Empty;
                    //}
                    //if (_modelWorksheet.TotalValueCol != 0)
                    //{
                    //    modelWorksheet.Cells[row, _modelWorksheet.TotalValueCol].Value = string.Empty;
                    //}
                    //if (_modelWorksheet.AnvisaRegCol != 0)
                    //{
                    //    modelWorksheet.Cells[row, _modelWorksheet.AnvisaRegCol].Value = string.Empty;
                    //}
                }

                modelPackage.Save();

                MessageBox.Show("Planilha Resetada!");
            }
        }

        private string GetWorksheetPath()
        {
            string selectedFilePath = string.Empty;

            using (var fileSelector = new OpenFileDialog())
            {
                fileSelector.Title = "Selecione uma planilha";
                fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                fileSelector.Multiselect = false;

                if (fileSelector.ShowDialog() == DialogResult.OK)
                {
                    if (IsExcelFile(fileSelector.FileName))
                        selectedFilePath = fileSelector.FileName;
                    else
                        MessageBox.Show("O arquivo selecionado não é um arquivo Excel válido.",
                            "Erro - Formato de arquivo inválido",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return selectedFilePath;
            }
        }

        private bool IsExcelFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension.Equals(".xls", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".xlsm", StringComparison.OrdinalIgnoreCase);
        }

        private ModelWorksheet GetModelWorksheet(string path, int portal)
        {
            switch (portal)
            {
                case 0:
                    MessageBox.Show("Selecione um Portal");
                    return null;
                case 1:
                    return new BncModel(path);
                case 2:
                    return new LicitanetModel(path);
                case 3:
                    return new PcpModel(path);
                default:
                    throw new NotImplementedException($"Situação não esperada. Portal escolhido: {portal}");
            }
        }
    }
}
