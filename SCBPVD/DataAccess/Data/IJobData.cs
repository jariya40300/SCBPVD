using SCBPVD.DataAccess.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SCBPVD.DataAccess.Data
{
    public interface IJobData
    {
        Task<int> InserAccount(Job job, DataTable dt_account, DataTable dt_account_text, DataTable dt_company);
        Task<List<Job>> GetJobs();
    }
}