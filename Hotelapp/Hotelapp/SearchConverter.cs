using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Hotelapp
{
    public class SearchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var searchText = value as string;
            var hotels = App.Current.Properties["Hotels"] as List<Hottel>;

            // Return the full list if the search text is empty
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return hotels;
            }

            // Filter the list based on search text
            return hotels?.Where(h => h.Name.ToLower().Contains(searchText.ToLower())).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}