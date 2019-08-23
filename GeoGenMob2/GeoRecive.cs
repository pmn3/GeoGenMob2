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

       public double XLatitude=0;
       public double YLongitude=0;
       
       public void InitGeoCoordinates()
        {
            ReciveGeo();
        }

        public double printXGeoCoordinates()
        {
            double Xp = XLatitude;
            return Xp;
            
        }
        public double printYGeoCoordinates()
        {
            double Yp = YLongitude;
            return Yp;

        }

        public async Task ReciveGeo()
            {

                try
                {
                    //var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);

                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        XLatitude = location.Latitude;
                        YLongitude = location.Longitude;
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
                // return coordinates;
                // return result;
            }
        
    }
}