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

using Xamarin.Essentials;


namespace GeoRecive 
{
    public class GeoCoordinates
    {

        public async Task<(double X1Latitude, double Y1Longitude)> ReciveGeo()
       // public async Task<string> ReciveGeo()
        {
           // string result="";
            double XLatitude = 0;
            double YLongitude = 0;
            var coordinates = (XLatitude, YLongitude);
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                   coordinates.XLatitude = location.Latitude;
                   coordinates.YLongitude = location.Longitude;
                   // result = coordinates.XLatitude.ToString() + ";" + coordinates.YLongitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            return coordinates;
           // return result;
        }
    }
}