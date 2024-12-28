using System;
using System.Linq;
using Xamarin.Forms;

namespace Hotelapp
{
    public partial class MyBookings : ContentPage
    {
        public MyBookings()
        {
            InitializeComponent();
            BookingsListView.ItemsSource = BookingManager.GetBookings(); // Load bookings into ListView
        }
    }
}
