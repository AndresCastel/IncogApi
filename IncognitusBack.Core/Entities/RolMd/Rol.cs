using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class Rol : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
