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
        var email = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ErrorLabel.Text = "Usuario y contraseña requeridos.";
            ErrorLabel.IsVisible = true;
            return;
        }

        try
        {
            var loginRequest = new
            {
                Email = email,
                Password = password
            };

            var response = await GHHttpClient.Client.PostAsJsonAsync(GHHttpClient.LoginUri, loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                GHHttpClient.Client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(GHHttpClient.AUTH_SCHEME, result!.AccessToken);

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
                ErrorLabel.Text = "Usuario o contraseña incorrectos.";
                ErrorLabel.IsVisible = true;
            }
        }
        catch (Exception)
        {
            ErrorLabel.Text = "Error de conexión con el servidor.";
            ErrorLabel.IsVisible = true;
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register());
    }
}