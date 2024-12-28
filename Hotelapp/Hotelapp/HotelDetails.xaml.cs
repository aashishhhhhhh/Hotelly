using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Hotelapp
{
    public partial class HotelDetails : ContentPage
    {
        private Hottel selectedHotel;

        public HotelDetails(Hottel selectedHotel)
        {
            InitializeComponent();
            this.selectedHotel = selectedHotel ?? new Hottel { Name = "Default Hotel" }; // Default if null

            // Set hotel details directly in code-behind
            HotelImage.Source = selectedHotel.ImageUrl;
            HotelName.Text = selectedHotel.Name;
            HotelLocation.Text = selectedHotel.Location;
            HotelRating.Text = $"Rating: {selectedHotel.Rating} ★"; // Example formatting
            HotelPrice.Text = $"Price per Night: ${selectedHotel.PricePerNight:F2}";
            HotelDescription.Text = selectedHotel.Description;

            // Set amenities and contact information
            HotelAmenities.Text = "Free WiFi, Pool, Spa"; // Example amenities; replace with actual values if needed
            HotelContactInfo.Text = "+977 1-1234567"; // Example contact; replace with actual values if needed
            HotelEmail.Text = "info@hotel.com"; // Example email; replace with actual values if needed
        }

        // Event handler for the "Book Now" button
        private async void OnBookNowClicked(object sender, EventArgs e)
        {
            if (selectedHotel != null)
            {
                string selectedHotelName = selectedHotel.Name;

                // Create a list of hotel names (you can customize this as needed)
                List<string> hotelNames = new List<string> { selectedHotelName };

                // Create a new Booking object
                Booking booking = new Booking(selectedHotelName, hotelNames);

                // Navigate to the Booking page with the booking object
                await Navigation.PushAsync(new Booking(selectedHotelName, hotelNames));
            }
            else
            {
                await DisplayAlert("Error", "Hotel information is missing.", "OK");
            }
        }
    }
}
