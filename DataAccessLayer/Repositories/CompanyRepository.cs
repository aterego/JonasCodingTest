using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Common.Logger;
using System;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbWrapper<Company> _companyDbWrapper;
        private readonly ILogger _logger;

        public CompanyRepository(IDbWrapper<Company> companyDbWrapper, ILogger logger)
        {
            _companyDbWrapper = companyDbWrapper;
            _logger = logger;

        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            try
            {
                return await _companyDbWrapper.FindAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllAsync", ex);
                throw;
            }
        }

        public async Task<Company> GetByCodeAsync(string companyCode)
        {
            try
            {
                var result = await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode));
                return result?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetByCodeAsync with companyCode={companyCode}", ex);
                throw;
            }
        }

        public async Task<bool> SaveCompanyAsync(Company company)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"Error in SaveCompanyAsync with company={company}", ex);
                throw;
            }
        }
        
        //Delete comapny
        public async Task<bool> DeleteCompanyAsync(string siteId, string companyCode)
        {
            try
            {
                var companyToDelete = (await _companyDbWrapper.FindAsync(t =>
                    t.SiteId.Equals(siteId) && t.CompanyCode.Equals(companyCode)))?.FirstOrDefault();
                if (companyToDelete != null)
                {
                    return await _companyDbWrapper.DeleteAsync(t =>
                        t.SiteId.Equals(siteId) && t.CompanyCode.Equals(companyCode));
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteCompanyAsync with siteId={siteId}, companyCode={companyCode}", ex);
                throw;
            }
        }

    }
}
