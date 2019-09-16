using System;
using System.Collections.Generic;
using System.Text;
using IncognitusBack.Core.Entities;

namespace IncognitusBack.Core.Specifications.EmployeeSP
{
    public class TimeSheetReportSpecification : BaseSpecification<TimesheetsReport>
    {
        //public TimeSheetReportSpecification()
        //    : base()
        //{
        //}
        public TimeSheetReportSpecification(string Name)
            : base(o => o.Employee == Name)
        {
        }

        //public EmployeeRegisterSpecification(bool Active)
        //   : base(o => o.Active == Active)
        //{
        //}
    }
}