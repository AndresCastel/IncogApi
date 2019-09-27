using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using IncogUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Services
{
    public class ChargesViewModelService : IChargesViewModelService
    {
        private readonly IAsyncRepository<RosterC> _RosterRepository;
        private readonly IAsyncRepository<Employee> _employRepository;
        public ChargesViewModelService(IAsyncRepository<RosterC> RosterRepository, IAsyncRepository<Employee> EmployRepository)
        {
            _RosterRepository = RosterRepository;
            _employRepository = EmployRepository;
        }
        public async Task<MessageResponseViewModel<RosterWM>> ChageRoster(List<RosterCViewModel> lstRoster)
        {
            MessageResponseViewModel<RosterWM> Message = new MessageResponseViewModel<RosterWM>();
            try
            {
                foreach (var item in lstRoster)
                {
                    await _RosterRepository.AddAsync(CreateRosterCFromViewModel(item));
                }
                Message.Succesfull = true;
                return Message;
            }
            catch (Exception ex)
            {
                Message.Succesfull = false;
                Message.Message = ex.Message;
                return Message;
            }
        }

        public async Task<MessageResponseViewModel<EmployeesChargeMW>> ChageEmployees(List<EmployeeViewModel> lstEmployees)
        {
            MessageResponseViewModel<EmployeesChargeMW> Message = new MessageResponseViewModel<EmployeesChargeMW>();
            try
            {
                foreach (var item in lstEmployees)
                {
                    await _employRepository.AddAsync(CreateEmployeesFromViewModel(item));
                }
                Message.Succesfull = true;
                return Message;
            }
            catch (Exception ex)
            {
                Message.Succesfull = false;
                Message.Message = ex.Message + ex.InnerException;
                return Message;
            }
        }

        private Employee CreateEmployeesFromViewModel(EmployeeViewModel employ)
        {
            var viewModel = new Employee();
            viewModel.Id = employ.Id;
            viewModel.Active = employ.Active;
            viewModel.Barcode = employ.Barcode;
            viewModel.Email = employ.Email;
            viewModel.LastName = employ.LastName;
            viewModel.MiddleName = employ.MiddleName;
            viewModel.Name = employ.Name;
            viewModel.Payroll = employ.Payroll;
            viewModel.RolId = employ.RolId;
            return viewModel;
        }

        private RosterC CreateRosterCFromViewModel(RosterCViewModel roster)
        {
            var viewModel = new RosterC();
            viewModel.Id = roster.Id;
            viewModel.Area = roster.Area;
            viewModel.Break = roster.Break;
            viewModel.Confirm = roster.Confirm;
            viewModel.Date = General.CastStringtoDateTime(roster.Date);
            viewModel.Day = roster.Day;
            viewModel.Employee = roster.Employee;
            viewModel.EndTime = roster.EndTime;
            viewModel.LabourType = roster.LabourType;
            viewModel.LookedIn = roster.LookedIn;
            viewModel.Payroll = roster.Payroll;
            viewModel.Precint = roster.Precint;
            viewModel.ShiftNum = roster.ShiftNum;
            viewModel.StartTime = roster.StartTime;
            viewModel.Zone = roster.Zone;
            viewModel.EventName = roster.EventName;
            return viewModel;
        }
    }
}
