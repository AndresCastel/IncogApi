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
        public int Employee_RegisterId { get; set; }
        [ForeignKey("Employee_RegisterId")]
        public Employee_Register Employee_Register { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
    }
}
