using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SCBPVD
{
    public class ReportSuammary
    {


        public static async Task Create(List<Company> companies, List<Account> accounts, string path)
        {
            try
            {
                if (companies.Count > 0)
                {
                    int height = companies.Count;



                    string sheet_name = "Summary";
                    object[,] resultlist = new object[height, 9];

                    for (int i = 0; i < height; i++)
                    {
                        string type = string.Empty;


                        resultlist[i, 0] = companies[i].company_code;
                        resultlist[i, 1] = companies[i].company_name;
                        resultlist[i, 2] = companies[i].send_by.Contains("C") ? "1" : "2";
                        resultlist[i, 3] = companies[i].send_by.Contains("E") ? "Email" : "CD";
                        resultlist[i, 4] = companies[i].total_account;
                        resultlist[i, 5] = companies[i].total_account_text;
                        resultlist[i, 6] = companies[i].total_success;
                        resultlist[i, 7] = companies[i].total_reject;
                        resultlist[i, 8] = companies[i].error_message;

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
                    ChartRange1.FormulaR1C1 = "CompanyName";
                    ChartRange1 = xlWorkSheet1.get_Range("C1");
                    ChartRange1.FormulaR1C1 = "Type";
                    ChartRange1 = xlWorkSheet1.get_Range("D1");
                    ChartRange1.FormulaR1C1 = "Delivery (CD / email)";
                    ChartRange1 = xlWorkSheet1.get_Range("E1");
                    ChartRange1.FormulaR1C1 = "Total_csv";
                    ChartRange1 = xlWorkSheet1.get_Range("F1");
                    ChartRange1.FormulaR1C1 = "Total_text";
                    ChartRange1 = xlWorkSheet1.get_Range("G1");
                    ChartRange1.FormulaR1C1 = "Total_success";
                    ChartRange1 = xlWorkSheet1.get_Range("H1");
                    ChartRange1.FormulaR1C1 = "Total_reject";
                    ChartRange1 = xlWorkSheet1.get_Range("I1");
                    ChartRange1.FormulaR1C1 = "Reason_reject";

                    ChartRange1 = xlWorkSheet1.get_Range("A1", "A" + height + 1);
                    ChartRange1.NumberFormat = "@";



                    var startCell = (Excel.Range)xlWorkSheet1.Cells[2, 1];
                    var endCell = (Excel.Range)xlWorkSheet1.Cells[height + 1, 9];

                    var writeRange = xlWorkSheet1.Range[startCell, endCell];

                    writeRange.Value2 = resultlist;




                    if (accounts.Count > 0)
                    {
                        int height_acc = accounts.Count;
                        xlWorkbook1.Sheets.Add(After: xlWorkbook1.Sheets[xlWorkbook1.Sheets.Count]);
                        xlWorkSheet1 = (Excel.Worksheet)xlWorkbook1.Worksheets.get_Item(2);
                        xlWorkSheet1.Name = "Error";

                        ChartRange1 = xlWorkSheet1.get_Range("A1");
                        ChartRange1.FormulaR1C1 = "CompanyCode";
                        ChartRange1 = xlWorkSheet1.get_Range("B1");
                        ChartRange1.FormulaR1C1 = "PDF_running";
                        ChartRange1 = xlWorkSheet1.get_Range("C1");
                        ChartRange1.FormulaR1C1 = "DeptCode";
                        ChartRange1 = xlWorkSheet1.get_Range("D1");
                        ChartRange1.FormulaR1C1 = "MemberCode";
                        ChartRange1 = xlWorkSheet1.get_Range("E1");
                        ChartRange1.FormulaR1C1 = "MemberID";
                        ChartRange1 = xlWorkSheet1.get_Range("F1");
                        ChartRange1.FormulaR1C1 = "MemberName";
                        ChartRange1 = xlWorkSheet1.get_Range("G1");
                        ChartRange1.FormulaR1C1 = "ReasonError";


                        ChartRange1 = xlWorkSheet1.get_Range("A1", "A" + height_acc + 1);
                        ChartRange1.NumberFormat = "@";
                        ChartRange1 = xlWorkSheet1.get_Range("C1", "E" + height_acc + 1);
                        ChartRange1.NumberFormat = "@";




                        object[,] resultlist_error = new object[height_acc, 7];

                        for (int i = 0; i < height_acc; i++)
                        {
                            resultlist_error[i, 0] = accounts[i].com_code;
                            resultlist_error[i, 1] = accounts[i].no;
                            resultlist_error[i, 2] = accounts[i].depart_code;
                            resultlist_error[i, 3] = accounts[i].member_code;
                            resultlist_error[i, 4] = accounts[i].member_id;
                            resultlist_error[i, 5] = accounts[i].member_name;
                            resultlist_error[i, 6] = accounts[i].reason_reject;
                        }


                        var startCell_error = (Excel.Range)xlWorkSheet1.Cells[2, 1];
                        var endCell_error = (Excel.Range)xlWorkSheet1.Cells[height_acc + 1, 7];

                        var writeRange_error = xlWorkSheet1.Range[startCell_error, endCell_error];

                        writeRange_error.Value2 = resultlist_error;



                    }

                    xlApp1.DisplayAlerts = false;
                    xlWorkbook1.CheckCompatibility = false;
                    xlWorkbook1.DoNotPromptForConvert = true;

                    string file_name_report = $"{path}\\Summary_Report_{DateTime.Now.ToString("yyyyMMdd")}.xls";

                    xlWorkbook1.SaveAs(file_name_report, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkbook1.Close();

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
