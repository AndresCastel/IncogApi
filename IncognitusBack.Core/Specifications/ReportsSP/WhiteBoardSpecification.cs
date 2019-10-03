using IncognitusBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncognitusBack.Core.Specifications.ReportsSP
{
    public class WhiteBoardSpecification : BaseSpecification<WhiteboardActual>
    {
        public WhiteBoardSpecification(DateTime Date)
           : base(o => o.Date == Date)
        {

        }
    }
}
