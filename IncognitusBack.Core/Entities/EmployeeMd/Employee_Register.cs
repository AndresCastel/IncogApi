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
        public DateTime TransactionDate { get; set; }
        [DefaultValue("Insert 1 Here")]
        public bool Active { get; set; }
    }
}
