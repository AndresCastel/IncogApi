using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Specifications;
using IncognitusBack.Core.Specifications.ReportsSP;
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
        private readonly IAsyncRepositoryNormal<WhiteboardActual> _WhiteboardRepository;
        public ReportsViewModelService(IAsyncRepositoryNormal<TimesheetsReport> TimeRepository, IAsyncRepository<Employee_Register> employeeRegisterRepository,
            IAsyncRepositoryNormal<WhiteboardActual> WhiteboardRepository)
        {
            _WhiteboardRepository = WhiteboardRepository;
            _TimeRepository = TimeRepository;
            _employeeRegisterRepository = employeeRegisterRepository;
        }

        public async Task<List<WhiteboardActualViewModel>> GetWhiteBoard(FilterParametersWhiteboard filter)
        {
            List<WhiteboardActual> lst = new List<WhiteboardActual>();
            List<WhiteboardActualViewModel> lsttime = new List<WhiteboardActualViewModel>();
            try
            {
                switch (filter.filter)
                {
                    case "All":
                        WhiteBoardSpecification WhiteboardSpecificationall = new WhiteBoardSpecification(General.SplitCreateDate(filter.DateGridFilter));
                        lst = await _WhiteboardRepository.ListAllWhiteboardAsyncSpec(WhiteboardSpecificationall);
                        break;
                    //case "Employee":
                    //    var RosterSpDate = new TimeSheetSpecification(false, filter.Employee, General.CastStringtoDateTime(filter.DateGridFilter));
                    //    lst = await _WhiteboardRepository.ListAllTimeSheetAsyncSpec(RosterSpDate);
                    //    break;
                    //case "Export":
                    //    var RosterSpExpor = new TimeSheetSpecification(false, General.CastStringtoDateTime(filter.DateFrom), General.CastStringtoDateTime(filter.DateTo));
                    //    lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(RosterSpExpor);
                    //    break;
                    //default:
                    //    TimeSheetSpecification timeSheetSpecificationdef = new TimeSheetSpecification(General.CastStringtoDateTime(filter.DateGridFilter));
                    //    lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(timeSheetSpecificationdef);
                    //    break;
                }

                foreach (var item in lst)
                {
                    lsttime.Add(CreateViewModelFromWhiteboard(item));
                }
            }
            catch (Exception ex)
            {


            }

            return lsttime;
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
                        TimeSheetSpecification timeSheetSpecificationall = new TimeSheetSpecification(General.SplitCreateDate(filter.DateGridFilter));
                        lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(timeSheetSpecificationall);
                        break;
                    case "Employee":
                        var RosterSpDate = new TimeSheetSpecification(false, filter.Employee, General.SplitCreateDate(filter.DateGridFilter));
                        lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(RosterSpDate);
                        break;
                    case "Export":
                        var RosterSpExpor = new TimeSheetSpecification(false, General.SplitCreateDate(filter.DateFrom), General.SplitCreateDate(filter.DateTo));
                        lst = await _TimeRepository.ListAllTimeSheetAsyncSpec(RosterSpExpor);
                        break;
                    default:
                        TimeSheetSpecification timeSheetSpecificationdef = new TimeSheetSpecification(General.SplitCreateDate(filter.DateGridFilter));
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
            

            try
            {
                var tm = (await _employeeRegisterRepository.ListAsync(timeSheetSpecificationall)).FirstOrDefault();
                if (tm != null)
                {
                    if(tm.Active && string.IsNullOrEmpty(tm.EndTime))
                    {
                        if (timesheet.Active && string.IsNullOrEmpty(timesheet.EndTime))
                        {
                            tm.StartTime = timesheet.StartTime;
                            tm.EndTime = timesheet.EndTime; 
                            tm.Break = timesheet.Break;
                            tm.Active = timesheet.Active;
                        }
                        else
                        {
                            tm.Type_RegisterId = 2;
                            tm.StartTime = timesheet.StartTime;
                            tm.EndTime = timesheet.EndTime;
                            tm.Break = timesheet.Break;
                            tm.Active = timesheet.Active;
                            //Create new Register
                            Employee_Register employee = new Employee_Register()
                            {
                                EmployeeId = tm.EmployeeId,
                                Active = true,
                                Day = tm.Day,
                                Payroll = tm.Payroll,
                                Type_RegisterId = 2,
                                StartTime = null,
                                RosterId = tm.RosterId
                            };
                            employee = await _employeeRegisterRepository.AddAsync(employee);
                        }

                    }
                    else
                    {
                        if (timesheet.Active && string.IsNullOrEmpty(timesheet.EndTime))
                        {
                            tm.StartTime = timesheet.StartTime;
                            tm.EndTime = null;
                            tm.Type_RegisterId = 1;
                            tm.Break = timesheet.Break;
                            tm.Active = timesheet.Active;
                            var Registerespe = new EmployeeRegisterSpecification(tm.EmployeeId, true);
                            var UltimateRegister = (await _employeeRegisterRepository.ListAsync(Registerespe)).FirstOrDefault();
                            await _employeeRegisterRepository.DeleteAsync(UltimateRegister);
                        }
                        else
                        {
                            tm.Type_RegisterId = 2;
                            tm.StartTime = timesheet.StartTime;
                            tm.EndTime = timesheet.EndTime;
                            tm.Break = timesheet.Break;
                            tm.Active = timesheet.Active;
                            
                        }
                    }

                   
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
            viewModel.LastName = Timesh.LastName;
            viewModel.MiddleName = Timesh.MiddleName;
            viewModel.Name = Timesh.Name;
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

        private WhiteboardActualViewModel CreateViewModelFromWhiteboard(WhiteboardActual Timesh)
        {
            var viewModel = new WhiteboardActualViewModel();
            viewModel.Area = Timesh.Area;
            viewModel.Name = Timesh.Name;
            viewModel.LastName = Timesh.LastName;
            viewModel.MiddleName = Timesh.MiddleName;
            viewModel.Name = Timesh.Name;
            viewModel.Payroll = Timesh.Payroll;
            viewModel.StartTime = Timesh.StartTime;
            viewModel.EndTime = Timesh.EndTime;
            viewModel.RosterStart = Timesh.RosterStart;
            viewModel.RosterEnd = Timesh.RosterEnd;
            viewModel.Date = Timesh.Date;
            viewModel.LabourType = Timesh.LabourType;
            viewModel.Precint = Timesh.Precint;
            viewModel.Zone = Timesh.Zone;
            viewModel.EventName = Timesh.EventName;
            return viewModel;
        }

    }
}
