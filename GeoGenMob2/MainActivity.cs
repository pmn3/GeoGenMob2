﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Locations;

using Xamarin.Essentials;

using Geocore;

using GeoRecive;


namespace GeoGenMob2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //==========
            //LocationManager locationManager;
            //===========
            EditText urltext = FindViewById<EditText>(Resource.Id.textInputEditText1);
            EditText usertext = FindViewById<EditText>(Resource.Id.textInputEditText2);
            EditText devtext = FindViewById<EditText>(Resource.Id.textInputEditText3);
            EditText countertext = FindViewById<EditText>(Resource.Id.textInputEditText4);
            TextView printtext = FindViewById<TextView>(Resource.Id.textView5);
            TextView printmess = FindViewById<TextView>(Resource.Id.textView6);


            Button savebutton = FindViewById<Button>(Resource.Id.button1);
            Button startbutton = FindViewById<Button>(Resource.Id.button2);
            Button geobutton = FindViewById<Button>(Resource.Id.button3);

            //==========
            savebutton.Click += (sender, e) =>
            {
                //string url = urltext.Text;
                //printtext.SetText();
                Geocore.Settings.SaveJSON(urltext.Text,usertext.Text,devtext.Text);
                printtext.Text = "url: "+urltext.Text+"\n"+"user: "+usertext.Text+"\n"+"dev: "+devtext.Text;                
            };

            startbutton.Click += (sender, e) =>
            {
                // int n0 = int.Parse(countertext.Text);
                //countertext.Text = "2";
                int n0;
                if (int.TryParse(countertext.Text,out n0))
                {
                    Geocore.Settings.startSend(n0);
                }
                else
                {
                    printmess.Text = "В поле counter укажите число."; 
                }
               
            };

            GeoCoordinates GC = new GeoCoordinates();
            geobutton.Click += (sender, e) =>
            {
                GC.ReciveGeo();
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}