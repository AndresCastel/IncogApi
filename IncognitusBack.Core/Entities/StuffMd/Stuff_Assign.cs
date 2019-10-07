using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class Stuff_Assign : BaseEntity
    {
        public int StuffId { get; set; }
        [ForeignKey("StuffId")]
        public Stuff Stuff { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
    }
}
