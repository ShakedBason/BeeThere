using BeThere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BeThere
{
    public class GetLocationService
    {
        readonly bool stopping = false;
        public GetLocationService() { }

        [Obsolete]
        public async Task Run(CancellationToken token)
        {
            await Task.Run(async () =>
            {
                while (!stopping)
                {
                    token.ThrowIfCancellationRequested();
                    try
                    {
                        await Task.Delay(2000); //time count to get location
                        var request = new GeolocationRequest(GeolocationAccuracy.High);
                        var location = await Geolocation.GetLocationAsync(request);
                        if (location != null)
                        {
                            var message = new LocationMessage
                            {
                                Latitude = location.Latitude,
                                Longitude = location.Longitude
                            };


                            Device.BeginInvokeOnMainThread(() =>
                            {
                                MessagingCenter.Send(message, "Location");
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            var errorMessage = new LocationErrorMessage();
                            MessagingCenter.Send(errorMessage, "LocationError");
                        });
                    }
                }
            }, token);
        }
    }
}

