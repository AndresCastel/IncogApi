using System;
using System.Collections.Generic;
using System.Text;

namespace IncognitusBack.Core.Entities.Roster
{
    public class FilterParametersRosterEntity
    {
        public int Day { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Employee { get; set; }
        public string Payroll { get; set; }
    }
}
