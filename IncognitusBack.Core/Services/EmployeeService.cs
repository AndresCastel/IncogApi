using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IncognitusBack.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAsyncRepository<Employee> _employeeRepository;
        public EmployeeService(IAsyncRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
    }


}
