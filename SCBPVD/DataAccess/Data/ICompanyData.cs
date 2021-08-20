using SCBPVD.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCBPVD.DataAccess.Data
{
    public interface ICompanyData
    {
        Task<List<Company>> GetCompanies(int id);
        Task<List<Company>> GetCompaniesSendEmail(int id);
        Task UpdateCompanies(Company company);
    }
}