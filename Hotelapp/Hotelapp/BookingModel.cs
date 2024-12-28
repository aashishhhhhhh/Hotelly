using System;
using System.Collections.Generic;
using System.Text;

public class BookingModel
{
    public string HotelName { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int Guests { get; set; }
    public string BedType { get; set; }
    public int Rooms { get; set; } // Ensure this property is defined
    public decimal EstimatedPrice { get; set; } // Ensure this property is defined
}