using System;
using System.Collections.Generic;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class WhiteboardActual : BaseNotKey
    {
        public string EventName { get; set; }
        public string Precint { get; set; }
        public string Zone { get; set; }
        public string Payroll { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Area { get; set; }
        public string LabourType { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RosterStart { get; set; }
        public string RosterEnd { get; set; }
        public DateTime Date { get; set; }
    }
}
