using System;
using Xamarin.Forms;
using System.Data.SqlClient;
using Xamarin.Forms.Xaml;

namespace Hotelapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // Event handler for the Login button
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            // Validate input
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Both email and password must be filled in.", "OK");
                return;
            }

            using (SqlConnection connection = new SqlConnection("Data Source=HPElitebook840;Initial Catalog=HotelAppDB;Integrated Security=True;"))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)await command.ExecuteScalarAsync();

                        if (count == 1)
                        {
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            await DisplayAlert("Error", "Invalid email or password.", "OK");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        // Event handler for the Create Account button
        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccountPage());
        }

        // Event handler for the Forgot Password label
        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            await DisplayAlert("Forgot Password", "Feature not implemented yet.", "OK");
        }
    }
}
