using OfficeOpenXml;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PropProAssistant
{
    public partial class Form_PropTransfer : Form
    {
        private Button Btn_ResetButton;
        private Label Lbl_DebugLabel;
        private Label Lbl_DebugMenuLabel;

        private string _pathMainSheet = string.Empty;
        private string _pathModelSheet = string.Empty;

        public Form_PropTransfer()
        {
            InitializeComponent();

            Btn_MainSheetSelector.Text = "Selecionar Planilha Origem";
            Btn_MainSheetSelector.AutoSize = true;

            Btn_ModelSheetSelector.Text = "Selecionar Planilha Modelo";
            Btn_ModelSheetSelector.AutoSize = true;

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

        private void Btn_MainSheetSelector_Click(object sender, EventArgs e)
        {
            using (var fileSelector = new OpenFileDialog())
            {
                fileSelector.Title = "Selecionar Planilha Origem";
                fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                if (fileSelector.ShowDialog() == DialogResult.OK)
                {
                    _pathMainSheet = fileSelector.FileName;
                }
            }
        }

        private void Btn_ModelSheetSelector_Click(object sender, EventArgs e)
        {
            using (var fileSelector = new OpenFileDialog())
            {
                fileSelector.Title = "Selecionar Planilha Modelo";
                fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                if (fileSelector.ShowDialog() == DialogResult.OK)
                {
                    _pathModelSheet = fileSelector.FileName;
                }
            }
        }

        private void Btn_DataTransfer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pathMainSheet) || string.IsNullOrEmpty(_pathModelSheet))
            {
                MessageBox.Show("Você deve selecionar uma planilha antes", "Erro - Planilha não selecionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var mainPackage = new ExcelPackage(new FileInfo(_pathMainSheet)))
            using (var modelPackage = new ExcelPackage(new FileInfo(_pathModelSheet)))
            {
                var mainSheet = mainPackage.Workbook.Worksheets[0];
                var modelSheet = modelPackage.Workbook.Worksheets[0];

                int mainRow = 2;

                for (int i = 2; i < modelSheet.Dimension.End.Row; i++)
                {
                    if (mainRow > mainSheet.Dimension.End.Row) break;

                    if (int.TryParse(modelSheet.Cells[i, 1].Value.ToString(), out var modelItem)
                        && int.TryParse(mainSheet.Cells[mainRow, 1].Value.ToString(), out var mainItem)
                        && modelItem == mainItem)
                    {
                        modelSheet.Cells[i, 3].Value = mainSheet.Cells[mainRow, 7].Value;
                        modelSheet.Cells[i, 4].Value = mainSheet.Cells[mainRow, 5].Value;
                        modelSheet.Cells[i, 5].Value = mainSheet.Cells[mainRow, 5].Value;
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
            if (string.IsNullOrEmpty(_pathModelSheet))
            {
                MessageBox.Show("Você deve selecionar uma planilha antes", "Erro - Planilha não selecionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var modelPackage = new ExcelPackage(new FileInfo(_pathModelSheet)))
            {
                var modelSheet = modelPackage.Workbook.Worksheets[0];

                for (int i = 2; i < modelSheet.Dimension.End.Row; i++)
                {
                    modelSheet.Cells[i, 3].Value = string.Empty;
                    modelSheet.Cells[i, 4].Value = string.Empty;
                    modelSheet.Cells[i, 5].Value = string.Empty;
                }

                modelPackage.Save();

                MessageBox.Show("Planilha Resetada!");

                if (modelPackage != null) modelPackage.Dispose();
            }
        }
    }
}
