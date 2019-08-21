using System;
using System.Collections.Generic;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class TimesheetsReport: BaseNotKey
    {
        public string Payroll { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Sign_In { get; set; }
        public DateTime Sign_Off { get; set; }
        public int Break { get; set; }
    }
}
