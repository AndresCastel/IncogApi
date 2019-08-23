using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Services
{
    public class ChargesViewModelService : IChargesViewModelService
    {
        private readonly IAsyncRepository<RosterC> _RosterRepository;
        public ChargesViewModelService(IAsyncRepository<RosterC> RosterRepository)
        {
            _RosterRepository = RosterRepository;
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

        private RosterC CreateRosterCFromViewModel(RosterCViewModel roster)
        {
            var viewModel = new RosterC();
            viewModel.Id = roster.Id;
            viewModel.Area = roster.Area;
            viewModel.Break = roster.Break;
            viewModel.Confirm = roster.Confirm;
            viewModel.Date = roster.Date;
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
