using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Common.Logger;
using System;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, ILogger logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            try
            {
                var res = await _employeeRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllEmployeesAsync", ex);
                throw;
            }
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode)
        {
            try
            {
                var result = await _employeeRepository.GetByCodeAsync(employeeCode);
                return _mapper.Map<EmployeeInfo>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEmployeeByCodeAsync with employeeCode={employeeCode}", ex);
                throw;
            }
        }

        public async Task AddEmployeeAsync(EmployeeInfo employee)
        {
            try
            {
                var result = _mapper.Map<Employee>(employee);
                await _employeeRepository.SaveEmployeeAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddEmployeeAsync with employee={employee}", ex);
                throw;
            }
        }

        public async Task UpdateEmployeeAsync(EmployeeInfo employee)
        {
            try
            {
                var result = _mapper.Map<Employee>(employee);
                await _employeeRepository.SaveEmployeeAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateEmployeeAsync with employee={employee}", ex);
                throw;
            }
        }

        public async Task DeleteEmployeeAsync(string employeeCode)
        {
            try
            {
                await _employeeRepository.DeleteEmployeeAsync(employeeCode);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteEmployeeAsync with employeeCode={employeeCode}", ex);
                throw;
            }
        }
    }
}
