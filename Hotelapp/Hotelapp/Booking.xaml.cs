using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Hotelapp
{
    public partial class Booking : ContentPage
    {
        private string hotelName;
        private Dictionary<string, decimal> bedTypeRates;

        public Booking(string selectedHotelName, List<string> hotelNames)
        {
            InitializeComponent();
            PopulateHotelPicker(hotelNames);
            HotelPicker.SelectedItem = selectedHotelName;
            InitializeBedTypeRates(); // Initialize bed type rates
        }

        public Booking(string hotelName)
        {
            this.hotelName = hotelName;
            InitializeBedTypeRates();
        }

        private void InitializeBedTypeRates()
        {
            bedTypeRates = new Dictionary<string, decimal>
            {
                { "Single", 50.00m },
                { "Double", 75.00m },
                { "Triple", 100.00m }
            };
        }

        private void PopulateHotelPicker(List<string> hotels)
        {
            HotelPicker.ItemsSource = hotels;
        }

        private async void OnConfirmBookingClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GuestsEntry.Text) || !int.TryParse(GuestsEntry.Text, out int guests) || guests <= 0)
            {
                await DisplayAlert("Invalid Input", "Please enter a valid number of guests.", "OK");
                return;
            }

            if (RoomsPicker.SelectedItem == null)
            {
                await DisplayAlert("Invalid Input", "Please select the number of rooms.", "OK");
                return;
            }

            if (BedTypePicker.SelectedItem == null)
            {
                await DisplayAlert("Invalid Input", "Please select a bed type.", "OK");
                return;
            }

            var booking = new BookingModel
            {
                HotelName = HotelPicker.SelectedItem?.ToString(),
                CheckInDate = CheckInDatePicker.Date,
                CheckOutDate = CheckOutDatePicker.Date,
                Guests = guests,
                Rooms = int.Parse(RoomsPicker.SelectedItem.ToString()),
                BedType = BedTypePicker.SelectedItem.ToString(),
                EstimatedPrice = CalculateEstimatedPrice(guests, int.Parse(RoomsPicker.SelectedItem.ToString()), BedTypePicker.SelectedItem.ToString(), CheckInDatePicker.Date, CheckOutDatePicker.Date)
            };

            BookingManager.AddBooking(booking);

            await DisplayAlert("Booking Confirmed", "Your booking has been confirmed!", "OK");
            await Navigation.PopAsync();
        }

        private decimal CalculateEstimatedPrice(int guests, int rooms, string bedType, DateTime checkInDate, DateTime checkOutDate)
        {
            decimal pricePerRoomPerNight = 100.00m;
            decimal bedTypeRate = bedTypeRates.ContainsKey(bedType) ? bedTypeRates[bedType] : 0.00m;

            // Calculate number of nights stayed
            int nightsStayed = (checkOutDate - checkInDate).Days;

            if (nightsStayed < 1)
            {
                return 0m; // Invalid stay duration
            }

            return rooms * (pricePerRoomPerNight + bedTypeRate) * nightsStayed;
        }

        private void UpdateEstimatedPrice()
        {
            if (RoomsPicker.SelectedItem != null && BedTypePicker.SelectedItem != null && int.TryParse(GuestsEntry.Text, out int guests))
            {
                int rooms = int.Parse(RoomsPicker.SelectedItem.ToString());
                string bedType = BedTypePicker.SelectedItem.ToString();
                DateTime checkInDate = CheckInDatePicker.Date;
                DateTime checkOutDate = CheckOutDatePicker.Date;

                EstimatedPriceLabel.Text = $"Estimated Price: {CalculateEstimatedPrice(guests, rooms, bedType, checkInDate, checkOutDate):C}";
            }
            else
            {
                EstimatedPriceLabel.Text = "Price will be calculated after booking";
            }
        }

        private void GuestsEntry_TextChanged(object sender, TextChangedEventArgs e) => UpdateEstimatedPrice();
        private void RoomsPicker_SelectedIndexChanged(object sender, EventArgs e) => UpdateEstimatedPrice();
        private void BedTypePicker_SelectedIndexChanged(object sender, EventArgs e) => UpdateEstimatedPrice();

        // Event handlers for DatePickers

        private void CheckInDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var selectedCheckInDate = e.NewDate;

            // Ensure the Check-Out Date is not earlier than Check-In Date
            if (CheckOutDatePicker.Date < selectedCheckInDate)
            {
                CheckOutDatePicker.Date = selectedCheckInDate;
            }

            // Update the minimum date for Check-Out to be the Check-In date
            CheckOutDatePicker.MinimumDate = selectedCheckInDate;
            UpdateEstimatedPrice(); // Update the price when Check-In date changes
        }

        private void CheckOutDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var selectedCheckOutDate = e.NewDate;

            // If the selected check-out date is before the check-in date, reset it
            if (selectedCheckOutDate < CheckInDatePicker.Date)
            {
                CheckOutDatePicker.Date = CheckInDatePicker.Date;
            }
            UpdateEstimatedPrice(); // Update the price when Check-Out date changes
        }
    }
}
