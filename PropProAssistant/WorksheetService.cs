using OfficeOpenXml;

namespace PropProAssistant
{
    public static class WorksheetService
    {
        public static bool IsSomeColumnCellFilled(ExcelWorksheet worksheet, int column)
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
    }
}
