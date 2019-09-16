using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class Employee_Register : BaseEntity
    {
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employe { get; set; }
        public int Type_RegisterId { get; set; }
        [ForeignKey("Type_RegisterId")]
        public Type_Register Type_Register { get; set; }       
        public DateTime Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Payroll { get; set; }
        public int Break { get; set; }
        [DefaultValue("Insert 1 Here")]
        public bool Active { get; set; }
        public int RosterId { get; set; }
        [ForeignKey("RosterId")]
        public RosterC Roster { get; set; }
    }
}
