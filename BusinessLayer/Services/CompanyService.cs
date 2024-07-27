using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;
using Common.Logger;
using System;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, ILogger logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync()
        {
            try
            {
                var res = await _companyRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CompanyInfo>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllCompaniesAsync", ex);
                throw;
            }
        }

        //Add Company info
        public async Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode)
        {
            try
            {
                var result = await _companyRepository.GetByCodeAsync(companyCode);
                return _mapper.Map<CompanyInfo>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCompanyByCodeAsync with companyCode={companyCode}", ex);
                throw;
            }
        }

        //Add Company
        public async Task AddCompanyAsync(CompanyInfo company)
        {
            try
            {
                var result = _mapper.Map<Company>(company);
                await _companyRepository.SaveCompanyAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddCompanyAsync with company={company}", ex);
                throw;
            }
        }

        //AVA Update company
        public async Task UpdateCompanyAsync(CompanyInfo company)
        {
            try
            {
                var result = _mapper.Map<Company>(company);
                await _companyRepository.SaveCompanyAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateCompanyAsync with company={company}", ex);
                throw;
            }
        }

        //Delete company
        public async Task DeleteCompanyAsync(string siteId, string companyCode)
        {
            try
            {
                await _companyRepository.DeleteCompanyAsync(siteId, companyCode);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteCompanyAsync with siteId={siteId}, companyCode={companyCode}", ex);
                throw;
            }
        }
    }
}
