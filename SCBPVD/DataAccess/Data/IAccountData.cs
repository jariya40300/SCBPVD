using SCBPVD.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCBPVD.DataAccess.Data
{
    public interface IAccountData
    {
        Task<List<Account>> GetAccounts(int id, string company_code);
        Task UpdateAccounts(Account account);
        Task UpdateAccounts(string messageId, string Status, string reasonReject, string companyCode, int jobId);
        //Task UpdateAccounts(string messageId, string Status, string reasonReject);
        Task UpdateAccounts(Dictionary<string, string[]> data);
        Task<List<CompanyZip>> GetAccountsZip(int id, string company_code);
        Task<List<Account>> GetAccountsSendEmail(int id);

    }
}