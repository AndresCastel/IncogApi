using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IncognitusBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly IReportsViewModelService _ReportsService;

        public ReportsController(IReportsViewModelService EReportsService)
        {
            this._ReportsService = EReportsService;
        }

        [HttpPost("timesheet")]
        public async Task<List<TimesheetsReportViewModel>> GetEmployeesSignInOff(FilterParametersTimesheet filter)
        {
            var result = await _ReportsService.GetEmployeesSignInOff(filter);

            return result;
        }
        [HttpPost("timesheet/edit")]
        public async Task<MessageResponseViewModel<bool>> EditEmployeesSignInOff(TimesheetsReportViewModel timesheet)
        {
            var result = await _ReportsService.EditEmployeesSignInOff(timesheet);

            return result;
        }

        [HttpPost("timesheet/delete")]
        public async Task<MessageResponseViewModel<bool>> DeleteEmployeesSignInOff(TimesheetsReportViewModel timesheet)
        {
            var result = await _ReportsService.DeleteEmployeesSignInOff(timesheet);

            return result;
        }
    }
}