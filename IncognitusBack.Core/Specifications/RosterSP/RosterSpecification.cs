using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncognitusBack.Core.Specifications.RosterSP
{
    public class RosterSpecification : BaseSpecification<RosterC>
    {
        public RosterSpecification(string Payroll, DateTime dateTime)
            : base(o => o.Payroll == Payroll && o.Date.Date == dateTime.Date )
        {
        }
       


    }
}
