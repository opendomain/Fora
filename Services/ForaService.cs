using Fora.Model;

namespace Fora.Services
{
    public class ForaService : IForaService
    {
        private readonly List<EdgarCompanyInfo> _edgarCompanyInfoList;

        public ForaService()
        {
            _edgarCompanyInfoList = new List<EdgarCompanyInfo>();

            AddCompany(0000018926, "LUMEN TECHNOLOGIES, INC.");
            AddCompany(0000892553, "CHART INDUSTRIES, INC.");
            AddCompany(1858912, "GARDINER HEALTHCARE ACQUISITIONS CORP.");
        }

        public bool AddCompany(int Cik, string entityName)
        {
            EdgarCompanyInfo edgarCompanyInfo;

            int companyIndex = _edgarCompanyInfoList.FindIndex(index => index.Cik == Cik);
            if (companyIndex < 0)
            {
                // TODO: Check if name alreasdy exists
                edgarCompanyInfo = new EdgarCompanyInfo(Cik, entityName);
                _edgarCompanyInfoList.Add(edgarCompanyInfo);

                return true;
            }

            return false;
        }

        public bool DeleteCompany(int Cik)
        {
            int companyIndex = _edgarCompanyInfoList.FindIndex(index => index.Cik == Cik);
            if (companyIndex >= 0) { 
                _edgarCompanyInfoList.RemoveAt(companyIndex);
                return true;
            }

            return false;
        }

        public List<EdgarCompanyInfo> GetAllCompanies()
        {
            return _edgarCompanyInfoList.ToList();
        }

        public EdgarCompanyInfo? GetCompany(int Cik)
        {
            // TODO: Use Id instead of Cik?
            return _edgarCompanyInfoList.FirstOrDefault(c => c.Cik == Cik);
        }

        public bool UpdateCompany(int Cik, string entityName)
        {
            int companyIndex = _edgarCompanyInfoList.FindIndex(index => index.Cik == Cik);
            if (companyIndex >= 0)
            {
                EdgarCompanyInfo edgarCompanyInfo;
                edgarCompanyInfo = _edgarCompanyInfoList[companyIndex];

                edgarCompanyInfo.EntityName = entityName;

                // TODO: Is this required?
                _edgarCompanyInfoList[companyIndex] = edgarCompanyInfo;

                return true;
            }

            return false;

        }
    }
}
