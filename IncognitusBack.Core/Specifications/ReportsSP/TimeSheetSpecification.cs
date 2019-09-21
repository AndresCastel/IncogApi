using System;
using System.Collections.Generic;
using System.Text;
using IncognitusBack.Core.Entities;

namespace IncognitusBack.Core.Specifications
{
    public class TimeSheetSpecification : BaseSpecification<TimesheetsReport>
    {
        public TimeSheetSpecification(DateTime Date)
            : base(o => o.StartTime!=null && o.Day == Date)
        {
            
        }

        public TimeSheetSpecification(bool Timesheet, string employee, DateTime Date)
            : base(o => o.Active == Timesheet && o.Employee.Contains(employee) && o.Day == Date)
        {

        }
        public TimeSheetSpecification(bool Timesheet, DateTime DateFrom, DateTime DateTo)
           : base(o => o.Active == Timesheet && o.Day >= DateFrom && o.Day <=  DateTo)
        {

        }

        public TimeSheetSpecification(int id)
           : base(o => o.Id == id)
        {

        }

    }
}
