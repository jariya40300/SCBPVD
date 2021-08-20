using iTextSharp.text.pdf;
using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SCBPVD
{
    public class EncryptPDF
    {
        public static async Task<Tuple<bool, string>> Encrypt(Account acc)
        {
            string file_name_new = string.Empty;
            bool result = true;
            Account account = new Account();
            account = acc;

            try
            {
                string path = Path.GetDirectoryName(account.filename_txt);
                string InputFile = Path.Combine(path, account.filename_pdf);
                if (File.Exists(InputFile))
                {
                    file_name_new = account.filename_pdf.Substring(account.filename_pdf.IndexOf("pvd_") + 4, account.filename_pdf.Length - 4 - (account.filename_pdf.IndexOf("pvd_")));


                    string OutputFile = Path.Combine(path, file_name_new);
                    //byte[] data = File.ReadAllBytes(InputFile);

                    using (Stream input = new FileStream(InputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (Stream output = new FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                        {

                            PdfReader reader = new PdfReader(input);
                            PdfEncryptor.Encrypt(reader, output, true, account.password, account.password, PdfWriter.ALLOW_SCREENREADERS);
                        }
                    }
                }
                else
                {
                    file_name_new = "ไม่พบไฟล์ PDF";
                    result = false;
                }



            }
            catch (Exception ex)
            {

                file_name_new = ex.Message;
                result = false; ;
            }
            return Tuple.Create(result, file_name_new);
        }
    }
}
