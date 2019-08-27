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

using Geocore;
using System.Threading;
using System.Threading.Tasks;

namespace GeoGenMob2
{
    public class GeoStartStop
    {
        bool STOP;
        public int period;
        string messagesend;

        public GeoStartStop()
        {
            STOP = false;
            period = 5000; //5- секунд
        }
        public void stopgeo(bool stop0)
        {
            STOP = stop0;
        }

        public void periodstart(int period0)
        {
            //переводим из секунд в милисекунды
            period = period0 * 1000; 
        }
        
        public string mess()
        {
            return messagesend;
        }
        public bool statusSTOP()
        {
            return STOP;
        }

        public void initSTOP()
        {
            STOP = true;
            messagesend = "отправка координат остановлена";
        }
        public void initSTART()
        {
            STOP = false;
            
        }

        public  async Task StartGeo(int m0)
        {
            initSTART();
            periodstart(m0);
            int N = 0;
            while(statusSTOP() != true)
            {
                N++;
                await Geocore.Settings.SendGEO(1);
                messagesend = "отправил " + N + "раз";
                Thread.Sleep(period);
            }
        }

        public void StopGeo()
        {
            initSTOP();
        }

    }

}