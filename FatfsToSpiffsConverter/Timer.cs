using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FatfsToSpiffsConverter
{
    public static class Timer
    {
        /// <summary>
        /// контроль таймаута приема данных
        /// </summary>
        public static void StartTimer(ElapsedEventHandler handler, out System.Timers.Timer aTimer, double inerval)
        {
            aTimer = new System.Timers.Timer(inerval);
            aTimer.AutoReset = true;
            aTimer.Elapsed += handler;
            aTimer.Start();
        }

        public static void ResetTimer(System.Timers.Timer aTimer)
        {
            aTimer.Stop();
            aTimer.Start();
        }
    }
}
