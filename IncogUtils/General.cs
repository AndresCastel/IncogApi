using System;
using System.Collections.Generic;
using System.Text;

namespace IncogUtils
{
   public class General
    {
        //public static DateTime CastStringtoDateTime(string date)
        //{
        //    DateTime oDate = DateTime.ParseExact(date, "yyyy/MM/dd", null);
        //    return oDate;
        //}

        public static DateTime SplitCreateDate(string date)
        {
            string[] dtae = new string[date.Length];
            dtae = date.Split('/');
            int year = Convert.ToInt16(dtae[0]);
            int month = Convert.ToInt16(dtae[1]);
            int day = Convert.ToInt16(dtae[2]);
            DateTime datetie = new DateTime(year, month, day);
            DateTime oDate = DateTime.ParseExact(datetie.ToString("yyyy/MM/dd"), "yyyy/MM/dd", null);
            return oDate;
        }
    }
}
