using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Entities.Roster;
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
    public class RosterViewModelService : IRosterViewModelService
    {
        private readonly IAsyncRepository<RosterC> _RosterRepository;
        public RosterViewModelService(IAsyncRepository<RosterC> RosterRepository)
        {
            _RosterRepository = RosterRepository;
        }

        public async Task<List<RosterCViewModel>> GetAllRoster(FilterParametersRoster filter)
        {
            List<RosterC> lstroster = new List<RosterC>();
            switch (filter.filter)
            {
                case "All":
                    RosterSpecification timeSheetSpecificationall = new RosterSpecification(General.CastStringtoDateTime(filter.DateGridFilter));
                    lstroster = await _RosterRepository.ListAsync(timeSheetSpecificationall);
                    break;
                case "Employee":
                    var RosterSpemployee = new RosterSpecification(filter.Employee, General.CastStringtoDateTime(filter.DateGridFilter));
                    lstroster = await _RosterRepository.ListAsync(RosterSpemployee);
                    break;
                case "Export":
                    var RosterSpExpor = new RosterSpecification(General.CastStringtoDateTime(filter.DateFrom), General.CastStringtoDateTime(filter.DateTo));
                    lstroster = await _RosterRepository.ListAsync(RosterSpExpor);
                    break;
                default:
                    RosterSpecification timeSheetSpecificationdef = new RosterSpecification(General.CastStringtoDateTime(filter.DateGridFilter));
                    lstroster = await _RosterRepository.ListAsync(timeSheetSpecificationdef);
                    break;
            }

            //Andres, Think component to add filters depending of the filters
           
 
            List<RosterCViewModel> lst = new List<RosterCViewModel>();

                foreach (var item in lstroster)
                {
                    lst.Add(CreateViewModelFromRoster(item));
                }
            return lst;
        }

        private RosterCViewModel CreateViewModelFromRoster(RosterC Roster)
        {
            var viewModel = new RosterCViewModel();
            viewModel.Id = Roster.Id;
            viewModel.Area = Roster.Area;
            viewModel.Break = Roster.Break;
            viewModel.Confirm = Roster.Confirm;
            viewModel.Date = Roster.Date.ToShortDateString();
            viewModel.Day = Roster.Day;
            viewModel.Payroll = Roster.Payroll;
            viewModel.Employee = Roster.Employee;
            viewModel.EndTime = Roster.EndTime;
            viewModel.EventName = Roster.EventName;
            viewModel.LabourType = Roster.LabourType;
            viewModel.LookedIn = Roster.LookedIn;
            viewModel.Precint = Roster.Precint;
            viewModel.ShiftNum = Roster.ShiftNum;
            viewModel.StartTime = Roster.StartTime;
            viewModel.Zone = Roster.Zone;
            return viewModel;
        }

        private RosterC CreateRosterFromViewModel(RosterCViewModel Roster)
        {
            var viewModel = new RosterC();
            viewModel.Id = Roster.Id;
            viewModel.Area = Roster.Area;
            viewModel.Break = Roster.Break;
            viewModel.Confirm = Roster.Confirm;
            viewModel.Date = General.CastStringtoDateTime(Roster.Date);
            viewModel.Day = Roster.Day;
            viewModel.Payroll = Roster.Payroll;
            viewModel.Employee = Roster.Employee;
            viewModel.EndTime = Roster.EndTime;
            viewModel.EventName = Roster.EventName;
            viewModel.LabourType = Roster.LabourType;
            viewModel.LookedIn = Roster.LookedIn;
            viewModel.Precint = Roster.Precint;
            viewModel.ShiftNum = Roster.ShiftNum;
            viewModel.StartTime = Roster.StartTime;
            viewModel.Zone = Roster.Zone;
            return viewModel;
        }

        public async Task<MessageResponseViewModel<bool>> SetRoster(RosterCViewModel roster)
        {
            MessageResponseViewModel<bool> res = new MessageResponseViewModel<bool>();

            try
            {
                RosterC ros = CreateRosterFromViewModel(roster);
                await _RosterRepository.AddAsync(ros);
                 res.Succesfull = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message + ex.InnerException;
                res.Succesfull = false;
                return res;
            }

        }

    }
}
