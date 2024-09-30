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


        /// <summary>
        /// Get Edgar Company Info directly from Edgar Service
        /// </summary>
        /// <param name="cik">Long company ID (10 digits max)</param>
        /// <returns>EdgarCompanyInfo</returns>
        [HttpGet]
        public async Task<IActionResult> Get(long cik)
        {
            EdgarCompanyInfo? edgarCompanyInfo = null;
            try
            {
                edgarCompanyInfo = await _callEdgarService.GetEdgarInfo(cik);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR: Edgar GET " + ex.Message);
            }

            if (edgarCompanyInfo == null) { 
                return NotFound();
            }

            return Ok(edgarCompanyInfo);
        }
    }
}
