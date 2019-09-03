using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Specifications;
using IncognitusBack.Core.Specifications.RosterSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Services
{
    public class ReportsViewModelService : IReportsViewModelService
    {
        private readonly IAsyncRepository<Employee> _employeeRepository;
        private readonly IAsyncRepositoryNormal<TimesheetsReport> _TimeRepository;
        public ReportsViewModelService(IAsyncRepositoryNormal<TimesheetsReport> TimeRepository)
        {
            _TimeRepository = TimeRepository;
        }

  

        public async Task<List<TimesheetsReportViewModel>> GetEmployeesSignInOff()
        {
            List<TimesheetsReportViewModel> lst = new List<TimesheetsReportViewModel>();
            try
            {
                var timeSheetSpecification = new TimeSheetSpecification(false);
                var Time = await _TimeRepository.ListAllTimeSheetAsyncSpec(timeSheetSpecification);

                foreach (var item in Time)
                {
                    lst.Add(CreateViewModelFromTimesheets(item));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return lst;
        }

        private TimesheetsReportViewModel CreateViewModelFromTimesheets(TimesheetsReport Timesh)
        {
            var viewModel = new TimesheetsReportViewModel();
            viewModel.Name = Timesh.Name;
            viewModel.LastName = Timesh.LastName;
            viewModel.Break = Timesh.Break;
            viewModel.Payroll = Timesh.Payroll;
            viewModel.StartTime = Timesh.StartTime;
            viewModel.EndTime = Timesh.EndTime;
            viewModel.Day = Timesh.Day;
            return viewModel;
        }

    }
}
