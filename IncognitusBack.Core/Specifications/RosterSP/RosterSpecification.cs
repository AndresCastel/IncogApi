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
            : base(o => o.Payroll.TrimStart(new Char[] { '0' }) == Payroll.TrimStart(new Char[] { '0' }) && o.Date == dateTime.Date )
        {
        }

        public RosterSpecification(DateTime Date)
           : base(o => o.Date == Date)
        {

        }

        public RosterSpecification(bool Timesheet, string employee, DateTime Date)
            : base(o => o.Employee == employee && o.Date == Date.Date)
        {
        }

        public RosterSpecification(DateTime DateFrom, DateTime DateTo)
            : base(o => o.Date >= DateFrom.Date && o.Date <= DateTo.Date)
        {
        }

    }
}
