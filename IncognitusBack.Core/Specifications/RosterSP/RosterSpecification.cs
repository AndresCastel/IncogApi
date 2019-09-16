using IncognitusBack.Core.Entities;
using IncognitusBack.Core.Entities.Roster;
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

        public RosterSpecification(string Payroll)
            : base(o => o.Payroll == Payroll)
        {
        }

        public RosterSpecification(DateTime DateStart, DateTime DateEnd)
            : base(o => o.Date.Date >= DateStart.Date && o.Date.Date<= DateEnd.Date )
        {
        }

    }
}
