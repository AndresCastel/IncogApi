using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Specifications;
using IncognitusBack.Core.Specifications.RosterSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Services
{
    public class EmployeeViewModelService : IEmployeeViewModelService
    {
        private readonly IAsyncRepository<RosterC> _RosterRepository;
        private readonly IAsyncRepository<Employee> _employeeRepository;
        private readonly IAsyncRepository<Employee_Register> _employee_RegisterRepository;
        private readonly IAsyncRepository<Stuff_Assign> _stuffAssingRepository;
        public EmployeeViewModelService(IAsyncRepository<Employee> employeeRepository, IAsyncRepository<Employee_Register> employee_RegisterRepository,
           IAsyncRepository<Stuff_Assign> StuffAssingRepository, IAsyncRepository<RosterC> RosterRepository)
        {
            _RosterRepository = RosterRepository;
            _stuffAssingRepository = StuffAssingRepository;
            _employeeRepository = employeeRepository;
            _employee_RegisterRepository = employee_RegisterRepository;
        }
        public async Task<MessageResponseViewModel<EmployeeVsRosterVM>> GetEmployeebyBarcode(string Barcode)
        {
            MessageResponseViewModel<EmployeeVsRosterVM> ReturnMessage = new MessageResponseViewModel<EmployeeVsRosterVM>();

            EmployeeVsRosterVM employRegister = new EmployeeVsRosterVM();
            //Get Employee
            var EmployeeSpec = new EmployeeSpecification(Barcode);
            var employ = (await _employeeRepository.ListAsync(EmployeeSpec)).FirstOrDefault();
            
            if (employ != null)
            {
               
                //Get Roster by employ
                var EmployeeRoster = new RosterSpecification(employ.Payroll, DateTime.Now.Date);
                var employroster = (await _RosterRepository.ListAsync(EmployeeRoster)).FirstOrDefault();
                if (employroster == null)
                {
                    if (employ.RolId != 1)
                    {
                        ReturnMessage.Succesfull = false;
                        ReturnMessage.Message = employ.Name + " " + employ.LastName + " Does not have any shift today";
                        return ReturnMessage;
                    }                   
                }
                //Get if that employ has 
                var EmployeeRegisterSpec = new EmployeeRegisterSpecification(employ.Id, true);
                var employeeregister = (await _employee_RegisterRepository.ListAsync(EmployeeRegisterSpec)).FirstOrDefault();
                if (employeeregister != null)
                {
                    employRegister.employregister = CreateViewModelFromEmployeeRegister(employeeregister);
                    var StuffAsign = new StuffAssignSpecification(employeeregister.Id);
                    var stuffassing = (await _stuffAssingRepository.ListAsync(StuffAsign));
                    List<StuffAssignViewModel> lst = new List<StuffAssignViewModel>();

                    foreach (var item in stuffassing)
                    {
                        lst.Add(CreateViewModelFromStuff(item));
                    }
                    employRegister.employregister.lstStuffAssig = lst;
                    employRegister.employregister.Employee = CreateViewModelFromEmployee(employ);
                }
                else
                {
                    employRegister.employregister = new EmployeeRegisterViewModel();
                    employRegister.employregister.Employee = CreateViewModelFromEmployee(employ);
                }
                
                employRegister.employRoster = CreateViewModelFromRoster(employroster);
                
                
                ReturnMessage.Data = employRegister;
                ReturnMessage.Succesfull = true;

            }
            else
            {
                if (employ.Rol.Id != 1)
                {
                    ReturnMessage.Succesfull = false;
                    ReturnMessage.Message = "This User does not exist in the DataBase";
                }
            }
            
            return ReturnMessage;
        }

        //public async Task<bool> GetEmployeesFromExcel()
        //{
        //    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //    Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"sandbox_test.xlsx");
        //    Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
        //}

        public async Task<MessageResponseViewModel<EmployeeRegisterViewModel>> RegisterEmployee(EmployeeRegisterViewModel Register)
        {
            MessageResponseViewModel<EmployeeRegisterViewModel> resultMessage = new MessageResponseViewModel<EmployeeRegisterViewModel>();
            //Validate if there is a register previously
            var Registerespe = new EmployeeRegisterSpecification(Register.EmployeeId, true);
            var UltimateRegister = (await _employee_RegisterRepository.ListAsync(Registerespe)).FirstOrDefault();
           

            if (UltimateRegister == null)
            {
                Employee_Register employee = new Employee_Register()
                {
                    EmployeeId = Register.EmployeeId,
                    Active = Register.Active,     
                    Day = Register.Day,
                    Type_RegisterId = Register.Type_RegisterId,
                    StartTime = Register.StartTime
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
                    if (Register.Day != DateTime.MinValue) { UltimateRegister.StartTime = Register.StartTime; }
                    if (Register.Day != DateTime.MinValue) { UltimateRegister.EndTime = Register.EndTime; }                       
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
                        //TODO: Andres create funtion to convert hours to military hours whitout : points
                       // StartTime = DateTime.MinValue
                    };
                    employeenew = await _employee_RegisterRepository.AddAsync(employeenew);
                    UltimateRegister.Day = Register.Day.Date;
                    UltimateRegister.Break = Register.Break;
                    UltimateRegister.EndTime = Register.EndTime;
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
            viewModel.Payroll = employee.Payroll;
            viewModel.Active = employee.Active;
            return viewModel;
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

        private EmployeeRegisterViewModel CreateViewModelFromEmployeeRegister(Employee_Register employeereg)
        {
            var viewModel = new EmployeeRegisterViewModel();
            viewModel.Id = employeereg.Id;
            viewModel.StartTime = employeereg.StartTime;
            viewModel.EndTime = employeereg.EndTime;
            viewModel.Day = employeereg.Day;
            viewModel.Type_RegisterId = employeereg.Type_RegisterId;
            viewModel.Active = employeereg.Active;
            return viewModel;
        }

        private Employee_Register  CreateEmployeeRegisterFromViewModel(EmployeeRegisterViewModel employeereg)
        {
            var viewModel = new Employee_Register();
            viewModel.Id = employeereg.Id;
            viewModel.StartTime = employeereg.StartTime;
            viewModel.EndTime = employeereg.EndTime;
            viewModel.Day = employeereg.Day;            
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
                        time.StartTime = item.StartTime;
                        time.EndTime = item.EndTime;
                        time.Day = item.Day;
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
            viewModel.StartTime = Timesh.StartTime;
            viewModel.EndTime = Timesh.EndTime;
            viewModel.Day = Timesh.Day;
            return viewModel;
        }
    }
}
