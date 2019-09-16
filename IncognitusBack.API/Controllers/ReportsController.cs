﻿using System;
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
        public async Task<List<TimesheetsReportViewModel>> GetEmployeesSignInOff(FilterParametersRoster filter)
        {
            var result = await _ReportsService.GetEmployeesSignInOff(filter);

            return result;
        }
    }
}