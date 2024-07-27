using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
         public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = await _companyService.GetAllCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public async Task Post([FromBody] string value)
        {
            var companyDto = JsonConvert.DeserializeObject<CompanyDto>(value);
            var company = _mapper.Map<CompanyInfo>(companyDto);
            await _companyService.AddCompanyAsync(company);
        }

        // PUT api/<controller>/5
        public async Task Put(int id, [FromBody] string value)
        {
            var companyDto = JsonConvert.DeserializeObject<CompanyDto>(value);
            var company = _mapper.Map<CompanyInfo>(companyDto);
            var (siteId, companyCode) = DecodeId(id);
            company.SiteId = siteId;
            company.CompanyCode = companyCode;
            await _companyService.UpdateCompanyAsync(company);
        }

        // DELETE api/<controller>/5
        public async Task Delete(int id)
        {
            var (siteId, companyCode) = DecodeId(id);
            await _companyService.DeleteCompanyAsync(siteId, companyCode);
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