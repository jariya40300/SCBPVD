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
    public class SendEmailNormal
    {
        private static readonly string Template = @"P:\Data Control\SCB\SCB_Statement\Template\PVD_2.html";
        private static IAccountData _db = new AccountData();
        public async static Task Send(Account _account, string session_id)
        {
            Account account = new Account();
            account = _account;

            //account.email_to = "jariya.b@papermate.co.th";
            //account.member_id = Guid.NewGuid().ToString();


            try
            {
                //if (CheckFormatEmail(account.email_to))
                //{              
                string path_file_att = Path.GetDirectoryName(account.filename_txt);

                if (File.Exists(path_file_att + "\\" + account.file_name_new))
                    {

                        string file_att = path_file_att + "\\" + account.file_name_new;

                        string htmlBody = PopulateBody(account);
                        htmlBody = htmlBody.Replace("&", "%26");
                        byte[] b = await File.ReadAllBytesAsync(file_att);

                        string base64_file = Convert.ToBase64String(b);

                        String file_attm = base64_file.Replace("+", @"%2B");
                        file_attm = file_attm.Replace("/", @"%2F");
                        file_attm = file_attm.Replace("=", @"%3D");


                        List<ResponseTaximail.attmFile> list_file_attm = new List<ResponseTaximail.attmFile>();
                        list_file_attm.Add(new ResponseTaximail.attmFile { filename = account.file_name_new, file_data = file_attm });
                        string file_send = JsonConvert.SerializeObject(list_file_attm);
                        string group = "SCBPVD";

                        string send_email_parms = "session_id=" + session_id + "&message_id=" + account.message_id + "&transactional_group_name=" + group + "&subject="
                                                        + $"แจ้งการจัดส่งรายงาน Statement ณ วันที่ 30 มิถุนายน 2564 / E-Statement PVD as of 30 June 2021 [{account.com_code} - {account.member_code}]" + "&to_name=" + account.email_to + "&to_email=" + account.email_to +
                                                     "&from_name=" + "Noreply_Registrar_PVD" + "&from_email=" + "Noreply_Registrar_PVD@scb.co.th" + "&content_html=" + htmlBody + "&attachment=" + file_send;


                        var client = new RestClient(@"https://api.taximail.com/v2/transactional");
                        var requst = new RestRequest(Method.POST);

                        requst.RequestFormat = DataFormat.Json;

                        requst.AddHeader("cache-control", "no-cache");
                        requst.AddHeader("content-type", "application/x-www-form-urlencoded");
                        requst.AddParameter("application/x-www-form-urlencoded", send_email_parms, ParameterType.RequestBody);

                        IRestResponse response = client.Execute(requst);
                        JsonDeserializer deserializer = new JsonDeserializer();

                        if (!response.Content.Contains("502 Bad Gateway"))
                        {
                            var status = deserializer.Deserialize<ResponseTaximail.SendEmail>(response);

                            if (status.status == "success")
                            {
                                account.status = "Send";
                                account.send_date = DateTime.Now;
                                account.is_send = 1;
                                await _db.UpdateAccounts(account).ConfigureAwait(false);
                            }
                            else
                            {
                                account.status = "Reject";
                                account.send_date = DateTime.Now;
                                account.reason_reject = status.err_msg;
                                account.is_send = 1;
                                await _db.UpdateAccounts(account);

                            }
                        }
                        else
                        {
                            await Send(account, session_id);

                        }
                    }
                    else
                    {
                        account.send_date = DateTime.Now;
                        account.status = "Reject";
                        account.reason_reject = "Invalid file attach";
                        account.is_send = 1;
                        await _db.UpdateAccounts(account);
                    }



                //}
                //else
                //{
                //    account.send_date = DateTime.Now;
                //    account.status = "Reject";
                //    account.reason_reject = "Invalid Email";
                //    account.is_send = 1;
                //    await _db.UpdateAccounts(account);
                //}
            }
            catch (Exception ex)
            {
                account.send_date = DateTime.Now;
                account.status = "Reject";
                account.reason_reject = ex.Message;
                account.is_send = 1;
                await _db.UpdateAccounts(account);
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

        private static string PopulateBody(Account account)
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
