using System;
using System.Collections.Generic;
using System.Text;

namespace SCBPVD.DataAccess.Models
{
    public class AccountText
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public string qr_tracking { get; set; }
        public string companycode { get; set; }
        public string pdf_running { get; set; }
        public string company_name { get; set; }
        public string contract_name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string province { get; set; }
        public string zipcode { get; set; }
        public string tel { get; set; }
        public string dept_code { get; set; }
        public string dept_name { get; set; }
        public string member_code { get; set; }
        public string member_name { get; set; }
        public string filename_pdf { get; set; }
        public string seq_lot { get; set; }
        public string filename_txt { get; set; }
    }
}
