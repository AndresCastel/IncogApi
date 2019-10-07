using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Barcode { get; set; }
        public string Payroll { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; }
        public bool Active { get; set; }
        public List<StuffAssignViewModel> lstStuffAssig { get; set; }
    }
}
