using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbWrapper<Company> _companyDbWrapper;

        public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
        {
            _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyDbWrapper.FindAllAsync(); 
        }

        public async Task<Company> GetByCodeAsync(string companyCode)
        {
            var result = await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode));
            return result?.FirstOrDefault();
        }

        public async Task<bool> SaveCompanyAsync(Company company)
        {
            var itemRepo = (await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode)))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return await _companyDbWrapper.UpdateAsync(itemRepo);
            }

            return await _companyDbWrapper.InsertAsync(company);
        }
        
        //Delete comapny
        public async Task<bool> DeleteCompanyAsync(string siteId, string companyCode)
        {
            var companyToDelete = (await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(siteId) && t.CompanyCode.Equals(companyCode)))?.FirstOrDefault();
            if (companyToDelete != null)
            {
                return await _companyDbWrapper.DeleteAsync(t => t.SiteId.Equals(companyToDelete.SiteId) && t.CompanyCode.Equals(companyToDelete.CompanyCode));
            }

            return false;
        }

    }
}
