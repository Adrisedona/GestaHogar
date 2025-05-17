namespace GestaHogar.UI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, e) => await Navigation.PushAsync(new Login());
            RootGrid.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
