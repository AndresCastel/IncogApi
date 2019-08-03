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
        public async Task<EmployeeRegisterViewModel> GetEmployee(string barcode)
        {
            var result = await _EmployeeService.GetEmployeebyBarcode(barcode);

            return result;
        }

        [HttpPost("register/")]
        public async Task<MessageResponseViewModel> RegisterEmployee(EmployeeRegisterViewModel EmployeeRegister)
        {
            var result = await _EmployeeService.RegisterEmployee(EmployeeRegister);

            return result;
        }

    }
}
