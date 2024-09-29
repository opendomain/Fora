using Fora.Model;

namespace Fora.Services
{
    public interface ICrudDbService
    {
        Task<List<EdgarCompanyData>> GetAllCompanyData(bool onlyGetUpdatedFlag, bool onlyGetValidNames);

        Task<EdgarCompanyData?> GetCompanyData(long cik);

        Task<bool> AddCompanyData(long cik, string? entityName);

        Task<bool> UpdateCompanyData(long cik, string? entityName);
        
        Task<bool> DeleteCompanyData(long cik);
    }
}
