using GestaHogar.Client;
using GestaHogar.DTO;
using GestaHogar.UI.Views;
using System.Net.Http.Json;

namespace GestaHogar.UI;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        var username = UsernameEntry.Text?.Trim();
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            ErrorLabel.Text = "Todos los campos son obligatorios.";
            ErrorLabel.IsVisible = true;
            return;
        }

        if (password != confirmPassword)
        {
            ErrorLabel.Text = "Las contraseñas no coinciden.";
            ErrorLabel.IsVisible = true;
            return;
        }

        try
        {
            var registerRequest = new
            {
                Username = username,
                Email = email,
                Password = password
            };

            var response = await GHHttpClient.Client.PostAsJsonAsync(GHHttpClient.RegisterUri, registerRequest);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Usuario registrado correctamente.", "OK");

                var loginRequest = new
                {
                    Email = email,
                    Password = password
                };

                response = await GHHttpClient.Client.PostAsJsonAsync(GHHttpClient.LoginUri, loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    Preferences.Set(GHHttpClient.TokenKey, result!.AccessToken);

                    GHHttpClient.Client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(GHHttpClient.AUTH_SCHEME, result.AccessToken);

                    var reponseProducts = await GHHttpClient.Client.GetAsync(GHHttpClient.GetUserProductsUri);

                    if (reponseProducts.IsSuccessStatusCode)
                    {
                        var content = await reponseProducts.Content.ReadFromJsonAsync<List<UserProductDto>>();

                        await Navigation.PushAsync(new UserProducts(content!));
                    }
                    else
                    {
                        ErrorLabel.Text = "Error al obtener sus productos.";
                        ErrorLabel.IsVisible = true;
                    }
                }
            }
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                ErrorLabel.Text = "Error al registrar: " + errorMsg;
                ErrorLabel.IsVisible = true;
            }
        }
        catch
        {
            ErrorLabel.Text = "No se pudo conectar con el servidor.";
            ErrorLabel.IsVisible = true;
        }
    }
}