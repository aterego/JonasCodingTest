using System.Collections.Generic;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);
        void AddCompany(CompanyInfo company);
        void UpdateCompany(CompanyInfo company);
        void DeleteCompany(string siteId, string companyCode);
    }
}
