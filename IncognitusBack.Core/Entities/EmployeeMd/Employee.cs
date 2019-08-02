using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string Name { get; set; }       
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Payroll { get; set; }
        public string Barcode { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public Rol Rol { get; set; }
        [DefaultValue("Insert 1 Here")]
        public bool Active { get; set; }

    }
}
