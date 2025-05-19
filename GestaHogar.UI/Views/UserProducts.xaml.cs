using System.Collections.ObjectModel;
using System.Net.Http.Json;
using GestaHogar.Client;
using GestaHogar.DTO;
using GestaHogar.Models;

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
        var response = await GHHttpClient.Client.GetAsync(GHHttpClient.GetProductsUri);

        if (response.IsSuccessStatusCode)
        {
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();

            await Navigation.PushAsync(new AddUserProduct(products!, this.UserProductsList));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener la lista de productos.", "OK");
        }
    }

    private async void OnEditProductClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is UserProductDto product)
        {
            await Navigation.PushAsync(new FormUserProduct(this.UserProductsList, product, false));
        }
    }

    private async void OnDeleteProductClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is UserProductDto product)
        {
            bool confirm = await DisplayAlert(
                "Eliminar producto",
                $"¿Seguro que deseas eliminar {product.ProductName}?",
                "Sí",
                "No"
            );

            if (confirm)
            {
                var response = await GHHttpClient.Client.DeleteAsync(
                    GHHttpClient.DeleteUserProductUri((int)product.ProductId!)
                );

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

    private async void OnPrintListClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PrintList([.. UserProductsList]));
    }

    private async void OnUpdateAllClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Actualizar productos",
            "¿Seguro que desea actualizar todos los productos? "
                + $"Esta operación establecerá el stock actual de todos a su stock normal",
            "Sí",
            "No"
        );

        if (!confirm)
            return;

        var response = await GHHttpClient.Client.PutAsync(
            GHHttpClient.PutUpdateAllUserProductsUri,
            null
        );
        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Todos los productos han sido actualizados.", "OK");
            foreach (var userProduct in UserProductsList)
            {
                userProduct.CurrentStock = userProduct.NormalStock;
            }
        }
        else
        {
            await DisplayAlert("Error", "No se pudo actualizar la lista.", "OK");
        }
    }

    private async void OnUpdateProductClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is UserProductDto userProduct)
        {
            bool confirm = await DisplayAlert(
                "Actualizar producto",
                $"¿Seguro que desea actualizar {userProduct.ProductName}? "
                    + $"Esta operación establecerá su stock actual a su stock normal",
                "Sí",
                "No"
            );

            if (!confirm)
                return;

            var response = await GHHttpClient.Client.PutAsync(
                GHHttpClient.PutUpdateUserProduct((int)userProduct.ProductId!),
                null
            );

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert(
                    "Éxito",
                    $"Producto {userProduct.ProductName} actualizado.",
                    "OK"
                );
                userProduct.CurrentStock = userProduct.NormalStock;
            }
            else
            {
                await DisplayAlert(
                    "Error",
                    $"No se pudo actualizar el producto {userProduct.ProductName}.",
                    "OK"
                );
            }
        }
    }
}
