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

        [HttpPost("get")]
        public async Task<MessageResponseViewModel<EmployeeVsRosterVM>> GetEmployee(EmployeeRegisterViewModel employee)
        {

            var result = await _EmployeeService.GetEmployeebyBarcode(employee);

            return result;
        }


        [HttpGet("getall")]
        public async Task<MessageResponseViewModel<List<EmployeeViewModel>>> GetAllEmployee()
        {

            var result = await _EmployeeService.GetAllEmployees();

            return result;
        }

        [HttpPost("get/code")]
        public async Task<MessageResponseViewModel<EmployeeViewModel>> GetEmployee(EmployeeViewModel employ)
        {

            var result = await _EmployeeService.GetEmployee(employ.Barcode);

            return result;
        }

        [HttpGet("Stuff")]
        public async Task<MessageResponseViewModel<AllStuffVM>> GetStuffAsig()
        {
            var result = await _EmployeeService.GetStuffAsig();

            return result;
        }

       

        [HttpPost("register/")]
        public async Task<MessageResponseViewModel<EmployeeRegisterViewModel>> RegisterEmployee(EmployeeRegisterViewModel EmployeeRegister)
        {
            var result = await _EmployeeService.RegisterEmployee(EmployeeRegister);

            return result;
        }

    }
}
