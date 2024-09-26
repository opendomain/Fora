using Fora.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdgarCompany : ControllerBase
    {
        private IForaService _foraService;

        public EdgarCompany(IForaService foraService)
        {
            _foraService = foraService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Model.EdgarCompanyInfo>? allEdgarCompanies = _foraService.GetAllCompanies();
            return Ok(allEdgarCompanies);
        }

        [HttpGet("{Cik}")]
        public IActionResult Get(int Cik)
        {
            Model.EdgarCompanyInfo? edgarCompany = _foraService.GetCompany(Cik);
            return Ok(edgarCompany);
        }

    }
}
