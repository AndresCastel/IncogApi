using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class Type_Register : BaseEntity
    {
        [Required]
        public string Name { get; set; }

    }
}
