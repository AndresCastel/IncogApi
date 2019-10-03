using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
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
    public class EmployeeViewModelService : IEmployeeViewModelService
    {
        private readonly IAsyncRepository<RosterC> _RosterRepository;
        private readonly IAsyncRepository<Employee> _employeeRepository;
        private readonly IAsyncRepository<Employee_Register> _employee_RegisterRepository;
        private readonly IAsyncRepository<Stuff_Assign> _stuffAssingRepository;
        private readonly IAsyncRepositoryNormal<TimesheetsReport> _TimeRepository;
        public EmployeeViewModelService(IAsyncRepository<Employee> employeeRepository, IAsyncRepository<Employee_Register> employee_RegisterRepository,
           IAsyncRepository<Stuff_Assign> StuffAssingRepository, IAsyncRepository<RosterC> RosterRepository)
        {
           // _TimeRepository = TimeRepository;
            _RosterRepository = RosterRepository;
            _stuffAssingRepository = StuffAssingRepository;
            _employeeRepository = employeeRepository;
            _employee_RegisterRepository = employee_RegisterRepository;
        }

        public async Task<MessageResponseViewModel<AllStuffVM>> GetStuffAsig()
        {
            MessageResponseViewModel<AllStuffVM> ReturnMessage = new MessageResponseViewModel<AllStuffVM>();
            
            try
            {
                AllStuffVM AllStuff = new AllStuffVM();
                AllStuff.Stuff = new List<StuffAssignViewModel>();
                //Get All Stuff Assigned
                var lststuff_Assigns = await _stuffAssingRepository.ListAllAsync();
                if (lststuff_Assigns != null)
                {

                    foreach (var item in lststuff_Assigns)
                    {
                        StuffAssignViewModel stuffVM = new StuffAssignViewModel();
                        stuffVM = CreateViewModelFromStuff(item);
                        AllStuff.Stuff.Add(stuffVM);
                    }

                }
                ReturnMessage.Succesfull = true;
                ReturnMessage.Data = AllStuff;
            }
            catch (Exception ex)
            {

                ReturnMessage.Succesfull = false;
                ReturnMessage.Data = null;
            }
            
            return ReturnMessage;
        }

        public async Task<MessageResponseViewModel<EmployeeVsRosterVM>> GetEmployeebyBarcode(EmployeeRegisterViewModel employee)
        {
            MessageResponseViewModel<EmployeeVsRosterVM> ReturnMessage = new MessageResponseViewModel<EmployeeVsRosterVM>();
            try
            {
               

                EmployeeVsRosterVM employRegister = new EmployeeVsRosterVM();
                //Get Employee
                var EmployeeSpec = new EmployeeSpecification(employee.Employee.Barcode);
                var employ = (await _employeeRepository.ListAsync(EmployeeSpec)).FirstOrDefault();

                if (employ != null)
                {
                    //Get if that employ has 
                    var EmployeeRegisterSpec = new EmployeeRegisterSpecification(employ.Id, true);
                    var employeeregister = (await _employee_RegisterRepository.ListAsync(EmployeeRegisterSpec)).FirstOrDefault();
                    RosterC employroster = new RosterC();
                    //Get Roster by employ
                    if(employeeregister!=null)
                    {
                        if(employeeregister.Type_RegisterId==1 && employeeregister.StartTime!= null && employeeregister.EndTime== null)
                        {
                            var EmployeeRoster = new RosterSpecification(employ.Payroll, employeeregister.Day, employeeregister.RosterId);
                            employroster = (await _RosterRepository.ListAsync(EmployeeRoster)).FirstOrDefault();
                        }
                        else
                        {
                            var EmployeeRoster = new RosterSpecification(employ.Payroll, General.SplitCreateDate(employee.Day));
                            employroster = (await _RosterRepository.ListAsync(EmployeeRoster)).FirstOrDefault();
                        }
                    }
                    else
                    {
                        var EmployeeRoster = new RosterSpecification(employ.Payroll, General.SplitCreateDate(employee.Day));
                        employroster = (await _RosterRepository.ListAsync(EmployeeRoster)).FirstOrDefault();
                    }
                   
                        if (employroster == null)
                    {
                        //if is true he is not an admin
                        if (employ.RolId != 1)
                        {
                            if (employeeregister != null)
                            {
                                if (employeeregister.Type_RegisterId == 2 && employeeregister.EndTime == null)
                                {
                                    ReturnMessage.Succesfull = false;
                                    ReturnMessage.Message = employ.Name + " " + employ.LastName + " Does not have any shift today";
                                    return ReturnMessage;
                                }
                                else
                                {
                                    var EmployeeRosterSignIn = new RosterSpecification(employ.Payroll, employeeregister.Day);
                                    var employrosterSignIn = (await _RosterRepository.ListAsync(EmployeeRosterSignIn)).FirstOrDefault();
                                    employroster = employrosterSignIn;
                                }
                            }
                            else
                            {
                                ReturnMessage.Succesfull = false;
                                ReturnMessage.Message = employ.Name + " " + employ.LastName + " Does not have any shift today" + employee.Day;
                                return ReturnMessage;

                            }

                        }
                        else
                        {
                            if (employeeregister != null)
                            {
                                var EmployeeRosterSignIn = new RosterSpecification(employ.Payroll, employeeregister.Day);
                                var employrosterSignIn = (await _RosterRepository.ListAsync(EmployeeRosterSignIn)).FirstOrDefault();
                                employroster = employrosterSignIn;
                            }
                        }
                    }

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

                    if (employroster != null)
                    {
                        employRegister.employRoster = CreateViewModelFromRoster(employroster);
                    }




                    ReturnMessage.Data = employRegister;
                    ReturnMessage.Succesfull = true;

                }
                else
                {
                    ReturnMessage.Succesfull = false;
                    ReturnMessage.Message = "This User does not exist in the DataBase";
                }
            }
            catch (Exception ex)
            {
                 ReturnMessage.Succesfull=false;
                ReturnMessage.Message = ex.Message + ex.InnerException;
                return ReturnMessage;
            }
            
            return ReturnMessage;
        }

        public async Task<MessageResponseViewModel<List<EmployeeViewModel>>> GetAllEmployees()
        {
            MessageResponseViewModel<List<EmployeeViewModel>> ReturnMessage = new MessageResponseViewModel<List<EmployeeViewModel>>();
            try
            {

                List<EmployeeViewModel> employRegister = new List<EmployeeViewModel>();
                //Get Employee

                var employ = await _employeeRepository.ListAllAsync();

                if (employ != null)
                {
                    foreach (var item in employ)
                    {
                        employRegister.Add(CreateViewModelFromEmployee(item));
                    }

                    ReturnMessage.Data = employRegister;
                    ReturnMessage.Succesfull = true;
                }
                else
                {
                    ReturnMessage.Succesfull = false;
                    ReturnMessage.Message = "There are not employees in the Database";
                    return ReturnMessage;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage.Succesfull = false;
                ReturnMessage.Message = ex.Message + ex.InnerException;
                return ReturnMessage;
            }
            return ReturnMessage;
        }

        public async Task<MessageResponseViewModel<EmployeeViewModel>> GetEmployee(string Barcode)
        {
            MessageResponseViewModel<EmployeeViewModel> ReturnMessage = new MessageResponseViewModel<EmployeeViewModel>();
            try
            {

                var EmployeeSpec = new EmployeeSpecification(Barcode);
                var employ = (await _employeeRepository.ListAsync(EmployeeSpec)).FirstOrDefault();

                if (employ != null)
                {
                    ReturnMessage.Data = CreateViewModelFromEmployee(employ);
                    ReturnMessage.Succesfull = true;
                }
                else
                {
                    ReturnMessage.Succesfull = false;
                    ReturnMessage.Message = "This employee does not exist in the Database";
                    return ReturnMessage;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage.Succesfull = false;
                ReturnMessage.Message = ex.Message + ex.InnerException;
                return ReturnMessage;
            }
            return ReturnMessage;
        }

        public async Task<MessageResponseViewModel<EmployeeRegisterViewModel>> RegisterEmployee(EmployeeRegisterViewModel Register)
        {
            MessageResponseViewModel<EmployeeRegisterViewModel> resultMessage = new MessageResponseViewModel<EmployeeRegisterViewModel>();
            try
            {
                
                //Validate if there is a register previously
                var Registerespe = new EmployeeRegisterSpecification(Register.EmployeeId, true);
                var UltimateRegister = (await _employee_RegisterRepository.ListAsync(Registerespe)).FirstOrDefault();


                if (UltimateRegister == null)
                {
                    Employee_Register employee = new Employee_Register()
                    {
                        EmployeeId = Register.EmployeeId,
                        Active = Register.Active,
                        Day = General.SplitCreateDate(Register.Day),
                        Payroll = Register.Payroll,
                        Type_RegisterId = Register.Type_RegisterId,
                        StartTime = Register.StartTime,
                        RosterId = Register.RosterId
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
                    switch (Register.Type_RegisterId)
                    {
                        //SignIn
                        case 1:
                            UltimateRegister.Type_RegisterId = Register.Type_RegisterId;
                            UltimateRegister.StartTime = Register.StartTime;
                            UltimateRegister.Day = General.SplitCreateDate(Register.Day);
                            UltimateRegister.Payroll = Register.Payroll;
                            UltimateRegister.Active = Register.Active;
                            UltimateRegister.RosterId = Register.RosterId;
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
                                    Employee_RegisterId = UltimateRegister.Id
                                ,
                                    StuffId = item.StuffId,
                                    Quantity = item.Quantity
                                };
                                await _stuffAssingRepository.AddAsync(stuffassg);
                            }
                            break;
                        //signOff
                        case 2:
                            //Update Last register
                            UltimateRegister.Type_RegisterId = Register.Type_RegisterId;
                            UltimateRegister.Break = Register.Break;
                            UltimateRegister.Payroll = Register.Payroll;
                            UltimateRegister.EndTime = Register.EndTime;
                           // UltimateRegister.RosterId = Register.RosterId;
                            UltimateRegister.Active = false;
                            await _employee_RegisterRepository.UpdateAsync(UltimateRegister);
                            //Create a new Register
                            Employee_Register employee = new Employee_Register()
                            {
                                EmployeeId = Register.EmployeeId,
                                Active = true,
                                Day = General.SplitCreateDate(Register.Day),
                                Payroll = Register.Payroll,
                                Type_RegisterId = Register.Type_RegisterId,
                                RosterId = Register.RosterId
                    };
                            employee = await _employee_RegisterRepository.AddAsync(employee);
                            var StuffAsignOff = new StuffAssignSpecification(UltimateRegister.Id);
                            var stuffassingoff = (await _stuffAssingRepository.ListAsync(StuffAsignOff));
                            foreach (var item in stuffassingoff)
                            {
                                await _stuffAssingRepository.DeleteAsync(item);
                            }
                            foreach (var item in Register.lstStuffAssig)
                            {
                                Stuff_Assign stuffassg = new Stuff_Assign()
                                {
                                    Active = item.Active,
                                    Employee_RegisterId = employee.Id
                                ,
                                    StuffId = item.StuffId,
                                    Quantity = item.Quantity
                                };
                                await _stuffAssingRepository.AddAsync(stuffassg);
                            }
                            break;

                        //Equipment
                        case 3:
                            var StuffAsignEquip = new StuffAssignSpecification(UltimateRegister.Id);
                            var stuffassingEquip = (await _stuffAssingRepository.ListAsync(StuffAsignEquip));
                            foreach (var item in stuffassingEquip)
                            {
                                await _stuffAssingRepository.DeleteAsync(item);
                            }
                            foreach (var item in Register.lstStuffAssig)
                            {
                                Stuff_Assign stuffassg = new Stuff_Assign()
                                {
                                    Active = item.Active,
                                    Employee_RegisterId = UltimateRegister.Id
                                ,
                                    StuffId = item.StuffId,
                                    Quantity = item.Quantity
                                };
                                await _stuffAssingRepository.AddAsync(stuffassg);
                            }
                            break;
                        default:
                            break;
                    }

                }


                resultMessage.Succesfull = true;
                return resultMessage;
            }
            catch (Exception ex)
            {
                resultMessage.Succesfull = false;
                resultMessage.Message = ex.Message + ex.InnerException;
                return resultMessage;
            }


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

        private EmployeeRegisterViewModel CreateViewModelFromEmployeeRegister(Employee_Register employeereg)
        {
            var viewModel = new EmployeeRegisterViewModel();
            viewModel.Id = employeereg.Id;
            viewModel.StartTime = employeereg.StartTime;
            viewModel.EndTime = employeereg.EndTime;
            viewModel.Payroll = employeereg.Payroll;
            viewModel.Day = employeereg.Day.ToShortTimeString();
            viewModel.Type_RegisterId = employeereg.Type_RegisterId;
            viewModel.Active = employeereg.Active;
            viewModel.RosterId = employeereg.RosterId;
            return viewModel;
        }

        private Employee_Register  CreateEmployeeRegisterFromViewModel(EmployeeRegisterViewModel employeereg)
        {
            var viewModel = new Employee_Register();
            viewModel.Id = employeereg.Id;
            viewModel.StartTime = employeereg.StartTime;
            viewModel.EndTime = employeereg.EndTime;
            viewModel.Day = General.SplitCreateDate(employeereg.Day);
            viewModel.Payroll = employeereg.Payroll;
            viewModel.Type_RegisterId = employeereg.Type_RegisterId;
            viewModel.Active = employeereg.Active;
            viewModel.RosterId = employeereg.RosterId;
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

        private TimesheetsReportViewModel CreateViewModelFromTimesheets(TimesheetsReport Timesh)
        {
            var viewModel = new TimesheetsReportViewModel();
            viewModel.Area = Timesh.Area;
            viewModel.Employee = Timesh.Employee;
           // viewModel.LastName = Timesh.LastName;
            viewModel.Break = Timesh.Break;
            viewModel.Payroll = Timesh.Payroll;
            viewModel.StartTime = Timesh.StartTime;
            viewModel.EndTime = Timesh.EndTime;
            viewModel.Day = Timesh.Day.ToShortTimeString();
            viewModel.LabourType = Timesh.LabourType;
            viewModel.Precint = Timesh.Precint;
            viewModel.Zone = Timesh.Zone;
            viewModel.Id = Timesh.Id;
            viewModel.LookedIn = Timesh.LookedIn;
            viewModel.EventName = Timesh.EventName;
            viewModel.Confirm = Timesh.Confirm;
            return viewModel;
        }

        //public Task<TimesheetsReport> GetTimesheetsAsync()
        //{
        //    return _TimeRepository.GetTimesheetsAsync();
        //}
    }
}
