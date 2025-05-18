using GestaHogar.Client;
using GestaHogar.Models;
using GestaHogar.Util;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace GestaHogar.UI.Views;

public partial class AddProduct : ContentPage
{
    private readonly ObservableCollection<Product> _products;

    public AddProduct(ObservableCollection<Product> products)
    {
        InitializeComponent();
        UnitPicker.ItemsSource = Enum.GetValues<EUnit>().ToList();
        _products = products;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            string.IsNullOrWhiteSpace(CategoryEntry.Text) ||
            string.IsNullOrWhiteSpace(AmountEntry.Text) ||
            UnitPicker.SelectedItem is not EUnit selectedUnit)
        {
            await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
            return;
        }

        if (!float.TryParse(AmountEntry.Text, out float amount) || amount < 0)
        {
            await DisplayAlert("Error", "La cantidad debe ser un número positivo.", "OK");
            return;
        }

        var product = new Product
        {
            Name = NameEntry.Text.Trim(),
            Category = CategoryEntry.Text.Trim(),
            Amount = new UFloat(amount),
            Unit = selectedUnit
        };

        var response = await GHHttpClient.Client.PostAsJsonAsync(GHHttpClient.PostProductUri, product);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Producto creado correctamente.", "OK");
            await Navigation.PopAsync();
            _products.Add(product);
        }
        else
        {
            await DisplayAlert("Error", "No se pudo crear el producto.", "OK");
        }
    }
}