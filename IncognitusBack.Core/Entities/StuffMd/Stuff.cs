using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IncognitusBack.Core.Entities
{
    public class Stuff : BaseEntity
    {
        public string Name { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public StuffType StuffType { get; set; }
    }
}
