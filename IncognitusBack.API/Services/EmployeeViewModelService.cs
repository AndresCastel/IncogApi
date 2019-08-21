using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Services
{
    public class EmployeeViewModelService : IEmployeeViewModelService
    {
        private readonly IAsyncRepository<Employee> _employeeRepository;
        private readonly IAsyncRepository<Employee_Register> _employee_RegisterRepository;
        private readonly IAsyncRepository<Stuff_Assign> _stuffAssingRepository;
        public EmployeeViewModelService(IAsyncRepository<Employee> employeeRepository, IAsyncRepository<Employee_Register> employee_RegisterRepository,
           IAsyncRepository<Stuff_Assign> StuffAssingRepository)
        {
            _stuffAssingRepository = StuffAssingRepository;
            _employeeRepository = employeeRepository;
            _employee_RegisterRepository = employee_RegisterRepository;
        }
        public async Task<EmployeeRegisterViewModel> GetEmployeebyBarcode(string Barcode)
        {
            //var abc = await  _TimeRepository.ListAllAsync();
            EmployeeRegisterViewModel employRegister = new EmployeeRegisterViewModel();
            //Get Employee
            var EmployeeSpec = new EmployeeSpecification(Barcode);
            var employ = (await _employeeRepository.ListAsync(EmployeeSpec)).FirstOrDefault();

            //Get Register Employee
            if (employ != null)
            {
                var EmployeeRegisterSpec = new EmployeeRegisterSpecification(employ.Id, true);
                var employeeregister = (await _employee_RegisterRepository.ListAsync(EmployeeRegisterSpec)).FirstOrDefault();
                if (employeeregister != null)
                {
                    employRegister = CreateViewModelFromEmployeeRegister(employeeregister);
                    var StuffAsign = new StuffAssignSpecification(employeeregister.Id);
                    var stuffassing = (await _stuffAssingRepository.ListAsync(StuffAsign));
                    List<StuffAssignViewModel> lst = new List<StuffAssignViewModel>();

                    foreach (var item in stuffassing)
                    {
                        lst.Add(CreateViewModelFromStuff(item));
                    }

                    employRegister.lstStuffAssig = lst;
                }
                employRegister.Employee = CreateViewModelFromEmployee(employ);
                               
            }

            return employRegister;
        }

        //public async Task<bool> GetEmployeesFromExcel()
        //{
        //    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //    Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"sandbox_test.xlsx");
        //    Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
        //}

        public async Task<MessageResponseViewModel> RegisterEmployee(EmployeeRegisterViewModel Register)
        {
            MessageResponseViewModel resultMessage = new MessageResponseViewModel();
            //Validate if there is a register previously
            var Registerespe = new EmployeeRegisterSpecification(Register.EmployeeId, true);
            var UltimateRegister = (await _employee_RegisterRepository.ListAsync(Registerespe)).FirstOrDefault();

            if (UltimateRegister == null)
            {
                Employee_Register employee = new Employee_Register()
                {
                    EmployeeId = Register.EmployeeId,
                    Active = Register.Active
                ,
                    Type_RegisterId = Register.Type_RegisterId,
                    SignIn = Register.SignIn
                };
                employee = await _employee_RegisterRepository.AddAsync(employee);

                foreach (var item in Register.lstStuffAssig)
                {
                    Stuff_Assign stuffassing = new Stuff_Assign()
                    {
                        Active = item.Active,
                        Employee_RegisterId = employee.Id
                    ,
                        StuffId = item.StuffId,
                        Quantity = item.Quantity
                    };
                    await _stuffAssingRepository.AddAsync(stuffassing);
                }
            }
            else
            {
                if (Register.Active)
                {
                    if (Register.SignIn != DateTime.MinValue) { UltimateRegister.SignIn = Register.SignIn; }
                    if (Register.SignOff != DateTime.MinValue) { UltimateRegister.Signoff = Register.SignOff; }                       
                    UltimateRegister.Active = true; 
                    UltimateRegister.Type_RegisterId = Register.Type_RegisterId;
                    await _employee_RegisterRepository.UpdateAsync(UltimateRegister);
                    var StuffAsign = new StuffAssignSpecification(UltimateRegister.Id);
                    var stuffassing = (await _stuffAssingRepository.ListAsync(StuffAsign));
                    foreach (var item in stuffassing)
                    {
                        await _stuffAssingRepository.DeleteAsync(item);
                    }

                    foreach (var item in Register.lstStuffAssig)
                    {
                        Stuff_Assign stuffassg = new Stuff_Assign()
                        {
                            Active = item.Active,
                            Employee_RegisterId = Register.Id
                        ,
                            StuffId = item.StuffId,
                            Quantity = item.Quantity
                        };
                        await _stuffAssingRepository.AddAsync(stuffassg);
                    }

                }
                else
                {
                    Employee_Register employeenew = new Employee_Register()
                    {
                        EmployeeId = Register.EmployeeId,
                        Active = true
               ,
                        Type_RegisterId = 1,
                        SignIn = DateTime.MinValue
                    };
                    employeenew = await _employee_RegisterRepository.AddAsync(employeenew);
                    UltimateRegister.Break = Register.Break;
                    UltimateRegister.Signoff = Register.SignOff;
                    UltimateRegister.Type_RegisterId = 2;
                    UltimateRegister.Active = false;
                    await _employee_RegisterRepository.UpdateAsync(UltimateRegister);
                    var StuffAsign = new StuffAssignSpecification(UltimateRegister.Id);
                    var stuffassing = (await _stuffAssingRepository.ListAsync(StuffAsign));
                    foreach (var item in stuffassing)
                    {
                        await _stuffAssingRepository.DeleteAsync(item);
                    }

                    foreach (var item in Register.lstStuffAssig)
                    {
                        Stuff_Assign stuffassg = new Stuff_Assign()
                        {
                            Active = item.Active,
                            Employee_RegisterId = employeenew.Id
                        ,
                            StuffId = item.StuffId,
                            Quantity = item.Quantity
                        };
                        await _stuffAssingRepository.AddAsync(stuffassg);
                    }

                }
            }


            resultMessage.Succesfull = true;
            return resultMessage;


        }


        private EmployeeViewModel CreateViewModelFromEmployee(Employee employee)
        {
            var viewModel = new EmployeeViewModel();
            viewModel.Id = employee.Id;
            viewModel.Name = employee.Name;
            viewModel.MiddleName = employee.MiddleName;
            viewModel.LastName = employee.LastName;
            viewModel.Email = employee.Email;
            viewModel.RolId = employee.RolId;
            viewModel.Active = employee.Active;
            return viewModel;
        }

        private EmployeeRegisterViewModel CreateViewModelFromEmployeeRegister(Employee_Register employeereg)
        {
            var viewModel = new EmployeeRegisterViewModel();
            viewModel.Id = employeereg.Id;
            viewModel.SignIn = employeereg.SignIn;
            viewModel.SignOff = employeereg.Signoff;
            viewModel.Type_RegisterId = employeereg.Type_RegisterId;
            viewModel.Active = employeereg.Active;
            return viewModel;
        }

        private Employee_Register  CreateEmployeeRegisterFromViewModel(EmployeeRegisterViewModel employeereg)
        {
            var viewModel = new Employee_Register();
            viewModel.Id = employeereg.Id;
            viewModel.SignIn = employeereg.SignIn;
            viewModel.Signoff = employeereg.SignOff;
            viewModel.Type_RegisterId = employeereg.Type_RegisterId;
            viewModel.Active = employeereg.Active;
            return viewModel;
        }

        private StuffAssignViewModel CreateViewModelFromStuff(Stuff_Assign stuff_Assign)
        {
            var viewModel = new StuffAssignViewModel();
            viewModel.Id = stuff_Assign.Id;
            viewModel.Active = stuff_Assign.Active;
            viewModel.Employee_RegisterId = stuff_Assign.Employee_RegisterId;
            viewModel.Quantity = stuff_Assign.Quantity;
            viewModel.StuffId = stuff_Assign.StuffId;
            return viewModel;
        }

        public async Task<List<TimesheetsReportViewModel>> GetEmployeesSignInOff()
        {
            List<TimesheetsReportViewModel> lst = new List<TimesheetsReportViewModel>();
            

            EmployeeRegisterViewModel employRegister = new EmployeeRegisterViewModel();
            //Get Employee
            var EmployeeRegisterSpec = new EmployeeRegisterSpecification(false);
            List<Employee_Register> lstemployReg = (await _employee_RegisterRepository.ListAsync(EmployeeRegisterSpec));

            //Get Employee
            List<Employee> lstemploy = (await _employeeRepository.ListAllAsync());

            foreach (Employee_Register item in lstemployReg)
            {
                TimesheetsReport time = new TimesheetsReport();
                foreach (Employee emp in lstemploy)
                {
                    if(item.EmployeeId == emp.Id)
                    {
                        time.Break = item.Break;
                        time.LastName = emp.LastName;
                        time.Name = emp.Name;
                        time.Payroll = emp.Payroll;
                        time.Sign_In = item.SignIn;
                        time.Sign_Off = item.Signoff;
                        lst.Add(CreateViewModelFromTimesheets(time));
                    }
                    
                }
            }

            return lst;
        }

        private TimesheetsReportViewModel CreateViewModelFromTimesheets(TimesheetsReport Timesh)
        {
            var viewModel = new TimesheetsReportViewModel();
            viewModel.Name = Timesh.Name;
            viewModel.LastName = Timesh.LastName;
            viewModel.Break = Timesh.Break;
            viewModel.Payroll = Timesh.Payroll;
            viewModel.Sign_In = Timesh.Sign_In;
            viewModel.Sign_Off = Timesh.Sign_Off;
            return viewModel;
        }
    }
}
