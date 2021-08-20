using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SCBPVD
{
    public class ReportAttach
    {
        public static void Create(List<Account> _accounts, Company _company, string file_name)
        {

            string file_name_report = string.Empty;
            List<Account> accounts = new List<Account>();
            Company company = new Company();
            try
            {
                company = _company;
                accounts = _accounts;

                if (accounts.Count > 0)
                {
                    int height = accounts.Count;

                    string path = accounts[0].filename_txt;
                    string filename = file_name;
                    string sheet_name = company.company_code;


                    //sheet_name = sheet_name.Replace(".txt", "");
                    //sheet_name = sheet_name.Substring(0, sheet_name.IndexOf("d") + 1);


                    object[,] resultlist = new object[height, 7];

                    for (int i = 0; i < height; i++)
                    {
                        resultlist[i, 0] = accounts[i].com_code;
                        resultlist[i, 1] = accounts[i].no;
                        resultlist[i, 2] = company.company_name;
                        resultlist[i, 3] = accounts[i].dept_name;
                        resultlist[i, 4] = accounts[i].member_code;
                        resultlist[i, 5] = accounts[i].member_name;
                        resultlist[i, 6] = accounts[i].file_name_new;
                    }

                    Excel.Application xlApp1;
                    Excel.Workbook xlWorkbook1;
                    Excel.Worksheet xlWorkSheet1;

                    object misValue = System.Reflection.Missing.Value;

                    Excel.Range ChartRange1;

                    xlApp1 = new Excel.Application();
                    xlWorkbook1 = xlApp1.Workbooks.Add(misValue);
                    xlWorkSheet1 = (Excel.Worksheet)xlWorkbook1.Worksheets.get_Item(1);
                    xlWorkSheet1.Name = sheet_name;


                    ChartRange1 = xlWorkSheet1.get_Range("A1");
                    ChartRange1.FormulaR1C1 = "CompanyCode";
                    ChartRange1 = xlWorkSheet1.get_Range("B1");
                    ChartRange1.FormulaR1C1 = "PDF_running";
                    ChartRange1 = xlWorkSheet1.get_Range("C1");
                    ChartRange1.FormulaR1C1 = "CompanyName";
                    ChartRange1 = xlWorkSheet1.get_Range("D1");
                    ChartRange1.FormulaR1C1 = "DeptName";
                    ChartRange1 = xlWorkSheet1.get_Range("E1");
                    ChartRange1.FormulaR1C1 = "MemberCode";
                    ChartRange1 = xlWorkSheet1.get_Range("F1");
                    ChartRange1.FormulaR1C1 = "MemberName";
                    ChartRange1 = xlWorkSheet1.get_Range("G1");
                    ChartRange1.FormulaR1C1 = "FilenamePDF(NEW)";


                    ChartRange1 = xlWorkSheet1.get_Range("A1", "A" + height + 1);
                    ChartRange1.NumberFormat = "@";
                    ChartRange1 = xlWorkSheet1.get_Range("E1", "E" + height + 1);
                    ChartRange1.NumberFormat = "@";


                    var startCell = (Excel.Range)xlWorkSheet1.Cells[2, 1];
                    var endCell = (Excel.Range)xlWorkSheet1.Cells[height + 1, 7];

                    var writeRange = xlWorkSheet1.Range[startCell, endCell];

                    writeRange.Value2 = resultlist;



                    xlApp1.DisplayAlerts = false;
                    xlWorkbook1.CheckCompatibility = false;
                    xlWorkbook1.DoNotPromptForConvert = true;

                    file_name_report = $"{Path.GetDirectoryName(path)}\\{filename}";

                    xlWorkbook1.SaveAs(file_name_report, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkbook1.Close();
                    xlApp1.Quit();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public static void SetPassword(string file_name, string pin)
        {
            string password = pin;
            string file_name_report = file_name;
            try
            {
                Excel.Application xlApp1;
                object misValue = System.Reflection.Missing.Value;

                xlApp1 = new Excel.Application();

                Excel.Workbook xlWorkBook = xlApp1.Workbooks.Open(file_name_report);
                xlWorkBook.Password = password;
                xlWorkBook.Save();
                xlWorkBook.Close();
                xlApp1.Quit();

            }
            catch (Exception ex)
            {

                throw;
            }


        }
    }
}
