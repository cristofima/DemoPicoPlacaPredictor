using System;
using System.Collections.Generic;

namespace PicoPlacaPredictor.Infrastructure
{
    public class Constants
    {
        public static TimeSpan INIT_TIME = new TimeSpan(5, 0, 0);
        public static TimeSpan FINAL_TIME = new TimeSpan(20, 0, 0);

        public static Dictionary<DayOfWeek, IList<int>> DAYS_PICO_PLACA = new Dictionary<DayOfWeek, IList<int>>()
        {
            {DayOfWeek.Monday,new List<int>{1,2} },
            {DayOfWeek.Tuesday,new List<int>{3,4} },
            {DayOfWeek.Wednesday,new List<int>{5,6} },
            {DayOfWeek.Thursday,new List<int>{7,8} },
            {DayOfWeek.Friday,new List<int>{9,0} }
        };
    }
}