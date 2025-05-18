using GestaHogar.DTO;
using GestaHogar.PDF;

namespace GestaHogar.UI.Views;

public partial class PrintList : ContentPage
{
    private readonly List<UserProductDto> _userProducts;
    public PrintList(List<UserProductDto> userProducts)
    {
        InitializeComponent();
        _userProducts = userProducts;
    }

    private async void OnPrintClicked(object sender, EventArgs e)
    {
        IEnumerable<UserProductDto> filteredList = _userProducts;
        string name = "ListaCompleta";

        if (LessThanNormalRadio.IsChecked)
        {
            filteredList = _userProducts.Where(p => p.CurrentStock < p.NormalStock);
            name = "ListaNormal";
        }
        else if (LessThanHalfRadio.IsChecked)
        {
            filteredList = _userProducts.Where(p => p.CurrentStock < p.NormalStock / 2);
            name = "ListaMitad";
        }
        else if (ZeroStockRadio.IsChecked)
        {
            filteredList = _userProducts.Where(p => p.CurrentStock == 0);
            name = "ListaCero";
        }

        await DisplayAlert("Imprimir", $"Se imprimirán {filteredList.Count()} productos.", "OK");
        
        await PdfPrinter.PrintPdf(GetSavePdfPath(name), filteredList);
    }

    private string GetSavePdfPath(string name)
    {
        var curPlat = DeviceInfo.Current.Platform;

        if (curPlat == DevicePlatform.iOS || curPlat == DevicePlatform.Android)
        {
            return Path.Combine(FileSystem.AppDataDirectory, "GestaHogar", "PDFs", $"{DateTime.Now:D}_{name}.pdf");
        }
        else if (curPlat == DevicePlatform.WinUI)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GestaHogar", "PDFs", $"{DateTime.Now:D}_{name}.pdf");
        }
        else
        {
            throw new NotSupportedException($"Platform {curPlat} is not supported.");//no deberia lanzar esta excepcion
        }
    }
}