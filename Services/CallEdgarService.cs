using Fora.Model;
using System.Net.Http.Headers;

namespace Fora.Services
{
    public class CallEdgarService: ICallEdgarService
    {
        private readonly string _baseAddress = "https://data.sec.gov/api/xbrl/companyfacts/";
        private HttpClient? _httpClient = null;

        public CallEdgarService()
        {
            SetupClient();
        }

        private void SetupClient() {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseAddress);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.42.0");
        }

        private string FormatID(int cik)
        {
            string fileName = "CIK" + cik.ToString("0000000000") + ".json";

            return fileName;
        }

        public async Task<EdgarCompanyInfo?> GetEdgarInfo(int cik)
        {
            EdgarCompanyInfo? edgarCompanyInfo = null;

            string ckid = FormatID(cik);
            edgarCompanyInfo = await _httpClient.GetFromJsonAsync<EdgarCompanyInfo>(ckid);

            return edgarCompanyInfo;
        }
    }
}
