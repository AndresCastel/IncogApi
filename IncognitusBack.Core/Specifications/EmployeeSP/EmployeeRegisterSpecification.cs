using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncognitusBack.Core.Specifications
{
    public class EmployeeRegisterSpecification : BaseSpecification<Employee_Register>
    {
        public EmployeeRegisterSpecification(int EmployeerId)
            : base(o => o.EmployeeId == EmployeerId)
        {
        }
    }
}
