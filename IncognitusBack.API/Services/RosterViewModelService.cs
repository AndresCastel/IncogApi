using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Entities.Roster;
using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Specifications;
using IncognitusBack.Core.Specifications.RosterSP;
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
                    lstroster = await _RosterRepository.ListAllAsync();
                    break;
                case "Payroll":
                    var RosterSpPayroll = new RosterSpecification(filter.Payroll);
                    lstroster = await _RosterRepository.ListAsync(RosterSpPayroll);
                    break;
                case "Date":
                    var RosterSpDate = new RosterSpecification(filter.DateStart, filter.DateEnd);
                    lstroster = await _RosterRepository.ListAsync(RosterSpDate);
                    break;
                default:
                    lstroster = await _RosterRepository.ListAllAsync();
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
            viewModel.Date = Roster.Date;
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

        private FilterParametersRosterEntity CreateParameters(FilterParametersRoster Roster)
        {
            var viewModel = new FilterParametersRosterEntity();
            viewModel.DateEnd = Roster.DateEnd;
            viewModel.DateStart = Roster.DateStart;
            viewModel.Day = Roster.Day;
            viewModel.Employee = Roster.Employee;
            viewModel.Payroll = Roster.Payroll;
            viewModel.Employee = Roster.Employee;
            return viewModel;
        }

    }
}
