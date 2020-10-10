using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Spreadsheet;

namespace CHADEV
{
    //ADD DevExpress.Office.Core
    //ADD DevExpress.Spreadsheet.Core
    public partial class XLS : DevExpress.XtraEditors.XtraForm
    {
        public XLS()
        {
            InitializeComponent();
        }

        private void XLS_Load(object sender, EventArgs e)
        {
            // Create a new workbook.
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];
            // Access the first worksheet in the workbook.


            // Set the unit of measurement.
            workbook.Unit = DevExpress.Office.DocumentUnit.Point;

            workbook.BeginUpdate();
            try
            {
                // Create a multiplication table.
                worksheet.Cells["A1"].Value = "*";
                for (int i = 1; i < 11; i++)
                {
                    // Create the header column.
                    worksheet.Columns["A"][i].Value = i;
                    // Create the header row.
                    worksheet.Rows["1"][i].Value = i;
                }

                // Multiply values of header cells.
                worksheet.Range["B2:K11"].Formula = "=B$1*$A2";

                // Obtain the data range.
                CellRange tableRange = worksheet.GetDataRange();

                // Specify the row height and column width.
                tableRange.RowHeight = 40;
                tableRange.ColumnWidth = 40;

                // Align the table content.
                tableRange.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                tableRange.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;

                // Fill the header cells.
                CellRange headerCells = worksheet.Range.Union(worksheet.Range["A1:K1"], worksheet.Range["A2:A11"]);
                headerCells.FillColor = Color.FromArgb(0xf7, 0x9b, 0x77);
                headerCells.Font.Bold = true;

                // Fill cells that contain multiplication results.
                worksheet.Range["B2:K11"].FillColor = Color.FromArgb(0xfe, 0xf2, 0xe4);
            }
            finally
            {
                workbook.EndUpdate();
            }

            // Calculate the workbook.
            workbook.Calculate();

            // Save the document file under the specified name.
            workbook.SaveDocument("TestDoc.xlsx", DocumentFormat.OpenXml);

            // Export the document to PDF.
            workbook.ExportToPdf("TestDoc.pdf");

            // Open the PDF document using the default viewer.
            System.Diagnostics.Process.Start("TestDoc.pdf");

            // Open the XLSX document using the default application.
            System.Diagnostics.Process.Start("TestDoc.xlsx");
        }
    }
}