using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncognitusBack.Core.Specifications
{
    public class StuffAssignSpecification : BaseSpecification<Stuff_Assign>
    {
        public StuffAssignSpecification(int EmployeeId)
            : base(o => o.EmployeeId == EmployeeId)
        {
        }

    }
}