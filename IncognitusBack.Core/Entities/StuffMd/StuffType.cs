using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class StuffType : BaseEntity
    {
        [Required]
        public string NameStuff { get; set; }
    }
}
