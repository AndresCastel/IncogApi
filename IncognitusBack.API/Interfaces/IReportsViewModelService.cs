using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Interfaces
{
    public interface IReportsViewModelService
    {
        Task<List<TimesheetsReportViewModel>> GetEmployeesSignInOff();
    }
}
