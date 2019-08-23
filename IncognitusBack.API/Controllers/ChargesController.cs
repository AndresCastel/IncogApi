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
    public class ChargesController : ControllerBase
    {
        private readonly IChargesViewModelService _ChargesService;

        public ChargesController(IChargesViewModelService ChargesService)
        {
            this._ChargesService = ChargesService;
        }

        [HttpPost("Roster/")]
        public async Task<MessageResponseViewModel<RosterWM>> ChageRoster(RosterWM Roster)
        {
            var result = await _ChargesService.ChageRoster(Roster.lstRoster);

            return result;
        }
    }
}