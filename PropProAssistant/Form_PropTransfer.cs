using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PropProAssistant
{
    public partial class Form_PropTransfer : Form
    {
        public Form_PropTransfer()
        {
            InitializeComponent();

            Btn_FileSelector.Text = "Selecionar Planilha";
            Btn_FileSelector.AutoSize = true;
        }

        private void Btn_FileSelector_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileSelector = new OpenFileDialog();
            fileSelector.Title = "Selecionar Planilha";
            fileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileSelector.FileName;
                Excel.Application oXL = null;
                Excel._Workbook oWB = null;
                Excel._Worksheet oSheet = null;
                Excel.Range oRng = null;

                try
                {
                    oXL = new Excel.Application();
                    oXL.Visible = false;

                    oWB = oXL.Workbooks.Open(filePath);
                    oSheet = oWB.ActiveSheet;

                    var startCell = "A1";
                    var endRow = oSheet.Rows.End[Excel.XlDirection.xlDown].Address.Split('$');
                    var rowCount = int.Parse(endRow[2]);
                    var endColumn = oSheet.Columns.End[Excel.XlDirection.xlToRight].Address.Split('$');
                    var columnCount = endColumn[1];
                    var endCell = $"{columnCount}{rowCount}";
                    var header = oSheet.get_Range(startCell, $"{columnCount}1");

                    oRng = oSheet.get_Range(startCell, endCell);

                    foreach (var value in oSheet.get_Range(startCell, $"A{rowCount}").Value2)
                    {
                        MessageBox.Show(value.ToString());
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu uma exceção: " + ex.Message);
                }
                finally
                {
                    if (oWB != null) oWB.Close();
                    if (oXL != null) oXL.Quit();
                }
            }
        }
    }
}
