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



namespace Geocore
{
   // public static class Settings
   public class Settings
    {
        class GEO
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
            var geogenmobpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"geosettings.conf");
            using (var conf = File.CreateText(geogenmobpath))
            {
               conf.WriteLine(initjson);
            }
        }

        //Читаем файл JSON и возвращаем его содержимое ввиде строки
        private static string printJSON()
        //public async Task<string> printJSON()
        {
            string urlline = "";
            //string path = @"settings.conf";
            string line = "";

            var geogenmobpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "geosettings.conf");
            if (geogenmobpath == null || !File.Exists(geogenmobpath))
            {
                return "File not found";
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

        public static void SendGEO(object n)
        //public static async void SendGEO(object n)
        {
            Random rnd = new Random();
            //string pathlog = @"sendgeo.log";

            //string url = "https://localhost:44359/home/inputgeoJSON";
            //string url = "http://random-red.ddns.net:62424/home/inputgeoJSON";
            string jsonstr = printJSON();
            GEO testgeo = JsonConvert.DeserializeObject<GEO>(jsonstr);

            int n1 = int.Parse(n.ToString()); //перобразовываем из n - object в n1 int 

            try
            {
                for (int i = 0; i < n1; i++)
                {

                    testgeo.X = rnd.Next(99);

                    testgeo.Y = rnd.Next(99);

                    string json = JsonConvert.SerializeObject(testgeo);
                    Console.WriteLine("JSON: {0}", json);

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
        public static void startSend(int n0)
        {            
            Thread ThreadGenGC = new Thread(SendGEO);
            ThreadGenGC.Start(n0);
               // Console.WriteLine("Поток ThreadGenGC запущен");
            ThreadGenGC.Join();
            //Console.WriteLine("Повторить? (да - 1,нет - 0)");
            //return "Отправка завершена.";

        }


    }
}