using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Services
{
    public class EmployeeViewModelService: IEmployeeViewModelService
    {
        private readonly IAsyncRepository<Employee> _employeeRepository;
        public EmployeeViewModelService(IAsyncRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeViewModel> GetEmployeebyBarcode(string Barcode)
        {
            var EmployeeSpec = new EmployeeSpecification(Barcode);
            var employ = (await _employeeRepository.ListAsync(EmployeeSpec)).FirstOrDefault();

            return CreateViewModelFromEmployee(employ);
        }

        private EmployeeViewModel CreateViewModelFromEmployee(Employee employee)
        {
            var viewModel = new EmployeeViewModel();
            viewModel.Id = employee.Id;
            viewModel.Name = employee.Name;
            viewModel.MiddleName = employee.MiddleName;
            viewModel.LastName = employee.LastName;
            viewModel.Email = employee.Email;
            viewModel.RolId = employee.RolId;
            viewModel.Active = employee.Active;
            return viewModel;
        }
    }
}
