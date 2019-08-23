using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.ViewModels
{
    public class TimesheetsReportViewModel
    {
        public int Id { get; set; }
        public string Payroll { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Break { get; set; }
    }
}
