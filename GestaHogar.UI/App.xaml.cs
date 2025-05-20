using System.Net.Http.Json;
using GestaHogar.Client;

namespace GestaHogar.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new MainPage()));
        }

        public override async void CloseWindow(Window window)
        {
            await GHHttpClient.Client.PostAsJsonAsync(GHHttpClient.LogoutUri, new object());
            base.CloseWindow(window);
        }
    }
}