using System;
using System.Collections.Generic;
using System.Text;
using IncognitusBack.Core.Entities;

namespace IncognitusBack.Core.Specifications
{
    public class TimeSheetSpecification : BaseSpecification<TimesheetsReport>
    {
        public TimeSheetSpecification()
            : base(o => o.StartTime!=null)
        {
            
        }

        public TimeSheetSpecification(bool Timesheet, string employee)
            : base(o => o.Active == Timesheet && o.Employee.Contains(employee))
        {

        }

    }
}
