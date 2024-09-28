using Fora.Model;
using Fora.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Edgar : ControllerBase
    {
        private ICallEdgarService _callEdgarService;
        private ICrudDbService _crudDbService;
        public Edgar(ICallEdgarService callEdgarService, ICrudDbService crudDbService)
        {
            _callEdgarService = callEdgarService;
            _crudDbService = crudDbService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long Cik)
        {
            EdgarCompanyInfo? edgarCompanyInfo = await _callEdgarService.GetEdgarInfo(Cik);

            if (edgarCompanyInfo == null) { 
                return NotFound();
            }

            return Ok(edgarCompanyInfo);
        }
    }
}
