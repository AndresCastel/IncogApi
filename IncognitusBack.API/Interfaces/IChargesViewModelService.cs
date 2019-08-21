using IncognitusBack.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Interfaces
{
    public interface IChargesViewModelService
    {
        Task<MessageResponseViewModel> ChageRoster(List<RosterCViewModel> lstRoster);
    }
}
