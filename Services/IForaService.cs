using Fora.Model;

namespace Fora.Services
{
    public interface IForaService
    {
        Task<List<EdgarCompanyData>> GetAllCompanies();

        Task<EdgarCompanyData?> GetCompany(int Cik);

        bool AddCompany(int Cik, string entityName);

        bool UpdateCompany(int Cik, string entityName);
        
        bool DeleteCompany(int Cik);
    }
}
