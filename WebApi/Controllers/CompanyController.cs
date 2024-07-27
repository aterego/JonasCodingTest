using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public IEnumerable<CompanyDto> GetAll()
        {
            var items = _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public CompanyDto Get(string companyCode)
        {
            var item = _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            var companyDto = JsonConvert.DeserializeObject<CompanyDto>(value);
            var company = _mapper.Map<CompanyInfo>(companyDto);
            _companyService.AddCompany(company);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            var companyDto = JsonConvert.DeserializeObject<CompanyDto>(value);
            var company = _mapper.Map<CompanyInfo>(companyDto);
            var (siteId, companyCode) = DecodeId(id);
            company.SiteId = siteId;
            company.CompanyCode = companyCode;
            _companyService.UpdateCompany(company);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var (siteId, companyCode) = DecodeId(id);
            _companyService.DeleteCompany(siteId, companyCode);
        }

        private (string siteId, string companyCode) DecodeId(int id)
        {
            //assume id is"{siteId}-{companyCode}"
            var decodedString = id.ToString(); 
            var parts = decodedString.Split('-');
            return (parts[0], parts[1]);
        }

    }
}