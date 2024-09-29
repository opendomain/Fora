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
        private ICrudDbService _crudDbService;

        public Company(IMapper mapper, ICrudDbService crudDbService)
        {
            _mapper = mapper;
            _crudDbService = crudDbService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(char? startChar)
        {
            List<EdgarCompanyData>? allEdgarCompanyData = await _crudDbService.GetAllCompanyData(onlyGetUpdatedFlag: true, onlyGetValidNames: true);

            if (startChar != null)
            {
                // TODO: allow more than one letter?
                string strLetter = (startChar.HasValue) ? startChar.ToString(): "";

                allEdgarCompanyData = allEdgarCompanyData.Where(ec => ec.EntityName.StartsWith(strLetter, StringComparison.CurrentCultureIgnoreCase )).ToList();
            }

            List<CompanyOutput> allCompanies = _mapper.Map<List<CompanyOutput>>(allEdgarCompanyData);

            return Ok(allCompanies);
        }

        [HttpGet("{Cik}")]
        public async Task<IActionResult> Get(long Cik)
        {
            EdgarCompanyData? edgarCompanyData = await _crudDbService.GetCompanyData(Cik);

            Model.CompanyOutput company;
            company = _mapper.Map<Model.CompanyOutput>(edgarCompanyData);

            return Ok(company);
        }

    }
}
