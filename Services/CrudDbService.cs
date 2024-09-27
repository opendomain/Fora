using Fora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        // TODO: make filters ternary
        public async Task<List<EdgarCompanyData>> GetAllCompanyData(bool onlyGetUpdatedFlag, bool onlyGetValidNames)
        {
            List<EdgarCompanyData>? allEdgarCompanyData = await _db.EdgarCompanyDataList.ToListAsync();

            if (allEdgarCompanyData != null && allEdgarCompanyData.Count > 0)
            {
                if (onlyGetUpdatedFlag)
                {
                    allEdgarCompanyData = allEdgarCompanyData.FindAll(ec => ec.Updated != null);
                }

                if (onlyGetValidNames)
                {
                    allEdgarCompanyData = allEdgarCompanyData.FindAll(ec => !string.IsNullOrEmpty(ec.EntityName) );
                }

            }
            return allEdgarCompanyData;
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
            edgarCompanyData.Updated = DateTime.UtcNow;
            var result = await _db.SaveChangesAsync();
            return result >= 0;
        }
    }
}
