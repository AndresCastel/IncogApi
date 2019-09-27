using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Specifications;
using IncognitusBack.Core.Specifications.RosterSP;
using IncogUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Services
{
    public class ReportsViewModelService : IReportsViewModelService
    {
        private readonly IAsyncRepository<Employee> _employeeRepository;
        private readonly IAsyncRepository<Employee_Register> _employeeRegisterRepository;
        private readonly IAsyncRepositoryNormal<TimesheetsReport> _TimeRepository;
        public ReportsViewModelService(IAsyncRepositoryNormal<TimesheetsReport> TimeRepository, IAsyncRepository<Employee_Register> employeeRegisterRepository)
        {
            _TimeRepository = TimeRepository;
            _employeeRegisterRepository = employeeRegisterRepository;
        }

  

        public async Task<List<TimesheetsReportViewModel>> GetEmployeesSignInOff(FilterParametersTimesheet filter)
        {
            List<TimesheetsReport> lst = new List<TimesheetsReport>();
            List<TimesheetsReportViewModel> lsttime = new List<TimesheetsReportViewModel>();
            try
            {
                switch (filter.filter)
                {
                    case "All":
                        TimeSheetSpecification timeSheetSpecificationall = new TimeSheetSpecification(General.CastStringtoDateTime(filter.DateGridFilter));
                        lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(timeSheetSpecificationall);
                        break;
                    case "Employee":
                        var RosterSpDate = new TimeSheetSpecification(false, filter.Employee, General.CastStringtoDateTime(filter.DateGridFilter));
                        lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(RosterSpDate);
                        break;
                    case "Export":
                        var RosterSpExpor = new TimeSheetSpecification(false, General.CastStringtoDateTime(filter.DateFrom), General.CastStringtoDateTime(filter.DateTo));
                        lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(RosterSpExpor);
                        break;
                    default:
                        TimeSheetSpecification timeSheetSpecificationdef = new TimeSheetSpecification(General.CastStringtoDateTime(filter.DateGridFilter));
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

              
            }
            
            return lsttime;
        }

        public async Task<MessageResponseViewModel<bool>> EditEmployeesSignInOff(TimesheetsReportViewModel timesheet)
        {
            MessageResponseViewModel<bool> result = new MessageResponseViewModel<bool>();
            EmployeeRegisterSpecification timeSheetSpecificationall = new EmployeeRegisterSpecification(timesheet.Id);
            var tm = (await _employeeRegisterRepository.ListAsync(timeSheetSpecificationall)).FirstOrDefault();

            try
            {
                if (tm != null)
                {
                    tm.Break = timesheet.Break;
                    tm.StartTime = timesheet.StartTime;
                    tm.EndTime = timesheet.EndTime;
                    tm.Active = timesheet.Active;
                    await _employeeRegisterRepository.UpdateAsync(tm);
                }
            }
            catch (Exception ex)
            {
                result.Succesfull = false;
                result.Message = ex.Message + ex.InnerException;
                return result;
            }
            result.Succesfull = true;
            return result;
        }

        public async Task<MessageResponseViewModel<bool>> DeleteEmployeesSignInOff(TimesheetsReportViewModel timesheet)
        {
            MessageResponseViewModel<bool> result = new MessageResponseViewModel<bool>();
            EmployeeRegisterSpecification timeSheetSpecificationall = new EmployeeRegisterSpecification(timesheet.Id);
            var tm = (await _employeeRegisterRepository.ListAsync(timeSheetSpecificationall)).FirstOrDefault();



            try
            {
                if (tm != null)
                {
                    await _employeeRegisterRepository.DeleteAsync(tm);
                }
            }
            catch (Exception ex)
            {
                result.Succesfull = false;
                result.Message = ex.Message + ex.InnerException;
                return result;
            }
            result.Succesfull = true;
            return result;
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
            viewModel.Day = Timesh.Day.ToShortDateString();
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
