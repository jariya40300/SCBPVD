using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCBPVD.DataAccess.Data
{
    public class JobData : IJobData
    {
        private ISqlDataAccess _db = new SqlDataAccess();

        public Task<int> InserAccount(Job job, DataTable dt_account, DataTable dt_account_text,DataTable dt_company)
        {
            string sql = "SP_Account_Ins";
            return _db.SaveDataScalar<int, dynamic>(sql, new
            {
                create_date = job.create_date,
                create_by = job.create_by,
                total_company = job.total_company,
                cycle = job.cycle,
                code_cycle = job.code_cycle,
                TblAccount = dt_account,
                TblAccountText = dt_account_text,
                TblCompany = dt_company
            });
        }

        public Task<List<Job>> GetJobs()
        {
            string sql = "SP_Job_Sel";
            return _db.LoadData<Job,dynamic>(sql, new { });
        }
    }
}
