using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IncognitusBack.Core.Interfaces
{
    public interface IReportsService
    {
        Task<TimesheetsReport> GetTimesheetsAsync();
    }
}
