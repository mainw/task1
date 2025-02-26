using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022UserANLib
{
    public class Calculations
    {
        public static string[] AvailablePeriods(
            TimeSpan[] startTimes,
            int[] durations,
            TimeSpan beginWorkingTime,
            TimeSpan endWorkingTime,
            int consultationTime)
        {
            var intervalEnd = beginWorkingTime + new TimeSpan(0, consultationTime, 0);
            var temp = startTimes.Where(p => p >= beginWorkingTime && p < intervalEnd);
            if (endWorkingTime < intervalEnd)
            {
                return new string[] { };
            }
            if (temp.Count() == 0)
            {
                List<string> strings = new List<string>() { beginWorkingTime.ToString().Substring(0,5) + " - " + intervalEnd.ToString().Substring(0, 5) };
                strings.AddRange(AvailablePeriods(startTimes, durations, intervalEnd, endWorkingTime, consultationTime));
                return strings.ToArray();
            }
            else
            {
                var beginNewTime = temp.FirstOrDefault();
                beginNewTime = beginNewTime.Add(new TimeSpan(0, durations[Array.IndexOf(startTimes, beginNewTime)], 0));
                return AvailablePeriods(startTimes, durations, beginNewTime, endWorkingTime, consultationTime);
            }
        }
    }
}
