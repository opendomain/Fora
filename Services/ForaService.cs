using Fora.Model;
using Microsoft.EntityFrameworkCore;

namespace Fora.Services
{
    public class ForaService : IForaService
    {
        private readonly EdgarCompanyDataContext _db;

        public ForaService(EdgarCompanyDataContext db)
        {
            _db = db;
        }

        public bool AddCompany(int Cik, string entityName)
        {
            return false;
        }

        public bool DeleteCompany(int Cik)
        {
            return false;
        }

        public async Task<List<EdgarCompanyData>> GetAllCompanies()
        {
            return await _db.EdgarCompanyDataList.ToListAsync();
        }

        public async Task<EdgarCompanyData?> GetCompany(int Cik)
        {
            // TODO: Use Id instead of Cik?
            return await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == Cik);
        }

        public bool UpdateCompany(int Cik, string entityName)
        {
            return false;
        }
    }
}
