using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCBPVD.DataAccess.Data
{
    public class CompanyData : ICompanyData
    {
        private ISqlDataAccess _db = new SqlDataAccess();

        public Task<List<Company>> GetCompanies(int id)
        {
            string sql = "SP_Company_Sel";
            return _db.LoadData<Company, dynamic>(sql, new
            {
                job_id = id
            });
        }

        public Task<List<Company>> GetCompaniesSendEmail(int id)
        {
            string sql = "SP_Company_SendEmail_Sel";
            return _db.LoadData<Company, dynamic>(sql, new
            {
                job_id = id
            });
        }

        public Task UpdateCompanies(Company company)
        {
            string sql = "SP_Company_Upd";
            return _db.SaveData<dynamic>(sql, company);
        }
    }
}
