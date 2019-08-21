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
        public DateTime Sign_In { get; set; }
        public DateTime Sign_Off { get; set; }
        public int Break { get; set; }
    }
}
