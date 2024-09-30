using AutoMapper;
using Fora.Model;
using Fora.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Fora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Company : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICrudDbService _crudDbService;
        private readonly ILogger<Company> _logger;

        public Company(IMapper mapper, ICrudDbService crudDbService, ILogger<Company> logger)
        {
            _mapper = mapper;
            _crudDbService = crudDbService;
            _logger = logger;
        }

        /// <summary>
        /// Get ALL Company Output from Database
        /// </summary>
        /// <param name="startChar">optional filter for company name that starts with a (single) letter</param>
        /// <returns>List of CompanyOutput</returns>
        [HttpGet]
        public async Task<IActionResult> Get(char? startChar)
        {
            List<EdgarCompanyData>? allEdgarCompanyData = null;
            List<CompanyOutput> allCompanies = null;

            try
            {
                allEdgarCompanyData = await _crudDbService.GetAllCompanyData(onlyGetUpdatedFlag: true, onlyGetValidNames: true);

                if (allEdgarCompanyData != null) {
                    if (startChar != null)
                    {
                        // TODO: allow more than one letter?
                        string strLetter = (startChar.HasValue) ? startChar.ToString() : "";

                        allEdgarCompanyData = allEdgarCompanyData.Where(ec => ec.EntityName.StartsWith(strLetter, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    }

                    allCompanies = _mapper.Map<List<CompanyOutput>>(allEdgarCompanyData);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR: Company GET " +  ex.Message);
            }

            if (allCompanies == null) return NotFound();

            return Ok(allCompanies);
        }

        // 
        /// <summary>
        /// Get CompanyOutput from Database using Company CIK 
        /// </summary>
        /// <param name="Cik">Unique company ID</param>
        /// <returns>CompanyOutput</returns>
        [HttpGet("{Cik}")]
        public async Task<IActionResult> Get(long Cik)
        {
            EdgarCompanyData? edgarCompanyData = null;
            Model.CompanyOutput company = null;

            try
            {
                edgarCompanyData = await _crudDbService.GetCompanyData(Cik);

                if (edgarCompanyData != null) {
                    company = _mapper.Map<Model.CompanyOutput>(edgarCompanyData);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR: Company GET(id) " + ex.Message);
            }

            if (company == null) return NotFound();
            return Ok(company);
        }

    }
}
