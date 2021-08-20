using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SCBPVD
{
    class Reprint
    {
        public static async Task Create(List<Account> accounts,string path_file)
        {
            try
            {
                if (accounts.Count > 0)
                {
                    string path = path_file;

                    if (path != null)
                    {
                        string new_path = path.Replace("Good", "Print");

                        if (!Directory.Exists(new_path))
                        {
                            Directory.CreateDirectory(new_path);
                        }

                        if (accounts[0].filename_txt != null && accounts[0].filename_txt != "")
                        {
                            string new_text_file = new_path + "\\" + Path.GetFileName(accounts[0].filename_txt);

                            using (StreamWriter sw = File.CreateText(new_text_file))
                            {
                                sw.WriteLine("QR_tracking|CompanyCode|PDF_running|CompanyName|ContractName|" +
                                    "Address1|Address2|Address3|Province|Zipcode|Tel|DeptCode|DeptName|MemberCode|" +
                                    "MemberName|FilenamePDF|SeqLot");
                                foreach (var item in accounts)
                                {
                                    if (item.filename_pdf != null && item.filename_pdf != "")
                                    {
                                        string txt =
                                       $"{item.qr_tracking}" +
                                       $"|{item.com_code}" +
                                       $"|{item.no}" +
                                       $"|{item.company_name}" +
                                       $"|{item.contract_name}" +
                                       $"|{item.address1}" +
                                       $"|{item.address2}" +
                                       $"|{item.address3}" +
                                       $"|{item.province}" +
                                       $"|{item.zipcode}" +
                                       $"|{item.tel}" +
                                       $"|{item.depart_code}" +
                                       $"|{item.dept_name}" +
                                       $"|{item.member_code}" +
                                       $"|{item.member_name}" +
                                       $"|{item.filename_pdf}" +
                                       $"|{item.seq_lot}"; ;
                                        sw.WriteLine(txt);
                                        File.Copy(path + "\\" + item.filename_pdf, new_path + "\\" + item.filename_pdf, true);
                                    }


                                }


                            }
                        }

                        
                    }


                }
            }
            catch (Exception ex)
            {

                throw;
            }

           
        }
    }
}
