using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.ViewModels
{
    public class EmployeeVsRosterVM
    {
        public EmployeeRegisterViewModel employregister { get; set; }
        public RosterCViewModel employRoster { get; set; }
        public EmployeeViewModel employ { get; set; }
    }
}
