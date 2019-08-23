using IncognitusBack.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Interfaces
{
    public interface IChargesViewModelService
    {
        Task<MessageResponseViewModel<RosterWM>> ChageRoster(List<RosterCViewModel> lstRoster);
    }
}
