using GestaHogar.Client;
using GestaHogar.DTO;
using System.Collections.ObjectModel;

namespace GestaHogar.UI.Views;

public partial class UserProducts : ContentPage
{
    public ObservableCollection<UserProductDto> UserProductsList { get; private set; }
    public UserProducts(List<UserProductDto> userProducts)
    {
        InitializeComponent();
        UserProductsList = [.. userProducts];
        BindingContext = this;
    }

    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FormUserProduct());
    }

    private async void OnEditProductClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is UserProductDto product)
        {
            await Navigation.PushAsync(new FormUserProduct(product));
        }
    }

    private async void OnDeleteProductClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is UserProductDto product)
        {
            bool confirm = await DisplayAlert("Eliminar producto",
                $"¿Seguro que deseas eliminar {product}?", "Sí", "No");
            if (confirm)
            {
                var response = await GHHttpClient.Client.DeleteAsync(GHHttpClient.DeleteUserProductUri(product.ProductId));
                if (response.IsSuccessStatusCode)
                {
                    UserProductsList.Remove(product);
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el producto.", "OK");
                }
            }
        }
    }
}