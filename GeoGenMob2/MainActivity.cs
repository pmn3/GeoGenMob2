using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Locations;

using Xamarin.Essentials;

using Geocore;

using GeoRecive;
using Android.Content;

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
            //SetContentView(Resource.Layout.layout1);
            //==========
            //LocationManager locationManager;
            //===========
            EditText countertext = FindViewById<EditText>(Resource.Id.textInputEditText1);
            EditText periodtext = FindViewById<EditText>(Resource.Id.textInputEditText2);

            TextView printtext = FindViewById<TextView>(Resource.Id.textView1);
            TextView printmess = FindViewById<TextView>(Resource.Id.textView2); //инфо1
            TextView info2 = FindViewById<TextView>(Resource.Id.textView5);



            Button startbutton = FindViewById<Button>(Resource.Id.button1);
            Button geobutton = FindViewById<Button>(Resource.Id.button2);
            Button StartGEObutton = FindViewById<Button>(Resource.Id.button3);
            Button StopGEObutton  = FindViewById<Button>(Resource.Id.button4);
            Button Settingsbutton = FindViewById<Button>(Resource.Id.button5);


            //==========

            //==========           
            startbutton.Click += (sender, e) =>
            {
                // int n0 = int.Parse(countertext.Text);
                //countertext.Text = "2";
                int n0;
                if (int.TryParse(countertext.Text, out n0))
                {
                    Geocore.Settings.startSend(n0);
                }
                else
                {
                    periodtext.Text = "В поле period укажите число.";
                }

            };

            GeoStartStop GS = new GeoStartStop();
            StartGEObutton.Click += (sender, e) =>
            {
                // int n0 = int.Parse(countertext.Text);
                //countertext.Text = "2";
                int m0;
                if (int.TryParse(periodtext.Text, out m0))
                {
                    GS.StartGeo(m0);
                    info2.Text = "Начал отправку координат";
                }
                else
                {
                    info2.Text = "В поле  укажите число.";
                }

            };



            StopGEObutton.Click += (sender, e) =>
            {
                // int n0 = int.Parse(countertext.Text);
                //countertext.Text = "2";
                GS.StopGeo();
                info2.Text = GS.mess();


            };

            GeoCoordinates GC = new GeoCoordinates();
            GC.InitGeoCoordinates();

            geobutton.Click += (sender, e) =>
            {
                GC.InitGeoCoordinates();
                double X0Latitude = GC.printXGeoCoordinates();
                double Y0Longitude = GC.printYGeoCoordinates();

                printmess.Text = X0Latitude.ToString() + ";" + Y0Longitude.ToString();

            };

            Settingsbutton.Click += (sender, e)=>
            {
                //SetContentView(Resource.Layout.layout_settings);
                Intent intent = new Intent(this, typeof(SettingsLayout));
                StartActivity(intent);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}