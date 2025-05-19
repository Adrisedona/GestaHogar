using GestaHogar.Client;
using GestaHogar.DTO;
using GestaHogar.Models;
using GestaHogar.Util;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace GestaHogar.UI;

public partial class FormUserProduct : ContentPage
{
    public UserProductDto UserProduct { get; private set; }
    private readonly bool _post;

    public FormUserProduct(UserProductDto userProduct, bool post)
    {
        this.UserProduct = userProduct;
        BindingContext = this;
        _post = post;
        InitializeComponent();
    }

    public string ProductName => UserProduct.ProductName;
    public string Category => UserProduct.Category;
    public UFloat NormalStock
    {
        get => UserProduct.NormalStock;
        set => UserProduct.NormalStock = (UFloat)float.Parse(value.ToString());
    }
    public UFloat CurrentStock
    {
        get => UserProduct.CurrentStock;
        set => UserProduct.CurrentStock = (UFloat)float.Parse(value.ToString());
    }
    public UFloat DailyUse
    {
        get => UserProduct.DailyUse;
        set => UserProduct.DailyUse = (UFloat)float.Parse(value.ToString());
    }
    public EUnit Unit => UserProduct.Unit;

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        UserProduct.DailyUse = float.Parse(this.DailyUseEntry.Text);
        UserProduct.CurrentStock = float.Parse(this.CurrentStockEntry.Text);
        UserProduct.NormalStock = float.Parse(this.NormalStockEntry.Text);

        var response = await (_post
            ? GHHttpClient.Client.PostAsJsonAsync(GHHttpClient.PostUserProductUri, UserProduct.GetUserProduct())
            : GHHttpClient.Client.PutAsJsonAsync(GHHttpClient.PutUserProductUri((int)UserProduct.ProductId!), UserProduct.GetUserProduct())
        );

        if (!response.IsSuccessStatusCode)
        {
            await DisplayAlert("Error", "No se pudo guardar el producto.", "OK");
            return;
        }

        await DisplayAlert("Guardado", "Producto guardado correctamente.", "OK");
        await Navigation.PopAsync();
    }
}