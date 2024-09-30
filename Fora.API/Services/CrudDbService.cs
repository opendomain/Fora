using Fora.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fora.Services
{
    /// <summary>
    /// Service to connect to Database (SQL Server or LocalDB)
    /// </summary>
    public class CrudDbService : ICrudDbService
    {
        private readonly EdgarCompanyDataContext _db;
        private readonly ILogger<CrudDbService> _logger;

        public CrudDbService(EdgarCompanyDataContext db, ILogger<CrudDbService> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Add Company to Db.  
        /// Not used, as expected company IDs are loaded at Design time of DB
        /// </summary>
        /// <param name="cik">Comany ID</param>
        /// <param name="entityName">optional.  If empty, is it assume that the data must be loaded</param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete Company from database.  
        /// Not Used
        /// </summary>
        /// <param name="cik">company id</param>
        /// <returns>Bool if successful.  Not checked</returns>
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

        /// <summary>
        /// Get ALL company data from database
        /// </summary>
        /// <param name="onlyGetUpdatedFlag">Get company if the updated flag is set, meaning the full data has been loaded</param>
        /// <param name="onlyGetValidNames">Get company if the name is not null. Also used to load company data</param>
        /// <returns></returns>
        public async Task<List<EdgarCompanyData>> GetAllCompanyData(bool onlyGetUpdatedFlag, bool onlyGetValidNames)
        {
            List<EdgarCompanyData>? allEdgarCompanyData = null;

            try
            {
                allEdgarCompanyData = await _db.EdgarCompanyDataList.ToListAsync();

                // TODO: make filters ternary
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

        /// <summary>
        /// Get one company info from database
        /// </summary>
        /// <param name="cik">Compnay id</param>
        /// <returns>EdgarCompany</returns>
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

        /// <summary>
        /// Update the database - usually just the Entity name
        /// NOTE:  This will also save any other changes to the model, including child records 
        /// </summary>
        /// <param name="cik">Company id</param>
        /// <param name="entityName">Optional.  If it is NULL, it will try to be loaded later</param>
        /// <returns>BOOL on success - should be checked</returns>
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
