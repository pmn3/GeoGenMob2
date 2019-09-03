using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

using GeoRecive;
using GeoGenMob2;
using Android.Widget;

namespace Geocore
{
    // public static class Settings
    public class Settings
    {
        public class GEO
        {
            public string url0;
            public string nameID;
            public string geonamedevice;
            public double X;
            public double Y;
        }       

                //SaveJSON  - Сохраняем настройки в файл JSON (Сереализация)
                public static void SaveJSON(string url1, string user1, string dev1)
        // public async Task SaveJSON(string url1, string user1, string dev1)
        {
            GEO initgeo = new GEO();
            initgeo.url0 = url1;
            initgeo.nameID = user1;
            initgeo.geonamedevice = dev1;

            string initjson = JsonConvert.SerializeObject(initgeo);

            //string path = @"settings.conf";
            // тут происходит неведомая хрень по выделению места под файл geosetting.conf
            var geogenmobpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "geosettings.conf");
            using (var conf = File.CreateText(geogenmobpath))
            {
                conf.WriteLine(initjson);
            }
        }

        //Читаем файл JSON и возвращаем его содержимое ввиде строки (urlline)
        public static string printJSON()
        //public async Task<string> printJSON()
        {
            string urlline = "";
            //string path = @"settings.conf";
            string line = "";

            var geogenmobpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "geosettings.conf");
            if (geogenmobpath == null || !File.Exists(geogenmobpath))
            {
                //return "File not found";
                urlline = "файл с настройками пуст";
            }
            using (var conf = File.OpenText(geogenmobpath))
            {
                while (line != null)
                {
                    line = conf.ReadLine();
                    //Console.WriteLine(line);
                    if (line != null)
                    {
                        urlline = line;
                    }
                }
            }
            return urlline;
        }

        //Десериализуем полученную строку из файла JSON и
        // отправляем на сервер

        // public static void SendGEO(object n)
        public static async Task SendGEO(object n)
        {
            try
            {
               // Random rnd = new Random();
                //string url = "https://localhost:44359/home/inputgeoJSON";
                //string url = "http://random-red.ddns.net:62424/home/inputgeoJSON";
                string jsonstr = printJSON();
                GEO testgeo = JsonConvert.DeserializeObject<GEO>(jsonstr);

                GeoCoordinates GCcore = new GeoCoordinates();
                await GCcore.InitGeoCoordinates();
                int n1 = int.Parse(n.ToString()); //перобразовываем из n - object в n1 int   
                                                  //try
                                                  //{
                                                  //GeoCoordinates GCcore = new GeoCoordinates();
                for (int i = 0; i < n1; i++)
                {
                    //Thread.Sleep(2000);
                    Thread.Sleep(1000); //ждём одну секунду

                    await GCcore.InitGeoCoordinates();
                    testgeo.X = GCcore.printXGeoCoordinates();
                    testgeo.Y = GCcore.printYGeoCoordinates();

                    string json = JsonConvert.SerializeObject(testgeo);

                    var httpRequest = (HttpWebRequest)WebRequest.Create(testgeo.url0);
                    httpRequest.Method = "POST";
                    httpRequest.ContentType = "application/json";
                    using (var requestStream = httpRequest.GetRequestStream())
                    using (var writer = new StreamWriter(requestStream))
                    {
                        writer.Write(json);
                    }
                    using (var httpResponse = httpRequest.GetResponse())
                    using (var responseStream = httpResponse.GetResponseStream())
                    using (var reader = new StreamReader(responseStream))
                    {
                        string response = reader.ReadToEnd();
                    }
                    //await GCcore.InitGeoCoordinates();

                }
            }
            catch (WebException e)
            {
                var geogenlogpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "geogen.log");
                using (var log = File.CreateText(geogenlogpath))
                {
                    log.WriteLine(e);
                }

                //using (StreamWriter log = File.CreateText(pathlog))
                //{
                //    log.WriteLine(e);
                //    log.WriteLine("-----");
                //}
                // Console.WriteLine(e); //показываем ошибку
                //Console.WriteLine("");
            }
            //Console.WriteLine("===>OK<===");
        }

        //Создаём поток SendGEO
        //public static void startSend(int n0)
        //{            
        //    Thread ThreadGenGC = new Thread(SendGEO);
        //    ThreadGenGC.Start(n0);
        //       // Console.WriteLine("Поток ThreadGenGC запущен");
        //    ThreadGenGC.Join();
        //    //Console.WriteLine("Повторить? (да - 1,нет - 0)");
        //    //return "Отправка завершена.";

        //}



        public static void startSend(int n0)
        {
            SendGEO(n0);
        }
       
        
        
        // TextView messtest = Fin   
        //GeoStartStop GS = new GeoStartStop();

        //public  async Task StartGeo(int m0)
        //{
        //    //GeoStartStop GS = new GeoStartStop();
        //    int N = 0;
        //    GS.initSTART();
        //    while (GS.statusSTOP() != true)
        //    {
        //        await SendGEO(1);
        //        N++;
        //        GS.messagesend = "отправлено " + N + " раз";               
        //        Thread.Sleep(m0);               

        //    }
        //}

        //public static void StartSendGEO(int m1)
        //{
        //    StartGeo(m1);
        //}
        //public static void StopGeo()
        //{
        //    GS.initSTOP();
        //}
    }
}