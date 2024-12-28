using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Hotelapp
{
    public partial class MainPage : ContentPage
    {
        private List<string> hotelNames;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnBookHotelClicked(object sender, EventArgs e)
        {
            // Assuming "Hyatt Regency" is the selected hotel. Change this based on actual user selection logic.
            string selectedHotelName = "Hyatt Regency";
            await Navigation.PushAsync(new Booking(selectedHotelName, hotelNames));
        }
        private async void OnViewHotelsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewHotel());
        }

        private async void OnMyBookingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyBookings());
        }

        private async void OnProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }
        
    }
}
