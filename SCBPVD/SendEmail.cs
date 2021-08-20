using ExcelDataReader;
using SCBPVD.DataAccess.Data;
using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCBPVD
{
    public partial class SendEmail : Form
    {
        private ICompanyData _db_company = new CompanyData();
        private IAccountData _db_account = new AccountData();
        public Job job;

        private List<Company> companies = new List<Company>();

        public SendEmail()
        {
            InitializeComponent();
        }

        private async void SendEmail_Load(object sender, EventArgs e)
        {
            try
            {
                companies = new List<Company>();
                companies = await _db_company.GetCompaniesSendEmail(job.id);

                lb_job.Text = $"เลขที่ {job.id} รอบงาน {job.create_date}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }



        }

        private async Task SendEC()
        {

            try
            {
                ResponseTaximail.SessionStatus sessionStatus = await Session.GetSession();

                if (sessionStatus.status == "success")
                {
                    string session_id = sessionStatus.data.session_id;

                    int total = companies.Count;

                    progressBar_send_email.Maximum = total;
                    progressBar_send_email.Value = 0;

                    lb_total.Text = total.ToString();

                    int count = 0;

                    foreach (var item in companies)
                    {
                        List<CompanyZip> companyZips = new List<CompanyZip>();

                        companyZips = await _db_account.GetAccountsZip(job.id, item.company_code);

                        string path_report = $"{Path.GetDirectoryName(companyZips[0].zip_file)}\\{item.file_report_attach}";
                        ReportAttach.SetPassword(path_report, item.company_code);

                        int total_email = companyZips.Count();
                        int index = 1;


                        Thread.Sleep(2000);

                        foreach (var companyZip in companyZips)
                        {
                            string file_report = index == total_email ? item.file_report_attach : "";
                            string email_index = total_email == 1 ? "" : $"{index}/{total_email}";
                            await SendEmailZip.Send(session_id, email_index, companyZip, file_report, job.id).ConfigureAwait(false);
                            index++;
                        }
                        count++;
                        progressBar_send_email.Value = count;
                        lb_send.Text = count.ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }



        }

        private async Task SendEA()
        {

            try
            {
                ResponseTaximail.SessionStatus sessionStatus = await Session.GetSession();

                if (sessionStatus.status == "success")
                {
                    string session_id = sessionStatus.data.session_id;

                    List<Account> accounts = new List<Account>();
                    accounts = await _db_account.GetAccountsSendEmail(job.id);

                    int total = accounts.Count;

                    progressBar_send_email.Maximum = total;
                    progressBar_send_email.Value = 0;

                    lb_total.Text = total.ToString();

                    int count = 0;

                    foreach (var item in accounts)
                    {
                        SendEmailNormal.Send(item, session_id).ConfigureAwait(false);
                        count++;
                        progressBar_send_email.Value = count;
                        lb_send.Text = count.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }



        }


        private async void btn_send_email_Click(object sender, EventArgs e)
        {

            try
            {
                var type = companies.First().send_by;

                if (type == "EC")
                {

                    try
                    {
                        ResponseTaximail.SessionStatus sessionStatus = await Session.GetSession();

                        if (sessionStatus.status == "success")
                        {
                            string session_id = sessionStatus.data.session_id;

                            int total = companies.Count;

                            progressBar_send_email.Maximum = total;
                            progressBar_send_email.Value = 0;

                            lb_total.Text = total.ToString();

                            int count = 0;

                            foreach (var item in companies)
                            {
                                List<CompanyZip> companyZips = new List<CompanyZip>();

                                companyZips = await _db_account.GetAccountsZip(job.id, item.company_code);


                                if (companyZips.Count>0)
                                {
                                    string path_report = $"{Path.GetDirectoryName(companyZips[0].zip_file)}\\{item.file_report_attach}";
                                    ReportAttach.SetPassword(path_report, item.company_code);

                                    int total_email = companyZips.Count();
                                    int index = 1;


                                    Thread.Sleep(2000);

                                    foreach (var companyZip in companyZips)
                                    {
                                        string file_report = index == total_email ? item.file_report_attach : "";
                                        string email_index = total_email == 1 ? "" : $"{index}/{total_email}";
                                        await SendEmailZip.Send(session_id, email_index, companyZip, file_report, job.id).ConfigureAwait(false);
                                        index++;
                                    }
                                    count++;
                                }
                               
                                //progressBar_send_email.Value = count;
                                //lb_send.Text = count.ToString();
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        throw;
                    }


                    MessageBox.Show("ส่งสำเร็จ");
                }
                else if (type == "EA")
                {
                    try
                    {
                        ResponseTaximail.SessionStatus sessionStatus = await Session.GetSession();

                        if (sessionStatus.status == "success")
                        {
                            string session_id = sessionStatus.data.session_id;

                            List<Account> accounts = new List<Account>();
                            accounts = await _db_account.GetAccountsSendEmail(job.id);

                            int total = accounts.Count;

                            progressBar_send_email.Maximum = total;
                            progressBar_send_email.Value = 0;

                            lb_total.Text = total.ToString();

                            int count = 0;

                            foreach (var item in accounts)
                            {
                              await  SendEmailNormal.Send(item, session_id).ConfigureAwait(false);
                                count++;

                                Thread.Sleep(10);
                                //progressBar_send_email.Value = count;
                                //lb_send.Text = count.ToString();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        throw;
                    }
                    MessageBox.Show("ส่งสำเร็จ");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }

        private void btn_read_log_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog op = new OpenFileDialog())
                {
                    if (op.ShowDialog()== DialogResult.OK)
                    {
                        txb_file_name.Text = op.FileName;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<Dictionary<string, string[]>> Read(string pathFile)
        {

            DataSet DS_all = new DataSet();
            Dictionary<string, string[]> dic_log = new Dictionary<string, string[]>();

            using (var stream = File.Open(pathFile, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {


                    DS_all = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {


                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {


                            FilterRow = rowReader => rowReader.Depth > 1
                        }


                    });


                }
            }


            for (int i = 0; i < DS_all.Tables.Count; i++)
            {
                string Table_Name = DS_all.Tables[i].TableName;
                int start = 0;
                if (Table_Name == "All OPENED")
                {
                    start = 3;
                }

                for (int j = start; j < DS_all.Tables[i].Rows.Count; j++)
                {
                    string Message_ID = string.Empty;
                    string Reason = string.Empty;
                    string Status = string.Empty;
                    string key = string.Empty;
                    if (i == 2)
                    {
                        key = DS_all.Tables[i].Rows[j][3].ToString();
                    }
                    else if (i == 3)
                    {
                        key = DS_all.Tables[i].Rows[j][5].ToString();
                    }
                    else if (Table_Name == "All UNREAD") { key = DS_all.Tables[i].Rows[j][3].ToString(); }
                    else
                    {
                        key = DS_all.Tables[i].Rows[j][4].ToString();
                    }
                    if (!dic_log.Keys.Contains(key))
                    {
                        switch (Table_Name)
                        {
                            case "All OPENED":

                                dic_log.Add(DS_all.Tables[i].Rows[j][4].ToString(), new string[] { "Open", "", DS_all.Tables[i].Rows[j][2].ToString() });
                                break;
                            case "ALL CLICKED":
                                dic_log.Add(DS_all.Tables[i].Rows[j][4].ToString(), new string[] { "Success", "", DS_all.Tables[i].Rows[j][2].ToString() });
                                break;
                            case "ALL UNSUBSCRIPTIONS":
                                dic_log.Add(DS_all.Tables[i].Rows[j][3].ToString(), new string[] { "Reject", "UNSUBSCRIPTIONS", DS_all.Tables[i].Rows[j][2].ToString() });
                                break;
                            case "ALL BOUNCED":
                                dic_log.Add(DS_all.Tables[i].Rows[j][5].ToString(), new string[] { "Reject", DS_all.Tables[i].Rows[j][4].ToString(), DS_all.Tables[i].Rows[j][2].ToString() });
                                break;
                            case "All SPAM COMPLAINT":
                                dic_log.Add(DS_all.Tables[i].Rows[j][4].ToString(), new string[] { "Reject", "SPAM COMPLAINT", DS_all.Tables[i].Rows[j][3].ToString() });
                                break;
                            case "All UNREAD":
                                dic_log.Add(DS_all.Tables[i].Rows[j][3].ToString(), new string[] { "Success", "", DS_all.Tables[i].Rows[j][2].ToString() });
                                break;
                        }
                    }


                }
            }

            return dic_log;
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string path = txb_file_name.Text;
                Dictionary<string, string[]> data = await Read(path);

                if (data.Count>0)
                {

                    var task = Task.Run(() => { _db_account.UpdateAccounts(data).ConfigureAwait(false); });
                  
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async void btn_create_report_Click(object sender, EventArgs e)
        {
            List<Company> companies = new List<Company>();
            companies = await _db_company.GetCompanies(job.id);

            foreach (var item in companies)
            {

            }

        }
    }
}
