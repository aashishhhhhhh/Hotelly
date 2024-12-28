using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hotelapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage : ContentPage
    {
        // Use 10.0.2.2 for the Android Emulator to access your local machine
        private const string BaseApiUrl = "http://10.0.2.2:5069/api/users/";

        public CreateAccountPage()
        {
            InitializeComponent();
        }

        // Event handler for the Sign Up button
        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            // Validate input
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "All fields must be filled in.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            if (!IsValidEmail(email))
            {
                await DisplayAlert("Error", "Invalid email format.", "OK");
                return;
            }

            if (!IsValidPassword(password))
            {
                await DisplayAlert("Error", "Password must be at least 8 characters long, contain at least one uppercase letter, and one digit.", "OK");
                return;
            }

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BaseApiUrl);

                    var newUser = new
                    {
                        Email = email,
                        Username = username,
                        PasswordHash = password // You may want to hash the password on the client side before sending
                    };

                    string jsonContent = JsonConvert.SerializeObject(newUser);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("register", content);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Success", "Account created successfully!", "OK");
                        await Navigation.PopAsync(); // Navigate back to the previous page after success
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to create an account. " + response.ReasonPhrase, "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        // Event handler for the Back button
        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Navigate back to the previous page
        }

        // Method to validate email format
        private bool IsValidEmail(string email)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        // Method to validate password strength
        private bool IsValidPassword(string password)
        {
            return password.Length >= 8 &&
                   System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]") && // At least one uppercase letter
                   System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]");  // At least one digit
        }
    }
}
