using System;
using System.Collections.Generic;

namespace Hotelapp
{
    public static class BookingManager
    {
        private static readonly List<BookingModel> bookings = new List<BookingModel>();

        public static void AddBooking(BookingModel booking)
        {
            bookings.Add(booking);
        }

        public static List<BookingModel> GetBookings()
        {
            return bookings;
        }
    }

    public class BookingModel
    {
        public string HotelName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Guests { get; set; }
        public int Rooms { get; set; }
        public decimal EstimatedPrice { get; set; }
        public string BedType { get; set; }

        // Display information for the booking
        public string DisplayInfo =>
            $"Hotel: {HotelName}, Check-In: {CheckInDate:d}, Check-Out: {CheckOutDate:d}, Guests: {Guests}, Rooms: {Rooms}, Bed Type: {BedType}, Estimated Price: {EstimatedPrice:C}";
    }
}
