using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office;
using System.Data;

namespace ADManager
{
    class Report
    {

        private readonly System.Data.DataTable _dataTable;

        public Report(System.Data.DataTable _dataTable)
        {
            this._dataTable = _dataTable;

        }

        /// <summary>
        /// Get report for Users And Computers.
        /// Kullanıcı ve Bilgisayar bazlı raporları aldığımız method.
        /// İlgili formlardaki Datagridview dataları datatable olarak bu sınıfa aktarılır. Bu sınıf vasıtasıyla Excel Rapor oluşturulur.
        /// </summary>

        public string ReportGridviewToExcel()
        {

            string reporState = string.Empty;
            // Excel uygulama oluşturuyoruz. 
            Microsoft.Office.Interop.Excel._Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Workbook oluşturma
            Microsoft.Office.Interop.Excel._Workbook workBook = excelApp.Workbooks.Add(Type.Missing);

            //Workbook içerisinde çalışma alanı tanımlama
            Microsoft.Office.Interop.Excel.Worksheet workSheet = null;

            excelApp.Visible = true;
            try
            {
             // İlk çalışma sayfasını referans olarak aldık.
                workSheet = workBook.ActiveSheet;
                workSheet.Name = "Liste";

                for (int i = 1; i < _dataTable.Columns.Count + 1; i++)
                {
                    workSheet.Cells[1, i] = _dataTable.Columns[i - 1].ColumnName;
                }

                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < _dataTable.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = _dataTable.Rows[i][j];
                    }
                }

                excelApp.Quit();
                reporState = "Başarıyla Veriler Aktarıldı";

            }

            catch (Exception ex)
            {

                reporState = ex.Message;
            }

            return reporState;

        }


    }
}
