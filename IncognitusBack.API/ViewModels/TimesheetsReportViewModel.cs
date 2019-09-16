using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.ViewModels
{
    public class TimesheetsReportViewModel
    {
        public string Payroll { get; set; }
        public string Employee { get; set; }
        public string LastName { get; set; }
        public DateTime Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Break { get; set; }
        public bool Active { get; set; }
        public string LabourType { get; set; }
        public string Precint { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
        public int Id { get; set; }
        public bool LookedIn { get; set; }
        public string EventName { get; set; }
        public bool Confirm { get; set; }
    }
}
