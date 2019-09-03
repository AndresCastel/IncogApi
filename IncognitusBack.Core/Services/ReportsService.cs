using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IncognitusBack.Core.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsService _TimesheetRepository;

        public ReportsService(IReportsService TimesheetRepositor)
        {
            _TimesheetRepository = TimesheetRepositor;
        }
        public Task<TimesheetsReport> GetTimesheetsAsync()
        {
            return _TimesheetRepository.GetTimesheetsAsync();
        }
    }
}
