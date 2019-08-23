using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class RosterC : BaseEntity
    {
        public int Day { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Break { get; set; }
        public string Precint { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
        public int ShiftNum { get; set; }
        public string LabourType { get; set; }
        public string Employee { get; set; }
        public string Payroll { get; set; }
        public bool LookedIn { get; set; }
        [DefaultValue("Insert 0 Here")]
        public bool Confirm { get; set; }
        public string EventName { get; set; }
    }
}
