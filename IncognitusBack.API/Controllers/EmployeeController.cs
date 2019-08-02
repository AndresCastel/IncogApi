using IncognitusBack.API.App_Start;
using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IncognitusBack.API.ViewModels;
using IncognitusBack.API.Interfaces;

namespace IncognitusBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeViewModelService _EmployeeService;

        public EmployeeController(IEmployeeViewModelService EmployeeService)
        {
            this._EmployeeService = EmployeeService;
        }

        [HttpGet("get/{barcode}")]
        public async Task<EmployeeViewModel> GetEmployee(string barcode)
        {
            var result = await _EmployeeService.GetEmployeebyBarcode(barcode);

            return result;
        }

    }
}
