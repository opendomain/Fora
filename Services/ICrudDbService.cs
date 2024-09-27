using Fora.Model;

namespace Fora.Services
{
    public interface ICrudDbService
    {
        Task<List<EdgarCompanyData>> GetAllCompanyData();

        Task<EdgarCompanyData?> GetCompanyData(int cik);

        Task<bool> AddCompanyData(int cik, string entityName);

        Task<bool> UpdateCompanyData(int cik, string entityName);
        
        Task<bool> DeleteCompanyData(int cik);
    }
}
