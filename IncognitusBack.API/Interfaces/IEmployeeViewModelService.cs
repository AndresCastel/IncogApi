using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Interfaces
{
    public interface IEmployeeViewModelService
    {

        Task<MessageResponseViewModel<EmployeeVsRosterVM>> GetEmployeebyBarcode(EmployeeRegisterViewModel employee);

        Task<MessageResponseViewModel<List<EmployeeViewModel>>> GetAllEmployees();

        Task<MessageResponseViewModel<EmployeeRegisterViewModel>> RegisterEmployee(EmployeeRegisterViewModel Register);

        Task<MessageResponseViewModel<AllStuffVM>> GetStuffAsig();
    }
}
