using Fora.Model;
using Microsoft.EntityFrameworkCore;

namespace Fora.Services
{
    public class CrudDbService : ICrudDbService
    {
        private readonly EdgarCompanyDataContext _db;

        public CrudDbService(EdgarCompanyDataContext db)
        {
            _db = db;
        }

        public async Task<bool> AddCompanyData(int cik, string entityName)
        {
            EdgarCompanyData edgarCompanyData = new EdgarCompanyData(cik, entityName);
            
            await _db.EdgarCompanyDataList.AddAsync(edgarCompanyData);

            var result = await _db.SaveChangesAsync();
            return result >= 0;
        }

        public async Task<bool> DeleteCompanyData(int cik)
        {
            EdgarCompanyData? edgarCompanyData = await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);
            if (edgarCompanyData != null) {
                return false;
            }
            _db.EdgarCompanyDataList.Remove(edgarCompanyData);
            var result = await _db.SaveChangesAsync();
            return result >= 0;
        }

        public async Task<List<EdgarCompanyData>> GetAllCompanyData()
        {
            return await _db.EdgarCompanyDataList.ToListAsync();
        }

        public async Task<EdgarCompanyData?> GetCompanyData(int cik)
        {
            // TODO: Use Id instead of Cik?
            return await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);
        }

        public async Task<bool> UpdateCompanyData(int cik, string entityName)
        {
            EdgarCompanyData? edgarCompanyData = await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);
            if (edgarCompanyData == null) {
                return false;
            }

            edgarCompanyData.EntityName = entityName;
            var result = await _db.SaveChangesAsync();
            return result >= 0;
        }
    }
}
