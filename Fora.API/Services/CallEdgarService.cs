using Fora.Controllers;
using Fora.Model;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Fora.Services
{
    /// <summary>
    /// Service to call Edgar HTTP endpoint to get company data
    /// </summary>
    public class CallEdgarService : ICallEdgarService
    {
        private const long MAX_CIK = 9999999999;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CallEdgarService> _logger;

        public CallEdgarService(IHttpClientFactory httpClientFactory, ILogger<CallEdgarService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        
        /// <summary>
        /// Format cik be exactly 10 digits long prepended with "CIK"  and endith with ".json" as expected for Edgar API 
        /// </summary>
        /// <param name="cik">Company id - 10 digits max</param>
        /// <returns>String of formated company request</returns>
        /// <exception cref="ArgumentException">If CIK is negative or more than 10 digits</exception>
        private string FormatID(long cik)
        {
            if (cik <= 0 || cik > MAX_CIK)
            {
                throw new ArgumentException("cik out of range");
            }

            string fileName = "CIK" + cik.ToString("0000000000") + ".json";

            return fileName;
        }

        /// <summary>
        /// Call Edgar using HTTP client to get company info.
        /// HTTP client info is Injected to allow change of URL or headers
        /// </summary>
        /// <param name="cik">long - 10 digits max</param>
        /// <returns>EdageCompanyInfo</returns>
        public async Task<EdgarCompanyInfo?> GetEdgarInfo(long cik)
        {
            EdgarCompanyInfo? edgarCompanyInfo = null;
            HttpClient httpClient = null;

            try
            {
                string ckid = FormatID(cik);

                // TODO: check if better to create client for each request
                httpClient = _httpClientFactory.CreateClient("Edgar");

                var response = await httpClient.GetAsync(ckid);
                if (response != null)
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            edgarCompanyInfo = await response.Content.ReadFromJsonAsync<EdgarCompanyInfo>();
                            break;

                        case System.Net.HttpStatusCode.NotFound:
                            // Use empty company name to indicate not found
                            edgarCompanyInfo = new EdgarCompanyInfo(cik, "");
                            break;

                        // TODO: Deal with 403 - rate limiting
                        default:
                            _logger.LogWarning("Bad Status for retrival of Edgar data.");
                            edgarCompanyInfo = null;
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR retrival of Edgar data for CIK:" + cik + " message:" + ex.Message);
                // TODO: filter out Company Errors
                edgarCompanyInfo = new EdgarCompanyInfo(cik, "" );
            }

            return edgarCompanyInfo;
        }
    }
}
