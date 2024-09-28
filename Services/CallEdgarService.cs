using Fora.Controllers;
using Fora.Model;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Fora.Services
{
    public class CallEdgarService : ICallEdgarService
    {
        private readonly long MAX_CIK = 9999999999;

        private IHttpClientFactory _httpClientFactory;

        public CallEdgarService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            //, IConfiguration configuration
            //string? baseAddress = configuration["EdgarUrl"];

            //if (!string.IsNullOrWhiteSpace(baseAddress)) _baseAddress = baseAddress;
        }

        private string FormatID(long cik)
        {
            if (cik <= 0 || cik > MAX_CIK)
            {
                throw new ArgumentException("cik out of range");
            }

            string fileName = "CIK" + cik.ToString("0000000000") + ".json";

            return fileName;
        }

        public async Task<EdgarCompanyInfo?> GetEdgarInfo(long cik)
        {
            EdgarCompanyInfo? edgarCompanyInfo = null;


            string ckid = FormatID(cik);
            try
            {
                // TODO: check if better to create client for each request
                HttpClient httpClient;

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
                            edgarCompanyInfo = new EdgarCompanyInfo(cik, "");
                            break;

                        default:
                            // TODO: Deal with 403 - rate limiting
                            edgarCompanyInfo = null;
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: check if 404 still needed here
                // Use empty company name to indicate not found
                edgarCompanyInfo = new EdgarCompanyInfo(cik, "ERROR: " + ex.Message);
            }

            return edgarCompanyInfo;
        }
    }
}
