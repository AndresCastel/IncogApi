using System;
using System.Collections.Generic;
using System.Text;

namespace IncogUtils
{
   public class General
    {
        public static DateTime CastStringtoDateTime(string date)
        {
            DateTime oDate = DateTime.ParseExact(date, "yyyy-MM-dd", null);
            return oDate;
        }
    }
}
