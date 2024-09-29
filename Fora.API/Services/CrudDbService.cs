using Fora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fora.Services
{
    public class CrudDbService : ICrudDbService
    {
        private readonly EdgarCompanyDataContext _db;
        private readonly ILogger<CrudDbService> _logger;

        public CrudDbService(EdgarCompanyDataContext db, ILogger<CrudDbService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<bool> AddCompanyData(long cik, string? entityName)
        {
            int result = -1;
            EdgarCompanyData edgarCompanyData = null;

            try
            {
                edgarCompanyData = new EdgarCompanyData(cik, entityName);
                await _db.EdgarCompanyDataList.AddAsync(edgarCompanyData);
                result = await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR CrudDbService AddCompanyData:" + ex.Message);
                result = -1;
            }

            // TODO: Check result
            return (result >= 0);
        }

        public async Task<bool> DeleteCompanyData(long cik)
        {
            int result = -1;
            EdgarCompanyData? edgarCompanyData = null;

            try
            {
                edgarCompanyData = await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);
                if (edgarCompanyData != null)
                {
                    _db.EdgarCompanyDataList.Remove(edgarCompanyData);
                    result = await _db.SaveChangesAsync();                
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR CrudDbService DeleteCompanyData:" + ex.Message);
                result = -1;
            }

            // TODO: Check result
            return (result >= 0);

        }

        // TODO: make filters ternary
        public async Task<List<EdgarCompanyData>> GetAllCompanyData(bool onlyGetUpdatedFlag, bool onlyGetValidNames)
        {
            List<EdgarCompanyData>? allEdgarCompanyData = null;

            try
            {
                allEdgarCompanyData = await _db.EdgarCompanyDataList.ToListAsync();

                if (allEdgarCompanyData != null)
                {
                    if (onlyGetUpdatedFlag)
                    {
                        allEdgarCompanyData = allEdgarCompanyData.FindAll(ec => ec.Updated != null);
                    }

                    if (onlyGetValidNames)
                    {
                        allEdgarCompanyData = allEdgarCompanyData.FindAll(ec => !string.IsNullOrEmpty(ec.EntityName));
                    }
                }
            }
            catch (Exception ex )
            {
                _logger.LogError("ERROR CrudDbService GetAllCompanyData:" + ex.Message);
            }

            return allEdgarCompanyData;
        }

        public async Task<EdgarCompanyData?> GetCompanyData(long cik)
        {
            // TODO: Use Id instead of Cik?

            EdgarCompanyData? edgarCompanyData = null;
            try
            {
                edgarCompanyData = await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR CrudDbService GetCompanyData:" + ex.Message);
            }

            return edgarCompanyData;
        }

        // NOTE:  This will also save any other changes to the model, including child records
        public async Task<bool> UpdateCompanyData(long cik, string? entityName)
        {
            int result = -1;

            EdgarCompanyData? edgarCompanyData = null;

            try
            {
                edgarCompanyData = await _db.EdgarCompanyDataList.FirstOrDefaultAsync(c => c.Cik == cik);

                if (edgarCompanyData != null)
                {
                    edgarCompanyData.EntityName = entityName;
                    edgarCompanyData.Updated = DateTime.UtcNow;
                    result = await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR CrudDbService UpdateCompanyData:" + ex.Message);
            }

            // TODO: check result
            return (result >= 0);
        }
    }
}
