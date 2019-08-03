using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.ViewModels
{
    public class StuffAssignViewModel : ViewModelBase
    {
        public int StuffId { get; set; }
        public int Employee_RegisterId { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
    }
}
