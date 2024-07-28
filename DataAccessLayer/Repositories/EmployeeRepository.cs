using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Common.Logger;
using System;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;
        private readonly ILogger _logger;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper, ILogger logger)
        {
            _employeeDbWrapper = employeeDbWrapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await _employeeDbWrapper.FindAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllAsync", ex);
                throw;
            }
        }

        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            try
            {
                var result = await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode));
                return result?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetByCodeAsync with employeeCode={employeeCode}", ex);
                throw;
            }
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            try
            {
                var itemRepo = (await _employeeDbWrapper.FindAsync(t =>
                    t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode) && t.EmployeeCode.Equals(employee.EmployeeCode)))?.FirstOrDefault();
                if (itemRepo != null)
                {
                    itemRepo.EmployeeName = employee.EmployeeName;
                    itemRepo.Occupation = employee.Occupation;
                    itemRepo.EmployeeStatus = employee.EmployeeStatus;
                    itemRepo.EmailAddress = employee.EmailAddress;
                    itemRepo.Phone = employee.Phone;
                    itemRepo.LastModified = employee.LastModified;
                    return await _employeeDbWrapper.UpdateAsync(itemRepo);
                }

                return await _employeeDbWrapper.InsertAsync(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SaveEmployeeAsync with employee={employee}", ex);
                throw;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            try
            {
                var employeeToDelete = (await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode)))?.FirstOrDefault();
                if (employeeToDelete != null)
                {
                    return await _employeeDbWrapper.DeleteAsync(t => t.EmployeeCode.Equals(employeeCode));
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteEmployeeAsync with employeeCode={employeeCode}", ex);
                throw;
            }
        }
    }
}
