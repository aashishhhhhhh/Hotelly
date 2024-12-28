using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hotelapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewHotel : ContentPage
    {
        private List<Hottel> hotels; // Store the original hotel list

        public ViewHotel()
        {
            InitializeComponent();

            // Initialize the hotel list
            hotels = new List<Hottel>
            {
                // Kathmandu Hotels
                new Hottel { Name = "Hyatt Regency", Location = "Kathmandu", ImageUrl = "https://www.telegraph.co.uk/content/dam/Travel/hotels/asia/nepal/the-pavilions-himalayas-pool-p.jpg", Description = "Luxurious hotel with spa and pool.", Rating = 4.5, PricePerNight = 150 },
                new Hottel { Name = "The Soaltee", Location = "Kathmandu", ImageUrl = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/21/97/e2/ae/hotel-exterior.jpg?w=1200&h=-1&s=1", Description = "Five-star hotel with fine dining.", Rating = 4.7, PricePerNight = 200 },
                new Hottel { Name = "Hotel Annapurna", Location = "Kathmandu", ImageUrl = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/2b/d3/30/81/caption.jpg?w=1200&h=-1&s=1", Description = "Beautiful hotel with mountain views.", Rating = 4.6, PricePerNight = 120 },
                
                // Pokhara Hotels
                new Hottel { Name = "Hotel Grand Holiday", Location = "Pokhara", ImageUrl = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/13/06/3a/af/nepali-ghar-hotel.jpg?w=700&h=-1&s=1", Description = "Comfortable hotel near the lake.", Rating = 4.2, PricePerNight = 90 },
                new Hottel { Name = "Fish Tail Lodge", Location = "Pokhara", ImageUrl = "https://www.remotelands.com/travelogues/app/uploads/2017/11/Pool-Mountains-Bures.jpg", Description = "Stunning views of the Himalayas.", Rating = 4.8, PricePerNight = 180 },
                new Hottel { Name = "Pokhara Grande Hotel", Location = "Pokhara", ImageUrl = "https://www.remotelands.com/storage/media/573/conversions/b130716001-banner-size.jpg", Description = "Luxury hotel with great amenities.", Rating = 4.7, PricePerNight = 150 },
                
                // Jhapa Hotels
                new Hottel { Name = "Hotel Kanchanjunga", Location = "Jhapa", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSroRnnHIMYESotUny3ue83FAsorX3_-CzJbA&s", Description = "Cozy hotel with local cuisine.", Rating = 4.3, PricePerNight = 70 },
                new Hottel { Name = "Buddha Maya Garden", Location = "Jhapa", ImageUrl = "https://assets.cntraveller.in/photos/6351735e19c50fd3ffe4bc95/16:9/w_1024%2Cc_limit/junior-suit-banner.jpeg", Description = "Garden view hotel with peaceful ambiance.", Rating = 4.4, PricePerNight = 80 },
                new Hottel { Name = "Hotel Bhadra", Location = "Jhapa", ImageUrl = "https://www.remotelands.com/travelogues/app/uploads/2017/11/The-Dwarikahotelnepal.jpg", Description = "Friendly service and comfortable stay.", Rating = 4.2, PricePerNight = 60 }
            };

            // Set the initial list to display all hotels
            HotelsListView.ItemsSource = hotels;
        }

        // Event handler for search text changes
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            FilterHotels();
        }

        // Event handler for location picker selection changes
        private void OnLocationChanged(object sender, EventArgs e)
        {
            FilterHotels();
        }

        // Event handler for clearing the search bar
        private void OnClearSearchClicked(object sender, EventArgs e)
        {
            SearchBar.Text = string.Empty;
            FilterHotels();
        }

        // Event handler for clearing the location picker
        private void OnClearLocationClicked(object sender, EventArgs e)
        {
            LocationPicker.SelectedIndex = -1;
            FilterHotels();
        }

        private void FilterHotels()
        {
            var searchText = SearchBar.Text?.ToLower();
            var selectedLocation = LocationPicker.SelectedItem?.ToString();

            IEnumerable<Hottel> filteredHotels = hotels;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredHotels = filteredHotels.Where(h => h.Name.ToLower().Contains(searchText) || h.Location.ToLower().Contains(searchText));
            }

            if (!string.IsNullOrEmpty(selectedLocation))
            {
                filteredHotels = filteredHotels.Where(h => h.Location.Equals(selectedLocation, StringComparison.OrdinalIgnoreCase));
            }

            HotelsListView.ItemsSource = filteredHotels.ToList();
        }

        // Event handler for "View Details" button click
        private async void OnViewDetailsClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedHotel = button?.CommandParameter as Hottel;

            if (selectedHotel != null)
            {
                await Navigation.PushAsync(new HotelDetails(selectedHotel));
            }
            else
            {
                await DisplayAlert("Error", "Unable to view details for the selected hotel.", "OK");
            }
        }
    }
}
