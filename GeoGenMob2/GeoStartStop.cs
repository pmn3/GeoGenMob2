using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GeoGenMob2
{
    public class GeoStartStop
    {
        bool STOP;
        int period;
        int test;

        public void stopgeo(bool stop0)
        {
            STOP = stop0;
        }

        public void periodstart(int period0)
        {
            //переводим из секунд в милисекунды
            period = period * 1000; 
        }
    }
}