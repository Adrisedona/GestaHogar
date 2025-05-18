using System.Net.Http.Json;
using GestaHogar.Client;
using GestaHogar.DTO;
using GestaHogar.UI.Views;

namespace GestaHogar.UI;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ErrorLabel.Text = "Usuario y contrase�a requeridos.";
            ErrorLabel.IsVisible = true;
            return;
        }

        try
        {
            var loginRequest = new
            {
                Username = username,
                Password = password
            };

            var response = await GHHttpClient.Client.PostAsJsonAsync(GHHttpClient.LoginUri, loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                // Guarda el token JWT para futuras peticiones
                Preferences.Set(GHHttpClient.TokenKey, result!.Token);

                GHHttpClient.Client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(GHHttpClient.AUTH_SCHEME, result.Token);

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
            else
            {
                ErrorLabel.Text = "Usuario o contrase�a incorrectos.";
                ErrorLabel.IsVisible = true;
            }
        }
        catch (Exception)
        {
            ErrorLabel.Text = "Error de conexi�n con el servidor.";
            ErrorLabel.IsVisible = true;
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register());
    }
}