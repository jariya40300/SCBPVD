using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using SCBPVD.DataAccess.Data;
using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCBPVD
{
    public class SendEmailZip
    {
        private static readonly string Template = @"P:\Data Control\SCB\SCB_Statement\Template\PVD_1.html";
        private static IAccountData _db = new AccountData();
        public async static Task Send(string session_id, string emailIndex, CompanyZip companyZip, string file_report, int job_id)
        {


            CompanyZip company = companyZip;
            string message_id = company.message_id;
            string email_index = emailIndex;
            try
            {

                string filename = company.zip_file;
                string file_name_attach_report = file_report;

                if (File.Exists(filename))
                {

                    string[] email_list = company.email_to.Split(";");

                    if (email_list.Length > 0)
                    {
                        string file_att = filename;
                        string email_to = email_list[0];


                        List<ResponseTaximail.attmFile> list_file_attm = new List<ResponseTaximail.attmFile>();

                        string htmlBody = PopulateBody();
                        htmlBody = htmlBody.Replace("&", "%26");
                        byte[] b = File.ReadAllBytes(file_att);

                        string base64_file = Convert.ToBase64String(b);

                        String file_attm = base64_file.Replace("+", @"%2B");
                        file_attm = file_attm.Replace("/", @"%2F");
                        file_attm = file_attm.Replace("=", @"%3D");





                        list_file_attm.Add(new ResponseTaximail.attmFile { filename = Path.GetFileName(filename), file_data = file_attm });

                        if (file_name_attach_report != "")
                        {
                            string path_report = $"{Path.GetDirectoryName(company.zip_file)}\\{file_name_attach_report}";

                            if (File.Exists(path_report))
                            {
                                byte[] b_reoort = File.ReadAllBytes(path_report);
                                string base64_file_report = Convert.ToBase64String(b_reoort);
                                String report_file_attm = base64_file_report.Replace("+", @"%2B");
                                report_file_attm = report_file_attm.Replace("/", @"%2F");
                                report_file_attm = report_file_attm.Replace("=", @"%3D");
                                list_file_attm.Add(new ResponseTaximail.attmFile { filename = Path.GetFileName(file_name_attach_report), file_data = report_file_attm });
                            }
                            else
                            {
                                string status = "Reject";
                                string reason_reject = "Invalid file report attach";
                                await _db.UpdateAccounts(message_id, status, reason_reject, company.com_code, job_id);
                            }
                        }


                        List<ResponseTaximail.EmailCC> emailCCs = new List<ResponseTaximail.EmailCC>();

                        for (int i = 1; i < email_list.Length; i++)
                        {
                            emailCCs.Add(new ResponseTaximail.EmailCC { name = email_list[i], email = email_list[i] });
                        }


                        string file_send = JsonConvert.SerializeObject(list_file_attm);
                        string group = "SCBPVD";

                        string send_email_parms = "session_id=" + session_id + "&message_id=" + message_id + "&transactional_group_name=" + group + "&subject="
                                                        + $"แจ้งการจัดส่งรายงาน Statement ณ วันที่ 30 มิถุนายน 2564 / E-Statement PVD as of 30 June 2021 [{company.com_code}] {email_index}" + "&to_name=" + email_to + "&to_email=" + email_to +
                                                     "&from_name=" + "Noreply_Registrar_PVD" + "&from_email=" + "Noreply_Registrar_PVD@scb.co.th" + "&content_html=" + htmlBody + "&attachment=" + file_send;




                        if (emailCCs.Count > 0)
                        {
                            string email_cc = JsonConvert.SerializeObject(emailCCs);
                            send_email_parms = send_email_parms + "&cc=" + email_cc;
                        }



                        var client = new RestClient(@"https://api.taximail.com/v2/transactional");
                        var requst = new RestRequest(Method.POST);

                        requst.RequestFormat = DataFormat.Json;

                        requst.AddHeader("cache-control", "no-cache");
                        requst.AddHeader("content-type", "application/x-www-form-urlencoded");
                        requst.AddParameter("application/x-www-form-urlencoded", send_email_parms, ParameterType.RequestBody);


                        try
                        {
                            IRestResponse response = await client.ExecuteAsync(requst);
                            JsonDeserializer deserializer = new JsonDeserializer();

                            if (!response.Content.Contains("502 Bad Gateway"))
                            {
                                if (!response.Content.Equals(""))
                                {
                                    //var status = deserializer.Deserialize<ResponseTaximail.SendEmail>(response);

                                    if (response.Content.Contains("\"success\":true"))
                                    {
                                        await _db.UpdateAccounts(message_id, "Send", "", company.com_code, job_id);
                                    }
                                    else
                                    {
                                        await _db.UpdateAccounts(message_id, "Send", response.Content, company.com_code, job_id);
                                    }
                                }
                                else
                                {
                                    await _db.UpdateAccounts(message_id, "Reject", response.ErrorMessage, company.com_code, job_id);
                                }

                            }
                            else
                            {
                                await Send(session_id, email_index, company, file_name_attach_report, job_id);

                            }
                        }
                        catch (Exception ex)
                        {

                            await _db.UpdateAccounts(message_id, "Reject", ex.Message, company.com_code, job_id);
                        }
                    }

                }
                else
                {
                    await _db.UpdateAccounts(message_id, "Reject", "Invalid file attach", company.com_code, job_id);

                }

            }
            catch (Exception ex)
            {
                await _db.UpdateAccounts(message_id, "Reject", ex.Message, company.com_code, job_id);


            }



            //return account;


        }
        private static bool CheckFormatEmail(string EmailAddress)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(EmailAddress, expresion))
            {
                if (Regex.Replace(EmailAddress, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private static string PopulateBody()
        {

            string body = string.Empty;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = System.Text.Encoding.GetEncoding(874);
            using (StreamReader reader = new StreamReader(Template, encoding))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("{$NameTH$}", account.NameTH);
            //body = body.Replace("{$NameEN$}", account.NameEN);
            //body = body.Replace("{$Account$}", account.AccountNo);

            return body;
        }
    }
}
