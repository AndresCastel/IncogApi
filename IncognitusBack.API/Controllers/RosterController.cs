using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IncognitusBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RosterController : ControllerBase
    {
        private readonly IRosterViewModelService _RosterService;

        public RosterController(IRosterViewModelService RosterService)
        {
            this._RosterService = RosterService;
        }

        [HttpPost("get")]
        public async Task<MessageResponseViewModel<RosterWM>> GetAllRoster(FilterParametersRoster filter)
        {
            MessageResponseViewModel<RosterWM> roster = new MessageResponseViewModel<RosterWM>();
            RosterWM ros = new RosterWM();
            var result = await _RosterService.GetAllRoster(filter);
            ros.lstRoster = result;
            roster.Data = ros;
            roster.Succesfull = true;
            return roster;
        }

        [HttpPost("set")]
        public async Task<MessageResponseViewModel<bool>> SetRoster(RosterCViewModel roster)
        {
            MessageResponseViewModel<bool> rosterres = new MessageResponseViewModel<bool>();
            var result = await _RosterService.SetRoster(roster);
           
            rosterres = result;
            return rosterres;
        }

    }
}