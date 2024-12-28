using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hotelapp
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadProfileData();
        }

        private void LoadProfileData()
        {
            
            UserNameLabel.Text = "John Doe";      
            UserEmailLabel.Text = "john@example.com"; 
        }

        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            // Logic for editing profile
            await DisplayAlert("Edit Profile", "Edit Profile option clicked.", "OK");
        }

        private async void OnLogOutClicked(object sender, EventArgs e)
        {
            // Logic for logging out
            bool confirmLogout = await DisplayAlert("Log Out", "Are you sure you want to log out?", "Yes", "No");
            if (confirmLogout)
            {
                // Clear user session or redirect to login page
                await Navigation.PopToRootAsync();
            }
        }
    }
}