using AutoMapper;
using Fora.Model;
using Fora.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Company : ControllerBase
    {
        private readonly IMapper _mapper;
        private IForaService _foraService;

        public Company(IMapper mapper, IForaService foraService)
        {
            _mapper = mapper;
            _foraService = foraService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(char? startChar)
        {
            List<EdgarCompanyData>? allEdgarCompanyData = await _foraService.GetAllCompanies();

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
        public async Task<IActionResult> Get(int Cik)
        {
            EdgarCompanyData? edgarCompanyData = await _foraService.GetCompany(Cik);

            Model.CompanyOutput company;
            company = _mapper.Map<Model.CompanyOutput>(edgarCompanyData);

            return Ok(company);
        }

    }
}
