using System;
using System.Collections.Generic;
using System.Text;
using IncognitusBack.Core.Entities;

namespace IncognitusBack.Core.Specifications
{
    public class TimeSheetSpecification : BaseSpecification<TimesheetsReport>
    {
        public TimeSheetSpecification(bool Timesheet)
            : base(o => o.Active == Timesheet)
        {
            
        }

    }
}
