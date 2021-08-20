using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCBPVD.DataAccess.Data
{
    public class AccountData : IAccountData
    {
        private ISqlDataAccess _db = new SqlDataAccess();

        public Task<List<Account>> GetAccounts(int id, string company_code)
        {
            string sql = "SP_Account_Sel";
            return _db.LoadData<Account, dynamic>(sql, new
            {
                job_id = id,
                com_code = company_code
            });
        }

        public Task<List<Account>> GetAccountsSendEmail(int id)
        {
            string sql = "SP_Account_SendEmail_Sel";
            return _db.LoadData<Account, dynamic>(sql, new
            {
                job_id = id,
  
            });
        }


        public Task<List<CompanyZip>> GetAccountsZip(int id, string company_code)
        {
            string sql = "SP_Account_SendZip_Sel";
            return _db.LoadData<CompanyZip, dynamic>(sql, new
            {
                job_id = id,
                com_code = company_code
            });
        }


        public Task UpdateAccounts(Account account)
        {
            string sql = "SP_Account_Upd";
            return _db.SaveData<dynamic>(sql,account);
        }
        public Task UpdateAccounts(string messageId,string Status,string reasonReject,string companyCode ,int jobId)
        {
            string sql = "SP_Account_SendEmail_Upd";
            return _db.SaveData<dynamic>(sql, new {

                company_id  = jobId,
                com_code = companyCode,
                message_id = messageId,
                status= Status,
                reason_reject = reasonReject

            });
        }

        public Task UpdateAccounts(Dictionary<string, string[]> data)
        {
            string sql = "SP_Account_SendEmail_ByMessageId_Upd";
            return _db.SaveData(sql,data);
        }

    }
}
