using System;
using System.Collections.Generic;
using System.Text;

namespace SCBPVD.DataAccess.Models
{
   public class ResponseTaximail
    {
        public class SessionStatus
        {
            public string status { get; set; }
            public string code { get; set; }
            public Session data { get; set; }
            public string err_msg { get; set; }

        }
        public class Session
        {
            public string expire { get; set; }
            public string user_type { get; set; }
            public string session_id { get; set; }
        }
        public class SendEmail
        {
            public string status { get; set; }
            public string code { get; set; }
            public StatusSendEmail data { get; set; }
            public string err_msg { get; set; }

        }
        public class StatusSendEmail
        {
            public string message_id { get; set; }
            public string claimed { get; set; }
        }


        public class attmFile
        {
            public string filename { get; set; }
            public string file_data { get; set; }
        }

        public class EmailCC
        {
            public string name { get; set; }
            public string email { get; set; }
        }
    }
}
