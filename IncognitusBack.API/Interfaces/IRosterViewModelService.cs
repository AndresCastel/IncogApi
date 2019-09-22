using IncognitusBack.API.ViewModels;
using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.Interfaces
{
    public interface IRosterViewModelService
    {
        Task<List<RosterCViewModel>> GetAllRoster(FilterParametersRoster filter);

        Task<MessageResponseViewModel<bool>> SetRoster(RosterCViewModel roster);
    }
}
