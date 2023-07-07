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
                    oSheet = oWB.Worksheets["Principal"];

                    oRng = oSheet.get_Range("A1", "E1");
                    var values = oRng.Value2;

                    foreach (var value in values)
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
