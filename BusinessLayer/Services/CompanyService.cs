using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public IEnumerable<CompanyInfo> GetAllCompanies()
        {
            var res = _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        //Add Company info
        public CompanyInfo GetCompanyByCode(string companyCode)
        {
            var result = _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        //Add Company
        public void AddCompany(CompanyInfo company)
        {
            var result = _mapper.Map<Company>(company);
            _companyRepository.SaveCompany(result);
        }

        //AVA Update company
        public void UpdateCompany(CompanyInfo company)
        {
            var result = _mapper.Map<Company>(company);
            _companyRepository.SaveCompany(result);
        }

        //Delete company
        public void DeleteCompany(string siteId, string companyCode)
        {
            _companyRepository.DeleteCompany(siteId, companyCode);
        }
    }
}
