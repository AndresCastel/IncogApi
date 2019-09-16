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

  

        public async Task<List<TimesheetsReportViewModel>> GetEmployeesSignInOff(FilterParametersRoster filter)
        {
            List<TimesheetsReport> lst = new List<TimesheetsReport>();
            List<TimesheetsReportViewModel> lsttime = new List<TimesheetsReportViewModel>();
            try
            {
                switch (filter.filter)
                {
                    case "All":
                        TimeSheetSpecification timeSheetSpecificationall = new TimeSheetSpecification();
                        lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(timeSheetSpecificationall);
                        break;
                    case "Employee":
                        var RosterSpDate = new TimeSheetSpecification(false, filter.Employee);
                        lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(RosterSpDate);
                        break;
                    default:
                        TimeSheetSpecification timeSheetSpecificationdef = new TimeSheetSpecification();
                        lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(timeSheetSpecificationdef);
                        break;
                }

                foreach (var item in lst)
                {
                    lsttime.Add(CreateViewModelFromTimesheets(item));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return lsttime;
        }

        private TimesheetsReportViewModel CreateViewModelFromTimesheets(TimesheetsReport Timesh)
        {
            var viewModel = new TimesheetsReportViewModel();
            viewModel.Area = Timesh.Area;
            viewModel.Employee = Timesh.Employee;
            // viewModel.LastName = Timesh.LastName;
            viewModel.Break = Timesh.Break;
            viewModel.Payroll = Timesh.Payroll;
            viewModel.StartTime = Timesh.StartTime;
            viewModel.EndTime = Timesh.EndTime;
            viewModel.Day = Timesh.Day;
            viewModel.LabourType = Timesh.LabourType;
            viewModel.Precint = Timesh.Precint;
            viewModel.Zone = Timesh.Zone;
            viewModel.Id = Timesh.Id;
            viewModel.LookedIn = Timesh.LookedIn;
            viewModel.EventName = Timesh.EventName;
            viewModel.Confirm = Timesh.Confirm;
            return viewModel;
        }

    }
}
