using Microsoft.Extensions.DependencyInjection;

namespace AppMinhasCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
       MainPage = new NavigationPage(new Views.Inicial());
        }
    }
}