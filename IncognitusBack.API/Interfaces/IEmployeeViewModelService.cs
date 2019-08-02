using IncognitusBack.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Interfaces
{
    public interface IEmployeeViewModelService
    {
        Task<EmployeeViewModel> GetEmployeebyBarcode(string Barcode);
    }
}
