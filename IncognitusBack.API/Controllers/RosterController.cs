using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using IncognitusBack.API.Interfaces;
using IncognitusBack.API.ViewModels;
using IncogUtils;
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

        [HttpPost("test")]
        public async Task<MessageResponseViewModel<string>> Getdate(TestObjectVM test)
        {
            // string test2 ="";
            MessageResponseViewModel<string> roster = new MessageResponseViewModel<string>();
            try
            {
               
                string test2 = "Local: " + test.Datestring + " " + test.Date.ToString("g", CultureInfo.CreateSpecificCulture("en-AU")) + " " + test.Date.Date.ToString();
                // DateTime daatt = General.SplitCreateDate(test.Datestring);
                //General.CastStringtoDateTime
                //DateTime oDate = DateTime.ParseExact(test.Datestring, "yyyy-MM-dd", null);
                var usCulture = new System.Globalization.CultureInfo("en-AU");
                
                roster.Succesfull = true;
                roster.Message = test2 + "  Server: " + DateTime.Now.Date.ToString() + " ServerUTC " + DateTime.UtcNow.Date.ToString() ;
                return roster;
            }
            catch (Exception ex)
            {
                roster.Succesfull = false;
                roster.Message = ex.Message + ex.InnerException;
                return roster;
            }
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