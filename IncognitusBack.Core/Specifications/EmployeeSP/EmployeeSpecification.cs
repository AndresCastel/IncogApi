using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncognitusBack.Core.Specifications
{
    public class EmployeeSpecification : BaseSpecification<Employee>
    {
        public EmployeeSpecification(string Barcode)
            : base(o => o.Barcode == Barcode)
        {
        }
    }
}
