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
        public IActionResult Get() {
            var allCompanies = _foraService.GetAllCompanies();
            return Ok(allCompanies);
        }

        [HttpGet("{Cik}")]
        public IActionResult Get(int Cik) {
            var allCompanies = _foraService.GetCompany(Cik);
            return Ok(allCompanies);
        }

    }
}
