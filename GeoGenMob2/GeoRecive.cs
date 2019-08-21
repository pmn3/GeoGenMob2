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

        public async Task<(double XLatitude, double YLongitude)> ReciveGeo()
        {
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
        }
    }
}