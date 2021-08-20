using SCBPVD.DataAccess.Models;
using SCBPVD.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Threading;

namespace SCBPVD
{
    public partial class Form1 : Form
    {
        private IJobData _db_job = new JobData();
        private ICompanyData _db_company = new CompanyData();
        private IAccountData _db_account = new AccountData();

        DataTable dt_account = new DataTable();
        DataTable dt_account_text = new DataTable();
        DataTable dt_company = new DataTable();

        private List<string> file_text;

        private Dictionary<string, Company> company = new Dictionary<string, Company>();


        int total_task = 0;
        int total_company = 0;
        List<Account> account_error = new List<Account>();
        List<Company> companies_report = new List<Company>();

        List<Job> jobs = new List<Job>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_LoadAsync(object sender, EventArgs e)
        {
            dt_account = new DataTable();

            dt_account.Columns.Add("no", typeof(int));
            dt_account.Columns.Add("com_code", typeof(string));
            dt_account.Columns.Add("depart_code", typeof(string));
            dt_account.Columns.Add("member_code", typeof(string));
            dt_account.Columns.Add("member_id", typeof(string));
            dt_account.Columns.Add("type", typeof(int));
            dt_account.Columns.Add("delivery", typeof(string));
            dt_account.Columns.Add("filename_pdf", typeof(string));
            dt_account.Columns.Add("insert_account", typeof(string));
            dt_account.Columns.Add("Insert_company", typeof(string));
            dt_account.Columns.Add("file_insert", typeof(string));
            dt_account.Columns.Add("file_insert_company", typeof(string));
            dt_account.Columns.Add("email_to", typeof(string));
            dt_account.Columns.Add("email_bcc", typeof(string));
            dt_account.Columns.Add("file_name_new", typeof(string));
            dt_account.Columns.Add("password", typeof(string));
            dt_account.Columns.Add("message_id", typeof(string));
            dt_account.Columns.Add("is_send", typeof(int));
            dt_account.Columns.Add("status", typeof(string));
            dt_account.Columns.Add("reason_reject", typeof(string));


            dt_account_text = new DataTable();

            dt_account_text.Columns.Add("qr_tracking", typeof(string));
            dt_account_text.Columns.Add("companycode", typeof(string));
            dt_account_text.Columns.Add("pdf_running", typeof(int));
            dt_account_text.Columns.Add("company_name", typeof(string));
            dt_account_text.Columns.Add("contract_name", typeof(string));
            dt_account_text.Columns.Add("address1", typeof(string));
            dt_account_text.Columns.Add("address2", typeof(string));
            dt_account_text.Columns.Add("address3", typeof(string));
            dt_account_text.Columns.Add("province", typeof(string));
            dt_account_text.Columns.Add("zipcode", typeof(string));
            dt_account_text.Columns.Add("tel", typeof(string));
            dt_account_text.Columns.Add("dept_code", typeof(string));
            dt_account_text.Columns.Add("dept_name", typeof(string));
            dt_account_text.Columns.Add("member_code", typeof(string));
            dt_account_text.Columns.Add("member_name", typeof(string));
            dt_account_text.Columns.Add("filename_pdf", typeof(string));
            dt_account_text.Columns.Add("seq_lot", typeof(string));
            dt_account_text.Columns.Add("filename_txt", typeof(string));


            dt_company = new DataTable();

            dt_company.Columns.Add("company_code", typeof(string));
            dt_company.Columns.Add("total_account", typeof(int));
            dt_company.Columns.Add("total_account_text", typeof(int));
            dt_company.Columns.Add("status", typeof(string));
            dt_company.Columns.Add("error_message", typeof(string));
            dt_company.Columns.Add("company_name", typeof(string));


            dataGridView1.AutoGenerateColumns = false;


            _ = GetJobs();




            timer1.Start();

        }

        private async Task GetJobs()
        {
            jobs = await _db_job.GetJobs();
            dataGridView1.DataSource = jobs;

        }

        private void btn_selct_file_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "csv files(*.csv)|*.csv|txt files(*.txt)|*.txt";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    txb_file_name.Text = op.FileName;
                }

            }
        }

        private void btn_selct_folder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog op = new FolderBrowserDialog())
            {
                op.Description = "เลือกโฟลเดอร์ไฟล์ข้อมูล";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    txb_folder_name.Text = op.SelectedPath;
                }
            }
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            dt_account.Rows.Clear();
            dt_account_text.Rows.Clear();
            dt_company.Rows.Clear();

            company = new Dictionary<string, Company>();

            string line_error = string.Empty;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding(874);

            try
            {
                string file_email = txb_file_name.Text;
                string folder_data = txb_folder_name.Text;

                file_text = new List<string>();
                await DirSearch(folder_data);

                if (file_text.Count() > 0)
                {
                    string[] allLine = await File.ReadAllLinesAsync(file_email, encoding);

                    foreach (var item in allLine)
                    {
                        string[] line = item.Split(',');

                        if (line.Length >= 16)
                        {
                            if (!company.ContainsKey(line[1].Trim()))
                            {
                                company.Add(line[1], new Company
                                {
                                    company_code = line[1],
                                    total_account = 1,
                                    total_account_text = 0
                                });

                            }
                            else
                            {

                                company[line[1]] = new Company
                                {
                                    company_code = company[line[1]].company_code,
                                    total_account = company[line[1]].total_account += 1,
                                    total_account_text = company[line[1]].total_account_text
                                };
                            }
                            DataRow dr = dt_account.NewRow();
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (i == 0 || i == 5)
                                {
                                    dr[i] = Convert.ToInt32(line[i].Trim());
                                }
                                else
                                {
                                    dr[i] = line[i].Trim();
                                }
                            }
                            dr[16] = $"Msg_Id<{Guid.NewGuid().ToString()}-{DateTime.Now.ToString("yyyyMMdd_HHmmss")}>";
                            dr[17] = 0;
                            dr[18] = "Wait";
                            dr[19] = "";
                            dt_account.Rows.Add(dr);
                        }
                    }

                    foreach (var item in file_text)
                    {
                        string[] allLine_text = await File.ReadAllLinesAsync(item, Encoding.UTF8);

                        foreach (var l in allLine_text)
                        {
                            string[] line = l.Split('|');

                            if (line.Length == 17 && !l.Contains("QR_tracking|CompanyCode|PDF_running|"))
                            {
                                if (!company.ContainsKey(line[1].Trim()))
                                {
                                    company.Add(line[1], new Company
                                    {
                                        company_code = line[1],
                                        total_account = 0,
                                        total_account_text = 1,
                                        company_name = line[3],
                                        file_report_attach = Path.GetDirectoryName(item),

                                    });
                                }
                                else
                                {
                                    company[line[1]] = new Company
                                    {
                                        company_code = company[line[1]].company_code,
                                        total_account = company[line[1]].total_account,
                                        company_name = line[3],
                                        total_account_text = company[line[1]].total_account_text += 1
                                    };
                                }
                                DataRow dr = dt_account_text.NewRow();
                                for (int i = 0; i < line.Length; i++)
                                {
                                    if (i == 2)
                                    {
                                        dr[i] = Convert.ToInt32(line[i].Trim());
                                    }
                                    else
                                    {
                                        dr[i] = line[i].Trim();
                                    }
                                }
                                dr[17] = item;

                                dt_account_text.Rows.Add(dr);
                            }
                        }
                    }

                    Job job = new Job
                    {
                        code_cycle = Guid.NewGuid().ToString(),
                        create_by = "jariya",
                        cycle = DateTime.Now,
                        create_date = DateTime.Now,
                        total_company = company.Count(),
                    };

                    foreach (var item in company)
                    {
                        var value = item.Value;
                        string error = value.total_account == value.total_account_text ? "" : "จำนวนงานไฟล์ csv และไฟล์ text ไม่ตรงกัน";
                        dt_company.Rows.Add(value.company_code, value.total_account, value.total_account_text, "Wait", error, value.company_name);
                    }


                    int job_id = await _db_job.InserAccount(job, dt_account, dt_account_text, dt_company);

                    //int job_id = 69;

                    await DeliveryType(job_id);

                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล Text ไฟล์กรุณาตรวจสอบ");
                }


            }
            catch (Exception ex)
            {

                throw;
            }

        }


        int check = 0;
        private async Task DeliveryType(int id)
        {
            try
            {
                List<Company> companies = await _db_company.GetCompanies(id);
                List<Account> accounts = new List<Account>();
                account_error = new List<Account>();
                companies_report = new List<Company>();

               
                foreach (var item in companies)
                {

                    accounts = await _db_account.GetAccounts(id, item.company_code);

                    //int csv = accounts.Count(a => a.id > 0);

                    if (accounts.Count() > 0)
                    {

                        var count_type = accounts.GroupBy(a => a.type).Count();

                        if (count_type < 3)
                        {
                            int type = accounts.First(a => a.type != 0).type;
                            if (type == 2)
                            {
                                total_task++;
                                total_company++;


                                var task = Task.Run(async () =>
                                {


                                    List<Account> accounts_send = new List<Account>();
                                    List<Account> accounts_reject = new List<Account>();
                                    List<Account> accounts_task = accounts;

                                    int reject = 0;
                                    int success = 0;

                                    try
                                    {
                                        string path = Path.GetDirectoryName(accounts_task[0].filename_txt);

                                        foreach (var acc in accounts_task)
                                        {
                                            if (acc.filename_txt != null)
                                            {
                                                if (acc.id != 0)
                                                {
                                                    if (acc.email_to == "")
                                                    {
                                                        acc.status = "reject";
                                                        acc.reason_reject = "does not have the email";
                                                        reject++;
                                                        account_error.Add(acc);
                                                        accounts_reject.Add(acc);
                                                    }
                                                    else if (acc.password == "")
                                                    {
                                                        acc.status = "reject";
                                                        acc.reason_reject = "does not have the password";
                                                        reject++;
                                                        account_error.Add(acc);
                                                        accounts_reject.Add(acc);
                                                    }
                                                    else
                                                    {
                                                        Tuple<bool, string> file_new = await EncryptPDF.Encrypt(acc).ConfigureAwait(false);
                                                        if (file_new.Item1)
                                                        {
                                                            acc.file_name_new = file_new.Item2;
                                                            accounts_send.Add(acc);
                                                            success++;
                                                        }
                                                        else
                                                        {
                                                            acc.status = "reject";
                                                            acc.reason_reject = file_new.Item2;
                                                            reject++;
                                                            account_error.Add(acc);
                                                            accounts_reject.Add(acc);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    acc.status = "reject";
                                                    acc.reason_reject = "does not have data in csv file";
                                                    reject++;
                                                    account_error.Add(acc);
                                                    accounts_reject.Add(acc);
                                                }

                                            }
                                            else
                                            {
                                                acc.status = "reject";
                                                acc.reason_reject = "does not have data in text file";
                                                reject++;
                                                account_error.Add(acc);
                                                accounts_reject.Add(acc);
                                            }



                                            acc.send_date = DateTime.Now;
                                            await _db_account.UpdateAccounts(acc);
                                        }
                                        string report_name = Path.GetFileName(accounts_task[0].filename_txt);
                                        report_name = report_name == null ? "" : report_name.Replace(".txt", ".xls");

                                        //Company company = item;
                                        item.total_reject = reject;
                                        item.send_by = "EA";
                                        item.file_report_attach = report_name;
                                        item.total_success = success;
                                        companies_report.Add(item);

                                        Thread.Sleep(2000);

                                        if (report_name != "")
                                        {
                                            ReportAttach.Create(accounts_send, item, report_name);
                                        }

                                        Thread.Sleep(2000);
                                        await _db_company.UpdateCompanies(item).ConfigureAwait(false);
                                        await Reprint.Create(accounts_reject, path).ConfigureAwait(false);
                                    }
                                    catch (Exception ex)
                                    {

                                        throw;
                                    }



                                }).ContinueWith(t => total_task--);



                            }
                            //Send Company
                            else
                            {
                                total_task++;
                                total_company++;

                                //if (item.company_code== "00147")
                                //{

                                //}
                                var task = Task.Run(async () =>
                                {
                                    int reject = 0;
                                    List<Account> accounts_task = accounts;
                                    string path = Path.GetDirectoryName(accounts_task[0].filename_txt);
                                    Dictionary<string, Account> account_zip = new Dictionary<string, Account>();
                                    List<Account> accounts_reject = new List<Account>();

                                    foreach (var acc in accounts_task)
                                    {
                                        if (acc.filename_txt != null)
                                        {
                                            if (acc.id != 0)
                                            {
                                                if (acc.email_to == "")
                                                {
                                                    acc.status = "reject";
                                                    acc.reason_reject = "does not have the email";
                                                    acc.send_date = DateTime.Now;
                                                    reject++;
                                                    account_error.Add(acc);
                                                    accounts_reject.Add(acc);
                                                    await _db_account.UpdateAccounts(acc);
                                                }
                                                else if (acc.password == "")
                                                {
                                                    acc.status = "reject";
                                                    acc.reason_reject = "does not have the password";
                                                    acc.send_date = DateTime.Now;
                                                    reject++;
                                                    account_error.Add(acc);
                                                    accounts_reject.Add(acc);
                                                    await _db_account.UpdateAccounts(acc);
                                                }
                                                else
                                                {
                                                    Tuple<bool, string> file_new = await EncryptPDF.Encrypt(acc);

                                                    if (file_new.Item1)
                                                    {
                                                        acc.file_name_new = file_new.Item2;
                                                        account_zip.Add(file_new.Item2, acc);
                                                    }
                                                    else
                                                    {
                                                        acc.status = "reject";
                                                        acc.reason_reject = file_new.Item2;
                                                        acc.send_date = DateTime.Now;
                                                        reject++;
                                                        account_error.Add(acc);
                                                        accounts_reject.Add(acc);
                                                        await _db_account.UpdateAccounts(acc);
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                acc.reason_reject = "does not have data in csv file";
                                                acc.status = "reject";
                                                acc.send_date = DateTime.Now;
                                                reject++;
                                                account_error.Add(acc);
                                                accounts_reject.Add(acc);
                                                await _db_account.UpdateAccounts(acc);
                                            }



                                        }
                                        else
                                        {
                                            acc.reason_reject = "does not have data in text file";
                                            acc.status = "reject";
                                            acc.send_date = DateTime.Now;
                                            reject++;
                                            account_error.Add(acc);
                                            accounts_reject.Add(acc);
                                            await _db_account.UpdateAccounts(acc);
                                        }


                                    }

                                    string report_name = Path.GetFileName(accounts_task[0].filename_txt);
                                    report_name = report_name == null ? "" : report_name.Replace(".txt", ".xls");

                                    //Company company = new Company();



                                    int total_file = account_zip.Count();

                                    double start_size = 0;
                                    int start_position = 0;
                                    int end_position = 0;
                                    int count_file = 1;

                                    List<Account> accounts_send = new List<Account>();

                                    //รายการที่นำมา zip
                                    for (int i = 0; i < account_zip.Count(); i++)
                                    {

                                        FileInfo fileInfo = new FileInfo(path + "\\" + account_zip.ElementAt(i).Key);

                                        double size_file = fileInfo.Length;

                                        size_file = size_file * 0.000001;
                                        start_size += size_file;

                                        // < 5 mb
                                        if (start_size > 5 || i == (total_file - 1))
                                        {

                                            int end = i == (total_file - 1) ? i + 1 : i;

                                            end_position = i;

                                            string zip_name = path + $"\\{accounts_task[0].com_code}_{count_file}.zip";

                                            ZipOutputStream ZipOutStream = new ZipOutputStream(File.Create(zip_name));
                                            ZipOutStream.SetLevel(9);



                                            string message_id = $"Msg_Id<{Guid.NewGuid().ToString()}-{DateTime.Now.ToString("yyyyMMdd_HHmmss")}>";

                                            for (int j = start_position; j < end; j++)
                                            {
                                                Account account = account_zip.ElementAt(j).Value;
                                                if (end_position < account_zip.Count())
                                                {
                                                    string GetFilePath = path + "\\" + account_zip.ElementAt(j).Key;
                                                    string[] FilePath = GetFilePath.Split('\\');

                                                    int getLength = FilePath[FilePath.Length - 1].Length;

                                                    string FolderPath = GetFilePath.Substring(0, GetFilePath.Length - getLength);


                                                    int folderOffset = FolderPath.Length + (FolderPath.EndsWith("\\") ? 0 : 1);
                                                    await AddFileToZip(ZipOutStream, GetFilePath, folderOffset);


                                                }
                                                account.zip_file = zip_name;
                                                account.message_id = message_id;
                                                account.send_date = DateTime.Now;
                                                await _db_account.UpdateAccounts(account);
                                                accounts_send.Add(account);
                                            }

                                            ZipOutStream.Finish();
                                            ZipOutStream.Close();


                                            if (total_file - i != 1)
                                            {
                                                i--;
                                                count_file++;
                                                end_position = 0;
                                                start_size = 0;
                                            }


                                        }
                                        else
                                        {
                                            if (start_size == size_file)
                                            {
                                                start_position = i;
                                            }
                                        }


                                    }

                                    item.total_success = accounts_send.Count();
                                    item.total_reject = reject;
                                    item.send_by = count_file > 5 ? "DC" : "EC";
                                    item.file_report_attach = report_name;
                                    check++;
                                    companies_report.Add(item);


                                    if (report_name != "")
                                    {
                                        ReportAttach.Create(accounts_send, item, report_name);
                                    }
                                    Thread.Sleep(2000);

                                    await _db_company.UpdateCompanies(item).ConfigureAwait(false);
                                    await Reprint.Create(accounts_reject, path).ConfigureAwait(false);
                                    //total_task--;
                                }).ContinueWith(t => total_task--);

                            }
                        }
                        //มีเงื่อนไขการส่งมากกว่า 1 ประเภท
                        else
                        {
                            Company company = item;
                            company.status = "reject";
                            company.error_message = "พบเงื่อนไขการส่งมากกว่าหนึ่งแบบ";
                            company.send_by = "EC";
                            companies_report.Add(company);
                        }
                    }
                    //not found this company
                    else
                    {
                        Company company = item;
                        company.status = "reject";
                        company.error_message = "ไม่ CSV";
                        company.send_by = "EC";
                        //companies_report.Add(company);
                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }



        }

        private async Task SendEmail()
        {
            //int total_email = zip_filename.Count();
            //int running = 1;

            //foreach (var zip in zip_filename)
            //{
            //    string index = total_email == 1 ? "" : $"{running}/{total_email}";
            //    SendEmailZip.Send(zip.Key, zip.Value, session_id, index);
            //    running++;
            //}

        }

        private Task AddFileToZip(ZipOutputStream ZipOutStream, string file_name, int folderOffset)
        {
            try
            {
                string filename = file_name;

                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity


                newEntry.Size = fi.Length;

                ZipOutStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, ZipOutStream, buffer);
                }
                ZipOutStream.CloseEntry();
            }
            catch (Exception ex)
            {

                throw;
            }

            return Task.CompletedTask;
        }

        private async Task DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d, "*.txt"))
                    {
                        //Debug.WriteLine(f);
                        file_text.Add(f);
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (total_task == 0 && companies_report.Count > 1)
            {
                int i = check;
                if (total_company == companies_report.Count)
                {
                    total_company = 0;
                    ReportSuammary.Create(companies_report, account_error, Path.GetDirectoryName(txb_file_name.Text)).ConfigureAwait(false);
                    MessageBox.Show("การบันทึกเสร็จสิ้น");

                }

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "JobId")
            {

                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["JobId"].Value.ToString());

                Job job = jobs.Single(j => j.id == id);

                SendEmail send = new SendEmail();

                send.job = job;

                send.Show();


            }
        }
    }
}
