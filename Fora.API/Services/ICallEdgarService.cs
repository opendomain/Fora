using Fora.Model;

namespace Fora.Services
{
    public interface ICallEdgarService
    {
        Task<EdgarCompanyInfo?> GetEdgarInfo(long cik);
    }
}
