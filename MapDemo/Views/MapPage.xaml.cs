using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.ApplicationModel;

namespace BeThere.Views;


public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

    }

    private async void OnSearchButtonPressed(object sender, EventArgs e)
    {
        string locationName = mapSearchBar.Text;

        var locations = await Geocoding.GetLocationsAsync(locationName);
        var location = locations?.FirstOrDefault();
        if (location != null)
        {
            var mapSpan = MapSpan.FromCenterAndRadius(
                new Location(location.Latitude, location.Longitude),
                Distance.FromKilometers(1)); // Adjust the desired radius for the map view

            mappy.MoveToRegion(mapSpan);
        }

    }
}


