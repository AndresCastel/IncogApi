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
    public class EmployeeViewModelService: IEmployeeViewModelService
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
            EmployeeRegisterViewModel employRegister = new EmployeeRegisterViewModel();
            //Get Employee
            var EmployeeSpec = new EmployeeSpecification(Barcode);
            var employ = (await _employeeRepository.ListAsync(EmployeeSpec)).FirstOrDefault();
            
            //Get Register Employee
            var EmployeeRegisterSpec = new EmployeeRegisterSpecification(employ.Id);
            var employeeregister = (await _employee_RegisterRepository.ListAsync(EmployeeRegisterSpec)).FirstOrDefault();
            employRegister = CreateViewModelFromEmployeeRegister(employeeregister);
            employRegister.Employee = CreateViewModelFromEmployee(employ);
            //Get StuffAssign
            var StuffAsign = new StuffAssignSpecification(employeeregister.Id);
            var stuffassing = (await _stuffAssingRepository.ListAsync(StuffAsign));
            List<StuffAssignViewModel> lst = new List<StuffAssignViewModel>();

            foreach (var item in stuffassing)
            {
                lst.Add(CreateViewModelFromStuff(item));
            }

            employRegister.lstStuffAssig = lst;

            return employRegister;
        }

        public async Task<MessageResponseViewModel> RegisterEmployee(EmployeeRegisterViewModel Register)
        {
            MessageResponseViewModel resultMessage = new MessageResponseViewModel();
            //Validate if there is a register previously
            var Registerespe = new EmployeeRegisterSpecification(Register.EmployeeId);
            var employregister = (await _employee_RegisterRepository.ListAsync(Registerespe)).FirstOrDefault();

            if (employregister == null)
            {
                if (Register.Type_RegisterId == 1)
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
                    resultMessage.Succesfull = false;
                    resultMessage.Message = "You have not signed In";
                    return resultMessage;
                }
            }
            else
            {
                if(Register.Type_RegisterId == employregister.Type_RegisterId)
                {
                    if (Register.Type_RegisterId == 1)
                    {
                        resultMessage.Succesfull = false;
                        resultMessage.Message = "You already did signed In : " + employregister.SignIn.ToString();
                        return resultMessage;
                    }
                    if (Register.Type_RegisterId == 2)
                    {
                        resultMessage.Succesfull = false;
                        resultMessage.Message = "You already did signed Off";
                        return resultMessage;
                    }
                    if (Register.Type_RegisterId == 3)
                    {
                        var StuffAsign = new StuffAssignSpecification(Register.Id);
                       // var employregister = (await _stuffAssingRepository.ListAsync(StuffAsign));
                    }
                }
                //employregister.
                await _employee_RegisterRepository.UpdateAsync(employregister);
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
    }
}
