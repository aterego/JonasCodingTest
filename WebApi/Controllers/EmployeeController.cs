using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET api/<controller>
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var items = await _employeeService.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeDto>(item);
        }

        // POST api/<controller>
        public async Task Post([FromBody] string value)
        {
            var employeeDto = JsonConvert.DeserializeObject<EmployeeDto>(value);
            var employee = _mapper.Map<EmployeeInfo>(employeeDto);
            await _employeeService.AddEmployeeAsync(employee);
        }

        // PUT api/<controller>/5
        public async Task Put(string employeeCode, [FromBody] string value)
        {
            var employeeDto = JsonConvert.DeserializeObject<EmployeeDto>(value);
            var employee = _mapper.Map<EmployeeInfo>(employeeDto);
            employee.EmployeeCode = employeeCode;
            await _employeeService.UpdateEmployeeAsync(employee);
        }

        // DELETE api/<controller>/5
        public async Task Delete(string employeeCode)
        {
            await _employeeService.DeleteEmployeeAsync(employeeCode);
        }
    }
}
