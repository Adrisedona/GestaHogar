using GestaHogar.Models;
using GestaHogar.DTO;
using System.Collections.ObjectModel;

namespace GestaHogar.UI.Views;

public partial class AddUserProduct : ContentPage
{
    private const int COLUMNS = 2;

    public ObservableCollection<Product> ProductsList { get; private set; } = [];
    public ObservableCollection<UserProductDto> UserProductsList { get; private set; } = [];

    public AddUserProduct(List<Product> products, ObservableCollection<UserProductDto> userProducts)
	{
		InitializeComponent();
		ProductsList = [.. products.Where(p => !userProducts.Any(up => up.ProductId == p.Id))];
        UserProductsList = userProducts;
        ProductsList.CollectionChanged += (s, e) => BuildGrid();
        BuildGrid();
    }

    private void BuildGrid()
    {
        int columns = COLUMNS;
        int row = 0, col = 0;

        ProductsGrid.ColumnDefinitions.Clear();
        ProductsGrid.RowDefinitions.Clear();
        for (int i = 0; i < columns; i++)
            ProductsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

        int totalRows = (ProductsList.Count + 1) / columns;
        for (int i = 0; i < totalRows; i++)
            ProductsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        foreach (var product in ProductsList)
        {
            var button = new Button
            {
                Text = product.Name,
                Margin = new Thickness(5)
            };
            button.Clicked += (s, e) => OnProductClicked(product);

            ProductsGrid.Add(button, col, row);

            col++;
            if (col >= columns)
            {
                col = 0;
                row++;
            }
        }

        if (col >= columns)
        {
            col = 0;
            row++;
            ProductsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        var addButton = new Button
        {
            Text = "Agregar nuevo producto",
            Margin = new Thickness(5)
        };
        addButton.Clicked += OnAddProductClicked;
        ProductsGrid.Add(addButton, col, row);
    }

    private async void OnProductClicked(Product product)
    {
        await Navigation.PushAsync(new FormUserProduct(this.UserProductsList ,new UserProductDto(product), true));
    }

    private async void OnAddProductClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddProduct(this.ProductsList));
    }
}