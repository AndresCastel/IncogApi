using IncognitusBack.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Interfaces
{
    public interface IEmployeeViewModelService
    {
        Task<List<TimesheetsReportViewModel>> GetEmployeesSignInOff();

        Task<EmployeeRegisterViewModel> GetEmployeebyBarcode(string Barcode);

        Task<MessageResponseViewModel> RegisterEmployee(EmployeeRegisterViewModel Register);

        
    }
}
