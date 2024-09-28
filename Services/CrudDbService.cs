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

        public async Task<bool> AddCompanyData(long cik, string? entityName)
        {
            EdgarCompanyData edgarCompanyData = new EdgarCompanyData(cik, entityName);
            
            await _db.EdgarCompanyDataList.AddAsync(edgarCompanyData);

            var result = await _db.SaveChangesAsync();

            // TODO: Check result
            return result >= 0;
        }

        public async Task<bool> DeleteCompanyData(long cik)
        {
            EdgarCompanyData? edgarCompanyData = await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);
            if (edgarCompanyData != null) {
                return false;
            }
            _db.EdgarCompanyDataList.Remove(edgarCompanyData);
            var result = await _db.SaveChangesAsync();

            // TODO: Check result
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

        public async Task<EdgarCompanyData?> GetCompanyData(long cik)
        {
            // TODO: Use Id instead of Cik?
            return await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);
        }

        public async Task<bool> UpdateCompanyData(long cik, string? entityName)
        {
            EdgarCompanyData? edgarCompanyData = await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);
            if (edgarCompanyData == null) {
                return false;
            }

            edgarCompanyData.EntityName = entityName;
            edgarCompanyData.Updated = DateTime.UtcNow;
            var result = await _db.SaveChangesAsync();

            // TODO: check result
            return result >= 0;
        }

        public async Task<bool> AddUnits(long cik, List<Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd> infoFactUsGaapIncomeLossUnitsUsdList)
        {
            if (infoFactUsGaapIncomeLossUnitsUsdList != null && infoFactUsGaapIncomeLossUnitsUsdList.Count > 0) {
                EdgarCompanyData? edgarCompanyData = await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);
                if (edgarCompanyData == null)
                {
                    return false;
                }

                // TODO: Is clear needed?
                edgarCompanyData.Usd.Clear();
                foreach (Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd in infoFactUsGaapIncomeLossUnitsUsdList)
                {
                    edgarCompanyData.Usd.Add(infoFactUsGaapIncomeLossUnitsUsd);
                }
                var result = await _db.SaveChangesAsync();

                return result >= 0;
            }
            return false;
        }
    }
}
