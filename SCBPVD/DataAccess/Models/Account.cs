using System;
using System.Collections.Generic;
using System.Text;

namespace SCBPVD.DataAccess.Models
{
    public class Account
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public int no { get; set; }
        public string com_code { get; set; }
        public string depart_code { get; set; }
        public string member_code { get; set; }
        public string member_id { get; set; }
        public int type { get; set; }
        public string delivery { get; set; }
        public string filename_pdf { get; set; }
        public string insert_account { get; set; }
        public string Insert_company { get; set; }
        public string file_insert { get; set; }
        public string file_insert_company { get; set; }
        public string email_to { get; set; }
        public string email_bcc { get; set; }
        public string file_name_new { get; set; }
        public string password { get; set; }
        public string message_id { get; set; }
        public int is_send { get; set; }
        public string status { get; set; }
        public string reason_reject { get; set; }
        public string filename_txt { get; set; }
        public DateTime send_date { get; set; }
        public string zip_file { get; set; }
        public string member_name { get; set; }
        public string dept_name { get; set; }
        public string qr_tracking { get; set; }
        public string company_name { get; set; }
        public string contract_name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string province { get; set; }
        public string zipcode { get; set; }
        public string tel { get; set; }
        public string seq_lot { get; set; }
    }
}
