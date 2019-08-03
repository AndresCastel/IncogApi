using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.ViewModels
{
    public class EmployeeRegisterViewModel : ViewModelBase
    {
        public int EmployeeId { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public int Type_RegisterId { get; set; }
        public DateTime SignIn { get; set; }
        public DateTime SignOff { get; set; }
        public int Break { get; set; }
        public bool Active { get; set; }
        public List<StuffAssignViewModel> lstStuffAssig { get; set; }
    }
}