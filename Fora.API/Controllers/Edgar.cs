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
        private readonly ICallEdgarService _callEdgarService;
        private readonly ILogger<Edgar> _logger;

        public Edgar(ICallEdgarService callEdgarService, ILogger<Edgar> logger)
        {
            _callEdgarService = callEdgarService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long cik)
        {
            EdgarCompanyInfo? edgarCompanyInfo = await _callEdgarService.GetEdgarInfo(cik);

            if (edgarCompanyInfo == null) { 
                return NotFound();
            }

            return Ok(edgarCompanyInfo);
        }
    }
}
