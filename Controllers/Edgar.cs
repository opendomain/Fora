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
        public Edgar(ICallEdgarService callEdgarService)
        {
            _callEdgarService = callEdgarService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int cik)
        {
            EdgarCompanyInfo? edgarCompanyInfo = await _callEdgarService.GetEdgarInfo(cik);

            if (edgarCompanyInfo == null) { 
                return NotFound();
            }

            return Ok(edgarCompanyInfo);
        }


    }
}
