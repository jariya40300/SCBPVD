using System;
using System.Collections.Generic;
using System.Text;

namespace SCBPVD.DataAccess.Models
{
    public class Company
    {
        public int id { get; set; }
        public int job_id { get; set; }
        public string company_code { get; set; }
        public int total_account { get; set; }
        public int total_account_text { get; set; }
        public string status { get; set; }
        public string error_message { get; set; }
        public string file_report_attach { get; set; }
        public string send_by { get; set; }
        public int total_reject { get; set; }
        public int total_success { get; set; }
        public string company_name { get; set; }
   
    }


}
