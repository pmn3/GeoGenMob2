using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Support.V7.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

using Xamarin.Essentials;
using Geocore;
using GeoRecive;

namespace GeoGenMob2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    //class SettingsLayout : AppCompatActivity
    class SettingsLayout : MainActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.layout_settings);
            //========
            EditText URLtext = FindViewById<EditText>(Resource.Id.textInputEditText1);
            EditText Usertext = FindViewById<EditText>(Resource.Id.textInputEditText2);
            EditText Devtext = FindViewById<EditText>(Resource.Id.textInputEditText3);

            Button Savebutton = FindViewById<Button>(Resource.Id.button1);
            Button Backbutton = FindViewById<Button>(Resource.Id.button2);

            TextView CurrentSettingstext = FindViewById<TextView>(Resource.Id.textView4);


            //========
            //===кнопки===

            CurrentSettingstext.Text = TestSettings();

            Savebutton.Click += (sender, e) =>
            {
                Geocore.Settings.SaveJSON(URLtext.Text, Usertext.Text, Devtext.Text); // сохраняем файл
                CurrentSettingstext.Text = TestSettings();

                //string printjsonstr = Settings.printJSON(); //получаем сохранённый JSON
                //if (printjsonstr != null)
                //{
                //    Settings.GEO PrintGeoSet = JsonConvert.DeserializeObject<Settings.GEO>(printjsonstr); //разбираем JSON
                //    CurrentSettingstext.Text = "url: " + PrintGeoSet.url0 + "\n" + "user: " + PrintGeoSet.nameID + "\n" + "dev: " + PrintGeoSet.geonamedevice;
                //}
                //else
                //{
                //    CurrentSettingstext.Text = "Укажите настройки.";
                //}
            };



            Backbutton.Click += (sender, e) =>
            {
                SetContentView(Resource.Layout.activity_main);
            };
            //============
        }
        public string TestSettings()
        {
            string text = "";
            string printjsonstr = Settings.printJSON(); //получаем сохранённый JSON
            if (printjsonstr != null)
            {
                Settings.GEO PrintGeoSet = JsonConvert.DeserializeObject<Settings.GEO>(printjsonstr); //разбираем JSON
                text = "url: " + PrintGeoSet.url0 + "\n" + "user: " + PrintGeoSet.nameID + "\n" + "dev: " + PrintGeoSet.geonamedevice;
                //CurrentSettingstext.Text = "url: " + PrintGeoSet.url0 + "\n" + "user: " + PrintGeoSet.nameID + "\n" + "dev: " + PrintGeoSet.geonamedevice;
            }
            else
            {
                //CurrentSettingstext.Text = "Укажите настройки.";
                text = "Укажите настройки.";
            }
            return text;
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}