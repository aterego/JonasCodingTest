using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            var res = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode)
        {
            var result = await _employeeRepository.GetByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task AddEmployeeAsync(EmployeeInfo employee)
        {
            var result = _mapper.Map<Employee>(employee);
            await _employeeRepository.SaveEmployeeAsync(result);
        }

        public async Task UpdateEmployeeAsync(EmployeeInfo employee)
        {
            var result = _mapper.Map<Employee>(employee);
            await _employeeRepository.SaveEmployeeAsync(result);
        }

        public async Task DeleteEmployeeAsync(string employeeCode)
        {
            await _employeeRepository.DeleteEmployeeAsync(employeeCode);
        }
    }
}
